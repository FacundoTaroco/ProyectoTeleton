"use strict";



var chatbox = document.getElementById("chatbox");
var conexion = new signalR.HubConnectionBuilder().withUrl("/ConnectedHub").build();



conexion.on("MensajeRecibido", function (user, mensaje) {

    if (user != document.querySelector("#txtUsuarioManda").value) { 
        let fecha = new Date();
        insertarMensajeRecibido(fecha.toString(),user,mensaje)
    }

})


conexion.start().then(function () {
    document.getElementById("btnEnviar").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
})


document.getElementById("btnEnviar").addEventListener("click", function (e) {
    let userManda = document.querySelector("#txtUsuarioManda").value;
    let userRecibe = document.querySelector("#txtUsuarioRecibe").value;
    let mensaje = document.querySelector("#txtMensaje").value;
    let fecha = new Date();

    insertarMensajeMandado(fecha.toString(), userManda, mensaje);
   
        conexion.invoke("SendMessage",userManda, userRecibe, mensaje).catch(function (err) {
        return console.error(err.toString());
        })
        
        e.preventDefault();
    
                       
})


function cargarChat(chat, tipoUsuario) {

    if (chat != "") {

        chat.Mensajes.$values.forEach(function (mensaje) {
            if (tipoUsuario == "PACIENTE") {
                if (mensaje.EsDePaciente) {
                    insertarMensajeMandado(mensaje.fecha, mensaje.nombreUsuario,mensaje.contenido)

                } else {

                    insertarMensajeRecibido(mensaje.fecha, mensaje.nombreUsuario, mensaje.contenido)
                }


            } else {

                if (mensaje.EsDePaciente) {
                    insertarMensajeRecibido(mensaje.fecha, mensaje.nombreUsuario, mensaje.contenido)

                } else {

                    insertarMensajeEnviado(mensaje.fecha, mensaje.nombreUsuario, mensaje.contenido)
                }
            }
           


        })



    }

}


function insertarMensajeRecibido(fechaRecibida, user, mensaje) {
    let fecha = new Date(fechaRecibida);
    let dia = fecha.getDate();
    let mes = fecha.toLocaleString('es-ES', { month: 'long' }); // Nombre del mes en español
    let hora = fecha.getHours();
    let minutos = fecha.getMinutes().toString().padStart(2, '0');
    let fechaString = `${dia} de ${mes}, ${hora}:${minutos}`


    let divInsertar = ` <div class="d-flex justify-content-between">
                            <p class="small mb-1">${user}</p>
                            <p class="small mb-1 text-muted">${fechaString}</p>
                        </div>
                        <div class="d-flex flex-row">
                        <div class="chat-header-recibido">
                                <p class="small p-2 rounded-3 chat-message" style="background-color: #f5f6f7;">
                                   ${mensaje}
                                </p>
                            </div>
                        </div>`

    chatbox.innerHTML += divInsertar;
    chatbox.scrollTop = chatbox.scrollHeight;


}
function cambiarARecepcionista() {

    document.querySelector("#txtUsuarioRecibe").value = "Recepcionista";


}

function cambiarABot() {


    document.querySelector("#txtUsuarioRecibe").value = "CHATBOT";

}

function insertarMensajeMandado(fechaRecibida, userManda, mensaje) {
    let fecha = new Date(fechaRecibida);
    let dia = fecha.getDate();
    let mes = fecha.toLocaleString('es-ES', { month: 'long' }); // Nombre del mes en español
    let hora = fecha.getHours();
    let minutos = fecha.getMinutes().toString().padStart(2, '0');
    let fechaString = `${dia} de ${mes}, ${hora}:${minutos}`
    if (mensaje != "") {

        let divInsertar = ` <div class="d-flex justify-content-between">
                            <p class="small mb-1">${userManda}</p>
                            <p class="small mb-1 text-muted">${fechaString}</p>
                        </div>
                        <div class="d-flex flex-row">
                        <div  class="chat-header-enviado" >
                                <p class="small p-2 rounded-3 chat-message" style="background-color: #ff8080;">
                                   ${mensaje}
                                </p>
                            </div>
                        </div>`
        chatbox.innerHTML += divInsertar;
        chatbox.scrollTop = chatbox.scrollHeight;

    }


}