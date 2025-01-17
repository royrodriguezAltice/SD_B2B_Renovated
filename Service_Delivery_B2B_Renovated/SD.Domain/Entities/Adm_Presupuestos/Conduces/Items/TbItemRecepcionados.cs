using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Adm_Presupuestos.Conduces.Items
{
    public class TbItemRecepcionados
    {
        public int Id { get; set; }

        public string? NoRecepcion { get; set; }

        public int? OrdenCompra { get; set; }

        public string? Descripcion { get; set; }

        public decimal? Cantidad { get; set; }

    }
}
