using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioAdministrador:IRepositorioAdministrador
    {
        private LibreriaContext _context;
        public RepositorioAdministrador(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(Administrador obj)
        {

            try
            {
                if (obj == null) { throw new Exception("No se recibio el admin"); }//hacer algunas excepciones personalizadas 
                obj.Validar();
                obj.Id = 0;
              
                ValidarUnique(obj);
                _context.Administradores.Add(obj);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ValidarUnique(Administrador obj) {
            try
            {
                foreach (Administrador a in GetAll()) {

                    if (a.NombreUsuario.Equals(obj.NombreUsuario)) {

                        throw new Exception("El administrador ya existe, ingrese otro nombre de usuario");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
       
        }

        public void Delete(int id)
        {
            try
            {

                var admin = GetPorId(id);
                if (admin == null) { throw new Exception("No se encontro admin"); }
                _context.Administradores.Remove(admin);

                _context.SaveChanges();

            }
            catch (Exception) { throw; }
        }

        public IEnumerable<Administrador> GetAll()
        {
            try
            {
                IEnumerable<Administrador> admins = _context.Administradores.ToList();
                return admins;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public Administrador GetPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("No se recibio id");
                }
                var admin = _context.Administradores.FirstOrDefault(adm => adm.Id == id);
                if (admin == null)
                {
                    throw new Exception("No se encontro ningun admin con esa cedula");
                }
                return admin;

            }
            catch (Exception) // Excepciones personalizadaaas
            {

                throw;
            }
        }

        public void Update(Administrador obj)
        {
            try
            {
                if (obj == null) { throw new Exception("No se recibio recepcionista para editar"); }
                obj.Validar();

                _context.Administradores.Update(obj);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
