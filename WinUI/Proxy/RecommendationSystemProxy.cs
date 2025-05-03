using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ClassLibrary.IRepository;
using Domain;

namespace WinUI.Proxy
{
    internal class RecommendationSystemProxy : IDoctorRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5005/";

        public RecommendationSystemProxy(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}api/doctor");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Doctor>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<Doctor> GetDoctorByUserIdAsync(int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}api/doctor/user/{userId}");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Doctor>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<Doctor>> GetDoctorsByDepartmentIdAsync(int departmentId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}api/doctor/department/{departmentId}");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Doctor>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> AddDoctorAsync(Doctor doctor)
        {
            string json = JsonSerializer.Serialize(doctor);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}api/doctor", content);
            response.EnsureSuccessStatusCode();

            return true;
        }

        public async Task<bool> DeleteDoctorAsync(int doctorId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{_baseUrl}api/doctor/{doctorId}");
            response.EnsureSuccessStatusCode();

            return true;
        }

        Task IDoctorRepository.AddDoctorAsync(Doctor doctor)
        {
            return AddDoctorAsync(doctor);
        }

        Task IDoctorRepository.DeleteDoctorAsync(int id)
        {
            return DeleteDoctorAsync(id);
        }
    }
}
