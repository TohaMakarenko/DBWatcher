using System.Collections.Generic;
using System.Data.SqlClient;

namespace DBWatcher.Core.ScriptResult
{
    public class ScriptResult
    {
        public bool IsSuccess { get; set; } = true;
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