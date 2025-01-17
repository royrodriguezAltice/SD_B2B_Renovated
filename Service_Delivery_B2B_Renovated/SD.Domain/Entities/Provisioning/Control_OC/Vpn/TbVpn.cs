using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Provisioning.Control_OC.Vpn
{
	public partial class TbVpn
	{
		public int Id { get; set; }

		public string? Name { get; set; }

		public int? VrfGroup { get; set; }

		public string? Rd { get; set; }

		public string? Descrip { get; set; }
	}
}
