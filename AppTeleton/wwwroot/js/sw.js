




self.addEventListener('push', e => {

    self.registration.showNotification("Tituloo", { body: e.data.text() })

})

self.addEventListener('notificationclick', function (event) {
    event.notification.close();
});

