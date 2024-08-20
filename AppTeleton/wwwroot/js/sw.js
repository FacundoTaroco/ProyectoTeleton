
self.onnotificationclick = (event) => {
    event.stopImmediatePropagation();
    event.notification.close();

    const url = new URL(event.notification.data.url, self.location.origin).href;

    event.waitUntil(
        clients
            .matchAll({
                type: "window",
            })
            .then((clientList) => {
                for (const client of clientList) {
                    if (client.url === url && "focus" in client) return client.focus();
                }
                if (clients.openWindow) return clients.openWindow(url);
            }),
    );
};

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
    var link = datos.link;
    var icon = "../images/TeletonIcono.png"; //ESTO DESPUES VER DE CAMBIARLO Y HACERLO ADAPTABLE


    e.waitUntil(self.registration.showNotification(title, {
        body: message,
        icon: icon,
        badge: icon,
        data: {
           url: link // URL para redirigir al usuario
        }

    }));
   

})

