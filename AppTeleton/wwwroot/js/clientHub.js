"use strict";



var chatbox = document.getElementById("chatbox");
var conexion = new signalR.HubConnectionBuilder().withUrl("/ConnectedHub").build();
var ultimoMensajeEnviado = "";


mostrarBarraTexto();


conexion.on("MensajeRecibido", function (user, userDestino, mensaje, isFinal, paraPaciente) {


    if ((user != document.querySelector("#txtUsuarioManda").value && user == document.querySelector("#txtUsuarioRecibe").value) || paraPaciente) { 

        document.querySelector("#txtUsuarioRecibe").value = user;
        let fecha = new Date();
        insertarMensajeRecibido(fecha.toString(), user, mensaje)
       

        if (user == "CHATBOT" && isFinal && mensaje != "Reescriba la pregunta, por favor.") {
            mostrarBotonesFeedback();
        }
    }

})


function CerrarChat() {

    let userRecibe = document.querySelector("#txtUsuarioRecibe").value;
    document.getElementById("txtMensaje").value = "Este chat se cerro";
    document.getElementById("txtMensaje").disabled = true;
    document.getElementById("btnEnviar").style.display = "none";
    conexion.invoke("CerrarChat", userRecibe).catch(function (err) {
        return console.error(err.toString());
    })

}
conexion.on("CerrarChatEnVivo", function () {

    document.getElementById("txtMensaje").value = "Este chat se cerro";
    document.getElementById("txtMensaje").disabled = true;
    document.getElementById("btnEnviar").style.display = "none";
})

conexion.on("MostrarBotoneraAsistencia", function () {
    mostrarBotonesAsistenciaPersonalizada();
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

    if (mensaje != "") {

  document.querySelector("#txtMensaje").value = "";
    let fecha = new Date();

    insertarMensajeMandado(fecha.toString(), userManda, mensaje);
   
        conexion.invoke("SendMessage",userManda, userRecibe, mensaje).catch(function (err) {
        return console.error(err.toString());
        })
    ultimoMensajeEnviado = mensaje;
        
        e.preventDefault();


    }
  
    
                       
})

function feedBackNegativo() {
    let userManda = document.querySelector("#txtUsuarioManda").value;
 
    conexion.invoke("FeedBackNegativo", ultimoMensajeEnviado, userManda).then(function () {


        if (document.getElementById("botoneraAsistencia").style.display == "none") {
                mostrarBarraTexto();
        }
        
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

function SolicitarRecepcionista() {
  
    let userManda = document.querySelector("#txtUsuarioManda").value;


    conexion.invoke("SolicitarAsistenciaPersonalizada", userManda).then(function () {

        let fecha = new Date();
        let mensaje = "Una recepcionista lo atendera por esta u otra via lo antes posible, recuerde que el horario de atencion personalizada es de 8am hasta las 5pm"
        insertarMensajeRecibido(fecha.toString(), "CHATBOT", mensaje)
        mostrarBarraTexto();
    }).catch(function (err) {
        return console.error(err.toString());
    })

}



