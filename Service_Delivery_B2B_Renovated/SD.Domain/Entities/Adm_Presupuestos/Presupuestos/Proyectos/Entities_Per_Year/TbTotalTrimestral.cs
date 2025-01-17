using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Adm_Presupuestos.Presupuestos.Proyectos.Entities_Per_Year
{
	public partial class TbTotalTrimestral
	{
		public int Id { get; set; }

		public decimal? Q1 { get; set; }

		public decimal? Q2 { get; set; }

		public decimal? Q3 { get; set; }

		public decimal? Q4 { get; set; }

		public string? Anho { get; set; }

		public string? Line_Budget { get; set; }

		public string? Budget_SA { get; set; }

		public string? Code { get; set; }

		public string? Tipo_Total { get; set; }
	}
}
