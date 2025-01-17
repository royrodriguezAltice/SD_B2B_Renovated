using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Adm_Presupuestos.Requisiciones.Item
{
    public partial class TbItem
    {
        public int Id { get; set; }

        public int? OrdenCompra { get; set; }

        public int? Requisicion { get; set; }

        public string? Descripcion { get; set; }

        public string? Categoria { get; set; }

        public string? NombreEquipo { get; set; }

        public string? Codigo { get; set; }

        public decimal? Cantidad { get; set; }

        public decimal? CostoPromedio { get; set; }

        public decimal? CostoTotalRd { get; set; }

        public decimal? CostoTotalMonedaC { get; set; }

        public string? TipoMoneda { get; set; }

        public decimal? TasaCambio { get; set; }

        public string? TipoCompra { get; set; }

        public decimal? Descuento { get; set; }

        public decimal? MontoConDescuento { get; set; }

        public decimal? MontoConDescuentoRd { get; set; }

        public decimal? TotalDescontado { get; set; }

        public decimal? TotalDescontadoDOP { get; set; }
    }
}
