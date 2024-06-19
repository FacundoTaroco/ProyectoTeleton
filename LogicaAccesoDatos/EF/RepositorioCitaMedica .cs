using LogicaAccesoDatos.EF.Excepciones;
using LogicaNegocio.DTO;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioCitaMedica : IRepositorioCitaMedica
    {
        private readonly LibreriaContext _context;

        public RepositorioCitaMedica(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CitaMedicaDTO>> ObtenerCitasMedicasDelDiaAsync(DateTime fecha)
        {
            return await _context.CitasMedicas
                .Where(c => c.Fecha.Date == fecha.Date)
                .Select(c => new CitaMedicaDTO
                {
                    PkAgenda = c.PkAgenda,
                    Cedula = c.Cedula,
                    NombreCompleto = c.NombreCompleto,
                    Servicio = c.Servicio,
                    Fecha = c.Fecha,
                    HoraInicio = c.HoraInicio,
                    Tratamiento = c.Tratamiento,
                    Estado = c.Estado
                })
                .ToListAsync();
        }

        public async Task ActualizarEstadoLlegadaAsync(int idCita, string estado)
        {
            var cita = await _context.CitasMedicas.FirstOrDefaultAsync(c => c.PkAgenda == idCita);
            if (cita != null)
            {
                cita.Estado = estado;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException($"No se encontró la cita médica con Id {idCita}");
            }
        }
    }
}
