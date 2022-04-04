using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp
{
    public enum TipoMensaje
    {
        success,
        info,
        warning,
        error
    }

    public class Mensaje
    {
        public TipoMensaje Tipo { get; set; }
        public string Texto { get; set; }

        //constructor
        public Mensaje(TipoMensaje Tipo, string Texto)
        {
            this.Tipo = Tipo;
            this.Texto = Texto;
        }

        public string ImprimirLlamada()
        {
            String msg = this.Texto;
            msg = msg.Replace("'", @"\'"); // Eventuales apostrofos para cumplir formato de cadena javascript
            msg = msg.Replace(System.Environment.NewLine, "<br>");
            return "<script>toastr['" + this.Tipo + "']('" + msg + "');</script>";
        }
    }

}
