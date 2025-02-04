using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Provisioning.Control_OC.Template.Exceptions
{
    public class VerifyProfileExistenceFailedException : Exception
    {
        public override string Message { get; }

        public VerifyProfileExistenceFailedException() : base()
        {
            Message = "Ha fallado el verificar la existencia del profile.";
        }
    }
}
