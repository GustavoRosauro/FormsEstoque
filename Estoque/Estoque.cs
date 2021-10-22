using Estoque.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estoque
{
    public partial class Estoque : Form
    {
        private readonly EstoqueDAO _estoque;
        public Estoque()
        {
            InitializeComponent();
            _estoque = new EstoqueDAO();
        }
        

        private void btnClick_Click(object sender, EventArgs e)
        {
            var estoque = new EstoqueModel(txtNome.Text);
            estoque.Preco = Convert.ToDouble(txtPreco.Text);
            estoque.Quantidade = Convert.ToInt32(txtQuantidade.Text);
            var linhas = _estoque.InserirProduto(estoque);
            if (linhas < 1)
            {
                MessageBox.Show("Erro ao inserir fale com o setor de tecnologia!");
                return;
            }
            else
            {
                MessageBox.Show("Inserido com sucesso!");
                LimparCampos();
                LoadGrid();
            }
        }
        private void LimparCampos()
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    control.Text = "";
                }
            }
        }

        private void Estoque_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        { 
            dgvEstoque.DataSource = _estoque.RetornaListaProdutos();
        }
    }
}
