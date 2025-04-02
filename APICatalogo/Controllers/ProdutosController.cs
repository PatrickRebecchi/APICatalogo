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
        // Endpoint GET para retornar todos os produtos
        [HttpGet]
        public async Task<IEnumerable<Produto>> GetAsync(
            [FromServices] AppDbContext context)
        {
            // Retorna todos os produtos da tabela 'Produtos'
            return await context.Produtos.ToListAsync();
        }
    }
}
