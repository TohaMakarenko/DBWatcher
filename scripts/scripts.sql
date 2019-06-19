Activity monitor


This query will return very similar information to what activity monitor returns


IF OBJECT_ID('tempdb..#tmp') IS NULL
BEGIN
  SELECT
    * INTO #tmp
  FROM sys.dm_exec_sessions s
  PRINT 'ждем секунду для накопления статистики при первом запуске'
  -- при последующих запусках не ждем, т.к. сравниваем с результатом предыдущего запуска
  WAITFOR DELAY '00:00:01';
END
IF OBJECT_ID('tempdb..#tmp1') IS NOT NULL
  DROP TABLE #tmp1

DECLARE @d DATETIME
DECLARE @dd FLOAT
SELECT
  @d = crdate
FROM tempdb.dbo.sysobjects
WHERE id = OBJECT_ID('tempdb..#tmp')

SELECT
  * INTO #tmp1
FROM sys.dm_exec_sessions s
SELECT
  @dd = DATEDIFF(ms, @d, GETDATE())


SELECT TOP 30
  SUM(s.cpu_time - ISNULL(t.cpu_time, 0)) AS cpu_Diff
 ,SUM(CONVERT(NUMERIC(16, 2), (s.cpu_time - ISNULL(t.cpu_time, 0)) / @dd * 1000)) AS cpu_sec
 ,SUM(s.reads + s.writes - ISNULL(t.reads, 0) - ISNULL(t.writes, 0)) AS totIO_Diff
 ,SUM(CONVERT(NUMERIC(16, 2), (s.reads + s.writes - ISNULL(t.reads, 0) - ISNULL(t.writes, 0)) / @dd * 1000)) AS totIO_sec
 ,SUM(s.reads - ISNULL(t.reads, 0)) AS reads_Diff
 ,SUM(CONVERT(NUMERIC(16, 2), (s.reads - ISNULL(t.reads, 0)) / @dd * 1000)) AS reads_sec
 ,SUM(s.writes - ISNULL(t.writes, 0)) AS writes_Diff
 ,SUM(CONVERT(NUMERIC(16, 2), (s.writes - ISNULL(t.writes, 0)) / @dd * 1000)) AS writes_sec
 ,SUM(s.logical_reads - ISNULL(t.logical_reads, 0)) AS logical_reads_Diff
 ,SUM(CONVERT(NUMERIC(16, 2), (s.logical_reads - ISNULL(t.logical_reads, 0)) / @dd * 1000)) AS logical_reads_sec
 ,SUM(s.memory_usage)
 ,SUM(s.memory_usage - ISNULL(t.memory_usage, 0)) AS [mem_D]
FROM #tmp1 s
LEFT JOIN #tmp t
  ON s.session_id = t.session_id
--totIO_Diff desc
--logical_reads_Diff desc

DELETE FROM #tmp

INSERT INTO #tmp
  SELECT * FROM #tmp1

-- ==============================================================================================
-- ==============================================================================================


Check fragmentation



SELECT dbschemas.[name] as 'Schema',
dbtables.[name] as 'Table',
dbindexes.[name] as 'Index',
indexstats.avg_fragmentation_in_percent,
indexstats.page_count
FROM sys.dm_db_index_physical_stats (DB_ID(), NULL, NULL, NULL, NULL) AS indexstats
INNER JOIN sys.tables dbtables on dbtables.[object_id] = indexstats.[object_id]
INNER JOIN sys.schemas dbschemas on dbtables.[schema_id] = dbschemas.[schema_id]
INNER JOIN sys.indexes AS dbindexes ON dbindexes.[object_id] = indexstats.[object_id]
AND indexstats.index_id = dbindexes.index_id
WHERE indexstats.database_id = DB_ID()
and indexstats.avg_fragmentation_in_percent > @fragmentation
ORDER BY indexstats.avg_fragmentation_in_percent desc