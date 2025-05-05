using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using ClassLibrary.IRepository;
using WinUI.Proxy;
using WinUI.View;

namespace WinUI
{
    public sealed partial class MainWindow : Window
    {
        // HttpClient instance to make HTTP requests
        private static readonly HttpClient client = new HttpClient();
        
        public MainWindow()
        {
            this.InitializeComponent();
        }

        // Button click event to make the GET request and update the button's content
        private async void notificationButton_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(typeof(UserView));
        }
    }
}
