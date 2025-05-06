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
        public async Task<List<Notification>> GetAllNotificationsAsync()
        {
            var entities = await this.dbContext.Notifications.ToListAsync();

            return entities.Select(entity => new Notification
            {
                NotificationId = entity.NotificationId,
                UserId = entity.UserId,
                DeliveryDateTime = entity.DeliveryDateTime,
                Message = entity.Message
            }).ToList();
        }

        /// <inheritdoc/>
        public async Task<List<Notification>> GetNotificationsByUserIdAsync(int userId)
        {
            var entities = await this.dbContext.Notifications
                .Where(n => n.UserId == userId)
                .ToListAsync();

            return entities.Select(entity => new Notification
            {
                NotificationId = entity.NotificationId,
                UserId = entity.UserId,
                DeliveryDateTime = entity.DeliveryDateTime,
                Message = entity.Message
            }).ToList();
        }

        /// <inheritdoc/>
        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            var entity = await this.dbContext.Notifications.FindAsync(id);

            if (entity == null)
            {
                throw new Exception($"Notification with ID {id} not found.");
            }

            return new Notification
            {
                NotificationId = entity.NotificationId,
                UserId = entity.UserId,
                DeliveryDateTime = entity.DeliveryDateTime,
                Message = entity.Message
            };
        }

        /// <inheritdoc/>
        public async Task AddNotificationAsync(Notification notification)
        {
            var entity = new NotificationEntity
            {
                UserId = notification.UserId,
                DeliveryDateTime = notification.DeliveryDateTime,
                Message = notification.Message
            };

            this.dbContext.Notifications.Add(entity);
            await this.dbContext.SaveChangesAsync();

            notification.NotificationId = entity.NotificationId; // Set the ID after insert
        }

        /// <inheritdoc/>
        public async Task DeleteNotificationAsync(int id)
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
