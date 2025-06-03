using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [RoutePrefix("api/servicio-adicional")]
    public class ServicioAdicionalPrestadoController : ApiController
    {
        private clsServicioAdicionalPrestado clsServicioAdicionalPrestado = new clsServicioAdicionalPrestado();

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                List<SERVICIO_ADICIONAL_PRESTADO> servicios = clsServicioAdicionalPrestado.ConsultarTodos();
                if (servicios == null || servicios.Count == 0)
                {
                    return NotFound();
                }
                return Ok(servicios);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                SERVICIO_ADICIONAL_PRESTADO servicio = clsServicioAdicionalPrestado.ConsultarXId(id);
                if (servicio == null)
                {
                    return NotFound();
                }
                return Ok(servicio);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] SERVICIO_ADICIONAL_PRESTADO servicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                clsServicioAdicionalPrestado.servicioAdicionalPrestado = servicio;
                string resultado = clsServicioAdicionalPrestado.Insertar();
                if (resultado.StartsWith("Error"))
                {
                    return BadRequest(resultado);
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id:int}/completar")]
        public IHttpActionResult Completar(int id)
        {
            try
            {
                string resultado = clsServicioAdicionalPrestado.CompletarServicio(id);
                if (resultado.StartsWith("Error") || resultado.StartsWith("No se puede"))
                {
                    return BadRequest(resultado);
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id:int}/cancelar")]
        public IHttpActionResult Cancelar(int id)
        {
            try
            {
                string resultado = clsServicioAdicionalPrestado.CancelarServicio(id);
                if (resultado.StartsWith("Error") || resultado.StartsWith("No se puede"))
                {
                    return BadRequest(resultado);
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
