using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque.Model
{
    public class EstoqueModel
    {
        public int Id { get; set; }
        public string Produto { get; private set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }
        public EstoqueModel(string produto)
        {
            if (string.IsNullOrEmpty(produto))
                throw new Exception("O produto está vazio");
            Produto = produto;
        }
    }
}
