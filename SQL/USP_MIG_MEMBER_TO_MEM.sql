USE [NEW_DB_NAME] -- ★ 타겟 DB 이름으로 변경 필요
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
    ===========================================================================
    Object: StoredProcedure [dbo].[USP_MIG_MEMBER_TO_MEM]
    Description: 구 DB(T_Member) -> 신 DB(T_MEM, T_MEM_PHOTO) 데이터 이관
    Created: 2026-01-09
    Author: Antigravity
    
    Parameters:
        @OldDbName     : 원본(Old) DB명 (예: 'SPAPT_OLD')
        @CompanyIdx    : 신규 시스템 회사 IDX (INI 설정값)
        @CompanyCode   : 신규 시스템 회사 코드 (INI 설정값)
        
    Usage:
        EXEC USP_MIG_MEMBER_TO_MEM 'SPAPT_거제유로스카이241130', 1, 'APT001'
    ===========================================================================
*/
CREATE OR ALTER PROCEDURE [dbo].[USP_MIG_MEMBER_TO_MEM]
    @OldDbName NVARCHAR(MAX),
    @CompanyIdx INT,
    @CompanyCode VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @Msg NVARCHAR(MAX);

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1. 매핑 정보를 담을 임시 테이블 생성
        -- NewFIdx: 신규 생성된 F_IDX, OldMbNo: 기존 회원번호
        CREATE TABLE #MapTable (
            NewFIdx BIGINT,
            OldMbNo NVARCHAR(50) COLLATE Korean_Wansung_CI_AS
        );

        -- 2. T_MEM (회원기본정보) 이관
        -- OUTPUT 절을 사용하여 Insert된 Identity 값과 원본 Key를 캡처합니다.
        SET @SQL = N'
        INSERT INTO T_MEM (
            F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID,
            F_USER_NO, F_MEMNM, F_MEMID, F_MEMPW,
            F_SEX, F_BIRTH, F_POST, F_ADDR1, F_ADDR2,
            F_HP, F_TEL, F_USER_CARDNO, F_MEMO,
            F_DONG, F_HO,
            F_WDATE, F_UDATE
        )
        OUTPUT INSERTED.F_IDX, INSERTED.F_USER_NO INTO #MapTable(NewFIdx, OldMbNo)
        SELECT
            @P_CompanyIdx, @P_CompanyCode, @P_CompanyCode, /* ID는 Code와 동일하게 처리 (필요시 수정) */
            MbNo, MbName, MbId, MbPass,
            Sex, BirthDate, Post, Juso1, Juso2,
            HpNo, TelNo, MbCardNo, MBMemo,
            DongAddr, HoAddr,
            GETDATE(), GETDATE()
        FROM ' + QUOTENAME(@OldDbName) + N'.dbo.T_Member;
        ';

        EXEC sp_executesql @SQL, 
            N'@P_CompanyIdx INT, @P_CompanyCode VARCHAR(50)', 
            @P_CompanyIdx=@CompanyIdx, @P_CompanyCode=@CompanyCode;
        
        DECLARE @MemCount INT = (SELECT COUNT(*) FROM #MapTable);
        
        -- 3. T_MEM_PHOTO (회원사진) 이관
        -- #MapTable의 NewFIdx와 원본 테이블을 조인하여 사진 데이터 이관
        SET @SQL = N'
        INSERT INTO T_MEM_PHOTO (
            F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID,
            F_MEM_IDX,
            F_PHOTO,
            F_WDATE, F_UDATE
        )
        SELECT
            @P_CompanyIdx, @P_CompanyCode, @P_CompanyCode,
            M.NewFIdx,
            Old.MbSajin,
            GETDATE(), GETDATE()
        FROM #MapTable M
        INNER JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_Member Old ON M.OldMbNo = Old.MbNo
        WHERE Old.MbSajin IS NOT NULL 
          AND DATALENGTH(Old.MbSajin) > 0; -- 빈 이미지 제외
        ';

        EXEC sp_executesql @SQL, 
            N'@P_CompanyIdx INT, @P_CompanyCode VARCHAR(50)', 
            @P_CompanyIdx=@CompanyIdx, @P_CompanyCode=@CompanyCode;
            
        COMMIT TRANSACTION;
        
        -- 임시 테이블 정리
        DROP TABLE #MapTable;

        -- 결과 리턴
        SELECT 'SUCCESS' AS Result, @MemCount AS MigratedCount, 'Migration Complete' AS Message;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        -- 에러 정보 리턴
        SELECT 'ERROR' AS Result, 0 AS MigratedCount, ERROR_MESSAGE() AS Message;
    END CATCH
END
GO
