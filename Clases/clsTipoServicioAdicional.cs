using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsTipoServicioAdicional
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public TIPO_SERVICIO_ADICIONAL tipoServicioAdicional { get; set; }

        public string Insertar()
        {
            try
            {
                TIPO_SERVICIO_ADICIONAL tipoExistente = ConsultarXNombre(tipoServicioAdicional.nombre_servicio);
                if (tipoExistente != null)
                {
                    return "El tipo de servicio adicional ya está registrado.";
                }

                dbVeterinaria.TIPO_SERVICIO_ADICIONAL.Add(tipoServicioAdicional);
                dbVeterinaria.SaveChanges();
                return "Tipo de servicio adicional insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar el tipo de servicio adicional: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                TIPO_SERVICIO_ADICIONAL tipoExistente = ConsultarXId(tipoServicioAdicional.id_tipo_servicio_adicional);
                if (tipoExistente == null)
                {
                    return "Tipo de servicio adicional no encontrado.";
                }

                dbVeterinaria.TIPO_SERVICIO_ADICIONAL.AddOrUpdate(tipoServicioAdicional);
                dbVeterinaria.SaveChanges();
                return "Tipo de servicio adicional actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el tipo de servicio adicional: " + ex.Message;
            }
        }

        public TIPO_SERVICIO_ADICIONAL ConsultarXId(int id)
        {
            return dbVeterinaria.TIPO_SERVICIO_ADICIONAL.FirstOrDefault(t => t.id_tipo_servicio_adicional == id);
        }

         public TIPO_SERVICIO_ADICIONAL ConsultarXNombre(string nombre)
        {
            return dbVeterinaria.TIPO_SERVICIO_ADICIONAL.FirstOrDefault(t => t.nombre_servicio == nombre);
        }

        public List<TIPO_SERVICIO_ADICIONAL> ConsultarTodos()
        {
            return dbVeterinaria.TIPO_SERVICIO_ADICIONAL.ToList();
        }

        public string EliminarXId(int id)
        {
            try
            {
                TIPO_SERVICIO_ADICIONAL tipo = ConsultarXId(id);
                if (tipo == null)
                {
                    return "El tipo de servicio adicional no existe.";
                }

                dbVeterinaria.TIPO_SERVICIO_ADICIONAL.Remove(tipo);
                dbVeterinaria.SaveChanges();
                return "Tipo de servicio adicional eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el tipo de servicio adicional: " + ex.Message;
            }
        }
    }
}
