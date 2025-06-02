using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsUsuarioRol
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public USUARIO_ROL usuarioRol { get; set; }

        public string Insertar()
        {
            try
            {
                USUARIO_ROL usuarioRolExistente = ConsultarXUsuarioRol(usuarioRol.id_usuario, usuarioRol.id_rol);
                if (usuarioRolExistente != null)
                {
                    return "La asignación de rol a este usuario ya existe.";
                }

                dbVeterinaria.USUARIO_ROL.Add(usuarioRol);
                dbVeterinaria.SaveChanges();
                return "Asignación de rol a usuario insertada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar la asignación de rol a usuario: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                USUARIO_ROL usuarioRolExistente = ConsultarXId(usuarioRol.id_usuario_rol);
                if (usuarioRolExistente == null)
                {
                    return "Asignación de rol a usuario no encontrada.";
                }

                dbVeterinaria.USUARIO_ROL.AddOrUpdate(usuarioRol);
                dbVeterinaria.SaveChanges();
                return "Asignación de rol a usuario actualizada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar la asignación de rol a usuario: " + ex.Message;
            }
        }

        public USUARIO_ROL ConsultarXId(int id)
        {
            return dbVeterinaria.USUARIO_ROL.FirstOrDefault(ur => ur.id_usuario_rol == id);
        }

        public USUARIO_ROL ConsultarXUsuarioRol(int idUsuario, int idRol)
        {
            return dbVeterinaria.USUARIO_ROL.FirstOrDefault(ur => ur.id_usuario == idUsuario && ur.id_rol == idRol);
        }

        public List<USUARIO_ROL> ConsultarTodos()
        {
            return dbVeterinaria.USUARIO_ROL.ToList();
        }

        public string EliminarXId(int id)
        {
            try
            {
                USUARIO_ROL usuarioRol = ConsultarXId(id);
                if (usuarioRol == null)
                {
                    return "La asignación de rol a usuario no existe.";
                }

                dbVeterinaria.USUARIO_ROL.Remove(usuarioRol);
                dbVeterinaria.SaveChanges();
                return "Asignación de rol a usuario eliminada correctamente.";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar la asignación de rol a usuario: " + ex.Message;
            }
        }
    }
}
