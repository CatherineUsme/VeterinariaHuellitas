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
    [RoutePrefix("api/duenos")]
    public class DueñosController : ApiController
    {

        [HttpGet]
        [Route("ConsultarTodos")]

        public List<DUENO> ConsultarTodos()
        {
            clsDueno dueno = new clsDueno();
            return dueno.ConsultarTodos();
        }

        [HttpGet]
        [Route("ConsultarXCedula")]
        public DUENO ConsultarXCedula(string cedula)
        {
            clsDueno dueno = new clsDueno();
            return dueno.ConsultarXCedula(cedula);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] DUENO dueno)
        {
            clsDueno duen = new clsDueno();
            duen.dueno = dueno;
            return duen.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] DUENO dueno)
        {
            clsDueno duen = new clsDueno();
            duen.dueno = dueno;
            return duen.Actualizar();
        }

        [HttpPut]
        [Route("Inactivar")]
        public string Inactivar(string cedula)
        {
            clsDueno duen = new clsDueno();
            return duen.ModificarEstado(cedula, false);
        }

        [HttpPut]
        [Route("Activar")]
        public string Activar(string cedula)
        {
            clsDueno duen = new clsDueno();
            return duen.ModificarEstado(cedula, true);
        }

        [HttpDelete]
        [Route("EliminarXCedula")]
        public string EliminarXCedula(string cedula)
        {
            clsDueno duen = new clsDueno();
            return duen.EliminarXCedula(cedula);
        }
    }
}