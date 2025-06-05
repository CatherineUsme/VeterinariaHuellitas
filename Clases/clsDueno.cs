using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsDueno
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public DUENO dueno { get; set; }

        public string Insertar()
        {
            try
            {
                DUENO duenoExite = ConsultarXCedula(dueno.cedula);
                if (duenoExite != null)
                {
                    return "El dueño ya está registrado en la base de datos.";
                }
                dueno.fecha_registro = DateTime.Now; // Asigna la fecha del sistema
                dueno.activo = true; // Activo por defecto
                dbVeterinaria.DUENOes.Add(dueno);
                dbVeterinaria.SaveChanges();
                return "Dueño insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar el dueño: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                DUENO duen = ConsultarXCedula(dueno.cedula);
                if (duen != null)
                {
                    // Mantener la fecha de registro original
                    dueno.fecha_registro = duen.fecha_registro;
                    dbVeterinaria.DUENOes.AddOrUpdate(dueno);
                    dbVeterinaria.SaveChanges();
                    return "Dueño actualizado correctamente.";
                }
                else
                {
                    return "dueño no encontrado.";
                }
            }
            catch (Exception ex)
            {
                return "Error al actualizar el dueño: " + ex.Message;
            }
        }

        public DUENO ConsultarXCedula(string cedula)
        {
            return dbVeterinaria.DUENOes.FirstOrDefault(e => e.cedula == cedula);
        }

        public List<DUENO> ConsultarTodos()
        {
            return dbVeterinaria.DUENOes
                .OrderBy(n => n.nombre).ToList();
        }

        public string EliminarXCedula(string cedula)
        {
            try
            {
                DUENO duen = ConsultarXCedula(cedula);
                if (duen == null)
                {
                    return "El dueño ingresado no existe, por lo tanto no se puede eliminar";
                }

                dbVeterinaria.DUENOes.Remove(duen);
                dbVeterinaria.SaveChanges();
                return "Se eliminó el dueño correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el dueño: " + ex.Message;
            }
        }

        public string ModificarEstado(string cedula, bool Activo)
        {
            try
            {
                DUENO duen = ConsultarXCedula(cedula);
                if (duen == null)
                {
                    return "La cedula ingresada no existe en la Base de Datos";
                }
                duen.activo = Activo;
                dbVeterinaria.SaveChanges();
                if (Activo)
                {
                    return "Se activó correctamente el dueño";
                }
                else
                {
                    return "Se inactivó correctamente el dueño";
                }
            }
            catch (Exception ex)
            {
                return "Hubo un error al modificar el estado del dueño: " + ex.Message;
            }
        }
    }
}