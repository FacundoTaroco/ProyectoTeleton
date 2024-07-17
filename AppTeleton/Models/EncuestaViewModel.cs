using LogicaNegocio.Entidades;

namespace AppTeleton.Models
{
    public class EncuestaViewModel
    {
        public int TratamientoId { get; set; }
        public string TratamientoNombre { get; set; }
        public string PacienteNombre { get; set; }
        public int SatisfaccionGeneral { get; set; } // Puntuación de 1 a 5
        public string Comentarios { get; set; }

        public EncuestaViewModel() { }

        public EncuestaViewModel(int tratamientoId, string tratamientoNombre, string pacienteNombre, int satisfaccionGeneral, string comentarios)
        {
            TratamientoId = tratamientoId;
            TratamientoNombre = tratamientoNombre;
            PacienteNombre = pacienteNombre;
            SatisfaccionGeneral = satisfaccionGeneral;
            Comentarios = comentarios;
        }
    }
}
