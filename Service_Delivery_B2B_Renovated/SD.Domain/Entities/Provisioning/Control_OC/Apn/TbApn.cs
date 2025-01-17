using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Provisioning.Control_OC.Apn
{
    public partial class TbApn
    {
        public string? Id { get; set; }
        public string? SegundoEquipo { get; set; }
        public string? SegundaVlan { get; set; }
        public string? RouterTarget { get; set; }
        public string? IpRoutePolicy { get; set; }
        public string? SecondIpRoutePolicy { get; set; }
        public string? MaskRoutePolicy { get; set; }
        public string? NombreApn { get; set; }
    }
}
