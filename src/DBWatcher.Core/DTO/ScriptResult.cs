using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DBWatcher.Core.Repositories
{
    public class ScriptResult
    {
        public IEnumerable<IEnumerable<dynamic>> Data { get; set; }
        public bool IsSuccess { get; set; } = true;

        public int TotalCount {
            get => Data.Sum(d => d.Count());
        }

        public IEnumerable<SqlError> Errors { get; set; }

        public void SetErrors(SqlException exception)
        {
            IsSuccess = false;
            var errors = new SqlError[exception.Errors.Count];
            for (int i = 0; i < errors.Length; i++) {
                errors[i] = exception.Errors[i];
            }

            Errors = errors;
        }
    }
}