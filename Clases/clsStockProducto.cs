using System.Collections.Generic;
using System.Linq;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsStockProducto
    {
        private VeterinariaEntities db = new VeterinariaEntities();
        public STOCK_PRODUCTO_SEDE stock { get; set; }

        public List<STOCK_PRODUCTO_SEDE> ConsultarTodos()
        {
            return db.STOCK_PRODUCTO_SEDE.ToList();
        }

        public STOCK_PRODUCTO_SEDE ConsultarXId(int id)
        {
            return db.STOCK_PRODUCTO_SEDE.FirstOrDefault(s => s.id_stock_producto_sede == id);
        }

        public List<STOCK_PRODUCTO_SEDE> ConsultarPorProducto(int idProducto)
        {
            return db.STOCK_PRODUCTO_SEDE.Where(s => s.id_producto == idProducto).ToList();
        }

        public List<STOCK_PRODUCTO_SEDE> ConsultarPorSede(int idSede)
        {
            return db.STOCK_PRODUCTO_SEDE.Where(s => s.id_sede == idSede).ToList();
        }

        public string Insertar()
        {
            if (stock == null)
                return "Stock inválido.";
            if (stock.id_producto <= 0 || stock.id_sede <= 0)
                return "Producto y sede obligatorios.";
            db.STOCK_PRODUCTO_SEDE.Add(stock);
            db.SaveChanges();
            return "Stock insertado correctamente.";
        }

        public string Actualizar()
        {
            if (stock == null)
                return "Stock inválido.";
            var existente = db.STOCK_PRODUCTO_SEDE.FirstOrDefault(s => s.id_stock_producto_sede == stock.id_stock_producto_sede);
            if (existente == null)
                return "Stock no encontrado.";
            db.Entry(existente).CurrentValues.SetValues(stock);
            db.SaveChanges();
            return "Stock actualizado correctamente.";
        }

        public string EliminarXId(int id)
        {
            var s = db.STOCK_PRODUCTO_SEDE.FirstOrDefault(x => x.id_stock_producto_sede == id);
            if (s == null)
                return "Stock no encontrado.";
            db.STOCK_PRODUCTO_SEDE.Remove(s);
            db.SaveChanges();
            return "Stock eliminado correctamente.";
        }
    }
}
