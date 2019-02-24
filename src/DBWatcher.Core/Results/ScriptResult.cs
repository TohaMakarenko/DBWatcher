using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DBWatcher.Core.Results
{
    public class ScriptResult<TResult>
    {
        public IEnumerable<TResult> Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        
        public SqlException SqlException { get; set; }
        public IEnumerable<SqlError> Errors { get; set; }

        public virtual int TotalCount {
            get => Data.Count();
        }
        
        public void SetErrors(SqlException exception)
        {
            IsSuccess = false;
            SqlException = exception;
            var errors = new SqlError[exception.Errors.Count];
            for (int i = 0; i < errors.Length; i++) {
                errors[i] = exception.Errors[i];
            }

            Errors = errors;
        }

        /// <summary>
        /// Get data if successful, otherwise throws an exception
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TResult> GetResult()
        {
            return IsSuccess ? Data : throw SqlException;
        }
    }
}