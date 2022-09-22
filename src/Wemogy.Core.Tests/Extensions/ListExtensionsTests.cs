using System.Collections.Generic;
using Wemogy.Core.Extensions;
using Xunit;

namespace Wemogy.Core.Tests.Extensions
{
    public class ListExtensionsTests
    {
        [Fact]
        public void GetAllCombinations_ShouldWork()
        {
            // arrange
            var list = new List<string>()
            {
                "id", "createdAt", "navigationProperty 1", "navigationProperty 2"
            };

            // act
            var combinations = list.GetAllCombinations();

            // assert
            Assert.Equal(15, combinations.Count);
        }

        [Fact]
        public void Pad_ShouldWork()
        {
            // Arrange
            var list1 = new List<bool>(); // length: 0
            var list2 = new List<int>() { 12, 34 }; // length: 2
            var list3 = new List<bool>() { false }; // length: 1
            var list4 = new List<int>() { 12, 3, 44, 564, 334, 5, 66, 5554, 34, 123 }; // length: 10
            var multiple = 6;

            // Act
            list1.Pad(multiple);
            list2.Pad(multiple);
            list3.Pad(multiple);
            list4.Pad(multiple);

            // Assert
            Assert.Empty(list1);
            Assert.Equal(6, list2.Count);
            Assert.Equal(6, list3.Count);
            Assert.Equal(12, list4.Count);
        }
    }
}
