using SD.Application.Interfaces.Convention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Helpers.Convention
{
    public class Formatter : IFormatter
    {
        public string UserCreator(string engineer)
        {
            // Verificamos si el campo UsuarioCreo no está vacío
            if (!string.IsNullOrWhiteSpace(engineer))
            {
                string[] words = engineer.Split(' ');
                engineer = "";
                foreach (string word in words)
                {
                    if (!string.IsNullOrWhiteSpace(word))
                    {
                        engineer += word[0];
                    }
                }
            }
            else
            {
                return "Desconocido";
            }
            return engineer;
        }
    }
}
