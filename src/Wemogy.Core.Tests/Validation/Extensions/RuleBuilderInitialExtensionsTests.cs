using System;
using FluentAssertions;
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

        [Theory]
        [InlineData("abc", false)]
        [InlineData("", false)]
        [InlineData("00000000-0000-0000-0000-000000000000", false)]
        [InlineData("00000000-0000-0000-0000-000000000001", true)]
        public void MustBeValidButNotEmptyGuidTestValidator_ShouldWorkAsExpected(
            string guidPropertyValue,
            bool expectedIsValidResult)
        {
            // Arrange
            var user = new User()
            {
                Firstname = guidPropertyValue
            };
            var validator = new MustBeValidButNotEmptyGuidTestValidator();

            // Act
            var validationResult = validator.Validate(user);

            // Assert
            validationResult.IsValid.Should().Be(expectedIsValidResult);
        }

        [Theory]
        [InlineData("spaceblocks")]
        [InlineData("realtime")]
        public void MustBeAValidSlugTestValidator_ShouldWorkAsExpected(string slug)
        {
            // Arrange
            var user = new User()
            {
                Firstname = slug
            };
            var validator = new MustBeAValidSlugTestValidator();

            // Act
            var validationResult = validator.Validate(user);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void MustBeAValidSlugTestValidator_MustThrow_IfEmpty()
        {
            // Arrange
            var user = new User()
            {
                Firstname = string.Empty
            };
            var validator = new MustBeAValidSlugTestValidator();

            // Act and Assert
            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));
        }

        [Theory]
        [InlineData("subscription1")]
        [InlineData("id@ntity")]
        [InlineData("9e1a7458-d894-4861-8d4c-d64a12419e03")]
        [InlineData("space blocks")]
        [InlineData("real-time")]
        [InlineData("RealTime")]
        [InlineData("ViệtNam")]
        public void MustBeAValidSlugTestValidator_MustThrow_IfContainNotAllowCharacters(string slug)
        {
            // Arrange
            var user = new User()
            {
                Firstname = slug
            };
            var validator = new MustBeAValidSlugTestValidator();

            // Act
            var validationResult = validator.Validate(user);

            // Assert
            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));
        }
    }
}
