using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Huellitas.Clases;
using VeterinariaHuellitas.Models;
using System.Security.Claims;
using VeterinariaHuellitas.Clases;

namespace VeterinariaHuellitas.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid login request.");
            }

            var authService = new AuthService();
            bool isAuthenticated = authService.AuthenticateUser(request.Username, request.Password);

            if (isAuthenticated)
            {
                List<string> roles = authService.GetUserRoles(request.Username);
                var token = TokenManager.GenerateJwtToken(request.Username, roles);
                return Ok(new { token = token });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("whoami")]
        [AuthorizeRoles("Administrador", "Veterinario")]
        public IHttpActionResult WhoAmI()
        {
            var principal = User as ClaimsPrincipal;

            if (principal != null && principal.Identity.IsAuthenticated)
            {
                var username = principal.Identity.Name;
                var roles = principal.Claims
                                     .Where(c => c.Type == ClaimTypes.Role)
                                     .Select(c => c.Value)
                                     .ToList();

                return Ok(new { username = username, roles = roles });
            }

            return Unauthorized();
        }


        
        [HttpPost]
        [Route("cambiarPassTodos")]
        public IHttpActionResult SetAllPasswords([FromBody] string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                return BadRequest("Password requerido.");
            }

            clsUsuario servicio = new clsUsuario();
            var usuarios = servicio.ConsultarTodos();
            var authService = new AuthService();
            string salt = Guid.NewGuid().ToString();
            string hash = authService.HashPassword(newPassword, salt);
            foreach (var usuario in usuarios)
            {
                usuario.clave_hash = hash;
                usuario.salt = salt;
                servicio.usuario = usuario;
                servicio.Actualizar();
            }
            return Ok(new { message = "Contraseñas actualizadas para todos los usuarios." });
        }
    }
}