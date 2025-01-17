using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Adm_Presupuestos.Presupuestos.Proyectos.Proyecto
{
    public partial class TbProyecto
    {

        public int Id { get; set; }

        public string? Father { get; set; }

        public string? Stream { get; set; }

        public string? Abrv { get; set; }

        public string? Code { get; set; }

        public string? NombreProyecto { get; set; }

        public string? BudgetDescription { get; set; }

        public string? Organizacion { get; set; }

        public string? DescripOrganizacion { get; set; }

        public decimal? BudgetSometido { get; set; }

        public decimal? BudgetAprobado { get; set; }

        public decimal? BudgetRestante { get; set; }

        public decimal? BudgetTransferido { get; set; }

        public string? Detalle { get; set; }

        public string? Director { get; set; }

        public string? AccountNumber { get; set; }

        public string? FaEssbase { get; set; }

        public string? OriginalCurrency { get; set; }

        public string? Type { get; set; }

        public string? LineBudg { get; set; }

        public string? Concepto { get; set; }

        public string? Year { get; set; }

        public string? Suplidor { get; set; }

        public decimal? TasaCambio { get; set; }

        public string? Concatenate { get; set; }

        public decimal? BudgetRecibido { get; set; }

        public decimal? BudgetConsumido { get; set; }

        public decimal? BudgetComprometido { get; set; }

        public string? Company { get; set; }

        public string? DetalleRecibido { get; set; }

        public int? CantidadRecibida { get; set; }

        public int? CantidadTransferida { get; set; }

        public int? Total_POs { get; set; }
    }
}
