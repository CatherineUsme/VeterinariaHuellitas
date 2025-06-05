$(document).ready(function () {
    listarSedes();

    $('#formSede').on('submit', function (e) {
        e.preventDefault();
        guardarSede();
    });

    $('#btnCancelar').on('click', function () {
        limpiarFormulario();
    });
});

function listarSedes() {
    $.getJSON('/api/Sede/ConsultarTodos', function (data) {
        var $tbody = $('#tablaSedes tbody');
        $tbody.empty();
        $.each(data, function (i, sede) {
            $tbody.append('<tr>' +
                '<td>' + sede.id_sede + '</td>' +
                '<td>' + sede.nombre_sede + '</td>' +
                '<td>' + sede.direccion + '</td>' +
                '<td>' + sede.telefono + '</td>' +
                '<td>' + sede.ciudad + '</td>' +
                '<td>' + (sede.activa ? 'Sí' : 'No') + '</td>' +
                '<td>' +
                '<button class="btn btn-sm btn-warning me-1" onclick="editarSede(' + sede.id_sede + ')">Editar</button>' +
                '<button class="btn btn-sm btn-danger" onclick="eliminarSede(' + sede.id_sede + ')">Eliminar</button>' +
                '</td>' +
                '</tr>');
        });
    });
}

function guardarSede() {
    var sede = {
        id_sede: $('#id_sede').val() || 0,
        nombre_sede: $('#nombre_sede').val(),
        direccion: $('#direccion').val(),
        telefono: $('#telefono').val(),
        ciudad: $('#ciudad').val(),
        activa: $('#activa').val() === "true"
    };

    var esNuevo = sede.id_sede == 0 || sede.id_sede === "";

    var url = esNuevo ? '/api/Sede/Insertar' : '/api/Sede/Actualizar';
    var tipo = esNuevo ? 'POST' : 'PUT';

    $.ajax({
        url: url,
        type: tipo,
        contentType: 'application/json',
        data: JSON.stringify(sede),
        success: function (resp) {
            $('#mensajeSede').html('<div class="alert alert-success">' + resp + '</div>');
            limpiarFormulario();
            listarSedes();
        },
        error: function () {
            $('#mensajeSede').html('<div class="alert alert-danger">Error al guardar la sede.</div>');
        }
    });
}

function editarSede(id) {
    $.getJSON('/api/Sede/Consultar', { id_sede: id }, function (sede) {
        $('#id_sede').val(sede.id_sede);
        $('#nombre_sede').val(sede.nombre_sede);
        $('#direccion').val(sede.direccion);
        $('#telefono').val(sede.telefono);
        $('#ciudad').val(sede.ciudad);
        $('#activa').val(sede.activa ? "true" : "false");
        $('#btnGuardar').text('Actualizar Sede');
        $('#btnCancelar').removeClass('d-none');
    });
}

function eliminarSede(id) {
    if (!confirm('¿Está seguro de eliminar esta sede?')) return;
    $.ajax({
        url: '/api/Sede/Eliminar?id_sede=' + id,
        type: 'DELETE',
        success: function (resp) {
            $('#mensajeSede').html('<div class="alert alert-success">' + resp + '</div>');
            listarSedes();
        },
        error: function () {
            $('#mensajeSede').html('<div class="alert alert-danger">Error al eliminar la sede.</div>');
        }
    });
}

function limpiarFormulario() {
    $('#formSede')[0].reset();
    $('#id_sede').val('');
    $('#btnGuardar').text('Guardar Sede');
    $('#btnCancelar').addClass('d-none');
    $('#mensajeSede').empty();
}