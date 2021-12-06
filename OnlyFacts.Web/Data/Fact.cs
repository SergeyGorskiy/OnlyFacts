using System.Collections.Generic;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace OnlyFacts.Web.Data
{
    public class Fact : Auditable
    {
        public string Content { get; set; }

        public ICollection<Tag> Tags { get; set; }

    }
}