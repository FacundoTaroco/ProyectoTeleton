// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const urlBase64ToUint8Array = base64String => {
    const padding = '='.repeat((4 - (base64String.length % 4)) % 4)
    const base64 = (base64String + padding)
        .replace(/\-/g, '+')
        .replace(/_/g, '/');

    const rawData = atob(base64);
    const outputArray = new Uint8Array(rawData.length);

    for (let i = 0; i < rawData.length; i++) {
        outputArray[i] = rawData.charCodeAt(i);

    }

    return outputArray;
}
function base64Encode(arrayBuffer) {
    return btoa(String.fromCharCode.apply(null, new Uint8Array(arrayBuffer)))
}


const suscribirse = async () => {
    checkPermission()
    await requestNotificationPermission()
    let reg = await registerSW()

    if (reg.active.state == "activated") {
        reg.pushManager.getSubscription()
        .then(function (subscription) {
            if (subscription == null) {
                subscribeUser(reg);
            }
        })

    }
}
function subscribeUser(swReg) {
    const applicationServerKey = urlBase64ToUint8Array("BKbHbSWuzzAuiXHQ9iS1yVSI0uly-gzp-EKLr-qQOaYFsMlMfP4_TybiwMxNc7oeln31U9MXdIQlMCQ68-51sT0");
    swReg.pushManager.subscribe({
        userVisibleOnly: true,
        applicationServerKey: applicationServerKey
    })
        .then(function (subscription) {
            console.log('Usuario suscrito:', subscription);
            guardarNotificacionServidor(subscription);
        })
        .catch(function (error) {
            console.error('Error al suscribir al usuario', error);
        });
}




const checkPermission = () => {
    if (!('serviceWorker' in navigator)) {
        throw new Error("no hay soporte para el servicio")
    }
    if (!('Notification') in window) {
        throw new Error("no hay soporte para la api de notificaciones");

    }
}
const registerSW = async () => {
    const registration = await navigator.serviceWorker.register('../js/sw.js');
    return registration;
}
const requestNotificationPermission = async () => {
    const permission = await Notification.requestPermission();

    if (permission !== 'granted') {
        throw new Error("Permiso de notificaciones no aceptado")
    }
    }






    const guardarNotificacionServidor = (suscripcion) => {

        let p256dh = base64Encode(suscripcion.getKey('p256dh'));
        let auth = base64Encode(suscripcion.getKey('auth'));


    document.getElementById("endpoint").value = suscripcion.endpoint;
    document.getElementById("p256dh").value = p256dh;
    document.getElementById("auth").value = auth;
    document.getElementById("subscriptionForm").submit();


}
const hideNotificationBlock =() =>{
    document.getElementById("notificationBlock").style.display = "none";
}