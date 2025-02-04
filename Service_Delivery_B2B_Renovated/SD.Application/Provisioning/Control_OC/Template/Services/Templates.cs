using SD.Application.Interfaces.Services.Provisioning.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Provisioning.Control_OC.OC.DTOs.GetOC;

namespace SD.Application.Provisioning.Control_OC.Template.Services
{
    public class Templates : ITemplates
    {
        public GetOcDTO _tbOc;

        private IServiceTemplate _serviceTemplate;
        private IVPNTemplate _vpnTemplate;
        private ILoopbackTemplate _loopbackTemplate;
        private ISubTemplates _subTemplates;


        public Templates(GetOcDTO tbOc, IServiceTemplate serviceTemplate, IVPNTemplate vpnTemplate, ILoopbackTemplate loopbackTemplate, ISubTemplates subTemplates)
        {
            _tbOc = tbOc;
            _serviceTemplate = serviceTemplate;
            _vpnTemplate = vpnTemplate;
            _loopbackTemplate = loopbackTemplate;
            _subTemplates = subTemplates;
        }

        public string GenerateTemplate()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(_serviceTemplate.AddLineHeader());
            sb.AppendLine(_serviceTemplate.AddJump());

            if (_tbOc.CreateVPN == "1")
            {
                sb.AppendLine(_vpnTemplate.CreateTemplate(null));
            };
            if (_tbOc.CreateLoopBack == "1")
            {
                sb.AppendLine(_loopbackTemplate.CreateTemplate(null));
            };
            if (_tbOc.Producto == "APN")
            {
                /*Se confecciona el template del primer Equipo*/
                //Se crea la vpn con el nombre del primer equipo
                sb.AppendLine(_vpnTemplate.CreateTemplate(null));
                //Se crea la interface en el primer equipo con la primera vlan ingresada
                sb.AppendLine(_subTemplates.CreateInterface("Primero", "Primera"));
                //Se crea la interface en el primer equipo con la segunda vlan ingresada
                sb.AppendLine(_subTemplates.CreateInterface("Primero", "Segunda"));
                //Se crea el template para la loopback en el primer equipo
                sb.AppendLine(_loopbackTemplate.CreateTemplate("Primero"));
                //Se configura la politica de enrutamiento para el primer equipo
                sb.AppendLine(_subTemplates.ConfigureRoutePolicy("Primero"));
                //Se connfigura el RouterOspf
                sb.AppendLine(_subTemplates.ConfigureRouterOspf());
                //Se configura el RouterBgp
                sb.AppendLine(_subTemplates.ConfigureRouterBgp(true, true));
                //Se crea el template del servicio de APN
                sb.AppendLine(_serviceTemplate.CreateTemplate());

                /*Se confecciona el template del segundo Equipo*/
                //Se crea la vpn con el nombre del segundo equipo
                sb.AppendLine(_vpnTemplate.CreateTemplate("Segundo"));
                //Se crea la interface en el segundo equipo con la primera vlan ingresada
                sb.AppendLine(_subTemplates.CreateInterface("Segundo", "Primera"));
                //Se crea la interface en el segundo equipo con la segunda vlan ingresada
                sb.AppendLine(_subTemplates.CreateInterface("Segundo", "Segunda"));
                //Se crea el template para la loopback en el segundo equipo
                sb.AppendLine(_loopbackTemplate.CreateTemplate("Segundo"));
                //Se configura la politica de enrutamiento para el segundo equipo
                sb.AppendLine(_subTemplates.ConfigureRoutePolicy("Segundo"));
                //Se connfigura el RouterOspf
                sb.AppendLine(_subTemplates.ConfigureRouterOspf());
                //Se configura el RouterBgp
                sb.AppendLine(_subTemplates.ConfigureRouterBgp(false, false));
            }
            else
            {
                sb.AppendLine(_serviceTemplate.CreateTemplate());
            }
            return sb.ToString();
        }
    }
}
