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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sql_lab3
{
    public partial class orders : Form
    {
        DataSet ds = new DataSet();
        public orders()
        {
            InitializeComponent();
            label1.Text = "Orders";
            label2.Text = "Enter customer last name:";
            button1.Text = "Search";
            label3.Text = "Result:";
            button9.Text = "Back to Main menu";
            button7.Text = "Add new order";
            button4.Text = "Renew information";
            button3.Text = "Clear";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            main_menu form = new main_menu();
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ArrayList Empty = new ArrayList();
            dataGridView1.DataSource = Empty;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new_order form = new new_order();
            form.Show();
            //this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new_order form = new new_order();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cus_surname = textBox1.Text;
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query;
                if (string.IsNullOrWhiteSpace(cus_surname))
                {
                    //query = "SELECT car_model as model, cus_surname as [customer surname], date_ord as [order date], price, em_sur as [employee surname], payment_status as [payment status] FROM orders";
                    query = "SELECT  o.car_model AS model, o.cus_surname AS [customer surname], o.date_ord AS [order date], " +
                        "o.price, o.em_sur AS [employee surname], o.payment_status AS [payment status], pm.method FROM orders o " +
                        "JOIN payment_method pm ON o.payment_method_id = pm.id_payment_method";
                }
                else
                {
                    query = "SELECT  o.car_model AS model, o.cus_surname AS [customer surname], o.date_ord AS [order date], " +
                        "o.price, o.em_sur AS [employee surname], o.payment_status AS [payment status], pm.method FROM orders o " +
                        "JOIN payment_method pm ON o.payment_method_id = pm.id_payment_method WHERE o.cus_surname = @cus_surname";
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    if (!string.IsNullOrWhiteSpace(cus_surname))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@cus_surname", cus_surname);
                    }

                    ds.Clear();
                    adapter.Fill(ds, "orders");
                    dataGridView1.DataSource = ds.Tables["orders"];
                }
            }
        }
    }
}
