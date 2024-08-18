using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Services;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaService tarefaService;

        public TarefaController(TarefaService tarefaService)
        {
            this.tarefaService = tarefaService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Tarefa), 200)]
        public IActionResult ObterPorId(int id)
        {
            return Ok(tarefaService.ObterPorId(id));
        }

        [HttpGet("ObterTodos")]
        [ProducesResponseType(typeof(IEnumerable<Tarefa>), 200)]
        public IActionResult ObterTodos()
        {
            return Ok(tarefaService.ObterTodos());
        }

        [HttpGet("ObterPorTitulo")]
        [ProducesResponseType(typeof(Tarefa), 200)]
        public IActionResult ObterPorTitulo(string titulo)
        {
            return Ok(tarefaService.ObterPorTitulo(titulo));
        }

        [HttpGet("ObterPorData")]
        [ProducesResponseType(typeof(IEnumerable<Tarefa>), 200)]
        public IActionResult ObterPorData(DateTime data)
        {
            return Ok(tarefaService.ObterPorData(data));
        }

        [HttpGet("ObterPorStatus")]
        [ProducesResponseType(typeof(IEnumerable<Tarefa>), 200)]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            return Ok(tarefaService.ObterPorStatus(status));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult Criar(Tarefa tarefa)
        {
            tarefaService.Criar(tarefa);
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            tarefaService.Atualizar(id, tarefa);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public IActionResult Deletar(int id)
        {
            tarefaService.Deletar(id);
            return NoContent();
        }
    }
}
