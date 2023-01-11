using MongoDB.Bson;
using MonitoreoCasa.COMMON.Entidades;
using MonitoreoCasa.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoreoCasa.BIZ
{
    public class ManejadorSensor : IManejadorSensor
    {
        IRepositorioGenerico<Sensor> repositorio;
        public ManejadorSensor(IRepositorioGenerico<Sensor> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<Sensor> ObtenerTodos => repositorio.Read;

        public Sensor Actualizar(Sensor entidad)=>repositorio.Update(entidad);
        

        public Sensor Agregar(Sensor entidad)
        {
            return repositorio.Create(entidad);
        }

        public Sensor BuscarPorId(string id)
        {
            return repositorio.SearchById(id);
        }

        public bool Eliminar(string id)
        {
            return repositorio.Delete(id);
        }

        public List<Sensor> SensoresDeUsuario(string idUsuario)
        {
            return repositorio.Query(p => p.IdUsuario == idUsuario);
        }
    }
}
