using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;
using System.Data.Entity;

namespace VeterinariaHuellitas.Clases
{
    public class clsUsuario
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public USUARIO usuario { get; set; }

        public string Insertar()
        {
            try
            {
                USUARIO usuarioExistente = ConsultarXUsername(usuario.username);
                if (usuarioExistente != null)
                {
                    return "El nombre de usuario ya está registrado.";
                }

                dbVeterinaria.USUARIOs.Add(usuario);
                dbVeterinaria.SaveChanges();
                return "Usuario insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar el usuario: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                USUARIO usuarioExistente = ConsultarXId(usuario.id_usuario);
                if (usuarioExistente == null)
                {
                    return "Usuario no encontrado.";
                }

                dbVeterinaria.USUARIOs.AddOrUpdate(usuario);
                dbVeterinaria.SaveChanges();
                return "Usuario actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el usuario: " + ex.Message;
            }
        }

        public USUARIO ConsultarXId(int id)
        {
            return dbVeterinaria.USUARIOs.Include(u => u.USUARIO_ROL).FirstOrDefault(u => u.id_usuario == id);
        }

         public USUARIO ConsultarXUsername(string username)
        {
            return dbVeterinaria.USUARIOs.Include(u => u.USUARIO_ROL).FirstOrDefault(u => u.username == username);
        }

        public List<USUARIO> ConsultarTodos()
        {
            return dbVeterinaria.USUARIOs.Include(u => u.USUARIO_ROL).ToList();
        }

        public string EliminarXId(int id)
        {
            try
            {
                USUARIO usuario = ConsultarXId(id);
                if (usuario == null)
                {
                    return "El usuario no existe.";
                }

                dbVeterinaria.USUARIOs.Remove(usuario);
                dbVeterinaria.SaveChanges();
                return "Usuario eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el usuario: " + ex.Message;
            }
        }

        public string ModificarEstado(int id, bool activo)
        {
            try
            {
                USUARIO usuario = ConsultarXId(id);
                if (usuario == null)
                {
                    return "El usuario ingresado no existe.";
                }
                usuario.activo = activo;
                dbVeterinaria.SaveChanges();
                if (activo)
                {
                    return "Se activó correctamente el usuario.";
                }
                else
                {
                    return "Se inactivó correctamente el usuario.";
                }
            }
            catch (Exception ex)
            {
                return "Hubo un error al modificar el estado del usuario: " + ex.Message;
            }
        }
    }
}
