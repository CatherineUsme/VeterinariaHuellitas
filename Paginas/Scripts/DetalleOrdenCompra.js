$(document).ready(function () {
    cargarOrdenes();
    cargarProductos();
    listarDetalles();

    $('#formDetalleOrden').on('submit', function (e) {
        e.preventDefault();
        guardarDetalle();
    });

    $('#btnCancelar').on('click', function () {
        limpiarFormulario();
    });

    $('#cantidad_solicitada, #precio_unitario_compra').on('input', function () {
        calcularSubtotal();
    });

    $('#id_orden_compra').on('change', function () {
        listarDetalles();
    });
});

function cargarOrdenes() {
    $.getJSON('/api/OrdenCompra', function (data) {
        var $orden = $('#id_orden_compra');
        $orden.empty().append('<option value="">Seleccione una orden</option>');
        $.each(data, function (i, o) {
            $orden.append('<option value="' + o.id_orden_compra + '">#' + o.id_orden_compra + ' - ' + (o.PROVEEDOR ? o.PROVEEDOR.nombre_empresa : '') + '</option>');
        });
    });
}

function cargarProductos() {
    $.getJSON('/api/Producto', function (data) {
        var $prod = $('#id_producto');
        $prod.empty().append('<option value="">Seleccione un producto</option>');
        $.each(data, function (i, p) {
            $prod.append('<option value="' + p.id_producto + '">' + p.nombre + '</option>');
        });
    });
}

function listarDetalles() {
    var idOrden = $('#id_orden_compra').val();
    if (!idOrden) {
        $('#tablaDetalleOrden tbody').empty();
        return;
    }
    $.getJSON('/api/DetalleOrdenCompra/OrdenCompra/' + idOrden, function (data) {
        var $tbody = $('#tablaDetalleOrden tbody');
        $tbody.empty();
        $.each(data, function (i, det) {
            $tbody.append('<tr>' +
                '<td>' + det.id_detalle_orden_compra + '</td>' +
                '<td>' + det.id_orden_compra + '</td>' +
                '<td>' + (det.PRODUCTO ? det.PRODUCTO.nombre : det.id_producto) + '</td>' +
                '<td>' + det.cantidad_solicitada + '</td>' +
                '<td>' + det.precio_unitario_compra.toFixed(2) + '</td>' +
                '<td>' + (det.subtotal != null ? det.subtotal.toFixed(2) : '') + '</td>' +
                '<td>' +
                '<button class="btn btn-sm btn-warning me-1" onclick="editarDetalle(' + det.id_detalle_orden_compra + ')">Editar</button>' +
                '<button class="btn btn-sm btn-danger" onclick="eliminarDetalle(' + det.id_detalle_orden_compra + ')">Eliminar</button>' +
                '</td>' +
                '</tr>');
        });
    });
}

function guardarDetalle() {
    var detalle = {
        id_detalle_orden_compra: $('#id_detalle_orden_compra').val() || 0,
        id_orden_compra: parseInt($('#id_orden_compra').val()),
        id_producto: parseInt($('#id_producto').val()),
        cantidad_solicitada: parseInt($('#cantidad_solicitada').val()),
        precio_unitario_compra: parseFloat($('#precio_unitario_compra').val())
    };

    var esNuevo = detalle.id_detalle_orden_compra == 0 || detalle.id_detalle_orden_compra === "";

    var url = '/api/DetalleOrdenCompra';
    var tipo = esNuevo ? 'POST' : 'PUT';

    if (!detalle.id_orden_compra || !detalle.id_producto || !detalle.cantidad_solicitada || !detalle.precio_unitario_compra) {
        $('#mensajeDetalleOrden').html('<div class="alert alert-danger">Todos los campos son obligatorios.</div>');
        return;
    }

    $.ajax({
        url: esNuevo ? url : url + '/' + detalle.id_detalle_orden_compra,
        type: tipo,
        contentType: 'application/json',
        data: JSON.stringify(detalle),
        success: function (resp) {
            $('#mensajeDetalleOrden').html('<div class="alert alert-success">' + resp + '</div>');
            limpiarFormulario();
            listarDetalles();
        },
        error: function (xhr) {
            let msg = "Error al guardar el detalle.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeDetalleOrden').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function editarDetalle(id) {
    $.getJSON('/api/DetalleOrdenCompra/' + id, function (det) {
        $('#id_detalle_orden_compra').val(det.id_detalle_orden_compra);
        $('#id_orden_compra').val(det.id_orden_compra);
        $('#id_producto').val(det.id_producto);
        $('#cantidad_solicitada').val(det.cantidad_solicitada);
        $('#precio_unitario_compra').val(det.precio_unitario_compra);
        $('#subtotal').val(det.subtotal != null ? det.subtotal.toFixed(2) : '');
        $('#btnGuardar').text('Actualizar Detalle');
        $('#btnCancelar').removeClass('d-none');
    });
}

function eliminarDetalle(id) {
    if (!confirm('¿Está seguro de eliminar este detalle?')) return;
    $.ajax({
        url: '/api/DetalleOrdenCompra/' + id,
        type: 'DELETE',
        success: function (resp) {
            $('#mensajeDetalleOrden').html('<div class="alert alert-success">' + resp + '</div>');
            listarDetalles();
        },
        error: function (xhr) {
            let msg = "Error al eliminar el detalle.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeDetalleOrden').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function limpiarFormulario() {
    $('#formDetalleOrden')[0].reset();
    $('#id_detalle_orden_compra').val('');
    $('#btnGuardar').text('Guardar Detalle');
    $('#btnCancelar').addClass('d-none');
    $('#mensajeDetalleOrden').empty();
    calcularSubtotal();
}

function calcularSubtotal() {
    var cantidad = parseInt($('#cantidad_solicitada').val()) || 0;
    var precio = parseFloat($('#precio_unitario_compra').val()) || 0;
    var subtotal = cantidad * precio;
    $('#subtotal').val(subtotal > 0 ? subtotal.toFixed(2) : '');
}