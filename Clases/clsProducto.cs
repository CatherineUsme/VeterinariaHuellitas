using System.Collections.Generic;
using System.Linq;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsProducto
    {
        private VeterinariaEntities db = new VeterinariaEntities();
        public PRODUCTO producto { get; set; }

        public List<PRODUCTO> ConsultarTodos()
        {
            return db.PRODUCTOes.ToList();
        }

        public PRODUCTO ConsultarXId(int id)
        {
            return db.PRODUCTOes.FirstOrDefault(p => p.id_producto == id);
        }

        public string Insertar()
        {
            if (producto == null)
                return "Producto inválido.";
            if (string.IsNullOrWhiteSpace(producto.nombre))
                return "El nombre es obligatorio.";
            if (db.PRODUCTOes.Any(p => p.nombre == producto.nombre))
                return "Ya existe un producto con ese nombre.";
            db.PRODUCTOes.Add(producto);
            db.SaveChanges();
            return "Producto insertado correctamente.";
        }

        public string Actualizar()
        {
            if (producto == null)
                return "Producto inválido.";
            var existente = db.PRODUCTOes.FirstOrDefault(p => p.id_producto == producto.id_producto);
            if (existente == null)
                return "Producto no encontrado.";
            if (string.IsNullOrWhiteSpace(producto.nombre))
                return "El nombre es obligatorio.";
            if (db.PRODUCTOes.Any(p => p.nombre == producto.nombre && p.id_producto != producto.id_producto))
                return "Ya existe un producto con ese nombre.";
            db.Entry(existente).CurrentValues.SetValues(producto);
            db.SaveChanges();
            return "Producto actualizado correctamente.";
        }

        public string EliminarXId(int id)
        {
            var prod = db.PRODUCTOes.FirstOrDefault(p => p.id_producto == id);
            if (prod == null)
                return "Producto no encontrado.";
            db.PRODUCTOes.Remove(prod);
            db.SaveChanges();
            return "Producto eliminado correctamente.";
        }
    }
}
