using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsDetalleFactura
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public DETALLE_FACTURA detalleFactura { get; set; }

        public string Insertar()
        {
            try
            {
                if (detalleFactura.cantidad <= 0)
                {
                    return "La cantidad debe ser mayor a 0.";
                }

                if (detalleFactura.precio_unitario <= 0)
                {
                    return "El precio unitario debe ser mayor a 0.";
                }                detalleFactura.subtotal_item = detalleFactura.cantidad * detalleFactura.precio_unitario;

                dbVeterinaria.DETALLE_FACTURA.Add(detalleFactura);
                dbVeterinaria.SaveChanges();
                return "Detalle de factura insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar el detalle de factura: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                DETALLE_FACTURA detalleExistente = ConsultarXId(detalleFactura.id_detalle_factura);
                if (detalleExistente == null)
                {
                    return "Detalle de factura no encontrado.";
                }            var factura = dbVeterinaria.FACTURAs.Where(f => f.id_factura == detalleExistente.id_factura)
                .Select(f => new { f.estado })
                .FirstOrDefault();
                
            if (factura != null && factura.estado == "ANULADA")
            {
                return "No se puede modificar un detalle de una factura anulada.";
            }

                if (detalleFactura.cantidad <= 0)
                {
                    return "La cantidad debe ser mayor a 0.";
                }

                if (detalleFactura.precio_unitario <= 0)
                {
                    return "El precio unitario debe ser mayor a 0.";
                }

                detalleFactura.subtotal_item = detalleFactura.cantidad * detalleFactura.precio_unitario;

                dbVeterinaria.DETALLE_FACTURA.AddOrUpdate(detalleFactura);
                dbVeterinaria.SaveChanges();
                return "Detalle de factura actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el detalle de factura: " + ex.Message;
            }
        }

        public DETALLE_FACTURA ConsultarXId(int id)
        {
            return dbVeterinaria.DETALLE_FACTURA.FirstOrDefault(df => df.id_detalle_factura == id);
        }

        public List<DETALLE_FACTURA> ConsultarPorFactura(int idFactura)
        {
            return dbVeterinaria.DETALLE_FACTURA
                .Where(df => df.id_factura == idFactura)
                .ToList();
        }

        public string EliminarXId(int id)
        {
            try
            {
                DETALLE_FACTURA detalle = ConsultarXId(id);
                if (detalle == null)
                {
                    return "El detalle de factura no existe.";
                }

                var factura = dbVeterinaria.FACTURAs.Find(detalle.id_factura);
                if (factura.estado == "ANULADA")
                {
                    return "No se puede eliminar un detalle de una factura anulada.";
                }

                dbVeterinaria.DETALLE_FACTURA.Remove(detalle);
                dbVeterinaria.SaveChanges();
                return "Detalle de factura eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el detalle de factura: " + ex.Message;
            }
        }
    }
}
