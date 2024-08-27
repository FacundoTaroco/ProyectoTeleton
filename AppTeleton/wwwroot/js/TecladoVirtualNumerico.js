


let teclado = document.querySelector("#tecladoVirtual")
teclado.style.display = "none";



document.querySelector("#txtCedulaTotem").addEventListener("focus", mostrarTeclado)



//Teclado virtual que se muestra en la vista de Index Totem

let botones = document.querySelectorAll(".tecladoNumero");


for (let i = 0; i < botones.length; i++) {

    botones[i].addEventListener("click", apretarBoton);

}

function mostrarTeclado() {

    teclado.style.display = "flex";
}






function apretarBoton() {

    let campoDeTexto = document.querySelector("#txtCedulaTotem")
    let botonApretado = this.getAttribute("boton");

    if (botonApretado == "0") {
        campoDeTexto.value += "0"
    }
    else if (botonApretado == "1") {
        campoDeTexto.value += "1"
    }
    else if (botonApretado == "2") {
        campoDeTexto.value += "2"
    }
    else if (botonApretado == "3") {
        campoDeTexto.value += "3"
    }
    else if (botonApretado == "4") {
        campoDeTexto.value += "4"
    }
    else if (botonApretado == "5") {
        campoDeTexto.value += "5"
    }
    else if (botonApretado == "6") {
        campoDeTexto.value += "6"
    }
    else if (botonApretado == "7") {
        campoDeTexto.value += "7"
    }
    else if (botonApretado == "8") {
        campoDeTexto.value += "8"
    }
     else if (botonApretado == "9") {
        campoDeTexto.value += "9"
    }
    else if (botonApretado == "Borrar") {

        texto = campoDeTexto.value;
        textoBorrado = texto.slice(0, -1);

        campoDeTexto.value = textoBorrado;


    }




}