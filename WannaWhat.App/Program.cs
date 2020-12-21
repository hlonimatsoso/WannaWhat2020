using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNet.SignalR.Client;
using WannaWhat.App.Interfaces;
using WannaWhat.App.SignalR;

namespace WannaWhat.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("oidc", options.ProviderOptions);
            });

            
            builder.Services.AddTransient<ISignalRConnection, SignalRConnection>();
            builder.Services.AddTransient<ISignalRClient, SignalRClient>();


            //builder.Services.AddScoped<HubConnection>();

            builder.Services.AddHttpClient("userApi", client => 
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                      .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            await builder.Build().RunAsync();
        }
    }
}
