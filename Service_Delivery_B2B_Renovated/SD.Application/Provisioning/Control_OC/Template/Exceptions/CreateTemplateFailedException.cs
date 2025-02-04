using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Provisioning.Control_OC.Template.Exceptions
{
    public class CreateTemplateFailedException :  Exception
    {
        public override string Message {get;}

        public CreateTemplateFailedException(string message) : base(message)
        {
            Message = $"La creación del template ha fallado con la siguiente excepción: {message}";
        }
    }
}
