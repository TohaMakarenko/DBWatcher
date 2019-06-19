SELECT 
       TotalCPU_ms   = SUM(s.cpu_time), 
       TotalPhyIO_mb = SUM((s.reads + s.writes) * 8 / 1024), 
       MemUsage_kb   = SUM(s.memory_usage * 8192 / 1024)
    FROM sys.dm_exec_sessions s LEFT OUTER JOIN sys.dm_exec_connections c ON (s.session_id = c.session_id)
    LEFT OUTER JOIN sys.dm_exec_requests r ON (s.session_id = r.session_id)
    LEFT OUTER JOIN sys.dm_os_tasks t ON (r.session_id = t.session_id AND r.request_id = t.request_id)
    LEFT OUTER JOIN 
    (
        -- Using row_number to select longest wait for each thread, 
        -- should be representative of other wait relationships if thread has multiple involvements. 
        SELECT *, ROW_NUMBER() OVER (PARTITION BY waiting_task_address ORDER BY wait_duration_ms DESC) AS row_num
        FROM sys.dm_os_waiting_tasks 
    ) w ON (t.task_address = w.waiting_task_address) AND w.row_num = 1
    LEFT OUTER JOIN sys.dm_exec_requests r2 ON (r.session_id = r2.blocking_session_id)
    OUTER APPLY sys.dm_exec_sql_text(r.sql_handle) as st

    WHERE s.session_Id > 50                         -- ignore anything pertaining to the system spids.