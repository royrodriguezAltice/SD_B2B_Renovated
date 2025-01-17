using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Admin.User.DTOs
{
	public class GetUsuario
	{

		public record GetUsuarioSSHDTO(string ClaveTacas,
									   string UsrTacas);

	}
}
