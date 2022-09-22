using System;
using Wemogy.Core.Errors.Enums;

namespace Wemogy.Core.Errors.Exceptions
{
    public class PreconditionFailedErrorException : ErrorException
    {
        public PreconditionFailedErrorException(
            string code,
            string description,
            Exception? innerException)
            : base(ErrorType.PreconditionFailed, code, description, innerException)
        {
        }
    }
}
