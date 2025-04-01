using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopularProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
"Values('Coca-Cola Diet','Refrigerante de Cola 350 ml',5.45,'cocacola.jpg',50,GETDATE(),1)");

            mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
            "Values('Lanche de Atum','Lanche de Atum com maionese',8.50,'atum.jpg',10,GETDATE(),3)");

            mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
            "Values('Pudim 100 g','Pudim de leite condensado 100g',6.75,'pudim.jpg',20,GETDATE(),4)");

            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
            "Values ('Coxinha', 'Coxinha de frango com massa crocante', 5.50, 'coxinha.jpg', 50, GETDATE(), 2)");

            mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
            "Values('Coca-Cola Original','Refrigerante de Cola 350 ml',5.45,'cocacola_original.jpg',40,GETDATE(),1)");

            mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
            "Values('Lanche de Frango','Lanche de frango com maionese',9.50,'frango.jpg',15,GETDATE(),3)");

            mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
            "Values('Pudim de Chocolate 100 g','Pudim de chocolate 100g',7.50,'pudim_chocolate.jpg',30,GETDATE(),4)");

            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
            "Values ('Pastel de Carne', 'Pastel de carne com massa crocante', 4.75, 'pastel_carne.jpg', 60, GETDATE(), 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
        }
    }
}
