"use strict";



var chatbox = document.getElementById("chatbox");
var conexion = new signalR.HubConnectionBuilder().withUrl("/ConnectedHub").build();
var ultimoMensajeEnviado = "";

mostrarBarraTexto();


conexion.on("MensajeRecibido", function (user, mensaje, isFinal) {

    if (user != document.querySelector("#txtUsuarioManda").value) { 
        let fecha = new Date();
        insertarMensajeRecibido(fecha.toString(), user, mensaje)
       

        if (user == "CHATBOT" && isFinal && mensaje != "Reescriba la pregunta, por favor.") {
            mostrarBotonesFeedback();
        }
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
    ultimoMensajeEnviado = mensaje;
        
        e.preventDefault();
    
                       
})

function feedBackNegativo() {
    conexion.invoke("FeedBackNegativo", ultimoMensajeEnviado).then(function () {

        mostrarBarraTexto();
    }).catch(function (err) {
        return console.error(err.toString());
    })

}

function feedBackPositivo() {

    let userManda = document.querySelector("#txtUsuarioManda").value;
    conexion.invoke("FeedBackPositivo", ultimoMensajeEnviado, userManda).then(function () {

        
        mostrarBarraTexto();
        document.querySelector("#actualizarAlCerrarChat").submit();

    }).catch(function (err) {
        return console.error(err.toString());
    })
}

