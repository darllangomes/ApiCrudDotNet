using Microsoft.AspNetCore.Mvc;
using ApiCrud.Models;
using ApiCrud.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var produtos = await _produtoRepository.GetAll(pageNumber, pageSize);
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetById(int id)
        {
            var produto = await _produtoRepository.GetById(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Produto produto)
        {
            await _produtoRepository.Add(produto);
            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest("O ID da rota não corresponde ao ID do produto.");
            }
            await _produtoRepository.Update(produto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }
            await _produtoRepository.Delete(id);
            return NoContent();
        }
    }
}
