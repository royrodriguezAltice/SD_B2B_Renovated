using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Adm_Presupuestos.Conduces.Recepcion
{
    public partial class TbRecepcionC
    {
        public int Id { get; set; }

        public string? FechaRecepcion { get; set; }

        public int? OrdenCompra { get; set; }

        public string? NoRecepcion { get; set; }

        public string? NoRecepcionOracle { get; set; }

        public string? Conduce { get; set; }

        public string? Ing { get; set; }

        public decimal? CantidadRecepcion { get; set; }

        public decimal? CantidadDisponible { get; set; }

        public decimal? CantidadOriginal { get; set; }

        public string? Suplidor { get; set; }

        public string? Descripcion { get; set; }

        public decimal? MontoOriginal { get; set; }

        public decimal? MontoDisponible { get; set; }

        public decimal? MontoRecepcionado { get; set; }

        public string? LineBudg { get; set; }
    }
}
