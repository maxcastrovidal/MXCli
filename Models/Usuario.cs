using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{

    public class Usuario
    {
        public int? IdEstado { get; set; } //Auxiliar
        public string? DsEstado { get; set; } //Auxiliar

        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public int? Edad { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecActualizacion { get; set; }

    }    

}
