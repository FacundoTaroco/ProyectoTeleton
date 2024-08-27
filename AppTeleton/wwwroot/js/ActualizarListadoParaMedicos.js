//Maneja la logica para actualizar el listado de pacientes de medicos en tiempo real cada vez que un nuevo paciente accede al totem de recepcion.

"use strict";




var conexion = new signalR.HubConnectionBuilder().withUrl("/ActualizarListadoParaMedicosHub").build();
actualizarEventos()
conexion.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
})
conexion.on("ActualizarListado", function (listadoCitas) {

    let cuerpoTabla = document.querySelector("#cuerpoTabla")
    cuerpoTabla.innerHTML = "";
    for (let i = 0; i < listadoCitas.length; i++) {
        let cita = listadoCitas[i];
        cuerpoTabla.innerHTML +=
        `
            <tr>
                <td>${cita.nombreCompleto}</td>
                <td>${cita.cedula}</td>
                <td>${cita.servicio}</td>
                <td>${cita.horaInicio}</td>
                <td><input type="button" data-nombre="${cita.nombreCompleto}" data-cedula="${cita.cedula}" class="botonesLLamados" value="Realizar Llamado"></td>
            </tr>
        `
    }
    actualizarEventos()
})

function actualizarEventos() {
    let botones = document.querySelectorAll(".botonesLLamados")
    for (let i = 0; i < botones.length; i++) {
        let boton = botones[i];
        boton.addEventListener("click", HacerLLamado);
    }
}

function HacerLLamado() {
    let nombre = this.dataset.nombre
    let cedula = this.dataset.cedula

    let formulario = document.querySelector("#formularioLLamado");
    document.querySelector("#txtNombre").value = nombre;
    document.querySelector("#txtCedula").value = cedula;

    formulario.submit();
}