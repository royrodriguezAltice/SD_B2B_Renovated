using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Common.Exceptions
{
	public class ValidateUserFailedException : Exception
	{
		public override string Message { get; }
	
		public ValidateUserFailedException() : base()
		{
			Message = "No se pudo validar las credenciales del usuario";
		}
	}
}
