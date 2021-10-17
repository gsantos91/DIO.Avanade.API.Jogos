using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIO.Avanade.API.Jogos.Exceptions{

    public class JogoNaoCadastradoException : Exception{

        public JogoNaoCadastradoException()
            : base("Esse jogo não está cadastrado.")
        {}
    }
}