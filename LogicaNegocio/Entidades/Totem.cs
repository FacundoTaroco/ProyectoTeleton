using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Totem : Usuario
    {
        private static Totem instance = null;
        private static readonly object padlock = new object();

        // Constructor privado para patrón singleton
        private Totem()
        {
            this.Rol = "Totem";
            this.Nombre = "Totem Principal";
            this.NombreUsuario = "totem";
            this.Contrasenia = "totem123";
        }

        // Constructor público requerido por Entity Framework
        public Totem(bool forEF = false)
        {
        }

        public static Totem Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Totem();
                    }
                    return instance;
                }
            }
        }

        //public void RegistrarAcceso(AccesoTotem acceso)
        //{
        //    using (var dbContext = new YourDbContext()) // Reemplaza YourDbContext con tu contexto de base de datos
        //    {
        //        try
        //        {
        //            dbContext.AccesosTotem.Add(acceso);
        //            dbContext.SaveChanges();
        //            Console.WriteLine("Acceso registrado correctamente.");
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error al registrar el acceso: {ex.Message}");
        //        }
        //    }
        //}

        //public static void RegistroTotem()
        //{
        //    using (var dbContext = new YourDbContext()) // Reemplaza YourDbContext con tu contexto de base de datos
        //    {
        //        try
        //        {
        //            var totemUser = new Totem(); // Crear una instancia de Totem (singleton)

        //            // Agregar el usuario totem a la base de datos
        //            dbContext.Usuarios.Add(totemUser);
        //            dbContext.SaveChanges();

        //            Console.WriteLine("Usuario Totem registrado correctamente.");
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error al registrar el usuario Totem: {ex.Message}");
        //        }
        //    }
        //}

    }
}


