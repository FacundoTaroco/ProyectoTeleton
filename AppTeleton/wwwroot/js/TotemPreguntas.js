

function toggleAnswer(id) {
    var answer = document.getElementById('answer-' + id);
    if (answer.classList.contains('show')) {
        answer.classList.remove('show');
    } else {
        
        answer.classList.add('show');
        reproducirMensaje(answer.textContent)
    }
}



function reproducirMensaje(mensaje) {
    window.speechSynthesis.cancel();
    const audio = new SpeechSynthesisUtterance(mensaje);

    audio.pitch = 1;
    audio.rate = 1;
    audio.volume = 1;

    window.speechSynthesis.speak(audio);
}