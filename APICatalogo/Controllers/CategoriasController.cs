using APICatalogo.Data;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

public class CategoriasController : Controller
{
    private readonly AppDbContext _context;
    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        var categorias = _context.Categorias.ToList();
        return Ok(categorias);
    }


    [HttpGet("{id:int}", Name = "obterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
        if (categoria == null)
        {
            return NotFound();
        }
        return Ok(categoria);
    }


    [HttpPost]
    public ActionResult<Categoria> Post([FromBody] Categoria categoria)
    {
        if (categoria == null)
        {
            return BadRequest();
        }
        _context.Categorias.Add(categoria);
        _context.SaveChanges();
        return CreatedAtRoute("obterCategoria", new { id = categoria.CategoriaId }, categoria);
    }

}

