using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioAccesoTotem : IRepositorioAccesoTotem
    {


        private LibreriaContext _context;
        public RepositorioAccesoTotem(LibreriaContext context)
        {
            _context = context;
        }
        public void Add(AccesoTotem obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccesoTotem> GetAll()
        {
            throw new NotImplementedException();
        }

        public AccesoTotem GetPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(AccesoTotem obj)
        {
            throw new NotImplementedException();
        }
    }
}
