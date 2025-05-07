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
        private readonly ApplicationDbContext dbContext;

        public NotificationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<List<Notification>> getAllNotificationsAsync()
        {
            var entities = await this.dbContext.Notifications.ToListAsync();

            return entities.Select(entity => new Notification
            {
                notificationId = entity.notificationId,
                userId = entity.userId,
                deliveryDateTime = entity.deliveryDateTime,
                message = entity.message
            }).ToList();
        }

        /// <inheritdoc/>
        public async Task<List<Notification>> getNotificationsByUserIdAsync(int userId)
        {
            var entities = await this.dbContext.Notifications
                .Where(n => n.userId == userId)
                .ToListAsync();

            return entities.Select(entity => new Notification
            {
                notificationId = entity.notificationId,
                userId = entity.userId,
                deliveryDateTime = entity.deliveryDateTime,
                message = entity.message
            }).ToList();
        }

        /// <inheritdoc/>
        public async Task<Notification> getNotificationByIdAsync(int id)
        {
            var entity = await this.dbContext.Notifications.FindAsync(id);

            if (entity == null)
            {
                throw new Exception($"Notification with ID {id} not found.");
            }

            return new Notification
            {
                notificationId = entity.notificationId,
                userId = entity.userId,
                deliveryDateTime = entity.deliveryDateTime,
                message = entity.message
            };
        }

        /// <inheritdoc/>
        public async Task addNotificationAsync(Notification notification)
        {
            var entity = new NotificationEntity
            {
                userId = notification.userId,
                deliveryDateTime = notification.deliveryDateTime,
                message = notification.message
            };

            this.dbContext.Notifications.Add(entity);
            await this.dbContext.SaveChangesAsync();

            notification.notificationId = entity.notificationId; // Set the ID after insert
        }

        /// <inheritdoc/>
        public async Task deleteNotificationAsync(int id)
        {
            var entity = await this.dbContext.Notifications.FindAsync(id);

            if (entity == null)
            {
                throw new Exception($"Notification with ID {id} not found.");
            }

            this.dbContext.Notifications.Remove(entity);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
