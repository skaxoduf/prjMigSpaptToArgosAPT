SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
    ===========================================================================
    Object: StoredProcedure [dbo].[USP_MIG_OLD_TO_NEW_GANGJWA]
    Description: 구 DB 강좌 데이터(T_SuKangBan, T_TrsInout) -> 신 DB 강좌/주문 통합 이관
    Created: 2026-01-16
    Author: Antigravity
    
    Parameters:
        @CompanyCode : 회사 코드 (신규 시스템 기준)
        @MigrateHistory : 수강/매출 내역 이관 여부 (1:이관, 0:마스터만 생성)
        
    History:
        2026-01-16 Initial Create
    ===========================================================================
*/
IF OBJECT_ID('[dbo].[USP_MIG_OLD_TO_NEW_GANGJWA]', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[USP_MIG_OLD_TO_NEW_GANGJWA];
GO

CREATE PROCEDURE [dbo].[USP_MIG_OLD_TO_NEW_GANGJWA]
    @P_CompanyCode VARCHAR(50),
    @OldDbName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    DECLARE @P_CompanyIdx INT;
    DECLARE @RealCompanyID VARCHAR(50); 
    DECLARE @SQL NVARCHAR(MAX); -- Dynamic SQL Variable
    DECLARE @MigrateHistory BIT = 1; -- 1: Migrate History, 0: Master Only (Default 1)

    DECLARE @CurrentDate DATETIME = GETDATE();
    
    -- 로그 테이블이 없다면 임시 테이블 사용 (또는 PRINT)
    DECLARE @LogTable TABLE (LogMsg NVARCHAR(MAX));

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 0. 회사 정보 조회
        SELECT TOP 1 @P_CompanyIdx = F_IDX, @RealCompanyID = F_COMPANY_ID
        FROM T_COMPANY 
        WHERE F_COMPANY_CODE = @P_CompanyCode;
        
        IF @P_CompanyIdx IS NULL OR @RealCompanyID IS NULL
        BEGIN
            RAISERROR('Invalid Company Code or Company ID not found for the given Company Code.', 16, 1);
            RETURN;
        END

        -------------------------------------------------------------------------
        -- STEP 1. 기초 코드 및 분류 이관 (T_COMPANY_GANGJWA_BUNRYU, CODE)
        -------------------------------------------------------------------------
        -- [대분류] T_GbCode1 -> T_COMPANY_GANGJWA_BUNRYU (대분류, Depth=1)
        SET @SQL = N'
        INSERT INTO T_COMPANY_GANGJWA_BUNRYU (
            F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID,
            F_GangJwaBunRyu_NAME, F_DEPTH, F_PARENT_IDX, F_SORT, 
            F_ALWAYS_YN, F_VIEW_YN, F_DELYN, F_WDATE, F_UPDATE
        )
        SELECT 
            @P_CompanyIdx, @P_CompanyCode, @P_CompanyId,
            GbCodeName1, 1, 0, ROW_NUMBER() OVER(ORDER BY GbCode1),
            ''1'', ''1'', 1, GETDATE(), GETDATE()
        FROM ' + QUOTENAME(@OldDbName) + N'.dbo.T_GbCode1
        WHERE GbCodeName1 NOT IN (SELECT F_GangJwaBunRyu_NAME FROM T_COMPANY_GANGJWA_BUNRYU WHERE F_COMPANY_IDX = @P_CompanyIdx AND F_DEPTH = 1);
        ';
        EXEC sp_executesql @SQL, N'@P_CompanyIdx INT, @P_CompanyCode VARCHAR(50), @P_CompanyId VARCHAR(50)', 
            @P_CompanyIdx=@P_CompanyIdx, @P_CompanyCode=@P_CompanyCode, @P_CompanyId=@RealCompanyID;

        -- [중분류] T_GbCode2 -> T_COMPANY_GANGJWA_BUNRYU (중분류, Depth=2)
        -- Parent IDX를 찾기 위해 Join 필요 (Name 기반 매칭)
        SET @SQL = N'
        INSERT INTO T_COMPANY_GANGJWA_BUNRYU (
            F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID,
            F_GangJwaBunRyu_NAME, F_DEPTH, F_PARENT_IDX, F_SORT, 
            F_ALWAYS_YN, F_VIEW_YN, F_DELYN, F_WDATE, F_UPDATE
        )
        SELECT 
            @P_CompanyIdx, @P_CompanyCode, @P_CompanyId,
            A.GbCodeName2, 2, P.F_IDX, ROW_NUMBER() OVER(ORDER BY A.GbCode2),
            ''1'', ''1'', 1, GETDATE(), GETDATE()
        FROM ' + QUOTENAME(@OldDbName) + N'.dbo.T_GbCode2 A
        JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_GbCode1 B ON A.GbCode1 = B.GbCode1
        JOIN T_COMPANY_GANGJWA_BUNRYU P ON P.F_COMPANY_IDX = @P_CompanyIdx AND P.F_DEPTH = 1 AND P.F_GangJwaBunRyu_NAME = B.GbCodeName1
        WHERE A.GbCodeName2 NOT IN (
            SELECT C.F_GangJwaBunRyu_NAME 
            FROM T_COMPANY_GANGJWA_BUNRYU C 
            WHERE C.F_COMPANY_IDX = @P_CompanyIdx AND C.F_DEPTH = 2 AND C.F_PARENT_IDX = P.F_IDX
        );
        ';
        EXEC sp_executesql @SQL, N'@P_CompanyIdx INT, @P_CompanyCode VARCHAR(50), @P_CompanyId VARCHAR(50)', 
            @P_CompanyIdx=@P_CompanyIdx, @P_CompanyCode=@P_CompanyCode, @P_CompanyId=@RealCompanyID;


        -- [소분류] T_GbCode2 -> T_COMPANY_GANGJWA_BUNRYU (소분류, Depth=3)
        -- 요구사항: 기존 DB의 '소분류(GbCode2)'를 신규 DB의 '중분류'뿐만 아니라 '소분류'로도 사용
        -- Parent IDX는 Depth 2(중분류)의 IDX가 되어야 함
        SET @SQL = N'
        INSERT INTO T_COMPANY_GANGJWA_BUNRYU (
            F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID,
            F_GangJwaBunRyu_NAME, F_DEPTH, F_PARENT_IDX, F_SORT, 
            F_ALWAYS_YN, F_VIEW_YN, F_DELYN, F_WDATE, F_UPDATE
        )
        SELECT 
            @P_CompanyIdx, @P_CompanyCode, @P_CompanyId,
            A.GbCodeName2, 3, P2.F_IDX, ROW_NUMBER() OVER(ORDER BY A.GbCode2), -- Depth 3, Parent=Depth 2
            ''1'', ''1'', 1, GETDATE(), GETDATE()
        FROM ' + QUOTENAME(@OldDbName) + N'.dbo.T_GbCode2 A
        JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_GbCode1 B ON A.GbCode1 = B.GbCode1
        JOIN T_COMPANY_GANGJWA_BUNRYU P1 ON P1.F_COMPANY_IDX = @P_CompanyIdx AND P1.F_DEPTH = 1 AND P1.F_GangJwaBunRyu_NAME = B.GbCodeName1
        JOIN T_COMPANY_GANGJWA_BUNRYU P2 ON P2.F_COMPANY_IDX = @P_CompanyIdx AND P2.F_DEPTH = 2 AND P2.F_GangJwaBunRyu_NAME = A.GbCodeName2 AND P2.F_PARENT_IDX = P1.F_IDX
        WHERE A.GbCodeName2 NOT IN (
            SELECT C.F_GangJwaBunRyu_NAME 
            FROM T_COMPANY_GANGJWA_BUNRYU C 
            WHERE C.F_COMPANY_IDX = @P_CompanyIdx AND C.F_DEPTH = 3 AND C.F_PARENT_IDX = P2.F_IDX
        );
        ';
        EXEC sp_executesql @SQL, N'@P_CompanyIdx INT, @P_CompanyCode VARCHAR(50), @P_CompanyId VARCHAR(50)', 
            @P_CompanyIdx=@P_CompanyIdx, @P_CompanyCode=@P_CompanyCode, @P_CompanyId=@RealCompanyID;

        
        -- 1-2. 옵션 코드 (Options) 생성 - T_SkCode Mapping (Dynamic SQL)
        -- [User Mapping] SkGbCode=5 : 개월 -> Target Type 6
        -- SkGbCode=6 : 요일 -> Target Type 4
        -- SkGbCode=7 : 시간 -> Target Type 5
        
        -- [일반구분1] SkGbCode=1 (헬스PT, GX 등) -> 신규 F_TYPE=1
        SET @SQL = N'
        INSERT INTO T_COMPANY_GANGJWA_CODE (
            F_COMPANY_IDX, F_COMPANY_CODE, F_TYPE, F_TYPE_NAME, 
            F_CODE_NAME, F_SORT, F_DELYN, F_WDATE
        )
        SELECT 
            @P_CompanyIdx, @P_CompanyCode, 1, ''강좌명코드1'',
            LTRIM(RTRIM(SkName)),
            ROW_NUMBER() OVER(ORDER BY SkCode), 1, GETDATE()
        FROM ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode
        WHERE SkGbCode = ''1''
        AND LTRIM(RTRIM(SkName)) NOT IN (SELECT F_CODE_NAME FROM T_COMPANY_GANGJWA_CODE WHERE F_COMPANY_IDX=@P_CompanyIdx AND F_TYPE=1);
        ';
        EXEC sp_executesql @SQL, N'@P_CompanyIdx INT, @P_CompanyCode VARCHAR(50)', @P_CompanyIdx=@P_CompanyIdx, @P_CompanyCode=@P_CompanyCode;

        -- [일반구분2] SkGbCode=2 (횟수) -> 신규 F_TYPE=2
        SET @SQL = N'
        INSERT INTO T_COMPANY_GANGJWA_CODE (
            F_COMPANY_IDX, F_COMPANY_CODE, F_TYPE, F_TYPE_NAME, 
            F_CODE_NAME, F_SORT, F_DELYN, F_WDATE
        )
        SELECT 
            @P_CompanyIdx, @P_CompanyCode, 2, ''강좌명코드2'',
            LTRIM(RTRIM(SkName)),
            ROW_NUMBER() OVER(ORDER BY SkCode), 1, GETDATE()
        FROM ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode
        WHERE SkGbCode = ''2''
        AND LTRIM(RTRIM(SkName)) NOT IN (SELECT F_CODE_NAME FROM T_COMPANY_GANGJWA_CODE WHERE F_COMPANY_IDX=@P_CompanyIdx AND F_TYPE=2);
        ';
        EXEC sp_executesql @SQL, N'@P_CompanyIdx INT, @P_CompanyCode VARCHAR(50)', @P_CompanyIdx=@P_CompanyIdx, @P_CompanyCode=@P_CompanyCode;

        -- [일반구분3] SkGbCode=3(1:1), 4(분) -> 신규 F_TYPE=3
        SET @SQL = N'
        INSERT INTO T_COMPANY_GANGJWA_CODE (
            F_COMPANY_IDX, F_COMPANY_CODE, F_TYPE, F_TYPE_NAME, 
            F_CODE_NAME, F_SORT, F_DELYN, F_WDATE
        )
        SELECT 
            @P_CompanyIdx, @P_CompanyCode, 3, ''강좌명코드3'',
            LTRIM(RTRIM(SkName)),
            ROW_NUMBER() OVER(ORDER BY SkCode), 1, GETDATE()
        FROM ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode
        WHERE SkGbCode IN (''3'', ''4'')
        AND LTRIM(RTRIM(SkName)) NOT IN (SELECT F_CODE_NAME FROM T_COMPANY_GANGJWA_CODE WHERE F_COMPANY_IDX=@P_CompanyIdx AND F_TYPE=3);
        ';
        EXEC sp_executesql @SQL, N'@P_CompanyIdx INT, @P_CompanyCode VARCHAR(50)', @P_CompanyIdx=@P_CompanyIdx, @P_CompanyCode=@P_CompanyCode;

        -- [강좌요일] SkGbCode=6 -> 신규 F_TYPE=4
        SET @SQL = N'
        INSERT INTO T_COMPANY_GANGJWA_CODE (
            F_COMPANY_IDX, F_COMPANY_CODE, F_TYPE, F_TYPE_NAME, 
            F_CODE_NAME, F_SORT, F_DELYN, F_WDATE
        )
        SELECT 
            @P_CompanyIdx, @P_CompanyCode, 4, ''강좌요일'',
            LTRIM(RTRIM(SkName)),
            ROW_NUMBER() OVER(ORDER BY SkCode), 1, GETDATE()
        FROM ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode
        WHERE SkGbCode = ''6''
        AND LTRIM(RTRIM(SkName)) NOT IN (SELECT F_CODE_NAME FROM T_COMPANY_GANGJWA_CODE WHERE F_COMPANY_IDX=@P_CompanyIdx AND F_TYPE=4);
        ';
        EXEC sp_executesql @SQL, N'@P_CompanyIdx INT, @P_CompanyCode VARCHAR(50)', @P_CompanyIdx=@P_CompanyIdx, @P_CompanyCode=@P_CompanyCode;

        -- [강좌시간] SkGbCode=7 -> 신규 F_TYPE=5
        SET @SQL = N'
        INSERT INTO T_COMPANY_GANGJWA_CODE (
            F_COMPANY_IDX, F_COMPANY_CODE, F_TYPE, F_TYPE_NAME, 
            F_CODE_NAME, F_SORT, F_DELYN, F_WDATE
        )
        SELECT 
            @P_CompanyIdx, @P_CompanyCode, 5, ''강좌시간'',
            LTRIM(RTRIM(SkName)),
            ROW_NUMBER() OVER(ORDER BY SkCode), 1, GETDATE()
        FROM ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode
        WHERE SkGbCode = ''7''
        AND LTRIM(RTRIM(SkName)) NOT IN (SELECT F_CODE_NAME FROM T_COMPANY_GANGJWA_CODE WHERE F_COMPANY_IDX=@P_CompanyIdx AND F_TYPE=5);
        ';
        EXEC sp_executesql @SQL, N'@P_CompanyIdx INT, @P_CompanyCode VARCHAR(50)', @P_CompanyIdx=@P_CompanyIdx, @P_CompanyCode=@P_CompanyCode;

        -- [강좌개월] SkGbCode=5 -> 신규 F_TYPE=6
        SET @SQL = N'
        INSERT INTO T_COMPANY_GANGJWA_CODE (
            F_COMPANY_IDX, F_COMPANY_CODE, F_TYPE, F_TYPE_NAME, 
            F_CODE_NAME, F_SORT, F_DELYN, F_WDATE
        )
        SELECT 
            @P_CompanyIdx, @P_CompanyCode, 6, ''강좌개월'',
            LTRIM(RTRIM(REPLACE(SkName, ''개월'', ''''))), -- 숫자만 남기기 (공백제거 추가)
            ROW_NUMBER() OVER(ORDER BY SkCode), 1, GETDATE()
        FROM ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode
        WHERE SkGbCode = ''5''
        AND LTRIM(RTRIM(REPLACE(SkName, ''개월'', ''''))) NOT IN (SELECT F_CODE_NAME FROM T_COMPANY_GANGJWA_CODE WHERE F_COMPANY_IDX=@P_CompanyIdx AND F_TYPE=6);
        ';
        EXEC sp_executesql @SQL, N'@P_CompanyIdx INT, @P_CompanyCode VARCHAR(50)', @P_CompanyIdx=@P_CompanyIdx, @P_CompanyCode=@P_CompanyCode;


        -------------------------------------------------------------------------
        -- STEP 2. 강좌 마스터 이관 (T_GANGJWA_MAIN)
        -------------------------------------------------------------------------
        -- STEP 3. 수강/매출 내역 이관 (T_TrsInout -> T_ORDER...)
        -------------------------------------------------------------------------
        IF @MigrateHistory = 1
        BEGIN
            INSERT INTO @LogTable VALUES ('STEP 3. 수강 내역 이관 시작');

            -- 임시 테이블 사용
            CREATE TABLE #MigOrders (
                OldTrsNo VARCHAR(50),
                NewOrderIdx BIGINT,
                MemIdx BIGINT,
                MemName NVARCHAR(50),
                Amount DECIMAL(18,0),
                GjName NVARCHAR(100),
                SDate VARCHAR(10),
                EDate VARCHAR(10),
                WDate DATETIME,
                Remark NVARCHAR(MAX),
                OrderState INT,
                Bunryu1Name NVARCHAR(50),
                Bunryu2Name NVARCHAR(50),
                Gb1 VARCHAR(10), Nm1 NVARCHAR(50),
                Gb2 VARCHAR(10), Nm2 NVARCHAR(50),
                Gb3 VARCHAR(10), Nm3 NVARCHAR(50),
                Gb4 VARCHAR(10), Nm4 NVARCHAR(50),
                Gb5 VARCHAR(10), Nm5 NVARCHAR(50),
                Gb6 VARCHAR(10), Nm6 NVARCHAR(50),
                Gb7 VARCHAR(10), Nm7 NVARCHAR(50)
            );

        -- 데이터 수집 (Old DB -> #MigOrders)
        -- Logic: 
        --   chkdata='정상' AND tjcode='0001' -> State 1
        --   chkdata='종료' -> State 0
        SET @SQL = N'
        INSERT INTO #MigOrders (OldTrsNo, MemIdx, MemName, Amount, GjName, SDate, EDate, WDate, Remark, OrderState, Bunryu1Name, Bunryu2Name, 
            Gb1, Nm1, Gb2, Nm2, Gb3, Nm3, Gb4, Nm4, Gb5, Nm5, Gb6, Nm6, Gb7, Nm7)
        SELECT 
            A.TrsNo,
            M.F_IDX, -- 이미 이관된 회원 IDX
            ISNULL(M.F_MEMNM, B.MbName),
            A.TotAmt,
            REPLACE(A.SuKangBan, '' ▶ '', '' ''),
            CONVERT(VARCHAR(10), CAST(A.StartDate AS DATE), 120),
            CONVERT(VARCHAR(10), CAST(A.EndDate AS DATE), 120),
            A.TrsDate,
            A.Bigo, 
            CASE 
                WHEN A.chkdata = ''정상'' AND A.tjcode = ''0001'' THEN 1
                WHEN A.chkdata = ''종료'' THEN 0
                ELSE 1 -- 그 외는 일단 정상으로 간주 (또는 필요시 0)
            END,
            G1.GbCodeName1,
            G2.GbCodeName2,
            S1.SkGbCode, S1.SkName,
            S2.SkGbCode, S2.SkName,
            S3.SkGbCode, S3.SkName,
            S4.SkGbCode, S4.SkName,
            S5.SkGbCode, S5.SkName,
            S6.SkGbCode, S6.SkName,
            S7.SkGbCode, S7.SkName
            FROM ' + QUOTENAME(@OldDbName) + N'.dbo.T_TrsInout A
            LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_Member B ON A.MbNo = B.MbNo
            LEFT JOIN T_MEM M ON M.F_USER_NO = A.MbNo AND M.F_COMPANY_CODE = @P_CompanyCode
            LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_GbCode1 G1 ON A.GbCode1 = G1.GbCode1
            LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_GbCode2 G2 ON A.GbCode1 = G2.GbCode1 AND A.GbCode2 = G2.GbCode2
            LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S1 ON A.SkCode1 = S1.SkGbCode + S1.SkCode
            LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S2 ON A.SkCode2 = S2.SkGbCode + S2.SkCode
            LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S3 ON A.SkCode3 = S3.SkGbCode + S3.SkCode
            LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S4 ON A.SkCode4 = S4.SkGbCode + S4.SkCode
            LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S5 ON A.SkCode5 = S5.SkGbCode + S5.SkCode
            LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S6 ON A.SkCode6 = S6.SkGbCode + S6.SkCode
            LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S7 ON A.SkCode7 = S7.SkGbCode + S7.SkCode
            WHERE A.rbcode = ''0001'' -- 강좌 매출만
            AND A.AccChk = ''1'' -- 무조건 필수 (삭제여부: 1=정상, 2=삭제)
        ';
        EXEC sp_executesql @SQL, N'@P_CompanyCode VARCHAR(50)', @P_CompanyCode=@P_CompanyCode;
        
        -------------------------------------------------------------------------
        -- STEP 2. 강좌 마스터 생성 (T_GANGJWA_MAIN)
        -------------------------------------------------------------------------
        -- T_SuKangBan(수강내역)을 기준으로 '강좌 마스터'를 추출하여 생성합니다.
        -- 기존에는 매출내역(#MigOrders)에서 없으면 생성했으나, 매출이 없는(무료/관리자등록) 강좌도 
        -- 마스터로 존재해야 하므로 T_SuKangBan에서 Distinct로 가져옵니다.
        
        -- 임시 테이블로 유니크 강좌 추출
        CREATE TABLE #UniqueCourses (
            GjName NVARCHAR(100),
            Bunryu1Name NVARCHAR(50),
            Bunryu2Name NVARCHAR(50),
            GjType INT,           -- SkChkGb
            JeongWon INT,         -- JungWonCnt
            DayIpjangCnt INT,     -- DayInCnt
            GjTypeCnt INT,        -- SkCnt
            StartDate VARCHAR(10),-- SkSDate
            EndDate VARCHAR(10),  -- SkEDate
            CodeIdx1 INT, CodeIdx2 INT, CodeIdx3 INT, CodeIdx4 INT,
            CodeIdx5 INT, CodeIdx6 INT, CodeIdx7 INT
        );

        SET @SQL = N'
        INSERT INTO #UniqueCourses (GjName, Bunryu1Name, Bunryu2Name, GjType, JeongWon, DayIpjangCnt, GjTypeCnt, StartDate, EndDate,
            CodeIdx1, CodeIdx2, CodeIdx3, CodeIdx4, CodeIdx5, CodeIdx6, CodeIdx7)
        SELECT DISTINCT 
            REPLACE(A.SuKangBan, '' ▶ '', '' ''),
            G1.GbCodeName1,
            G2.GbCodeName2,
            CAST(A.SkChkGb AS INT),
            A.JungWonCnt,
            A.DayInCnt,
            A.SkCnt,
            CONVERT(VARCHAR(10), CAST(A.SkSDate AS DATE), 120),
            CONVERT(VARCHAR(10), CAST(A.SkEDate AS DATE), 120),
            C1.F_IDX, C2.F_IDX, C3.F_IDX, C4.F_IDX, C5.F_IDX, C6.F_IDX, C7.F_IDX
        FROM ' + QUOTENAME(@OldDbName) + N'.dbo.T_SuKangBan A
        LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_GbCode1 G1 ON A.GbCode1 = G1.GbCode1
        LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_GbCode2 G2 ON A.GbCode1 = G2.GbCode1 AND A.GbCode2 = G2.GbCode2
        LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S1 ON A.SkCode1 = S1.SkGbCode + S1.SkCode
        LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S2 ON A.SkCode2 = S2.SkGbCode + S2.SkCode
        LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S3 ON A.SkCode3 = S3.SkGbCode + S3.SkCode
        LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S4 ON A.SkCode4 = S4.SkGbCode + S4.SkCode
        LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S5 ON A.SkCode5 = S5.SkGbCode + S5.SkCode
        LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S6 ON A.SkCode6 = S6.SkGbCode + S6.SkCode
        LEFT JOIN ' + QUOTENAME(@OldDbName) + N'.dbo.T_SkCode S7 ON A.SkCode7 = S7.SkGbCode + S7.SkCode
        LEFT JOIN T_COMPANY_GANGJWA_CODE C1 ON C1.F_COMPANY_IDX = @P_CompanyIdx AND C1.F_CODE_NAME = (CASE WHEN S1.SkGbCode=''5'' THEN REPLACE(S1.SkName, ''개월'', '''') ELSE S1.SkName END) AND C1.F_TYPE = (CASE WHEN S1.SkGbCode=''1'' THEN 1 WHEN S1.SkGbCode=''2'' THEN 2 WHEN S1.SkGbCode IN (''3'',''4'') THEN 3 WHEN S1.SkGbCode=''6'' THEN 4 WHEN S1.SkGbCode=''7'' THEN 5 WHEN S1.SkGbCode=''5'' THEN 6 ELSE 0 END)
        LEFT JOIN T_COMPANY_GANGJWA_CODE C2 ON C2.F_COMPANY_IDX = @P_CompanyIdx AND C2.F_CODE_NAME = (CASE WHEN S2.SkGbCode=''5'' THEN REPLACE(S2.SkName, ''개월'', '''') ELSE S2.SkName END) AND C2.F_TYPE = (CASE WHEN S2.SkGbCode=''1'' THEN 1 WHEN S2.SkGbCode=''2'' THEN 2 WHEN S2.SkGbCode IN (''3'',''4'') THEN 3 WHEN S2.SkGbCode=''6'' THEN 4 WHEN S2.SkGbCode=''7'' THEN 5 WHEN S2.SkGbCode=''5'' THEN 6 ELSE 0 END)
        LEFT JOIN T_COMPANY_GANGJWA_CODE C3 ON C3.F_COMPANY_IDX = @P_CompanyIdx AND C3.F_CODE_NAME = (CASE WHEN S3.SkGbCode=''5'' THEN REPLACE(S3.SkName, ''개월'', '''') ELSE S3.SkName END) AND C3.F_TYPE = (CASE WHEN S3.SkGbCode=''1'' THEN 1 WHEN S3.SkGbCode=''2'' THEN 2 WHEN S3.SkGbCode IN (''3'',''4'') THEN 3 WHEN S3.SkGbCode=''6'' THEN 4 WHEN S3.SkGbCode=''7'' THEN 5 WHEN S3.SkGbCode=''5'' THEN 6 ELSE 0 END)
        LEFT JOIN T_COMPANY_GANGJWA_CODE C4 ON C4.F_COMPANY_IDX = @P_CompanyIdx AND C4.F_CODE_NAME = (CASE WHEN S4.SkGbCode=''5'' THEN REPLACE(S4.SkName, ''개월'', '''') ELSE S4.SkName END) AND C4.F_TYPE = (CASE WHEN S4.SkGbCode=''1'' THEN 1 WHEN S4.SkGbCode=''2'' THEN 2 WHEN S4.SkGbCode IN (''3'',''4'') THEN 3 WHEN S4.SkGbCode=''6'' THEN 4 WHEN S4.SkGbCode=''7'' THEN 5 WHEN S4.SkGbCode=''5'' THEN 6 ELSE 0 END)
        LEFT JOIN T_COMPANY_GANGJWA_CODE C5 ON C5.F_COMPANY_IDX = @P_CompanyIdx AND C5.F_CODE_NAME = (CASE WHEN S5.SkGbCode=''5'' THEN REPLACE(S5.SkName, ''개월'', '''') ELSE S5.SkName END) AND C5.F_TYPE = (CASE WHEN S5.SkGbCode=''1'' THEN 1 WHEN S5.SkGbCode=''2'' THEN 2 WHEN S5.SkGbCode IN (''3'',''4'') THEN 3 WHEN S5.SkGbCode=''6'' THEN 4 WHEN S5.SkGbCode=''7'' THEN 5 WHEN S5.SkGbCode=''5'' THEN 6 ELSE 0 END)
        LEFT JOIN T_COMPANY_GANGJWA_CODE C6 ON C6.F_COMPANY_IDX = @P_CompanyIdx AND C6.F_CODE_NAME = (CASE WHEN S6.SkGbCode=''5'' THEN REPLACE(S6.SkName, ''개월'', '''') ELSE S6.SkName END) AND C6.F_TYPE = (CASE WHEN S6.SkGbCode=''1'' THEN 1 WHEN S6.SkGbCode=''2'' THEN 2 WHEN S6.SkGbCode IN (''3'',''4'') THEN 3 WHEN S6.SkGbCode=''6'' THEN 4 WHEN S6.SkGbCode=''7'' THEN 5 WHEN S6.SkGbCode=''5'' THEN 6 ELSE 0 END)
        LEFT JOIN T_COMPANY_GANGJWA_CODE C7 ON C7.F_COMPANY_IDX = @P_CompanyIdx AND C7.F_CODE_NAME = (CASE WHEN S7.SkGbCode=''5'' THEN REPLACE(S7.SkName, ''개월'', '''') ELSE S7.SkName END) AND C7.F_TYPE = (CASE WHEN S7.SkGbCode=''1'' THEN 1 WHEN S7.SkGbCode=''2'' THEN 2 WHEN S7.SkGbCode IN (''3'',''4'') THEN 3 WHEN S7.SkGbCode=''6'' THEN 4 WHEN S7.SkGbCode=''7'' THEN 5 WHEN S7.SkGbCode=''5'' THEN 6 ELSE 0 END)
        WHERE A.SkDelYb = 0 
        ';
        EXEC sp_executesql @SQL, N'@P_CompanyIdx INT', @P_CompanyIdx=@P_CompanyIdx;

        -- T_GANGJWA_MAIN에 Insert
        -- (수정) Depth 3 (소분류) 까지 매핑
        -- Bunryu2Name이 중분류이자 소분류임 (소분류 IDX Lookup 추가)
        INSERT INTO T_GANGJWA_MAIN (
            F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID,
            F_GJ_NAME, F_GJ_NAME_IDX,
            F_BUNRYU_B_IDX, F_BUNRYU_M_IDX, F_BUNRYU_S_IDX,
            F_BUNRYU_B_NAME, F_BUNRYU_M_NAME, F_BUNRYU_S_NAME,
            F_GJ_START_DATE, F_GJ_END_DATE,
            F_JEOBSU_START_DATE, F_JEOBSU_END_DATE, F_SANGSI_JEOBSU,
            F_GJ_TYPE, F_GJ_TYPE_CNT,
            F_VIEW, F_JEONGWON, F_IPJANG_INTERVAL, F_DAY_IPJANG_CNT, F_GENDER,
            F_SORT, F_DELYN, F_WDATE, F_UPDATE
        )
        SELECT 
            @P_CompanyIdx, @P_CompanyCode, @RealCompanyID,
            A.GjName, 
            STUFF(
                ISNULL(',' + CAST(A.CodeIdx1 AS VARCHAR), '') + 
                ISNULL(',' + CAST(A.CodeIdx2 AS VARCHAR), '') + 
                ISNULL(',' + CAST(A.CodeIdx3 AS VARCHAR), '') + 
                ISNULL(',' + CAST(A.CodeIdx4 AS VARCHAR), '') + 
                ISNULL(',' + CAST(A.CodeIdx5 AS VARCHAR), '') + 
                ISNULL(',' + CAST(A.CodeIdx6 AS VARCHAR), '') + 
                ISNULL(',' + CAST(A.CodeIdx7 AS VARCHAR), ''),
                1, 1, ''
            ), -- F_GJ_NAME_IDX (Comma Separated)
            ISNULL(B1.F_IDX, 0), ISNULL(B2.F_IDX, 0), ISNULL(B3.F_IDX, 0), -- 대/중 매핑, 소분류도 매핑
            ISNULL(A.Bunryu1Name, ''), ISNULL(A.Bunryu2Name, ''), ISNULL(A.Bunryu2Name, ''), -- 소분류명 = 중분류명
            ISNULL(A.StartDate, ''), ISNULL(A.EndDate, ''), -- 날짜 Mapping (Null이면 공백)
            '', '', 'Y', -- 접수기간 (상시접수 Y)
            ISNULL(A.GjType, 6), ISNULL(A.GjTypeCnt, 0), -- Type Mapping, Cnt Mapping
            1, ISNULL(A.JeongWon, 999), 0, ISNULL(A.DayIpjangCnt, 0), 'A', -- View 1 (Y), Jeongwon Mapping
            0, 1, GETDATE(), GETDATE()
        FROM #UniqueCourses A
        LEFT JOIN T_COMPANY_GANGJWA_BUNRYU B1 ON B1.F_COMPANY_IDX = @P_CompanyIdx AND B1.F_DEPTH=1 AND B1.F_GangJwaBunRyu_NAME = A.Bunryu1Name
        LEFT JOIN T_COMPANY_GANGJWA_BUNRYU B2 ON B2.F_COMPANY_IDX = @P_CompanyIdx AND B2.F_DEPTH=2 AND B2.F_GangJwaBunRyu_NAME = A.Bunryu2Name AND B2.F_PARENT_IDX = B1.F_IDX
        LEFT JOIN T_COMPANY_GANGJWA_BUNRYU B3 ON B3.F_COMPANY_IDX = @P_CompanyIdx AND B3.F_DEPTH=3 AND B3.F_GangJwaBunRyu_NAME = A.Bunryu2Name AND B3.F_PARENT_IDX = B2.F_IDX -- 소분류 Lookup
        WHERE A.GjName NOT IN (SELECT F_GJ_NAME FROM T_GANGJWA_MAIN WHERE F_COMPANY_IDX = @P_CompanyIdx);

        DROP TABLE #UniqueCourses;


        -- 커서 변수
        DECLARE @Mig_TrsNo VARCHAR(50), @Mig_MemIdx BIGINT, @Mig_Amount DECIMAL(18,0);
        DECLARE @Mig_MemName NVARCHAR(50), @Mig_GjName NVARCHAR(100);
        DECLARE @Mig_SDate VARCHAR(10), @Mig_EDate VARCHAR(10), @Mig_WDate DATETIME, @Mig_Remark NVARCHAR(MAX);
        DECLARE @Mig_OrderState INT;
        DECLARE @Mig_Bunryu1Name NVARCHAR(50), @Mig_Bunryu2Name NVARCHAR(50);
        DECLARE @G1 VARCHAR(10), @N1 NVARCHAR(50), @G2 VARCHAR(10), @N2 NVARCHAR(50), @G3 VARCHAR(10), @N3 NVARCHAR(50);
        DECLARE @G4 VARCHAR(10), @N4 NVARCHAR(50), @G5 VARCHAR(10), @N5 NVARCHAR(50), @G6 VARCHAR(10), @N6 NVARCHAR(50), @G7 VARCHAR(10), @N7 NVARCHAR(50);
        
        DECLARE @Target_GangjwaIdx INT;
        DECLARE @New_OrderIdx BIGINT, @New_OrderDetailIdx BIGINT, @New_InfoIdx BIGINT;
        DECLARE @New_OrderNo VARCHAR(20); -- 신규 주문번호 변수
        DECLARE @New_BunryuBIdx INT, @New_BunryuMIdx INT; -- 신규 분류 IDX 변수
        DECLARE @New_MonthCodeIdx INT, @New_WeekCodeIdx INT, @New_TimeCodeIdx INT;
        DECLARE @New_MonthCodeName NVARCHAR(50), @New_WeekCodeName NVARCHAR(50), @New_TimeCodeName NVARCHAR(50);
        DECLARE @New_AmountCodeIdx INT, @New_AmountCodeName NVARCHAR(50);

        DECLARE curTrs CURSOR FOR 
            SELECT OldTrsNo, MemIdx, MemName, Amount, GjName, SDate, EDate, WDate, Remark, OrderState, Bunryu1Name, Bunryu2Name,
                   Gb1, Nm1, Gb2, Nm2, Gb3, Nm3, Gb4, Nm4, Gb5, Nm5, Gb6, Nm6, Gb7, Nm7
            FROM #MigOrders
            ORDER BY MemIdx, SDate;

                OPEN curTrs;
            FETCH NEXT FROM curTrs INTO @Mig_TrsNo, @Mig_MemIdx, @Mig_MemName, @Mig_Amount, @Mig_GjName, @Mig_SDate, @Mig_EDate, @Mig_WDate, @Mig_Remark, @Mig_OrderState, @Mig_Bunryu1Name, @Mig_Bunryu2Name,
                   @G1, @N1, @G2, @N2, @G3, @N3, @G4, @N4, @G5, @N5, @G6, @N6, @G7, @N7;

            WHILE @@FETCH_STATUS = 0
            BEGIN
                -- 3-0. [검증] 회원 IDX가 없으면 스킵 (회원 미이관 데이터)
                IF @Mig_MemIdx IS NULL
                BEGIN
                     FETCH NEXT FROM curTrs INTO @Mig_TrsNo, @Mig_MemIdx, @Mig_MemName, @Mig_Amount, @Mig_GjName, @Mig_SDate, @Mig_EDate, @Mig_WDate, @Mig_Remark, @Mig_OrderState, @Mig_Bunryu1Name, @Mig_Bunryu2Name,
                            @G1, @N1, @G2, @N2, @G3, @N3, @G4, @N4, @G5, @N5, @G6, @N6, @G7, @N7;
                     CONTINUE;
                END

                -- 3-1. [중복 체크] 거래 내역 중복 확인
                -- (회원, 강좌명, 시작일, 종료일)
                IF EXISTS (
                    SELECT 1 
                    FROM T_ORDER_DETAIL_GANGJWA_INFO A
                    JOIN T_ORDER_GANGJWA_MAIN B ON A.F_IDX = B.F_ORDER_DETAIL_GANGJWA_INFO_IDX
                    WHERE A.F_COMPANY_IDX = @P_CompanyIdx 
                    AND A.F_MEM_IDX = @Mig_MemIdx 
                    AND B.F_GJ_NAME = @Mig_GjName 
                    AND A.F_START_DATE = @Mig_SDate 
                    AND A.F_END_DATE = @Mig_EDate
                )
                BEGIN
                    -- 중복이면 건너뛰기
                    FETCH NEXT FROM curTrs INTO @Mig_TrsNo, @Mig_MemIdx, @Mig_MemName, @Mig_Amount, @Mig_GjName, @Mig_SDate, @Mig_EDate, @Mig_WDate, @Mig_Remark, @Mig_OrderState, @Mig_Bunryu1Name, @Mig_Bunryu2Name,
                            @G1, @N1, @G2, @N2, @G3, @N3, @G4, @N4, @G5, @N5, @G6, @N6, @G7, @N7;
                    CONTINUE;
                END

                -- 3-2. 강좌 매핑 (이름으로 찾기)
                SET @Target_GangjwaIdx = NULL;
                SELECT TOP 1 @Target_GangjwaIdx = F_IDX 
                FROM T_GANGJWA_MAIN 
                WHERE F_COMPANY_IDX = @P_CompanyIdx AND F_GJ_NAME = @Mig_GjName AND F_DELYN = 1;

                -- 3-2-1. [분류 IDX 찾기] T_COMPANY_GANGJWA_BUNRYU
                -- 대분류
                SET @New_BunryuBIdx = NULL;
                IF @Mig_Bunryu1Name IS NOT NULL
                    SELECT TOP 1 @New_BunryuBIdx = F_IDX 
                    FROM T_COMPANY_GANGJWA_BUNRYU 
                    WHERE F_COMPANY_IDX = @P_CompanyIdx AND F_DEPTH = 1 AND F_GangJwaBunRyu_NAME = @Mig_Bunryu1Name;
                
                -- 중분류
                SET @New_BunryuMIdx = NULL;
                IF @Mig_Bunryu2Name IS NOT NULL
                    SELECT TOP 1 @New_BunryuMIdx = F_IDX 
                    FROM T_COMPANY_GANGJWA_BUNRYU 
                    WHERE F_COMPANY_IDX = @P_CompanyIdx AND F_DEPTH = 2 AND F_GangJwaBunRyu_NAME = @Mig_Bunryu2Name
                    AND (@New_BunryuBIdx IS NULL OR F_PARENT_IDX = @New_BunryuBIdx);


                IF @Mig_MemIdx IS NOT NULL AND @Target_GangjwaIdx IS NOT NULL
                BEGIN
                    -- [신규 주문번호 생성]
                    -- POS_NO = -1 (마이그레이션 용)
                    EXEC dbo.USP_ORDER_NO -1, @P_CompanyIdx, @P_CompanyCode, @New_OrderNo OUTPUT;

                     -- 3-3. 주문 마스터 (T_ORDER) 생성
                    INSERT INTO T_ORDER (
                        F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID, 
                        F_ORDER_NO, F_MEM_IDX, F_MEM_NAME, F_MEM_DANJI_IDX,
                        F_TOT_AMOUNT, F_TOT_DC_AMOUNT, F_TOT_PAY_AMOUNT, 
                        F_ORDER_POS_NO, F_ORDER_POS_NAME, F_ORDER_TYPE_NO, 
                        F_WDATE
                    )
                    VALUES (
                        @P_CompanyIdx, @P_CompanyCode, @RealCompanyID,
                        @New_OrderNo, @Mig_MemIdx, @Mig_MemName, 0,
                        @Mig_Amount, 0, @Mig_Amount, -- DC Amount 0
                        -1, 'Migration', 1, -- POS -1, Type 1
                        @Mig_WDate
                    );
                    
                    SET @New_OrderIdx = SCOPE_IDENTITY();

                    -- 3-4. 주문 상세 (T_ORDER_DETAIL)
                    INSERT INTO T_ORDER_DETAIL (
                        F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID,
                        F_ORDER_IDX, F_ORDER_NO, F_ORDER_SEQ,
                        F_ORDER_TYPE_NO, F_ORDER_TYPE_NAME,
                        F_AMOUNT, F_DC_AMOUNT,
                        F_ORDER_CNT, F_PAY_AMOUNT, 
                        F_MEM_IDX, F_MEM_NAME, F_MEM_DANJI_IDX,
                        F_WDATE
                    )
                    VALUES (
                        @P_CompanyIdx, @P_CompanyCode, @RealCompanyID,
                        @New_OrderIdx, @New_OrderNo, 1,
                        1, '강좌', -- Type 1
                        @Mig_Amount, 0,
                        1, @Mig_Amount,
                        @Mig_MemIdx, @Mig_MemName, 0,
                        @Mig_WDate
                    );

                    SET @New_OrderDetailIdx = SCOPE_IDENTITY();

                    -- [코드 IDX & Name Lookup]
                    SET @New_MonthCodeIdx = NULL; SET @New_MonthCodeName = NULL;
                    SET @New_WeekCodeIdx = NULL;  SET @New_WeekCodeName = NULL;
                    SET @New_TimeCodeIdx = NULL;  SET @New_TimeCodeName = NULL;
                    SET @New_AmountCodeIdx = NULL; SET @New_AmountCodeName = NULL;

                    -- Logic: G1~G7 순회하며 Type 확인
                    -- G=5(Month), G=6(Week), G=7(Time)
                    -- 그 외 -> Amount (Type 1,2,3 중 하나)
                    
                    DECLARE @LoopG VARCHAR(10), @LoopN NVARCHAR(50);
                    DECLARE @i INT = 1;
                    
                    WHILE @i <= 7
                    BEGIN
                        IF @i = 1 BEGIN SET @LoopG = LTRIM(RTRIM(@G1)); SET @LoopN = LTRIM(RTRIM(@N1)); END
                        ELSE IF @i = 2 BEGIN SET @LoopG = LTRIM(RTRIM(@G2)); SET @LoopN = LTRIM(RTRIM(@N2)); END
                        ELSE IF @i = 3 BEGIN SET @LoopG = LTRIM(RTRIM(@G3)); SET @LoopN = LTRIM(RTRIM(@N3)); END
                        ELSE IF @i = 4 BEGIN SET @LoopG = LTRIM(RTRIM(@G4)); SET @LoopN = LTRIM(RTRIM(@N4)); END
                        ELSE IF @i = 5 BEGIN SET @LoopG = LTRIM(RTRIM(@G5)); SET @LoopN = LTRIM(RTRIM(@N5)); END
                        ELSE IF @i = 6 BEGIN SET @LoopG = LTRIM(RTRIM(@G6)); SET @LoopN = LTRIM(RTRIM(@N6)); END
                        ELSE IF @i = 7 BEGIN SET @LoopG = LTRIM(RTRIM(@G7)); SET @LoopN = LTRIM(RTRIM(@N7)); END

                        IF @LoopG = '5' AND @New_MonthCodeIdx IS NULL
                            SELECT TOP 1 @New_MonthCodeIdx = F_IDX, @New_MonthCodeName = F_CODE_NAME FROM T_COMPANY_GANGJWA_CODE WHERE F_COMPANY_IDX = @P_CompanyIdx AND F_TYPE = 6 AND F_CODE_NAME = REPLACE(@LoopN, '개월', '') AND F_DELYN = 1;
                        ELSE IF @LoopG = '6' AND @New_WeekCodeIdx IS NULL
                            SELECT TOP 1 @New_WeekCodeIdx = F_IDX, @New_WeekCodeName = F_CODE_NAME FROM T_COMPANY_GANGJWA_CODE WHERE F_COMPANY_IDX = @P_CompanyIdx AND F_TYPE = 4 AND F_CODE_NAME = @LoopN AND F_DELYN = 1;
                        ELSE IF @LoopG = '7' AND @New_TimeCodeIdx IS NULL
                            SELECT TOP 1 @New_TimeCodeIdx = F_IDX, @New_TimeCodeName = F_CODE_NAME FROM T_COMPANY_GANGJWA_CODE WHERE F_COMPANY_IDX = @P_CompanyIdx AND F_TYPE = 5 AND F_CODE_NAME = @LoopN AND F_DELYN = 1;
                        ELSE IF @LoopG IS NOT NULL AND @LoopG NOT IN ('', '5', '6', '7') AND @New_AmountCodeIdx IS NULL
                            SELECT TOP 1 @New_AmountCodeIdx = F_IDX, @New_AmountCodeName = F_CODE_NAME FROM T_COMPANY_GANGJWA_CODE WHERE F_COMPANY_IDX = @P_CompanyIdx AND F_TYPE IN (1,2,3) AND F_CODE_NAME = @LoopN AND F_DELYN = 1;

                        SET @i = @i + 1;
                    END
                    
                    -- 3-5. 강좌 정보 상세 (T_ORDER_DETAIL_GANGJWA_INFO)
                        INSERT INTO T_ORDER_DETAIL_GANGJWA_INFO (
                            F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID,
                            F_ORDER_IDX, F_ORDER_DETAIL_IDX,
                            F_GJ_AMOUNT, 
                            F_START_DATE, F_END_DATE,
                            F_DC_RATE, F_DAY_IPJANG_COUNT, F_BIGO,
                            F_MEM_IDX, F_MEM_NAME, F_MEM_DANJI_IDX,
                            F_ORDER_STATE, F_DEL_YN, F_WDATE, F_UPDATE,
                            F_MONTH_CODE_TYPE, F_MONTH_CODE_TYPE_NAME, F_MONTH_CODE_IDX, F_MONTH_CODE_NAME,
                            F_WEEK_CODE_TYPE, F_WEEK_CODE_TYPE_NAME, F_WEEK_CODE_IDX, F_WEEK_CODE_NAME,
                            F_TIME_CODE_TYPE, F_TIME_CODE_TYPE_NAME, F_TIME_CODE_IDX, F_TIME_CODE_NAME,
                            F_GJ_AMOUNT_IDX, F_GJ_AMOUNT_NAME
                        )
                        VALUES (
                            @P_CompanyIdx, @P_CompanyCode, @RealCompanyID,
                            @New_OrderIdx, @New_OrderDetailIdx,
                            @Mig_Amount,
                            @Mig_SDate, @Mig_EDate,
                            0, 0, @Mig_Remark, -- DC Rate 0, Ipjang 0, Bigo
                            @Mig_MemIdx, @Mig_MemName, 0,
                            @Mig_OrderState, 1, @Mig_WDate, @CurrentDate,
                            6, '강좌개월', @New_MonthCodeIdx, @New_MonthCodeName,
                            4, '강좌요일', @New_WeekCodeIdx, @New_WeekCodeName,
                            5, '강좌시간', @New_TimeCodeIdx, @New_TimeCodeName,
                            @New_AmountCodeIdx, @New_AmountCodeName
                        );
                        
                        SET @New_InfoIdx = SCOPE_IDENTITY();

                    -- 3-6. 강좌 스냅샷 (T_ORDER_GANGJWA_MAIN) 생성
                        INSERT INTO T_ORDER_GANGJWA_MAIN (
                            F_COMPANY_IDX, F_COMPANY_CODE, F_COMPANY_ID, F_ORDER_IDX, F_ORDER_DETAIL_IDX,
                            F_ORDER_DETAIL_GANGJWA_INFO_IDX, F_GANGJWA_MAIN_IDX, F_BUNRYU_B_IDX, F_BUNRYU_M_IDX, F_BUNRYU_S_IDX,
                            F_BUNRYU_B_NAME, F_BUNRYU_M_NAME, F_BUNRYU_S_NAME, F_GJ_NAME_IDX, F_GJ_NAME,
                            F_GENDER, F_VIEW, F_DAY_IPJANG_CNT, F_IPJANG_INTERVAL, F_JEONGWON,
                            F_DC_IDX, F_GJ_START_DATE, F_GJ_END_DATE, F_SANGSI_JEOBSU, F_JEOBSU_START_DATE,
                            F_JEOBSU_END_DATE, F_GJ_TYPE, F_GJ_TYPE_CNT, F_SORT
                        )
                        SELECT 
                            @P_CompanyIdx, @P_CompanyCode, @RealCompanyID, @New_OrderIdx,  @New_OrderDetailIdx,
                            @New_InfoIdx, F_IDX, 
                            ISNULL(@New_BunryuBIdx, F_BUNRYU_B_IDX), -- Old DB 매핑 IDX 우선 사용
                            ISNULL(@New_BunryuMIdx, F_BUNRYU_M_IDX), -- Old DB 매핑 IDX 우선 사용
                            F_BUNRYU_S_IDX,
                            ISNULL(@Mig_Bunryu1Name, F_BUNRYU_B_NAME), -- Old DB 대분류명 우선 사용
                            ISNULL(@Mig_Bunryu2Name, F_BUNRYU_M_NAME), -- Old DB 소분류명 우선 사용
                            F_BUNRYU_S_NAME, F_GJ_NAME_IDX, F_GJ_NAME,
                            F_GENDER, F_VIEW, F_DAY_IPJANG_CNT, F_IPJANG_INTERVAL, F_JEONGWON,
                            F_DC_IDX, F_GJ_START_DATE, F_GJ_END_DATE, F_SANGSI_JEOBSU, F_JEOBSU_START_DATE,
                            F_JEOBSU_END_DATE, F_GJ_TYPE, F_GJ_TYPE_CNT, F_SORT
                        FROM T_GANGJWA_MAIN
                        WHERE F_IDX = @Target_GangjwaIdx;
                END

                FETCH NEXT FROM curTrs INTO @Mig_TrsNo, @Mig_MemIdx, @Mig_MemName, @Mig_Amount, @Mig_GjName, @Mig_SDate, @Mig_EDate, @Mig_WDate, @Mig_Remark, @Mig_OrderState, @Mig_Bunryu1Name, @Mig_Bunryu2Name,
                       @G1, @N1, @G2, @N2, @G3, @N3, @G4, @N4, @G5, @N5, @G6, @N6, @G7, @N7;
            END
            
            CLOSE curTrs;
            DEALLOCATE curTrs;
        END

        SELECT 'SUCCESS' AS Result, * FROM @LogTable;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        SELECT 'ERROR' AS Result, ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
