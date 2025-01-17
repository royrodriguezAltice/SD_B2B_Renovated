using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Provisioning.Control_OC.OC.Exceptions
{
	public class OcNotFoundException : Exception
	{
		public override string Message { get; }

		public OcNotFoundException() : base()
		{
			Message = "No se pudo encontrar la OC";
		}
	}
}
