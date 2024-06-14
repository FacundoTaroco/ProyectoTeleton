﻿using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.ChatCU
{
    public class GetChats
    {
        private IRepositorioChat _repo;
        public GetChats(IRepositorioChat repo)
        {
            _repo = repo;
        }


        public Chat GetChatAbiertoDePaciente(int idPaciente) {
            return _repo.GetChatAbiertoDePaciente(idPaciente);
        }

        public bool PacienteTieneChatAbierto(int idPaciente) {
            return _repo.PacienteTieneChatAbierto(idPaciente);
        }
    }
}
