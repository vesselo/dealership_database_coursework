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
    public partial class car_maintenance : Form
    {
        DataSet ds = new DataSet();
        public car_maintenance()
        {
            InitializeComponent();
            label1.Text = "Ownership services";
            label2.Text = "Customers last name:";
            label3.Text = "Customers car VIN:";
            label4.Text = "Service type:";
            label5.Text = "Date of service:";
            button1.Text = "Create";
            label6.Text = "Enter car model:";
            label7.Text = "Enter service type:";
            button2.Text = "Search";
            button3.Text = "Back to Main menu";
            checkBox1.Text = "Customers partnership";
            label8.Text = "Choose what you want to do in this window";
            label9.Text = "Cost";
            label10.Text = "Employee surname";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            main_menu form = new main_menu();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string model = textBox2.Text;
            string serv_type = textBox3.Text;
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT c.model AS [car model], sm.serv_date AS [service date]," +
                        " sm.serv_type AS [service type], sm.cost, e.l_name as [employee surname] FROM " +
                        "service_maintenance sm JOIN cars c ON sm.car_id = c.id_car JOIN employees e " +
                        "ON sm.employee_id = e.id_employees"; 
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@model", (object)model ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@serv_type", (object)serv_type ?? DBNull.Value);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            ds.Clear();
                            adapter.Fill(ds, "service_maintenance");
                            dataGridView1.DataSource = ds.Tables["service_maintenance"];
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error^ " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                                
                string l_name = textBox1.Text;
                string vin = comboBox2.Text;
                string service_type = comboBox1.Text;
                DateTime date_serv = dateTimePicker1.Value;
                string cost = textBox4.Text;
                string employee_last_name = textBox5.Text;

                string checkExistenceQuery = "select count(*) from customers where l_name = @l_name";
                using (SqlCommand checkEmail = new SqlCommand(checkExistenceQuery, connection))
                {
                    checkEmail.Parameters.AddWithValue("@l_name", l_name);
                    int existingEmail = (int)checkEmail.ExecuteScalar();

                    if (existingEmail <= 0)
                    {
                        MessageBox.Show("Customer does not exist in this database. Add new customer.");
                        return;
                    }
                }

                string checkExistenceQueryVIN = "select count(*) from cars where VIN = @vin";
                using (SqlCommand checkEmail = new SqlCommand(checkExistenceQueryVIN, connection))
                {
                    checkEmail.Parameters.AddWithValue("@vin", vin);
                    int existingEmail = (int)checkEmail.ExecuteScalar();

                    if (existingEmail <= 0)
                    {
                        MessageBox.Show("VIN does not exist in this database. Add new car.");
                        return;
                    }
                }

                string checkExistenceQueryEmployee = "select count(*) from employees where l_name = @employee_last_name";
                using (SqlCommand checkEmail = new SqlCommand(checkExistenceQueryEmployee, connection))
                {
                    checkEmail.Parameters.AddWithValue("@employee_last_name", employee_last_name);
                    int existingEmail = (int)checkEmail.ExecuteScalar();

                    if (existingEmail <= 0)
                    {
                        MessageBox.Show("Employee does not exist in this database. Add new employee.");
                        return;
                    }
                }

                string insertQuery = "INSERT INTO service_maintenance (car_id, serv_date, serv_type, customer_id, cost, employee_id) SELECT c.id_car, @date_serv, @service_type, cu.id_customer, @cost, e.id_employees FROM cars c JOIN customers cu ON cu.l_name = @l_name JOIN employees e ON e.l_name = @employee_last_name WHERE c.vin = @vin;";
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@vin", vin);
                    insertCommand.Parameters.AddWithValue("@date_serv", date_serv);
                    insertCommand.Parameters.AddWithValue("@service_type", service_type);
                    insertCommand.Parameters.AddWithValue("@l_name", l_name);
                    insertCommand.Parameters.AddWithValue("@cost", cost);
                    insertCommand.Parameters.AddWithValue("@employee_last_name", employee_last_name);


                    insertCommand.ExecuteNonQuery();
                }
                MessageBox.Show("The request has been created succesfully.");
            }

            
        }
    }
}
