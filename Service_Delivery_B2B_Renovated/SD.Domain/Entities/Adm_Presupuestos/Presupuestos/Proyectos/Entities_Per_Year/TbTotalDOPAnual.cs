using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Adm_Presupuestos.Presupuestos.Proyectos.Entities_Per_Year
{
	public partial class TbTotalDOPAnual
	{
		public int Id { get; set; }

		public decimal? Enero { get; set; }

		public decimal? Febrero { get; set; }

		public decimal? Marzo { get; set; }

		public decimal? Abril { get; set; }

		public decimal? Mayo { get; set; }

		public decimal? Junio { get; set; }

		public decimal? Julio { get; set; }

		public decimal? Agosto { get; set; }

		public decimal? Septiembre { get; set; }

		public decimal? Octubre { get; set; }

		public decimal? Noviembre { get; set; }

		public decimal? Diciembre { get; set; }

		public string? Anho { get; set; }

		public decimal? TotalDOP { get; set; }

		public string? Line_Budget { get; set; }

		public string? Budget_SA { get; set; }

		public string? Code { get; set; }
	}
}
