using LogicaNegocio.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioCitaMedica
    {
        //Task<IEnumerable<CitaMedicaDTO>> ObtenerCitasMedicasDelDiaAsync(DateTime fecha);
        Task<IEnumerable<CitaMedicaDTO>> ObtenerCitas();
        Task ActualizarEstadoLlegadaAsync(int idCita, string llego);
    }
}
