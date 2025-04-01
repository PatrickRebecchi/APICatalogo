using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }
    [Required(ErrorMessage ="Valor invalido")]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "Valor invalido")]
    public string? Descricao { get; set; }
    [Required(ErrorMessage = "Valor invalido")]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Preco { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    [Required(ErrorMessage = "Valor invalido")]
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}
