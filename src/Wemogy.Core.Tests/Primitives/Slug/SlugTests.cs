using Wemogy.Core.Primitives.Slug.Exceptions;
using Xunit;

namespace Wemogy.Core.Tests.Primitives.Slug
{
    public class SlugTests
    {
        [Fact]
        public void Validate_ShouldWork()
        {
            // valid strings
            Wemogy.Core.Primitives.Slug.Slug.Validate("abc");
            Wemogy.Core.Primitives.Slug.Slug.Validate("abc12");
            Wemogy.Core.Primitives.Slug.Slug.Validate("12");
            Wemogy.Core.Primitives.Slug.Slug.Validate("_");
            Wemogy.Core.Primitives.Slug.Slug.Validate("abc12_");

            // invalid strings
            Assert.Throws<InvalidSlugException>(() => Wemogy.Core.Primitives.Slug.Slug.Validate("Abc"));
            Assert.Throws<InvalidSlugException>(() => Wemogy.Core.Primitives.Slug.Slug.Validate("abc-"));
            Assert.Throws<InvalidSlugException>(() => Wemogy.Core.Primitives.Slug.Slug.Validate("abcA"));
            Assert.Throws<InvalidSlugException>(() => Wemogy.Core.Primitives.Slug.Slug.Validate("abc12-"));
        }
    }
}
