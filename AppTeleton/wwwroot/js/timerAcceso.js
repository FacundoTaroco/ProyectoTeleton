function startTimer(duration, display, form) {
    var timer = duration, minutes, seconds;
   
    setInterval(function () {
        minutes = parseInt(timer / 60, 10);
        seconds = parseInt(timer % 60, 10);
        if (seconds == 0) {
            form.submit();
        }
        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;
       

        display.textContent = minutes + ":" + seconds;

        if (--timer < 0) {
            timer = duration;
        }
        

    }, 1000);
}

window.onload = function () {
    var segundos = 10,
        display = document.querySelector('#time');
    form = document.querySelector("#formCerrarSesionAutomaticamente");
    startTimer(segundos, display,form);
};