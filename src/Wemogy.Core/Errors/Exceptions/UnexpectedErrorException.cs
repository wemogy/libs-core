using System;
using Wemogy.Core.Errors.Enums;

namespace Wemogy.Core.Errors.Exceptions
{
    public class UnexpectedErrorException : ErrorException
    {
        public UnexpectedErrorException(
            string code,
            string description,
            Exception? innerException)
            : base(ErrorType.Unexpected, code, description, innerException)
        {
        }
    }
}
