using MongoDB.Bson;
using MonitoreoCasa.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;


namespace MonitoreoCasa.COMMON.Interfaces
{
    public interface IRepositorioGenerico<T> where T:BaseDTO
    {
        List<T> Read { get; }
        T SearchById(string id);
        T Create(T entidad);
        T Update(T entidad);
        bool Delete(string id);
        List<T> Query(Expression<Func<T, bool>> query);
    }
}
