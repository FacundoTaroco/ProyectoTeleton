using LogicaAccesoDatos.EF.Excepciones;
using LogicaNegocio.DTO;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.Data.SqlClient;
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
        private LibreriaContext _context;

        public RepositorioCitaMedica(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CitaMedicaDTO>> ObtenerCitas()
        {
            return await _context.CitasMedicas
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

        public async Task<IEnumerable<CitaMedicaDTO>> ObtenerCitasPorCedulaYFecha(string cedula, DateTime fecha)
        {
            return await _context.CitasMedicas
                .Where(c => c.Cedula == cedula && c.Fecha.Date == fecha.Date) // Filtrar por cedula y fecha
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

        /*public async Task ActualizarEstadoLlegadaAsync(string cedula, string estado)
        {
            var cita = await _context.CitasMedicas.FirstOrDefaultAsync(c => c.Cedula == cedula);
            if (cita != null)
            {
                cita.Estado = estado;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException($"No se encontró la cita médica con Id {cita.Id}");
            }
        }*/
        public async Task RecepcionarPacienteAsync(int pkAgenda)
        {
            var param = new SqlParameter("@pkAgenda", pkAgenda);
            await _context.Database.ExecuteSqlRawAsync("EXEC RecepcionarPaciente @pkAgenda", param);
        }
    }
}
