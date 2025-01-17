using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Admin.Acceso.DTOs
{
	public class GetUsuarioInfoDTO()
	{
		public string CodigoUsu { get; set; }
		public string? NombreUsu { get; set; }
		public string? Estado { get; set; }
		public string? CodigoDep { get; set; }
		public ClaimsIdentity claims { get; set; }
	};
}
