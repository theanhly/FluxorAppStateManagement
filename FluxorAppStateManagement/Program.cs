using FluxorAppStateManagement.Components;
using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.State;
using FluxorAppStateManagement.State.Factories;

namespace FluxorAppStateManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddSingleton<StateManager>();
            builder.Services.AddSingleton<CounterService>();
            builder.Services.AddSingleton<CounterFactory>();
            builder.Services.AddSingleton<WeatherService>();
            builder.Services.AddSingleton<WeatherFactory>();
            builder.Services.AddSingleton<AutomaticServices>();

            var app = builder.Build();
            app.Services.GetRequiredService<AutomaticServices>();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
