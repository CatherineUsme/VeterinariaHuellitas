const baseUrl = "http://localhost:44302/api";
let razas = [];
let especies = [];

function mostrarMensaje(msg, tipo = "success") {
    const div = document.getElementById("mensajeRaza");
    div.innerHTML = `<div class="alert alert-${tipo}" role="alert">${msg}</div>`;
    setTimeout(() => div.innerHTML = "", 3000);
}

function cargarEspecies() {
    return fetch(`${baseUrl}/especiesmascota/ConsultarTodos`)
        .then(r => r.json())
        .then(data => {
            especies = data;
            const sel = document.getElementById("id_especie_raza");
            sel.innerHTML = '<option value="">Seleccione una especie</option>';
            especies.forEach(e => {
                const opt = document.createElement("option");
                opt.value = e.id_especie;
                opt.text = e.nombre_especie;
                sel.appendChild(opt);
            });
        });
}

function cargarRazas() {
    fetch(`${baseUrl}/razasmascota/ConsultarTodos`)
        .then(r => r.json())
        .then(data => {
            razas = data;
            mostrarRazas();
        });
}

function mostrarRazas() {
    const tbody = document.querySelector("#tablaRazas tbody");
    tbody.innerHTML = "";
    razas.forEach(raza => {
        const especie = especies.find(e => e.id_especie === raza.id_especie);
        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${raza.id_raza}</td>
            <td>${raza.nombre_raza}</td>
            <td>${especie ? especie.nombre_especie : ""}</td>
            <td>
                <button class="btn btn-sm btn-warning me-1" onclick="editarRaza(${raza.id_raza})">Editar</button>
                <button class="btn btn-sm btn-danger" onclick="eliminarRaza(${raza.id_raza})">Eliminar</button>
            </td>
        `;
        tbody.appendChild(tr);
    });
}

function limpiarFormulario() {
    document.getElementById("formRaza").reset();
    document.getElementById("id_raza_form").value = "";
    document.getElementById("btnGuardarRaza").textContent = "Guardar Raza";
    document.getElementById("btnCancelarRaza").classList.add("d-none");
}

function editarRaza(id) {
    const raza = razas.find(r => r.id_raza === id);
    if (!raza) return;
    document.getElementById("id_raza_form").value = raza.id_raza;
    document.getElementById("nombre_raza").value = raza.nombre_raza;
    document.getElementById("id_especie_raza").value = raza.id_especie;
    document.getElementById("btnGuardarRaza").textContent = "Actualizar Raza";
    document.getElementById("btnCancelarRaza").classList.remove("d-none");
}

function eliminarRaza(id) {
    if (!confirm("¿Está seguro de eliminar esta raza?")) return;
    const raza = razas.find(r => r.id_raza === id);
    if (!raza) return;
    fetch(`${baseUrl}/razasmascota/EliminarXNombre?nombre_raza=${encodeURIComponent(raza.nombre_raza)}`, { method: "DELETE" })
        .then(r => {
            if (!r.ok) throw new Error();
            mostrarMensaje("Raza eliminada correctamente.", "success");
            cargarRazas();
        })
        .catch(() => mostrarMensaje("Error al eliminar la raza.", "danger"));
}

document.getElementById("formRaza").addEventListener("submit", function (e) {
    e.preventDefault();
    const idRaza = document.getElementById("id_raza_form").value;
    const raza = {
        id_raza: idRaza ? parseInt(idRaza) : 0,
        nombre_raza: document.getElementById("nombre_raza").value,
        id_especie: parseInt(document.getElementById("id_especie_raza").value)
    };

    let url, method, body;
    if (idRaza) {
        url = `${baseUrl}/razasmascota/Actualizar`;
        method = "PUT";
        body = JSON.stringify(raza);
    } else {
        url = `${baseUrl}/razasmascota/Insertar`;
        method = "POST";
        body = JSON.stringify(raza);
    }

    fetch(url, {
        method: method,
        headers: { "Content-Type": "application/json" },
        body: body
    })
        .then(r => {
            if (!r.ok) throw new Error();
            mostrarMensaje(idRaza ? "Raza actualizada correctamente." : "Raza registrada correctamente.", "success");
            limpiarFormulario();
            cargarRazas();
        })
        .catch(() => mostrarMensaje("Error al guardar la raza.", "danger"));
});

document.getElementById("btnCancelarRaza").addEventListener("click", function () {
    limpiarFormulario();
});

window.editarRaza = editarRaza;
window.eliminarRaza = eliminarRaza;

document.addEventListener("DOMContentLoaded", function () {
    cargarEspecies().then(cargarRazas);
    limpiarFormulario();
});