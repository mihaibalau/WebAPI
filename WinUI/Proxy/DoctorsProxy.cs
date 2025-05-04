using ClassLibrary.IRepository;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace WinUI.Proxy
{
    internal class DoctorsProxy : IDoctorRepository
    {
        private readonly HttpClient _client;

        public DoctorsProxy(HttpClient client)
        {
            _client = client;
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            // Serialize ( convert object -> string )  the doctor object into JSON
            var content = new StringContent(
                JsonSerializer.Serialize(doctor),
                Encoding.UTF8,
                "application/json");

            await _client.PostAsync("https://localhost:7004/api/doctor", content);
        }


        public Task DeleteDoctorAsync(int id)
        {
             return _client.DeleteAsync($"https://localhost:7004/api/doctor/delete/{id}");
        }

        public Task<List<Doctor>> GetAllDoctorsAsync()
        {
            var response = _client.GetStringAsync("https://localhost:7004/api/doctor");
            var doctors = JsonSerializer.Deserialize<List<Doctor>>(response.Result); // Deserialize ( convert string -> object ) the JSON response into a list of Doctor objects
            return Task.FromResult(doctors);
        }

        public Task<Doctor> GetDoctorByUserIdAsync(int id)
        {
            var response = _client.GetStringAsync($"https://localhost:7004/api/doctor/{id}");
            var doctor = JsonSerializer.Deserialize<Doctor>(response.Result);  
            return Task.FromResult(doctor);

        }

        public Task<List<Doctor>> GetDoctorsByDepartmentIdAsync(int departmentId)
        {
            var response = _client.GetStringAsync($"https://localhost:7004/api/doctor/doctor/{departmentId}");
            var doctors = JsonSerializer.Deserialize<List<Doctor>>(response.Result);  
            return Task.FromResult(doctors);
        }

    }
}
