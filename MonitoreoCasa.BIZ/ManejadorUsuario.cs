using MongoDB.Bson;
using MonitoreoCasa.COMMON.Entidades;
using MonitoreoCasa.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitoreoCasa.BIZ
{
    public class ManejadorUsuario : IManejadorUsuario
    {
        IRepositorioGenerico<Usuario> repositorio;
        public ManejadorUsuario(IRepositorioGenerico<Usuario> repositorio)
        {
            this.repositorio = repositorio;
        }

        /*
         * S responsabilidad unica
         * O Abierto/Cerrado
         * L Substitución de Liskov
         * I Segregación de intefaces
         * D Inversión de Dependencia (Inyección de dependencias)
         * 
         */
        public List<Usuario> ObtenerTodos => repositorio.Read;

        public Usuario Actualizar(Usuario entidad)
        {
            return repositorio.Update(entidad);
        }

        public Usuario Agregar(Usuario entidad)
        {
            return repositorio.Create(entidad);
        }

        public Usuario BuscarPorId(string id)
        {
            return repositorio.SearchById(id);
        }

        public bool Eliminar(string id)
        {
            return repositorio.Delete(id);
        }

        public Usuario Login(string nombreUsuario, string password)
        {
            return repositorio.Query(p => p.NombreUsuario == nombreUsuario && p.Password == password).SingleOrDefault();
        }
    }
}
