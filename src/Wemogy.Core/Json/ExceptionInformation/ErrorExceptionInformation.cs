using Wemogy.Core.Errors.Enums;

namespace Wemogy.Core.Json.ExceptionInformation
{
    public class ErrorExceptionInformation
    {
        public ErrorType ErrorType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public ErrorExceptionInformation(
            ErrorType errorType,
            string code,
            string description)
        {
            ErrorType = errorType;
            Code = code;
            Description = description;
        }
    }
}
