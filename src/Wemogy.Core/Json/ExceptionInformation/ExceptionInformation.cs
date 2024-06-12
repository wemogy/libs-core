using System;

namespace Wemogy.Core.Json.ExceptionInformation
{
    /// <summary>
    /// This class represents the most important properties of a exception in a JSON serializable way
    /// Thanks to: https://stackoverflow.com/a/72968664
    /// </summary>
    public class ExceptionInformation
    {
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string? StackTrace { get; set; }
        public ExceptionInformation? InnerException { get; set; }

        public ExceptionInformation(
            Exception exception,
            bool includeInnerException = true,
            bool includeStackTrace = false)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            ExceptionType = exception.GetType().FullName ?? exception.GetType().Name;
            Message = exception.Message;
            Source = exception.Source;
            StackTrace = includeStackTrace ? exception.StackTrace : null;
            if (includeInnerException && exception.InnerException is not null)
            {
                InnerException = new ExceptionInformation(exception.InnerException, includeInnerException, includeStackTrace);
            }
        }
    }
}
