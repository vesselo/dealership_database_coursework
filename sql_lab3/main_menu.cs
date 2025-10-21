using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sql_lab3
{
    public partial class main_menu : Form
    {
        public main_menu()
        {
            InitializeComponent();
            label1.Text = "Welcome to Main menu!";
            label2.Text = "Choose the information you want to know";
            button1.Text = "Customers and suppliers";
            button2.Text = "Cars and details available";
            button3.Text = "Car maintenance";
            button4.Text = "Orders";
            button5.Text = "Employees";
            button6.Text = "Log-out";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Are you sure? Press the button OK down below to confirm the exit.");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            customers_suppliers form = new customers_suppliers();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cars_available form = new cars_available();
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            car_maintenance form = new car_maintenance();
            form.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            orders form = new orders();
            form.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            employees form = new employees();
            form.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
