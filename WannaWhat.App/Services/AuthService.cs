using Microsoft.AspNetCore.Mvc;
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
        public async Task<GeneralResponseDTO<bool>> RegisterUser(RegisterViewModel userForRegistration)
        {
            UserRegistrationResponse result = new UserRegistrationResponse();
            var content = JsonSerializer.Serialize(userForRegistration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var registrationResult = await _client.PostAsync("api/users/registration", bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();
            Console.WriteLine($"registrationResult: {registrationResult}");

            //if (!registrationResult.IsSuccessStatusCode)
            //{
            Console.WriteLine($"Response: {registrationContent}");
            var resultDes = JsonSerializer.Deserialize<GeneralResponseDTO<bool>>(registrationContent, _options);
            Console.WriteLine($"Response deserialised: {resultDes}");

            return resultDes;
            //}
            //return new UserRegistrationResponseDto { };
        }

        public Task<UserRegistrationResponse> IsRegistrationViewModelValid(RegisterViewModel userForRegistration)
        {
            UserRegistrationResponse errorResponse = new UserRegistrationResponse();

            if (string.IsNullOrEmpty(userForRegistration.UserName))
            {
                errorResponse.errors.PersonalInfoName.Add("Username may not be null");
            }

            return Task.FromResult(errorResponse);
        }
    }
}
