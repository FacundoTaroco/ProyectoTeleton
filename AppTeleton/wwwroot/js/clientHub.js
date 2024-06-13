"use strict";



var chatbox = document.getElementById("chatbox");
var conexion = new signalR.HubConnectionBuilder().withUrl("/ConnectedHub").build();



conexion.on("MensajeRecibido", function (user, mensaje) {

    if (user != document.querySelector("#txtUsuario").value) { 
        let fecha = new Date();
        let dia = fecha.getDate();
        let mes = fecha.toLocaleString('es-ES', { month: 'long' }); // Nombre del mes en español
        let hora = fecha.getHours();
        let minutos = fecha.getMinutes().toString().padStart(2, '0');
        let fechaString =   `${dia} de ${mes}, ${hora}:${minutos}`


    let divInsertar = ` <div class="d-flex justify-content-between">
                            <p class="small mb-1">${user}</p>
                            <p class="small mb-1 text-muted">${fechaString }</p>
                        </div>
                        <div class="d-flex flex-row justify-content-start">
                        <div>
                                <p class="small p-2 ms-3 mb-3 rounded-3" style="background-color: #f5f6f7;">
                                   ${mensaje}
                                </p>
                            </div>
                        </div>`
                         
    chatbox.innerHTML += divInsertar;}
})


conexion.start().then(function () {
    document.getElementById("btnEnviar").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
})


document.getElementById("btnEnviar").addEventListener("click", function (e) {
    let user = document.querySelector("#txtUsuario").value;
    let mensaje = document.querySelector("#txtMensaje").value;
    let fecha = new Date();
    let dia = fecha.getDay();
    let mes = fecha.toLocaleString('es-ES', { month: 'long' }); // Nombre del mes en español
    let hora = fecha.getHours();
    let minutos = fecha.getMinutes().toString().padStart(2, '0');
    let fechaString = `${dia} de ${mes}, ${hora}:${minutos}`
    if (mensaje != "") {

    let divInsertar = ` <div class="d-flex justify-content-between">
                            <p class="small mb-1">${user}</p>
                            <p class="small mb-1 text-muted">${fechaString}</p>
                        </div>
                        <div class="d-flex flex-row justify-content-end mb-4 pt-1">
                        <div>
                                <p class="small p-2 ms-3 mb-3 rounded-3" style="background-color: #ff8080;">
                                   ${mensaje}
                                </p>
                            </div>
                        </div>`
    chatbox.innerHTML += divInsertar;

    conexion.invoke("SendMessage", user, mensaje).catch(function (err) {
        return console.error(err.toString());
    })
        e.preventDefault();
    }
                       
})