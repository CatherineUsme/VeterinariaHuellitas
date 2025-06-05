$(document).ready(function () {
    listarTipoCirugias();

    $('#formTipoCirugia').on('submit', function (e) {
        e.preventDefault();
        guardarTipoCirugia();
    });

    $('#btnCancelar').on('click', function () {
        limpiarFormulario();
    });
});

function listarTipoCirugias() {
    $.getJSON('/api/TipoCirugia', function (data) {
        var $tbody = $('#tablaTipoCirugia tbody');
        $tbody.empty();
        $.each(data, function (i, tipo) {
            $tbody.append('<tr>' +
                '<td>' + tipo.id_tipo_cirugia + '</td>' +
                '<td>' + tipo.nombre_tipo_cirugia + '</td>' +
                '<td>' + (tipo.descripcion || '') + '</td>' +
                '<td>' +
                '<button class="btn btn-sm btn-warning me-1" onclick="editarTipoCirugia(' + tipo.id_tipo_cirugia + ')">Editar</button>' +
                '<button class="btn btn-sm btn-danger" onclick="eliminarTipoCirugia(' + tipo.id_tipo_cirugia + ')">Eliminar</button>' +
                '</td>' +
                '</tr>');
        });
    }).fail(function (jqXHR) {
        $('#mensajeTipoCirugia').html('<div class="alert alert-danger">No se pudo obtener la información. ' + (jqXHR.responseText || '') + '</div>');
    });
}

function guardarTipoCirugia() {
    var tipoCirugia = {
        id_tipo_cirugia: $('#id_tipo_cirugia').val() || 0,
        nombre_tipo_cirugia: $('#nombre_tipo_cirugia').val(),
        descripcion: $('#descripcion').val()
    };

    var esNuevo = tipoCirugia.id_tipo_cirugia == 0 || tipoCirugia.id_tipo_cirugia === "";

    var url = '/api/TipoCirugia';
    var tipo = esNuevo ? 'POST' : 'PUT';

    $.ajax({
        url: url,
        type: tipo,
        contentType: 'application/json',
        data: JSON.stringify(tipoCirugia),
        success: function (resp) {
            $('#mensajeTipoCirugia').html('<div class="alert alert-success">' + resp + '</div>');
            limpiarFormulario();
            listarTipoCirugias();
        },
        error: function (xhr) {
            let msg = "Error al guardar el tipo de cirugía.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeTipoCirugia').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function editarTipoCirugia(id) {
    $.getJSON('/api/TipoCirugia/' + id, function (tipo) {
        $('#id_tipo_cirugia').val(tipo.id_tipo_cirugia);
        $('#nombre_tipo_cirugia').val(tipo.nombre_tipo_cirugia);
        $('#descripcion').val(tipo.descripcion);
        $('#btnGuardar').text('Actualizar Tipo de Cirugía');
        $('#btnCancelar').removeClass('d-none');
    });
}

function eliminarTipoCirugia(id) {
    if (!confirm('¿Está seguro de eliminar este tipo de cirugía?')) return;
    $.ajax({
        url: '/api/TipoCirugia/' + id,
        type: 'DELETE',
        success: function (resp) {
            $('#mensajeTipoCirugia').html('<div class="alert alert-success">' + resp + '</div>');
            listarTipoCirugias();
        },
        error: function (xhr) {
            let msg = "Error al eliminar el tipo de cirugía.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeTipoCirugia').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function limpiarFormulario() {
    $('#formTipoCirugia')[0].reset();
    $('#id_tipo_cirugia').val('');
    $('#btnGuardar').text('Guardar Tipo de Cirugía');
    $('#btnCancelar').addClass('d-none');
    $('#mensajeTipoCirugia').empty();
}