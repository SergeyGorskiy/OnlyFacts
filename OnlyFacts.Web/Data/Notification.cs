using Calabonga.EntityFrameworkCore.Entities.Base;

namespace OnlyFacts.Web.Data
{
    public class Notification : Auditable
    {
        public string Subject { get; set; }

        public string Content { get; set; }

        public bool IsCompleted { get; set; }

        public string AddressFrom { get; set; }

        public string AddressTo { get; set; }
    }
}