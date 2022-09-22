using System;
using Wemogy.Core.Errors.Enums;

namespace Wemogy.Core.Errors.Exceptions
{
    public class AuthorizationErrorException : ErrorException
    {
        public AuthorizationErrorException(
            string code,
            string description,
            Exception? innerException)
            : base(ErrorType.Authorization, code, description, innerException)
        {
        }
    }
}
