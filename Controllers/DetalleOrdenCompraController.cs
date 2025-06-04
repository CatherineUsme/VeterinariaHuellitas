using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class DetalleOrdenCompraController : ApiController
    {
        private clsDetalleOrdenCompra _detalleOrdenCompra = new clsDetalleOrdenCompra();

        // GET: api/DetalleOrdenCompra/5
        // Ver: Farmaceuta, Administrador, Veterinario, Recepcionista
        [AuthorizeRoles("Farmaceuta", "Administrador", "Veterinario", "Recepcionista")]
        public DETALLE_ORDEN_COMPRA Get(int id)
        {
            return _detalleOrdenCompra.ConsultarXId(id);
        }

        // GET: api/DetalleOrdenCompra/OrdenCompra/5
        // Ver: Farmaceuta, Administrador, Veterinario, Recepcionista
        [Route("api/DetalleOrdenCompra/OrdenCompra/{id}")]
        [HttpGet]
        [AuthorizeRoles("Farmaceuta", "Administrador", "Veterinario", "Recepcionista")]
        public IEnumerable<DETALLE_ORDEN_COMPRA> GetByOrdenCompra(int id)
        {
            return _detalleOrdenCompra.ConsultarPorOrdenCompra(id);
        }

        // POST: api/DetalleOrdenCompra
        // Crear: Farmaceuta, Administrador
        [AuthorizeRoles("Farmaceuta", "Administrador")]
        public string Post([FromBody]DETALLE_ORDEN_COMPRA detalleOrdenCompra)
        {
            _detalleOrdenCompra.detalleOrdenCompra = detalleOrdenCompra;
            return _detalleOrdenCompra.Insertar();
        }

        // PUT: api/DetalleOrdenCompra/5
        // Modificar: Administrador
        [AuthorizeRoles("Administrador")]
        public string Put(int id, [FromBody]DETALLE_ORDEN_COMPRA detalleOrdenCompra)
        {
            detalleOrdenCompra.id_detalle_orden_compra = id;
            _detalleOrdenCompra.detalleOrdenCompra = detalleOrdenCompra;
            return _detalleOrdenCompra.Actualizar();
        }

        // DELETE: api/DetalleOrdenCompra/5
        // Eliminar: Administrador
        [AuthorizeRoles("Administrador")]
        public string Delete(int id)
        {
            return _detalleOrdenCompra.Eliminar(id);
        }
    }
}
