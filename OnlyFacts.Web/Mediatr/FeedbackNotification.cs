using System;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;
using OnlyFacts.Web.Mediatr.Base;

namespace OnlyFacts.Web.Mediatr
{
    public class FeedbackNotification : NotificationBase
    {
        public FeedbackNotification(string content, Exception? exception = null) 
            : base("FEEDBACK from onlyfacts.ru", content, "address@from.net", "9497020@mail.ru", exception)
        {
        }
    }

    public class FeedbackNotificationHandler : NotificationHandlerBase<FeedbackNotification>
    {
        public FeedbackNotificationHandler(IUnitOfWork unitOfWork, ILogger<FeedbackNotification> logger) : base(unitOfWork, logger)
        {
        }
    }
}