using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DBWatcher.Core.Results
{
    /// <summary>
    /// Strong typed script result
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class ScriptResult<TResult>
    {
        /// <summary>
        /// Result rows
        /// </summary>
        public IEnumerable<TResult> Data { get; set; }
        /// <summary>
        /// Was script executed successfully 
        /// </summary>
        public bool IsSuccess { get; set; } = true;
        
        /// <summary>
        /// The exception that is thrown when SQL server returns a warning or error 
        /// </summary>
        public SqlException SqlException { get; set; }
        
        /// <summary>
        /// Collection of warnings or errors returned by SQL server
        /// </summary>
        public IEnumerable<SqlError> Errors { get; set; }

        /// <summary>
        /// Total count of results rows
        /// </summary>
        public virtual int TotalCount {
            get => Data.Count();
        }
        
        /// <summary>
        /// Set sql exception and errors
        /// </summary>
        /// <param name="exception"></param>
        public void SetException(SqlException exception)
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