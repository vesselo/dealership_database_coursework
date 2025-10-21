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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace sql_lab3
{
    public partial class new_order : Form
    {
        public new_order()
        {
            InitializeComponent();
            label2.Text = "Create new order";
            label8.Text = "Select car to create a new order";
            button1.Text = "Create an order";
            button3.Text = "Update info";
            label3.Text = "Car model";
            label4.Text = "Customer surname";
            label5.Text = "Date";
            label6.Text = "Price";
            label7.Text = "Employee surname";
            label1.Text = "Payment status";
            label9.Text = "Payment method";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string car_model = comboBox1.Text;
                string cus_surname = textBox2.Text;
                string price = textBox4.Text;
                DateTime date_ord = dateTimePicker1.Value;
                string em_sur = textBox5.Text;
                string payment_status = comboBox2.Text;

                if (string.IsNullOrWhiteSpace(car_model) || string.IsNullOrWhiteSpace(cus_surname) || string.IsNullOrWhiteSpace(price) || string.IsNullOrWhiteSpace(em_sur))
                {
                    MessageBox.Show("Please, fill all the gaps.");
                    return;
                }

                // Проверка наличия фамилии клиента
                string checkCustomerQuery = "SELECT COUNT(*) FROM customers WHERE l_name = @cus_surname";
                using (SqlCommand checkCustomerCommand = new SqlCommand(checkCustomerQuery, connection))
                {
                    checkCustomerCommand.Parameters.AddWithValue("@cus_surname", cus_surname);
                    int customerExists = (int)checkCustomerCommand.ExecuteScalar();

                    if (customerExists == 0)
                    {
                        MessageBox.Show("Customer surname not found in the database.");
                        return;
                    }
                }

                // Проверка наличия фамилии сотрудника
                string checkEmployeeQuery = "SELECT COUNT(*) FROM employees WHERE l_name = @em_sur";
                using (SqlCommand checkEmployeeCommand = new SqlCommand(checkEmployeeQuery, connection))
                {
                    checkEmployeeCommand.Parameters.AddWithValue("@em_sur", em_sur);
                    int employeeExists = (int)checkEmployeeCommand.ExecuteScalar();

                    if (employeeExists == 0)
                    {
                        MessageBox.Show("Employee surname not found in the database.");
                        return;
                    }
                }


                string insertQuery = "insert into orders (car_model,cus_surname,date_ord,price,em_sur,payment_status)" + 
                    "values (@car_model, @cus_surname, @date_ord, @price, @em_sur, @payment_status)";
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@car_model", car_model);
                    insertCommand.Parameters.AddWithValue("@cus_surname", cus_surname);
                    insertCommand.Parameters.AddWithValue("@date_ord", date_ord);
                    insertCommand.Parameters.AddWithValue("@price", price);
                    insertCommand.Parameters.AddWithValue("@em_sur", em_sur);
                    insertCommand.Parameters.AddWithValue("@payment_status", payment_status);

                    insertCommand.ExecuteNonQuery();
                }
                MessageBox.Show("Order has been created successfully.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-7E43SNE\\SQLEXPRESS;Initial Catalog=cardealer;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string car_model = comboBox1.Text;
                string cus_surname = textBox2.Text;
                string price = textBox4.Text;
                DateTime date_ord = dateTimePicker1.Value;
                string em_sur = textBox5.Text;
                string payment_status = comboBox2.Text;

                if (string.IsNullOrWhiteSpace(car_model) || string.IsNullOrWhiteSpace(cus_surname) || string.IsNullOrWhiteSpace(price) || string.IsNullOrWhiteSpace(em_sur))
                {
                    MessageBox.Show("Please, fill all the gaps.");
                    return;
                }

                // Проверка наличия фамилии клиента
                string checkCustomerQuery = "SELECT COUNT(*) FROM customers WHERE cus_surname = @cus_surname";
                using (SqlCommand checkCustomerCommand = new SqlCommand(checkCustomerQuery, connection))
                {
                    checkCustomerCommand.Parameters.AddWithValue("@cus_surname", cus_surname);
                    int customerExists = (int)checkCustomerCommand.ExecuteScalar();

                    if (customerExists == 0)
                    {
                        MessageBox.Show("Customer surname not found in the database.");
                        return;
                    }
                }

                // Проверка наличия фамилии сотрудника
                string checkEmployeeQuery = "SELECT COUNT(*) FROM employees WHERE em_sur = @em_sur";
                using (SqlCommand checkEmployeeCommand = new SqlCommand(checkEmployeeQuery, connection))
                {
                    checkEmployeeCommand.Parameters.AddWithValue("@em_sur", em_sur);
                    int employeeExists = (int)checkEmployeeCommand.ExecuteScalar();

                    if (employeeExists == 0)
                    {
                        MessageBox.Show("Employee surname not found in the database.");
                        return;
                    }
                }

                string updateQuery = "UPDATE orders SET car_model = @car_model, cus_surname = @cus_surname, price = @price, date_ord = @date_ord, em_sur = @em_sur payment_status = @payment_status where cus_surname = @cus_surname";

                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {                    
                    cmd.Parameters.AddWithValue("@car_model", car_model);
                    cmd.Parameters.AddWithValue("@cus_surname", cus_surname);
                    cmd.Parameters.AddWithValue("@date_ord", date_ord);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@em_sur", em_sur);
                    cmd.Parameters.AddWithValue("@payment_status", payment_status);
                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("The data update succesfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update data. Please make sure the supplier exists.");
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

private string lastSelectedValue = "";
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получаем текущее выбранное значение
            string selectedValue = comboBox2.SelectedItem.ToString();

            // Проверяем, является ли выбранное значение допустимым
            if (selectedValue != "completed" && selectedValue != "in progress" && selectedValue != "cancelled")
            {
                // Если нет, возвращаем к предыдущему значению
                comboBox2.SelectedItem = lastSelectedValue;
            }
            else
            {
                // Если да, сохраняем текущее значение
                lastSelectedValue = selectedValue;
            }
        }

        private void new_order_Load(object sender, EventArgs e)
        {
            // Добавляем элементы в ComboBox
            comboBox2.Items.Add("completed");
            comboBox2.Items.Add("in progress");
            comboBox2.Items.Add("cancelled");
            
            comboBox3.Items.Add("cash");
            comboBox3.Items.Add("card");
            comboBox3.Items.Add("car loan");
            comboBox3.Items.Add("leasing");
            // Устанавливаем значение по умолчанию
            comboBox2.SelectedIndex = 0;
            lastSelectedValue = comboBox2.SelectedItem.ToString();
            comboBox3.SelectedIndex = 0;
            lastSelectedValue = comboBox3.SelectedItem.ToString();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получаем текущее выбранное значение
            string selectedValue = comboBox3.SelectedItem.ToString();

            // Проверяем, является ли выбранное значение допустимым
            if (selectedValue != "cash" && selectedValue != "card" && selectedValue != "car loan" && selectedValue != "leasing")
            {
                // Если нет, возвращаем к предыдущему значению
                comboBox3.SelectedItem = lastSelectedValue;
            }
            else
            {
                // Если да, сохраняем текущее значение
                lastSelectedValue = selectedValue;
            }
        }
    }    
}
