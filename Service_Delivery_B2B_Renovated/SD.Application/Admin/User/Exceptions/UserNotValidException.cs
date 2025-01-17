using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Admin.User.Exceptions
{
	public class UserNotValidException : Exception
	{
		public override string Message { get; }
		public UserNotValidException() : base()
		{
			Message = "Las credenciales ingresadas no son validas";
		}
	}
}
