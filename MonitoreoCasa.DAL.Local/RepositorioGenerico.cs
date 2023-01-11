using LiteDB;
using MongoDB.Bson;
using MonitoreoCasa.COMMON.Entidades;
using MonitoreoCasa.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MonitoreoCasa.DAL.Local
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : BaseDTO
    {
        private string DBName = @"c:\Monitoreo\Monitoreo.db";
        private string TableName;
        public RepositorioGenerico()
        {
            TableName = typeof(T).Name;
        }
        public List<T> Read
        {
            get
            {
                List<T> datos = new List<T>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<T>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public T Create(T entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            entidad.FechaHora = DateTime.Now;
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    db.GetCollection<T>(TableName).Insert(entidad);
                }
                return entidad;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public bool Delete(string id)
        {
            try
            {
                int r;
                using (var db = new LiteDatabase(DBName))
                {
                    r = db.GetCollection<T>(TableName).Delete(e => e.Id == id);
                }
                return r > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<T> Query(Expression<Func<T, bool>> query)
        {
            try
            {
                List<T> datos = new List<T>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<T>(TableName).Find(query).ToList();
                }
                return datos;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public T SearchById(string id)
        {
            try
            {
                T dato;
                using (var db = new LiteDatabase(DBName))
                {
                    dato = db.GetCollection<T>(TableName).Find(e => e.Id == id).SingleOrDefault();
                }
                return dato;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public T Update(T entidad)
        {
            bool r;
            try
            {
                entidad.FechaHora = DateTime.Now;
                using (var db = new LiteDatabase(DBName))
                {
                    r = db.GetCollection<T>(TableName).Update(entidad);
                }
                return r ? entidad : null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
