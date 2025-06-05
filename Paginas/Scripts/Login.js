async function Ingresar() {
    let BaseURL = "http://localhost:44302";
    let URL = BaseURL + "/api/auth/login";
    const login = {
        Username: $("#txtUsuario").val(),
        Password: $("#txtClave").val()
    };

    try {
        const response = await fetch(URL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(login)
        });

        if (response.ok) {
            const data = await response.json();
            // Guarda el token en cookie o localStorage
            document.cookie = "auth_token=" + data.token + ";path=/";
            $("#dvMensaje").removeClass("alert alert-danger").addClass("alert alert-success");
            $("#dvMensaje").html("Login exitoso. Redirigiendo...");
            // Redirige inmediatamente a la página principal
            window.location.href = "Home.html";
        } else if (response.status === 401) {
            $("#dvMensaje").removeClass("alert alert-success").addClass("alert alert-danger");
            $("#dvMensaje").html("Usuario o clave incorrectos.");
        } else {
            $("#dvMensaje").removeClass("alert alert-success").addClass("alert alert-danger");
            $("#dvMensaje").html("Error al procesar la solicitud.");
        }
    } catch (e) {
        $("#dvMensaje").removeClass("alert alert-success").addClass("alert alert-danger");
        $("#dvMensaje").html("No se pudo conectar con el servicio.");
    }
}

// Clase auxiliar (opcional, no usada en el envío actual)
class Login {
    constructor(Usuario, Clave) {
        this.Usuario = Usuario;
        this.Clave = Clave;
    }
}