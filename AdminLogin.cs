using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftwareBackup
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    if (textBox1.Text == "Admin" && textBox2.Text == "Admin")
                    {
                        this.Hide();
                        FrmDashboard h = new FrmDashboard();
                        h.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid login credentials !", "Login Failed");
                    }
                }
                else
                {
                    MessageBox.Show("Password field cannot be left empty !", "Information");
                }
            }
            else
            {
                MessageBox.Show("Admin ID field cannot be left empty !", "Information");
            }
        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {
           
        }
    }
}
