using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SD.Domain.Interfaces.Repositories.Generic;
using SD.Persistence.Context;
using SD.Persistence.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Persistence.IOC
{
	public static class ServiceRegistration
	{
		public static void AddPersistenceLayer(ContainerBuilder builder, IConfiguration configuration)
		{
			builder.Register(c =>
			{
				var optionsBuilder = new DbContextOptionsBuilder<SdB2bDbContext>();
				optionsBuilder.UseMySql(configuration.GetConnectionString("MySqlConnection2"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.5.64-mariadb"));
				return new SdB2bDbContext(optionsBuilder.Options);
			}).AsSelf().InstancePerLifetimeScope();

		}
	}
}
