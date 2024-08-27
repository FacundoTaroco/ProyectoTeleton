//Archivo que gestiona muchas de las funcionalidades del chat 


var pacienteChat = "";
var mensajesConAudio = false;

//Recargar la vista haciendo un http request
function ActualizarPagina() {
    
    let form = document.getElementById("formularioRecargarPagina");
    form.submit();

}

document.querySelector("#btnAbrirListadoChats").addEventListener("click", AbrirListadoChatsMobile);


//Abrir el listado de chats cuando el dispositivo que esta usando la app es mobile
function AbrirListadoChatsMobile() {
    let contenedorListadoChats = document.querySelector("#contenedorListadoChats")
    let contenedorChat = document.querySelector("#contenedorChat")


    contenedorChat.style.display = "none";
    contenedorListadoChats.style.display = "block";

    document.querySelector("#btnAbrirListadoChats").style.display = "none";
    document.querySelector("#btnAbrirChat").style.display = "block";

}

document.querySelector("#btnAbrirChat").addEventListener("click", AbrirChatActivoMobile);


//Abrir el chat cuando el dispositivo que esta usando la app es mobile
function AbrirChatActivoMobile() {
    let contenedorChat = document.querySelector("#contenedorChat")
    let contenedorListadoChats = document.querySelector("#contenedorListadoChats")

    contenedorChat.style.display = "block";
    contenedorListadoChats.style.display = "none";

    document.querySelector("#btnAbrirListadoChats").style.display = "block";
    document.querySelector("#btnAbrirChat").style.display = "none";
}


document.querySelector("#btnAudio").addEventListener("click", ActivarAudio);

//Activa la sintesis de los mensajes recibidos(Texto a audio) 
function ActivarAudio() {

    if (mensajesConAudio == true) {
        document.querySelector("#imgAudioApagado").style.display = "block";
        document.querySelector("#imgAudioEncendido").style.display = "none";
        mensajesConAudio = false;
    } else {
        document.querySelector("#imgAudioApagado").style.display = "none";
        document.querySelector("#imgAudioEncendido").style.display = "block";
        mensajesConAudio = true;
    }
}


//Funcion que permite cargar un chat que se recibe desde el back-end
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


//Inserta un mensaje recibido
function insertarMensajeRecibido(fechaRecibida, user, mensaje) {
    let fecha = new Date(fechaRecibida);
    let dia = fecha.getDate();
    let mes = fecha.toLocaleString('es-ES', { month: 'long' }); 
    let hora = fecha.getHours();
    let minutos = fecha.getMinutes().toString().padStart(2, '0');
    let fechaString = `${dia} de ${mes}, ${hora}:${minutos}`


    let divInsertar = ` <div class="d-flex justify-content-start">
                            
                            <p class="small mb-1 text-muted fechaMensaje"> ${fechaString}</p>
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



//Sintesis del mensaje(Texto a audio)
function reproducirMensaje(mensaje) {
    window.speechSynthesis.cancel();
    const audio = new SpeechSynthesisUtterance(mensaje);

    audio.pitch = 1;
    audio.rate = 1;
    audio.volume = 1;

    window.speechSynthesis.speak(audio);
}



//Inserta un mensaje enviado
function insertarMensajeMandado(fechaRecibida, userManda, mensaje) {
    let fecha = new Date(fechaRecibida);
    let dia = fecha.getDate();
    let mes = fecha.toLocaleString('es-ES', { month: 'long' }); // Nombre del mes en español
    let hora = fecha.getHours();
    let minutos = fecha.getMinutes().toString().padStart(2, '0');
    let fechaString = `${dia} de ${mes}, ${hora}:${minutos}`
    if (mensaje != "") {
        // <p class="small mb-1 usuarioMensaje">${userManda}</p>
        let divInsertar = ` <div class="d-flex justify-content-end">
                           
                            <p class="small mb-1 text-muted fechaMensaje">${fechaString}</p>
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


//Oculta el campo de texto del chat y Muestra el formulario de "¿Le sirvio la respuesta?"
function mostrarBotonesFeedback() {


    document.getElementById("botoneraFeedback").style.display = "block";


    document.getElementById("botoneraAsistencia").style.display = "none";
    document.getElementById("txtMensaje").style.display = "none";
    document.getElementById("btnEnviar").style.display = "none";
}

//Oculta cualquier formulario y muestra el campo de texto del chat
function mostrarBarraTexto() {

    document.getElementById("botoneraFeedback").style.display = "none";

    document.getElementById("botoneraAsistencia").style.display = "none";
    document.getElementById("txtMensaje").style.display = "block";
    document.getElementById("btnEnviar").style.display = "block";
}

//Oculta el campo de texto del chat y Muestra el formulario de "¿Quiere solicitar ayuda personalizada?"
function mostrarBotonesAsistenciaPersonalizada() {
    document.getElementById("botoneraAsistencia").style.display = "block";

    document.getElementById("botoneraFeedback").style.display = "none";
    document.getElementById("txtMensaje").style.display = "none";
    document.getElementById("btnEnviar").style.display = "none";
}


//Carga el listado de chats cerrados
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


