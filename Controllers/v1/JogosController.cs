using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


using DIO.Avanade.API.Jogos.ViewModel;
using DIO.Avanade.API.Jogos.InputModel;
using DIO.Avanade.API.Jogos.Services;
using DIO.Avanade.API.Jogos.Exceptions;

namespace DIO.Avanade.API.Jogos.Controllers.v1{

    [Route("api/v1/[controller]")]
    [ApiController]

    public class JogosController : ControllerBase{

        private readonly iJogoService _jogoservice;

        public JogosController(iJogoService jogoService){

            _jogoservice = jogoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5){

            var jogos = await _jogoservice.Obter(pagina, quantidade);

            if(jogos.Count() == 0)
                return NoContent();

            return Ok();
        }

        [HttpGet("IDJogo:guid")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid IDJogo){

            var jogo = await _jogoservice.Obter(IDJogo);

            if(jogo == null)
                return NoContent();

            return Ok(jogo);
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel){

            try{

                var jogo = await _jogoservice.Inserir(jogoInputModel);
                
                return Ok(jogo);
            }
            catch(JogoJaCadastradoException ex){

                return UnprocessableEntity("Já existe um jogo com esse nome para essa produtora.");
            }
        }

        [HttpPut("{IDJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid IDJogo, [FromBody] JogoInputModel jogoInputModel){

            try{

                await _jogoservice.Atualizar(IDJogo, jogoInputModel);

                return Ok();
            }
            catch(JogoNaoCadastradoException ex){

                return NotFound("Esse jogo não existe.");
            }
        }

        [HttpPatch("{IDJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid IDJogo, [FromRoute] double preco){

            try{

                await _jogoservice.Atualizar(IDJogo, preco);

                return Ok();
            }
            catch(JogoNaoCadastradoException ex){

                return NotFound("Esse jogo não existe.");
            }
        }

        [HttpDelete("{IDJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid IDJogo){

            try{

                await _jogoservice.Remover(IDJogo);
                
                return Ok();
            }
            catch(JogoNaoCadastradoException ex){

                return NotFound("Esse jogo não existe.");
            }
        }
    }
}