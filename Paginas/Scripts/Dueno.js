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

function Consultar() {
    let cedula = $("#txtDocumento").val();
    let URL = UrlBase + "api/duenos/ConsultarXCedula?cedula=" + cedula;
    const dueno = await ConsultarServicioAuth(URL);
    if (dueno == null || empleado == undefined) {
        $("#dvMensaje").removeClass("alert alert-success");
        $("#dvMensaje").addClass("alert alert-danger");
        $("#dvMensaje").html("El dueño no existe");
    }
    else {
        $("#txtDocumento").val(dueno.cedula);
        $("#txtNombre").val(dueno.Nombre);
        $("#txtApellido").val(dueno.apellido);
        $("#txtEmail").val(dueno.email);
        $("#txtTelefono").val(dueno.telefono);
        $("#txtDireccion").val(dueno.direccion);
        $("#txtFechaRegistro").val(dueno.fecha_registro.split('T')[0]);
        $("#chkActivo").prop("checked", dueno.activo);)

    }
    
}

}