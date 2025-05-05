using Domain;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using WinUI.Service.NotificationServiceFile;

namespace WinUI.ViewModel.NotificationViewModel
{
    public class NotificationViewModel : INotificationViewModel, INotifyPropertyChanged
    {
        private readonly INotificationService notificationService;

        public ObservableCollection<Notification> Notifications { get; }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            private set
            {
                if (errorMessage != value)
                {
                    errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        public NotificationViewModel(INotificationService notificationService)
        {
            this.notificationService = notificationService;
            Notifications = new ObservableCollection<Notification>();
        }

        public async Task LoadNotificationsAsync(int userId)
        {
            try
            {
                var loaded = await notificationService.GetByUserIdAsync(userId);
                Notifications.Clear();
                foreach (var notification in loaded)
                    Notifications.Add(notification);

                ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to load notifications: {ex.Message}";
            }
        }

        public async Task DeleteNotificationAsync(int notificationId, int userId)
        {
            try
            {
                await notificationService.DeleteAsync(notificationId, userId);
                var item = FindNotificationById(notificationId);
                if (item != null)
                    Notifications.Remove(item);

                ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to delete notification: {ex.Message}";
            }
        }

        private Notification FindNotificationById(int id)
        {
            foreach (var notification in Notifications)
            {
                if (notification.NotificationId == id)
                    return notification;
            }
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
