$(document).ready(function () {
    cargarCargos();
    cargarSedes();
    listarEmpleados();

    $('#formEmpleado').on('submit', function (e) {
        e.preventDefault();
        guardarEmpleado();
    });

    $('#btnCancelar').on('click', function () {
        limpiarFormulario();
    });
});

function cargarCargos() {
    $.getJSON('/api/CargoEmpleado/ConsultarTodos', function (data) {
        var $cargo = $('#id_cargo_empleado');
        $cargo.empty().append('<option value="">Seleccione un cargo</option>');
        $.each(data, function (i, c) {
            $cargo.append('<option value="' + c.id_cargo_empleado + '">' + c.nombre_cargo + '</option>');
        });
    });
}

function cargarSedes() {
    $.getJSON('/api/Sede/ConsultarTodos', function (data) {
        var $sede = $('#id_sede_trabajo');
        $sede.empty().append('<option value="">Seleccione una sede</option>');
        $.each(data, function (i, s) {
            $sede.append('<option value="' + s.id_sede + '">' + s.nombre_sede + '</option>');
        });
    });
}

function listarEmpleados() {
    $.getJSON('/api/Empleado/ConsultarTodos', function (data) {
        var $tbody = $('#tablaEmpleados tbody');
        $tbody.empty();
        $.each(data, function (i, emp) {
            $tbody.append('<tr>' +
                '<td>' + emp.id_empleado + '</td>' +
                '<td>' + emp.nombre + '</td>' +
                '<td>' + emp.apellido + '</td>' +
                '<td>' + emp.cedula + '</td>' +
                '<td>' + emp.email + '</td>' +
                '<td>' + emp.telefono + '</td>' +
                '<td>' + (emp.fecha_ingreso ? emp.fecha_ingreso.substring(0, 10) : '') + '</td>' +
                '<td>' + (emp.CARGO_EMPLEADO ? emp.CARGO_EMPLEADO.nombre_cargo : emp.id_cargo_empleado) + '</td>' +
                '<td>' + (emp.SEDE ? emp.SEDE.nombre_sede : emp.id_sede_trabajo) + '</td>' +
                '<td>' + (emp.activo ? 'Sí' : 'No') + '</td>' +
                '<td>' +
                '<button class="btn btn-sm btn-warning me-1" onclick="editarEmpleado(' + emp.id_empleado + ')">Editar</button>' +
                '<button class="btn btn-sm btn-danger" onclick="eliminarEmpleado(' + emp.id_empleado + ')">Eliminar</button>' +
                '</td>' +
                '</tr>');
        });
    });
}

function guardarEmpleado() {
    var empleado = {
        id_empleado: $('#id_empleado').val() || 0,
        nombre: $('#nombre').val(),
        apellido: $('#apellido').val(),
        cedula: $('#cedula').val(),
        email: $('#email').val(),
        telefono: $('#telefono').val(),
        fecha_ingreso: $('#fecha_ingreso').val(),
        id_cargo_empleado: parseInt($('#id_cargo_empleado').val()),
        id_sede_trabajo: parseInt($('#id_sede_trabajo').val()),
        activo: $('#activo').val() === "true"
    };

    var esNuevo = empleado.id_empleado == 0 || empleado.id_empleado === "";

    var url = esNuevo ? '/api/Empleado/Insertar' : '/api/Empleado/Actualizar';
    var tipo = esNuevo ? 'POST' : 'PUT';

    $.ajax({
        url: url,
        type: tipo,
        contentType: 'application/json',
        data: JSON.stringify(empleado),
        success: function (resp) {
            $('#mensajeEmpleado').html('<div class="alert alert-success">' + resp + '</div>');
            limpiarFormulario();
            listarEmpleados();
        },
        error: function () {
            $('#mensajeEmpleado').html('<div class="alert alert-danger">Error al guardar el empleado.</div>');
        }
    });
}

function editarEmpleado(id) {
    $.getJSON('/api/Empleado/Consultar', { id_empleado: id }, function (emp) {
        $('#id_empleado').val(emp.id_empleado);
        $('#nombre').val(emp.nombre);
        $('#apellido').val(emp.apellido);
        $('#cedula').val(emp.cedula);
        $('#email').val(emp.email);
        $('#telefono').val(emp.telefono);
        $('#fecha_ingreso').val(emp.fecha_ingreso ? emp.fecha_ingreso.substring(0, 10) : '');
        $('#id_cargo_empleado').val(emp.id_cargo_empleado);
        $('#id_sede_trabajo').val(emp.id_sede_trabajo);
        $('#activo').val(emp.activo ? "true" : "false");
        $('#btnGuardar').text('Actualizar Empleado');
        $('#btnCancelar').removeClass('d-none');
    });
}

function eliminarEmpleado(id) {
    if (!confirm('¿Está seguro de eliminar este empleado?')) return;
    $.ajax({
        url: '/api/Empleado/Eliminar?id_empleado=' + id,
        type: 'DELETE',
        success: function (resp) {
            $('#mensajeEmpleado').html('<div class="alert alert-success">' + resp + '</div>');
            listarEmpleados();
        },
        error: function () {
            $('#mensajeEmpleado').html('<div class="alert alert-danger">Error al eliminar el empleado.</div>');
        }
    });
}

function limpiarFormulario() {
    $('#formEmpleado')[0].reset();
    $('#id_empleado').val('');
    $('#btnGuardar').text('Guardar Empleado');
    $('#btnCancelar').addClass('d-none');
    $('#mensajeEmpleado').empty();
}