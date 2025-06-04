using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsMetodoPago
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public METODO_PAGO metodoPago { get; set; }

        public string Insertar()
        {
            try
            {
                METODO_PAGO metodoPagoExistente = ConsultarXNombre(metodoPago.nombre_metodo);
                if (metodoPagoExistente != null)
                {
                    return "Ya existe un método de pago con ese nombre.";
                }

                metodoPago.activo = true;
                dbVeterinaria.METODO_PAGO.Add(metodoPago);
                dbVeterinaria.SaveChanges();
                return "Método de pago insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar el método de pago: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                METODO_PAGO metodoPagoExistente = ConsultarXId(metodoPago.id_metodo_pago);
                if (metodoPagoExistente == null)
                {
                    return "Método de pago no encontrado.";
                }

                dbVeterinaria.METODO_PAGO.AddOrUpdate(metodoPago);
                dbVeterinaria.SaveChanges();
                return "Método de pago actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el método de pago: " + ex.Message;
            }
        }

        public METODO_PAGO ConsultarXId(int id)
        {
            return dbVeterinaria.METODO_PAGO.FirstOrDefault(mp => mp.id_metodo_pago == id);
        }

        public METODO_PAGO ConsultarXNombre(string nombre)
        {
            return dbVeterinaria.METODO_PAGO.FirstOrDefault(mp => mp.nombre_metodo.ToLower() == nombre.ToLower());
        }

        public List<METODO_PAGO> ConsultarTodos()
        {
            return dbVeterinaria.METODO_PAGO.Where(mp => mp.activo == true).ToList();
        }

        public string EliminarXId(int id)
        {
            try
            {
                METODO_PAGO metodoPago = ConsultarXId(id);
                if (metodoPago == null)
                {
                    return "El método de pago no existe.";
                }                var tieneFacturas = dbVeterinaria.FACTURAs.Any(f => f.id_metodo_pago == id);
                if (tieneFacturas)
                {
                    metodoPago.activo = false;
                    dbVeterinaria.METODO_PAGO.AddOrUpdate(metodoPago);
                }
                else
                {
                    dbVeterinaria.METODO_PAGO.Remove(metodoPago);
                }

                dbVeterinaria.SaveChanges();
                return "Método de pago eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el método de pago: " + ex.Message;
            }
        }
    }
}
