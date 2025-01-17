using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Adm_Presupuestos.Presupuestos.Proyecto.Proyecto_Organizacion
{
    public partial class TbOrgProyecto
    {
        public int Id { get; set; }

        public string? CodProyecto { get; set; }

        public string? DescProyecto { get; set; }

        public string? CodigoOrg { get; set; }

        public string? CodigoAcc { get; set; }

        public string? CodigoCat { get; set; }

        public string? DescripcionAcc { get; set; }
    }
}
