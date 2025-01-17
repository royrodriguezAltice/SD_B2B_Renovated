using SD.Domain.Entities.Administracion.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Administracion.Accesos.Acceso
{
    public class UserData
    {
        public TbUsuario Usuario { get; set; }

        public Modulo Modulo { get; set; }

    }

    public class Modulo
    {
        public string? PMO { get; set; }
        public string? PC { get; set; }
        public string? PROV { get; set; }
        public string? FCL { get; set; }
        public string? GINV { get; set; }
        public string? ADMPR { get; set; }
        public string? Reportes { get; set; }
    }
}
