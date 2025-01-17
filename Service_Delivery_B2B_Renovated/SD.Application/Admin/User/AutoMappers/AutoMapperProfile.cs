using AutoMapper;
using SD.Application.Admin.Acceso.DTOs;
using SD.Application.Admin.User.DTOs;
using SD.Domain.Entities.Administracion.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Admin.User.DTOs.GetUsuario;

namespace SD.Application.Admin.User.AutoMappers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile() 
		{
			CreateMap<TbUsuario?, GetUsuarioDTO?>().ReverseMap();
			CreateMap<TbUsuario, GetUsuarioDTO>().ReverseMap();
			CreateMap<TbUsuario, GetUsuarioSSHDTO>();
			CreateMap<TbUsuario, GetUsuarioInfoDTO>().ReverseMap();
			//CreateMap<TbUsuario?, GetUsuarioInfoDTO?>().ReverseMap();
		}
	}
}
