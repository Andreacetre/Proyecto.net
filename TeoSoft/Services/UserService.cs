using TeoSoft.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TeoSoft.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<User>>("usuario") ?? Enumerable.Empty<User>();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("usuario", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<User> UpdateUserAsync(string id, User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"usuario/{id}", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"usuario/{id}");
            return response.IsSuccessStatusCode;
        }

        public Task<string> GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CanDeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}