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
    [RoutePrefix("api/mascotas")]
    public class MascotasController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]

        public List<MASCOTA> ConsultarTodos()
        {
            clsMascota mascota = new clsMascota();
            return mascota.ConsultarTodos();
        }

        [HttpGet]
        [Route("ConsultarXDueno")]

        public List<MASCOTA> ConsultarXdueno(int id_dueno)
        {
            clsMascota mascota = new clsMascota();
            return mascota.ConsultarXDueno(id_dueno);
        }

        [HttpGet]
        [Route("ConsultarXId")]
        public MASCOTA ConsultarXId(int id_mascota)
        {
            clsMascota mascota = new clsMascota();
            return mascota.ConsultarXId(id_mascota);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] MASCOTA mascota, string cedula)
        {
            clsMascota masc = new clsMascota();
            masc.mascota =mascota;
            return masc.Insertar(cedula);
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] MASCOTA mascota)
        {
            clsMascota masc = new clsMascota();
            masc.mascota = mascota;
            return masc.Actualizar();
        }

        [HttpPut]
        [Route("Inactivar")]
        public string Inactivar(int id_mascota)
        {
            clsMascota masc = new clsMascota();
            return masc.ModificarEstado(id_mascota, false);
        }

        [HttpPut]
        [Route("Activar")]
        public string Activar(int id_mascota)
        {
            clsMascota masc = new clsMascota();
            return masc.ModificarEstado(id_mascota, true);
        }

        [HttpDelete]
        [Route("EliminarXId")]
        public string EliminarXId(int id_mascota)
        {
            clsMascota masc = new clsMascota();
            return masc.EliminarXId(id_mascota);
        }
    }
}