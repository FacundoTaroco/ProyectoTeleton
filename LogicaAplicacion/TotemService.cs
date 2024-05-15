using LogicaAccesoDatos.EF;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion
{
    public class TotemService
    {
        private readonly LibreriaContext _context;

        public TotemService(LibreriaContext context)
        {
            _context = context;
        }

        public void RegistrarAcceso()
        {
            var totem = Totem.Instance;
            var acceso = new AccesoTotem
            {
                FechaAcceso = DateTime.Now,
                TotemId = totem.Id
            };

            _context.AccesosTotem.Add(acceso);
            _context.SaveChanges();
        }

        public void IniciarSesion()
        {
            var totem = Totem.Instance;
            var sesion = new SesionTotem
            {
                InicioSesion = DateTime.Now,
                TotemId = totem.Id
            };

            _context.SesionesTotem.Add(sesion);
            _context.SaveChanges();
        }

        public void FinalizarSesion(int sesionId)
        {
            var sesion = _context.SesionesTotem.Find(sesionId);
            if (sesion != null)
            {
                sesion.FinSesion = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
