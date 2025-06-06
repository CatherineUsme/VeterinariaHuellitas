const baseUrl = "http://veterinariahuellitas.runasp.net/api";


let mascotas = [];
let empleados = [];
let sedes = [];
let tiposCita = [];
let citas = [];

function mostrarMensaje(msg, tipo = "success") {
    const div = document.getElementById("mensajeCita");
    div.innerHTML = `<div class="alert alert-${tipo}" role="alert">${msg}</div>`;
    setTimeout(() => div.innerHTML = "", 3000);
}

function cargarMascotas() {
    return fetch(`${baseUrl}/mascotas/ConsultarTodos`)
        .then(r => r.json())
        .then(data => {
            mascotas = data;
            const sel = document.getElementById("id_mascota");
            sel.innerHTML = '<option value="">Seleccione una mascota</option>';
            mascotas.forEach(m => {
                const opt = document.createElement("option");
                opt.value = m.id_mascota;
                opt.text = m.nombre;
                sel.appendChild(opt);
            });
        });
}

function cargarEmpleados() {
    return fetch(`${baseUrl}/Empleado/ConsultarTodos`)
        .then(r => r.json())
        .then(data => {
            empleados = data;
            const sel = document.getElementById("id_empleado_asignado");
            sel.innerHTML = '<option value="">Seleccione un empleado</option>';
            empleados.forEach(e => {
                const opt = document.createElement("option");
                opt.value = e.id_empleado;
                opt.text = `${e.nombre} ${e.apellido}`;
                sel.appendChild(opt);
            });
        });
}

function cargarSedes() {
    return fetch(`${baseUrl}/Sede/ConsultarTodos`)
        .then(r => r.json())
        .then(data => {
            sedes = data;
            const sel = document.getElementById("id_sede");
            sel.innerHTML = '<option value="">Seleccione una sede</option>';
            sedes.forEach(s => {
                const opt = document.createElement("option");
                opt.value = s.id_sede;
                opt.text = s.nombre_sede;
                sel.appendChild(opt);
            });
        });
}

function cargarTiposCita() {
    return fetch(`${baseUrl}/tipocita`)
        .then(r => r.json())
        .then(data => {
            tiposCita = data;
            const sel = document.getElementById("id_tipo_cita");
            sel.innerHTML = '<option value="">Seleccione un tipo</option>';
            tiposCita.forEach(tc => {
                const opt = document.createElement("option");
                opt.value = tc.id_tipo_cita;
                opt.text = tc.nombre_tipo_cita || tc.nombre || `Tipo ${tc.id_tipo_cita}`;
                sel.appendChild(opt);
            });
        });
}

function cargarCitas() {
    fetch(`${baseUrl}/citas`)
        .then(r => r.json())
        .then(data => {
            citas = data;
            mostrarCitas();
        });
}

function mostrarCitas() {
    const tbody = document.querySelector("#tablaCitas tbody");
    tbody.innerHTML = "";
    citas.forEach(cita => {
        const mascota = mascotas.find(m => m.id_mascota === cita.id_mascota);
        const tipo = tiposCita.find(tc => tc.id_tipo_cita === cita.id_tipo_cita);
        const empleado = empleados.find(e => e.id_empleado === cita.id_empleado_asignado);
        const sede = sedes.find(s => s.id_sede === cita.id_sede);
        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${cita.id_cita}</td>
            <td>${mascota ? mascota.nombre : cita.id_mascota}</td>
            <td>${empleado ? empleado.nombre + " " + empleado.apellido : cita.id_empleado_asignado}</td>
            <td>${sede ? sede.nombre_sede : cita.id_sede}</td>
            <td>${tipo ? (tipo.nombre_tipo_cita || tipo.nombre) : cita.id_tipo_cita}</td>
            <td>${cita.fecha_hora_cita ? cita.fecha_hora_cita.replace("T", " ").substring(0, 16) : ""}</td>
            <td>${cita.estado}</td>
            <td>${cita.observaciones || ""}</td>
            <td>
                <button class="btn btn-sm btn-warning me-1" onclick="editarCita(${cita.id_cita})">Editar</button>
                <button class="btn btn-sm btn-danger" onclick="eliminarCita(${cita.id_cita})">Eliminar</button>
            </td>
        `;
        tbody.appendChild(tr);
    });
}

function limpiarFormulario() {
    document.getElementById("formCita").reset();
    document.getElementById("id_cita").value = "";
    document.getElementById("btnGuardar").textContent = "Agendar Cita";
    document.getElementById("btnCancelar").classList.add("d-none");
    // Fecha actual del sistema
    const now = new Date();
    const local = now.toISOString().slice(0, 16);
    document.getElementById("fecha_hora_cita").value = local;
}

function editarCita(id) {
    const cita = citas.find(c => c.id_cita === id);
    if (!cita) return;
    document.getElementById("id_cita").value = cita.id_cita;
    document.getElementById("id_mascota").value = cita.id_mascota;
    document.getElementById("id_empleado_asignado").value = cita.id_empleado_asignado;
    document.getElementById("id_sede").value = cita.id_sede;
    document.getElementById("id_tipo_cita").value = cita.id_tipo_cita;
    document.getElementById("fecha_hora_cita").value = cita.fecha_hora_cita ? cita.fecha_hora_cita.substring(0, 16) : "";
    document.getElementById("estado").value = cita.estado;
    document.getElementById("observaciones").value = cita.observaciones || "";
    document.getElementById("btnGuardar").textContent = "Actualizar Cita";
    document.getElementById("btnCancelar").classList.remove("d-none");
}

function eliminarCita(id) {
    if (!confirm("¿Está seguro de eliminar esta cita?")) return;
    fetch(`${baseUrl}/citas/${id}`, { method: "DELETE" })
        .then(r => {
            if (!r.ok) throw new Error();
            mostrarMensaje("Cita eliminada correctamente.", "success");
            cargarCitas();
        })
        .catch(() => mostrarMensaje("Error al eliminar la cita.", "danger"));
}

document.getElementById("formCita").addEventListener("submit", function (e) {
    e.preventDefault();
    const idCita = document.getElementById("id_cita").value;
    const cita = {
        id_cita: idCita ? parseInt(idCita) : 0,
        id_mascota: parseInt(document.getElementById("id_mascota").value),
        id_empleado_asignado: parseInt(document.getElementById("id_empleado_asignado").value),
        id_sede: parseInt(document.getElementById("id_sede").value),
        id_tipo_cita: parseInt(document.getElementById("id_tipo_cita").value),
        fecha_hora_cita: document.getElementById("fecha_hora_cita").value,
        estado: document.getElementById("estado").value,
        observaciones: document.getElementById("observaciones").value
    };

    const url = `${baseUrl}/citas`;
    const method = idCita ? "PUT" : "POST";

    fetch(url, {
        method: method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(cita)
    })
        .then(r => {
            if (!r.ok) throw new Error();
            mostrarMensaje(idCita ? "Cita actualizada correctamente." : "Cita agendada correctamente.", "success");
            limpiarFormulario();
            cargarCitas();
        })
        .catch(() => mostrarMensaje("Error al guardar la cita.", "danger"));
});

document.getElementById("btnCancelar").addEventListener("click", function () {
    limpiarFormulario();
});

window.editarCita = editarCita;
window.eliminarCita = eliminarCita;

// Inicialización
document.addEventListener("DOMContentLoaded", function () {
    Promise.all([cargarMascotas(), cargarEmpleados(), cargarSedes(), cargarTiposCita()]).then(cargarCitas);
    limpiarFormulario();
});