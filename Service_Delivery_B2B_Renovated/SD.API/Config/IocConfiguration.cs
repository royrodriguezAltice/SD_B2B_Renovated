using Autofac;
using Autofac.Features.Variance;
using SD.Persistence.IOC;
using SD.Application.IOC;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using IContainer = Autofac.IContainer;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using System.Reflection;
using SD.API.Controllers;
using SD.Domain.Interfaces.Repositories.Generic;
using SD.Persistence.Repositories.Generic;
using System.Web.Mvc;
using Autofac.Builder;
using SD.Application.Admin.User.AutoMappers;

namespace SD.API.Config
{
	public class IocConfiguration
	{
		public static IContainer container { get; set; }

		public static T GetInstance<T>()
		{
			return container.Resolve<T>();
		}

		public static void Configure(ContainerBuilder builder, IConfiguration configuration)
		{
            builder.RegisterSource(new ContravariantRegistrationSource());

            RegisterPersistenceLayer(builder, configuration);
            RegisterApplicationLayer(builder);
            RegisterRepositories(builder);
            RegisterControllers(builder);
            RegisterAutoMapper(builder);

        }

		private static void RegisterAutoMapper(ContainerBuilder builder)
		{
			//builder.RegisterAutoMapper(Assembly.GetExecutingAssembly());
            builder.RegisterAutoMapper(typeof(AutoMapperProfile).Assembly);
        }

        private static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterType<HomeController>().InstancePerLifetimeScope();
            builder.RegisterType<AccessController>().InstancePerLifetimeScope();
        }

        private static void RegisterApplicationLayer(ContainerBuilder builder)
		{
			SD.Application.IOC.ServiceRegistration.AddApplicationLayer(builder);
		}

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
        }

        private static void RegisterPersistenceLayer(ContainerBuilder builder, IConfiguration config)
		{
			SD.Persistence.IOC.ServiceRegistration.AddPersistenceLayer(builder, config);
		}

	}
}
