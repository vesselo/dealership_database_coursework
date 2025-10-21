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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sql_lab3
{
    public partial class cars_available : Form
    {
        DataSet ds = new DataSet();
        SqlConnection cnn;
        public cars_available()
        {
            InitializeComponent();
            label1.Text = "Cars and details in stock";
            label2.Text = "Choose:";
            label3.Text = "Detail name:";
            button1.Text = "Search";
            button2.Text = "Search";
            label4.Text = "Result of search:";
            label5.Text = "Result of search:";
            button3.Text = "Back to Main menu";
            label6.Text = "If there is no detail or car in stock, fill the form down below:";
            label7.Text = "Detail name:";
            label8.Text = "Supplier:";
            label9.Text = "Quantity:";
            button4.Text = "Send request";
            comboBox1.Items.Add("available");
            comboBox1.Items.Add("sold");
            comboBox1.Items.Add("in repair");
            label10.Text = "If you can't find anything, then you need to fill out a replenishment request down below";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string detail_name = textBox3.Text;
                string sup_name = textBox4.Text;
                string quantity = textBox5.Text;
                                
                string insertQuery = "insert into replenishment (detail_name,sup_name,quantity)" + "values (@detail_name, @sup_name, @quantity)";
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@detail_name", detail_name);
                    insertCommand.Parameters.AddWithValue("@sup_name", sup_name);
                    insertCommand.Parameters.AddWithValue("@quantity", quantity);

                    insertCommand.ExecuteNonQuery();
                }
                MessageBox.Show("Replenishment request was created successfully.");
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }                        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            main_menu form = new main_menu();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string car_status = comboBox1.Text;
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query;
                if (string.IsNullOrWhiteSpace(car_status))
                {
                    query = "SELECT model, VIN, price, mileage FROM cars";
                }
                else
                {
                    query = "SELECT model, VIN, price, mileage FROM cars WHERE car_status = @car_status";
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    if (!string.IsNullOrWhiteSpace(car_status))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@car_status", car_status);
                    }

                    ds.Clear();
                    adapter.Fill(ds, "cars");
                    dataGridView1.DataSource = ds.Tables["cars"];
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string detail_name = textBox2.Text;
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query;
                if (string.IsNullOrWhiteSpace(detail_name))
                {
                    query = "SELECT detail_name, price FROM car_details";
                }
                else
                {
                    query = "SELECT detail_name, price FROM car_details WHERE detail_name = @detail_name";
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    if (!string.IsNullOrWhiteSpace(detail_name))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@detail_name", detail_name);
                    }

                    ds.Clear();
                    adapter.Fill(ds, "car_details");
                    dataGridView2.DataSource = ds.Tables["car_details"];
                }
            }
        }
    }
}
