using MongoDB.Bson;
using MonitoreoCasa.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoreoCasa.COMMON.Interfaces
{
    public interface IManejadorSensor:IManejadorGenerico<Sensor>
    {
        List<Sensor> SensoresDeUsuario(string idUsuario);

    }
}
