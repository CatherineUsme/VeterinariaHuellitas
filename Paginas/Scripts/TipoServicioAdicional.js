$(document).ready(function () {
    cargarTipos();

    $('#formTipoServicioAdicional').on('submit', function (e) {
        e.preventDefault();
        let tipo = obtenerDatosFormulario();
        let esEdicion = !!tipo.id_tipo_servicio_adicional;
        if (esEdicion) {
            actualizarTipo(tipo);
        } else {
            insertarTipo(tipo);
        }
    });

    $('#btnCancelarTipo').on('click', function () {
        limpiarFormulario();
    });

    $('#tablaTipoServicioAdicional tbody').on('click', '.btn-editar', function () {
        let id = $(this).data('id');
        consultarTipo(id);
    });

    $('#tablaTipoServicioAdicional tbody').on('click', '.btn-eliminar', function () {
        let id = $(this).data('id');
        if (confirm('¿Está seguro de eliminar este tipo de servicio adicional?')) {
            eliminarTipo(id);
        }
    });
});

function obtenerDatosFormulario() {
    return {
        id_tipo_servicio_adicional: $('#id_tipo_servicio_adicional').val(),
        nombre_servicio: $('#nombre_servicio').val(),
        descripcion: $('#descripcion').val(),
        precio_base: $('#precio_base').val(),
        activo: $('#activo').val() === "true"
    };
}

function limpiarFormulario() {
    $('#formTipoServicioAdicional')[0].reset();
    $('#id_tipo_servicio_adicional').val('');
    $('#btnGuardarTipo').text('Guardar');
    $('#btnCancelarTipo').addClass('d-none');
}

function cargarTipos() {
    $.ajax({
        url: '/api/TipoServicioAdicional',
        method: 'GET',
        success: function (data) {
            llenarTablaTipos(data);
        },
        error: function (xhr) {
            mostrarMensaje('Error al cargar los tipos de servicio. Código: ' + xhr.status, false);
        }
    });
}

function llenarTablaTipos(tipos) {
    let tbody = $('#tablaTipoServicioAdicional tbody');
    tbody.empty();
    if (tipos && tipos.length > 0) {
        tipos.forEach(function (tipo) {
            tbody.append(`
                <tr>
                    <td>${tipo.id_tipo_servicio_adicional}</td>
                    <td>${tipo.nombre_servicio || ''}</td>
                    <td>${tipo.descripcion || ''}</td>
                    <td>${tipo.precio_base != null ? tipo.precio_base : ''}</td>
                    <td>${tipo.activo ? 'Sí' : 'No'}</td>
                    <td>
                        <button class="btn btn-sm btn-primary btn-editar" data-id="${tipo.id_tipo_servicio_adicional}">Editar</button>
                        <button class="btn btn-sm btn-danger btn-eliminar" data-id="${tipo.id_tipo_servicio_adicional}">Eliminar</button>
                    </td>
                </tr>
            `);
        });
    } else {
        tbody.append('<tr><td colspan="6" class="text-center">No hay tipos registrados.</td></tr>');
    }
}

function insertarTipo(tipo) {
    $.ajax({
        url: '/api/TipoServicioAdicional',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(tipo),
        success: function (msg) {
            mostrarMensaje(msg, true);
            limpiarFormulario();
            cargarTipos();
        },
        error: function (xhr) {
            mostrarMensaje(xhr.responseText || 'Error al insertar el tipo.', false);
        }
    });
}

function consultarTipo(id) {
    $.ajax({
        url: '/api/TipoServicioAdicional/' + id,
        method: 'GET',
        success: function (tipo) {
            if (tipo) {
                $('#id_tipo_servicio_adicional').val(tipo.id_tipo_servicio_adicional);
                $('#nombre_servicio').val(tipo.nombre_servicio);
                $('#descripcion').val(tipo.descripcion);
                $('#precio_base').val(tipo.precio_base);
                $('#activo').val(tipo.activo ? "true" : "false");
                $('#btnGuardarTipo').text('Actualizar');
                $('#btnCancelarTipo').removeClass('d-none');
            }
        },
        error: function (xhr) {
            mostrarMensaje('Error al consultar el tipo. Código: ' + xhr.status, false);
        }
    });
}

function actualizarTipo(tipo) {
    $.ajax({
        url: '/api/TipoServicioAdicional',
        method: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(tipo),
        success: function (msg) {
            mostrarMensaje(msg, true);
            limpiarFormulario();
            cargarTipos();
        },
        error: function (xhr) {
            mostrarMensaje(xhr.responseText || 'Error al actualizar el tipo.', false);
        }
    });
}

function eliminarTipo(id) {
    $.ajax({
        url: '/api/TipoServicioAdicional/' + id,
        method: 'DELETE',
        success: function (msg) {
            mostrarMensaje(msg, true);
            cargarTipos();
        },
        error: function (xhr) {
            mostrarMensaje(xhr.responseText || 'Error al eliminar el tipo.', false);
        }
    });
}

function mostrarMensaje(msg, esExito) {
    let div = $('#mensajeTipoServicioAdicional');
    div.html(`<div class="alert ${esExito ? 'alert-success' : 'alert-danger'}">${msg}</div>`);
    setTimeout(() => div.empty(), 3500);
}