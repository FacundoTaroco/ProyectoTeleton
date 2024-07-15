const idleTimeout = 10; // Tiempo de inactividad en segundos (10 minutos)
let timer; // Variable para el temporizador
let timeLeft = idleTimeout; // Variable para el tiempo restante inicializada con idleTimeout

function startTimer(duration, display, form) {
    function updateTimer() {
        let minutes = Math.floor(timeLeft / 60);
        let seconds = timeLeft % 60;

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = minutes + ":" + seconds;

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

    connection.invoke("UserClicked").catch(function (err) {
        console.error("Error al invocar UserClicked:", err.toString());
    });
});

window.onload = function () {
    const display = document.getElementById('time');
    const form = document.getElementById("formCerrarSesionAutomaticamente");
    startTimer(idleTimeout, display, form);
};

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/userActivityHub")
    .build();

connection.start().then(function () {
    console.log("SignalR connection established.");
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveUserClicked", function () {
    resetTimer();
});
