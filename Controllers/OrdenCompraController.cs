using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class OrdenCompraController : ApiController
    {
        private clsOrdenCompra _ordenCompra = new clsOrdenCompra();

        public IEnumerable<ORDEN_COMPRA> Get()
        {
            return _ordenCompra.ConsultarTodos();
        }

        public ORDEN_COMPRA Get(int id)
        {
            return _ordenCompra.ConsultarXId(id);
        }

        public string Post([FromBody]ORDEN_COMPRA ordenCompra)
        {
            _ordenCompra.ordenCompra = ordenCompra;
            return _ordenCompra.Insertar();
        }

        public string Put(int id, [FromBody]ORDEN_COMPRA ordenCompra)
        {
            ordenCompra.id_orden_compra = id;
            _ordenCompra.ordenCompra = ordenCompra;
            return _ordenCompra.Actualizar();
        }

        [Route("api/OrdenCompra/Proveedor/{id}")]
        [HttpGet]
        public IEnumerable<ORDEN_COMPRA> GetByProveedor(int id)
        {
            return _ordenCompra.ConsultarPorProveedor(id);
        }

        [Route("api/OrdenCompra/Sede/{id}")]
        [HttpGet]
        public IEnumerable<ORDEN_COMPRA> GetBySede(int id)
        {
            return _ordenCompra.ConsultarPorSede(id);
        }

        [Route("api/OrdenCompra/Estado/{estado}")]
        [HttpGet]
        public IEnumerable<ORDEN_COMPRA> GetByEstado(string estado)
        {
            return _ordenCompra.ConsultarPorEstado(estado);
        }

        [Route("api/OrdenCompra/{id}/Aprobar")]
        [HttpPut]
        public string PutAprobar(int id)
        {
            return _ordenCompra.CambiarEstado(id, "Aprobada");
        }

        [Route("api/OrdenCompra/{id}/Recibir")]
        [HttpPut]
        public string PutRecibir(int id)
        {
            return _ordenCompra.CambiarEstado(id, "Recibida");
        }


        [Route("api/OrdenCompra/{id}/Cancelar")]
        [HttpPut]
        public string PutCancelar(int id)
        {
            return _ordenCompra.CambiarEstado(id, "Cancelada");
        }
    }
}
