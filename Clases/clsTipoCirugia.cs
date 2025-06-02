using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsTipoCirugia
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public TIPO_CIRUGIA tipoCirugia { get; set; }

        public string Insertar()
        {
            try
            {
                TIPO_CIRUGIA tipoExistente = ConsultarXNombre(tipoCirugia.nombre_tipo_cirugia);
                if (tipoExistente != null)
                {
                    return "El tipo de cirugía ya está registrado.";
                }

                dbVeterinaria.TIPO_CIRUGIA.Add(tipoCirugia);
                dbVeterinaria.SaveChanges();
                return "Tipo de cirugía insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar el tipo de cirugía: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                TIPO_CIRUGIA tipoExistente = ConsultarXId(tipoCirugia.id_tipo_cirugia);
                if (tipoExistente == null)
                {
                    return "Tipo de cirugía no encontrado.";
                }

                dbVeterinaria.TIPO_CIRUGIA.AddOrUpdate(tipoCirugia);
                dbVeterinaria.SaveChanges();
                return "Tipo de cirugía actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el tipo de cirugía: " + ex.Message;
            }
        }

        public TIPO_CIRUGIA ConsultarXId(int id)
        {
            return dbVeterinaria.TIPO_CIRUGIA.FirstOrDefault(t => t.id_tipo_cirugia == id);
        }

         public TIPO_CIRUGIA ConsultarXNombre(string nombre)
        {
            return dbVeterinaria.TIPO_CIRUGIA.FirstOrDefault(t => t.nombre_tipo_cirugia == nombre);
        }

        public List<TIPO_CIRUGIA> ConsultarTodos()
        {
            return dbVeterinaria.TIPO_CIRUGIA.ToList();
        }

        public string EliminarXId(int id)
        {
            try
            {
                TIPO_CIRUGIA tipo = ConsultarXId(id);
                if (tipo == null)
                {
                    return "El tipo de cirugía no existe.";
                }

                dbVeterinaria.TIPO_CIRUGIA.Remove(tipo);
                dbVeterinaria.SaveChanges();
                return "Tipo de cirugía eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el tipo de cirugía: " + ex.Message;
            }
        }
    }
}
