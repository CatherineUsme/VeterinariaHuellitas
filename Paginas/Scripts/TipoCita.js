$(document).ready(function () {
    listarTipoCitas();

    $('#formTipoCita').on('submit', function (e) {
        e.preventDefault();
        guardarTipoCita();
    });

    $('#btnCancelar').on('click', function () {
        limpiarFormulario();
    });
});

function listarTipoCitas() {
    $.getJSON('/api/TipoCita', function (data) {
        var $tbody = $('#tablaTipoCita tbody');
        $tbody.empty();
        $.each(data, function (i, tipo) {
            $tbody.append('<tr>' +
                '<td>' + tipo.id_tipo_cita + '</td>' +
                '<td>' + tipo.nombre_tipo_cita + '</td>' +
                '<td>' + (tipo.duracion_estimada_minutos || '') + '</td>' +
                '<td>' +
                '<button class="btn btn-sm btn-warning me-1" onclick="editarTipoCita(' + tipo.id_tipo_cita + ')">Editar</button>' +
                '<button class="btn btn-sm btn-danger" onclick="eliminarTipoCita(' + tipo.id_tipo_cita + ')">Eliminar</button>' +
                '</td>' +
                '</tr>');
        });
    });
}

function guardarTipoCita() {
    var tipoCita = {
        id_tipo_cita: $('#id_tipo_cita').val() || 0,
        nombre_tipo_cita: $('#nombre_tipo_cita').val(),
        duracion_estimada_minutos: parseInt($('#duracion_estimada_minutos').val())
    };

    var esNuevo = tipoCita.id_tipo_cita == 0 || tipoCita.id_tipo_cita === "";

    var url = '/api/TipoCita';
    var tipo = esNuevo ? 'POST' : 'PUT';

    $.ajax({
        url: url,
        type: tipo,
        contentType: 'application/json',
        data: JSON.stringify(tipoCita),
        success: function (resp) {
            $('#mensajeTipoCita').html('<div class="alert alert-success">' + resp + '</div>');
            limpiarFormulario();
            listarTipoCitas();
        },
        error: function () {
            $('#mensajeTipoCita').html('<div class="alert alert-danger">Error al guardar el tipo de cita.</div>');
        }
    });
}

function editarTipoCita(id) {
    $.getJSON('/api/TipoCita/' + id, function (tipo) {
        $('#id_tipo_cita').val(tipo.id_tipo_cita);
        $('#nombre_tipo_cita').val(tipo.nombre_tipo_cita);
        $('#duracion_estimada_minutos').val(tipo.duracion_estimada_minutos);
        $('#btnGuardar').text('Actualizar Tipo de Cita');
        $('#btnCancelar').removeClass('d-none');
    });
}

function eliminarTipoCita(id) {
    if (!confirm('¿Está seguro de eliminar este tipo de cita?')) return;
    $.ajax({
        url: '/api/TipoCita/' + id,
        type: 'DELETE',
        success: function (resp) {
            $('#mensajeTipoCita').html('<div class="alert alert-success">' + resp + '</div>');
            listarTipoCitas();
        },
        error: function () {
            $('#mensajeTipoCita').html('<div class="alert alert-danger">Error al eliminar el tipo de cita.</div>');
        }
    });
}

function limpiarFormulario() {
    $('#formTipoCita')[0].reset();
    $('#id_tipo_cita').val('');
    $('#btnGuardar').text('Guardar Tipo de Cita');
    $('#btnCancelar').addClass('d-none');
    $('#mensajeTipoCita').empty();
}