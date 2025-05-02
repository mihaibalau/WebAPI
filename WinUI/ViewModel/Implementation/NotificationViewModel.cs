using Domain;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using WinUI.Service.NotificationServiceFile;
using WinUI.ViewModel.Interface;

namespace WinUI.ViewModel.Implementation
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
