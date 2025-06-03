using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsFactura
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public FACTURA factura { get; set; }

        private const string ESTADO_PENDIENTE = "Pendiente";
        private const string ESTADO_PAGADA = "Pagada";
        private const string ESTADO_ANULADA = "Anulada";

        private bool EsTransicionValida(string estadoActual, string nuevoEstado)
        {
            if (estadoActual == ESTADO_PENDIENTE)
            {
                return nuevoEstado == ESTADO_PAGADA || nuevoEstado == ESTADO_ANULADA;
            }
            return false;
        }

        public string Insertar()
        {
            try
            {
                using (var transaction = dbVeterinaria.Database.BeginTransaction())
                {
                    try
                    {
                        factura.fecha_emision = DateTime.Now;
                        factura.estado = ESTADO_PENDIENTE;
                        
                        if (factura.DETALLE_FACTURA == null || !factura.DETALLE_FACTURA.Any())
                        {
                            return "La factura debe tener al menos un detalle.";
                        }
                        
                        factura.subtotal_factura = factura.DETALLE_FACTURA.Sum(d => d.subtotal_item ?? 0);
                        factura.impuestos_factura = factura.subtotal_factura * 0.19m;
                        factura.total_factura = factura.subtotal_factura + factura.impuestos_factura - (factura.descuento_factura ?? 0);

                        dbVeterinaria.FACTURAs.Add(factura);
                        dbVeterinaria.SaveChanges();

                        transaction.Commit();
                        return "Factura insertada correctamente.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error en la transacción: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error al insertar la factura: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                using (var transaction = dbVeterinaria.Database.BeginTransaction())
                {
                    try
                    {
                        FACTURA facturaExistente = ConsultarXId(factura.id_factura);
                        if (facturaExistente == null)
                        {
                            return "Factura no encontrada.";
                        }

                        if (facturaExistente.estado == "ANULADA")
                        {
                            return "No se puede modificar una factura anulada.";
                        }

                        factura.fecha_pago = DateTime.Now;
                        factura.subtotal_factura = factura.DETALLE_FACTURA.Sum(d => d.subtotal_item);
                        factura.impuestos_factura = factura.subtotal_factura * 0.19m;
                        factura.total_factura = factura.subtotal_factura + factura.impuestos_factura - (factura.descuento_factura ?? 0);

                        dbVeterinaria.FACTURAs.AddOrUpdate(factura);
                        dbVeterinaria.SaveChanges();

                        transaction.Commit();
                        return "Factura actualizada correctamente.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error en la transacción: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error al actualizar la factura: " + ex.Message;
            }
        }

        public string AnularFactura(int id)
        {
            using (var transaction = dbVeterinaria.Database.BeginTransaction())
            {
                try
                {
                    var facturaExistente = ConsultarXId(id);
                    if (facturaExistente == null)
                    {
                        return "Factura no encontrada.";
                    }

                    if (!EsTransicionValida(facturaExistente.estado, ESTADO_ANULADA))
                    {
                        return "No se puede anular la factura en el estado actual.";
                    }

                    // Revertir inventario si la factura tiene productos
                    var detalles = dbVeterinaria.DETALLE_FACTURA
                        .Where(d => d.id_factura == id)
                        .ToList();

                    foreach (var detalle in detalles)
                    {
                        if (detalle.tipo_item == "Producto" && detalle.id_item_referencia.HasValue)
                        {
                            var stock = dbVeterinaria.STOCK_PRODUCTO_SEDE
                                .FirstOrDefault(s => s.id_producto == detalle.id_item_referencia && 
                                                   s.id_sede == facturaExistente.id_sede);

                            if (stock != null && detalle.cantidad.HasValue)
                            {
                                stock.cantidad_disponible += detalle.cantidad.Value;
                                dbVeterinaria.Entry(stock).State = EntityState.Modified;
                            }
                        }
                    }

                    facturaExistente.estado = ESTADO_ANULADA;
                    dbVeterinaria.SaveChanges();
                    transaction.Commit();

                    return "Factura anulada correctamente.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return "Error al anular la factura: " + ex.Message;
                }
            }
        }

        public string RegistrarPago(int id, int idMetodoPago)
        {
            using (var transaction = dbVeterinaria.Database.BeginTransaction())
            {
                try
                {
                    var facturaExistente = ConsultarXId(id);
                    if (facturaExistente == null)
                    {
                        return "Factura no encontrada.";
                    }

                    if (!EsTransicionValida(facturaExistente.estado, ESTADO_PAGADA))
                    {
                        return "No se puede registrar el pago en el estado actual.";
                    }

                    // Verificar que el método de pago existe
                    var metodoPago = dbVeterinaria.METODO_PAGO.Find(idMetodoPago);
                    if (metodoPago == null)
                    {
                        return "Método de pago no encontrado.";
                    }

                    facturaExistente.estado = ESTADO_PAGADA;
                    facturaExistente.fecha_pago = DateTime.Now;
                    facturaExistente.id_metodo_pago = idMetodoPago;

                    dbVeterinaria.SaveChanges();
                    transaction.Commit();

                    return "Pago registrado correctamente.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return "Error al registrar el pago: " + ex.Message;
                }
            }
        }

    public FACTURA ConsultarXId(int id)
    {
        return dbVeterinaria.FACTURAs.FirstOrDefault(f => f.id_factura == id);
    }

    public List<FACTURA> ConsultarTodos()
    {
        return dbVeterinaria.FACTURAs.ToList();
        }

        public List<FACTURA> ConsultarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return dbVeterinaria.FACTURAs
                .Include("DETALLE_FACTURA")
                .Include("METODO_PAGO")
                .Where(f => f.fecha_emision >= fechaInicio && f.fecha_emision <= fechaFin)
                .ToList();
        }

        public List<FACTURA> ConsultarPorCliente(int idCliente)
        {
            return dbVeterinaria.FACTURAs
                .Include("DETALLE_FACTURA")
                .Include("METODO_PAGO")
                .Where(f => f.id_dueno == idCliente)
                .ToList();
        }
    }
}
