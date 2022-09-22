using System;
using Wemogy.Core.Errors.Enums;

namespace Wemogy.Core.Errors.Exceptions
{
    public class ConflictErrorException : ErrorException
    {
        public ConflictErrorException(
            string code,
            string description,
            Exception? innerException)
            : base(ErrorType.Conflict, code, description, innerException)
        {
        }
    }
}
