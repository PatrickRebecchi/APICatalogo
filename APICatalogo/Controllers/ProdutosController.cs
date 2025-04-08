using APICatalogo.Data;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")] // Define a rota base para o controller. Exemplo: /produtos
    [ApiController] // Define que a classe é um controller de API
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context; // Contexto do banco de dados

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint GET para retornar todos os produtos
        [HttpGet]
        public async Task<IEnumerable<Produto>> GetAsync() // Retorna uma lista de produtos
        {
            // Retorna todos os produtos da tabela 'Produtos'
            return await _context.Produtos.ToListAsync();
        }

        [HttpGet("{id}", Name = "ObterProduto")] // Define um parâmetro na rota. Exemplo: /produtos/1
        public async Task<ActionResult<Produto>> GetAsync(int id)
        {
            // Busca um produto pelo ID
            var produto = await _context.Produtos.FindAsync(id);
            // Se não encontrar o produto, retorna 404
            if (produto == null)
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

            if (produtoExistente == null)
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
        }
    }

    

}
