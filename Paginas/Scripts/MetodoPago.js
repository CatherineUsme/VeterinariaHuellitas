$(document).ready(function () {
    cargarMetodosPago();

    $('#formMetodoPago').on('submit', function (e) {
        e.preventDefault();
        let metodo = obtenerDatosFormulario();
        let esEdicion = !!metodo.id_metodo_pago;
        if (esEdicion) {
            actualizarMetodoPago(metodo);
        } else {
            insertarMetodoPago(metodo);
        }
    });

    $('#btnCancelarMetodo').on('click', function () {
        limpiarFormulario();
    });

    $('#tablaMetodoPago tbody').on('click', '.btn-editar', function () {
        let id = $(this).data('id');
        consultarMetodoPago(id);
    });

    $('#tablaMetodoPago tbody').on('click', '.btn-eliminar', function () {
        let id = $(this).data('id');
        if (confirm('¿Está seguro de eliminar este método de pago?')) {
            eliminarMetodoPago(id);
        }
    });
});

function obtenerDatosFormulario() {
    return {
        id_metodo_pago: $('#id_metodo_pago').val(),
        nombre_metodo: $('#nombre_metodo').val(),
        activo: $('#activo').val() === "true"
    };
}

function limpiarFormulario() {
    $('#formMetodoPago')[0].reset();
    $('#id_metodo_pago').val('');
    $('#btnGuardarMetodo').text('Guardar');
    $('#btnCancelarMetodo').addClass('d-none');
}

function cargarMetodosPago() {
    $.ajax({
        url: '/api/metodo-pago',
        method: 'GET',
        success: function (data) {
            llenarTablaMetodosPago(data);
        },
        error: function (xhr) {
            mostrarMensaje('Error al cargar los métodos de pago. Código: ' + xhr.status, false);
        }
    });
}

function llenarTablaMetodosPago(metodos) {
    let tbody = $('#tablaMetodoPago tbody');
    tbody.empty();
    if (metodos && metodos.length > 0) {
        metodos.forEach(function (metodo) {
            tbody.append(`
                <tr>
                    <td>${metodo.id_metodo_pago}</td>
                    <td>${metodo.nombre_metodo || ''}</td>
                    <td>${metodo.activo ? 'Sí' : 'No'}</td>
                    <td>
                        <button class="btn btn-sm btn-primary btn-editar" data-id="${metodo.id_metodo_pago}">Editar</button>
                        <button class="btn btn-sm btn-danger btn-eliminar" data-id="${metodo.id_metodo_pago}">Eliminar</button>
                    </td>
                </tr>
            `);
        });
    } else {
        tbody.append('<tr><td colspan="4" class="text-center">No hay métodos de pago registrados.</td></tr>');
    }
}

function insertarMetodoPago(metodo) {
    $.ajax({
        url: '/api/metodo-pago',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(metodo),
        success: function (msg) {
            mostrarMensaje(msg, true);
            limpiarFormulario();
            cargarMetodosPago();
        },
        error: function (xhr) {
            mostrarMensaje(xhr.responseText || 'Error al insertar el método de pago.', false);
        }
    });
}

function consultarMetodoPago(id) {
    $.ajax({
        url: '/api/metodo-pago/' + id,
        method: 'GET',
        success: function (metodo) {
            if (metodo) {
                $('#id_metodo_pago').val(metodo.id_metodo_pago);
                $('#nombre_metodo').val(metodo.nombre_metodo);
                $('#activo').val(metodo.activo ? "true" : "false");
                $('#btnGuardarMetodo').text('Actualizar');
                $('#btnCancelarMetodo').removeClass('d-none');
            }
        },
        error: function (xhr) {
            mostrarMensaje('Error al consultar el método de pago. Código: ' + xhr.status, false);
        }
    });
}

function actualizarMetodoPago(metodo) {
    $.ajax({
        url: '/api/metodo-pago',
        method: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(metodo),
        success: function (msg) {
            mostrarMensaje(msg, true);
            limpiarFormulario();
            cargarMetodosPago();
        },
        error: function (xhr) {
            mostrarMensaje(xhr.responseText || 'Error al actualizar el método de pago.', false);
        }
    });
}

function eliminarMetodoPago(id) {
    $.ajax({
        url: '/api/metodo-pago/' + id,
        method: 'DELETE',
        success: function (msg) {
            mostrarMensaje(msg, true);
            cargarMetodosPago();
        },
        error: function (xhr) {
            mostrarMensaje(xhr.responseText || 'Error al eliminar el método de pago.', false);
        }
    });
}

function mostrarMensaje(msg, esExito) {
    let div = $('#mensajeMetodoPago');
    div.html(`<div class="alert ${esExito ? 'alert-success' : 'alert-danger'}">${msg}</div>`);
    setTimeout(() => div.empty(), 3500);
}