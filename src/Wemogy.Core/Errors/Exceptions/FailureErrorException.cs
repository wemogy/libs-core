using System;
using Wemogy.Core.Errors.Enums;

namespace Wemogy.Core.Errors.Exceptions
{
    public class FailureErrorException : ErrorException
    {
        public FailureErrorException(
            string code,
            string description,
            Exception? innerException)
            : base(ErrorType.Failure, code, description, innerException)
        {
        }
    }
}
