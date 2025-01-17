using SD.Application.Admin.Acceso.DTOs;
using SD.Application.Admin.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Admin.User.DTOs.GetUsuario;
using GetUsuarioInfoDTO = SD.Application.Admin.Acceso.DTOs.GetUsuarioInfoDTO;

namespace SD.Application.Interfaces.Services.Admin.Acceso
{
	public interface IAccesoService
	{
		Task<GetUsuarioInfoDTO> Login(CreateLoginDTO rc);
		Task LogOut();
		GetUsuarioSSHDTO ValidateUserSSH(string userName);
	}
}
