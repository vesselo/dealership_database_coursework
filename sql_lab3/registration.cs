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
    public partial class registration : Form
    {
        public registration()
        {
            InitializeComponent();
            label2.Text = "Adding new customer";
            button1.Text = "Submit";
            label3.Text = "First name";
            label4.Text = "Last name";
            label5.Text = "Phone";
            label6.Text = "Email address";
            label7.Text = "Residential address";
            button2.Text = "Back to customers and suppliers";
            checkBox1.Text = "Partnership";
            label8.Text = "Fill the information down below";
            button3.Text = "Renew";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void registration_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string f_name = textBox1.Text;
                string l_name = textBox2.Text;
                string phone = textBox3.Text;
                string email = textBox4.Text;
                string ph_address = textBox5.Text;

                string checkExistenceQuery = "select count(*) from customers where phone = @phone";
                using (SqlCommand checkPhone = new SqlCommand(checkExistenceQuery,connection))
                {
                    checkPhone.Parameters.AddWithValue("@phone", phone);
                    int existingPhone = (int)checkPhone.ExecuteScalar();

                    if (existingPhone > 0)
                    {
                        MessageBox.Show("Client with this phone number is already exist in this database.");
                        return;
                    }
                }

                string insertQuery = "insert into customers (f_name,l_name,phone,email,ph_address)" + "values (@f_name, @l_name, @phone, @email, @ph_address)";
                using (SqlCommand insertCommand = new SqlCommand(insertQuery,connection))
                {
                    insertCommand.Parameters.AddWithValue("@f_name",f_name);
                    insertCommand.Parameters.AddWithValue("@l_name", l_name);
                    insertCommand.Parameters.AddWithValue("@phone", phone);
                    insertCommand.Parameters.AddWithValue("@email", email);
                    insertCommand.Parameters.AddWithValue("@ph_address", ph_address);

                    insertCommand.ExecuteNonQuery();
                }
                MessageBox.Show("Client has been added to the database successfully.");
            }

            customers_suppliers form = new customers_suppliers();
            form.Show();
            this.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            customers_suppliers form = new customers_suppliers();
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string f_name = textBox1.Text;
                string l_name = textBox2.Text;
                string phone = textBox3.Text;
                string email = textBox4.Text;
                string ph_address = textBox5.Text;

                if (string.IsNullOrWhiteSpace(f_name) || string.IsNullOrWhiteSpace(l_name) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(ph_address))
                {
                    MessageBox.Show("Please, fill all the gaps.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string updateQuery = "UPDATE customers SET f_name = @f_name, l_name = @l_name, phone = @phone, email = @email, ph_address = @ph_address where l_name = @l_name";

                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@f_name", f_name);
                    cmd.Parameters.AddWithValue("@l_name", l_name);                    
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@ph_address", ph_address);

                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("The data update succesfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update data. Please make sure the customer exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
