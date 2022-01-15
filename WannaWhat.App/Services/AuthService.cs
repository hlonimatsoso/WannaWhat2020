using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WannaWhat.App.Interfaces;
using WannaWhat.DTOs;
using WannaWhat.ViewModels;

namespace WannaWhat.App.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public AuthService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:5002/");
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<UserRegistrationResponse> RegisterUser(RegisterViewModel userForRegistration)
        {
            if (userForRegistration.UserName is null)
            {
                var respo = new UserRegistrationErrorResponse();
                respo.errors = new WannaWhat.DTOs.Errors();
                respo.errors.Email = new List<string> { "Username may not be null" };
                return respo;
            }
            var content = JsonSerializer.Serialize(userForRegistration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var registrationResult = await _client.PostAsync("api/user/registration", bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();
            if (!registrationResult.IsSuccessStatusCode)
            {
                Console.WriteLine($"Response: {registrationContent}");
                var result = JsonSerializer.Deserialize<UserRegistrationErrorResponse>(registrationContent, _options);
                return result;
            }
            return new UserRegistrationResponseDto { };
        }
    }
}
