﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.querySelector('#navbar-toggler').addEventListener("click", navBarMobile)

function navBarMobile(){
    var iconoNavCerrado = document.querySelector('#iconoNavCerrado');
    var iconoNavAbierto = document.querySelector('#iconoNavAbierto');


    if (iconoNavCerrado.classList.contains('hidden')) {
        iconoNavCerrado.classList.remove('hidden')
        iconoNavAbierto.classList.add('hidden')
    } else {

        iconoNavCerrado.classList.add('hidden')
        iconoNavAbierto.classList.remove('hidden')

    }
}


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


async function suscribirse(){
    checkPermission()
    await requestNotificationPermission()
    let reg = await registerSW()

    await delay(500);

    if (reg.installing) {
        console.log('Service worker installing');
    } else if (reg.waiting) {
        console.log('Service worker installed');
    } else if (reg.active) {
        console.log('Service worker active');
    }

        reg.pushManager.getSubscription()
            .then(function (subscription) {
                if (subscription == null) {
                    subscribeUser(reg);
                }
            })
    
}
async function subscribeUser(swReg) {
    const applicationServerKey = urlBase64ToUint8Array("BKbHbSWuzzAuiXHQ9iS1yVSI0uly-gzp-EKLr-qQOaYFsMlMfP4_TybiwMxNc7oeln31U9MXdIQlMCQ68-51sT0");
    swReg.pushManager.subscribe({
        userVisibleOnly: true,
        applicationServerKey: applicationServerKey
    })
        .then(function (subscription) {
            console.log('Usuario suscrito:', subscription);
            guardarNotificacionServidor(subscription);
            verSiEsconderMensajeNotificacion();
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
const hideNotificationBlock = () =>{
    document.getElementById("notificationBlock").style.display = "none";
}

async function verSiEsconderMensajeNotificacion() {

    if ('serviceWorker' in navigator && 'PushManager' in window) {

        let regs = await navigator.serviceWorker.getRegistrations();

        if (regs.length != 0) {
            let reg = regs[0]
            reg.pushManager.getSubscription().then(function (subscription) {
                if (subscription) {
                    hideNotificationBlock();
                }
            })

        }

    }
}

function delay(time) {
    return new Promise(resolve => setTimeout(resolve, time));
}