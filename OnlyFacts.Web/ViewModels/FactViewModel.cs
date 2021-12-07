using System;
using System.Collections.Generic;
using OnlyFacts.Web.Data;

namespace OnlyFacts.Web.ViewModels
{
    public class FactViewModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Content { get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}