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
    [RoutePrefix("api/Cama")]
    public class CamaController : ApiController
    {
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] CAMA cama)
        {
            clsCama cam = new clsCama();
            cam.cama = cama;
            return cam.Insertar();
        }

        [HttpGet]
        [Route("ConsultarTodos")]
        public List<CAMA> ConsultarTodos()
        {
            clsCama cam = new clsCama();
            return cam.ConsultarTodos();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] CAMA cama)
        {
            clsCama cam = new clsCama();
            cam.cama = cama;
            return cam.Actualizar();
        }

        [HttpGet]
        [Route("Consultar")]
        public CAMA Consultar(int id_cama)
        {
            clsCama cam = new clsCama();
            return cam.Consultar(id_cama);
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int id_cama)
        {
            clsCama cam = new clsCama();
            return cam.Eliminar(id_cama);
        }

    }
}