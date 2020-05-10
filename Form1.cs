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
    public partial class Form1 : Form
    {
        string s = "Data Source=orcl;User Id=scott;Password=tiger;";
        string img;
        OracleConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "All files (*.*)|*.*";
                if (dialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
                {
                    img=dialog.FileName;
                    pictureBox1.ImageLocation = img;
                    
                }
            }
            catch(Exception){

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(s);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" ||textBox5.Text == "" || textBox7.Text == ""||comboBox1.Text=="")
            {
                MessageBox.Show("invalid data");
                return;
            }
/*            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
         
            cmd.Parameters.Add("pass", textBox2.Text);
            cmd.Parameters.Add("name", textBox1.Text);

            cmd.CommandText = "insert into USERRTABLE(USERPASSWORD,USERNAME) values(:pass,:name)";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
 * */

            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "select max(PRODUCT_ID) from PRODUCT";

            OracleDataReader rdr = cmd1.ExecuteReader();
            int id=0;
           
           if (rdr.Read())
             {
                 if (rdr.HasRows&&rdr[0]!=DBNull.Value)
                 {
                     id = Convert.ToInt32(rdr[0]);
                     id++;
                 }
              }
            
            else
            {
                id = 0;
            }
            rdr.Close();

            OracleCommand cmdd = new OracleCommand();
            cmdd.Connection = conn;
            cmdd.Parameters.Add("name", Form2.txt_name);
            cmdd.Parameters.Add("phonenumber", Convert.ToInt32(textBox5.Text));
            cmdd.CommandText = "select * from USER_PNO where USER_NAME=:name and PHONENO=:phonenumber";
            OracleDataReader rdrr= cmdd.ExecuteReader();
            if (rdrr.HasRows==false)
            {

                OracleCommand cmd3 = new OracleCommand();
                cmd3.Connection = conn;
                cmd3.Parameters.Add("name", Form2.txt_name);
                cmd3.Parameters.Add("phonenumber", Convert.ToInt32(textBox5.Text));

                cmd3.CommandText = "insert into USER_PNO values(:name,:phonenumber)";
                cmd3.CommandType = CommandType.Text;
                cmd3.ExecuteNonQuery();
              
            }
          
            if (textBox6.Text != "")
            {
                OracleCommand cmddd = new OracleCommand();
                cmddd.Connection = conn;
                cmddd.Parameters.Add("name", Form2.txt_name);
                cmddd.Parameters.Add("phonenumber", Convert.ToInt32(textBox6.Text));
                cmddd.CommandText = "select * from USER_PNO where USER_NAME=:name and PHONENO=:phonenumber";
                OracleDataReader rdrrr = cmddd.ExecuteReader();
                if (rdrrr.HasRows == false)
                {
                    OracleCommand cmd7 = new OracleCommand();
                    cmd7.Connection = conn;
                    cmd7.Parameters.Add("name", Form2.txt_name);
                    cmd7.Parameters.Add("phonenumber2", Convert.ToInt32(textBox6.Text));

                    cmd7.CommandText = "insert into USER_PNO values(:name,:phonenumber2)";
                    cmd7.CommandType = CommandType.Text;
                    cmd7.ExecuteNonQuery();
                }
            }

            int catcount=0;

            OracleCommand cmd4 = new OracleCommand();
            cmd4.Connection = conn;
            cmd4.CommandText = "select CATEGORY_NAME,CATEGORY_ID from CATEGORY ";
            cmd4.CommandType = CommandType.Text;

            OracleDataReader rdr2 = cmd4.ExecuteReader();
            bool categorynameexists=false;
            while (rdr2.Read())
            {
                if(comboBox1.Text==Convert.ToString(rdr2[0])){
                    categorynameexists = true;
                    catcount = Convert.ToInt32(rdr2[1]);
                    break;
                }
            }
            rdr2.Close();
          
            if (categorynameexists == false)
            {
                OracleCommand cmd5 = new OracleCommand();
                cmd5.Connection = conn;
                cmd5.CommandText = "select max(CATEGORY_ID) from CATEGORY ";
                cmd5.CommandType = CommandType.Text;
                OracleDataReader rdr3 = cmd5.ExecuteReader();
                catcount = 0;
                if (rdr3.Read())
                {
                    if (rdr3.HasRows && rdr3[0] != DBNull.Value)
                    {
                        catcount = Convert.ToInt32(rdr3[0]);
                        catcount++;
                    }
                }
                else
                {
                    catcount = 0;
                }
                rdr3.Close();

                OracleCommand cmd6 = new OracleCommand();
                cmd6.Connection = conn;
                cmd6.Parameters.Add("catid", catcount);
                cmd6.Parameters.Add("catname", comboBox1.Text);
                cmd6.CommandText = "insert into CATEGORY values(:catid,:catname)";
                cmd6.CommandType = CommandType.Text;
                cmd6.ExecuteNonQuery();
            }

            /*OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.Parameters.Add("id", id);
            cmd2.Parameters.Add("productname", textBox3.Text);
            cmd2.Parameters.Add("category", catcount);
            cmd2.Parameters.Add("description", textBox4.Text);
            //cmd2.Parameters.Add("phonenumber", Convert.ToInt32(textBox5.Text));
            //cmd2.Parameters.Add("phonenumber2", Convert.ToInt32(textBox6.Text));
            cmd2.Parameters.Add("price", Convert.ToInt32(textBox7.Text));
            cmd2.Parameters.Add("photo", "ssss");
            cmd2.Parameters.Add("name", textBox1.Text);
            //cmd2.Parameters.Add("date", "2018-01-25");
            cmd2.CommandText = "insert into PRODUCT (PRODUCT_ID,PRODUCT_NAME,PRICE,DESCRIPTION,CATEGORY_NO,USER_NAME,PRODUCT_PHOTO) values(:id,:productname,:price,:description,:category,:name,:photo)";
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();

            */
            conn.Close();
            conn.Open();
            OracleCommand cmd8 = new OracleCommand();
            cmd8.Connection = conn;


            cmd8.Parameters.Add("id", id);
            cmd8.Parameters.Add("productname", textBox3.Text);
            cmd8.Parameters.Add("price", Convert.ToInt32(textBox7.Text));
            cmd8.Parameters.Add("pdate", DateTime.Now);
            cmd8.Parameters.Add("description", textBox4.Text);
            cmd8.Parameters.Add("category", catcount);
            //cmd2.Parameters.Add("phonenumber", Convert.ToInt32(textBox5.Text));
            //cmd2.Parameters.Add("phonenumber2", Convert.ToInt32(textBox6.Text));
            cmd8.Parameters.Add("name", Form2.txt_name);
            cmd8.Parameters.Add("photo", img);
            
             
           
          //  cmd2.CommandText = "insert into PRODUCT (PRODUCT_ID,PRODUCT_NAME,PRICE,DESCRIPTION,CATEGORY_NO,USER_NAME,PRODUCT_PHOTO) values(:id,:productname,:price,:description,:category,:name,:photo)";
           cmd8.CommandText = "insert into PRODUCT (PRODUCT_ID,PRODUCT_NAME,PRICE,PRODUCT_DATE,PRODUCT_DESC,CATEGORY_NO,USER_NAME,PHOTO) values(:id,:productname,:price,:pdate,:description,:category,:name,:photo)";
             //cmd2.CommandText="insert into PRODUCT (PRODUCT_ID,PRODUCT_NAME,PRICE,DESCRIPTION,CATEGORY_NO,USER_NAME,PRODUCT_PHOTO) values(0,'frs',423,'gwraef',0,'hosam','tbr')";
            
            //cmd2.Parameters.Add("date", "2018-01-25");
            //cmd8.CommandText = "insert into PRODUCT (PRODUCT_ID) values(:id)";
            ///cmd2.CommandType = CommandType.Text;
            cmd8.ExecuteNonQuery();



            MessageBox.Show("done");

        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
