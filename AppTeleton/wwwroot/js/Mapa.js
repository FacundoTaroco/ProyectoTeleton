

const consultorios = document.querySelectorAll('.consultorio');
const infoBox = document.getElementById('info-box');
ocultarTodosLosOverlay()


function ocultarTodosLosOverlay() {

    document.querySelector("#consultorio1").style.opacity = "0"
    document.querySelector("#consultorio2").style.opacity = "0"
    document.querySelector("#consultorio3").style.opacity = "0"
    document.querySelector("#consultorio4uno").style.opacity = "0"
    document.querySelector("#consultorio4dos").style.opacity = "0"
    document.querySelector("#consultorio5uno").style.opacity = "0"
    document.querySelector("#consultorio5dos").style.opacity = "0"
    document.querySelector("#consultorio6uno").style.opacity = "0"
    document.querySelector("#consultorio6dos").style.opacity = "0"
    document.querySelector("#consultorio6tres").style.opacity = "0"
    document.querySelector("#consultorio6cuatro").style.opacity = "0"
    document.querySelector("#consultorio7uno").style.opacity = "0"
    document.querySelector("#consultorio7dos").style.opacity = "0"
    document.querySelector("#consultorio7tres").style.opacity = "0"
}



consultorios.forEach(consultorio => {
    consultorio.addEventListener('click', function () {
        ocultarTodosLosOverlay();
        const info = this.getAttribute('data-info');
        const clase = this.getAttribute('data-clase-cons');
        let Seleccionados = document.querySelectorAll("." + clase); 
        Seleccionados.forEach(s => {

            s.style.opacity = "1";

        })

        infoBox.textContent = info;
        infoBox.style.display = 'block';
    });
});



function showInfo(id) {
    const consultorio = document.getElementById(id);
    if (consultorio) {
        const info = consultorio.getAttribute('data-info');
        infoBox.textContent = info;
        infoBox.style.display = 'block';
    }
}