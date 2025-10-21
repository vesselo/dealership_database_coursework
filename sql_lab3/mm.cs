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
    public partial class mm : Form
    {
        public mm()
        {
            InitializeComponent();
            label1.Text = "Welcome to Main menu!";
            label2.Text = "Choose the information you want to know";
            button1.Text = "Customers and suppliers";
            button2.Text = "Cars and details available";            
            button4.Text = "Orders";            
            button6.Text = "Log-out";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Are you sure? Press the button OK down below to confirm the exit.");
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            orders form = new orders();
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cars_available form = new cars_available();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            customers_suppliers form = new customers_suppliers();
            form.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
