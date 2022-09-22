using System;
using Wemogy.Core.Errors.Enums;

namespace Wemogy.Core.Errors.Exceptions
{
    public class NotFoundErrorException : ErrorException
    {
        public NotFoundErrorException(
            string code,
            string description,
            Exception? innerException)
            : base(ErrorType.NotFound, code, description, innerException)
        {
        }
    }
}
