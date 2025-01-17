using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Adm_Presupuestos.Presupuestos.Proyectos.Categoria
{
    public partial class TbCategoriaProyecto
    {
        public int Id { get; set; }

        public string? CodProyecto { get; set; }

        public string? Categoria { get; set; }
    }
}
