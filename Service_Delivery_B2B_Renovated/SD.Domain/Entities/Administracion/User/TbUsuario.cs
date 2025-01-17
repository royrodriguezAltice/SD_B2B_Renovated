using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Administracion.User
{
	public partial class TbUsuario
	{
		public string CodigoUsu { get; set; } = null!;

		public string? LoginUsu { get; set; }

		public string? ClaveUsu { get; set; }

		public string? NombreUsu { get; set; }

		public DateTime? FechaCrea { get; set; }

		public DateTime? FechaMod { get; set; }

		public string? Estado { get; set; }

		public string? CodigoDep { get; set; }

		public string? UsrTacas { get; set; }

		public string? ClaveTacas { get; set; }

		public string? UsrMerengue { get; set; }

		public string? ClaveMerengue { get; set; }

	}
}
