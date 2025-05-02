using Domain;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using WinUI.Service.NotificationServiceFile;
using WinUI.ViewModel.Interface;

namespace WinUI.ViewModel.Implementation
{
    public class NotificationViewModel : INotificationViewModel
    {
        private readonly INotificationService notificationService;

        public ObservableCollection<Notification> Notifications { get; }

        public NotificationViewModel(INotificationService notificationService)
        {
            this.notificationService = notificationService;
            Notifications = new ObservableCollection<Notification>();
        }
    }
}
