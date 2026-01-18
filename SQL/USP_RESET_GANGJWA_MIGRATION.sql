
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
    ===========================================================================
    Object: StoredProcedure [dbo].[USP_RESET_GANGJWA_MIGRATION]
    Description: 강좌 이관 데이터 초기화 (거래내역 및 이관된 마스터 삭제)
    Created: 2026-01-17
    Author: Antigravity
    
    Parameters:
        @P_CompanyCode : 회사 코드
        
    History:
        2026-01-17 Initial Create
    ===========================================================================
*/
IF OBJECT_ID('[dbo].[USP_RESET_GANGJWA_MIGRATION]', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[USP_RESET_GANGJWA_MIGRATION];
GO

CREATE PROCEDURE [dbo].[USP_RESET_GANGJWA_MIGRATION]
    @P_CompanyCode VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    DECLARE @P_CompanyIdx INT;
    
    BEGIN TRY
        BEGIN TRANSACTION;

        -- 0. 회사 IDX 조회
        SELECT TOP 1 @P_CompanyIdx = F_IDX 
        FROM T_COMPANY 
        WHERE F_COMPANY_CODE = @P_CompanyCode;
        
        IF @P_CompanyIdx IS NULL
        BEGIN
            RAISERROR('Company Not Found', 16, 1);
            RETURN;
        END

        -- 1. 강좌 스냅샷 삭제 (T_ORDER_GANGJWA_MAIN)
        DELETE FROM T_ORDER_GANGJWA_MAIN
        WHERE F_COMPANY_IDX = @P_CompanyIdx
        AND F_COMPANY_CODE = @P_CompanyCode
        AND F_ORDER_IDX IN (
            SELECT F_IDX FROM T_ORDER 
            WHERE F_COMPANY_IDX = @P_CompanyIdx 
            AND F_COMPANY_CODE = @P_CompanyCode
            AND F_ORDER_POS_NO = -1 AND F_ORDER_POS_NAME = 'Migration'
        );

        -- 2. 강좌 정보 상세 삭제 (T_ORDER_DETAIL_GANGJWA_INFO)
        DELETE FROM T_ORDER_DETAIL_GANGJWA_INFO
        WHERE F_COMPANY_IDX = @P_CompanyIdx
        AND F_COMPANY_CODE = @P_CompanyCode
        AND F_ORDER_IDX IN (
            SELECT F_IDX FROM T_ORDER 
            WHERE F_COMPANY_IDX = @P_CompanyIdx 
            AND F_COMPANY_CODE = @P_CompanyCode
            AND F_ORDER_POS_NO = -1 AND F_ORDER_POS_NAME = 'Migration'
        );

        -- 3. 주문 상세 삭제 (T_ORDER_DETAIL)
        DELETE FROM T_ORDER_DETAIL
        WHERE F_COMPANY_IDX = @P_CompanyIdx
        AND F_COMPANY_CODE = @P_CompanyCode
        AND F_ORDER_IDX IN (
            SELECT F_IDX FROM T_ORDER 
            WHERE F_COMPANY_IDX = @P_CompanyIdx 
            AND F_COMPANY_CODE = @P_CompanyCode
            AND F_ORDER_POS_NO = -1 AND F_ORDER_POS_NAME = 'Migration'
        );

        -- 4. 주문 마스터 삭제 (T_ORDER)
        DELETE FROM T_ORDER
        WHERE F_COMPANY_IDX = @P_CompanyIdx 
        AND F_COMPANY_CODE = @P_CompanyCode
        AND F_ORDER_POS_NO = -1 
        AND F_ORDER_POS_NAME = 'Migration';

        -- 5. 강좌 옵션 삭제 (T_GANGJWA_MAIN_CODE_OPTION) - Child of T_GANGJWA_MAIN
        DELETE FROM T_GANGJWA_MAIN_CODE_OPTION
        WHERE F_COMPANY_IDX = @P_CompanyIdx
        AND F_COMPANY_CODE = @P_CompanyCode;

        -- 6. 강좌 금액 삭제 (T_GANGJWA_MAIN_AMOUNT) - Child of T_GANGJWA_MAIN
        DELETE FROM T_GANGJWA_MAIN_AMOUNT
        WHERE F_COMPANY_IDX = @P_CompanyIdx
        AND F_COMPANY_CODE = @P_CompanyCode;

        -- 7. 강좌 마스터 삭제 (T_GANGJWA_MAIN)
        DELETE FROM T_GANGJWA_MAIN
        WHERE F_COMPANY_IDX = @P_CompanyIdx
        AND F_COMPANY_CODE = @P_CompanyCode;
        -- (주의: F_GJ_NAME_IDX='0' 조건 제거하여 전체 삭제. 이 SP는 초기화용이므로 모두 지우는 것이 맞음)

        -- 8. 강좌 코드 삭제 (T_COMPANY_GANGJWA_CODE)
        DELETE FROM T_COMPANY_GANGJWA_CODE
        WHERE F_COMPANY_IDX = @P_CompanyIdx
        AND F_COMPANY_CODE = @P_CompanyCode;

        -- 9. 강좌 분류 삭제 (T_COMPANY_GANGJWA_BUNRYU)
        DELETE FROM T_COMPANY_GANGJWA_BUNRYU
        WHERE F_COMPANY_IDX = @P_CompanyIdx
        AND F_COMPANY_CODE = @P_CompanyCode;

        COMMIT TRANSACTION;
        
        SELECT 'SUCCESS' AS Result, '강좌 이관 데이터가 초기화되었습니다.' AS Msg;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        SELECT 'ERROR' AS Result, ERROR_MESSAGE() AS Msg;
    END CATCH
END
GO
