using FluentValidation;
using SD.Application.Provisioning.Control_OC.OC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Provisioning.Control_OC.OC.Validators.Create
{
	public class CreateOcValidator : AbstractValidator<CreateOcDTO>
	{
		public CreateOcValidator() 
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
		}
	}
}
