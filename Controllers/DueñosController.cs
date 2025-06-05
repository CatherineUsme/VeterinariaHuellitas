using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/duenos")]
    public class DueñosController : ApiController
    {

        [HttpGet]
        [Route("ConsultarTodos")]
        [AuthorizeRoles("Administrador", "Veterinario", "Recepcionista", "Farmaceuta")]
        public List<DUENO> ConsultarTodos()
        {
            clsDueno dueno = new clsDueno();
            return dueno.ConsultarTodos();
        }

        // Ver: Administrador, Veterinario, Recepcionista, Farmaceuta
        [HttpGet]
        [Route("ConsultarXCedula")]
        [AuthorizeRoles("Administrador", "Veterinario", "Recepcionista", "Farmaceuta")]
        public DUENO ConsultarXCedula(string cedula)
        {
            clsDueno dueno = new clsDueno();
            return dueno.ConsultarXCedula(cedula);
        }

        // Insertar: Administrador, Recepcionista
        [HttpPost]
        [Route("Insertar")]
        [AuthorizeRoles("Administrador", "Recepcionista")]
        public string Insertar([FromBody] DUENO dueno)
        {
            clsDueno duen = new clsDueno();
            duen.dueno = dueno;
            return duen.Insertar();
        }

        // Editar: Administrador, Veterinario, Recepcionista
        [HttpPut]
        [Route("Actualizar")]
        [AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public string Actualizar([FromBody] DUENO dueno)
        {
            clsDueno duen = new clsDueno();
            duen.dueno = dueno;
            return duen.Actualizar();
        }

        // Inactivar: Administrador, Recepcionista
        [HttpPut]
        [Route("Inactivar")]
        [AuthorizeRoles("Administrador", "Recepcionista")]
        public string Inactivar(string cedula)
        {
            clsDueno duen = new clsDueno();
            return duen.ModificarEstado(cedula, false);
        }

        // Activar: Administrador, Recepcionista
        [HttpPut]
        [Route("Activar")]
        [AuthorizeRoles("Administrador", "Recepcionista")]
        public string Activar(string cedula)
        {
            clsDueno duen = new clsDueno();
            return duen.ModificarEstado(cedula, true);
        }

        // Eliminar: Administrador, Recepcionista
        [HttpDelete]
        [Route("EliminarXCedula")]
        [AuthorizeRoles("Administrador", "Recepcionista")]
        public string EliminarXCedula(string cedula)
        {
            clsDueno duen = new clsDueno();
            return duen.EliminarXCedula(cedula);
        }
    }
}