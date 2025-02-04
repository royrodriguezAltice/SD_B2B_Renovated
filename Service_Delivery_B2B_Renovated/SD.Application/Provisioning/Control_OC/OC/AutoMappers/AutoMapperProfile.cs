using AutoMapper;
using SD.Application.Provisioning.Control_OC.OC.DTOs;
using SD.Domain.Entities.Provisioning.Control_OC.Oc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Provisioning.Control_OC.OC.DTOs.GetOC;

namespace SD.Application.Provisioning.Control_OC.OC.AutoMappers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile() 
		{
			CreateMap<TbOc, CreateOcIntAsym1IPDTO>();

			CreateMap<TbOc, CreateOcAPNDTO>();

			CreateMap<TbOc, CreateOcSIPDatosDTO>();

			CreateMap<TbOc, GetOcIntAsym1IPDTO>();

			CreateMap<TbOc, GetOcAPNDTO>();
			
			CreateMap<TbOc, GetOcSIPDatosDTO>();

			CreateMap<TbOc, UpdateOC>();

			CreateMap<GetOcDTO, CreateOcIntAsym1IPDTO>();

			CreateMap<GetOcDTO, CreateOcAPNDTO>();
			
			CreateMap<GetOcDTO, CreateOcSIPDatosDTO>();

			CreateMap<GetOcDTO, UpdateOC>();
			
		}
	}
}
