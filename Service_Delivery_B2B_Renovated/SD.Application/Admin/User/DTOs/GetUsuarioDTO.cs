using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Admin.User.DTOs
{
    public class GetUsuarioDTO
    {
        //public record GetUsuarioDTO(string CodigoUsu,
        //							string? LoginUsu,
        //							string? ClaveUsu,
        //							string?NombreUsu);

            public string? CodigoUsu { get; set; }
            public string? LoginUsu { get; set; }
            public string? ClaveUsu { get; set; }
            public string? NombreUsu { get; set; }
    }
}
