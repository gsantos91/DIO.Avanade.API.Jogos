using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIO.Avanade.API.Jogos.Entities{

    public class Jogo{
        
        public Guid ID {get; set;}
        public string Nome {get; set;}
        public string Produtora {get; set;}
        public double Preco {get; set;}
    }
}