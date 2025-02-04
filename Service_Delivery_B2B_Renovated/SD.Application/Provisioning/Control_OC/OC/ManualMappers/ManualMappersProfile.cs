using SD.Domain.Entities.Provisioning.Control_OC.Apn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Provisioning.Control_OC.OC.DTOs.GetOC;

namespace SD.Application.Provisioning.Control_OC.OC.ManualMappers
{
    public static class ManualMappersProfile
    {
        public static GetOcDTO MapAPNToOcDTO(GetOcDTO oc, TbApn apnData)
        {
            return oc with { ApnData = apnData };
        }
    }
}
