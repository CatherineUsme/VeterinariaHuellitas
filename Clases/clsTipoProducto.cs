using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsTipoProducto
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public TIPO_PRODUCTO tipoProducto { get; set; }

        public string Insertar()
        {
            try
            {
                TIPO_PRODUCTO tipoExistente = ConsultarXNombre(tipoProducto.nombre_tipo);
                if (tipoExistente != null)
                {
                    return "El tipo de producto ya está registrado.";
                }

                dbVeterinaria.TIPO_PRODUCTO.Add(tipoProducto);
                dbVeterinaria.SaveChanges();
                return "Tipo de producto insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar el tipo de producto: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                TIPO_PRODUCTO tipoExistente = ConsultarXId(tipoProducto.id_tipo_producto);
                if (tipoExistente == null)
                {
                    return "Tipo de producto no encontrado.";
                }

                dbVeterinaria.TIPO_PRODUCTO.AddOrUpdate(tipoProducto);
                dbVeterinaria.SaveChanges();
                return "Tipo de producto actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el tipo de producto: " + ex.Message;
            }
        }

        public TIPO_PRODUCTO ConsultarXId(int id)
        {
            return dbVeterinaria.TIPO_PRODUCTO.FirstOrDefault(t => t.id_tipo_producto == id);
        }

         public TIPO_PRODUCTO ConsultarXNombre(string nombre)
        {
            return dbVeterinaria.TIPO_PRODUCTO.FirstOrDefault(t => t.nombre_tipo == nombre);
        }

        public List<TIPO_PRODUCTO> ConsultarTodos()
        {
            return dbVeterinaria.TIPO_PRODUCTO.ToList();
        }

        public string EliminarXId(int id)
        {
            try
            {
                TIPO_PRODUCTO tipo = ConsultarXId(id);
                if (tipo == null)
                {
                    return "El tipo de producto no existe.";
                }

                dbVeterinaria.TIPO_PRODUCTO.Remove(tipo);
                dbVeterinaria.SaveChanges();
                return "Tipo de producto eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el tipo de producto: " + ex.Message;
            }
        }
    }
}
