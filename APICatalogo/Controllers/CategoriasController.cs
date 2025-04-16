using APICatalogo.Data;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : Controller
{

    private readonly AppDbContext _context;
    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet("todos-produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        return _context.Categorias.Include(p => p.Produtos).ToList(); // Include(p => p.Produtos) faz o join entre as tabelas
    }


    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        try
        {
            //throw new DataMisalignedException();
            return _context.Categorias.AsNoTracking().ToList();
        }
        catch (Exception ex)
        {
            //return BadRequest($"Erro ao obter categorias: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Ocorreu um erro ao tratar a sua solicitação: {ex.Message}");
        }

    }


    [HttpGet("{id:int}", Name = "obterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        try
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Ocorreu um erro ao tratar a sua solicitação: {ex.Message}");
        }

    }


    [HttpPost]
    public ActionResult<Categoria> Post([FromBody] Categoria categoria)
    {
        try
        {
            if (categoria == null)
            {
                return BadRequest();
            }
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return CreatedAtRoute("obterCategoria", new { id = categoria.CategoriaId }, categoria);
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                 $"Ocorreu um erro ao tratar a sua solicitação: {ex.Message}");
        }

    }
    [HttpPut("{id:int}")]
    public ActionResult<Categoria> Put(int id, [FromBody] Categoria categoria)
    {
        try
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }
            if (categoria == null)
            {
                return NotFound();
            }
            _context.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Ocorreu um erro ao tratar a sua solicitação: {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Categoria> Delete(int id)
    {
        try
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada.");
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Ocorreu um erro ao tratar a sua solicitação: {ex.Message}");
        }
    }
}
