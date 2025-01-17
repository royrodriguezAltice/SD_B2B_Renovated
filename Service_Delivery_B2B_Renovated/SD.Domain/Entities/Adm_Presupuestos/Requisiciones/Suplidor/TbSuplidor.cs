using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Adm_Presupuestos.Requisiciones.Suplidor
{
    public partial class TbSuplidor
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Contacto { get; set; }

        public string? CorreoContacto { get; set; }

        public string? Telefono { get; set; }
    }
}
