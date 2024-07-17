function suscribirse() {
    // Implementación para suscripción a notificaciones
}

function hideNotificationBlock() {
    // Oculta el bloque de notificaciones
    $(".alert-info").hide();

    // Muestra el bloque de encuesta después de 5 segundos
    setTimeout(function () {
        $("#encuestaBlock").show();
    }, 5000);
}

function irAEncuesta() {
    // Redirecciona a la página de agregar encuesta
    window.location.href = "/Encuesta/Add"; // Ajustar la URL según la estructura de tu proyecto
}

function reaparecer() {
    // Oculta el bloque de encuesta y lo hace reaparecer después de 5 segundos
    $("#encuestaBlock").hide();
    setTimeout(function () {
        $("#encuestaBlock").show();
    }, 5000);
}

// Mostrar el bloque de encuesta al cargar la página
$(document).ready(function () {
    $("#encuestaBlock").show();
});
