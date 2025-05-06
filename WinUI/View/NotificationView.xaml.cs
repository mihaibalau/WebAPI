using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using WinUI.ViewModel.NotificationViewModel;
using WinUI.Service.NotificationFile;
using ClassLibrary.IRepository;
using WinUI.Proxy;
using Domain;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotificationView : Page
    {
        public NotificationView()
        {
            this.InitializeComponent();
            this.NotificationViewModel = new NotificationViewModel(new NotificationService(new NotificationProxy(new System.Net.Http.HttpClient())));
            this.NotificationsListView.DataContext = this.NotificationViewModel;
        }

        public INotificationViewModel NotificationViewModel { get; }
        public int UserID { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs error)
        {
            base.OnNavigatedTo(error);
            if (error.Parameter is int userId)
            {
                this.UserID = userId;
                this.NotificationViewModel.LoadNotificationsAsync(userId);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs error)
        {
            this.Frame.Navigate(typeof(MainWindow));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs error)
        {
            var button = sender as Button;
            if (button != null)
            {
                var notification = button.DataContext as Notification;
                if (notification != null)
                {
                    this.NotificationViewModel.DeleteNotificationAsync(notification.NotificationId, this.UserID);
                }
            }
        }

        private void NotificationsListView_ItemClick(object sender, ItemClickEventArgs error)
        {
            if (error.ClickedItem is Notification selectedNotification)
            {
                this.Frame.Navigate(typeof(NotificationDetailsView), selectedNotification);
            }
        }
    }
}
