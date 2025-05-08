using ClassLibrary.Domain;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using WinUI.Service;
using System.ComponentModel;

namespace WinUI.ViewModel
{
    public class NotificationViewModel
    {
        private readonly NotificationService _notification_service;

        public ObservableCollection<Notification> notifications_collection { get; } = new();

        public NotificationViewModel(NotificationService notification_service, int _user_id)
        {
            this._notification_service = notification_service;
            this.userId = _user_id;
        }

        public int userId { get; set; }

        public async Task loadNotificationsAsync(int userid)
        {
            List<Notification> notifications = await this._notification_service.getNotificationsByUserIdAsync(userid);
            notifications_collection.Clear();
            foreach (var n in notifications)
                notifications_collection.Add(n);
        }

        public async Task deleteNotificationAsync(int notification_id, int user_id)
        {
                await _notification_service.deleteNotificationAsync(notification_id, user_id);
                var item = findNotificationById(notification_id);
                if (item != null)
                    notifications_collection.Remove(item);
        }

        private Notification findNotificationById(int id)
        {
            foreach (var notification in notifications_collection)
            {
                if (notification.notificationId == id)
                    return notification;
            }
            return null;
        }
    }
}