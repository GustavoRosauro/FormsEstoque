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
        private int IdModel { get; set; }
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
            string resultado = "Inserido";
            int linhas = 0;
            if (IdModel == 0)
            {
                linhas = _estoque.InserirProduto(estoque);
            }
            else
            {
                estoque.Id = IdModel;
                linhas = _estoque.EditarRegistro(estoque);
                IdModel = 0;
                resultado = "Editado";
            }
            if (linhas < 1)
            {
                MessageBox.Show("Erro fale com o setor de tecnologia!");
                return;
            }
            else
            {
                MessageBox.Show($"{resultado} com sucesso!");
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

        private void dgvEstoque_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var confirm = MessageBox.Show("Deseja remover esse registro", "Delete",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvEstoque.Rows[e.RowIndex].Cells[0].Value);
                _estoque.DeletarProduto(id);
                LoadGrid();
            }
            else if (MessageBox.Show("Deseja Editar esse registro", "Update",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                IdModel = Convert.ToInt32(dgvEstoque.Rows[e.RowIndex].Cells[0].Value);
                txtNome.Text = dgvEstoque.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtPreco.Text = dgvEstoque.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtQuantidade.Text = dgvEstoque.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
        }
    }
}
