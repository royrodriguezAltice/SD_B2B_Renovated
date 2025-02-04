using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD.Application.Helpers.Convention;
using SD.Application.Interfaces.Convention;
using SD.Application.Interfaces.Services.Provisioning.OC;
using SD.Application.Interfaces.Services.Provisioning.Templates;
using SD.Application.Provisioning.Control_OC.OC.DTOs;
using SD.Application.Provisioning.Control_OC.Template.DTOs;
using SD.Application.Provisioning.Control_OC.Template.Services;
using SD.Domain.Entities.Provisioning.Control_OC.Oc;
using System.Text;
using static SD.Application.Provisioning.Control_OC.OC.DTOs.GetOC;

namespace SD.API.Controllers.v1.Domain.Provisioning
{
    public class OcsController : Controller
    {
        private readonly IOcService _ocService;
        private readonly IFormatter _formatter;
        private readonly ITemplates _templates;
        private readonly IMapper _mapper;

        public OcsController(IOcService ocService, IMapper mapper, IFormatter formatter,ITemplates templates)
        {
            _ocService = ocService;
            _formatter = formatter;
            _templates = templates;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Templates(CreateOcDTO oc)
        {
            oc.DescripOc = TextsConvention.TemplateDescription(_mapper.Map<GetOcDTO>(oc)).DescripOc;
            oc.Descripcion = TextsConvention.TemplateDescription(_mapper.Map<GetOcDTO>(oc)).Description;

            StringBuilder builder = new StringBuilder();

            //Generar el template
            builder.Append(_templates.GenerateTemplate());

            return Json(new { Template = "", DescripOC = "" });
        }

        [Authorize("PROV_Escritura")]
        public async Task<IActionResult> ControlOC()
        {
            //Obtener el host del navegador
            var host = $"{Request.Host}";
            ViewBag.Host = host;

            var response = new Response<List<GetOC>>();
            try
            {
                response.status = true;
                response.value = await _ocService.GetAllOcAsync();
                response.message = "Data Éxitosa";
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [Authorize("PROV_Lectura")]
        public async Task<IActionResult> ControlOC_ReadOnly()
        {
            //Obtener el host del navegador
            var host = $"{Request.Host}";
            ViewBag.Host = host;

            var response = new Response<List<GetOC>>();
            try
            {
                response.status = true;
                response.value = await _ocService.GetAllOcAsync();
                response.message = "Data Éxitosa";
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [Authorize("PROV_Escritura")]
        public async Task<IActionResult> Create()
        {
            //Obtener el host del navegador
            var host = $"{Request.Host}";
            ViewBag.Host = host;

            var response = new Response<CreateOcDTO>();
            try
            {

            }
            catch(Exception ex)
            {

            }
            return View();
        }

        [Authorize("PROV_Escritura")]
        public async Task<IActionResult> Create(CreateOcDTO oc, List<IFormFile> files)
        {
            //Obtener el host del navegador
            var host = $"{Request.Host}";
            ViewBag.Host = host;

            var response = new Response<GetOcDTO>();
            try
            {
                response.status = true;
                response.value = await _ocService.CreateOcAsync(oc, files);
                response.message = "Creación de OC Éxitosa";
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        public async Task<IActionResult> Edit(int id, string engineer)
        {
            //Obtener el host del navegador
            var host = $"{Request.Host}";
            ViewBag.Host = host;

            var response = new Response<UpdateOC>();
            try
            {
                UpdateOC oc = _mapper.Map<UpdateOC>(_ocService.GetOcByIdAsync(id));

                if(id == null || _ocService == null)
                {
                    return NotFound();
                }

                if(oc == null)
                {
                    return NotFound();
                }

                oc.UsuarioCreo = _formatter.UserCreator(engineer);

                response.status = true;
                response.value = oc;
                response.message = "Se ha recuperado la OC éxitosamente.";

            }
            catch(Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return View(response);
        }

        public async Task<IActionResult> Edit(UpdateOC oc, int id, List<IFormFile> files)
        {
            //Obtener el host del navegador
            var host = $"{Request.Host}";
            ViewBag.Host = host;

            var response = new Response<GetOC>();
            try
            {
                response.status = true;
                response.value = await _ocService.UpdateOcAsync(oc, id, files);
                response.message = "Actualización de OC Éxitosa";
            }
            catch(Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }
    }
}
