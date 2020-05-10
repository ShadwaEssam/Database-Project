using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication22
{
    public partial class Form3 : Form
    {
        public Form3(string s1,string s2,string s3,string s4,string s5 ,string s6,string s7)
        {
            InitializeComponent();
            label1.Text = s1;
            label2.Text = s2;
            pictureBox1.Image = new Bitmap(s6);
            string tmp = "";
            for (int i = 0; i <s3.Length; i++)
            {
                if (s3[i] != ' ')
                    tmp += s3[i];
                else
                    break;
                
            }
                label3.Text = tmp;
            label4.Text = s4;
            label5.Text = s5;
            label6.Text = s7;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
