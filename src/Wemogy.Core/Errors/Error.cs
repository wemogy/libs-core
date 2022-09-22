using System;
using Wemogy.Core.Errors.Exceptions;

namespace Wemogy.Core.Errors
{
    public static class Error
    {
        /// <summary>
        ///     Creates an exception for an failure error in the application
        ///     Samples:
        ///     - Error.Failure("MyService.ApiUnavailable", "The realtime API is not available");
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="innerException">The inner exception, if available</param>
        public static FailureErrorException Failure(
            string code,
            string description,
            Exception? innerException = null)
        {
            return new FailureErrorException(code, description, innerException);
        }

        /// <summary>
        ///     Creates an exception for an unexpected error in the application
        ///     Samples:
        ///     - Error.Unexpected("Factory.AttributeMissing", "The attribute Validate is missing in the type User")
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="innerException">The inner exception, if available</param>
        public static UnexpectedErrorException Unexpected(
            string code,
            string description,
            Exception? innerException = null)
        {
            return new UnexpectedErrorException(code, description, innerException);
        }

        /// <summary>
        ///     Creates an exception for a validation error in the application
        ///     Samples:
        ///     - Error.Validation("ModelValidator.ValidationFailed", "The model is not valid")
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="innerException">The inner exception, if available</param>
        public static ValidationErrorException Validation(
            string code,
            string description,
            Exception? innerException = null)
        {
            return new ValidationErrorException(code, description, innerException);
        }

        /// <summary>
        ///     Creates an exception for a conflict error in the application
        ///     Samples:
        ///     - Error.Conflict("DatabaseService.ItemAlreadyExists", "The item already exists")
        /// </summary>
        /// <param name="code">The unique error code. (will be prefixed by caller class name and method name)</param>
        /// <param name="description">The error description.</param>
        /// <param name="innerException">The inner exception, if available</param>
        public static ConflictErrorException Conflict(
            string code,
            string description,
            Exception? innerException = null)
        {
            return new ConflictErrorException(code, description, innerException);
        }

        /// <summary>
        ///     Creates an exception for a conflict error in the application
        ///     Samples:
        ///     - Error.Conflict("DatabaseService.ItemAlreadyExists", "The item already exists")
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="innerException">The inner exception, if available</param>
        public static NotFoundErrorException NotFound(
            string code,
            string description,
            Exception? innerException = null)
        {
            return new NotFoundErrorException(code, description, innerException);
        }

        /// <summary>
        ///     Creates an exception for a authorization error in the application
        ///     Samples:
        ///     - Error.Authorization("DatabaseService.", "The item already exists")
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="innerException">The inner exception, if available</param>
        public static AuthorizationErrorException Authorization(
            string code,
            string description,
            Exception? innerException = null)
        {
            return new AuthorizationErrorException(code, description, innerException);
        }

        /// <summary>
        ///     Creates an exception for a precondition failed error in the application
        ///     Samples:
        ///     - Error.PreconditionFailed("DatabaseClientETagNotMatched", "The etag of the item does not match")
        ///     - Error.PreconditionFailed("StorageAccount", "Can not create a container for the not existing organization xy")
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="innerException">The inner exception, if available</param>
        public static PreconditionFailedErrorException PreconditionFailed(
            string code,
            string description,
            Exception? innerException = null)
        {
            return new PreconditionFailedErrorException(code, description, innerException);
        }
    }
}
