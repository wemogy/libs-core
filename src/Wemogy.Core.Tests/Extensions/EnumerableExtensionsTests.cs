using System;
using System.Collections.Generic;
using System.Linq;
using Wemogy.Core.Extensions;
using Xunit;

namespace Wemogy.Core.Tests.Extensions
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void Chunk_ShouldWork()
        {
            // Arrange
            var list1 = new List<bool>() { true, false, false, true, true, true, false, true };

            // Act
            var chunks = EnumerableExtensions.Chunk(list1, 3);

            // Assert
            Assert.Equal(3, chunks.Count);
            Assert.Equal(3, chunks[0].Count);
            Assert.Equal(3, chunks[1].Count);
            Assert.Equal(2, chunks[2].Count);
        }

        [Fact]
        public void MapChunks_ShouldWork()
        {
            // Arrange
            // ReSharper disable once CollectionNeverUpdated.Local
            var list1 = new List<int>(); // length: 0
            var list2 = new List<int> { 12, 34 }; // length: 2
            var list3 = new List<int> { 1 }; // length: 1
            var list4 = new List<int> { 12, 3, 44, 564, 334, 5, 66, 5554, 34, 123 }; // length: 10
            var chunkLength = 2;
            int MappingCallback(List<int> chunk) => chunk.Sum();

            // Act
            var list1MappedChunks = list1.MapChunks(chunkLength, MappingCallback);
            var list2MappedChunks = list2.MapChunks(chunkLength, MappingCallback);
            var list3MappedChunks = list3.MapChunks(chunkLength, MappingCallback);
            var list4MappedChunks = list4.MapChunks(chunkLength, MappingCallback);

            // Assert
            Assert.Empty(list1MappedChunks);

            Assert.Single(list2MappedChunks);
            Assert.Equal(46, list2MappedChunks[0]);

            Assert.Single(list3MappedChunks);
            Assert.Equal(1, list3MappedChunks[0]);

            Assert.Equal(5, list4MappedChunks.Count);
            Assert.Equal(15, list4MappedChunks[0]);
            Assert.Equal(608, list4MappedChunks[1]);
            Assert.Equal(339, list4MappedChunks[2]);
            Assert.Equal(5620, list4MappedChunks[3]);
            Assert.Equal(157, list4MappedChunks[4]);
        }

        [Fact]
        public void ToBitString_ShouldWork()
        {
            // Arrange
            var bits1 = new[] { true, false, true, false, false, true }; // 100101 (l)
            var bits2 = Array.Empty<bool>(); // 0
            var bits3 = new[] { false, false, false, false, false, false }; // 000000
            var bits4 = new[]
            {
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
            }; // 000000 000000
            var bits5 = new[]
            {
                false,
                false,
                true,
                true,
                false,
                true,
                true,
                false,
                true,
                false,
                false,
                true,
            }; // 100101(l) 101100(s)
            var bits6 = new[]
            {
                true,
                true,
                false,
                true,
                true,
                false,
                true,
                false,
                false,
                true,
            }; // 1001(J) 011011(b)

            // Act
            var bits1String = bits1.ToBitString();
            var bits2String = bits2.ToBitString();
            var bits3String = bits3.ToBitString();
            var bits4String = bits4.ToBitString();
            var bits5String = bits5.ToBitString();
            var bits6String = bits6.ToBitString();

            // Assert
            Assert.Equal("100101", bits1String);
            Assert.Equal(string.Empty, bits2String);
            Assert.Equal("000000", bits3String);
            Assert.Equal("000000000000", bits4String);
            Assert.Equal("100101101100", bits5String);
            Assert.Equal("1001011011", bits6String);
        }
    }
}
