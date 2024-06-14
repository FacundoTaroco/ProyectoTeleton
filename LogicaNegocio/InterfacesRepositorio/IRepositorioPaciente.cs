using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioPaciente:IRepositorio<Paciente>
    {
        public Paciente GetPacientePorCedula(string cedula);
        public Paciente GetPacientePorUsuario(string usuario);

        public bool ExistePaciente(string usuario);


    }
}
