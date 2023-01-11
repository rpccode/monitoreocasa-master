using MongoDB.Bson;
using MonitoreoCasa.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoreoCasa.COMMON.Interfaces
{
    public interface IManejadorGenerico<T> where T : BaseDTO
    {
        List<T> ObtenerTodos { get; }
        T Agregar(T entidad);
        T Actualizar(T entidad);
        bool Eliminar(string id);
        T BuscarPorId(string id);
    }
}
