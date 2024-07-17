"use strict";




var conexion = new signalR.HubConnectionBuilder().withUrl("/ActualizarListadoHub").build();



conexion.start().then(function () {
  
}).catch(function (err) {
    return console.error(err.toString());
})
conexion.on("ActualizarListado", function (listadoActualizado) {


    for (let i = 0; i < listadoActualizado.length; i++) {
        let cita = listadoActualizado[i];


        let tabActualizar = document.getElementById("tdEstado_" + cita.pkAgenda)

        if (tabActualizar != null) {

            if (cita.estado == "RCP") {

                tabActualizar.innerHTML = "<p>Llego</p>"

            }
            else {
                tabActualizar.innerHTML = "<p>No llego</p>"
            }

        }

    }

 
})