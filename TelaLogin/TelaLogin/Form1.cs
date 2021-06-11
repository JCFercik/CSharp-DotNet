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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "" || txtSenha.Text == "")
            {
                MessageBox.Show("Caixa de texto vazia!", "Formulário Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSenha.Clear();
                txtEmail.Focus();
            }
            else
            {
                Conexao con = new Conexao(); // Criando obj de acesso a classe conexao

                try
                {
                    con.Conectar(); // Abrindo conexão com o banco de dados SQLite

                    string sql = "SELECT * FROM usuarios WHERE email = '"+ txtEmail.Text +"' AND senha = '"+ txtSenha.Text +"'"; // Query sql

                    SQLiteDataAdapter dados = new SQLiteDataAdapter(sql, con.conn); // Query de consulta
                    DataTable usuario = new DataTable(); // Criando DataTable para receber dados do banco

                    dados.Fill(usuario); // Passando dados do DataAdapter para o DataTable

                    if (usuario.Rows.Count < 1) // Testando se existe algum registro
                    {
                        MessageBox.Show("Usuário Inválido", "Registro não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtSenha.Clear();
                        txtEmail.Focus();
                    }
                    else
                    {
                        string nome = usuario.Rows[0]["nome"].ToString();
                        string sobreNome = usuario.Rows[0]["sobrenome"].ToString();

                        MessageBox.Show("Bem vindo(a) " + nome + " " + sobreNome, "Login", MessageBoxButtons.OK, MessageBoxIcon.None);

                        txtEmail.Clear();
                        txtSenha.Clear();
                        txtEmail.Focus();
                    }
                    con.Desconectar();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NovaConta nova = new NovaConta();
            nova.ShowDialog();
        }
    }
}
