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
        private readonly INotificationService _notification_service;
        public int _user_id { get; set; }

        public ObservableCollection<Notification> _notifications_collection { get; } = new();

        public NotificationViewModel(NotificationService _notification_service, int _user_id)
        {
            this._notification_service = _notification_service;
            this._user_id = _user_id;
        }

        public async Task loadNotificationsAsync(int _user_id)
        {
            List<Notification> _notifications = await this._notification_service.getNotificationsByUserIdAsync(_user_id);

            _notifications_collection.Clear();

            foreach (Notification _notification in _notifications)
                _notifications_collection.Add(_notification);
        }

        public async Task deleteNotificationAsync(int _notification_id, int _user_id)
        {
                await this._notification_service.deleteNotificationAsync(_notification_id, _user_id);
                Notification _notification = findNotificationByIdAsync(_notification_id);
                if (_notification != null)
                    _notifications_collection.Remove(_notification);
        }

        public Notification findNotificationByIdAsync(int _id)
        {
            foreach (Notification notification in _notifications_collection)
            {
                if (notification._notification_id == _id)
                    return notification;
            }
            return null;
        }
    }
}