using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioCitaMedica : IRepositorio<CitaMedica>
    {
        //Task<IEnumerable<CitaMedicaDTO>> ObtenerCitasMedicasDelDiaAsync(DateTime fecha);
        Task<IEnumerable<CitaMedicaDTO>> ObtenerCitas();
        Task<IEnumerable<CitaMedicaDTO>> ObtenerCitasPorCedulaYFecha(string cedula, DateTime fecha);
        Task<IEnumerable<CitaMedicaDTO>> ObtenerCitasPorCedula(string cedula);
        //Task ActualizarEstadoLlegadaAsync(string cedula, string llego);
        Task RecepcionarPacienteAsync(int pkAgenda);
        Task<CitaMedicaDTO> ObtenerCitaPorTratamiento(string tratamiento); // Nuevo método
        Task<CitaMedicaDTO> ObtenerCitaPorId(int id); // Nuevo método
        Task ActualizarCita(CitaMedicaDTO cita); // Nuevo método
        
    }
}
