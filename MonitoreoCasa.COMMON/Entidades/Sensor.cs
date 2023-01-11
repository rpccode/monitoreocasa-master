using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoreoCasa.COMMON.Entidades
{
    public class Sensor:BaseDTO
    {
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string UnidadDeMedida { get; set; }
        public string Comando { get; set; }
        public bool EsEvento { get; set; }
        public string IdUsuario { get; set; }
        public override string ToString()
        {
            return String.Format("{0} [{1}]",Nombre,Comando);
        }
    }
}
