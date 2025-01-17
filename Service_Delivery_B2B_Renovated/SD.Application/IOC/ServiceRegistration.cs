using Autofac;
using SD.Application.Admin.Acceso.Service;
using SD.Application.Common.Services.Generics;
using SD.Application.Helpers.Security;
using SD.Application.Interfaces.Helpers.Security;
using SD.Application.Interfaces.Services.Admin.Acceso;
using SD.Application.Interfaces.Services.Generics;
using SD.Application.Interfaces.Services.Provisioning.OC;
using SD.Application.Provisioning.Control_OC.OC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IContainer = Autofac.IContainer;

namespace SD.Application.IOC
{
	public static class ServiceRegistration
	{
		public static void AddApplicationLayer(ContainerBuilder builder)
		{
			builder.RegisterGeneric(typeof(GenericService<,>)).As(typeof(IGenericService<,>));
			builder.RegisterType<OcService>().As<IOcService>().InstancePerLifetimeScope();
			builder.RegisterType<AccesoService>().As<IAccesoService>().InstancePerLifetimeScope();
			builder.RegisterType<SecretHasher>().As<ISecretHasher>().InstancePerLifetimeScope();
		}
	}
}
