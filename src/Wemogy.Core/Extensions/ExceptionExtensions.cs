using System;
using System.Net;
using FluentValidation;
using Wemogy.Core.Errors.Exceptions;
using Wemogy.Core.Errors.Extensions;

namespace Wemogy.Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static HttpStatusCode GetHttpStatusCode(this Exception exception)
        {
            if (exception is ErrorException errorException)
            {
                return errorException.ErrorType.ToHttpStatusCode();
            }

            if (exception is ValidationException)
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
