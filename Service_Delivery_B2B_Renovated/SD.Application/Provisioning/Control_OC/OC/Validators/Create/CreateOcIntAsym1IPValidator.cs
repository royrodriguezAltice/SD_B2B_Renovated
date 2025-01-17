using FluentValidation;
using SD.Application.Provisioning.Control_OC.OC.DTOs;
using SD.Domain.Entities.Provisioning.Control_OC.Oc;
using SD.Domain.Enums.Provisioning.Control_OC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Provisioning.Control_OC.OC.Validators.Create
{
    public class CreateOcIntAsym1IPValidator : AbstractValidator<CreateOcIntAsym1IPDTO>
    {

        public CreateOcIntAsym1IPValidator(OcTechnologies technology) 
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

			RuleFor(oc => oc.NodoAcceso)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.UplinkAcceso)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.PON)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.VlanAcceso)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.PuertoAcceso)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.Serial_ONT)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			switch (technology)
			{
				case OcTechnologies.GPON:

					RuleFor(oc => oc.PosicionOnt)
						.NotEmpty().WithMessage("")
						.WithMessage("");

					RuleFor(oc => oc.MangaGPON)
						.NotEmpty().WithMessage("")
						.WithMessage("");

					RuleFor(oc => oc.PuertoMG)
						.NotEmpty().WithMessage("")
						.WithMessage("");

					RuleFor(oc => oc.CoordenadasMangaX)
						.NotEmpty().WithMessage("")
						.WithMessage("");

					RuleFor(oc => oc.CoordenadasMangaY)
						.NotEmpty().WithMessage("")
						.WithMessage("");

					RuleFor(oc => oc.CoordenadasClienteX)
						.NotEmpty().WithMessage("")
						.WithMessage("");

					RuleFor(oc => oc.CoordenadasClienteY)
						.NotEmpty().WithMessage("")
						.WithMessage("");

					break;

				case OcTechnologies.FO_P2P:

					RuleFor(oc => oc.CoordenadasClienteX)
						.NotEmpty().WithMessage("")
						.WithMessage("");

					RuleFor(oc => oc.CoordenadasClienteY)
						.NotEmpty().WithMessage("")
						.WithMessage("");

					RuleFor(oc => oc.Site)
						.NotEmpty().WithMessage("")
						.WithMessage("");

					break;

				case OcTechnologies.MW_P2P:

					RuleFor(oc => oc.CoordenadasClienteX)
						.NotEmpty().WithMessage("")
						.WithMessage("");

					RuleFor(oc => oc.CoordenadasClienteY)
						.NotEmpty().WithMessage("")
						.WithMessage("");

					RuleFor(oc => oc.Site)
						.NotEmpty().WithMessage("")
						.WithMessage("");

					break;
			};

			RuleFor(oc => oc.EquipoAg)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.TipoPuertoAg)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.PuertoAg)
				.NotEmpty().WithMessage("")
				.WithMessage("");

			RuleFor(oc => oc.VpnAg)
				.NotEqual("Seleccionar...").WithMessage("")
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

			RuleFor(oc => oc.IpClienteWMask)
				.NotEqual("Seleccionar...").WithMessage("")
				.WithMessage("");

		}
	}
}
