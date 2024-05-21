using LogicaAplicacion.Servicios;
using LogicaNegocio.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CitaCU
{
    public class GetCitas
    {

        SolicitarCitasService _solicitarCitasService;
        public GetCitas(SolicitarCitasService citasService) {
            _solicitarCitasService = citasService;
        }


        public async Task<IEnumerable<CitaMedicaDTO>> ObtenerCitas() {
            try
            {
                IEnumerable<CitaMedicaDTO> citas = await _solicitarCitasService.ObtenerCitas();
                return citas;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IEnumerable<CitaMedicaDTO>> ObtenerCitasPorCedula(string cedula)
        {
            try
            {
                IEnumerable<CitaMedicaDTO> citas = await _solicitarCitasService.ObtenerCitasPorCedula(cedula);
                return citas;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
