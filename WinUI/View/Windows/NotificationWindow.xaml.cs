using Microsoft.UI.Xaml;
using WinUI.ViewModel;

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
            await ViewModel.LoadNotificationsAsync(ViewModel._user_id);
        }
    }
}