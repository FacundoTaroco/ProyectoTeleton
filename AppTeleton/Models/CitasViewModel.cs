using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using System;

namespace AppTeleton.Models
{
    public class CitasViewModel
    {
        public List<CitasPorFechaViewModel> citasPorFecha { get; set; } = new List<CitasPorFechaViewModel>();
        public Notificacion? Notificacion { get; set; } = null;
        public CitasViewModel() { }
        public CitasViewModel(List<CitasPorFechaViewModel> citas)
        {
            this.citasPorFecha = citas;
        }
        public CitasViewModel(List<CitasPorFechaViewModel> citas, Notificacion notificacion)
        {
            this.citasPorFecha = citas;
            this.Notificacion = notificacion;
        }
        public CitasViewModel(Notificacion notificacion)
        {
          
            this.Notificacion = notificacion;
        }


        public void CargarModelo(IEnumerable<CitaMedicaDTO> citas)
        {

            if (citas.Count() > 0) {
                citas = citas.OrderBy(c => c.Fecha).ThenBy(c=>c.HoraInicio).ToList();
                DateOnly fechaAnterior = DateOnly.FromDateTime(citas.First().Fecha);
                CitasPorFechaViewModel modelo = new CitasPorFechaViewModel();


            foreach (CitaMedicaDTO cita in citas) {

                    DateOnly fechaCita = DateOnly.FromDateTime(cita.Fecha);

                    if (fechaCita.Equals(fechaAnterior))
                    {
                        modelo.Fecha = fechaAnterior;
                        modelo.CitasDeFecha.Add(cita);
                    }
                    else {

                        citasPorFecha.Add(modelo);
                        modelo = new CitasPorFechaViewModel();
                        modelo.CitasDeFecha.Add(cita);
                    }
                    fechaAnterior = DateOnly.FromDateTime(cita.Fecha);
            }
                citasPorFecha.Add(modelo);
            }

            
        }

        
    }
}
