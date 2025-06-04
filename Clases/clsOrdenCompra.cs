using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsOrdenCompra
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public ORDEN_COMPRA ordenCompra { get; set; }

        public string Insertar()
        {
            try
            {
                if (ordenCompra == null)
                    return "No hay datos para insertar";

                ordenCompra.fecha_orden = DateTime.Now;
                ordenCompra.estado = "Pendiente";

                dbVeterinaria.ORDEN_COMPRA.Add(ordenCompra);
                dbVeterinaria.SaveChanges();

                return "Orden de compra insertada correctamente";
            }
            catch (Exception ex)
            {
                return $"Error al insertar la orden de compra: {ex.Message}";
            }
        }

        public string Actualizar()
        {
            try
            {
                if (ordenCompra == null)
                    return "No hay datos para actualizar";

                var existente = dbVeterinaria.ORDEN_COMPRA
                    .Include("DETALLE_ORDEN_COMPRA")
                    .FirstOrDefault(x => x.id_orden_compra == ordenCompra.id_orden_compra);

                if (existente == null)
                    return "Orden de compra no encontrada";

                if (existente.estado == "Recibida" || existente.estado == "Cancelada")
                    return "No se puede modificar una orden que está en estado final";

                existente.observaciones = ordenCompra.observaciones;
                dbVeterinaria.SaveChanges();

                return "Orden de compra actualizada correctamente";
            }
            catch (Exception ex)
            {
                return $"Error al actualizar la orden de compra: {ex.Message}";
            }
        }

        public string CambiarEstado(int id, string nuevoEstado)
        {
            using (var dbContextTransaction = dbVeterinaria.Database.BeginTransaction())
            {
                try
                {
                    var orden = dbVeterinaria.ORDEN_COMPRA
                        .Include("DETALLE_ORDEN_COMPRA")
                        .FirstOrDefault(o => o.id_orden_compra == id);

                    if (orden == null)
                        return "Orden de compra no encontrada";

                    // Validar transiciones de estado válidas
                    switch (orden.estado)
                    {
                        case "Pendiente":
                            if (nuevoEstado != "Aprobada" && nuevoEstado != "Cancelada")
                                return "Desde Pendiente solo se puede pasar a Aprobada o Cancelada";
                            break;
                        case "Aprobada":
                            if (nuevoEstado != "Recibida" && nuevoEstado != "Cancelada")
                                return "Desde Aprobada solo se puede pasar a Recibida o Cancelada";
                            break;
                        case "Recibida":
                        case "Cancelada":
                            return "No se puede cambiar el estado de una orden que está en estado final";
                    }

                    orden.estado = nuevoEstado;

                    // Si la orden pasa a estado Recibida, actualizar el stock
                    if (nuevoEstado == "Recibida")
                    {
                        foreach (var detalle in orden.DETALLE_ORDEN_COMPRA)
                        {
                            var stockProducto = dbVeterinaria.STOCK_PRODUCTO_SEDE
                                .FirstOrDefault(s => s.id_sede == orden.id_sede_destino 
                                                && s.id_producto == detalle.id_producto);

                            if (stockProducto == null)
                            {
                                stockProducto = new STOCK_PRODUCTO_SEDE
                                {
                                    id_sede = orden.id_sede_destino,
                                    id_producto = detalle.id_producto,
                                    cantidad_disponible = detalle.cantidad_solicitada,
                                    stock_minimo = 5,
                                    ultima_actualizacion = DateTime.Now
                                };
                                dbVeterinaria.STOCK_PRODUCTO_SEDE.Add(stockProducto);
                            }
                            else
                            {
                                stockProducto.cantidad_disponible += detalle.cantidad_solicitada;
                                stockProducto.ultima_actualizacion = DateTime.Now;
                            }
                        }
                    }

                    dbVeterinaria.SaveChanges();
                    dbContextTransaction.Commit();

                    return $"Estado actualizado correctamente a {nuevoEstado}";
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return $"Error al cambiar el estado: {ex.Message}";
                }
            }
        }

        public ORDEN_COMPRA ConsultarXId(int id)
        {
            try
            {
                return dbVeterinaria.ORDEN_COMPRA
                    .Include("DETALLE_ORDEN_COMPRA")
                    .Include("PROVEEDOR")
                    .Include("EMPLEADO")
                    .Include("SEDE")
                    .FirstOrDefault(x => x.id_orden_compra == id);
            }
            catch
            {
                return null;
            }
        }

        public List<ORDEN_COMPRA> ConsultarTodos()
        {
            try
            {
                return dbVeterinaria.ORDEN_COMPRA
                    .Include("DETALLE_ORDEN_COMPRA")
                    .Include("PROVEEDOR")
                    .Include("EMPLEADO")
                    .Include("SEDE")
                    .ToList();
            }
            catch
            {
                return new List<ORDEN_COMPRA>();
            }
        }

        public List<ORDEN_COMPRA> ConsultarPorProveedor(int idProveedor)
        {
            try
            {
                return dbVeterinaria.ORDEN_COMPRA
                    .Include("DETALLE_ORDEN_COMPRA")
                    .Include("PROVEEDOR")
                    .Include("EMPLEADO")
                    .Include("SEDE")
                    .Where(x => x.id_proveedor == idProveedor)
                    .ToList();
            }
            catch
            {
                return new List<ORDEN_COMPRA>();
            }
        }

        public List<ORDEN_COMPRA> ConsultarPorSede(int idSede)
        {
            try
            {
                return dbVeterinaria.ORDEN_COMPRA
                    .Include("DETALLE_ORDEN_COMPRA")
                    .Include("PROVEEDOR")
                    .Include("EMPLEADO")
                    .Include("SEDE")
                    .Where(x => x.id_sede_destino == idSede)
                    .ToList();
            }
            catch
            {
                return new List<ORDEN_COMPRA>();
            }
        }

        public List<ORDEN_COMPRA> ConsultarPorEstado(string estado)
        {
            try
            {
                return dbVeterinaria.ORDEN_COMPRA
                    .Include("DETALLE_ORDEN_COMPRA")
                    .Include("PROVEEDOR")
                    .Include("EMPLEADO")
                    .Include("SEDE")
                    .Where(x => x.estado == estado)
                    .ToList();
            }
            catch
            {
                return new List<ORDEN_COMPRA>();
            }
        }
    }
}
