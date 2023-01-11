using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoreoCasa.COMMON.Entidades
{
    public class Medicion:BaseDTO
    {
        public float Valor { get; set; }
        public string Lugar { get; set; }
        public string IdSensor { get; set; }
    }
}
