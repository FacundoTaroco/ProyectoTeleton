
//Teclado virtual que se muestra en la vista de Cerrar sesion totem

let mayusculas = false;

let campoUsuario = document.querySelector("#NombreUsuario");
let campoContra = document.querySelector("#Contrasenia");
let tecladoVirtual = document.querySelector("#tecladoVirtual");

let campoSeleccionado;


campoContra.addEventListener("focus", () => {

    tecladoVirtual.style.display = "flex";

    campoSeleccionado = campoContra;
})



campoUsuario.addEventListener("focus", () => {

    tecladoVirtual.style.display = "flex";

    campoSeleccionado = campoUsuario;
})


let botonesNumericos = document.querySelectorAll(".tecladoNumeroComp");

for (let i = 0; i < botonesNumericos.length; i++) {

    botonesNumericos[i].addEventListener("click", apretarNumero);

}


let botonesLetras = document.querySelectorAll(".tecladoLetra");

for (let i = 0; i < botonesLetras.length; i++) {

    botonesLetras[i].addEventListener("click", apretarLetra);

}




function apretarNumero() {

    let botonApretado = this.getAttribute("boton");
    campoSeleccionado.value += botonApretado;

}

function apretarLetra() {

    let botonApretado = this.getAttribute("boton");

    if (mayusculas) {

        campoSeleccionado.value += botonApretado.toUpperCase();

    } else {
        campoSeleccionado.value += botonApretado.toLowerCase();
    }
    
}

document.querySelector("#btnBorrar").addEventListener("click", () => {

    let texto = campoSeleccionado.value;
    let textoBorrado = texto.slice(0, -1);
    campoSeleccionado.value = textoBorrado;

})

document.querySelector("#btnMayusminus").addEventListener("click", () => {

    mayusculas = !mayusculas;

    let botonesLetras = document.querySelectorAll(".tecladoLetra");

    for (let i = 0; i < botonesLetras.length; i++) {


        if (mayusculas) {
            botonesLetras[i].innerHTML = botonesLetras[i].innerHTML.toUpperCase();
        } else {
            botonesLetras[i].innerHTML = botonesLetras[i].innerHTML.toLowerCase();
        }
        

    }

})



