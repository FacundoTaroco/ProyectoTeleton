using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    //Repositorio generico con las acciones basicas que tienen que tener los repositorios en acceso a datos
    public interface IRepositorio<T>
    {
        public void Add(T obj);
        public void Update(T obj);
        public void Delete(int id);
        public IEnumerable<T> GetAll();
        public T GetPorId(int id);
    }
}
