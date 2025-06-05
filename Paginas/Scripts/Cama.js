$(document).ready(function () {
    cargarSedes();
    listarCamas();

    $('#formCama').on('submit', function (e) {
        e.preventDefault();
        guardarCama();
    });

    $('#btnCancelar').on('click', function () {
        limpiarFormulario();
    });
});

function cargarSedes() {
    // Llama a tu API de sedes, ajusta la URL si es necesario
    $.getJSON('/api/Sede/ConsultarTodos', function (data) {
        var $sede = $('#id_sede');
        $sede.empty().append('<option value="">Seleccione una sede</option>');
        $.each(data, function (i, sede) {
            $sede.append('<option value="' + sede.id_sede + '">' + sede.nombre_sede + '</option>');
        });
    });
}

function listarCamas() {
    $.getJSON('/api/Cama/ConsultarTodos', function (data) {
        var $tbody = $('#tablaCamas tbody');
        $tbody.empty();
        $.each(data, function (i, cama) {
            $tbody.append('<tr>' +
                '<td>' + cama.id_cama + '</td>' +
                '<td>' + (cama.SEDE ? cama.SEDE.nombre_sede : cama.id_sede) + '</td>' +
                '<td>' + cama.numero_identificador + '</td>' +
                '<td>' + cama.tipo_cama + '</td>' +
                '<td>' + cama.estado + '</td>' +
                '<td>' +
                '<button class="btn btn-sm btn-warning me-1" onclick="editarCama(' + cama.id_cama + ')">Editar</button>' +
                '<button class="btn btn-sm btn-danger" onclick="eliminarCama(' + cama.id_cama + ')">Eliminar</button>' +
                '</td>' +
                '</tr>');
        });
    });
}

function guardarCama() {
    var cama = {
        id_cama: $('#id_cama').val() || 0,
        id_sede: parseInt($('#id_sede').val()),
        numero_identificador: $('#numero_identificador').val(),
        tipo_cama: $('#tipo_cama').val(),
        estado: $('#estado').val()
    };

    var esNuevo = cama.id_cama == 0;

    var url = esNuevo ? '/api/Cama/Insertar' : '/api/Cama/Actualizar';
    var tipo = esNuevo ? 'POST' : 'PUT';

    $.ajax({
        url: url,
        type: tipo,
        contentType: 'application/json',
        data: JSON.stringify(cama),
        success: function (resp) {
            $('#mensajeCama').html('<div class="alert alert-success">' + resp + '</div>');
            limpiarFormulario();
            listarCamas();
        },
        error: function () {
            $('#mensajeCama').html('<div class="alert alert-danger">Error al guardar la cama.</div>');
        }
    });
}

function editarCama(id) {
    $.getJSON('/api/Cama/Consultar', { id_cama: id }, function (cama) {
        $('#id_cama').val(cama.id_cama);
        $('#id_sede').val(cama.id_sede);
        $('#numero_identificador').val(cama.numero_identificador);
        $('#tipo_cama').val(cama.tipo_cama);
        $('#estado').val(cama.estado);
        $('#btnGuardar').text('Actualizar Cama');
        $('#btnCancelar').removeClass('d-none');
    });
}

function eliminarCama(id) {
    if (!confirm('¿Está seguro de eliminar esta cama?')) return;
    $.ajax({
        url: '/api/Cama/Eliminar?id_cama=' + id,
        type: 'DELETE',
        success: function (resp) {
            $('#mensajeCama').html('<div class="alert alert-success">' + resp + '</div>');
            listarCamas();
        },
        error: function () {
            $('#mensajeCama').html('<div class="alert alert-danger">Error al eliminar la cama.</div>');
        }
    });
}

function limpiarFormulario() {
    $('#formCama')[0].reset();
    $('#id_cama').val('');
    $('#btnGuardar').text('Guardar Cama');
    $('#btnCancelar').addClass('d-none');
    $('#mensajeCama').empty();
}