using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

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
        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Make the GET request to your WebAPI
                var response = await client.GetStringAsync("https://localhost:7004/api/department");

                // Update the Button content with the response
                myButton.Content = response;

                // Optionally, update a TextBlock with the response
                responseText.Text = response;
            }
            catch (Exception ex)
            {
                // Handle error (e.g., if the server is not running or there's no internet connection)
                myButton.Content = "Failed to load data";
                responseText.Text = $"Error: {ex.Message}";
            }
        }
    }
}
