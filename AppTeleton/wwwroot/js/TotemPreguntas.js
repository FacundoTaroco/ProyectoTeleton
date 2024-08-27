
//Front-end de la vista de preguntas en el Totem

function toggleAnswer(id) {

    let preguntas = document.querySelectorAll(".faq-answer")
    for (let i = 0; i < preguntas.length; i++) {
        preguntas[i].classList.remove('show');
    } 

    var answer = document.getElementById('answer-' + id);
    if (answer.classList.contains('show')) {
        window.speechSynthesis.cancel();
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