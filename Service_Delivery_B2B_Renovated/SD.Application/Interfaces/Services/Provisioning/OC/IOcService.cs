using Microsoft.AspNetCore.Http;
using SD.Application.Provisioning.Control_OC.OC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Provisioning.Control_OC.OC.DTOs.GetOC;

namespace SD.Application.Interfaces.Services.Provisioning.OC
{
	public interface IOcService
	{
		#region Get
		Task<GetOC> GetOcByOcAsync(string oc); 
		Task<GetOcDTO> GetOcByIdAsync(int id); 
		Task<List<GetOC>> GetAllOcAsync();
		#endregion
		#region Add
		Task<GetOcDTO> CreateOcAsync(CreateOcDTO oc, List<IFormFile> files);
		#endregion
		#region Update
		Task<GetOC> UpdateOcAsync(UpdateOC oc, int id, List<IFormFile> files);
		#endregion
		#region Remove
		Task<bool> DeleteOcAsync(int id);
		#endregion
		#region Methods
		Task<string> GenerateNewOc();
		#endregion
	}
}
