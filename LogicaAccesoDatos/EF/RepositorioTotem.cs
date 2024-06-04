using LogicaAccesoDatos.EF.Excepciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioTotem : IRepositorioTotem
    {
        private LibreriaContext _context;
        public RepositorioTotem(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(Totem obj)
        {

            try
            {
                if (obj == null) { throw new NullOrEmptyException("No se recibio el usuario"); }
                obj.Validar();
                obj.Id = 0;
               
                _context.Totems.Add(obj);
                _context.SaveChanges();
            }
            catch (UsuarioException)
            {
                throw;

            }
            catch (NullOrEmptyException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor al agregar un nuevo totem");
            }
        }

        public void Delete(int id)
        {
            try
            {

                var totem = GetPorId(id);
                if (totem == null) { throw new Exception("No se encontró totem"); }
                _context.Totems.Remove(totem);

                _context.SaveChanges();

            }
            catch (Exception) { throw; }
        }

        public Totem GetTotemPorUsr(string usr) {
            try
            {
                if (String.IsNullOrEmpty(usr)) {

                    throw new NullOrEmptyException("No se recibio nombre de usuario");
                }


                Totem totem = _context.Totems.FirstOrDefault(tot => tot.NombreUsuario == usr);

                if(totem == null) { throw new NotFoundException(); }    

                return totem;

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

                throw new ServerErrorException("Error del servidor al obtener el totem por usuario");
            }
        }

        public IEnumerable<Totem> GetAll()
        {
            try
            {
                IEnumerable<Totem> totems = new List<Totem>();  
                totems = _context.Totems.Include(tot => tot.Accesos).ToList();
                return totems;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public Totem GetPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new NullOrEmptyException("No se recibio id");
                }//retorna el totem con todas sus sesiones y todos sus accesos
                var totem = _context.Totems.Include(tot => tot.Accesos).FirstOrDefault(tot => tot.Id == id);
                   
                if (totem == null)
                {
                    throw new NotFoundException("No se encontro ninguna totem con ese id");
                }
                return totem;

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

                throw new ServerErrorException("Error del servidor al obtener el totem por id");
            }
        }

        public void Update(Totem obj)
        {
            try
            {
                if (obj == null) { throw new NullOrEmptyException("No se recibio totem para editar"); }
                obj.Validar();

                _context.Totems.Update(obj);
                _context.SaveChanges();
            }
            catch (UsuarioException)
            {
                throw;
            }
            catch (NullOrEmptyException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor al actualizar los datos del totem");
            }
        }
        

        

        

      
    }
}
