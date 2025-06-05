async function Ingresar() {
    let BaseURL = "http://joseitm20251.runasp.net";
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
            document.cookie = "token=" + data.token + ";path=/";
            $("#dvMensaje").removeClass("alert alert-danger").addClass("alert alert-success");
            $("#dvMensaje").html("Login exitoso. Redirigiendo...");
            // Redirige a la página principal
            setTimeout(() => window.location.href = "Home.html", 1000);
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

class Login {
    constructor(Usuario, Clave) {
        this.Usuario = Usuario;
        this.Clave = Clave;
    }
}