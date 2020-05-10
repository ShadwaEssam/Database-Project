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
    public partial class Form6 : Form
    {
        bool text1 = false;
        bool text2 = false;
        public static Form2 form_exit;
        public Form6()
        {
            InitializeComponent();
           
            KeyDown += new KeyEventHandler(Form6_KeyDown);
            
           
        }

        private void Form6_Load(object sender, EventArgs e)
        {

            linkLabel1.Hide();
            panel1.BringToFront();
            
            panel1.BackColor = Color.FromArgb(45,0,0,0);
           // panel2.BackColor = Color.FromArgb(0, 0, 0, 0);
            label1.BackColor = System.Drawing.Color.Transparent;          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(text1==false){
                textBox1.Text = "";
                text1 = true;
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (text2 == false)
            {
                textBox2.Text = "";
                text2 = true;
                textBox2.ForeColor = Color.Black;
                textBox2.UseSystemPasswordChar = true;
                textBox2.PasswordChar = '\u25CF';
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                Form5 f5 = new Form5();
                f5.ShowDialog();
            }
            else
            {
                MessageBox.Show("invalid Username/Password");
            }
        }

        private void Form6_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.A)
            {
                MessageBox.Show("eeeee");
            }
            
        }

        private void Form6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar>=48&&e.KeyChar<=57)
            {
                MessageBox.Show("A key pressed");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("eeeee");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
    
            Image img = new Bitmap(@"b.jpg");
            this.BackgroundImage = img;
     
            pictureBox2.Image = new Bitmap(@"d.png");
           
            pictureBox1.Image = new Bitmap(@"c.png");
            panel1.Hide();
            linkLabel1.Show();
           
            
        }

      

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            linkLabel1.Hide();
            Image img = new Bitmap(@"a.jpg");
            this.BackgroundImage = img;
            pictureBox2.Image = new Bitmap(@"c.png");

            pictureBox1.Image = new Bitmap(@"d.png");
            panel1.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 f2 = new Form2();
            form_exit = f2;
            f2.ShowDialog();
        }
    }
}
