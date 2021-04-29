using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftwareBackup
{
    public partial class AddUser : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-0MRMV3C7\SQLEXPRESS;Initial Catalog=Backup;Integrated Security=True");
        public AddUser()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            if (textBox2.Text != "")
            {
                if (textBox3.Text != "")
                {
                    if (textBox3.Text.All(c => Char.IsNumber(c)))
                    {
                        if (textBox4.Text != "")
                        {
                            
                            //string path = "C:\\SoftwareBackup";
                            //DirectoryInfo dir = new DirectoryInfo(path);
                            DirectoryInfo dir = new DirectoryInfo(@"C:\SoftwareBackup");
                            try
                            {
                                dir.CreateSubdirectory(textBox1.Text);
                            }
                            catch (IOException f)
                            {
                                MessageBox.Show(f.Message);
                            }
                            string direct= @"C:\SoftwareBackup\"+textBox1.Text+"";

                            string ins = "insert into userdetails values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + textBox3.Text + "','1','" + direct + "','" + textBox5.Text + "')";
                            SqlCommand cmd = new SqlCommand(ins, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Success ! Employee Details Stored", "Information");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            Reset();
                        }
                        else
                        {
                            MessageBox.Show("'Email ID' field cannot be left empty !", "Information");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Only Numbers accepted in Contact no. field !", "Information");
                    }
                }
                else
                {
                    MessageBox.Show("'Contact No' field cannot be left empty !", "Information");
                }
            }
            else
            {
                MessageBox.Show("'Employee Name' field cannot be left empty !", "Information");
            }
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            Reset();
        }
        public static string CreateRandomPassword(int PasswordLength)
        {
            string allowedChars = "0123456789abcdefghijkmnopqrstuvwxyz";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = allowedChars[(int)((allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        public void Reset()
        {
            string sel = "select top 1 uid from userdetails order by uid desc";
            SqlCommand cmd = new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                int i = Convert.ToInt32(dr[0].ToString());
                int i1 = i + 1;
                textBox1.Text = i1.ToString();
            }
            else
            {
                textBox1.Text = "101";
            }
            con.Close();
            textBox5.Text = CreateRandomPassword(6);
        }
    }
}
