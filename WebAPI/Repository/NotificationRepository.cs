namespace WebApi.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ClassLibrary.IRepository;
    using Data;
    using ClassLibrary.Domain;
    using Entity;
    using global::Data;

    /// <summary>
    /// Repository class for managing notification-related database operations.
    /// </summary>
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _db_context;

        public NotificationRepository(ApplicationDbContext _db_context)
        {
            this._db_context = _db_context;
        }

        /// <inheritdoc/>
        public async Task<List<Notification>> getAllNotificationsAsync()
        {
            List<NotificationEntity> _entities = await this._db_context.Notifications.ToListAsync();

            return _entities.Select(_entity => new Notification
            {
                _notification_id = _entity.notificationId,
                _user_id = _entity.userId,
                _delivery_date_time = _entity.deliveryDateTime,
                _message = _entity.message
            }).ToList();
        }

        /// <inheritdoc/>
        public async Task<List<Notification>> getNotificationsByUserIdAsync(int user_id)
        {
            var entities = await this._db_context.Notifications
                .Where(n => n.userId == user_id)
                .ToListAsync();

            return entities.Select(entity => new Notification
            {
                _notification_id = entity.notificationId,
                _user_id = entity.userId,
                _delivery_date_time = entity.deliveryDateTime,
                _message = entity.message
            }).ToList();
        }

        /// <inheritdoc/>
        public async Task<Notification> getNotificationByIdAsync(int id)
        {
            NotificationEntity _entity = await this._db_context.Notifications.FindAsync(id);

            if (_entity == null)
            {
                throw new Exception($"Notification with ID {id} not found.");
            }

            return new Notification
            {
                _notification_id = _entity.notificationId,
                _user_id = _entity.userId,
                _delivery_date_time = _entity.deliveryDateTime,
                _message = _entity.message
            };
        }

        /// <inheritdoc/>
        public async Task addNotificationAsync(Notification _notification)
        {
            NotificationEntity entity = new NotificationEntity
            {
                userId = _notification._user_id,
                deliveryDateTime = _notification._delivery_date_time,
                message = _notification._message
            };

            this._db_context.Notifications.Add(entity);
            await this._db_context.SaveChangesAsync();

            _notification._notification_id = entity.notificationId; // Set the ID after insert
        }

        /// <inheritdoc/>
        public async Task deleteNotificationAsync(int _id)
        {
            NotificationEntity _entity = await this._db_context.Notifications.FindAsync(_id);

            if (_entity == null)
            {
                throw new Exception($"Notification with ID {_id} not found.");
            }

            this._db_context.Notifications.Remove(_entity);
            await this._db_context.SaveChangesAsync();
        }
    }
}
