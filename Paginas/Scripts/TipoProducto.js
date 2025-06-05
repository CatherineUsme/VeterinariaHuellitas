$(document).ready(function () {
    listarTipoProductos();

    $('#formTipoProducto').on('submit', function (e) {
        e.preventDefault();
        guardarTipoProducto();
    });

    $('#btnCancelar').on('click', function () {
        limpiarFormulario();
    });
});

function listarTipoProductos() {
    $.getJSON('/api/TipoProducto', function (data) {
        var $tbody = $('#tablaTipoProducto tbody');
        $tbody.empty();
        $.each(data, function (i, tipo) {
            $tbody.append('<tr>' +
                '<td>' + tipo.id_tipo_producto + '</td>' +
                '<td>' + tipo.nombre_tipo + '</td>' +
                '<td>' + (tipo.descripcion || '') + '</td>' +
                '<td>' +
                '<button class="btn btn-sm btn-warning me-1" onclick="editarTipoProducto(' + tipo.id_tipo_producto + ')">Editar</button>' +
                '<button class="btn btn-sm btn-danger" onclick="eliminarTipoProducto(' + tipo.id_tipo_producto + ')">Eliminar</button>' +
                '</td>' +
                '</tr>');
        });
    }).fail(function (jqXHR) {
        $('#mensajeTipoProducto').html('<div class="alert alert-danger">No se pudo obtener la información. ' + (jqXHR.responseText || '') + '</div>');
    });
}

function guardarTipoProducto() {
    var tipoProducto = {
        id_tipo_producto: $('#id_tipo_producto').val() || 0,
        nombre_tipo: $('#nombre_tipo').val(),
        descripcion: $('#descripcion').val()
    };

    var esNuevo = tipoProducto.id_tipo_producto == 0 || tipoProducto.id_tipo_producto === "";

    var url = '/api/TipoProducto';
    var tipo = esNuevo ? 'POST' : 'PUT';

    $.ajax({
        url: url,
        type: tipo,
        contentType: 'application/json',
        data: JSON.stringify(tipoProducto),
        success: function (resp) {
            $('#mensajeTipoProducto').html('<div class="alert alert-success">' + resp + '</div>');
            limpiarFormulario();
            listarTipoProductos();
        },
        error: function (xhr) {
            let msg = "Error al guardar el tipo de producto.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeTipoProducto').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function editarTipoProducto(id) {
    $.getJSON('/api/TipoProducto/' + id, function (tipo) {
        $('#id_tipo_producto').val(tipo.id_tipo_producto);
        $('#nombre_tipo').val(tipo.nombre_tipo);
        $('#descripcion').val(tipo.descripcion);
        $('#btnGuardar').text('Actualizar Tipo de Producto');
        $('#btnCancelar').removeClass('d-none');
    });
}

function eliminarTipoProducto(id) {
    if (!confirm('¿Está seguro de eliminar este tipo de producto?')) return;
    $.ajax({
        url: '/api/TipoProducto/' + id,
        type: 'DELETE',
        success: function (resp) {
            $('#mensajeTipoProducto').html('<div class="alert alert-success">' + resp + '</div>');
            listarTipoProductos();
        },
        error: function (xhr) {
            let msg = "Error al eliminar el tipo de producto.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeTipoProducto').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function limpiarFormulario() {
    $('#formTipoProducto')[0].reset();
    $('#id_tipo_producto').val('');
    $('#btnGuardar').text('Guardar Tipo de Producto');
    $('#btnCancelar').addClass('d-none');
    $('#mensajeTipoProducto').empty();
}