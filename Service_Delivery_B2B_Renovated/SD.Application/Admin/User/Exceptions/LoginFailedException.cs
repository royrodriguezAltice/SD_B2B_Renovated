using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Admin.User.Exceptions
{
	public class LoginFailedException : Exception
	{
		public override string Message { get; }
		public LoginFailedException() : base() 
		{
			Message = "El proceso de login ha fallado.";
		}
	}
}
