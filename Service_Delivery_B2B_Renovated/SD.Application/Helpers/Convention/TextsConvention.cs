using SD.Application.Interfaces.Convention;
using SD.Application.Provisioning.Control_OC.OC.DTOs;
using SD.Application.Provisioning.Control_OC.Template.DTOs;
using SD.Domain.Entities.Provisioning.Control_OC.Oc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Helpers.Convention
{
    public class TextsConvention : ITextsConvention
    {
        static TextsConvention() 
        {
        
        }

        public static TemplateDescDTO TemplateDescription(GetOC.GetOcDTO oc)
        {
            return new TemplateDescDTO
            {
                DescripOc = $"{oc.Oc} OS-{oc.Os} {oc.Actividad} {oc.Bw}-{oc.Bws} {oc.Producto} ({oc.GPON_P2P}) {oc.Cliente}",
                Description = $"description B2B: [{oc.TipoServicio}] : {oc.Circuito} : {oc.IpClienteWMask} / {oc.IpIspMask} : {oc.Oc} : {oc.VpnAg} : Ing : {oc.Cliente}",
            };
        } 
    }
}
