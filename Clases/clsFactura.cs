using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsFactura
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public FACTURA factura { get; set; }

        public string Insertar()
        {
            try
            {
                using (var transaction = dbVeterinaria.Database.BeginTransaction())
                {
                    try
                    {
                        factura.fecha_emision = DateTime.Now;
                        factura.estado = "EMITIDA";
                        
                        if (factura.DETALLE_FACTURA == null || !factura.DETALLE_FACTURA.Any())
                        {
                            return "La factura debe tener al menos un detalle.";
                        }                    var detalles = dbVeterinaria.DETALLE_FACTURA
                        .Where(d => d.id_factura == factura.id_factura)
                        .ToList();

                    factura.subtotal_factura = detalles.Sum(d => d.subtotal_item ?? 0);
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
            try
            {
                using (var transaction = dbVeterinaria.Database.BeginTransaction())
                {
                    try
                    {
                        FACTURA facturaExistente = ConsultarXId(id);
                        if (facturaExistente == null)
                        {
                            return "Factura no encontrada.";
                        }

                        if (facturaExistente.estado == "ANULADA")
                        {
                            return "La factura ya está anulada.";
                        }

                        facturaExistente.estado = "ANULADA";
                        facturaExistente.fecha_pago = DateTime.Now;

                        dbVeterinaria.FACTURAs.AddOrUpdate(facturaExistente);
                        dbVeterinaria.SaveChanges();

                        transaction.Commit();
                        return "Factura anulada correctamente.";
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
                return "Error al anular la factura: " + ex.Message;
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
