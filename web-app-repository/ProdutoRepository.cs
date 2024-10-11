using MySqlConnector;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using wep_app_domain;

namespace web_app_repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly MySqlConnection mySqlConnection;

        public ProdutoRepository()
        {
            string connectionString = "Server=localhost;Database=sys;User=root;Password=123;";
            mySqlConnection = new MySqlConnection(connectionString);
        }

        public async Task<IEnumerable<Produto>> ListarProdutos()
        {
            await mySqlConnection.OpenAsync();
            string query = "select Id, Nome, Preco, Quantidade_Estoque, Data_Criacao from produtos;";

            var produtos = await mySqlConnection.QueryAsync<Produto>(query);
            return produtos;
        }

        public async Task SalvarProduto(Produto produto)
        {
            await mySqlConnection.OpenAsync();
            string sql = @"insert into produtos(Nome, Preco, Quantidade_Estoque, Data_Criacao) 
                           values(@nome, @preco, @quantidade_estoque, @data_criacao);";

            await mySqlConnection.ExecuteAsync(sql, produto);
            await mySqlConnection.CloseAsync();
        }

        public async Task AtualizarProduto(Produto produto)
        {
            await mySqlConnection.OpenAsync();
            string sql = @"update produtos 
                           set Nome = @nome, 
                               Preco = @preco, 
                               Quantidade_Estoque = @quantidade_estoque, 
                               Data_Criacao = @data_criacao
                           where Id = @id;";

            await mySqlConnection.ExecuteAsync(sql, produto);
            await mySqlConnection.CloseAsync();
        }

        public async Task RemoverProduto(int id)
        {
            await mySqlConnection.OpenAsync();
            string sql = @"delete from produtos where Id = @id;";

            await mySqlConnection.ExecuteAsync(sql, new { id });
            await mySqlConnection.CloseAsync();
        }
    }
}
