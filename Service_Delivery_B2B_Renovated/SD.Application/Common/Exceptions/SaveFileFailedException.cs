using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Common.Exceptions
{
    public class SaveFileFailedException : Exception
    {
        public override string Message { get; }

        public SaveFileFailedException(string message) : base(message) 
        {
            Message = $"No se pudo guardar el archivo correctamente, por el siguiente motivo : {message}";
        }
    }
}
