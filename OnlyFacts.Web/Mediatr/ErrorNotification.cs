using System;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;
using OnlyFacts.Web.Mediatr.Base;

namespace OnlyFacts.Web.Mediatr
{
    public class ErrorNotification : NotificationBase
    {
        public ErrorNotification(string content, Exception? exception = null) 
            : base("ERROR on onlyfacts.ru", content, "address@from.net", "9497020@mail.ru", exception)
        {
        }
    }

    public class ErrorNotificationHandler : NotificationHandlerBase<ErrorNotification>
    {
        public ErrorNotificationHandler(IUnitOfWork unitOfWork, ILogger<ErrorNotification> logger) : base(unitOfWork, logger)
        {
        }
    }
}