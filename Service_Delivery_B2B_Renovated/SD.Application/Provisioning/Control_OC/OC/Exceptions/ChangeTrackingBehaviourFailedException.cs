using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Provisioning.Control_OC.OC.Exceptions
{
    public class ChangeTrackingBehaviourFailedException : Exception
    {
        public override string Message { get; }

        public ChangeTrackingBehaviourFailedException() : base ()
        {
            Message = "No se pudo cambiar el comportamiento de rastreo correctamente.";
        }
    }
}
