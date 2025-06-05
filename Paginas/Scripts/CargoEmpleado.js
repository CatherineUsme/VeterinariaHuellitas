$(document).ready(function () {
    cargarCargos();

    // Guardar o actualizar cargo
    $('#formCargoEmpleado').on('submit', function (e) {
        e.preventDefault();
        let cargo = obtenerDatosFormulario();
        let esEdicion = !!cargo.id_cargo_empleado;

        if (esEdicion) {
            actualizarCargo(cargo);
        } else {
            insertarCargo(cargo);
        }
    });

    // Cancelar edición
    $('#btnCancelarCargo').on('click', function () {
        limpiarFormulario();
    });

    // Delegar evento de editar
    $('#tablaCargoEmpleado tbody').on('click', '.btn-editar', function () {
        let id = $(this).data('id');
        consultarCargo(id);
    });
});

// Obtener datos del formulario
function obtenerDatosFormulario() {
    return {
        id_cargo_empleado: $('#id_cargo_empleado').val(),
        nombre_cargo: $('#nombre_cargo').val(),
        descripcion: $('#descripcion').val(),
        salario_base: $('#salario_base').val(),
        activo: $('#activo').val() === "true"
    };
}

// Limpiar formulario
function limpiarFormulario() {
    $('#formCargoEmpleado')[0].reset();
    $('#id_cargo_empleado').val('');
    $('#btnGuardarCargo').text('Guardar Cargo');
    $('#btnCancelarCargo').addClass('d-none');
}

// Cargar todos los cargos
function cargarCargos() {
    $.ajax({
        url: '/api/CargoEmpleado/ConsultarTodos',
        method: 'GET',
        success: function (data) {
            llenarTablaCargos(data);
        },
        error: function (xhr) {
            mostrarMensaje('Error al cargar los cargos. Código: ' + xhr.status, false);
        }
    });
}

// Llenar la tabla con los cargos
function llenarTablaCargos(cargos) {
    let tbody = $('#tablaCargoEmpleado tbody');
    tbody.empty();
    if (cargos && cargos.length > 0) {
        cargos.forEach(function (cargo) {
            tbody.append(`
                <tr>
                    <td>${cargo.id_cargo_empleado}</td>
                    <td>${cargo.nombre_cargo || ''}</td>
                    <td>${cargo.descripcion || ''}</td>
                    <td>${cargo.salario_base != null ? cargo.salario_base : ''}</td>
                    <td>${cargo.activo ? 'Sí' : 'No'}</td>
                    <td>
                        <button class="btn btn-sm btn-primary btn-editar" data-id="${cargo.id_cargo_empleado}">Editar</button>
                    </td>
                </tr>
            `);
        });
    } else {
        tbody.append('<tr><td colspan="6" class="text-center">No hay cargos registrados.</td></tr>');
    }
}

// Insertar nuevo cargo
function insertarCargo(cargo) {
    $.ajax({
        url: '/api/CargoEmpleado/Insertar',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(cargo),
        success: function (msg) {
            mostrarMensaje(msg, true);
            limpiarFormulario();
            cargarCargos();
        },
        error: function (xhr) {
            mostrarMensaje('Error al insertar el cargo. Código: ' + xhr.status, false);
        }
    });
}

// Consultar un cargo para editar
function consultarCargo(id) {
    $.ajax({
        url: '/api/CargoEmpleado/Consultar',
        method: 'GET',
        data: { id_cargo_empleado: id },
        success: function (cargo) {
            if (cargo) {
                $('#id_cargo_empleado').val(cargo.id_cargo_empleado);
                $('#nombre_cargo').val(cargo.nombre_cargo);
                $('#descripcion').val(cargo.descripcion);
                $('#salario_base').val(cargo.salario_base);
                $('#activo').val(cargo.activo ? "true" : "false");
                $('#btnGuardarCargo').text('Actualizar Cargo');
                $('#btnCancelarCargo').removeClass('d-none');
            }
        },
        error: function (xhr) {
            mostrarMensaje('Error al consultar el cargo. Código: ' + xhr.status, false);
        }
    });
}

// Actualizar cargo existente
function actualizarCargo(cargo) {
    $.ajax({
        url: '/api/CargoEmpleado/Actualizar',
        method: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(cargo),
        success: function (msg) {
            mostrarMensaje(msg, true);
            limpiarFormulario();
            cargarCargos();
        },
        error: function (xhr) {
            mostrarMensaje('Error al actualizar el cargo. Código: ' + xhr.status, false);
        }
    });
}

// Mostrar mensajes al usuario
function mostrarMensaje(msg, esExito) {
    let div = $('#mensajeCargoEmpleado');
    div.html(`<div class="alert ${esExito ? 'alert-success' : 'alert-danger'}">${msg}</div>`);
    setTimeout(() => div.empty(), 3500);
}