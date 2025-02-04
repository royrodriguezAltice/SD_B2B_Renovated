using SD.Application.Provisioning.Control_OC.Template.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Provisioning.Control_OC.OC.DTOs.GetOC;

namespace SD.Application.Interfaces.Convention
{
    public interface ITextsConvention
    {
        abstract static TemplateDescDTO TemplateDescription(GetOcDTO oc);
    }
}
