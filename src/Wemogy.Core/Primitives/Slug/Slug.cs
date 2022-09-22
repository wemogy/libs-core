using System.Text.RegularExpressions;
using Wemogy.Core.Primitives.Slug.Exceptions;

namespace Wemogy.Core.Primitives.Slug
{
    public class Slug
    {
        public static void Validate(string slug)
        {
            var regex = new Regex("^[a-z0-9_]+$");
            if (!regex.IsMatch(slug))
            {
                throw new InvalidSlugException(slug);
            }
        }

        private readonly string _slug;

        public Slug(string slug)
        {
            Validate(slug);
            _slug = slug;
        }

        public override string ToString()
        {
            return _slug;
        }
    }
}
