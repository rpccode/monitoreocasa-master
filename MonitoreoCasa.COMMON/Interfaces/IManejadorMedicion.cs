using MongoDB.Bson;
using MonitoreoCasa.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoreoCasa.COMMON.Interfaces
{
    public interface IManejadorMedicion:IManejadorGenerico<Medicion>
    {
        List<Medicion> MedicionesDeSensor(string idSensor);
        List<Medicion> MedicionesEnLugar(string lugar);
        List<Medicion> MedicionesSuperioresValor(string idSensor, float valor);
        List<Medicion> MedicionesinferioresValor(string idSensor, float valor);
        List<Medicion> MedicionesDeSensorEnIntevalo(string idSensor, DateTime inicio, DateTime fin);
        List<Medicion> UltimasMediciones(string idSensor, int numero);
    }
}
