using System;
using System.Collections.Generic;
using Wemogy.Core.Json;
using Xunit;

namespace Wemogy.Core.Tests.Json
{
    public class JsonBuilderTests
    {
        [Fact]
        public void ShouldWork()
        {
            // Arrange
            var jsonBuilder = new JsonBuilder();
            var stringValue = "theStringValue";
            var intValue = 1;
            var doubleValue = 1.1;
            var boolValue = true;
            var dateTimeValue = DateTime.SpecifyKind(new DateTime(2000, 1, 1), DateTimeKind.Utc);
            var stringListValue = new List<string> { "itemA", "itemB" };
            var objectValue = new JsonBuilder().AddProperty("p1", "v1").AddProperty("b2", 2).Build();

            // Act
            jsonBuilder.AddProperty("myStringProperty", stringValue);
            jsonBuilder.AddProperty("myIntProperty", intValue);
            jsonBuilder.AddProperty("myDoubleProperty", doubleValue);
            jsonBuilder.AddProperty("myBoolProperty", boolValue);
            jsonBuilder.AddProperty("myDateTimeProperty", dateTimeValue);
            jsonBuilder.AddProperty("myStringListProperty", stringListValue);
            jsonBuilder.AddJsonObjectProperty("myObjectProperty", objectValue);
            jsonBuilder.AddProperty("ignoreNull", null);

            var json = jsonBuilder.Build();

            // Assert
            Assert.Equal(
                "{\"myStringProperty\": \"theStringValue\",\"myIntProperty\": 1,\"myDoubleProperty\": 1.1,\"myBoolProperty\": true,\"myDateTimeProperty\": 946684800000,\"myStringListProperty\": [\"itemA\",\"itemB\"],\"myObjectProperty\": {\"p1\": \"v1\",\"b2\": 2}}",
                json);
        }
    }
}
