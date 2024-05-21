using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class SesionMedico
    {
        public int Id { get; set; }
        public DateTime InicioSesion { get; set; }
        public bool SesionAbierta { get; set; }
        public int MedicoId { get; set; }
        public Medico _Medico { get; set; }

        //constructores para poder hacer las sesiones
        public SesionMedico()
        {
            _Medico = Medico.Instance;
            MedicoId = _Medico.Id;
        }
        public SesionMedico(DateTime inicioSesion)
        {
            _Medico = Medico.Instance;
            MedicoId = _Medico.Id;
            InicioSesion = inicioSesion;
            SesionAbierta = true;
        }

    }
}
