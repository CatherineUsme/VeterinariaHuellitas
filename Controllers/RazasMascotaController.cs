using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [RoutePrefix("api/razasmascota")]
    public class RazasMascotaController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]

        public List<RAZA_MASCOTA> ConsultarTodos()
        {
            clsRazaMascota razaMascota = new clsRazaMascota();
            return razaMascota.ConsultarTodos();
        }

        [HttpGet]
        [Route("ConsultarXEspecie")]

        public List<RAZA_MASCOTA> ConsultarXEspecie(int id_especie)
        {
            clsRazaMascota razaMascota = new clsRazaMascota();
            return razaMascota.ConsultarXEspecie(id_especie);
        }

        [HttpGet]
        [Route("ConsultarXId")]
        public RAZA_MASCOTA ConsultarXId(int id_raza)
        {
            clsRazaMascota razaMascota = new clsRazaMascota();
            return razaMascota.ConsultarXId(id_raza);
        }

        [HttpGet]
        [Route("ConsultarXNombre")]
        public RAZA_MASCOTA ConsultarXNombre(string nombre_raza)
        {
            clsRazaMascota razaMascota = new clsRazaMascota();
            return razaMascota.ConsultarXNombre(nombre_raza);
        }

        [HttpGet]
        [Route("ListarRazasXEspecie")]
        public IQueryable ListarRazasXEspecie()
        {
            clsRazaMascota razaMascota = new clsRazaMascota();
            return razaMascota.ListarRazasXEspecie();
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] RAZA_MASCOTA razaMascota)
        {
            clsRazaMascota raza = new clsRazaMascota();
            raza.razaMascota = razaMascota;
            return raza.Insertar();
        }


        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] RAZA_MASCOTA razaMascota)
        {
            clsRazaMascota raza = new clsRazaMascota();
            raza.razaMascota = razaMascota;
            return raza.Actualizar();
        }

        [HttpDelete]
        [Route("EliminarXNombre")]
        public string EliminarXNombre(string nombre_raza)
        {
            clsRazaMascota raza = new clsRazaMascota();
            return raza.EliminarXNombre(nombre_raza);
        }

    }
}