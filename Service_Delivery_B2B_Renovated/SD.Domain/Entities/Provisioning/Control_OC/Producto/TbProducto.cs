using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Provisioning.Control_OC.Producto
{
	public partial class TbProducto
	{
		public int Id { get; set; }

		public string? Descripcion { get; set; }

		public string TipoProducto { get; set; } = null!;

		public int? Orden { get; set; }
	}
}
