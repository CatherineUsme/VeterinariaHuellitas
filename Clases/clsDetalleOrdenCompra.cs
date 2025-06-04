using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsDetalleOrdenCompra
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public DETALLE_ORDEN_COMPRA detalleOrdenCompra { get; set; }

        public string Insertar()
        {
            try
            {
                if (detalleOrdenCompra == null)
                    return "No hay datos para insertar";

                // Validar orden de compra
                var orden = dbVeterinaria.ORDEN_COMPRA.Find(detalleOrdenCompra.id_orden_compra);
                if (orden == null)
                    return "La orden de compra no existe";

                if (orden.estado != "Pendiente" && orden.estado != "En Proceso")
                    return "No se pueden agregar detalles a una orden completada o cancelada";

                // Validar producto
                var producto = dbVeterinaria.PRODUCTOes.Find(detalleOrdenCompra.id_producto);
                if (producto == null)
                    return "El producto no existe";

                // Calcular subtotal
                detalleOrdenCompra.subtotal = detalleOrdenCompra.cantidad_solicitada * detalleOrdenCompra.precio_unitario_compra;

                dbVeterinaria.DETALLE_ORDEN_COMPRA.Add(detalleOrdenCompra);
                dbVeterinaria.SaveChanges();

                return "Detalle de orden de compra insertado correctamente";
            }
            catch (Exception ex)
            {
                return $"Error al insertar el detalle de orden de compra: {ex.Message}";
            }
        }

        public string Actualizar()
        {
            try
            {
                if (detalleOrdenCompra == null)
                    return "No hay datos para actualizar";

                var existente = dbVeterinaria.DETALLE_ORDEN_COMPRA.Find(detalleOrdenCompra.id_detalle_orden_compra);
                if (existente == null)
                    return "Detalle de orden de compra no encontrado";

                // Validar estado de la orden
                var orden = dbVeterinaria.ORDEN_COMPRA.Find(existente.id_orden_compra);
                if (orden.estado != "Pendiente" && orden.estado != "En Proceso")
                    return "No se pueden modificar detalles de una orden completada o cancelada";

                // Recalcular subtotal
                detalleOrdenCompra.subtotal = detalleOrdenCompra.cantidad_solicitada * detalleOrdenCompra.precio_unitario_compra;

                dbVeterinaria.DETALLE_ORDEN_COMPRA.AddOrUpdate(detalleOrdenCompra);
                dbVeterinaria.SaveChanges();

                return "Detalle de orden de compra actualizado correctamente";
            }
            catch (Exception ex)
            {
                return $"Error al actualizar el detalle de orden de compra: {ex.Message}";
            }
        }

        public DETALLE_ORDEN_COMPRA ConsultarXId(int id)
        {
            try
            {
                return dbVeterinaria.DETALLE_ORDEN_COMPRA
                    .Include("ORDEN_COMPRA")
                    .Include("PRODUCTO")
                    .FirstOrDefault(x => x.id_detalle_orden_compra == id);
            }
            catch
            {
                return null;
            }
        }

        public List<DETALLE_ORDEN_COMPRA> ConsultarPorOrdenCompra(int idOrdenCompra)
        {
            try
            {
                return dbVeterinaria.DETALLE_ORDEN_COMPRA
                    .Include("ORDEN_COMPRA")
                    .Include("PRODUCTO")
                    .Where(x => x.id_orden_compra == idOrdenCompra)
                    .ToList();
            }
            catch
            {
                return new List<DETALLE_ORDEN_COMPRA>();
            }
        }

        public string Eliminar(int id)
        {
            try
            {
                var detalle = dbVeterinaria.DETALLE_ORDEN_COMPRA.Find(id);
                if (detalle == null)
                    return "Detalle de orden de compra no encontrado";

                // Validar estado de la orden
                var orden = dbVeterinaria.ORDEN_COMPRA.Find(detalle.id_orden_compra);
                if (orden.estado != "Pendiente" && orden.estado != "En Proceso")
                    return "No se pueden eliminar detalles de una orden completada o cancelada";

                dbVeterinaria.DETALLE_ORDEN_COMPRA.Remove(detalle);
                dbVeterinaria.SaveChanges();

                return "Detalle de orden de compra eliminado correctamente";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar el detalle de orden de compra: {ex.Message}";
            }
        }
    }
}
