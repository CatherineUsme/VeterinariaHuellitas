using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsServicioAdicionalPrestado
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public SERVICIO_ADICIONAL_PRESTADO servicioAdicionalPrestado { get; set; }

        private const string ESTADO_PROGRAMADO = "Programado";
        private const string ESTADO_COMPLETADO = "Completado";
        private const string ESTADO_CANCELADO = "Cancelado";

        private bool EsTransicionValida(string estadoActual, string nuevoEstado)
        {
            if (estadoActual == ESTADO_PROGRAMADO)
            {
                return nuevoEstado == ESTADO_COMPLETADO || nuevoEstado == ESTADO_CANCELADO;
            }
            return false;
        }

        public string Insertar()
        {
            try
            {
                servicioAdicionalPrestado.estado = ESTADO_PROGRAMADO;
                dbVeterinaria.SERVICIO_ADICIONAL_PRESTADO.Add(servicioAdicionalPrestado);
                dbVeterinaria.SaveChanges();
                return "Servicio adicional prestado registrado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al registrar el servicio adicional prestado: " + ex.Message;
            }
        }

        public SERVICIO_ADICIONAL_PRESTADO ConsultarXId(int id)
        {
            return dbVeterinaria.SERVICIO_ADICIONAL_PRESTADO
                .Include(s => s.EMPLEADO)
                .Include(s => s.MASCOTA)
                .Include(s => s.SEDE)
                .Include(s => s.TIPO_SERVICIO_ADICIONAL)
                .FirstOrDefault(s => s.id_servicio_prestado == id);
        }

        public List<SERVICIO_ADICIONAL_PRESTADO> ConsultarTodos()
        {
            return dbVeterinaria.SERVICIO_ADICIONAL_PRESTADO
                .Include(s => s.EMPLEADO)
                .Include(s => s.MASCOTA)
                .Include(s => s.SEDE)
                .Include(s => s.TIPO_SERVICIO_ADICIONAL)
                .ToList();
        }

        public string CompletarServicio(int id)
        {
            using (var transaction = dbVeterinaria.Database.BeginTransaction())
            {
                try
                {
                    var servicio = ConsultarXId(id);
                    if (servicio == null)
                    {
                        return "Servicio no encontrado.";
                    }

                    if (!EsTransicionValida(servicio.estado, ESTADO_COMPLETADO))
                    {
                        return "No se puede completar el servicio en su estado actual.";
                    }

                    servicio.estado = ESTADO_COMPLETADO;
                    servicio.fecha_servicio = DateTime.Now;
                    dbVeterinaria.SaveChanges();
                    transaction.Commit();

                    return "Servicio completado correctamente.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return "Error al completar el servicio: " + ex.Message;
                }
            }
        }

        public string CancelarServicio(int id)
        {
            using (var transaction = dbVeterinaria.Database.BeginTransaction())
            {
                try
                {
                    var servicio = ConsultarXId(id);
                    if (servicio == null)
                    {
                        return "Servicio no encontrado.";
                    }

                    if (!EsTransicionValida(servicio.estado, ESTADO_CANCELADO))
                    {
                        return "No se puede cancelar el servicio en su estado actual.";
                    }

                    servicio.estado = ESTADO_CANCELADO;
                    dbVeterinaria.SaveChanges();
                    transaction.Commit();

                    return "Servicio cancelado correctamente.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return "Error al cancelar el servicio: " + ex.Message;
                }
            }
        }
    }
}
