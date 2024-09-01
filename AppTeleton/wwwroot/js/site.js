//Archivo utilizado junto al service worker (sw.js) para la gestion de notificaciones y permisos y algunas funcionalidades generales de la aplicacion




document.querySelector('#navbar-toggler').addEventListener("click", navBarMobile)

//Funcion del navbar responisve
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



//funcion para convertir datos
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


//Funcion para suscribirse al servicio de notificaciones de la aplicacion
async function suscribirse(){
    checkPermission() //verifica que la aplicacion cuente con los permisos necesarios
    await requestNotificationPermission() //Solicita permiso de notificaciones
    let reg = await registerSW() //Registra el service worker

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
                    subscribeUser(reg); //suscribe al usuario
                }
            })
    
}

//funcion para suscribir a los usuarios al servicio de notificaciones
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





//Envia el dispositivo con sus credenciales al back-end para ser guardado
    const guardarNotificacionServidor = (suscripcion) => {

        let p256dh = base64Encode(suscripcion.getKey('p256dh'));
        let auth = base64Encode(suscripcion.getKey('auth'));


    document.getElementById("endpoint").value = suscripcion.endpoint;
    document.getElementById("p256dh").value = p256dh;
    document.getElementById("auth").value = auth;
    document.getElementById("subscriptionForm").submit();


}

//esconde el formulario solicitando permiso de notificaciones
function hideNotificationBlock (){
    document.getElementById("notificationBlock").style.display = "none";
}

//Verifica si el usuario ya se suscribio al servicio de notificaciones para saber si mostrar los formularios
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



