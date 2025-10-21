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

namespace sql_lab3
{
    public partial class employees : Form
    {
        DataSet ds = new DataSet();
        SqlConnection cnn;
        
        public employees()
        {
            InitializeComponent();
            label1.Text = "The list of employees";
            label2.Text = "Enter last name of employee:";
            button1.Text = "Search";
            button2.Text = "Add new employee";
            button3.Text = "Delete an employee";
            button4.Text = "Back to Main menu";
            button5.Text = "Clear";
            cnn = new SqlConnection(@"Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            main_menu form = new main_menu();
            form.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string l_name = textBox1.Text;
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                string query;
                if (string.IsNullOrWhiteSpace(l_name))
                {                    
                    query = "SELECT f_name as [first name], l_name as [last name], position, phone, email FROM employees";
                }
                else
                {                    
                    query = "SELECT f_name as [first name], l_name as [last name], position, phone, email FROM employees WHERE l_name = @l_name";
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {                    
                    if (!string.IsNullOrWhiteSpace(l_name))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@l_name", l_name);
                    }

                    ds.Clear();
                    adapter.Fill(ds, "employees");
                    dataGridView1.DataSource = ds.Tables["employees"];
                }                                                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            add_employee form = new add_employee();
            form.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string firstNameToDelete = dataGridView1.SelectedRows[0].Cells["first name"].Value.ToString();
                //int IdEmployeetToDelete = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["first name"].Value);
                string deleteQuery = "DELETE FROM employees WHERE f_name = @firstNameToDelete";

                string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    cnn.Open(); // Открываем соединение

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, cnn))
                    {
                        // Добавляем параметры
                        cmd.Parameters.AddWithValue("@firstNameToDelete", firstNameToDelete);
                        cmd.ExecuteNonQuery(); // Выполняем команду
                    }

                    // Обновляем таблицу DataGridView
                    ds.Clear();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM employees", cnn);
                    da.Fill(ds, "employees");
                    dataGridView1.DataSource = ds.Tables["employees"];
                }
            }
            else
            {
                MessageBox.Show("Choose the row for deleting.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ArrayList Empty = new ArrayList();
            dataGridView1.DataSource = Empty;
        }
    }
}
