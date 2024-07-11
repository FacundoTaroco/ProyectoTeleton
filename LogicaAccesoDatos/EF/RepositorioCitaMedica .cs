using LogicaAccesoDatos.EF.Excepciones;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task<IEnumerable<CitaMedicaDTO>> ObtenerCitasPorCedula(string cedula)
        {
            return await _context.CitasMedicas
                .Where(c => c.Cedula == cedula) // Filtrar por cedula
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

        public async Task ActualizarCita(CitaMedicaDTO cita)
        {
            var citaEntity = await _context.CitasMedicas.FindAsync(cita.PkAgenda);
            if (citaEntity != null)
            {
                citaEntity.Detalles = cita.Detalles;
                _context.CitasMedicas.Update(citaEntity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException($"No se encontró la cita médica con Id {cita.PkAgenda}");
            }
        }

        public async Task<CitaMedicaDTO> ObtenerCitaPorId(int id)
        {
            return await _context.CitasMedicas
                .Where(c => c.PkAgenda == id)
                .Select(c => new CitaMedicaDTO
                {
                    PkAgenda = c.PkAgenda,
                    Cedula = c.Cedula,
                    NombreCompleto = c.NombreCompleto,
                    Servicio = c.Servicio,
                    Fecha = c.Fecha,
                    HoraInicio = c.HoraInicio,
                    Tratamiento = c.Tratamiento,
                    Estado = c.Estado,
                    Detalles = c.Detalles // Asumiendo que existe un campo Detalles en tu entidad
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CitaMedicaDTO> ObtenerCitaPorTratamiento(string tratamiento)
        {
            return await _context.CitasMedicas
                .Where(c => c.Tratamiento == tratamiento)
                .Select(c => new CitaMedicaDTO
                {
                    PkAgenda = c.PkAgenda,
                    Cedula = c.Cedula,
                    NombreCompleto = c.NombreCompleto,
                    Servicio = c.Servicio,
                    Fecha = c.Fecha,
                    HoraInicio = c.HoraInicio,
                    Tratamiento = c.Tratamiento,
                    Estado = c.Estado,
                    Detalles = c.Detalles // Asumiendo que existe un campo Detalles en tu entidad
                })
                .FirstOrDefaultAsync();
        }

        public void Add(CitaMedica obj)
        {

            try
            {
                if (obj == null) { throw new NullOrEmptyException("No se recibio la cita médica"); }
                obj.Validar();
                obj.Id = 0;
                ValidarUnique(obj);
                _context.CitasMedicas.Add(obj);
                _context.SaveChanges();

            }
            catch (NullOrEmptyException)
            {
                throw;
            }
            catch (PreguntaFrecException)
            {
                throw;
            }
            catch (UniqueException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor, algo fallo al agregar la pregunta frecuente");
            }
        }

        public void Delete(int id)
        {
            try
            {
                var citaMedica = GetPorId(id);
                _context.CitasMedicas.Remove(citaMedica);
                _context.SaveChanges();
            }
            catch (NullOrEmptyException) { throw; }
            catch (NotFoundException) { throw; }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor al eliminar la recepcionista");
            }
        }

        public void Update(CitaMedica obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new NullOrEmptyException("No se recibió cita médica para editar");
                }

                obj.Validar();

                // Verificar la existencia de la cita
                var existingCita = _context.CitasMedicas.AsNoTracking().FirstOrDefault(c => c.PkAgenda == obj.PkAgenda);
                if (existingCita == null)
                {
                    throw new NotFoundException("No se encontró cita médica a editar");
                }

                // Actualizar la entidad
                _context.CitasMedicas.Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (NullOrEmptyException)
            {
                throw;
            }
            catch (UsuarioException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor al actualizar la cita médica");
            }
            /*try
            {
                if (obj == null)
                {
                    throw new Exception("No se recibió cita médica para editar");
                }

                obj.Validar();

                var existingCita= _context.CitasMedicas.FirstOrDefault(c => c.PkAgenda == obj.PkAgenda);

                if (existingCita == null)
                {
                    throw new NotFoundException("No se encontró cita médica a editar");
                }
                _context.Entry(existingCita).CurrentValues.SetValues(obj);
                _context.SaveChanges();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (UsuarioException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }*/
        }

        public void ValidarUnique(CitaMedica obj)
        {
            try
            {
                foreach (CitaMedica a in _context.CitasMedicas.ToList())
                {

                    if (a.PkAgenda.Equals(obj.PkAgenda))
                    {
                        throw new UniqueException("La cita médica ya existe, ingrese otra cita médica");
                    }
                }
            }
            catch (UniqueException)
            {

                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<CitaMedica> GetAll()
        {
            try
            {
                IEnumerable<CitaMedica> CitasMedicas = new List<CitaMedica>();
                CitasMedicas = _context.CitasMedicas.ToList();
                return CitasMedicas;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Fallo el servidor al obtener las preguntas frecuentes");
            }
        }

        public CitaMedica GetPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new NullOrEmptyException("No se recibio id");
                }
                var citasMedica = _context.CitasMedicas.FirstOrDefault(cita => cita.Id == id);
                if (citasMedica == null)
                {
                    throw new NotFoundException("No se encontro ninguna pregunta frecunte con el id ingresado");
                }
                return citasMedica;

            }
            catch (NullOrEmptyException)
            {
                throw;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
