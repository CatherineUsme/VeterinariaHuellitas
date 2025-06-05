const baseUrl = "http://localhost:50000/api";

let mascotas = [];
let especies = [];
let razas = [];
let duenos = [];

function mostrarMensaje(msg, tipo = "success") {
    const div = document.getElementById("mensajeMascota");
    div.innerHTML = `<div class="alert alert-${tipo}" role="alert">${msg}</div>`;
    setTimeout(() => div.innerHTML = "", 3000);
}

function cargarEspecies() {
    return fetch(`${baseUrl}/especiesmascota/ConsultarTodos`)
        .then(r => r.json())
        .then(data => {
            especies = data;
            const sel = document.getElementById("id_especie");
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
    return fetch(`${baseUrl}/razasmascota/ConsultarTodos`)
        .then(r => r.json())
        .then(data => {
            razas = data;
            filtrarRazas();
        });
}

function filtrarRazas() {
    const idEspecie = parseInt(document.getElementById("id_especie").value);
    const sel = document.getElementById("id_raza");
    sel.innerHTML = '<option value="">Seleccione una raza</option>';
    razas.filter(rz => rz.id_especie === idEspecie).forEach(rz => {
        const opt = document.createElement("option");
        opt.value = rz.id_raza;
        opt.text = rz.nombre_raza;
        sel.appendChild(opt);
    });
}

function cargarDuenos() {
    return fetch(`${baseUrl}/duenos/ConsultarTodos`)
        .then(r => r.json())
        .then(data => {
            duenos = data;
            const sel = document.getElementById("id_dueno");
            sel.innerHTML = '<option value="">Seleccione un dueño</option>';
            duenos.forEach(d => {
                const opt = document.createElement("option");
                opt.value = d.id_dueno;
                opt.text = `${d.nombre} ${d.apellido}`;
                sel.appendChild(opt);
            });
        });
}

function cargarMascotas() {
    fetch(`${baseUrl}/mascotas/ConsultarTodos`)
        .then(r => r.json())
        .then(data => {
            mascotas = data;
            mostrarMascotas();
        });
}

function mostrarMascotas() {
    const tbody = document.querySelector("#tablaMascotas tbody");
    tbody.innerHTML = "";
    mascotas.forEach(mascota => {
        const raza = razas.find(r => r.id_raza === mascota.id_raza);
        const especie = especies.find(e => raza && e.id_especie === raza.id_especie);
        const dueno = duenos.find(d => d.id_dueno === mascota.id_dueno);
        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${mascota.id_mascota}</td>
            <td>${mascota.nombre}</td>
            <td>${mascota.fecha_nacimiento ? mascota.fecha_nacimiento.substring(0, 10) : ""}</td>
            <td>${mascota.peso_kg || ""}</td>
            <td>${mascota.color || ""}</td>
            <td>${mascota.sexo === "M" ? "Macho" : (mascota.sexo === "H" ? "Hembra" : "")}</td>
            <td>${especie ? especie.nombre_especie : ""}</td>
            <td>${raza ? raza.nombre_raza : ""}</td>
            <td>${dueno ? dueno.nombre + " " + dueno.apellido : ""}</td>
            <td>${mascota.microchip_id || ""}</td>
            <td>${mascota.notas_adicionales || ""}</td>
            <td>
                <button class="btn btn-sm btn-warning me-1" onclick="editarMascota(${mascota.id_mascota})">Editar</button>
                <button class="btn btn-sm btn-danger" onclick="eliminarMascota(${mascota.id_mascota})">Eliminar</button>
            </td>
        `;
        tbody.appendChild(tr);
    });
}

function limpiarFormulario() {
    document.getElementById("formMascota").reset();
    document.getElementById("id_mascota").value = "";
    document.getElementById("btnGuardar").textContent = "Guardar Mascota";
    document.getElementById("btnCancelar").classList.add("d-none");
    filtrarRazas();
}

function editarMascota(id) {
    const mascota = mascotas.find(m => m.id_mascota === id);
    if (!mascota) return;
    document.getElementById("id_mascota").value = mascota.id_mascota;
    document.getElementById("nombre").value = mascota.nombre;
    document.getElementById("fecha_nacimiento").value = mascota.fecha_nacimiento ? mascota.fecha_nacimiento.substring(0, 10) : "";
    document.getElementById("peso_kg").value = mascota.peso_kg || "";
    document.getElementById("color").value = mascota.color || "";
    document.getElementById("sexo").value = mascota.sexo || "";
    // Set especie y luego filtrar razas
    const raza = razas.find(r => r.id_raza === mascota.id_raza);
    if (raza) {
        document.getElementById("id_especie").value = raza.id_especie;
        filtrarRazas();
        document.getElementById("id_raza").value = mascota.id_raza;
    }
    document.getElementById("id_dueno").value = mascota.id_dueno;
    document.getElementById("microchip_id").value = mascota.microchip_id || "";
    document.getElementById("notas_adicionales").value = mascota.notas_adicionales || "";
    document.getElementById("btnGuardar").textContent = "Actualizar Mascota";
    document.getElementById("btnCancelar").classList.remove("d-none");
}

function eliminarMascota(id) {
    if (!confirm("¿Está seguro de eliminar esta mascota?")) return;
    fetch(`${baseUrl}/mascotas/EliminarXId?id_mascota=${id}`, { method: "DELETE" })
        .then(r => {
            if (!r.ok) throw new Error();
            mostrarMensaje("Mascota eliminada correctamente.", "success");
            cargarMascotas();
        })
        .catch(() => mostrarMensaje("Error al eliminar la mascota.", "danger"));
}

document.getElementById("id_especie").addEventListener("change", filtrarRazas);

document.getElementById("formMascota").addEventListener("submit", function (e) {
    e.preventDefault();
    const idMascota = document.getElementById("id_mascota").value;
    const mascota = {
        id_mascota: idMascota ? parseInt(idMascota) : 0,
        nombre: document.getElementById("nombre").value,
        fecha_nacimiento: document.getElementById("fecha_nacimiento").value || null,
        peso_kg: document.getElementById("peso_kg").value ? parseFloat(document.getElementById("peso_kg").value) : null,
        color: document.getElementById("color").value,
        sexo: document.getElementById("sexo").value,
        id_raza: parseInt(document.getElementById("id_raza").value),
        id_dueno: parseInt(document.getElementById("id_dueno").value),
        microchip_id: document.getElementById("microchip_id").value,
        notas_adicionales: document.getElementById("notas_adicionales").value
    };

    let url, method, body;
    if (idMascota) {
        url = `${baseUrl}/mascotas/Actualizar`;
        method = "PUT";
        body = JSON.stringify(mascota);
    } else {
        url = `${baseUrl}/mascotas/Insertar`;
        method = "POST";
        body = JSON.stringify(mascota);
    }

    fetch(url, {
        method: method,
        headers: { "Content-Type": "application/json" },
        body: body
    })
        .then(r => {
            if (!r.ok) throw new Error();
            mostrarMensaje(idMascota ? "Mascota actualizada correctamente." : "Mascota registrada correctamente.", "success");
            limpiarFormulario();
            cargarMascotas();
        })
        .catch(() => mostrarMensaje("Error al guardar la mascota.", "danger"));
});

document.getElementById("btnCancelar").addEventListener("click", function () {
    limpiarFormulario();
});

window.editarMascota = editarMascota;
window.eliminarMascota = eliminarMascota;

// Inicialización
document.addEventListener("DOMContentLoaded", function () {
    Promise.all([cargarEspecies(), cargarRazas(), cargarDuenos()]).then(cargarMascotas);
    limpiarFormulario();
});