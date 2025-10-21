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
    public partial class customers_suppliers : Form
    {
        DataSet ds = new DataSet();
        SqlConnection cnn;
        public customers_suppliers()
        {
            InitializeComponent();
            label1.Text = "Customers";
            label2.Text = "Suppliers";
            label3.Text = "Enter customer last name:";
            label4.Text = "Customer information:";
            button1.Text = "Search";
            label6.Text = "Enter supplier name:";
            label5.Text = "Customer information:";
            button2.Text = "Search";
            button3.Text = "Clear";
            button4.Text = "Renew infomation";
            button6.Text = "Clear";
            button5.Text = "Renew information";
            button7.Text = "Add new customer";
            button8.Text = "Add new supplier";
            button9.Text = "Back to main menu";
            button10.Text = "Delete";
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Проверяем, выбран ли элемент в dataGridView1 или dataGridView2
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Удаляем клиента
                int IdCustomerToDelete = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["first name"].Value);
                string deleteQuery = "DELETE FROM customers WHERE f_name = @IdCustomerToDelete";

                string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    cnn.Open(); // Открываем соединение

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, cnn))
                    {
                        // Добавляем параметры
                        cmd.Parameters.AddWithValue("@IdCustomerToDelete", IdCustomerToDelete);
                        cmd.ExecuteNonQuery(); // Выполняем команду
                    }

                    // Обновляем таблицу DataGridView1
                    ds.Clear();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM customers", cnn);
                    da.Fill(ds, "customers");
                    dataGridView1.DataSource = ds.Tables["customers"];
                }
            }
            else if (dataGridView2.SelectedRows.Count > 0)
            {
                // Удаляем поставщика
                int IdSupplierToDelete = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["sup_name"].Value);
                string deleteQuery = "DELETE FROM suppliers WHERE sup_name = @IdSupplierToDelete";

                string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    cnn.Open(); // Открываем соединение

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, cnn))
                    {
                        // Добавляем параметры
                        cmd.Parameters.AddWithValue("@IdSupplierToDelete", IdSupplierToDelete);
                        cmd.ExecuteNonQuery(); // Выполняем команду
                    }

                    // Обновляем таблицу DataGridView2
                    ds.Clear();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM suppliers", cnn);
                    da.Fill(ds, "suppliers");
                    dataGridView2.DataSource = ds.Tables["suppliers"];
                }
            }
            else
            {
                MessageBox.Show("Choose the row for deleting.");
            }            
        }

        private void button9_Click(object sender, EventArgs e)
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
                    query = "SELECT f_name as [first name], l_name as [last name], phone, email, ph_address as [physical address] FROM customers";
                }
                else
                {                    
                    query = "SELECT f_name as [first name], l_name as [last name], phone, email, ph_address as [physical address] FROM customers WHERE l_name = @l_name";
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {                    
                    if (!string.IsNullOrWhiteSpace(l_name))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@l_name", l_name);
                    }

                    ds.Clear();
                    adapter.Fill(ds, "customers");
                    dataGridView1.DataSource = ds.Tables["customers"];
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sup_name = textBox2.Text;
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query;
                if (string.IsNullOrWhiteSpace(sup_name))
                {
                    query = "SELECT sup_name as [supplier name], phone, email FROM suppliers";
                }
                else
                {
                    query = "SELECT sup_name as [supplier name], phone, email FROM suppliers WHERE sup_name = @sup_name";
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {                    
                    if (!string.IsNullOrWhiteSpace(sup_name))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@sup_name", sup_name);
                    }

                    ds.Clear();
                    adapter.Fill(ds, "suppliers");
                    dataGridView2.DataSource = ds.Tables["suppliers"];
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ArrayList Empty = new ArrayList();

            dataGridView1.DataSource = Empty;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ArrayList Empty = new ArrayList();

            dataGridView2.DataSource = Empty;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            registration form = new registration();
            form.Show();
            //this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            add_supplier form = new add_supplier();
            form.Show();
            //this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            registration form = new registration();            
            form.Show();
            //this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            add_supplier form = new add_supplier();
            form.Show();
            //this.Hide();
        }
    }
}
