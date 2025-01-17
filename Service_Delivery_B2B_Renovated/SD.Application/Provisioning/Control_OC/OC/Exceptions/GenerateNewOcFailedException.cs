using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Provisioning.Control_OC.OC.Exceptions
{
	public class GenerateNewOcFailedException : Exception
	{
		public override string Message { get; }
	
		public GenerateNewOcFailedException() : base() 
		{
			Message = "No se pudo generar el nuevo número de OC";
		}
	}
}
