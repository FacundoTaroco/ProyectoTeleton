using LogicaNegocio.Entidades;

namespace AppTeleton.Models
{
    public class PacienteIndexViewModel
    {

        public Notificacion Notificacion { get; set; }


        public PacienteIndexViewModel(Notificacion noti) {
        
            Notificacion = noti;    
        }
    }
}
