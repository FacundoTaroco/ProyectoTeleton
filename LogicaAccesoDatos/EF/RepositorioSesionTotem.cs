using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioSesionTotem : IRepositorioSesionTotem
    {

        private LibreriaContext _context;
        public RepositorioSesionTotem(LibreriaContext context)
        {
            _context = context;
        }
        public void Add(SesionTotem obj)
        {
            try
            {
                if (obj == null) { throw new Exception("No se recibio sesion"); }//hacer algunas excepciones personalizadas 
                obj.Id = 0;
                //Validar totem unique con los config!!

                _context.SesionesTotem.Add(obj);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SesionTotem> GetAll()
        {
            throw new NotImplementedException();
        }

        public SesionTotem GetPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SesionTotem> getSesionesAbiertasDeFecha(int idTotem, DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SesionTotem> getSesionesAbiertasDeTotem(int idTotem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SesionTotem> getSesionesDeTotem(int idTotem)
        {
            throw new NotImplementedException();
        }

        public void Update(SesionTotem obj)
        {
            throw new NotImplementedException();
        }
    }
}
