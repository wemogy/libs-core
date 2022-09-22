using System;
using System.Diagnostics.CodeAnalysis;
using Bogus;
using FluentAssertions;
using Wemogy.Core.Extensions;
using Xunit;

namespace Wemogy.Core.Tests.Extensions
{
    public enum TestEnum
    {
        None = 0,
        Value1 = 1,
        Value2 = 2
    }

    public class ObjectExtensionsTests
    {
        [Fact]
        public void Json()
        {
            var user = new Faker<TestClass>()
                .RuleFor(x => x.Id, f => f.Random.Guid())
                .RuleFor(x => x.CreatedAt, f => f.Person.DateOfBirth.ToUniversalTime())
                .RuleFor(x => x.Deleted, f => f.Random.Bool())
                .RuleFor(x => x.FlightCount, f => f.Random.Long()).Generate();

            var userDeserialized = user.ToJson().FromJson<TestClass>()!;

            Assert.Equal(user.Id, userDeserialized.Id);
            Assert.True(user.CreatedAt.IsSameUnixDateTime(userDeserialized.CreatedAt));
            Assert.Equal(user.Deleted, userDeserialized.Deleted);
            Assert.Equal(user.FlightCount, userDeserialized.FlightCount);
        }

        [Fact]
        public void FromJson_ShouldAcceptIntAndStringEnumValues()
        {
            // Arrange
            var json = @"{
""enumPropertyA"": ""value1"",
""enumPropertyB"": 2
}";

            // Act
            var instance = json.FromJson<TestClass>()!;

            // Assert
            Assert.Equal(TestEnum.Value1, instance.EnumPropertyA);
            Assert.Equal(TestEnum.Value2, instance.EnumPropertyB);
        }

        [Fact]
        public void FromJson_ShouldAcceptMissingEnumValues()
        {
            // Arrange
            var json = @"{
}";

            // Act
            var instance = json.FromJson<TestClass>()!;

            // Assert
            Assert.Equal(TestEnum.None, instance.EnumPropertyA);
            Assert.Equal(TestEnum.None, instance.EnumPropertyB);
        }

        [Fact]
        public void FromJson_ShouldAcceptTimeSpanValues()
        {
            // Arrange
            var json = @"{
""testTimeSpan"": ""01:02:03""
}";

            // Act
            var instance = json.FromJson<TestClass>()!;

            // Assert
            instance.TestTimeSpan.Should().Be(
                new TimeSpan(
                    1,
                    2,
                    3));
        }

        [Fact]
        public void ToJson_ShouldSerializeEnumValuesAsInt()
        {
            // Arrange
            var instance = new TestClass()
            {
                EnumPropertyA = TestEnum.Value1,
                EnumPropertyB = TestEnum.Value2
            };

            // Act
            var json = instance.ToJson();

            // Assert
            Assert.Contains("1", json);
            Assert.Contains("2", json);
            Assert.False(json.Contains("value1", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void ToJsonDocument_GivenValid_Works()
        {
            // Arrange
            var instance = new TestClass()
            {
                TestUrl = new Uri("https://wemogy.com")
            };

            // Act
            var json = instance.ToJsonDocument();

            // Assert
            Assert.NotNull(json);
        }

        [Fact]
        public void FromJsonDocument_GivenValid_Works()
        {
            // Arrange
            var instance = new TestClass()
            {
                EnumPropertyA = TestEnum.Value1,
                TestUrl = new Uri("https://wemogy.com")
            };
            var document = instance.ToJsonDocument();

            // Act
            var result = document.FromJsonDocument<TestClass>()!;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(instance.TestUrl, result.TestUrl);
            Assert.Equal(instance.EnumPropertyA, result.EnumPropertyA);
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Just test code")]
    public class TestClass
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Deleted { get; set; }

        public long FlightCount { get; set; }

        public TestEnum EnumPropertyA { get; set; }

        public TestEnum EnumPropertyB { get; set; }

        public Uri TestUrl { get; set; }

        public TimeSpan TestTimeSpan { get; set; }

        public TestClass()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Deleted = false;
            FlightCount = 0;
            EnumPropertyA = TestEnum.None;
            EnumPropertyB = TestEnum.None;
            TestUrl = new Uri("http://localhost");
            TestTimeSpan = TimeSpan.Zero;
        }
    }
}
