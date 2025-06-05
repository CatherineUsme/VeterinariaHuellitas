$(document).ready(function () {
    cargarCombos();
    cargarServicios();

    $('#formServicioAdicional').on('submit', function (e) {
        e.preventDefault();
        let servicio = obtenerDatosFormulario();
        let esEdicion = !!servicio.id_servicio_prestado;
        if (esEdicion) {
            actualizarServicio(servicio);
        } else {
            insertarServicio(servicio);
        }
    });

    $('#btnCancelarServicio').on('click', function () {
        limpiarFormulario();
    });

    $('#tablaServicioAdicional tbody').on('click', '.btn-editar', function () {
        let id = $(this).data('id');
        consultarServicio(id);
    });

    $('#tablaServicioAdicional tbody').on('click', '.btn-eliminar', function () {
        let id = $(this).data('id');
        if (confirm('¿Está seguro de eliminar este servicio?')) {
            eliminarServicio(id);
        }
    });
});

function cargarCombos() {
    // Mascotas
    $.get('/api/mascota/ConsultarTodos', function (data) {
        let sel = $('#id_mascota');
        sel.empty().append('<option value="">Seleccione una mascota</option>');
        data.forEach(function (m) {
            // Ajusta 'nombre' si tu modelo usa otro campo
            sel.append(`<option value="${m.id_mascota}">${m.nombre}</option>`);
        });
    });

    // Tipos de servicio adicional
    $.get('/api/TipoServicioAdicional', function (data) {
        let sel = $('#id_tipo_servicio_adicional');
        sel.empty().append('<option value="">Seleccione un tipo</option>');
        data.forEach(function (t) {
            // Ajusta 'nombre_servicio' si tu modelo usa otro campo
            sel.append(`<option value="${t.id_tipo_servicio_adicional}">${t.nombre_servicio}</option>`);
        });
    });

    // Empleados
    $.get('/api/empleado/ConsultarTodos', function (data) {
        let sel = $('#id_empleado_realiza');
        sel.empty().append('<option value="">Seleccione un empleado</option>');
        data.forEach(function (e) {
            // Ajusta 'nombre' y 'apellido' si tu modelo usa otros campos
            sel.append(`<option value="${e.id_empleado}">${e.nombre} ${e.apellido}</option>`);
        });
    });

    // Sedes
    $.get('/api/sede/ConsultarTodos', function (data) {
        let sel = $('#id_sede');
        sel.empty().append('<option value="">Seleccione una sede</option>');
        data.forEach(function (s) {
            // Ajusta 'nombre_sede' si tu modelo usa otro campo
            sel.append(`<option value="${s.id_sede}">${s.nombre_sede}</option>`);
        });
    });
}

function obtenerDatosFormulario() {
    return {
        id_servicio_prestado: $('#id_servicio_prestado').val(),
        id_mascota: $('#id_mascota').val(),
        id_tipo_servicio_adicional: $('#id_tipo_servicio_adicional').val(),
        id_empleado_realiza: $('#id_empleado_realiza').val(),
        id_sede: $('#id_sede').val(),
        fecha_servicio: $('#fecha_servicio').val(),
        estado: $('#estado').val(),
        costo_final: $('#costo_final').val()
    };
}

function limpiarFormulario() {
    $('#formServicioAdicional')[0].reset();
    $('#id_servicio_prestado').val('');
    $('#btnGuardarServicio').text('Guardar');
    $('#btnCancelarServicio').addClass('d-none');
}

function cargarServicios() {
    $.ajax({
        url: '/api/servicio-adicional',
        method: 'GET',
        success: function (data) {
            llenarTablaServicios(data);
        },
        error: function (xhr) {
            mostrarMensaje('Error al cargar los servicios. Código: ' + xhr.status, false);
        }
    });
}

function llenarTablaServicios(servicios) {
    let tbody = $('#tablaServicioAdicional tbody');
    tbody.empty();
    if (servicios && servicios.length > 0) {
        servicios.forEach(function (s) {
            tbody.append(`
                <tr>
                    <td>${s.id_servicio_prestado}</td>
                    <td>${s.MASCOTA ? s.MASCOTA.nombre : ''}</td>
                    <td>${s.TIPO_SERVICIO_ADICIONAL ? s.TIPO_SERVICIO_ADICIONAL.nombre_servicio : ''}</td>
                    <td>${s.EMPLEADO ? (s.EMPLEADO.nombre + ' ' + s.EMPLEADO.apellido) : ''}</td>
                    <td>${s.SEDE ? s.SEDE.nombre_sede : ''}</td>
                    <td>${s.fecha_servicio ? s.fecha_servicio.substring(0, 10) : ''}</td>
                    <td>${s.estado || ''}</td>
                    <td>${s.costo_final != null ? s.costo_final : ''}</td>
                    <td>
                        <button class="btn btn-sm btn-primary btn-editar" data-id="${s.id_servicio_prestado}">Editar</button>
                        <button class="btn btn-sm btn-danger btn-eliminar" data-id="${s.id_servicio_prestado}">Eliminar</button>
                    </td>
                </tr>
            `);
        });
    } else {
        tbody.append('<tr><td colspan="9" class="text-center">No hay servicios registrados.</td></tr>');
    }
}

function insertarServicio(servicio) {
    $.ajax({
        url: '/api/servicio-adicional',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(servicio),
        success: function (msg) {
            mostrarMensaje(msg, true);
            limpiarFormulario();
            cargarServicios();
        },
        error: function (xhr) {
            mostrarMensaje(xhr.responseText || 'Error al insertar el servicio.', false);
        }
    });
}

function consultarServicio(id) {
    $.ajax({
        url: '/api/servicio-adicional/' + id,
        method: 'GET',
        success: function (s) {
            if (s) {
                $('#id_servicio_prestado').val(s.id_servicio_prestado);
                $('#id_mascota').val(s.id_mascota);
                $('#id_tipo_servicio_adicional').val(s.id_tipo_servicio_adicional);
                $('#id_empleado_realiza').val(s.id_empleado_realiza);
                $('#id_sede').val(s.id_sede);
                $('#fecha_servicio').val(s.fecha_servicio ? s.fecha_servicio.substring(0, 10) : '');
                $('#estado').val(s.estado);
                $('#costo_final').val(s.costo_final);
                $('#btnGuardarServicio').text('Actualizar');
                $('#btnCancelarServicio').removeClass('d-none');
            }
        },
        error: function (xhr) {
            mostrarMensaje('Error al consultar el servicio. Código: ' + xhr.status, false);
        }
    });
}

function actualizarServicio(servicio) {
    $.ajax({
        url: '/api/servicio-adicional',
        method: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(servicio),
        success: function (msg) {
            mostrarMensaje(msg, true);
            limpiarFormulario();
            cargarServicios();
        },
        error: function (xhr) {
            mostrarMensaje(xhr.responseText || 'Error al actualizar el servicio.', false);
        }
    });
}

function eliminarServicio(id) {
    $.ajax({
        url: '/api/servicio-adicional/' + id,
        method: 'DELETE',
        success: function (msg) {
            mostrarMensaje(msg, true);
            cargarServicios();
        },
        error: function (xhr) {
            mostrarMensaje(xhr.responseText || 'Error al eliminar el servicio.', false);
        }
    });
}

function mostrarMensaje(msg, esExito) {
    let div = $('#mensajeServicioAdicional');
    div.html(`<div class="alert ${esExito ? 'alert-success' : 'alert-danger'}">${msg}</div>`);
    setTimeout(() => div.empty(), 3500);
}