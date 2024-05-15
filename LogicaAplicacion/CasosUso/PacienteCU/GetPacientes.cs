﻿using LogicaAccesoDatos.EF;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.PacienteCU
{
    public class GetPacientes
    {
        private IRepositorioPaciente _repo;
        public GetPacientes(IRepositorioPaciente repo)
        {
            _repo = repo;
        }


        public IEnumerable<Paciente> GetAll() {
            try
            {
                return _repo.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        
        }

        public Paciente GetPacientePorCedula(string cedula) {
            try
            {
                return _repo.GetPacientePorCedula(cedula);
            }
            catch (Exception)
            {

                throw;
            }
       
        }

        public Paciente GetPacientePorId(int id)
        {
            try
            {
                return _repo.GetPacientePorId(id);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
