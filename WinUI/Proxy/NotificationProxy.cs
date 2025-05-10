using ClassLibrary.IRepository;
using ClassLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinUI.Proxy
{
    /// <summary>
    /// Provides proxy methods to interact with the Notification Web API.
    /// </summary>
    public class NotificationProxy : INotificationRepository
    {
        /// <summary>
        /// The HTTP client used for sending requests to the Web API.
        /// </summary>

        private readonly HttpClient _http_client;
        private static readonly string s_base_api_url = Config._base_api_url;
        
        public NotificationProxy(HttpClient _http_client)
        {
            this._http_client = _http_client;
        }

        /// <inheritdoc/>
        public async Task<List<Notification>> getAllNotificationsAsync()
        {
            var response = await _http_client.GetAsync(s_base_api_url + "notification");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var notifications = JsonSerializer.Deserialize<List<Notification>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return notifications ?? new List<Notification>();
        }

        /// <inheritdoc/>
        public async Task<List<Notification>> getNotificationsByUserIdAsync(int _user_id)
        {
            var response = await _http_client.GetAsync(s_base_api_url + $"/notification/user/{_user_id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var notifications = JsonSerializer.Deserialize<List<Notification>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return notifications ?? new List<Notification>();
        }

        /// <inheritdoc/>
        public async Task<Notification> getNotificationByIdAsync(int _id)
        {
            HttpResponseMessage _response = await _http_client.GetAsync(s_base_api_url +  $"notification/{_id}");

            if (_response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"Notification with ID {_id} was not found.");
            }

            _response.EnsureSuccessStatusCode();

            string _json = await _response.Content.ReadAsStringAsync();

            Notification _notification = JsonSerializer.Deserialize<Notification>(_json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return _notification ?? throw new Exception("Failed to deserialize notification.");
        }

        /// <inheritdoc/>
        public async Task addNotificationAsync(Notification _notification)
        {
            StringContent _jsonContent = new StringContent(
                JsonSerializer.Serialize(_notification),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage _response = await _http_client.PostAsync(s_base_api_url +  "notification", _jsonContent);
            _response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc/>
        public async Task deleteNotificationAsync(int _id)
        {
            HttpResponseMessage _response = await _http_client.DeleteAsync(s_base_api_url + $"notification/delete/{_id}");

            if (_response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"Notification with ID {_id} was not found.");
            }

            _response.EnsureSuccessStatusCode();
        }
    }
}
