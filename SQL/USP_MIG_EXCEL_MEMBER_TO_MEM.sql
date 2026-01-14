SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
    ===========================================================================
    Object: StoredProcedure [dbo].[USP_MIG_EXCEL_MEMBER_TO_MEM]
    Description: 엑셀 데이터 -> 신 DB(T_MEM) 단건 이관 및 동/호 자동 생성
    Created: 2026-01-14
    Author: Antigravity
    
    Parameters:
        @CompanyCode : 회사 코드
        @Dong        : 동
        @Ho          : 호
        @CardNo      : 카드번호 (F_USER_CARDNO) - F_USER_NO로도 사용됨 (없으면 자동생성)
        @Name        : 입주민 명
        @RegDate     : 등록일자 (YYYY-MM-DD)
        @Phone       : 핸드폰
        @Contact     : 연락처
        @Memo        : 메모
        @Birth       : 생일 (YYYY-MM-DD)
        @Gender      : 성별 (남자/여자)
        
    Usage:
        EXEC USP_MIG_EXCEL_MEMBER_TO_MEM 'APT001', '101', '101', '00001234', '홍길동', ...
    ===========================================================================
*/
IF OBJECT_ID('[dbo].[USP_MIG_EXCEL_MEMBER_TO_MEM]', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[USP_MIG_EXCEL_MEMBER_TO_MEM];
GO

CREATE PROCEDURE [dbo].[USP_MIG_EXCEL_MEMBER_TO_MEM]
    @CompanyCode VARCHAR(50),
    @Dong        VARCHAR(50),
    @Ho          VARCHAR(50),
    @CardNo      VARCHAR(50),
    @Name        NVARCHAR(50),
    @RegDate     VARCHAR(20),
    @Phone       VARCHAR(20),
    @Contact     VARCHAR(20),
    @Memo        NVARCHAR(MAX),
    @Birth       VARCHAR(20),
    @Gender      VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    DECLARE @CompanyIdx INT;
    DECLARE @CompanyId VARCHAR(50);
    DECLARE @DanjiIdx INT;
    DECLARE @UserNo VARCHAR(50);
    DECLARE @SexCode VARCHAR(2);
    DECLARE @CurrentDate DATETIME = GETDATE();
    DECLARE @MemberIdx INT;
    DECLARE @DongType CHAR(1); -- 2026-01-14 F_TYPE 추가
    
    BEGIN TRY
        BEGIN TRANSACTION;

        -- 0. 기본 정보 조회
        SELECT TOP 1 @CompanyIdx = F_IDX, @CompanyId = F_COMPANY_ID
        FROM T_COMPANY 
        WHERE F_COMPANY_CODE = @CompanyCode;
        
        IF @CompanyIdx IS NULL
        BEGIN
            -- 업체 정보가 없으면 오류 처리
            RAISERROR('Invalid Company Code', 16, 1);
            RETURN;
        END

        -- 1. [T_DONG] 확인 및 생성
        -- 동 타입 결정 (N:숫자, E:영문, H:한글/기타)
        IF @Dong NOT LIKE '%[^0-9]%' 
            SET @DongType = 'N';
        ELSE IF @Dong NOT LIKE '%[^a-zA-Z]%'
            SET @DongType = 'E';
        ELSE 
            SET @DongType = 'H';

        -- 동 정보가 없으면 자동 생성
        IF NOT EXISTS (SELECT 1 FROM T_DONG WHERE F_COMPANY_IDX = @CompanyIdx AND F_DONG = @Dong)
        BEGIN
            INSERT INTO T_DONG (F_COMPANY_IDX, F_COMPANY_CODE, F_DONG, F_TYPE, F_USEYN, F_DELYN, F_WDATE, F_UPDATE)
            VALUES (@CompanyIdx, @CompanyCode, @Dong, @DongType, '1', '1', @CurrentDate, @CurrentDate);
        END
        
        -- 2. [T_DONG_HO] 확인 및 생성
        -- 호 정보가 없으면 자동 생성 (분리세대 '0' 고정)
        IF NOT EXISTS (SELECT 1 FROM T_DONG_HO WHERE F_COMPANY_IDX = @CompanyIdx AND F_DONG = @Dong AND F_HO = @Ho)
        BEGIN
            INSERT INTO T_DONG_HO (F_COMPANY_IDX, F_COMPANY_CODE, F_DONG, F_HO, F_BUNLI, F_DELYN, F_WDATE, F_UPDATE)
            VALUES (@CompanyIdx, @CompanyCode, @Dong, @Ho, '0', '1', @CurrentDate, @CurrentDate);
        END
        
        -- 3. 시스템 함수를 이용한 값 설정 (핵심 비즈니스 로직 적용)
        
        -- [단지 IDX] FN_GET_DANJI_IDX 사용
        -- 파라미터: CompanyIdx, CompanyCode, Dong, Ho, Bunli('0')
        SET @DanjiIdx = dbo.FN_GET_DANJI_IDX(@CompanyIdx, @CompanyCode, @Dong, @Ho, '0');
        
        -- [회원 번호] FN_MEM_USER_NO_CREATE 사용 (CardNo가 있으면 CardNo 우선? -> 요구사항 확인 필요하지만 보통 UserNo는 시스템 채번 사용)
        -- 카드번호(F_USER_CARDNO)는 물리적 카드 번호이고, F_USER_NO는 시스템 고유 식별 번호임.
        -- 기존 로직: CardNo가 있으면 그걸 UserNo로 씀 -> 변경: UserNo는 무조건 채번, CardNo는 UserCardNo 컬럼에 저장.
        SET @UserNo = dbo.FN_MEM_USER_NO_CREATE(@CompanyIdx);
        
        -- 성별 변환 (참고 SP 로직 반영)
        IF @Gender = '남자' SET @SexCode = 'M'
        ELSE IF @Gender = '여자' SET @SexCode = 'F'
        ELSE SET @SexCode = ''; -- 빈 값 허용

        -- 날짜 형식 보정
        IF ISNULL(@RegDate, '') = '' SET @RegDate = CONVERT(VARCHAR(10), @CurrentDate, 120);
        IF ISNULL(@Birth, '') = '' SET @Birth = '';

        -- 4. [T_MEM] 회원 등록
        -- 중복 체크 로직: F_COMPANY_CODE + F_USER_NO 기준? 아니면 동/호/이름 중복 체크?
        -- 마이그레이션 특성상 같은 동호에 같은 사람이 있을 수 있으므로 UserNo(PK역할)만 다르면 등록되는 것이 원칙.
        -- 단, 엑셀 재실행시 중복 등록 방지를 위해 '카드번호'나 '동+호+이름' 체크가 필요할 수 있음.
        -- 여기서는 일단 무조건 Insert (UserNo가 매번 새로 따지므로). 
        -- *주의*: 반복 실행 시 계속 쌓일 수 있음. 기존 데이터 초기화 후 실행 권장.
        
        INSERT INTO T_MEM (
            F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID,
            F_USER_NO, F_MEMNM, F_MEMID, F_MEMPW,
            F_SEX, F_BIRTH, 
            F_HP, F_TEL, F_USER_CARDNO, F_MEMO,
            F_DONG, F_HO, F_BUNLI, F_DANJI_IDX, 
            F_JIKWON_YN, -- 직원여부 (기본 0)
            F_WDATE, F_UDATE
        )
        VALUES (
            @CompanyIdx, @CompanyCode, @CompanyId,
            @UserNo, @Name, @UserNo, '', -- ID는 UserNo, PW는 Empty
            @SexCode, @Birth,
            @Phone, @Contact, @CardNo, @Memo,
            @Dong, @Ho, '0', @DanjiIdx,
            0, -- Jikwon_YN
            @RegDate, @CurrentDate
        );
        
        SET @MemberIdx = SCOPE_IDENTITY();

        -- 5. [T_MEM_PHOTO] 필수 생성 (참고 SP 로직 반영)
        INSERT INTO T_MEM_PHOTO (
            F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID, F_MEM_IDX
            -- 사진 데이터는 엑셀에서 오지 않으므로 NULL (기본값)
        )
        VALUES (
            @CompanyIdx, @CompanyCode, @CompanyId, @MemberIdx
        );
            
        SELECT 'SUCCESS' AS Result, 1 AS MigratedCount, 'Inserted Member: ' + @Name AS Message;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        -- 에러 메시지 반환
        SELECT 'ERROR' AS Result, 0 AS MigratedCount, ERROR_MESSAGE() AS Message;
    END CATCH
END
GO
