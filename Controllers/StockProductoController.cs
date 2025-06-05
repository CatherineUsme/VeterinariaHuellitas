using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class StockProductoController : ApiController
    {
        // Ver: Farmaceuta, Administrador, Veterinario, Recepcionista
        [HttpGet]
        [AuthorizeRoles("Farmaceuta", "Administrador", "Veterinario", "Recepcionista")]
        public List<STOCK_PRODUCTO_SEDE> Get()
        {
            clsStockProducto servicio = new clsStockProducto();
            return servicio.ConsultarTodos();
        }

        // Ver: Farmaceuta, Administrador, Veterinario, Recepcionista
        [HttpGet]
        [AuthorizeRoles("Farmaceuta", "Administrador", "Veterinario", "Recepcionista")]
        public STOCK_PRODUCTO_SEDE Get(int id)
        {
            clsStockProducto servicio = new clsStockProducto();
            return servicio.ConsultarXId(id);
        }

        // Ver: Farmaceuta, Administrador, Veterinario, Recepcionista
        [HttpGet]
        [Route("api/StockProducto/PorProducto/{idProducto}")]
        [AuthorizeRoles("Farmaceuta", "Administrador", "Veterinario", "Recepcionista")]
        public List<STOCK_PRODUCTO_SEDE> PorProducto(int idProducto)
        {
            clsStockProducto servicio = new clsStockProducto();
            return servicio.ConsultarPorProducto(idProducto);
        }

        // Ver: Farmaceuta, Administrador, Veterinario, Recepcionista
        [HttpGet]
        [Route("api/StockProducto/PorSede/{idSede}")]
        [AuthorizeRoles("Farmaceuta", "Administrador", "Veterinario", "Recepcionista")]
        public List<STOCK_PRODUCTO_SEDE> PorSede(int idSede)
        {
            clsStockProducto servicio = new clsStockProducto();
            return servicio.ConsultarPorSede(idSede);
        }

        // Gestionar: Farmaceuta, Administrador
        [HttpPost]
        [AuthorizeRoles("Farmaceuta", "Administrador")]
        public string Post([FromBody] STOCK_PRODUCTO_SEDE stock)
        {
            clsStockProducto servicio = new clsStockProducto();
            servicio.stock = stock;
            return servicio.Insertar();
        }

        // Gestionar: Farmaceuta, Administrador
        [HttpPut]
        [AuthorizeRoles("Farmaceuta", "Administrador")]
        public string Put([FromBody] STOCK_PRODUCTO_SEDE stock)
        {
            clsStockProducto servicio = new clsStockProducto();
            servicio.stock = stock;
            return servicio.Actualizar();
        }

        // Gestionar: Farmaceuta, Administrador
        [HttpDelete]
        [AuthorizeRoles("Farmaceuta", "Administrador")]
        public string Delete(int id)
        {
            clsStockProducto servicio = new clsStockProducto();
            return servicio.EliminarXId(id);
        }
    }
}
