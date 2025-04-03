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

        [HttpGet("{id}")] // Define um parâmetro na rota. Exemplo: /produtos/1
        public async Task<ActionResult<Produto>> GetAsync(int id)
        {
            // Busca um produto pelo ID
            var produto = await _context.Produtos.FindAsync(id);
            // Se não encontrar o produto, retorna 404
            if (produto == null)
            {
                return NotFound();
            }
            // Retorna o produto encontrado
            return produto;
        } 
    }
}
