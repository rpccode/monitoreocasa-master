using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoreoCasa.COMMON.Entidades
{
    public class Usuario : BaseDTO
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public bool EsAdministrador { get; set; }
    }
}
