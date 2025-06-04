using System;
using System.Collections.Generic;
using System.Linq;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class AuthService
    {
        public bool AuthenticateUser(string username, string password)
        {
            clsUsuario servicio = new clsUsuario();
            var usuario = servicio.ConsultarXUsername(username);
            if (usuario == null || usuario.activo != true)
                return false;
            string hash = HashPassword(password, usuario.salt);
            return usuario.clave_hash == hash;
        }

        public List<string> GetUserRoles(string username)
        {
            clsUsuario servicio = new clsUsuario();
            var usuario = servicio.ConsultarXUsername(username);
            if (usuario == null)
                return new List<string>();
            var roles = new List<string>();
            if (usuario.USUARIO_ROL != null)
            {
                foreach (var ur in usuario.USUARIO_ROL)
                {
                    string nombreRol = null;
                    if (ur.ROL != null && !string.IsNullOrEmpty(ur.ROL.nombre_rol))
                    {
                        nombreRol = ur.ROL.nombre_rol;
                    }
                    else if (ur.id_rol > 0)
                    {
                        using (var db = new VeterinariaEntities())
                        {
                            var rol = db.ROLs.FirstOrDefault(r => r.id_rol == ur.id_rol);
                            if (rol != null)
                                nombreRol = rol.nombre_rol;
                        }
                    }
                    if (!string.IsNullOrEmpty(nombreRol))
                        roles.Add(nombreRol);
                }
            }
            return roles;
        }

        public string HashPassword(string password, string salt)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var combined = System.Text.Encoding.UTF8.GetBytes(password + salt);
                var hash = sha256.ComputeHash(combined);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
