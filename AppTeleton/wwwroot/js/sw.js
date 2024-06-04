




self.addEventListener('push', e => {

    if (!(self.Notification && self.Notification.permission === 'granted')) {
        return;
    }

    let datos = {};
    if (e.data) {
        datos = e.data.json();
    }


    var title = datos.titulo;
    var message = datos.mensaje;
    var icon = "../images/TeletonIcono.png"; //ESTO DESPUES VER DE CAMBIARLO Y HACERLO ADAPTABLE


    e.waitUntil(self.registration.showNotification(title, {
        body: message,
        icon: icon,
        badge: icon
    }));

})

self.addEventListener('notificationclick', function (event) {
    event.notification.close();
});

