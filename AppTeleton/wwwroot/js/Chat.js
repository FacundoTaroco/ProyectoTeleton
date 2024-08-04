
var pacienteChat = "";
var mensajesConAudio = false;
function ActualizarPagina() {
    
    let form = document.getElementById("formularioRecargarPagina");
    form.submit();

}

document.querySelector("#btnAudio").addEventListener("click", ActivarAudio);
function ActivarAudio() {

    if (mensajesConAudio == true) {
        document.querySelector("#btnAudio").value = "Activar audio";
        mensajesConAudio = false;
    } else {
        document.querySelector("#btnAudio").value = "Desactivar audio";
        mensajesConAudio = true;
    }
}

function cargarChat(chat, tipoUsuario) {

    if (chat.Abierto == false) {
        //deshabilita los inputs si se detecta que el chat esta cerrado
        document.getElementById("txtMensaje").value = "Este chat se cerro";
        document.getElementById("txtMensaje").disabled = true;
        document.getElementById("btnEnviar").style.display = "none";
    } else {
        document.getElementById("txtMensaje").value = "";
        document.getElementById("txtMensaje").disabled = false;
        document.getElementById("btnEnviar").style.display = "block";
    }


    if (chat != "") {

       

        chat.Mensajes.$values.forEach(function (mensaje) {
            if (tipoUsuario == "PACIENTE") {
                if (mensaje.EsDePaciente) {
                    insertarMensajeMandado(mensaje.fecha, mensaje.nombreUsuario, mensaje.contenido)
                    pacienteChat = mensaje.nombreUsuario;
                } else {

                    insertarMensajeRecibido(mensaje.fecha, mensaje.nombreUsuario, mensaje.contenido)
                }


            } else {

                if (mensaje.EsDePaciente) {
                    insertarMensajeRecibido(mensaje.fecha, mensaje.nombreUsuario, mensaje.contenido)

                } else {

                    insertarMensajeMandado(mensaje.fecha, mensaje.nombreUsuario, mensaje.contenido)
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

    if (mensajesConAudio) {
        reproducirMensaje(mensaje)
    }

}




function reproducirMensaje(mensaje) {
    window.speechSynthesis.cancel();
    const audio = new SpeechSynthesisUtterance(mensaje);

    audio.pitch = 1;
    audio.rate = 1;
    audio.volume = 1;

    window.speechSynthesis.speak(audio);
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



function mostrarBotonesFeedback() {


    document.getElementById("botoneraFeedback").style.display = "block";


    document.getElementById("botoneraAsistencia").style.display = "none";
    document.getElementById("txtMensaje").style.display = "none";
    document.getElementById("btnEnviar").style.display = "none";
}

function mostrarBarraTexto() {

    document.getElementById("botoneraFeedback").style.display = "none";

    document.getElementById("botoneraAsistencia").style.display = "none";
    document.getElementById("txtMensaje").style.display = "block";
    document.getElementById("btnEnviar").style.display = "block";
}

function mostrarBotonesAsistenciaPersonalizada() {
    document.getElementById("botoneraAsistencia").style.display = "block";

    document.getElementById("botoneraFeedback").style.display = "none";
    document.getElementById("txtMensaje").style.display = "none";
    document.getElementById("btnEnviar").style.display = "none";
}


function cargarListadoChatsCerrados() {

    let chatsLi = document.querySelectorAll(".liChats");
    for (let i = 0; i < chatsLi.length; i++) {
        chatsLi[i].addEventListener("click", function (e) {

            let chatLi = this.getAttribute("chat-id");
            document.querySelector("#inputCargarChatCerrado").value = chatLi;
            let formCargarChat = document.querySelector("#cargarChatCerrado");

            formCargarChat.submit();
        })
    }
}





function LlamarTeleton() {

    window.location.href = 'tel:+23043620';
}


