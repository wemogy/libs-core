using System;

namespace Wemogy.Core.Tests.Validation.TestResources.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Firstname { get; set; }

        public bool IsDefault { get; set; }

        public User? Friend { get; set; }

        public User()
        {
            Firstname = string.Empty;
        }
    }
}
