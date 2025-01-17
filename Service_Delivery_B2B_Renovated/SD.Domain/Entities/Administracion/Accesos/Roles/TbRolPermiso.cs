using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Administracion.Accesos.Roles
{
	public class TbRolPermiso
	{

		public int Id { get; set; }
		public string CodigoUsu { get; set; } = null!;

		public string? NombreUsu { get; set; }

		public string? Rol { get; set; }

		public string? Permiso { get; set; }

	}
}
