using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sql_lab3
{
    public partial class add_employee : Form
    {
        public add_employee()
        {
            InitializeComponent();
            label2.Text = "Add new Employee";
            label3.Text = "First name:";
            label4.Text = "Last name:";
            label5.Text = "Position:";
            label6.Text = "Phone:";
            label7.Text = "Email address:";
            label1.Text = "Password";
            button3.Text = "Back to Main Menu";
            button1.Text = "Add";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string f_name = textBox1.Text;
                string l_name = textBox2.Text;
                string position = textBox3.Text;
                string phone = textBox4.Text;
                string email = textBox5.Text;
                string password = textBox6.Text;

                string checkExistenceQuery = "select count(*) from employees where email = @email";
                using (SqlCommand checkEmail = new SqlCommand(checkExistenceQuery, connection))
                {
                    checkEmail.Parameters.AddWithValue("@email", email);
                    int existingEmail = (int)checkEmail.ExecuteScalar();

                    if (existingEmail > 0)
                    {
                        MessageBox.Show("Employee with this email address is already exist in this database.");
                        return;
                    }
                }

                string insertQuery = "insert into employees (f_name,l_name,position,phone,email,pass_word)" + "values (@f_name, @l_name, @position, @phone, @email, @password)";
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@f_name", f_name);
                    insertCommand.Parameters.AddWithValue("@l_name", l_name);
                    insertCommand.Parameters.AddWithValue("@position", position);
                    insertCommand.Parameters.AddWithValue("@phone", phone);
                    insertCommand.Parameters.AddWithValue("@email", email);
                    insertCommand.Parameters.AddWithValue("@password", password);

                    insertCommand.ExecuteNonQuery();
                }
                MessageBox.Show("Employee has been added to the database successfully.");
            }

            employees form = new employees();
            form.Show();
            this.Hide();
            }

        private void button3_Click(object sender, EventArgs e)
        {
            main_menu form = new main_menu();
            form.Show();
            this.Hide();
        }
    }
}

