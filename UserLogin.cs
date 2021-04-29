
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

namespace SoftwareBackup
{
    public partial class UserLogin : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-0MRMV3C7\SQLEXPRESS;Initial Catalog=Backup;Integrated Security=True");
        public UserLogin()
        {
            InitializeComponent();
        }
        public static string SetValueForText1 = "";
        public static string SetValueForText2 = "";
        public static string CreateRandomPassword(int PasswordLength)
        {
            string allowedChars = "0123456789";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = allowedChars[(int)((allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    string str = "select uid,password,name,email,phone,status from userdetails where uid='" + textBox1.Text + "'";
                    SqlDataAdapter da = new SqlDataAdapter(str, con);
                    DataTable ds = new DataTable();
                    da.Fill(ds);
                    string id = ds.Rows[0][0].ToString();
                    string pass = ds.Rows[0][1].ToString();
                    string phn = ds.Rows[0][4].ToString();
                    string sts = ds.Rows[0][5].ToString();
                    if (textBox1.Text == id && textBox2.Text == pass)
                    {
                        if (sts != "0")
                        {
                            button1.Enabled = false;
                            string del = "delete from otp where uid='" + textBox1.Text + "'";
                            SqlCommand cmd1 = new SqlCommand(del, con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            string otp = CreateRandomPassword(4);
                            string ins = "insert into otp values('" + textBox1.Text + "','" + otp + "')";
                            SqlCommand cmd = new SqlCommand(ins, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            string url = @"https://api-mapper.clicksend.com/http/v2/send.php?method=http&username=kumar&key=62C82BE6-5D93-EED2-9700-17FD232A66E3&to=+91" + phn + "&message=Your generated OTP is " + otp + "";
                            webBrowser1.Navigate(url);
                            panel1.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Your Account has been blocked !", "Information");
                        }
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
                MessageBox.Show("Employee ID field cannot be left empty !", "Information");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sel = "select otp from otp where uid='" + textBox1.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(sel,con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if(ds.Tables[0].Rows.Count>0)
            {
                string otp = ds.Tables[0].Rows[0][0].ToString();
                if(textBox3.Text==otp)
                {
                    
                    SetValueForText1 = textBox1.Text;
                    this.Hide();
                    Explorer h = new Explorer();
                    h.Show();
                }
                else
                {
                    MessageBox.Show("OTP doesn't not match","Information");
                }
            }
        }

        private void UserLogin_Load(object sender, EventArgs e)
        {
            
            panel1.Visible = false;
        }
    }
}
