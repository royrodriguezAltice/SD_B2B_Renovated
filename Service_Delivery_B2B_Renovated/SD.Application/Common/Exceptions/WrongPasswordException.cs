using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Common.Exceptions
{
	public class WrongPasswordException : Exception
	{
		public override string Message { get; }

		public WrongPasswordException() : base()
		{
			Message = "No se pudo verificar la contraseña";
		}
	}
}
