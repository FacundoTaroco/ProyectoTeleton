using LogicaAccesoDatos.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.PacienteCU
{
    public class ABMPacientes
    {
        private IRepositorioPaciente _repo;
        public ABMPacientes(IRepositorioPaciente repo)
        {
            _repo = repo;
        }


        public void AltaPaciente(Paciente paciente) {
            try
            {
                _repo.Add(paciente);
            }
            catch (Exception)
            {

                throw;
            }
        
        }
        public void BajaPaciente(int id) {
            try
            {
                _repo.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }
        
        }

        public void ModificarPaciente(Paciente paciente) {
            try
            {
                _repo.Update(paciente);
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
