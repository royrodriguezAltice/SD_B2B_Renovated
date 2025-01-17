using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using SD.API.Config;

namespace SD.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            // Usa Autofac como el resolvedor de dependencias
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            // Configuración de dependencias en Autofac usando IocConfiguration
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                IocConfiguration.Configure(containerBuilder, builder.Configuration);
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Acceso/Index";

        //hora de Cierre
        var HoraCierre = "18:00:00";
        var partesHoraCierre = HoraCierre.Split(new char[1] { ':' });

        var fechaHoraActual = DateTime.Now;
        var fechaHoraCierre = new DateTime(fechaHoraActual.Year, fechaHoraActual.Month, fechaHoraActual.Day,
                   int.Parse(partesHoraCierre[0]), int.Parse(partesHoraCierre[1]), int.Parse(partesHoraCierre[2]));
        TimeSpan ts;
        if (fechaHoraCierre > fechaHoraActual)
            ts = fechaHoraCierre - fechaHoraActual;
        else
        {
            fechaHoraCierre = fechaHoraCierre.AddDays(1);
            ts = fechaHoraCierre - fechaHoraActual;
        }

        Task.Delay(ts).ContinueWith((x) => {
            //SshController.CloseConnection();
            //DBOracle.CloseConnection();
        });

        option.ExpireTimeSpan = ts;
        option.AccessDeniedPath = "/Home/Privacy";
    });

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Access}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
