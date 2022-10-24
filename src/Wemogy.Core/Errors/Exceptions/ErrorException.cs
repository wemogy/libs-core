using System;
using Wemogy.Core.Errors.Enums;

namespace Wemogy.Core.Errors.Exceptions
{
    public abstract class ErrorException : Exception
    {
        public ErrorType ErrorType { get; set; }
        public string Code { get; set; }

        public string Description { get; set; }

        protected ErrorException(
            ErrorType errorType,
            string code,
            string description,
            Exception? innerException)
            : base($"{code} - {description}", innerException)
        {
            ErrorType = errorType;
            Code = code;
            Description = description;
        }
    }
}
