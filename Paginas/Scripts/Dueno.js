var UrlBase = "http://localhost:44302/";

jQuery(function () {
    //Registrar los botones para responder al evento click 
    $("#dvMenu").load("../Paginas/Menu.html")
    LlenarTablaDuenos();
});
function LlenarTablaDuenos() {
    let URL = UrlBase + "api/duenos/ConsultarTodos";
    LlenarTablaXServiciosAuth(URL, "#tblDuenos");
}

async function Consultar() {
    let cedula = $("#txtDocumento").val();
    let URL = UrlBase + "api/duenos/ConsultarXCedula?cedula=" + cedula;
    const dueno = await ConsultarServicioAuth(URL);
    if (dueno == null || dueno == undefined) {
        $("#dvMensaje").removeClass("alert alert-success");
        $("#dvMensaje").addClass("alert alert-danger");
        $("#dvMensaje").html("El dueño no existe");
    }
    else {
        $("#txtDocumento").val(dueno.cedula);
        $("#txtNombre").val(dueno.nombre);
        $("#txtApellidos").val(dueno.apellido);
        $("#txtEmail").val(dueno.email);
        $("#txtTelefono").val(dueno.telefono);
        $("#txtDireccion").val(dueno.direccion);
        $("#txtId").val(dueno.id_dueno); 
        $("#txtFechaCreacion").val(dueno.fecha_registro.split('T')[0]);
        $("#chkActivo").prop("checked", dueno.activo);

    }
    
}

class dueno {
    constructor(id_dueno, nombre, apellido, cedula, email, telefono, direccion, fecha_registro, activo) {
        this.id_dueno = id_dueno;
        this.Nombre = nombre;
        this.apellido = apellido;
        this.cedula = cedula;
        this.email = email;
        this.telefono = telefono;
        this.direccion = direccion;
        this.fecha_registro = fecha_registro;
        this.activo = activo;
    }
}