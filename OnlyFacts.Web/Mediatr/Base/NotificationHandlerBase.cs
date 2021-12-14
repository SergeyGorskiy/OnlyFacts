using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlyFacts.Web.Data;
using OnlyFacts.Web.Extensions;

namespace OnlyFacts.Web.Mediatr.Base
{
    public abstract class NotificationHandlerBase<T> : INotificationHandler<T> where T : NotificationBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<T> _logger;

        protected NotificationHandlerBase(IUnitOfWork unitOfWork, ILogger<T> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(T notification, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Notification>();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(notification.Content);

            if (!(notification.Exception is null))
            {
                stringBuilder.AppendLine(notification.Exception.Message);

                if (!(notification.Exception.InnerException is null))
                {
                    stringBuilder.AppendLine(notification.Exception.InnerException?.Message);
                }

                stringBuilder.AppendLine(notification.Exception.GetBaseException().Message);
                stringBuilder.AppendLine(notification.Exception.StackTrace);
            }




            var entity = new Notification(notification.Subject, notification.Content, notification.AddressFrom, notification.AddressTo);
            await repository.InsertAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                _logger.DatabaseSavingError(nameof(Notification), _unitOfWork.LastSaveChangesResult.Exception);
                return;
            }

            _logger.NotificationAdded(notification.Subject);
        }
    }
}