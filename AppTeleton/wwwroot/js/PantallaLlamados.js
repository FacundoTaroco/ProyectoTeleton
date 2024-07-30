"use strict";
var conexion = new signalR.HubConnectionBuilder().withUrl("/PantallaLLamados").build();

let ListaLLamados = [];




conexion.start().then(function () {
}).catch(function (err) {
        return console.error(err.toString());
})


conexion.on("NuevoLlamado", function (llamado) {



    ListaLLamados.push(llamado);
    
    generarLlamado();

})



async function generarLlamado() {

   
    reproducirAudio();
    mostrarListado();

}



function mostrarListado() {

  
    let listado = document.querySelector("#ListaLLamados")
    listado.innerHTML = "";
    for (let i = 0; i < ListaLLamados.length; i++) {

        listado.innerHTML += `<li>${ListaLLamados[i].nombrePaciente} | ${ListaLLamados[i].cedula} | ${ListaLLamados[i].consultorio}</li>`
    }
}



function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

function reproducirAudio() {
    var audio = document.querySelector("#audioLLamada");
    audio.play();
}