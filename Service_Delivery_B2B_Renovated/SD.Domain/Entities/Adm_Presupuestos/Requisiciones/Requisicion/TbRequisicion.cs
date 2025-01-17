using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Adm_Presupuestos.Requisiciones.Requisicion
{
    public partial class TbRequisicion
    {
        public int Id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? OrdenCompra { get; set; }

        public int? Requisicion { get; set; }

        public string? FechaReq { get; set; }

        public string? FechaOrdenC { get; set; }

        public string? Suplidor { get; set; }

        public string? Concepto { get; set; }

        public string? Recurrente { get; set; }

        public string? Descripcion { get; set; }

        public string? Cliente { get; set; }

        public string? NoCotizacion { get; set; }

        public string? Proyecto { get; set; }

        public string? Abrv { get; set; }

        public string? Organizacion { get; set; }

        public string? LineBudg { get; set; }

        public string? DescricionProyecto { get; set; }

        public string? LineBudg1 { get; set; }

        public string? TipoServicio { get; set; }

        public string? Estatus { get; set; }

        public string? Ing { get; set; }

        public string? TipoCompra { get; set; }

        public decimal? CostoTotalRd { get; set; }

        public decimal? CostoTotalMonedaC { get; set; }

        public string? TipoMoneda { get; set; }

        public decimal? TasaCambio { get; set; }

        public decimal? TotalDescontado { get; set; }

        public decimal? TotalDescontadoDOP { get; set; }

    }
}
