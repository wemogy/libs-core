using System;
using System.Linq;
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
            Code = $"{GetCodePrefix()}-{code}";
            Description = description;
        }

        private string GetCodePrefix()
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var stackFrames = stackTrace.GetFrames();
            var errorCalledMethod = stackFrames?
                .Select(x => x.GetMethod())
                .FirstOrDefault(x => x.DeclaringType?.Namespace != null && !x.DeclaringType.Namespace.StartsWith("Wemogy.Core.Error"));
            if (errorCalledMethod == null)
            {
                return string.Empty;
            }

            if (errorCalledMethod.DeclaringType == null)
            {
                return errorCalledMethod.Name;
            }

            return $"{errorCalledMethod.DeclaringType.Name}-{errorCalledMethod.Name}";
        }
    }
}
