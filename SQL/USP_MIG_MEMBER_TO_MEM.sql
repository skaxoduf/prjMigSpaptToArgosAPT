

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
IF OBJECT_ID('[dbo].[USP_MIG_MEMBER_TO_MEM]', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[USP_MIG_MEMBER_TO_MEM];
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_MIG_MEMBER_TO_MEM]
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

        -- 0. Company ID 조회
        DECLARE @RealCompanyID VARCHAR(50);
        SELECT TOP 1 @RealCompanyID = F_COMPANY_ID 
        FROM T_COMPANY 
        WHERE F_COMPANY_CODE = @CompanyCode;

        -- 1. 매핑 정보를 담을 임시 테이블 생성
        -- NewFIdx: 신규 생성된 F_IDX, OldMbNo: 기존 회원번호
        CREATE TABLE #MapTable (
            NewFIdx BIGINT,
            OldMbNo NVARCHAR(50) COLLATE Korean_Wansung_CI_AS
        );

        -- 2. T_MEM (회원기본정보) 이관
        -- 2026-01-13 Update: F_COMPANY_ID 매핑, F_DANJI_IDX(T_DONG_HO 매핑)
        SET @SQL = N'
        INSERT INTO T_MEM (
            F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID,
            F_USER_NO, F_MEMNM, F_MEMID, F_MEMPW,
            F_SEX, F_BIRTH, F_POST, F_ADDR1, F_ADDR2,
            F_HP, F_TEL, F_USER_CARDNO, F_MEMO,
            F_DONG, F_HO, F_DANJI_IDX,
            F_WDATE, F_UDATE
        )
        OUTPUT INSERTED.F_IDX, INSERTED.F_USER_NO INTO #MapTable(NewFIdx, OldMbNo)
        SELECT
            @P_CompanyIdx, @P_CompanyCode, @P_RealCompanyID,
            Old.MbNo, Old.MbName, Old.MbId, Old.MbPass,
            Old.Sex, Old.BirthDate, Old.Post, Old.Juso1, Old.Juso2,
            Old.HpNo, Old.TelNo, Old.MbCardNo, Old.MBMemo,
            Old.DongAddr, Old.HoAddr, DH.F_IDX,
            GETDATE(), GETDATE()
        FROM ' + QUOTENAME(@OldDbName) + N'.dbo.T_Member Old
        LEFT JOIN T_DONG_HO DH ON DH.F_COMPANY_CODE = @P_CompanyCode 
                               AND DH.F_DONG = Old.DongAddr 
                               AND DH.F_HO = Old.HoAddr
        WHERE NOT EXISTS (
            SELECT 1 FROM T_MEM M 
            WHERE M.F_COMPANY_CODE = @P_CompanyCode 
              AND M.F_USER_NO = Old.MbNo
        );
        ';

        EXEC sp_executesql @SQL, 
            N'@P_CompanyIdx INT, @P_CompanyCode VARCHAR(50), @P_RealCompanyID VARCHAR(50)', 
            @P_CompanyIdx=@CompanyIdx, @P_CompanyCode=@CompanyCode, @P_RealCompanyID=@RealCompanyID;
        
        DECLARE @MemCount INT = (SELECT COUNT(*) FROM #MapTable);
        
        -- 3. T_MEM_PHOTO (회원사진) 이관
        -- 2026-01-13 Update: Binary -> Base64 String 변환 (F_PHOTO_BASE64)
        SET @SQL = N'
        INSERT INTO T_MEM_PHOTO (
            F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID,
            F_MEM_IDX,
            F_PHOTO_BASE64,
            F_PHOTO,
            F_WDATE, F_UDATE
        )
        SELECT
            @P_CompanyIdx, @P_CompanyCode, @P_RealCompanyID,
            M.NewFIdx,
            CAST(N'''' AS XML).value(''xs:base64Binary(sql:column("CA.MbSajinVarBin"))'', ''VARCHAR(MAX)''),
            CA.MbSajinVarBin,
            GETDATE(), GETDATE()
        FROM #MapTable M
        INNER JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_Member Old ON M.OldMbNo = Old.MbNo
        CROSS APPLY (SELECT CAST(Old.MbSajin AS VARBINARY(MAX)) AS MbSajinVarBin) CA
        WHERE Old.MbSajin IS NOT NULL 
          AND DATALENGTH(Old.MbSajin) > 0;
        ';

        EXEC sp_executesql @SQL, 
            N'@P_CompanyIdx INT, @P_CompanyCode VARCHAR(50), @P_RealCompanyID VARCHAR(50)', 
            @P_CompanyIdx=@CompanyIdx, @P_CompanyCode=@CompanyCode, @P_RealCompanyID=@RealCompanyID;
            
        COMMIT TRANSACTION;
        
        -- 임시 테이블 정리
        DROP TABLE #MapTable;

        -- 결과 리턴
        SELECT 'SUCCESS' AS Result, @MemCount AS MigratedCount, 'Migration Complete (Base64 Photo, Danji Linked)' AS Message;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        -- 에러 정보 리턴
        SELECT 'ERROR' AS Result, 0 AS MigratedCount, ERROR_MESSAGE() AS Message;
    END CATCH
END
GO
