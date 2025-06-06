// URL base de la API
const UrlBase = "http://veterinariahuellitas.runasp.net/api";
let duenos = [];

// Utilidad para mostrar mensajes al usuario
function mostrarMensaje(msg, tipo = "success") {
    const div = document.getElementById("mensajeDueno");
    div.innerHTML = `<div class="alert alert-${tipo}" role="alert">${msg}</div>`;
    setTimeout(() => div.innerHTML = "", 3000);
}

// Cargar todos los dueños y mostrarlos en la tabla
function cargarDuenos() {
    fetch(`${UrlBase}/duenos/ConsultarTodos`)
        .then(r => r.json())
        .then(data => {
            duenos = data;
            mostrarDuenos();
        });
}

// Renderizar la tabla de dueños
function mostrarDuenos() {
    const tbody = document.querySelector("#tablaDuenos tbody");
    tbody.innerHTML = "";
    duenos.forEach(dueno => {
        const inactivarBtn = dueno.activo
            ? `<button class="btn btn-sm btn-secondary me-1" onclick="inactivarDueno('${dueno.cedula}')">Inactivar</button>`
            : `<button class="btn btn-sm btn-success me-1" onclick="activarDueno('${dueno.cedula}')">Activar</button>`;
        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${dueno.id_dueno}</td>
            <td>${dueno.cedula}</td>
            <td>${dueno.nombre}</td>
            <td>${dueno.apellido}</td>
            <td>${dueno.email || ""}</td>
            <td>${dueno.telefono || ""}</td>
            <td>${dueno.direccion || ""}</td>
            <td>${dueno.fecha_registro ? dueno.fecha_registro.split('T')[0] : ""}</td>
            <td>${dueno.activo ? "Sí" : "No"}</td>
            <td>
                <button class="btn btn-sm btn-warning me-1" onclick="editarDueno('${dueno.cedula}')">Editar</button>
                <button class="btn btn-sm btn-danger me-1" onclick="eliminarDueno('${dueno.cedula}')">Eliminar</button>
                ${inactivarBtn}
            </td>
        `;
        tbody.appendChild(tr);
    });
}

// Limpiar el formulario y restablecer controles
function limpiarFormulario() {
    document.getElementById("formDueno").reset();
    document.getElementById("id_dueno").value = "";
    document.getElementById("btnGuardar").textContent = "Guardar";
    document.getElementById("btnCancelar").classList.add("d-none");
    document.getElementById("btnInactivar").classList.add("d-none");
    document.getElementById("btnActivar").classList.add("d-none");
    document.getElementById("cedula").readOnly = false;
    document.getElementById("activo").checked = false;
}

// Cargar datos de un dueño en el formulario para edición
function editarDueno(cedula) {
    const dueno = duenos.find(d => d.cedula === cedula);
    if (!dueno) return;
    document.getElementById("id_dueno").value = dueno.id_dueno;
    document.getElementById("cedula").value = dueno.cedula;
    document.getElementById("cedula").readOnly = true;
    document.getElementById("nombre").value = dueno.nombre;
    document.getElementById("apellido").value = dueno.apellido;
    document.getElementById("email").value = dueno.email || "";
    document.getElementById("telefono").value = dueno.telefono || "";
    document.getElementById("direccion").value = dueno.direccion || "";
    document.getElementById("fecha_registro").value = dueno.fecha_registro ? dueno.fecha_registro.split('T')[0] : "";
    document.getElementById("activo").checked = dueno.activo ? true : false;
    document.getElementById("btnGuardar").textContent = "Actualizar";
    document.getElementById("btnCancelar").classList.remove("d-none");

    // Mostrar botón Inactivar/Activar según estado
    if (dueno.activo) {
        document.getElementById("btnInactivar").classList.remove("d-none");
        document.getElementById("btnInactivar").onclick = function () {
            inactivarDueno(cedula);
        };
        document.getElementById("btnActivar").classList.add("d-none");
    } else {
        document.getElementById("btnInactivar").classList.add("d-none");
        document.getElementById("btnActivar").classList.remove("d-none");
        document.getElementById("btnActivar").onclick = function () {
            activarDueno(cedula);
        };
    }
}

// Eliminar un dueño
function eliminarDueno(cedula) {
    if (!confirm("¿Está seguro de eliminar este dueño?")) return;
    fetch(`${UrlBase}/duenos/EliminarXCedula?cedula=${encodeURIComponent(cedula)}`, { method: "DELETE" })
        .then(r => {
            if (!r.ok) throw new Error();
            mostrarMensaje("Dueño eliminado correctamente.", "success");
            cargarDuenos();
        })
        .catch(() => mostrarMensaje("Error al eliminar el dueño.", "danger"));
}

// Inactivar un dueño
function inactivarDueno(cedula) {
    if (!confirm("¿Está seguro de inactivar este dueño?")) return;
    fetch(`${UrlBase}/duenos/Inactivar?cedula=${encodeURIComponent(cedula)}`, { method: "PUT" })
        .then(r => {
            if (!r.ok) throw new Error();
            mostrarMensaje("Dueño inactivado correctamente.", "success");
            limpiarFormulario();
            cargarDuenos();
        })
        .catch(() => mostrarMensaje("Error al inactivar el dueño.", "danger"));
}

// Activar un dueño
function activarDueno(cedula) {
    if (!confirm("¿Está seguro de activar este dueño?")) return;
    fetch(`${UrlBase}/duenos/Activar?cedula=${encodeURIComponent(cedula)}`, { method: "PUT" })
        .then(r => {
            if (!r.ok) throw new Error();
            mostrarMensaje("Dueño activado correctamente.", "success");
            limpiarFormulario();
            cargarDuenos();
        })
        .catch(() => mostrarMensaje("Error al activar el dueño.", "danger"));
}

// Evento para guardar o actualizar un dueño
document.getElementById("formDueno").addEventListener("submit", function (e) {
    e.preventDefault();
    const idDueno = document.getElementById("id_dueno").value;
    const dueno = {
        id_dueno: idDueno ? parseInt(idDueno) : 0,
        nombre: document.getElementById("nombre").value,
        apellido: document.getElementById("apellido").value,
        cedula: document.getElementById("cedula").value,
        email: document.getElementById("email").value,
        telefono: document.getElementById("telefono").value,
        direccion: document.getElementById("direccion").value,
        fecha_registro: document.getElementById("fecha_registro").value || null,
        activo: document.getElementById("activo").checked
    };

    let url, method, body;
    if (idDueno) {
        url = `${UrlBase}/duenos/Actualizar`;
        method = "PUT";
        body = JSON.stringify(dueno);
    } else {
        url = `${UrlBase}/duenos/Insertar`;
        method = "POST";
        body = JSON.stringify(dueno);
    }

    fetch(url, {
        method: method,
        headers: { "Content-Type": "application/json" },
        body: body
    })
        .then(r => {
            if (!r.ok) throw new Error();
            mostrarMensaje(idDueno ? "Dueño actualizado correctamente." : "Dueño registrado correctamente.", "success");
            limpiarFormulario();
            cargarDuenos();
        })
        .catch(() => mostrarMensaje("Error al guardar el dueño.", "danger"));
});

// Evento para cancelar la edición
document.getElementById("btnCancelar").addEventListener("click", function () {
    limpiarFormulario();
});

// Exponer funciones globalmente para los botones generados dinámicamente
window.editarDueno = editarDueno;
window.eliminarDueno = eliminarDueno;
window.inactivarDueno = inactivarDueno;
window.activarDueno = activarDueno;

// Inicialización al cargar la página
document.addEventListener("DOMContentLoaded", function () {
    cargarDuenos();
    limpiarFormulario();
});

