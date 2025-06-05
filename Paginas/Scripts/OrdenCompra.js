$(document).ready(function () {
    cargarProveedores();
    cargarEmpleados();
    cargarSedes();
    listarOrdenes();

    $('#formOrdenCompra').on('submit', function (e) {
        e.preventDefault();
        guardarOrden();
    });

    $('#btnCancelar').on('click', function () {
        limpiarFormulario();
    });
});

function cargarProveedores() {
    $.getJSON('/api/Proveedor', function (data) {
        var $prov = $('#id_proveedor');
        $prov.empty().append('<option value="">Seleccione un proveedor</option>');
        $.each(data, function (i, p) {
            $prov.append('<option value="' + p.id_proveedor + '">' + p.nombre_empresa + '</option>');
        });
    });
}

function cargarEmpleados() {
    $.getJSON('/api/Empleado/ConsultarTodos', function (data) {
        var $emp = $('#id_empleado_solicita');
        $emp.empty().append('<option value="">Seleccione un empleado</option>');
        $.each(data, function (i, e) {
            $emp.append('<option value="' + e.id_empleado + '">' + e.nombre + ' ' + e.apellido + '</option>');
        });
    });
}

function cargarSedes() {
    $.getJSON('/api/Sede/ConsultarTodos', function (data) {
        var $sede = $('#id_sede_destino');
        $sede.empty().append('<option value="">Seleccione una sede</option>');
        $.each(data, function (i, s) {
            $sede.append('<option value="' + s.id_sede + '">' + s.nombre_sede + '</option>');
        });
    });
}

function listarOrdenes() {
    $.getJSON('/api/OrdenCompra', function (data) {
        var $tbody = $('#tablaOrdenCompra tbody');
        $tbody.empty();
        $.each(data, function (i, orden) {
            let acciones =
                '<button class="btn btn-sm btn-warning me-1" onclick="editarOrden(' + orden.id_orden_compra + ')">Editar</button>';

            // Botones de estado según el estado actual
            if (orden.estado === "Pendiente") {
                acciones +=
                    '<button class="btn btn-sm btn-success me-1" onclick="cambiarEstado(' + orden.id_orden_compra + ',\'Aprobar\')">Aprobar</button>' +
                    '<button class="btn btn-sm btn-secondary me-1" onclick="cambiarEstado(' + orden.id_orden_compra + ',\'Cancelar\')">Cancelar</button>';
            } else if (orden.estado === "Aprobada") {
                acciones +=
                    '<button class="btn btn-sm btn-info me-1" onclick="cambiarEstado(' + orden.id_orden_compra + ',\'Recibir\')">Recibir</button>' +
                    '<button class="btn btn-sm btn-secondary me-1" onclick="cambiarEstado(' + orden.id_orden_compra + ',\'Cancelar\')">Cancelar</button>';
            }

            $tbody.append('<tr>' +
                '<td>' + orden.id_orden_compra + '</td>' +
                '<td>' + (orden.PROVEEDOR ? orden.PROVEEDOR.nombre_empresa : orden.id_proveedor) + '</td>' +
                '<td>' + (orden.EMPLEADO ? (orden.EMPLEADO.nombre + " " + orden.EMPLEADO.apellido) : orden.id_empleado_solicita) + '</td>' +
                '<td>' + (orden.SEDE ? orden.SEDE.nombre_sede : orden.id_sede_destino) + '</td>' +
                '<td>' + (orden.fecha_orden ? orden.fecha_orden.substring(0, 10) : '') + '</td>' +
                '<td>' + (orden.estado || '') + '</td>' +
                '<td>' + (orden.observaciones || '') + '</td>' +
                '<td>' + acciones + '</td>' +
                '</tr>');
        });
    });
}

function guardarOrden() {
    var orden = {
        id_orden_compra: $('#id_orden_compra').val() || 0,
        id_proveedor: parseInt($('#id_proveedor').val()),
        id_empleado_solicita: parseInt($('#id_empleado_solicita').val()),
        id_sede_destino: parseInt($('#id_sede_destino').val()),
        observaciones: $('#observaciones').val()
    };

    var esNuevo = orden.id_orden_compra == 0 || orden.id_orden_compra === "";

    var url = esNuevo ? '/api/OrdenCompra' : '/api/OrdenCompra/' + orden.id_orden_compra;
    var tipo = esNuevo ? 'POST' : 'PUT';

    $.ajax({
        url: url,
        type: tipo,
        contentType: 'application/json',
        data: JSON.stringify(orden),
        success: function (resp) {
            $('#mensajeOrdenCompra').html('<div class="alert alert-success">' + resp + '</div>');
            limpiarFormulario();
            listarOrdenes();
        },
        error: function (xhr) {
            let msg = "Error al guardar la orden.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeOrdenCompra').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function editarOrden(id) {
    $.getJSON('/api/OrdenCompra/' + id, function (orden) {
        $('#id_orden_compra').val(orden.id_orden_compra);
        $('#id_proveedor').val(orden.id_proveedor);
        $('#id_empleado_solicita').val(orden.id_empleado_solicita);
        $('#id_sede_destino').val(orden.id_sede_destino);
        $('#observaciones').val(orden.observaciones);
        $('#btnGuardar').text('Actualizar Orden');
        $('#btnCancelar').removeClass('d-none');
    });
}

function cambiarEstado(id, accion) {
    let endpoint = '';
    let accionTexto = '';
    if (accion === 'Aprobar') {
        endpoint = '/api/OrdenCompra/' + id + '/Aprobar';
        accionTexto = 'aprobar';
    } else if (accion === 'Recibir') {
        endpoint = '/api/OrdenCompra/' + id + '/Recibir';
        accionTexto = 'marcar como recibida';
    } else if (accion === 'Cancelar') {
        endpoint = '/api/OrdenCompra/' + id + '/Cancelar';
        accionTexto = 'cancelar';
    } else {
        return;
    }

    if (!confirm('¿Está seguro de ' + accionTexto + ' esta orden de compra?')) return;

    $.ajax({
        url: endpoint,
        type: 'PUT',
        success: function (resp) {
            $('#mensajeOrdenCompra').html('<div class="alert alert-success">' + resp + '</div>');
            listarOrdenes();
        },
        error: function (xhr) {
            let msg = "Error al cambiar el estado de la orden.";
            if (xhr.responseText) msg += " " + xhr.responseText;
            $('#mensajeOrdenCompra').html('<div class="alert alert-danger">' + msg + '</div>');
        }
    });
}

function limpiarFormulario() {
    $('#formOrdenCompra')[0].reset();
    $('#id_orden_compra').val('');
    $('#btnGuardar').text('Guardar Orden');
    $('#btnCancelar').addClass('d-none');
    $('#mensajeOrdenCompra').empty();
}