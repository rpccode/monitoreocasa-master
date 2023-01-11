using MongoDB.Bson;
using MonitoreoCasa.COMMON.Entidades;
using MonitoreoCasa.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitoreoCasa.BIZ
{
    public class ManejadorMedicion:IManejadorMedicion
    {
        IRepositorioGenerico<Medicion> repositorio;
        public ManejadorMedicion(IRepositorioGenerico<Medicion> repositorio)
        {
            this.repositorio = repositorio;
        }

        public List<Medicion> ObtenerTodos => repositorio.Read;

        public Medicion Actualizar(Medicion entidad)
        {
            return repositorio.Update(entidad);
        }

        public Medicion Agregar(Medicion entidad)
        {
            return repositorio.Create(entidad);
        }

        public Medicion BuscarPorId(string id)
        {
            return repositorio.SearchById(id);
        }

        public bool Eliminar(string id)
        {
            return repositorio.Delete(id);
        }

        public List<Medicion> MedicionesDeSensor(string idSensor)
        {
            return repositorio.Query(s => s.IdSensor == idSensor);
        }

        public List<Medicion> MedicionesDeSensorEnIntevalo(string idSensor, DateTime inicio, DateTime fin)
        {
            return repositorio.Query(s => s.IdSensor == idSensor && s.FechaHora>=inicio && s.FechaHora<=fin);
        }

        public List<Medicion> MedicionesEnLugar(string lugar)
        {
            throw new NotImplementedException();
        }

        public List<Medicion> MedicionesinferioresValor(string idSensor, float valor)
        {
            return repositorio.Query(s => s.IdSensor == idSensor && s.Valor<valor);
        }

        public List<Medicion> MedicionesSuperioresValor(string idSensor, float valor)
        {
            return repositorio.Query(s => s.IdSensor == idSensor && s.Valor >= valor);
        }

        public List<Medicion> UltimasMediciones(string idSensor, int numero)
        {
            return repositorio.Query(s => s.IdSensor == idSensor).OrderByDescending(d => d.FechaHora).Take(numero).ToList();
        }
    }
}
