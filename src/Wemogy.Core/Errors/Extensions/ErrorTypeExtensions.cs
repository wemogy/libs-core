using System.Net;
using Wemogy.Core.Errors.Enums;

namespace Wemogy.Core.Errors.Extensions
{
    public static class ErrorTypeExtensions
    {
        public static HttpStatusCode ToHttpStatusCode(this ErrorType errorType)
        {
            switch (errorType)
            {
                case ErrorType.Authorization:
                    return HttpStatusCode.Forbidden;
                case ErrorType.Conflict:
                    return HttpStatusCode.Conflict;
                case ErrorType.Failure:
                    return HttpStatusCode.BadRequest;
                case ErrorType.NotFound:
                    return HttpStatusCode.NotFound;
                case ErrorType.PreconditionFailed:
                    return HttpStatusCode.PreconditionFailed;
                case ErrorType.Unexpected:
                    return HttpStatusCode.InternalServerError;
                case ErrorType.Validation:
                    return HttpStatusCode.BadRequest;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
