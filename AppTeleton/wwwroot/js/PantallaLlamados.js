//Gestion de los llamados en tiempo real

"use strict";
var conexion = new signalR.HubConnectionBuilder().withUrl("/PantallaLLamados").build();

let ListaLLamados = [];




conexion.start().then(function () {
}).catch(function (err) {
        return console.error(err.toString());
})



//Se recibe desde el back-end el nuevo llamado realizado
conexion.on("NuevoLlamado", function (llamado) {



    ListaLLamados.push(llamado);

    if (ListaLLamados.length > 6) {

        ListaLLamados =  ListaLLamados.slice(1);
    }
    
    generarLlamado();

})


//Genera el llamado
async function generarLlamado() {

   
    reproducirAudio();
    mostrarListado();

}



//Muestra el listado actualizado de llamados
function mostrarListado() {

  
    let listado = document.querySelector("#ListaLLamados")
    listado.innerHTML = "";
    for (let i = ListaLLamados.length-1; i >= 0; i--) {

        listado.innerHTML += `<tr><td>${ListaLLamados[i].nombrePaciente} </td><td> ${ListaLLamados[i].cedula}  </td><td> ${ListaLLamados[i].consultorio}</td></tr>`
    }
}



function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}


//Activa el sonido de los llamados
function reproducirAudio() {
    var audio = document.querySelector("#audioLLamada");
    audio.play();
}