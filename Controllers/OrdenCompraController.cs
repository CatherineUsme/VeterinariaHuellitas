using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class OrdenCompraController : ApiController
    {
        private clsOrdenCompra _ordenCompra = new clsOrdenCompra();

        // Ver: Farmaceuta, Administrador, Veterinario, Recepcionista
        public IEnumerable<ORDEN_COMPRA> Get()
        {
            return _ordenCompra.ConsultarTodos();
        }

        // Ver: Farmaceuta, Administrador, Veterinario, Recepcionista
        public ORDEN_COMPRA Get(int id)
        {
            return _ordenCompra.ConsultarXId(id);
        }

        // Crear: Farmaceuta, Administrador
        //[AuthorizeRoles("Farmaceuta", "Administrador")]
        public string Post([FromBody] ORDEN_COMPRA ordenCompra)
        {
            _ordenCompra.ordenCompra = ordenCompra;
            return _ordenCompra.Insertar();
        }

        // Modificar: Administrador
        //[AuthorizeRoles("Administrador")]
        [HttpPut]
        [Route("api/OrdenCompra/{id}")]
        public string Put(int id, [FromBody] ORDEN_COMPRA ordenCompra)
        {
            ordenCompra.id_orden_compra = id;
            _ordenCompra.ordenCompra = ordenCompra;
            return _ordenCompra.Actualizar();
        }

        // Ver por proveedor: Farmaceuta, Administrador, Recepcionista
        [Route("api/OrdenCompra/Proveedor/{id}")]
        [HttpGet]
        //[AuthorizeRoles("Farmaceuta", "Administrador", "Recepcionista")]
        public IEnumerable<ORDEN_COMPRA> GetByProveedor(int id)
        {
            return _ordenCompra.ConsultarPorProveedor(id);
        }

        // Ver por sede: Farmaceuta, Administrador, Recepcionista
        [Route("api/OrdenCompra/Sede/{id}")]
        [HttpGet]
        //[AuthorizeRoles("Farmaceuta", "Administrador", "Recepcionista")]
        public IEnumerable<ORDEN_COMPRA> GetBySede(int id)
        {
            return _ordenCompra.ConsultarPorSede(id);
        }

        // Ver por estado: Farmaceuta, Administrador, Recepcionista
        [Route("api/OrdenCompra/Estado/{estado}")]
        [HttpGet]
        //[AuthorizeRoles("Farmaceuta", "Administrador", "Recepcionista")]
        public IEnumerable<ORDEN_COMPRA> GetByEstado(string estado)
        {
            return _ordenCompra.ConsultarPorEstado(estado);
        }

        // Aprobar: Administrador
        [Route("api/OrdenCompra/{id}/Aprobar")]
        [HttpPut]
        //[AuthorizeRoles("Administrador")]
        public string PutAprobar(int id)
        {
            return _ordenCompra.CambiarEstado(id, "Aprobada");
        }

        // Marcar como Recibida: Administrador
        [Route("api/OrdenCompra/{id}/Recibir")]
        [HttpPut]
        //[AuthorizeRoles("Administrador")]
        public string PutRecibir(int id)
        {
            return _ordenCompra.CambiarEstado(id, "Recibida");
        }

        // Cancelar: Administrador
        [Route("api/OrdenCompra/{id}/Cancelar")]
        [HttpPut]
        //[AuthorizeRoles("Administrador")]
        public string PutCancelar(int id)
        {
            return _ordenCompra.CambiarEstado(id, "Cancelada");
        }
    }
}