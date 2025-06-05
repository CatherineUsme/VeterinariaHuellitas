var UrlBase = "http://localhost:44302/";

// Al cargar la página, inicializa la fecha y la tabla
jQuery(function () {
    $("#dvMenu").load("../Paginas/Menu.html");
    LlenarTablaDuenos();
    LimpiarFormulario();
});

// Llenar la tabla usando la función de CRUD.js
function LlenarTablaDuenos() {
    let URL = UrlBase + "api/duenos/ConsultarTodos";
    LlenarTablaXServiciosAuth(URL, "#tblDuenos");
}

// Ejecutar comando para Insertar, Actualizar o Eliminar
async function EjecutarComando(Metodo, Funcion) {
    let URL = UrlBase + "api/duenos/" + Funcion;
    if (Metodo === 'DELETE') {
        // Agrega la cédula como parámetro en la URL
        const cedula = $("#txtDocumento").val();
        URL += "?cedula=" + encodeURIComponent(cedula);
        await EjecutarComandoServicioAuth(Metodo, URL);
    } else {
    // Solo envía id_dueno si está presente (para actualizar)
        const dueno = {
            id_dueno: $("#txtId").val() || undefined,
            nombre: $("#txtNombre").val(),
            apellido: $("#txtApellidos").val(),
            cedula: $("#txtDocumento").val(),
            email: $("#txtEmail").val(),
            telefono: $("#txtTelefono").val(),
            direccion: $("#txtDireccion").val(),
            fecha_registro: $("#txtFechaCreacion").val(),
            activo: $("#chkActivo").is(":checked")
    };
    await EjecutarComandoServicioAuth(Metodo, URL, dueno);
    LlenarTablaDuenos();
    LimpiarFormulario();
}

// Consultar por cédula y llenar el formulario
async function Consultar() {
    let cedula = $("#txtDocumento").val();
    let URL = UrlBase + "api/duenos/ConsultarXCedula?cedula=" + cedula;
    const dueno = await ConsultarServicioAuth(URL);
    if (!dueno) {
        $("#dvMensaje").removeClass().addClass("alert alert-danger").html("El dueño no existe");
        LimpiarFormulario();
    } else {
        $("#dvMensaje").removeClass().addClass("alert alert-success").html("");
        $("#txtId").val(dueno.id_dueno);
        $("#txtNombre").val(dueno.nombre);
        $("#txtApellidos").val(dueno.apellido);
        $("#txtDocumento").val(dueno.cedula);
        $("#txtEmail").val(dueno.email);
        $("#txtTelefono").val(dueno.telefono);
        $("#txtDireccion").val(dueno.direccion);
        $("#txtFechaCreacion").val(dueno.fecha_registro ? dueno.fecha_registro.split('T')[0] : "");
        $("#chkActivo").prop("checked", dueno.activo);
    }
}

// Limpia el formulario y pone la fecha actual en fecha de creación
function LimpiarFormulario() {
    $("#txtId").val("");
    $("#txtNombre").val("");
    $("#txtApellidos").val("");
    $("#txtDocumento").val("");
    $("#txtEmail").val("");
    $("#txtTelefono").val("");
    $("#txtDireccion").val("");
    $("#txtFechaCreacion").val(new Date().toISOString().split('T')[0]);
    $("#chkActivo").prop("checked", true);
    $("#dvMensaje").removeClass().html("");
}
