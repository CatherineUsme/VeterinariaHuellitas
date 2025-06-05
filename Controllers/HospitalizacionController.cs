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
    [RoutePrefix("api/Hospitalizacion")]
    public class HospitalizacionController : ApiController
    {
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] HOSPITALIZACION hospitalizacion)
        {
            clsHospitalizacion hospi = new clsHospitalizacion();
            hospi.hospitalizacion = hospitalizacion;
            return hospi.Insertar();
        }

        [HttpGet]
        [Route("ConsultarTodos")]
        public List<HOSPITALIZACION> ConsultarTodos()
        {
            clsHospitalizacion hospi = new clsHospitalizacion();
            return hospi.ConsultarTodos();
        }

        [HttpGet]
        [Route("Consultar")]
        public HOSPITALIZACION Consultar(int id_hospitalizacion)
        {
            clsHospitalizacion hospi = new clsHospitalizacion();
            return hospi.Consultar(id_hospitalizacion);
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] HOSPITALIZACION hospitalizacion)
        {
            clsHospitalizacion hospi = new clsHospitalizacion();
            hospi.hospitalizacion = hospitalizacion;
            return hospi.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int id_hospitalizacion)
        {
            clsHospitalizacion hospi = new clsHospitalizacion();
            return hospi.Eliminar(id_hospitalizacion);
        }
    }
}