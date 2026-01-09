CREATE PROCEDURE [dbo].[USP_MIG_DONG_HO]
    @SourceDbName NVARCHAR(100),
    @CompanyIdx INT,
    @CompanyCode VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @ErrorMsg NVARCHAR(2000);

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1. [T_DONG] 이관 (t_aptgb1 -> T_DONG)
        -- 중복 방지: 동일한 CompanyIdx/F_DONG 이 없을 때만 Insert
        -- F_DELYN = '1' 로 고정 (샘플 데이터 기준)
        SET @SQL = '
        INSERT INTO T_DONG (F_COMPANY_IDX, F_COMPANY_CODE, F_DONG, F_TYPE, F_USEYN, F_DELYN, F_WDATE, F_UPDATE)
        SELECT DISTINCT
            ' + CONVERT(VARCHAR, @CompanyIdx) + ',
            ''' + @CompanyCode + ''',
            A.DongAddr,
            ''E'', -- F_TYPE (Fixed)
            ''1'', -- F_USEYN (Active)
            ''1'', -- F_DELYN (Sample says 1)
            GETDATE(),
            GETDATE()
        FROM [' + @SourceDbName + '].dbo.t_aptgb1 A
        WHERE NOT EXISTS (
            SELECT 1 FROM T_DONG T 
            WHERE T.F_COMPANY_IDX = ' + CONVERT(VARCHAR, @CompanyIdx) + '
            AND T.F_DONG = A.DongAddr
        )
        AND ISNULL(A.DongAddr, '''') <> ''''
        ';

        EXEC sp_executesql @SQL;


        -- 2. [T_DONG_HO] 이관 (t_aptgb2 -> T_DONG_HO)
        -- F_DELYN = '1' 로 고정 (샘플 데이터 기준)
        SET @SQL = '
        INSERT INTO T_DONG_HO (F_COMPANY_IDX, F_COMPANY_CODE, F_DONG, F_HO, F_BUNLI, F_DELYN, F_WDATE, F_UPDATE)
        SELECT DISTINCT
            ' + CONVERT(VARCHAR, @CompanyIdx) + ',
            ''' + @CompanyCode + ''',
            A.DongAddr,
            A.HoAddr,
            ''0'', -- F_BUNLI (Fixed)
            ''1'', -- F_DELYN (Sample says 1)
            GETDATE(),
            GETDATE()
        FROM [' + @SourceDbName + '].dbo.t_aptgb2 A
        WHERE NOT EXISTS (
            SELECT 1 FROM T_DONG_HO T 
            WHERE T.F_COMPANY_IDX = ' + CONVERT(VARCHAR, @CompanyIdx) + '
            AND T.F_DONG = A.DongAddr
            AND T.F_HO = A.HoAddr
        )
        AND ISNULL(A.DongAddr, '''') <> ''''
        AND ISNULL(A.HoAddr, '''') <> ''''
        ';

        EXEC sp_executesql @SQL;

        COMMIT TRANSACTION;

        -- 결과 반환
        SELECT 'SUCCESS' AS RESULT, '동/호 정보 이관이 완료되었습니다.' AS MSG;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @ErrorMsg = 'Error in USP_MIG_DONG_HO: ' + ERROR_MESSAGE();
        SELECT 'FAIL' AS RESULT, @ErrorMsg AS MSG;
    END CATCH
END
