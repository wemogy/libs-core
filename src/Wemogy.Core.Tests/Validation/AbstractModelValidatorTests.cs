using System;
using FluentValidation;
using Wemogy.Core.Extensions;
using Wemogy.Core.Tests.Validation.TestResources.Models;
using Wemogy.Core.Tests.Validation.TestResources.Validators;
using Xunit;

namespace Wemogy.Core.Tests.Validation
{
    public class AbstractModelValidatorTests
    {
        [Fact]
        public void AbstractModelValidator_GlobalValidationRules_ShouldBeUsed()
        {
            // Arrange
            var user1 = new User();
            var userValidator = new UserValidator();
            userValidator.Initialize();

            // Act & Assert
            // Throws because the GUID is an empty GUID
            Assert.Throws<ValidationException>(() => userValidator.ValidateAndThrow(user1));
        }

        [Fact]
        public void AbstractModelValidator_CreateValidationRules_ShouldBeUsed()
        {
            // Arrange
            var user1 = new User()
            {
                Id = Guid.NewGuid()
            };
            var userValidator = new UserValidator();
            userValidator.Initialize();

            // Act
            var validationResult = userValidator.Validate(user1);

            // Act & Assert
            // Throws because the firstname is NULL
            Assert.False(validationResult.IsValid);
            Assert.Throws<ValidationException>(() => userValidator.ValidateAndThrow(user1));
        }

        [Fact]
        public void AbstractModelValidator_UpdateValidationRules_ShouldBeUsed()
        {
            // Arrange
            var user1 = new User()
            {
                Id = Guid.NewGuid()
            };
            var userValidator = new UserValidator();
            userValidator.Initialize(user1.Clone());

            // Act
            user1.IsDefault = true;
            var validationResult = userValidator.Validate(user1);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal("Can not change is default", validationResult.Errors[0].ErrorMessage);
        }
    }
}
