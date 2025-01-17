using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Provisioning.Control_OC.OC.Exceptions
{
	public class CreateOcFailedException : Exception
	{
		public override string Message { get; }

		public CreateOcFailedException() : base()
		{
			Message = "No se pudo crear la OC";
		}
	}
}
