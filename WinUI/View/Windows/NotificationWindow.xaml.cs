using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUI.ViewModel;
using ClassLibrary.Domain;

namespace WinUI.View
{
    public sealed partial class NotificationWindow : Window
    {
        public NotificationViewModel ViewModel { get; }

        public NotificationWindow(NotificationViewModel viewModel)
        {
            this.InitializeComponent();
            ViewModel = viewModel;

            // Load notifications when window is created
            LoadNotifications();
        }

        private async void LoadNotifications()
        {
            // Call the viewmodel method to load notifications for the current user
            await ViewModel.loadNotificationsAsync(ViewModel.userId);
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs error)
        {
            var button = sender as Button;
            if (button != null)
            {
                var notification = button.DataContext as Notification;
                if (notification != null)
                {
                    await this.ViewModel.deleteNotificationAsync(notification._notificationId, ViewModel.userId);
                }
            }
        }
    }
}