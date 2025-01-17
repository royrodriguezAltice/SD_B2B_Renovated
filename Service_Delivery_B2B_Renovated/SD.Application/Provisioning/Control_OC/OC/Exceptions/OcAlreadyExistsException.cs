using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Provisioning.Control_OC.OC.Exceptions
{
	public class OcAlreadyExistsException : Exception
	{
		public override string Message { get; }

		public OcAlreadyExistsException() : base()
		{
			Message = "Una OC con este número identificador ya existe.";
		}
	}
}
