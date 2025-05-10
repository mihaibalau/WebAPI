using ClassLibrary.Domain;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using WinUI.Service;
using System.ComponentModel;

namespace WinUI.ViewModel
{
    public class NotificationViewModel : INotificationViewModel
    {
        private readonly NotificationService _notification_service;
        public int _user_id { get; set; }

        public ObservableCollection<Notification> _notifications_collection { get; } = new();

        public NotificationViewModel(NotificationService _notification_service, int _user_id)
        {
            this._notification_service = _notification_service;
            this._user_id = _user_id;
        }

        public async Task loadNotificationsAsync(int userid)
        {
            List<Notification> notifications = await this._notification_service.getNotificationsByUserIdAsync(userid);
            _notifications_collection.Clear();
            foreach (var n in notifications)
                _notifications_collection.Add(n);
        }

        public async Task deleteNotificationAsync(int notification_id, int user_id)
        {
                await _notification_service.deleteNotificationAsync(notification_id, user_id);
                var item = findNotificationById(notification_id);
                if (item != null)
                    _notifications_collection.Remove(item);
        }

        private Notification findNotificationById(int id)
        {
            foreach (var notification in _notifications_collection)
            {
                if (notification._notificationId == id)
                    return notification;
            }
            return null;
        }
    }
}