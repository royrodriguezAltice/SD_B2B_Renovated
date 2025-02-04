using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using SD.Application.Interfaces.Helpers.Files;
using SD.Application.Interfaces.Services.Provisioning.OC;
using SD.Application.Provisioning.Control_OC.OC.DTOs;
using SD.Application.Provisioning.Control_OC.OC.Exceptions;
using SD.Application.Provisioning.Control_OC.OC.Validators.Create;
using SD.Application.Provisioning.Control_OC.OC.Validators.Update;
using SD.Domain.Entities.Provisioning.Control_OC.Oc;
using SD.Domain.Enums.Common.Files;
using SD.Domain.Enums.Provisioning.Control_OC;
using SD.Domain.Interfaces.Repositories.Generic;
using SD.Domain.Interfaces.Repositories.Provisioning.Control_OC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Provisioning.Control_OC.OC.DTOs.GetOC;

namespace SD.Application.Provisioning.Control_OC.OC.Services
{
	public class OcService : IOcService
	{
		private readonly IOcRepository<TbOc> _tbOcRepository;
		private readonly IMapper _mapper;
		private readonly IFilesManager _filesManager;

		public OcService(IGenericRepository<TbOc> tbOcRepository, IMapper mapper)
		{
			_tbOcRepository = (IOcRepository<TbOc>?)tbOcRepository;
			_mapper = mapper;
		}

		public async Task<GetOcDTO> CreateOcAsync(CreateOcDTO oc, List<IFormFile> files)
		{
			try
			{
				ValidationResult validationResult = null;

				OcTechnologies technology = (OcTechnologies)Enum.Parse(typeof(OcTechnologies), oc.GPON_P2P);
				OcActivities activity = (OcActivities)Enum.Parse(typeof(OcActivities), oc.Actividad);
				OcProducts product = (OcProducts)Enum.Parse(typeof(OcProducts), oc.Producto);

				//Validar el modelo según la actividad y el producto
				switch (activity)
				{
					case OcActivities.INSTALACION:

						switch(product)
						{
							case OcProducts.INTERNET_ASIMETRICO_1_IP:

								var createOcIntAsym1IPValidator = new CreateOcIntAsym1IPValidator(technology);
								validationResult = createOcIntAsym1IPValidator.Validate(_mapper.Map<CreateOcIntAsym1IPDTO>(oc));

								break;

							case OcProducts.APN:

								var createOcAPNValidator = new CreateOcAPNValidator();
								validationResult = createOcAPNValidator.Validate(_mapper.Map<CreateOcAPNDTO>(oc));

                                oc.ApnData = await _tbOcRepository.GetOcApnDataAsync(oc.Oc);

                                break;

							case OcProducts.SIPDATOS:

								var createOcSIPDatosValidator = new CreateOcSIPDatosValidator(technology);
								validationResult = createOcSIPDatosValidator.Validate(_mapper.Map<CreateOcSIPDatosDTO>(oc));

								break;

							default:

								var createOcValidator = new CreateOcValidator();
								validationResult = createOcValidator.Validate(oc);

								break;
						}

						break;

					case OcActivities.UPGRADE:

						switch (product)
						{
							case OcProducts.INTERNET_ASIMETRICO_1_IP:
								break;

							case OcProducts.APN:
								break;

							case OcProducts.SIPDATOS:
								break;

							default:
								break;
						}

						break;

					case OcActivities.QUITESE:

						switch (product)
						{
							case OcProducts.INTERNET_ASIMETRICO_1_IP:
								break;

							case OcProducts.APN:
								break;

							case OcProducts.SIPDATOS:
								break;

							default:
								break;
						}

						break;

					default:
						break;
				};

				if (!validationResult.IsValid)
				{
					var errors = validationResult.Errors.Select(e => e.ErrorMessage);
					throw new CreateOcFailedException();
				}

				var existingOc = await _tbOcRepository.VerifyDataExistenceAsync(o => o.Oc == oc.Oc);

				string newOc = await GenerateNewOc();

				if (existingOc.Any())
				{
					throw new OcAlreadyExistsException();
				}

				oc.Oc = newOc;
				if(product == OcProducts.APN)
				{
					oc.ApnId = newOc;
                    oc.ApnData.Id = newOc;

					await _filesManager.FileSaving(files, newOc, FilesTypes.APN_FILE);

                }

				var ocCreated = await _tbOcRepository.CreateAsync(_mapper.Map<TbOc>(oc));

				return _mapper.Map<GetOcDTO>(ocCreated);
			}
			catch
			{
				throw;
			};
		}

		public async Task<bool> DeleteOcAsync(int id)
		{
			try
			{
				var userFound = await _tbOcRepository.GetByIdAsync(id);

				var userDelete = userFound ?? throw new OcNotFoundException();

				bool response = await _tbOcRepository.DeleteAsync(userFound);

				return response == false ? throw new DeleteOcFailedException() : response;
			}
			catch
			{
				throw;
			}
		}

