IF OBJECT_ID('[dbo].[USP_MIG_DONG_HO]', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[USP_MIG_DONG_HO];
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_MIG_DONG_HO]
    @OldDbName NVARCHAR(100),
    @CompanyIdx INT,
    @CompanyCode VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @ErrorMsg NVARCHAR(2000);

    DECLARE @TotalCount INT = 0;
    DECLARE @TmpCount INT = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1. [T_DONG] 이관 (t_aptgb1 -> T_DONG)
        -- 중복 방지: 동일한 CompanyIdx/F_DONG 이 없을 때만 Insert
        -- F_DELYN = '1' 로 고정 (샘플 데이터 기준)
        SET @SQL = N'
        INSERT INTO T_DONG (F_COMPANY_IDX, F_COMPANY_CODE, F_DONG, F_TYPE, F_USEYN, F_DELYN, F_WDATE, F_UPDATE)
        SELECT DISTINCT
            ' + CONVERT(VARCHAR, @CompanyIdx) + ',
            ''' + @CompanyCode + ''',
            A.DongAddr,
            CASE 
                WHEN A.DongAddr NOT LIKE ''%[^0-9]%'' THEN ''N''
                WHEN A.DongAddr NOT LIKE ''%[^a-zA-Z]%'' THEN ''E''
                ELSE ''H''
            END, -- F_TYPE (Calculated)
            ''1'', -- F_USEYN (Active)
            ''1'', -- F_DELYN (Sample says 1)
            GETDATE(),
            GETDATE()
        FROM [' + @OldDbName + '].dbo.t_aptgb1 A
        WHERE NOT EXISTS (
            SELECT 1 FROM T_DONG T 
            WHERE T.F_COMPANY_IDX = ' + CONVERT(VARCHAR, @CompanyIdx) + '
            AND T.F_DONG = A.DongAddr
        )
        AND ISNULL(A.DongAddr, '''') <> '''';
        SET @Cnt = @@ROWCOUNT;
        ';

        EXEC sp_executesql @SQL, N'@Cnt INT OUTPUT', @Cnt = @TmpCount OUTPUT;
        -- 동 정보 카운트는 포함하지 않거나 별도 표기하지만, 여기선 단순 합산 혹은 무시. 
        -- 사용자 요청은 "MigratedCount" 이므로 동/호 합산 혹은 호Count가 중요.
        
        -- 2. [T_DONG_HO] 이관 (t_aptgb2 -> T_DONG_HO)
        -- F_DELYN = '1' 로 고정 (샘플 데이터 기준)
        SET @SQL = N'
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
        FROM [' + @OldDbName + '].dbo.t_aptgb2 A
        WHERE NOT EXISTS (
            SELECT 1 FROM T_DONG_HO T 
            WHERE T.F_COMPANY_IDX = ' + CONVERT(VARCHAR, @CompanyIdx) + '
            AND T.F_DONG = A.DongAddr
            AND T.F_HO = A.HoAddr
        )
        AND ISNULL(A.DongAddr, '''') <> ''''
        AND ISNULL(A.HoAddr, '''') <> '''';
        SET @Cnt = @@ROWCOUNT;
        ';

        EXEC sp_executesql @SQL, N'@Cnt INT OUTPUT', @Cnt = @TotalCount OUTPUT;

        COMMIT TRANSACTION;

        -- 결과 반환
        SELECT 'SUCCESS' AS Result, @TotalCount AS MigratedCount, '동/호 정보 이관이 완료되었습니다.' AS Message;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @ErrorMsg = 'Error in USP_MIG_DONG_HO: ' + ERROR_MESSAGE();
        SELECT 'ERROR' AS Result, 0 AS MigratedCount, @ErrorMsg AS Message;
    END CATCH
END
