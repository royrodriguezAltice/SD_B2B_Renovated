using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Provisioning.Control_OC.OC.Exceptions
{
	public class GetOcsFailedException : Exception
	{
		public override string Message { get; }

		public GetOcsFailedException() : base()
		{
			Message = "No se pudieron obtener las Oc";
		}
	}
}
