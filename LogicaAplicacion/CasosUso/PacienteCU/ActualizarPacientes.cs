using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.PacienteCU  
{
    //Esta clase es la encargada de recibir a la lista de pacientes obtenida del servidor central de la teleton(o del endpoint de testing)
    //y validar si la lista de la aplicacion esta actualizada, si no lo esta se encarga de mapear o delegar el mapeo de un paciente de la teleton
    //a un usuario Paciente de la aplicacion para luego guardarlo en el contexto
    public class ActualizarPacientes
    {
        private GetPacientes GetPacientesCU;
        public ActualizarPacientes(/*Aca recibe la lista de pacientes en el formato que venga*/GetPacientes getPacientes)
        {
            GetPacientesCU = getPacientes;
        }
    }
}
