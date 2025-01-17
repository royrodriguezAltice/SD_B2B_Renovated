using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Adm_Presupuestos.Requisiciones.Categoria
{
    public partial class TbCategorium
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Detalle { get; set; }
    }
}
