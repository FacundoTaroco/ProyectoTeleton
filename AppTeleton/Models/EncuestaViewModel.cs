using LogicaNegocio.Entidades;

namespace AppTeleton.Models
{
    public class EncuestaViewModel
    {
        public int SatisfaccionGeneral { get; set; } // Puntuación de 1 a 5
        public string Comentarios { get; set; }

        public EncuestaViewModel() { }

        public EncuestaViewModel(int satisfaccionGeneral, string comentarios)
        {
            SatisfaccionGeneral = satisfaccionGeneral;
            Comentarios = comentarios;
        }
    }
}
