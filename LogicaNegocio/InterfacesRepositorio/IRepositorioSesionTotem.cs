using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioSesionTotem
    {
        public SesionTotem AgregarSesion(SesionTotem sesion);
        public void CerrarSesion(SesionTotem sesion);


        public SesionTotem GetSesionPorId(int id);
        public IEnumerable<SesionTotem> GetSesionesDeTotem(int idTotem);
        public IEnumerable<SesionTotem> GetSesionesAbiertasDeTotem(int idTotem);
        public IEnumerable<SesionTotem> GetSesionesDeFecha(int idTotem, DateTime fecha);
    }
}
