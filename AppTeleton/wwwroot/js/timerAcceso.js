//Archivo que gestiona la tarea de cerrar acceso automaticamente del totem cuando un paciente lleva mas de 60 segundos inactivo

const idleTimeout = 60; // Tiempo de inactividad en segundos (1 minuto)
let timer; // Variable para el temporizador
let timeLeft = idleTimeout; // Variable para el tiempo restante inicializada con idleTimeout

window.onload = function () {
    const display = document.getElementById('time');
    display.style.display = "none";
    const form = document.getElementById("formCerrarSesionAutomaticamente");
    startTimer(display, form);
};

function startTimer(display, form) {
    function updateTimer() {
        display.style.display = "none";

        if (timeLeft <= 10) {
            display.style.display = "block";
            display.innerHTML = `<p>Su sesion se cerrara en ${timeLeft} segundos, presione la pantalla para continuar usando el tótem</p>`;
        }
   
        if (timeLeft <= 0) {
            form.submit(); // Enviar formulario para cerrar sesión u otra acción
        } else {
            timeLeft--;
        }
    }

    updateTimer();
    setInterval(updateTimer, 1000); // Actualizar el temporizador cada segundo
}

function resetTimer() {
    clearTimeout(timer);
    timeLeft = idleTimeout; // Reiniciar timeLeft al valor de idleTimeout
    startTimer(timeLeft, display, form); // Reiniciar el temporizador con el nuevo timeLeft

    timer = setTimeout(function () {
        console.log("Usuario inactivo");
        document.getElementById("formCerrarSesionAutomaticamente").submit();
    }, idleTimeout * 1000);
}

document.addEventListener('click', function () {
    console.log("Click detectado");
    resetTimer();

});