$(document).ready(function () {
    listarProveedores();

    $('#formProveedor').on('submit', function (e) {
        e.preventDefault();
        guardarProveedor();
    });

    $('#btnCancelar').on('click', function () {
        limpiarFormulario();
    });
});

function listarProveedores() {
    $.getJSON('/api/Proveedor', function (data) {
        var $tbody = $('#tablaProveedores tbody');
        $tbody.empty();
        $.each(data, function (i, prov) {
            $tbody.append('<tr>' +
                '<td>' + prov.id_proveedor + '</td>' +
                '<td>' + prov.nombre_empresa + '</td>' +
                '<td>' + prov.nit_ruc + '</td>' +
                '<td>' + (prov.contacto_nombre || '') + '</td>' +
                '<td>' + (prov.contacto_telefono || '') + '</td>' +
                '<td>' + (prov.contacto_email || '') + '</td>' +
                '<td>' + (prov.activo ? 'Sí' : 'No') + '</td>' +
                '<td>' +
                '<button class="btn btn-sm btn-warning me-1" onclick="editarProveedor(' + prov.id_proveedor + ')">Editar</button>' +
                '<button class="btn btn-sm btn-danger" onclick="eliminarProveedor(' + prov.id_proveedor + ')">Eliminar</button>' +
                '</td>' +
                '</tr>');
        });
    }).fail(function (jqXHR) {
        $('#mensajeProveedor').html('<div class="alert alert-danger">No se pudo obtener la información. ' + (jqXHR.responseText || '') + '</div>');
    });
}

function guardarProveedor() {
    var proveedor = {
        id_proveedor: $('#id_proveedor').val() || 0,
        nombre_empresa: $('#nombre_empresa').val(),
        nit_ruc: $('#nit_ruc').val(),
        contacto_nombre: $('#contacto_nombre').val(),
        contacto_telefono: $('#contacto_telefono').val(),
        contacto_email: $('#contacto_email').val(),
        activo: $('#activo').val() === "true"
    };

    var esNuevo = proveedor.id_proveedor == 0 || proveedor.id_proveedor === "";

    var url = '/api/Proveedor';
    var tipo = esNuevo ? 'POST' : 'PUT';

    $.ajax({
        url: url,
        type: tipo,
        contentType: 'application/json',
        data: JSON.stringify(proveedor),
        success: function (resp) {
            $('#mensajeProveedor').html('<div class="alert alert-success">' + resp + '</div>');
            limpiarFormulario();
            listarProveedores();
        },
        error: function (xhr) {
            let msg = "Error al guardar el proveedor.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeProveedor').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function editarProveedor(id) {
    $.getJSON('/api/Proveedor/' + id, function (prov) {
        $('#id_proveedor').val(prov.id_proveedor);
        $('#nombre_empresa').val(prov.nombre_empresa);
        $('#nit_ruc').val(prov.nit_ruc);
        $('#contacto_nombre').val(prov.contacto_nombre);
        $('#contacto_telefono').val(prov.contacto_telefono);
        $('#contacto_email').val(prov.contacto_email);
        $('#activo').val(prov.activo ? "true" : "false");
        $('#btnGuardar').text('Actualizar Proveedor');
        $('#btnCancelar').removeClass('d-none');
    });
}

function eliminarProveedor(id) {
    if (!confirm('¿Está seguro de eliminar este proveedor?')) return;
    $.ajax({
        url: '/api/Proveedor/' + id,
        type: 'DELETE',
        success: function (resp) {
            $('#mensajeProveedor').html('<div class="alert alert-success">' + resp + '</div>');
            listarProveedores();
        },
        error: function (xhr) {
            let msg = "Error al eliminar el proveedor.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeProveedor').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function limpiarFormulario() {
    $('#formProveedor')[0].reset();
    $('#id_proveedor').val('');
    $('#btnGuardar').text('Guardar Proveedor');
    $('#btnCancelar').addClass('d-none');
    $('#mensajeProveedor').empty();
}