$(document).ready(function () {
    cargarTiposProducto();
    listarProductos();

    $('#formProducto').on('submit', function (e) {
        e.preventDefault();
        guardarProducto();
    });

    $('#btnCancelar').on('click', function () {
        limpiarFormulario();
    });
});

function cargarTiposProducto() {
    // Ajusta la URL si tu endpoint es diferente
    $.getJSON('/api/TipoProducto', function (data) {
        var $tipo = $('#id_tipo_producto');
        $tipo.empty().append('<option value="">Seleccione un tipo</option>');
        $.each(data, function (i, tipo) {
            $tipo.append('<option value="' + tipo.id_tipo_producto + '">' + tipo.nombre_tipo + '</option>');
        });
    });
}

function listarProductos() {
    $.getJSON('/api/Producto', function (data) {
        var $tbody = $('#tablaProductos tbody');
        $tbody.empty();
        $.each(data, function (i, prod) {
            $tbody.append('<tr>' +
                '<td>' + prod.id_producto + '</td>' +
                '<td>' + prod.nombre + '</td>' +
                '<td>' + (prod.descripcion || '') + '</td>' +
                '<td>' + (prod.TIPO_PRODUCTO ? prod.TIPO_PRODUCTO.nombre_tipo : prod.id_tipo_producto) + '</td>' +
                '<td>' + (prod.codigo_barras || '') + '</td>' +
                '<td>' + (prod.precio_venta_sugerido != null ? prod.precio_venta_sugerido.toFixed(2) : '') + '</td>' +
                '<td>' + (prod.requiere_receta ? 'Sí' : 'No') + '</td>' +
                '<td>' + (prod.activo ? 'Sí' : 'No') + '</td>' +
                '<td>' +
                '<button class="btn btn-sm btn-warning me-1" onclick="editarProducto(' + prod.id_producto + ')">Editar</button>' +
                '<button class="btn btn-sm btn-danger" onclick="eliminarProducto(' + prod.id_producto + ')">Eliminar</button>' +
                '</td>' +
                '</tr>');
        });
    });
}

function guardarProducto() {
    var producto = {
        id_producto: $('#id_producto').val() || 0,
        nombre: $('#nombre').val(),
        descripcion: $('#descripcion').val(),
        id_tipo_producto: parseInt($('#id_tipo_producto').val()),
        codigo_barras: $('#codigo_barras').val(),
        precio_venta_sugerido: $('#precio_venta_sugerido').val() ? parseFloat($('#precio_venta_sugerido').val()) : null,
        requiere_receta: $('#requiere_receta').val() === "true",
        activo: $('#activo').val() === "true"
    };

    var esNuevo = producto.id_producto == 0 || producto.id_producto === "";

    var url = '/api/Producto';
    var tipo = esNuevo ? 'POST' : 'PUT';

    $.ajax({
        url: url,
        type: tipo,
        contentType: 'application/json',
        data: JSON.stringify(producto),
        success: function (resp) {
            $('#mensajeProducto').html('<div class="alert alert-success">' + resp + '</div>');
            limpiarFormulario();
            listarProductos();
        },
        error: function (xhr) {
            let msg = "Error al guardar el producto.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeProducto').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function editarProducto(id) {
    $.getJSON('/api/Producto/' + id, function (prod) {
        $('#id_producto').val(prod.id_producto);
        $('#nombre').val(prod.nombre);
        $('#descripcion').val(prod.descripcion);
        $('#id_tipo_producto').val(prod.id_tipo_producto);
        $('#codigo_barras').val(prod.codigo_barras);
        $('#precio_venta_sugerido').val(prod.precio_venta_sugerido);
        $('#requiere_receta').val(prod.requiere_receta ? "true" : "false");
        $('#activo').val(prod.activo ? "true" : "false");
        $('#btnGuardar').text('Actualizar Producto');
        $('#btnCancelar').removeClass('d-none');
    });
}

function eliminarProducto(id) {
    if (!confirm('¿Está seguro de eliminar este producto?')) return;
    $.ajax({
        url: '/api/Producto/' + id,
        type: 'DELETE',
        success: function (resp) {
            $('#mensajeProducto').html('<div class="alert alert-success">' + resp + '</div>');
            listarProductos();
        },
        error: function (xhr) {
            let msg = "Error al eliminar el producto.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeProducto').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function limpiarFormulario() {
    $('#formProducto')[0].reset();
    $('#id_producto').val('');
    $('#btnGuardar').text('Guardar Producto');
    $('#btnCancelar').addClass('d-none');
    $('#mensajeProducto').empty();
}