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
        Task<GeneralResponseDTO<bool>> RegisterUser(RegisterViewModel userForRegistration);
        Task<UserRegistrationResponse> IsRegistrationViewModelValid(RegisterViewModel userForRegistration);

    }
}
