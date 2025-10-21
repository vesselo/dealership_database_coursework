using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace sql_lab3
{
    public partial class authorization : Form
    {
        //SqlConnection cnn;

        public authorization()
        {
            InitializeComponent();
            //cnn = new SqlConnection(@"Data Source=DESKTOP-7E43SNE\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            //cnn.Open();
            label1.Text = "Authorization";
            label2.Text = "Password";
            label3.Text = "Email adress";
            button1.Text = "Log-in";
            label4.Text = "This database is for dealership employees only";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string login = textBox1.Text;
                string password = textBox2.Text;

                SqlCommand cmd = new SqlCommand("SELECT position FROM employees WHERE email = @login AND pass_word = @password", connection);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);

                string position = (string)cmd.ExecuteScalar();

                if (position == "admin")
                {
                    main_menu form = new main_menu();
                    form.Show();
                    this.Hide();
                }
                else if (position == "Manager")
                {
                    mm form = new mm();
                    form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect login or password. Try again.");
                }                
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }
    }
}
