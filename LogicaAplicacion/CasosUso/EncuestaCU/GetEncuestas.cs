using LogicaAplicacion.Servicios;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.EncuestaCU
{
    public class GetEncuestas
    {
        SolicitarEncuestasService _solicitarEncuestasService;
        public GetEncuestas(SolicitarEncuestasService encuestasService)
        {
            _solicitarEncuestasService = encuestasService;
        }


        public async Task<IEnumerable<Encuesta>> ObtenerEncuestas()
        {
            try
            {
                IEnumerable<Encuesta> encuestas = await _solicitarEncuestasService.ObtenerEncuestas();
                return encuestas;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IEnumerable<Encuesta>> ObtenerEncuestasPorCedula(string cedula)
        {
            try
            {
                IEnumerable<Encuesta> encuestas = await _solicitarEncuestasService.ObtenerEncuestasPorCedula(cedula);
                return encuestas;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
