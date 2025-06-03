using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class StockProductoController : ApiController
    {
        [HttpGet]
        public List<STOCK_PRODUCTO_SEDE> Get()
        {
            clsStockProducto servicio = new clsStockProducto();
            return servicio.ConsultarTodos();
        }

        [HttpGet]
        public STOCK_PRODUCTO_SEDE Get(int id)
        {
            clsStockProducto servicio = new clsStockProducto();
            return servicio.ConsultarXId(id);
        }

        [HttpGet]
        [Route("api/StockProducto/PorProducto/{idProducto}")]
        public List<STOCK_PRODUCTO_SEDE> PorProducto(int idProducto)
        {
            clsStockProducto servicio = new clsStockProducto();
            return servicio.ConsultarPorProducto(idProducto);
        }

        [HttpGet]
        [Route("api/StockProducto/PorSede/{idSede}")]
        public List<STOCK_PRODUCTO_SEDE> PorSede(int idSede)
        {
            clsStockProducto servicio = new clsStockProducto();
            return servicio.ConsultarPorSede(idSede);
        }

        [HttpPost]
        public string Post([FromBody] STOCK_PRODUCTO_SEDE stock)
        {
            clsStockProducto servicio = new clsStockProducto();
            servicio.stock = stock;
            return servicio.Insertar();
        }

        [HttpPut]
        public string Put([FromBody] STOCK_PRODUCTO_SEDE stock)
        {
            clsStockProducto servicio = new clsStockProducto();
            servicio.stock = stock;
            return servicio.Actualizar();
        }

        [HttpDelete]
        public string Delete(int id)
        {
            clsStockProducto servicio = new clsStockProducto();
            return servicio.EliminarXId(id);
        }
    }
}
