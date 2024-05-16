﻿using LogicaAccesoDatos.EF.Config;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class LibreriaContext:DbContext
    {


        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; } 
        public DbSet<Recepcionista> Recepcionistas { get; set; }
        public DbSet<Administrador> Administradores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
            @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog=libreriaProyecto; Integrated Security=True;"
            );
        }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());//aca valido que no se repitan nombres de usuario

            
   
            base.OnModelCreating(modelBuilder);
        }


    }
}
