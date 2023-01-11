using MonitoreoCasa.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoreoCasa.COMMON.Interfaces
{
    public interface IManejadorUsuario:IManejadorGenerico<Usuario>
    {
        Usuario Login(string nombreUsuario, string password);
    }
}
