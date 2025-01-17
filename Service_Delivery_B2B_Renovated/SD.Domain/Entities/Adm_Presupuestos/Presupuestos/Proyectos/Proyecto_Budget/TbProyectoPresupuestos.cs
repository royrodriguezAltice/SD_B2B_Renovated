using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Adm_Presupuestos.Presupuestos.Proyectos.Proyecto_Budget
{
    public partial class TbProyectoPresupuestos
    {
        public int Id { get; set; }

        public string? CodProyecto { get; set; }

        public string? Item { get; set; }

        public string? DescBudget { get; set; }

        public string? Director { get; set; }

        public string? STREAM { get; set; }

        public string? Proyecto { get; set; }

        public string? StreamC { get; set; }
    }
}
