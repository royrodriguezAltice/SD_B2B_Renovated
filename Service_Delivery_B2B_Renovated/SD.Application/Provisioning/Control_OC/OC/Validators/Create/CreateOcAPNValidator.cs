using FluentValidation;
using SD.Application.Provisioning.Control_OC.OC.DTOs;
using SD.Domain.Enums.Provisioning.Control_OC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Provisioning.Control_OC.OC.Validators.Create
{
	public class CreateOcAPNValidator : AbstractValidator<CreateOcAPNDTO>
	{
		public CreateOcAPNValidator() 
		{
			#region Validaciones Generales

			RuleFor(oc => oc.Os)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.NombreFactura)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.Cliente)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.NumeroCuenta)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.Circuito)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.IdGrado)
				.NotEqual("Seleccionar...").WithMessage("Debe Seleccionar un grado.")
				.WithMessage("");

			RuleFor(oc => oc.Actividad)
				.NotEqual("Seleccionar...").WithMessage("Debe Seleccionar un actividad.")
				.WithMessage("");

			RuleFor(oc => oc.Plan)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.Bw)
				.NotEqual("Seleccionar...").WithMessage("Debe Seleccionar un bandwith de bajada.")
				.WithMessage("");

			RuleFor(oc => oc.Bws)
				.NotEqual("Seleccionar...").WithMessage("Debe Seleccionar un bandwith de subida.")
				.WithMessage("");

			RuleFor(oc => oc.Producto)
				.NotEqual("Seleccionar...").WithMessage("Debe Seleccionar un producto.")
				.WithMessage("");

			RuleFor(oc => oc.Ttk)
					.NotEmpty().WithMessage("")
					.WithMessage("");

			#endregion

			RuleFor(oc => oc.ApnData.NombreApn)
						.NotEmpty().WithMessage("")
						.WithMessage("");

			RuleFor(oc => oc.NodoAcceso)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.UplinkAcceso)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.VlanAcceso)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.ApnData.SegundaVlan)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.ApnData.RouterTarget)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.EquipoAg)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.ApnData.SegundoEquipo)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.TipoPuertoAg)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.PuertoAg)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.VpnAg)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.IpPrefix)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.IpIspAg)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.IpClienteWAg)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.ApnData.IpRoutePolicy)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.ApnData.SecondIpRoutePolicy)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.IpClienteWMask)
				.NotEqual("Seleccionar...").WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.ApnData.IpRoutePolicy)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.ApnData.MaskRoutePolicy)
				.NotEqual("Seleccionar...").WithMessage("")
				.WithMessage("");

		}
	}
}
