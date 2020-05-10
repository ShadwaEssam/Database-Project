using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;


namespace WindowsFormsApplication22
{
    public partial class Form2 : Form
    {
        public static string txt_name;
        public static string txt_pass;

       

        bool  entered_flow2=false;
        bool text_pressed = false;
        bool U1=false;
        bool U2=false;
        bool p1 = false;
        bool p2 = false;
        bool rp = false;
        int[] sid;
        string[] s1;
        string[] s2;
        string[] s3;
        string[] s4;
        string[] s5;
        string[] s6;
        string[] UN_uord;
        string s = "Data Source=orcl;User Id=scott;Password=tiger;";
        OracleDataReader rdr;
        OracleConnection conn;
        
        public Form2()
        {
            InitializeComponent();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex=0;
            pictureBox2.BringToFront();
            panel2.Visible = false;
            panel3.Visible = false;
            conn = new OracleConnection(s);
            conn.Open();
            int i = 0;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            //cmd.CommandText = "select * from PRODUCT";
            cmd.CommandText = "ALLPRODUCTS";
      
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("img", OracleDbType.RefCursor, ParameterDirection.Output); 
           
             rdr = cmd.ExecuteReader();
             s1 = new string[100];
             s2 = new string[100];

             s3 = new string[100];
             s4 = new string[100];
             s5 = new string[100];
             s6 = new string[100];
            if(rdr.HasRows){
                while (rdr.Read())
                {
                    PictureBox pic = retpic(i,rdr[7].ToString());
                    flowLayoutPanel1.Controls.Add(pic);
                    s1[i] = Convert.ToString(rdr[1]);
                    s2[i] = Convert.ToString(rdr[2]);
                    s3[i] = Convert.ToString(rdr[3]);
                    s4[i] = Convert.ToString(rdr[4]);
                    s5[i] = Convert.ToString(rdr[6]);
                    s6[i] = Convert.ToString(rdr[7]);
                    i++;
                   
                    pic.DoubleClick += new System.EventHandler(this.picdoubleclick);

                }
                rdr.Close();
            }
            
        }
        public void picdoubleclick(object sender,EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            int i = Convert.ToInt32(pic.Name);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            //cmd.CommandText = "select * from PRODUCT";
            cmd.CommandText = "ONEPRODUCT";

            cmd.Parameters.Add("x",s5[i]);

            cmd.Parameters.Add("pno", OracleDbType.RefCursor, ParameterDirection.Output);
            cmd.CommandType = CommandType.StoredProcedure;
            
            OracleDataReader rdr1 = cmd.ExecuteReader();
            string no="";
            if (rdr1.HasRows)
            {
                while (rdr1.Read())
                {
                    no=Convert.ToString(rdr1[0]);
                    break;
                }
            }
            Form3 f3 = new Form3(s1[i],s2[i],s3[i],s4[i],s5[i],s6[i],no);
            f3.ShowDialog();
        }
        public void picdoubleclick_updateordelete(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            int i = Convert.ToInt32(pic.Name);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            //cmd.CommandText = "select * from PRODUCT";
            cmd.CommandText = "ONEPRODUCT";

            cmd.Parameters.Add("x", UN_uord[i]);  

            cmd.Parameters.Add("pno", OracleDbType.RefCursor, ParameterDirection.Output);
            cmd.CommandType = CommandType.StoredProcedure;

            OracleDataReader rdr1 = cmd.ExecuteReader();
            string no = "";
            if (rdr1.HasRows)
            {
                while (rdr1.Read())
                {
                    no = Convert.ToString(rdr1[0]);
                    break;
                }
            }
            Form4 f4 = new Form4(sid[i],s1[i], s2[i], s4[i], s5[i], s6[i], no,UN_uord[i]);
            f4.ShowDialog();
        }
        public PictureBox retpic(int i,string picpath)
        {
            PictureBox Pic = new PictureBox();
            Pic.Name =  i.ToString();
            Pic.Width=252;
            Pic.Height=202;
            Pic.Image = new Bitmap(picpath);


            return Pic;
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public bool checkcategory()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select CATEGORY_NAME from CATEGORY";
            OracleDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    if (comboBox1.Text == rdr[0].ToString())
                    {
                        rdr.Close();
                        return true;
                    }
                }
                
            }
            rdr.Close();
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (entered_flow2 == false)
            {
                int i = 0;
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                //cmd.CommandText = "select * from PRODUCT";
                string command = "";
                int categoryid = -1;
                if (comboBox1.Text != "ALL")
                {
                    if (checkcategory() == false)
                    {
                        MessageBox.Show("no ad for this category right now");
                        return;
                    }
                    OracleCommand cmd2 = new OracleCommand();
                    cmd2.Connection = conn;
                    cmd2.Parameters.Add("catname", comboBox1.Text);
                    OracleParameter param = new OracleParameter();
                    cmd2.Parameters.Add("catid", OracleDbType.Int32, ParameterDirection.Output);
                    cmd2.CommandText = "GETCATID";
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.ExecuteNonQuery();
                    if (cmd2.Parameters["catid"].Value == null)
                    {
                        MessageBox.Show("no ad for that category right now");
                        return;
                    }
                    string id_str = cmd2.Parameters["catid"].Value.ToString();
                    categoryid = Convert.ToInt32(id_str);
                }
                if (comboBox1.Text != "ALL" && textBox2.Text != "")
                {


                    cmd.Parameters.Add("productname", textBox2.Text);
                    cmd.Parameters.Add("num", categoryid);

                    command = "select * from PRODUCT where PRODUCT_NAME LIKE :productname ||'%' and CATEGORY_NO=:num ";
                    cmd.CommandText = command;
                }
                else if (comboBox1.Text == "ALL" && textBox2.Text != "")
                {

                    cmd.Parameters.Add("productname", textBox2.Text);

                    command = "select * from PRODUCT where PRODUCT_NAME LIKE :productname || '%' ";
                    cmd.CommandText = command;
                }
                else if (comboBox1.Text == "ALL" && textBox2.Text == "")
                {
                    command = "select * from PRODUCT  ";
                    cmd.CommandText = command;
                }
                else if (comboBox1.Text != "ALL" && textBox2.Text == "")
                {

                    cmd.Parameters.Add("num", categoryid);
                    command = "select * from PRODUCT where CATEGORY_NO=:num";
                    cmd.CommandText = command;
                }

                rdr = cmd.ExecuteReader();
    
                if (rdr.HasRows)
                {
                    s1 = new string[100];
                    s2 = new string[100];
                    s3 = new string[100];
                    s4 = new string[100];
                    s5 = new string[100];
                    s6 = new string[100];
                    flowLayoutPanel1.Controls.Clear();
                    while (rdr.Read())
                    {
                        PictureBox pic = retpic(i, rdr[7].ToString());
                        flowLayoutPanel1.Controls.Add(pic);
                        s1[i] = Convert.ToString(rdr[1]);
                        s2[i] = Convert.ToString(rdr[2]);
                        s3[i] = Convert.ToString(rdr[3]);
                        s4[i] = Convert.ToString(rdr[4]);
                        s5[i] = Convert.ToString(rdr[6]);
                        s6[i] = Convert.ToString(rdr[7]);
                        i++;

                        pic.DoubleClick += new System.EventHandler(this.picdoubleclick);

                    }
                    rdr.Close();
                }
                else
                {
                    MessageBox.Show("no product with this name");
                }
            }
            else
            {
                int i = 0;
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                //cmd.CommandText = "select * from PRODUCT";
                string command = "";
                int categoryid = -1;
                if (comboBox1.Text != "ALL")
                {
                    if (checkcategory() == false)
                    {
                        MessageBox.Show("no ad for this category right now");
                        return;
                    }
                    OracleCommand cmd2 = new OracleCommand();
                    cmd2.Connection = conn;
                    cmd2.Parameters.Add("catname", comboBox1.Text);
                    OracleParameter param = new OracleParameter();
                    cmd2.Parameters.Add("catid", OracleDbType.Int32, ParameterDirection.Output);
                    cmd2.CommandText = "GETCATID";
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.ExecuteNonQuery();
                    if (cmd2.Parameters["catid"].Value == null)
                    {
                        MessageBox.Show("no ad for that category right now");
                        return;
                    }
                    string id_str = cmd2.Parameters["catid"].Value.ToString();
                    categoryid = Convert.ToInt32(id_str);
                }
                if (comboBox1.Text != "ALL" && textBox2.Text != "")
                {


                    cmd.Parameters.Add("productname", textBox2.Text);
                    cmd.Parameters.Add("num", categoryid);
                    cmd.Parameters.Add("name", txt_name);

                    command = "select * from PRODUCT where PRODUCT_NAME LIKE :productname ||'%' and CATEGORY_NO=:num and USER_NAME=:name";
                    cmd.CommandText = command;
                }
                else if (comboBox1.Text == "ALL" && textBox2.Text != "")
                {

                    cmd.Parameters.Add("productname", textBox2.Text);
                    cmd.Parameters.Add("name", txt_name);

                    command = "select * from PRODUCT where PRODUCT_NAME LIKE :productname || '%' and USER_NAME=:name";
                    cmd.CommandText = command;
                }
                else if (comboBox1.Text == "ALL" && textBox2.Text == "")
                {
                    cmd.Parameters.Add("name", txt_name);
                    command = "select * from PRODUCT and USER_NAME=:name ";
                    cmd.CommandText = command;
                }
                else if (comboBox1.Text != "ALL" && textBox2.Text == "")
                {

                    cmd.Parameters.Add("num", categoryid);
                    cmd.Parameters.Add("name", txt_name);
                    command = "select * from PRODUCT where CATEGORY_NO=:num and USER_NAME=:name";
                    cmd.CommandText = command;
                }

                rdr = cmd.ExecuteReader();
              
                if (rdr.HasRows)
                {
                    s1 = new string[100];
                    s2 = new string[100];
                    s3 = new string[100];
                    s4 = new string[100];
                    s5 = new string[100];
                    s6 = new string[100];
                    flowLayoutPanel1.Controls.Clear();
                    while (rdr.Read())
                    {
                        PictureBox pic = retpic(i, rdr[7].ToString());
                        flowLayoutPanel1.Controls.Add(pic);
                        s1[i] = Convert.ToString(rdr[1]);
                        s2[i] = Convert.ToString(rdr[2]);
                        s3[i] = Convert.ToString(rdr[3]);
                        s4[i] = Convert.ToString(rdr[4]);
                        s5[i] = Convert.ToString(rdr[6]);
                        s6[i] = Convert.ToString(rdr[7]);
                        i++;

                        pic.DoubleClick += new System.EventHandler(this.picdoubleclick);

                    }
                    rdr.Close();
                }
                else
                {
                    MessageBox.Show("no product with this name");
                }
            }

          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel3.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = true;
        }

        public bool checkaccount()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.Parameters.Add("pass",textBox6.Text);
            cmd.Parameters.Add("name",textBox7.Text);

            cmd.CommandText = "select * from USERRTABLE where USERPASSWORD=:pass and USERNAME=:name";


            cmd.CommandType = CommandType.Text;
            OracleDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Close();
                return true;
            }
            
            rdr.Close();
            return false;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            
            txt_name = textBox7.Text;
            txt_pass = textBox6.Text; 
            if (checkaccount() == true)
            {
                tabControl1.SelectedTab = tabPage2;
               

            panel2.Visible = false;
            panel3.Visible = false;
            conn = new OracleConnection(s);
            conn.Open();
            int i = 0;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            //cmd.CommandText = "select * from PRODUCT";
            cmd.Parameters.Add("name",textBox7.Text);
            cmd.CommandText = "select * from PRODUCT where USER_NAME=:name";
      
            cmd.CommandType = CommandType.Text;
           
             rdr = cmd.ExecuteReader();
             sid=new int[100];
             s1 = new string[100];
             s2 = new string[100];

             s3 = new string[100];
             s4 = new string[100];
             s5 = new string[100];
             s6 = new string[100];
             UN_uord = new string[100];
            if(rdr.HasRows){
                while (rdr.Read())
                {
                    PictureBox pic = retpic(i,rdr[7].ToString());
                    flowLayoutPanel2.Controls.Add(pic);
                    sid[i] = Convert.ToInt32(rdr[0]);
                    s1[i] = Convert.ToString(rdr[1]);
                    s2[i] = Convert.ToString(rdr[2]);
                    
                    s4[i] = Convert.ToString(rdr[4]);
                    s5[i] = Convert.ToString(rdr[5]);
                    s6[i] = Convert.ToString(rdr[7]);
                    UN_uord[i] = Convert.ToString(rdr[6]);
                    i++;
                   
                    pic.DoubleClick += new System.EventHandler(this.picdoubleclick_updateordelete);

                }
                rdr.Close();
            }
            txt_name = textBox7.Text;
            txt_pass = textBox6.Text;
            entered_flow2 = true;
            
        }
            
            else
            {
                MessageBox.Show("invalid username and password");
                return;
            }

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (text_pressed == false)
            {
                text_pressed = true;
                textBox2.Text = "";
            } 
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (text_pressed == false)
            {
                text_pressed = true;
                textBox2.Text = "";
            }
            }

        private void textBox2_Leave(object sender, EventArgs e)
        {
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (U1 == false)
            {
                U1 = true;
                textBox7.Text = "";
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (U1 == false)
            {
                U1 = true;
                textBox7.Text = "";
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (p1 == false)
            {
                p1 = true;
                textBox6.Text = "";
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (U2 == false)
            {
                U2 = true;
                textBox1.Text = "";
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (p2 == false)
            {
                p2 = true;
                textBox3.Text = "";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (rp == false)
            {
                rp = true;
                textBox4.Text = "";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("invalid data");
                return;
            }

            conn = new OracleConnection(s);
            conn.Open();

            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select USERNAME from USERRTABLE";
            cmd2.CommandType = CommandType.Text;
            OracleDataReader rdr = cmd2.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    if(textBox1.Text==rdr[0].ToString()){
                        MessageBox.Show("this username already exists");
                        return;
                    }
                }
            }

            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("passwords doesn't match");
                return;
            }
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            

            cmd.Parameters.Add("pass", textBox3.Text);
            cmd.Parameters.Add("name", textBox1.Text);

            cmd.CommandText = "insert into USERRTABLE(USERPASSWORD,USERNAME) values(:pass,:name)";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully added");
            
        }
    }
}
