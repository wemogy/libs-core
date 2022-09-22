using System;
using Wemogy.Core.Errors.Enums;

namespace Wemogy.Core.Errors.Exceptions
{
    public class ValidationErrorException : ErrorException
    {
        public ValidationErrorException(
            string code,
            string description,
            Exception? innerException)
            : base(ErrorType.Validation, code, description, innerException)
        {
        }
    }
}
