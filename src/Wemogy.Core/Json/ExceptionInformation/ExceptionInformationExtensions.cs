using System;
using Wemogy.Core.Errors;
using Wemogy.Core.Errors.Enums;
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

        public static Exception ToException(this ExceptionInformation exceptionInformation)
        {
            // destructure
            var errorExceptionInformation = exceptionInformation.ErrorExceptionInformation;

            if (errorExceptionInformation != null)
            {
                switch (errorExceptionInformation.ErrorType)
                {
                    case ErrorType.Failure:
                        return Error.Failure(errorExceptionInformation.Code, errorExceptionInformation.Description);
                    case ErrorType.Unexpected:
                        return Error.Unexpected(errorExceptionInformation.Code, errorExceptionInformation.Description);
                    case ErrorType.Validation:
                        return Error.Validation(errorExceptionInformation.Code, errorExceptionInformation.Description);
                    case ErrorType.Conflict:
                        return Error.Conflict(errorExceptionInformation.Code, errorExceptionInformation.Description);
                    case ErrorType.NotFound:
                        return Error.NotFound(errorExceptionInformation.Code, errorExceptionInformation.Description);
                    case ErrorType.Authorization:
                        return Error.Authorization(errorExceptionInformation.Code, errorExceptionInformation.Description);
                    case ErrorType.PreconditionFailed:
                        return Error.PreconditionFailed(errorExceptionInformation.Code, errorExceptionInformation.Description);
                }
            }

            return new Exception(exceptionInformation.Message);
        }
    }
}
