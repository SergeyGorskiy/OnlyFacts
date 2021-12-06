using System.Collections.Generic;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace OnlyFacts.Web.Data
{
    public class Tag : Identity
    {
        public string Name { get; set; }

        public ICollection<Fact> Facts { get; set; }


    }
}