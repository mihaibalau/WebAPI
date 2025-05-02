using ClassLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.IRepository;
using Domain;

namespace WinUI.Service.NotificationFile
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository repo;

        public NotificationService(INotificationRepository repo)
        {
            this.repo = repo;
        }

        public Task<List<Notification>> GetAllAsync()
        { 
            return repo.GetAllNotificationsAsync(); 
        }

        public Task<List<Notification>> GetByUserIdAsync(int userId)
        { 
            return repo.GetNotificationsByUserIdAsync(userId); 
        }

        public async Task DeleteAsync(int notificationId, int userId)
        {
            var notification = await repo.GetNotificationByIdAsync(notificationId);

            if (notification == null)
                throw new KeyNotFoundException($"Notification with ID {notificationId} not found.");

            if (notification.UserId != userId)
                throw new UnauthorizedAccessException(
                    $"User {userId} is not allowed to delete notification {notificationId}.");

            await repo.DeleteNotificationAsync(notificationId);
        }
    }
}
