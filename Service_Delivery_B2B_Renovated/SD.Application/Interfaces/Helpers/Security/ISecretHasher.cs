using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Interfaces.Helpers.Security
{
	public interface ISecretHasher
	{
		string Hash(string input);
		bool Verify(string input, string hashString);
	}
}
