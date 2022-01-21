using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WannaWhat.DTOs;
using WannaWhat.ViewModels;

namespace WannaWhat.App.Interfaces
{
    public interface IAuthService
    {
        Task<UserRegistrationResponse> RegisterUser(RegisterViewModel userForRegistration);
        Task<UserRegistrationResponse> IsRegistrationViewModelValid(RegisterViewModel userForRegistration);

    }
}