		public async Task<List<GetOC>> GetAllOcAsync()
		{
			try
			{
				var ocs = await _tbOcRepository.GetEverythingAsync();

				return ocs == null ? throw new GetOcsFailedException() : _mapper.Map<List<GetOC>>(ocs);
			}
			catch
			{
				throw;
			};
		}

		public async Task<GetOcDTO> GetOcByIdAsync(int id)
		{
			try
			{
				var oc = await _tbOcRepository.GetByIdAsync(id);

				return oc == null ? throw new OcNotFoundException() : _mapper.Map<GetOcDTO>(oc);
			}
			catch
			{
				throw;
			}
		}

		public async Task<GetOC> GetOcByOcAsync(string oc)
		{
			try
			{
				var order = await _tbOcRepository.VerifyDataExistenceAsync(o => o.Oc == oc);

				return order == null ? throw new OcNotFoundException() : _mapper.Map<GetOC>(oc);
			}
			catch
			{
				throw;
			}
		}

		public async Task<GetOC> UpdateOcAsync(UpdateOC oc, int id, List<IFormFile> files)
		{
			try
			{
				if(oc == null && id == 0)
				{
					throw new OcUpdateFailedException();
				}

				ValidationResult validationResult = null;

				OcTechnologies technology = (OcTechnologies)Enum.Parse(typeof(OcTechnologies), oc.GPON_P2P);
				OcActivities activity = (OcActivities)Enum.Parse(typeof(OcActivities), oc.Actividad);
				OcProducts product = (OcProducts)Enum.Parse(typeof(OcProducts), oc.Producto);

				//Validar el modelo según la actividad y el producto
				switch (activity)
				{
					case OcActivities.INSTALACION:

						switch (product)
						{
							case OcProducts.INTERNET_ASIMETRICO_1_IP:

								var createOcIntAsym1IPValidator = new UpdateOcIntAsym1IPValidator(technology);
								validationResult = createOcIntAsym1IPValidator.Validate(_mapper.Map<UpdateOC>(oc));

								break;

							case OcProducts.APN:

								var createOcAPNValidator = new UpdateOcAPNValidator();
								validationResult = createOcAPNValidator.Validate(_mapper.Map<UpdateOC>(oc));

								oc.ApnData = await _tbOcRepository.GetOcApnDataAsync(oc.Oc);

								break;

							case OcProducts.SIPDATOS:

								var createOcSIPDatosValidator = new UpdateOcSIPDatosValidator(technology);
								validationResult = createOcSIPDatosValidator.Validate(_mapper.Map<UpdateOC>(oc));

								break;

							default:

								var createOcValidator = new UpdateOcValidator();
								validationResult = createOcValidator.Validate(oc);

								break;
						}

						break;

					case OcActivities.UPGRADE:

						switch (product)
						{
							case OcProducts.INTERNET_ASIMETRICO_1_IP:
								break;

							case OcProducts.APN:
								break;

							case OcProducts.SIPDATOS:
								break;

							default:
								break;
						}

						break;

					case OcActivities.QUITESE:

						switch (product)
						{
							case OcProducts.INTERNET_ASIMETRICO_1_IP:
								break;

							case OcProducts.APN:
								break;

							case OcProducts.SIPDATOS:
								break;

							default:
								break;
						}

						break;

					default:
						break;
				};

				if (!validationResult.IsValid)
				{
					var errors = validationResult.Errors.Select(e => e.ErrorMessage);
					throw new OcUpdateFailedException();
				}

				var ocModel = _mapper.Map<TbOc>(oc);

				var ocFound = await _tbOcRepository.VerifyDataExistenceAsync(o => o.Id == id);

				var ocToUpdate = ocFound ?? throw new OcNotFoundException();

				var ocUpdated = await _tbOcRepository.UpdateAsync(ocModel, id);

				return ocUpdated == null ? throw new OcUpdateFailedException() : _mapper.Map<GetOC>(ocUpdated);
			}
			catch
			{
				throw;
			}
		}

		public async Task<string> GenerateNewOc()
		{
			try
			{
				int newNum = 1;

				var ocQueryable = await _tbOcRepository.VerifyDataExistenceAsync();
				var lastOc = ocQueryable.OrderByDescending(o => o.Id).FirstOrDefault();

				if(lastOc != null && !string.IsNullOrEmpty(lastOc.Oc))
				{
					string currentYear = DateTime.Now.ToString("yy");
					string lastOcYears = lastOc.Oc.Substring(2, 2);

					if (currentYear == lastOcYears)
					{
						string secondPart = lastOc.Oc.Substring(5);
						newNum = int.Parse(secondPart) + 1;
					};
				};
				return $"OC{DateTime.Now.ToString("yy")}-{newNum.ToString("D4")}";
			}
			catch
			{
				throw new GenerateNewOcFailedException();
			}
		}

    }
}
