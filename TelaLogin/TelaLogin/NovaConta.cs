using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace TelaLogin
{
    public partial class NovaConta : Form
    {
        public NovaConta()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text ==""||txtNome.Text == ""||txtSobreNome.Text == ""||txtSenha.Text == "")
            {
                MessageBox.Show("Caixa de texto vazia!", "Formulário Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNome.Focus();
            }
            else
            {
                Conexao con = new Conexao();
                // Salvando as informações do formulário no banco de dados
                try
                {
                    con.Conectar();

                    string sql = "INSERT INTO usuarios(nome, sobreNome, email, senha) VALUES ('" + txtNome.Text + "', '" + txtSobreNome.Text + "', '" + txtEmail.Text + "', '" + txtSenha.Text + "')";
                    SQLiteCommand comando = new SQLiteCommand(sql, con.conn);
                    
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Registro efetuado com sucesso!", "Registro salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtEmail.Clear();
                    txtNome.Clear();
                    txtSobreNome.Clear();
                    txtSenha.Clear();
                    txtNome.Focus();

                    con.Desconectar();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }
    }
}
