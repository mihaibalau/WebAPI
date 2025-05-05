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
using Domain;
using System.Diagnostics;
using WinUI.ViewModel.NotificationViewModel;
using WinUI.Service.NotificationFile;
using WinUI.Proxy;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotificationDetailsView : Page
    {
        public NotificationDetailsView()
        {
            this.InitializeComponent();
        }

        public Notification SelectedNotification { get; set; }

        private INotificationViewModel NotificationViewModel = new NotificationViewModel(new NotificationService(new NotificationProxy(new System.Net.Http.HttpClient())));
        protected override void OnNavigatedTo(NavigationEventArgs error)
        {
            base.OnNavigatedTo(error);
            if (error.Parameter is Notification notification)
            {
                this.SelectedNotification = notification;
                Debug.WriteLine($"Viewing notification detail:Message={this.SelectedNotification.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs error)
        {
            this.Frame.Navigate(typeof(NotificationView), this.SelectedNotification.UserId);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs error)
        {
            var button = sender as Button;
            if (button != null)
            {
                var notification = button.DataContext as Notification;
                if (notification != null)
                {
                    this.NotificationViewModel.DeleteNotificationAsync(notification.NotificationId, this.SelectedNotification.UserId);
                }
            }
        }
    }
}
