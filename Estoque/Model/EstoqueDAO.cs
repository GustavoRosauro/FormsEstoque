using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque.Model
{
    public class EstoqueDAO
    {
        const string ConnectionString = @"Data Source=localhost;Initial Catalog=APEXENSINO;Integrated Security=True";
        private SqlConnection ReturnOpenConnection()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
        public int InserirProduto(EstoqueModel estoque)
        {
            var props = estoque.GetType().GetProperties();
            string campos = string.Join(",", props.Where(x => x.Name.ToLower() != "id").Select(x => x.Name));
            string valores = string.Join(",", props.Where(x => x.Name.ToLower() != "id").Select(x => $"@{x.Name}"));
            string sqlIsnert = $"INSERT INTO ESTOQUE ({campos}) values ({valores})";
            using (var conn = ReturnOpenConnection())
            {
                using (var cmd = new SqlCommand(sqlIsnert, conn))
                {
                    cmd.Parameters.AddWithValue($"@{nameof(EstoqueModel.Preco)}", estoque.Preco);
                    cmd.Parameters.AddWithValue($"@{nameof(EstoqueModel.Produto)}", estoque.Produto);
                    cmd.Parameters.AddWithValue($"@{nameof(EstoqueModel.Quantidade)}", estoque.Quantidade);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public List<EstoqueModel> RetornaListaProdutos()
        {
            var props = new EstoqueModel("dfasdfasdf").GetType().GetProperties();
            string campos = string.Join(",", props.Select(x => x.Name));
            string sqlSelect = $"SELECT {campos} from estoque";
            var lista = new List<EstoqueModel>();
            using (var conn = ReturnOpenConnection())
            {
                using (var cmd = new SqlCommand(sqlSelect, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var estoque = new EstoqueModel(reader[nameof(EstoqueModel.Produto)].ToString());
                            estoque.Preco = Convert.ToDouble(reader[nameof(EstoqueModel.Preco)]);
                            estoque.Quantidade = Convert.ToInt32(reader[nameof(EstoqueModel.Quantidade)]);
                            estoque.Id = Convert.ToInt32(reader[nameof(EstoqueModel.Id)]);
                            lista.Add(estoque);
                        }
                    }
                }
                return lista;
            }
        }
        public void DeletarProduto(int id)
        {
            string sqlDelete = "DELETE FROM ESTOQUE WHERE ID = "+id;
            using (var conn = ReturnOpenConnection())
            {
                using (var command = new SqlCommand(sqlDelete, conn))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        public int EditarRegistro(EstoqueModel estoque)
        {
            string sqlUpdate = $@"UPDATE ESTOQUE  
                                    SET {nameof(EstoqueModel.Produto)} = @{nameof(EstoqueModel.Produto)}
                                    ,{nameof(EstoqueModel.Preco)} = @{nameof(EstoqueModel.Preco)}
                                    ,{nameof(EstoqueModel.Quantidade)} = @{nameof(EstoqueModel.Quantidade)}
                                     WHERE ID = "+estoque.Id;
            using (var conn = ReturnOpenConnection())
            {
                using (var cmd = new SqlCommand(sqlUpdate, conn))
                {
                    cmd.Parameters.AddWithValue($"@{nameof(EstoqueModel.Preco)}", estoque.Preco);
                    cmd.Parameters.AddWithValue($"@{nameof(EstoqueModel.Produto)}", estoque.Produto);
                    cmd.Parameters.AddWithValue($"@{nameof(EstoqueModel.Quantidade)}", estoque.Quantidade);
                    return cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
