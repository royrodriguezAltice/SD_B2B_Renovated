using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Provisioning.Control_OC.OC.Exceptions
{
	public class DeleteOcFailedException : Exception
	{
		public override string Message { get; }

		public DeleteOcFailedException () : base () 
		{
			Message = "No se pudo eliminar la OC";
		}
	}
}
