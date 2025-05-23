﻿using APICatalogo.Data;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")] // Define a rota /produtos
    [ApiController] // Define que a classe é um controller de API
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context; // Contexto do banco de dados

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }
        // Endpoint GET para retornar o primeiro produto

        [HttpGet("primeiro")] //rota    /produtos/primeiro
        public ActionResult<Produto> GetPrimeiro()
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(); // Busca o primeiro produto
                if (produto is null)
                {
                    return NotFound("Produto não encontrado.");
                }
                return Ok(produto); //produto encontrado
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao obter produto: {ex.Message}");
            }
        }

        // Endpoint GET para retornar todos os produtos
        [HttpGet]
        public async Task<IEnumerable<Produto>> GetAsync() // Retorna uma lista de produtos
        {
            // Retorna todos os produtos da tabela 'Produtos'
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }



        [HttpGet("{id}", Name = "ObterProduto")] // Define um parâmetro na rota. Exemplo: /produtos/1
        public async Task<ActionResult<Produto>> GetAsync(int id)
        {
            // Busca um produto pelo ID
            var produto = await _context.Produtos.FindAsync(id);
            // Se não encontrar o produto, retorna 404
            if (produto is null)
            {
                return NotFound("Produto não localizado!");
            }
            // Retorna o produto encontrado
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest("Produto não pode ser nulo.");
            }

            var produtoExistente = _context.Produtos
                .FirstOrDefault(p => p.Nome == produto.Nome);

            if (produtoExistente != null)
            {
                // Retorna um erro 409 (Conflito) caso o produto já exista
                return Conflict("Produto já cadastrado.");
            }

            // Adiciona o produto ao banco
            _context.Produtos.Add(produto);
            _context.SaveChanges();


            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id}")]
        public ActionResult AtualizarProduto(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest("O ID do produto não corresponde ao produto fornecido.");
            }

            var produtoExistente = _context.Produtos
                .AsNoTracking()
                .FirstOrDefault(p => p.ProdutoId == id);

            if (produtoExistente is null)
            {
                return NotFound("Produto não encontrado.");
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletarProduto(int id)
        {
            var produto = _context.Produtos.Find(id); // Busca o produto pelo ID
            if (id == 0)
            {
                return NotFound("Produto não encontrado.");
            }
            if (produto is null)
            {
                return NotFound("Produto não encontrado.");
            }
            _context.Produtos.Remove(produto); // Remove o produto do contexto
            _context.SaveChanges();
            return Ok("Produto deletado com sucesso.");
        }
    }
}
