using System;
using System.Collections.Generic;

namespace OnlyFacts.Web.ViewModels
{
    public class FactViewModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Content { get; set; } = null!;

        public IEnumerable<TagViewModel> Tags { get; set; } = null!;
    }
}