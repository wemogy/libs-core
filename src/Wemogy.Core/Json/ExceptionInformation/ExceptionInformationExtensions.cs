using System;
using Wemogy.Core.Extensions;

namespace Wemogy.Core.Json.ExceptionInformation
{
    public static class ExceptionInformationExtensions
    {
        public static ExceptionInformation ToExceptionInformation(this Exception exception, bool includeInnerException = true, bool includeStackTrace = false)
        {
            return new ExceptionInformation(exception, includeInnerException, includeStackTrace);
        }

        public static string ToJson(this Exception exception, bool includeInnerException = true, bool includeStackTrace = false)
        {
            return exception.ToExceptionInformation(includeInnerException, includeStackTrace).ToJson();
        }
    }
}
