using Deserialization.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Deserialization
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            });
            services.AddTransient<IWorkoutService, WorkoutService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IWorkoutService workoutService)
        {
            app.UseMvcWithDefaultRoute();
            workoutService.ResetAndSeedDatabase();
        }
    }
}