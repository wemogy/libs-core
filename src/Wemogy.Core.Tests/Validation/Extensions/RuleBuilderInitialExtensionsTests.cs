using System;
using FluentValidation;
using Wemogy.Core.Tests.Validation.TestResources.Models;
using Wemogy.Core.Tests.Validation.TestResources.Validators;
using Xunit;

namespace Wemogy.Core.Tests.Validation.Extensions
{
    public class RuleBuilderInitialExtensionsTests
    {
        [Fact]
        public void SetNullableValidator_ShouldValidate_IfPropertyIsNull()
        {
            // Arrange
            var nullablePropertyTestModel = new NullablePropertyTestModel();
            var nullablePropertyTestModelValidator = new NullablePropertyTestModelValidator();

            // Act
            var validationResult = nullablePropertyTestModelValidator.Validate(nullablePropertyTestModel);

            // Assert
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void SetNullableValidator_ShouldValidate_IfPropertyIsDefined()
        {
            // Arrange
            var nullablePropertyTestModel = new NullablePropertyTestModel()
            {
                NullableProperty = new User()
                {
                    Id = Guid.NewGuid()
                }
            };
            var nullablePropertyTestModelValidator = new NullablePropertyTestModelValidator();

            // Act
            var validationResult = nullablePropertyTestModelValidator.Validate(nullablePropertyTestModel);

            // Assert
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void SetNullableValidator_ShouldValidate_IfPropertyIsInvalidDefined()
        {
            // Arrange
            var nullablePropertyTestModel = new NullablePropertyTestModel()
            {
                NullableProperty = new User()
            };
            var nullablePropertyTestModelValidator = new NullablePropertyTestModelValidator();

            // Act and Assert
            Assert.Throws<ValidationException>(() =>
                nullablePropertyTestModelValidator.ValidateAndThrow(nullablePropertyTestModel));
        }

        [Fact]
        public void MustBeLowercaseWithoutSpecialCharacters_HappyPath()
        {
            // Arrange
            var user = new User()
            {
                Firstname = "sebastiankuesters"
            };
            var validator = new MustBeLowercaseWithoutSpecialCharactersTestValidator();

            // Act
            var validationResult = validator.Validate(user);

            // Assert
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void MustBeLowercaseWithoutSpecialCharacters_MustThrow_IfPropertyContainsSpecialCharacters_Case1()
        {
            // Arrange
            var user = new User()
            {
                Firstname = "sebastianküesters"
            };
            var validator = new MustBeLowercaseWithoutSpecialCharactersTestValidator();

            // Act and Assert
            Assert.Throws<ValidationException>(() =>
                validator.ValidateAndThrow(user));
        }

        [Fact]
        public void MustBeLowercaseWithoutSpecialCharacters_MustThrow_IfPropertyContainsSpecialCharacters_Case2()
        {
            // Arrange
            var user = new User()
            {
                Firstname = "sebastiankuesters$$"
            };
            var validator = new MustBeLowercaseWithoutSpecialCharactersTestValidator();

            // Act and Assert
            Assert.Throws<ValidationException>(() =>
                validator.ValidateAndThrow(user));
        }

        [Fact]
        public void MustBeLowercaseWithoutSpecialCharacters_MustThrow_IfPropertyContainsWhitespace()
        {
            // Arrange
            var user = new User()
            {
                Firstname = "sebastian kuesters"
            };
            var validator = new MustBeLowercaseWithoutSpecialCharactersTestValidator();

            // Act and Assert
            Assert.Throws<ValidationException>(() =>
                validator.ValidateAndThrow(user));
        }

        [Fact]
        public void MustBeLowercaseWithoutSpecialCharacters_MustThrow_IfPropertyContainsSlashes()
        {
            // Arrange
            var user = new User()
            {
                Firstname = "sebastian/kuesters"
            };
            var validator = new MustBeLowercaseWithoutSpecialCharactersTestValidator();

            // Act and Assert
            Assert.Throws<ValidationException>(() =>
                validator.ValidateAndThrow(user));
        }
    }
}
