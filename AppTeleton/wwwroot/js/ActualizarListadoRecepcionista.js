"use strict";




var conexion = new signalR.HubConnectionBuilder().withUrl("/ActualizarListadoHub").build();



conexion.start().then(function () {
  
}).catch(function (err) {
    return console.error(err.toString());
})
conexion.on("ActualizarListado", function (listadoActualizado) {

    let b = 1;
    let a = ""
    //listadoActualizado.$values.forEach(function (mensaje) {

    console.log("ACTUALIZAR LISTADOOO")

})