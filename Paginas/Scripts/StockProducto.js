$(document).ready(function () {
    cargarProductos();
    cargarSedes();
    listarStock();

    $('#formStockProducto').on('submit', function (e) {
        e.preventDefault();
        guardarStock();
    });

    $('#btnCancelar').on('click', function () {
        limpiarFormulario();
    });
});

function cargarProductos() {
    $.getJSON('/api/Producto', function (data) {
        var $prod = $('#id_producto');
        $prod.empty().append('<option value="">Seleccione un producto</option>');
        $.each(data, function (i, p) {
            $prod.append('<option value="' + p.id_producto + '">' + p.nombre + '</option>');
        });
    });
}

function cargarSedes() {
    $.getJSON('/api/Sede/ConsultarTodos', function (data) {
        var $sede = $('#id_sede');
        $sede.empty().append('<option value="">Seleccione una sede</option>');
        $.each(data, function (i, s) {
            $sede.append('<option value="' + s.id_sede + '">' + s.nombre_sede + '</option>');
        });
    });
}

function listarStock() {
    $.getJSON('/api/StockProducto', function (data) {
        var $tbody = $('#tablaStockProducto tbody');
        $tbody.empty();
        $.each(data, function (i, stock) {
            $tbody.append('<tr>' +
                '<td>' + stock.id_stock_producto_sede + '</td>' +
                '<td>' + (stock.PRODUCTO ? stock.PRODUCTO.nombre : stock.id_producto) + '</td>' +
                '<td>' + (stock.SEDE ? stock.SEDE.nombre_sede : stock.id_sede) + '</td>' +
                '<td>' + stock.cantidad_disponible + '</td>' +
                '<td>' + (stock.stock_minimo != null ? stock.stock_minimo : '') + '</td>' +
                '<td>' + (stock.lote || '') + '</td>' +
                '<td>' + (stock.fecha_vencimiento ? stock.fecha_vencimiento.substring(0, 10) : '') + '</td>' +
                '<td>' +
                '<button class="btn btn-sm btn-warning me-1" onclick="editarStock(' + stock.id_stock_producto_sede + ')">Editar</button>' +
                '<button class="btn btn-sm btn-danger" onclick="eliminarStock(' + stock.id_stock_producto_sede + ')">Eliminar</button>' +
                '</td>' +
                '</tr>');
        });
    });
}

function guardarStock() {
    var stock = {
        id_stock_producto_sede: $('#id_stock_producto_sede').val() || 0,
        id_producto: parseInt($('#id_producto').val()),
        id_sede: parseInt($('#id_sede').val()),
        cantidad_disponible: parseInt($('#cantidad_disponible').val()),
        stock_minimo: $('#stock_minimo').val() ? parseInt($('#stock_minimo').val()) : null,
        lote: $('#lote').val(),
        fecha_vencimiento: $('#fecha_vencimiento').val() || null
    };

    var esNuevo = stock.id_stock_producto_sede == 0 || stock.id_stock_producto_sede === "";

    var url = '/api/StockProducto';
    var tipo = esNuevo ? 'POST' : 'PUT';

    $.ajax({
        url: url,
        type: tipo,
        contentType: 'application/json',
        data: JSON.stringify(stock),
        success: function (resp) {
            $('#mensajeStockProducto').html('<div class="alert alert-success">' + resp + '</div>');
            limpiarFormulario();
            listarStock();
        },
        error: function (xhr) {
            let msg = "Error al guardar el stock.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeStockProducto').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function editarStock(id) {
    $.getJSON('/api/StockProducto/' + id, function (stock) {
        $('#id_stock_producto_sede').val(stock.id_stock_producto_sede);
        $('#id_producto').val(stock.id_producto);
        $('#id_sede').val(stock.id_sede);
        $('#cantidad_disponible').val(stock.cantidad_disponible);
        $('#stock_minimo').val(stock.stock_minimo);
        $('#lote').val(stock.lote);
        $('#fecha_vencimiento').val(stock.fecha_vencimiento ? stock.fecha_vencimiento.substring(0, 10) : '');
        $('#btnGuardar').text('Actualizar Stock');
        $('#btnCancelar').removeClass('d-none');
    });
}

function eliminarStock(id) {
    if (!confirm('¿Está seguro de eliminar este registro de stock?')) return;
    $.ajax({
        url: '/api/StockProducto/' + id,
        type: 'DELETE',
        success: function (resp) {
            $('#mensajeStockProducto').html('<div class="alert alert-success">' + resp + '</div>');
            listarStock();
        },
        error: function (xhr) {
            let msg = "Error al eliminar el stock.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeStockProducto').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function limpiarFormulario() {
    $('#formStockProducto')[0].reset();
    $('#id_stock_producto_sede').val('');
    $('#btnGuardar').text('Guardar Stock');
    $('#btnCancelar').addClass('d-none');
    $('#mensajeStockProducto').empty();
}