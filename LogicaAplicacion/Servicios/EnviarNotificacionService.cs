using LogicaAplicacion.CasosUso.DispositivoUsuarioCU;
using LogicaAplicacion.CasosUso.NotificacionCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaAplicacion.CasosUso.UsuarioCU;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using Microsoft.Extensions.Configuration;
using WebPush;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace LogicaAplicacion.Servicios
{
    public class EnviarNotificacionService
    {
        private GetUsuarios _getUsuario;
        private GetRecepcionistas _getRecepcionistas;
        private GetPacientes _getPacientes;
        private GetDispositivos _getDispositivos;
        private ABNotificacion _ABNotificacion;
        private BorrarDispositivoNotificacion _borrarDispositivo;
        private IConfiguration _config;
        public EnviarNotificacionService(GetUsuarios getUsuarios, BorrarDispositivoNotificacion borrarDispositivo, ABNotificacion aBNotificacion, IConfiguration configuration, GetRecepcionistas getRecepcionistas, GetPacientes getPacientes, GetDispositivos getDispositivos)
        {
            _getRecepcionistas = getRecepcionistas;
            _getPacientes = getPacientes;
           
            _getDispositivos = getDispositivos;
            _config = configuration;
            _ABNotificacion = aBNotificacion;
            _borrarDispositivo = borrarDispositivo;
            _getUsuario = getUsuarios;
        }
        public void Enviar(string titulo, string mensaje, int idUsuario) {
            try
            {
                Usuario usr = _getUsuario.GetUsuario(idUsuario);
                IEnumerable<DispositivoNotificacion> dispositivos = _getDispositivos.getDispositivosPacientePorId(idUsuario);
                Notificacion notificacionAGuardar = new Notificacion();
                notificacionAGuardar.Titulo = titulo;
                notificacionAGuardar.Mensaje = mensaje;
                notificacionAGuardar.Usuario = usr;
                notificacionAGuardar.IdUsuario = usr.Id;
                _ABNotificacion.Add(notificacionAGuardar);

                if (dispositivos.Count() > 0)
                {

                    NotificacionDTO payload = new NotificacionDTO(titulo, mensaje);


                    string vapidPublicKey = _config["ClavesNotificaciones:PublicKey"];
                    string vapidPrivateKey = _config["ClavesNotificaciones:PrivateKey"];
                    foreach (DispositivoNotificacion dispositivo in dispositivos)
                    {
                        try
                        {
                            PushSubscription pushSubscription = new PushSubscription(dispositivo.Endpoint, dispositivo.P256dh, dispositivo.Auth);
                            VapidDetails vapidDetails = new VapidDetails("mailto:lucasahre05@gmail.com", vapidPublicKey, vapidPrivateKey);
                            WebPushClient webPushClient = new WebPushClient();
                            webPushClient.SetVapidDetails(vapidDetails);
                            string payloadToken = JsonConvert.SerializeObject(payload, new JsonSerializerSettings()
                            {
                                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                            });
                            webPushClient.SendNotification(pushSubscription, payloadToken);
                        }
                        catch (WebPushException)
                        {
                            //si tira esta exception es porque el dispositivo ya no es valido entonces lo borramos de la BD  
                            _borrarDispositivo.Delete(dispositivo.Id);
                        }
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }

        }

        public void EnviarATodos(string titulo, string mensaje) {

            try
            {
                IEnumerable<Paciente> usuariosACargarNotificacion = _getPacientes.GetAll();
                foreach (Usuario u in usuariosACargarNotificacion)
                {
                    //aunque el paciente NO tenga dispositivos registrados(es decir no le van a llegar las notificaciones push) SI guardamos las notificaciones a su perfil para
                    //que las pueda ver en su hub de notificaciones cuando inicie sesion
                    Notificacion notificacion = new Notificacion(titulo, mensaje, u);
                    _ABNotificacion.Add(notificacion);
                }


                IEnumerable<DispositivoNotificacion> dispositivos = _getDispositivos.getAllDispositivos();
                if (dispositivos.Count() > 0)
                {
                    NotificacionDTO payload = new NotificacionDTO(titulo, mensaje);
                    string vapidPublicKey = _config["ClavesNotificaciones:PublicKey"];
                    string vapidPrivateKey = _config["ClavesNotificaciones:PrivateKey"];
                    foreach (DispositivoNotificacion dispositivo in dispositivos)
                    {
                        //EVENTUALMENTE HACER TAMBIEN PARA RECEPCIONISTA
                        if (dispositivo.Usuario is Paciente)
                        {
                            try
                            {
                                PushSubscription pushSubscription = new PushSubscription(dispositivo.Endpoint, dispositivo.P256dh, dispositivo.Auth);
                                VapidDetails vapidDetails = new VapidDetails("mailto:lucasahre05@gmail.com", vapidPublicKey, vapidPrivateKey);
                                WebPushClient webPushClient = new WebPushClient();
                                webPushClient.SetVapidDetails(vapidDetails);
                                string payloadToken = JsonConvert.SerializeObject(payload, new JsonSerializerSettings()
                                {
                                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                });
                                webPushClient.SendNotification(pushSubscription, payloadToken);
                            }
                            catch (WebPushException)
                            {
                                //si tira esta exception es porque el dispositivo ya no es valido entonces lo borramos de la BD  
                                _borrarDispositivo.Delete(dispositivo.Id);
                            }
                        }
                    }
                }
            }

            catch (Exception)
            {

                throw;
            }


        }

    }
}
