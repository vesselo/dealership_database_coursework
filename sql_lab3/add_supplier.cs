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
    public partial class add_supplier : Form
    {
        public add_supplier()
        {
            InitializeComponent();
            label2.Text = "Add new Supplier";
            label3.Text = "Supplier name:";
            label4.Text = "Phone:";
            label5.Text = "Email address:";
            label1.Text = "Fill the gaps down below to add or renew information";
            button3.Text = "Back to customers and suppliers";
            button1.Text = "Add";
            button2.Text = "Renew";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            customers_suppliers form = new customers_suppliers();
            form.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sup_name = textBox1.Text;
                string phone = textBox2.Text;
                string email = textBox3.Text;
                
                string checkExistenceQuery = "select count(*) from suppliers where phone = @phone";
                using (SqlCommand checkPhone = new SqlCommand(checkExistenceQuery, connection))
                {
                    checkPhone.Parameters.AddWithValue("@phone", phone);
                    int existingPhone = (int)checkPhone.ExecuteScalar();

                    if (existingPhone > 0)
                    {
                        MessageBox.Show("Supplier with this phone number is already exist in this database.");
                        return;
                    }
                }

                string insertQuery = "insert into suppliers (sup_name,phone,email)" + "values (@sup_name, @phone, @email)";
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@sup_name", sup_name);                    
                    insertCommand.Parameters.AddWithValue("@phone", phone);
                    insertCommand.Parameters.AddWithValue("@email", email);
                    
                    insertCommand.ExecuteNonQuery();
                }
                MessageBox.Show("Supplier has been added to the database successfully.");
            }

            customers_suppliers form = new customers_suppliers();
            form.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sup_name = textBox1.Text;
                string phone = textBox2.Text;
                string email = textBox3.Text;

                if (string.IsNullOrWhiteSpace(sup_name) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Please, fill all the gaps.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                string updateQuery = "UPDATE suppliers SET sup_name = @sup_name, phone = @phone, email = @email where sup_name = @sup_name";

                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@sup_name", sup_name);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@email", email);
                                        
                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("The data update succesfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update data. Please make sure the supplier exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while updating data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }               
            }                
        }
    }
}
