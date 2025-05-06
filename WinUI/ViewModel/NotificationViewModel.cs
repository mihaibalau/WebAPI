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
        private readonly NotificationService notification_service;

        public ObservableCollection<Notification> notifications_collection { get; } = new();

        public NotificationViewModel(NotificationService notification_service, int _user_id)
        {
            this.notification_service = notification_service;
            this._user_id = _user_id;
        }

        public int _user_id { get; set; }

        public async Task LoadNotificationsAsync(int userId)
        {
            List<Notification> notifications = await this.notification_service.GetNotificationsByUserIdAsync(userId);
            notifications_collection.Clear();
            foreach (var n in notifications)
                notifications_collection.Add(n);
        }
    }
}