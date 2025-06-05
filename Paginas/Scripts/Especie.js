const baseUrl = "http://veterinariahuellitas.runasp.net/api";
let especies = [];

function mostrarMensaje(msg, tipo = "success") {
    const div = document.getElementById("mensajeEspecie");
    div.innerHTML = `<div class="alert alert-${tipo}" role="alert">${msg}</div>`;
    setTimeout(() => div.innerHTML = "", 3000);
}

function cargarEspecies() {
    fetch(`${baseUrl}/especiesmascota/ConsultarTodos`)
        .then(r => r.json())
        .then(data => {
            especies = data;
            mostrarEspecies();
        });
}

function mostrarEspecies() {
    const tbody = document.querySelector("#tablaEspecies tbody");
    tbody.innerHTML = "";
    especies.forEach(especie => {
        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${especie.id_especie}</td>
            <td>${especie.nombre_especie}</td>
            <td>
                <button class="btn btn-sm btn-warning me-1" onclick="editarEspecie(${especie.id_especie})">Editar</button>
                <button class="btn btn-sm btn-danger" onclick="eliminarEspecie(${especie.id_especie})">Eliminar</button>
            </td>
        `;
        tbody.appendChild(tr);
    });
}

function limpiarFormulario() {
    document.getElementById("formEspecie").reset();
    document.getElementById("id_especie_form").value = "";
    document.getElementById("btnGuardarEspecie").textContent = "Guardar Especie";
    document.getElementById("btnCancelarEspecie").classList.add("d-none");
}

function editarEspecie(id) {
    const especie = especies.find(e => e.id_especie === id);
    if (!especie) return;
    document.getElementById("id_especie_form").value = especie.id_especie;
    document.getElementById("nombre_especie_form").value = especie.nombre_especie;
    document.getElementById("btnGuardarEspecie").textContent = "Actualizar Especie";
    document.getElementById("btnCancelarEspecie").classList.remove("d-none");
}

function eliminarEspecie(id) {
    if (!confirm("¿Está seguro de eliminar esta especie?")) return;
    const especie = especies.find(e => e.id_especie === id);
    if (!especie) return;
    fetch(`${baseUrl}/especiesmascota/EliminarXNombre?nombre_especie=${encodeURIComponent(especie.nombre_especie)}`, { method: "DELETE" })
        .then(r => {
            if (!r.ok) throw new Error();
            mostrarMensaje("Especie eliminada correctamente.", "success");
            cargarEspecies();
        })
        .catch(() => mostrarMensaje("Error al eliminar la especie.", "danger"));
}

document.getElementById("formEspecie").addEventListener("submit", function (e) {
    e.preventDefault();
    const idEspecie = document.getElementById("id_especie_form").value;
    const especie = {
        id_especie: idEspecie ? parseInt(idEspecie) : 0,
        nombre_especie: document.getElementById("nombre_especie_form").value
    };

    let url, method, body;
    if (idEspecie) {
        url = `${baseUrl}/especiesmascota/Actualizar`;
        method = "PUT";
        body = JSON.stringify(especie);
    } else {
        url = `${baseUrl}/especiesmascota/Insertar`;
        method = "POST";
        body = JSON.stringify(especie);
    }

    fetch(url, {
        method: method,
        headers: { "Content-Type": "application/json" },
        body: body
    })
        .then(r => {
            if (!r.ok) throw new Error();
            mostrarMensaje(idEspecie ? "Especie actualizada correctamente." : "Especie registrada correctamente.", "success");
            limpiarFormulario();
            cargarEspecies();
        })
        .catch(() => mostrarMensaje("Error al guardar la especie.", "danger"));
});

document.getElementById("btnCancelarEspecie").addEventListener("click", function () {
    limpiarFormulario();
});

window.editarEspecie = editarEspecie;
window.eliminarEspecie = eliminarEspecie;

document.addEventListener("DOMContentLoaded", function () {
    cargarEspecies();
    limpiarFormulario();
});