using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoreoCasa.COMMON.Entidades
{
    public abstract class BaseDTO
    {
        public string Id { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
