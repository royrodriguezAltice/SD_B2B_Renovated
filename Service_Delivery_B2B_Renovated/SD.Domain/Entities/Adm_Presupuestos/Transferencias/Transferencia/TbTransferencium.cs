using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Adm_Presupuestos.Transferencias.Transferencia
{
    public partial class TbTransferencium
    {
        public int Id { get; set; }

        public string? Detalle { get; set; }

        public string? FechaTransfer { get; set; }

        public string? ProyectoOrigen { get; set; }

        public decimal? MD_ProyectoOrigen { get; set; } //Monto Disponible Proyecto Origen

        public string? ProyectoDestino { get; set; }

        public decimal? MR_ProyectoDestino { get; set; } //Monto Requerido Proyecto Destino

        public string? Codigo { get; set; }

        public string? Ing { get; set; }
    }
}
