using Microsoft.AspNetCore.Mvc;
using CoreApp.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreApp.Controllers
{
    public class UsuariosController : Controller
    {
        public async Task<IActionResult> Index(int? Id = null,
                                               string? Nombre = null,
                                               int? EdadDesde = null,
                                               int? EdadHasta = null,
                                               Boolean? Activo = null,
                                               string? FecCreacionDesdeHasta = null)
        {

            String vURL = "http://maxcastrovidal-001-site1.ctempurl.com/Usuarios/Info?p0=1";

            string ConsultaActiva = "{";

            if (Id != null) { 
                vURL += "&Id=" + Id.ToString();
                ConsultaActiva += "'Id':" + Id.ToString() + ",";
            }

            if (!String.IsNullOrEmpty(Nombre)) { 
                vURL += "&Nombre=" + Nombre;
                ConsultaActiva += "'Nombre':'" + Nombre + "',";
            }

            if (EdadDesde != null) {
                vURL += "&EdadDesde=" + EdadDesde.ToString();
                ConsultaActiva += "'EdadDesde':" + EdadDesde.ToString() + ",";
            }

            if (EdadHasta != null) { 
                vURL += "&EdadHasta=" + EdadHasta.ToString();
                ConsultaActiva += "'EdadHasta':" + EdadHasta.ToString() + ",";
            }

            if (Activo != null) { 
                vURL += "&Activo=" + Activo.ToString();
                ConsultaActiva += "'EdadHasta':" + EdadHasta.ToString() + ",";
            }

            if (!String.IsNullOrEmpty(FecCreacionDesdeHasta)) 
            {
                DateTime f1 = Convert.ToDateTime(FecCreacionDesdeHasta.Substring(0, 10), new System.Globalization.CultureInfo("es-CL"));
                DateTime f2 = Convert.ToDateTime(FecCreacionDesdeHasta.Substring(13, 10), new System.Globalization.CultureInfo("es-CL"));

                vURL += "&FecCreacionDesde=" + f1.ToString("yyyyMMdd");
                vURL += "&FecCreacionHasta=" + f2.ToString("yyyyMMdd");

                ConsultaActiva += "'FecCreacionDesdeHasta':'" + FecCreacionDesdeHasta + "',";
            }

            if (ConsultaActiva != "{")
            {
                ConsultaActiva = ConsultaActiva.Substring(0, ConsultaActiva.Length - 1);
            }

            ConsultaActiva += "}";

            HttpContext.Session.SetString("Usuarios.ConsultaActiva", ConsultaActiva);

            string? Ordenar = HttpContext.Session.GetString("Usuarios.Ordenar");
            if (!String.IsNullOrEmpty(Ordenar)) { vURL += "&Ordenar=" + Ordenar; }

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(vURL);
            //if (response.IsSuccessStatusCode) {
            //}

            string resp = await response.Content.ReadAsStringAsync();
            resp = resp.Replace("\\u0022", "\"");
            resp = resp.Substring(1, resp.Length - 2);

            List<Usuario>? usuarios = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Usuario>>(resp);

            return View(usuarios);

        }

        public IActionResult Ordenar(string Atributo)
        {
            string? Ordenar = HttpContext.Session.GetString("Usuarios.Ordenar");
            if (Atributo == Ordenar) { Atributo += " DESC"; }
            HttpContext.Session.SetString("Usuarios.Ordenar", Atributo);
            return RedirectToAction("Recargar");
        }

        public IActionResult Paginar(int Pagina)
        {
            HttpContext.Session.SetInt32("Usuarios.PagActual", Pagina);
            return RedirectToAction("Recargar");
        }

        public IActionResult Recargar(string Salida = "View")
        {
            JObject? ConsultaActiva = JObject.Parse(HttpContext.Session.GetString("Usuarios.ConsultaActiva"));

            return RedirectToAction("Index", new
            {
                Id = (int?)ConsultaActiva["Id"],
                Nombre = (string?)ConsultaActiva["Nombre"],
                EdadDesde = (int?)ConsultaActiva["EdadDesde"],
                EdadHasta = (int?)ConsultaActiva["EdadHasta"],
                Activo = (Boolean?)ConsultaActiva["Activo"],
                FecCreacionDesdeHasta = (string?)ConsultaActiva["FecCreacionDesdeHasta"],
                Salida = Salida
            });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null) { return NotFound(); }

            Usuario usuario = new Usuario();

            if (Id == 0) {usuario.Id = 0;}

            if (Id != 0)
            {
                decimal Id2 = (decimal)Id;
                Id2 = Math.Abs(Id2);
                String vURL = "http://maxcastrovidal-001-site1.ctempurl.com/Usuarios/Info?Id=" + Id2.ToString();

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(vURL);
                string resp = await response.Content.ReadAsStringAsync();
                resp = resp.Replace("\\u0022", "\"");
                resp = resp.Substring(1, resp.Length - 2);
                List<Usuario>? usuarios = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Usuario>>(resp);
                usuario = usuarios[0];

                if (Id <0) { usuario.Id = usuario.Id * -1; }
            }

            return View(usuario);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int Id, [Bind("Id,Nombre,Edad,Activo")] Usuario usuario)
        {
            if (Id != usuario.Id) { return NotFound(); }

            String vURL = "http://maxcastrovidal-001-site1.ctempurl.com/Usuarios/Gest";
            vURL += "?Id=" + Id.ToString();
            if (Id >= 0)
            {
                vURL += "&Nombre=" + usuario.Nombre;
                vURL += "&Edad=" + usuario.Edad.ToString();
                vURL += "&Activo=" + usuario.Activo.ToString();
            }

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(vURL);
            string resp = await response.Content.ReadAsStringAsync();
            resp = resp.Replace("\\u0022", "\"");
            resp = resp.Substring(1, resp.Length - 2);
            List<Usuario>? usuarios = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Usuario>>(resp);
            usuario = usuarios[0];

            if (usuario.IdEstado != 0)
            {
                HttpContext.Session.Set<Mensaje>("MensajeAct", new Mensaje(TipoMensaje.error, usuario.DsEstado));
                goto SalirErr;
            }

            return RedirectToAction("Recargar"); //Index

        SalirErr:            
            return View(usuario);

        }
    }
}
