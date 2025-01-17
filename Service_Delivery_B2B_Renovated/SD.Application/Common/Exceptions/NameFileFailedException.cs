using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Common.Exceptions
{
    internal class NameFileFailedException : Exception
    {
        public override string Message { get; }

        public NameFileFailedException(string message) : base(message)
        {
            Message = $"No se pudo nombrar el archivo correctamente, por el siguiente motivo : {message}";
        }
    }
}
