//Maneja la logica para actualizar el listado de citas de administradores y recepcionistas en tiempo real cada vez que un nuevo paciente accede al totem de recepcion.


"use strict";




var conexion = new signalR.HubConnectionBuilder().withUrl("/ActualizarListadoHub").build();



conexion.start().then(function () {
  
}).catch(function (err) {
    return console.error(err.toString());
})
conexion.on("ActualizarListado", function (listadoActualizado) {

    //Recibe la inidcacion de que un usuario accedio al totem y debe actualizarse el listado

    for (let i = 0; i < listadoActualizado.length; i++) {
        let cita = listadoActualizado[i];


        let tabActualizar = document.getElementById("tdEstado_" + cita.pkAgenda)

        if (tabActualizar != null) {

            if (cita.estado == "RCP") {

                tabActualizar.innerHTML = "<p>Recepcionado</p>"

            }
            else {
                tabActualizar.innerHTML = "<p>NO recepcionado</p>"
            }

        }

    }

 
})