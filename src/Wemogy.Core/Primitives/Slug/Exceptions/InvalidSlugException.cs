using System;

namespace Wemogy.Core.Primitives.Slug.Exceptions
{
    public class InvalidSlugException : Exception
    {
        public InvalidSlugException(string slug)
            : base(
            $"Slug can only contain lowercase letters, numbers and underscores. Invalid slug: {slug}")
        {
        }
    }
}
