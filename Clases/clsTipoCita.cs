using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsTipoCita
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public TIPO_CITA tipoCita { get; set; }

        public string Insertar()
        {
            try
            {
                TIPO_CITA tipoExistente = ConsultarXNombre(tipoCita.nombre_tipo_cita);
                if (tipoExistente != null)
                {
                    return "El tipo de cita ya está registrado.";
                }

                dbVeterinaria.TIPO_CITA.Add(tipoCita);
                dbVeterinaria.SaveChanges();
                return "Tipo de cita insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar el tipo de cita: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                TIPO_CITA tipoExistente = ConsultarXId(tipoCita.id_tipo_cita);
                if (tipoExistente == null)
                {
                    return "Tipo de cita no encontrado.";
                }

                dbVeterinaria.TIPO_CITA.AddOrUpdate(tipoCita);
                dbVeterinaria.SaveChanges();
                return "Tipo de cita actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el tipo de cita: " + ex.Message;
            }
        }

        public TIPO_CITA ConsultarXId(int id)
        {
            return dbVeterinaria.TIPO_CITA.FirstOrDefault(t => t.id_tipo_cita == id);
        }

         public TIPO_CITA ConsultarXNombre(string nombre)
        {
            return dbVeterinaria.TIPO_CITA.FirstOrDefault(t => t.nombre_tipo_cita == nombre);
        }

        public List<TIPO_CITA> ConsultarTodos()
        {
            return dbVeterinaria.TIPO_CITA.ToList();
        }

        public string EliminarXId(int id)
        {
            try
            {
                TIPO_CITA tipo = ConsultarXId(id);
                if (tipo == null)
                {
                    return "El tipo de cita no existe.";
                }

                dbVeterinaria.TIPO_CITA.Remove(tipo);
                dbVeterinaria.SaveChanges();
                return "Tipo de cita eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el tipo de cita: " + ex.Message;
            }
        }
    }
}
