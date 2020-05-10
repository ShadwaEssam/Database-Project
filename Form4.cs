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
    public partial class Form4 : Form
    {
        OracleConnection conn;
        string s = "Data Source=orcl;User Id=scott;Password=tiger;";
        int Pid;
        string firstPN;
        string username;
        string img;

        public Form4(int id,string s1, string s2, string s4, string s5, string s6, string s7,string user_name)
        {

          
            InitializeComponent();
            conn = new OracleConnection(s);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.Parameters.Add("id", id);
            cmd.CommandText = "select CATEGORY_NAME from CATEGORY where CATEGORY_ID=:id";
            OracleDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                if (rdr.Read())
                {
                    comboBox1.Text = rdr[0].ToString();
                }
            }
          
            textBox1.Text = s1;
            textBox2.Text = s2;
            textBox3.Text = s4;
            textBox4.Text = s7;
           
            pictureBox1.Image = new Bitmap(s6);
            img = s6;  
          //  comboBox1.Text = s7;
            Pid = id;
            firstPN = s7;
            username = user_name;
          
            
        }

        private void Form4_Load(object sender, EventArgs e)
        {
           
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All files (*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                img = dialog.FileName;
                pictureBox1.ImageLocation = img;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            //cmd.CommandText = "select * from PRODUCT";
            cmd.Parameters.Add("id", Pid);
            cmd.CommandText = "delete from PRODUCT where PRODUCT_ID=:id";

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            MessageBox.Show("product deleted");
            this.Close();
            Form6.form_exit.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("invalid data");
                return;
            }
            int cat_id=0;
            

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.Parameters.Add("Catname",comboBox1.Text);
            cmd.CommandText = "select CATEGORY_ID from CATEGORY where CATEGORY_NAME=:Catname";
            OracleDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                if (rdr.Read())
                {
                    cat_id = Convert.ToInt32(rdr[0]);
                }
                rdr.Close();
            }
            else
            {
               if(comboBox1.Items.Contains(comboBox1.Text)){
                   OracleCommand cmd5 = new OracleCommand();
                   cmd5.Connection = conn;
                   cmd5.CommandText = "select max(CATEGORY_ID) from CATEGORY ";
                   cmd5.CommandType = CommandType.Text;
                   OracleDataReader rdr3 = cmd5.ExecuteReader();
                   int catcount = 0;
                   if (rdr3.Read())
                   {
                       if (rdr3.HasRows && rdr3[0] != DBNull.Value)
                       {
                           catcount = Convert.ToInt32(rdr3[0]);
                           catcount++;
                       }
                       
                   }
                   cat_id = catcount;
                   rdr3.Close();

                   OracleCommand cmd6 = new OracleCommand();
                   cmd6.Connection = conn;
                   cmd6.Parameters.Add("catid", catcount);
                   cmd6.Parameters.Add("catname", comboBox1.Text);
                   cmd6.CommandText = "insert into CATEGORY values(:catid,:catname)";
                   cmd6.CommandType = CommandType.Text;
                   cmd6.ExecuteNonQuery();

               }
               else
               {
                   MessageBox.Show("No category with that name");
                   return;
               }
            }

            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.Parameters.Add("Pname", textBox1.Text);           
            cmd1.Parameters.Add("Price", Convert.ToInt32(textBox2.Text));
            cmd1.Parameters.Add("Descr", textBox3.Text);
            cmd1.Parameters.Add("Catid", cat_id);
            cmd1.Parameters.Add("photo", img);
            cmd1.Parameters.Add("P_ID", Pid);

            cmd1.CommandText = "update PRODUCT set PRODUCT_NAME=:Pname,PRICE=:Price,PRODUCT_DESC=:Descr,CATEGORY_NO=:Catid,PHOTO=:photo where PRODUCT_ID=:P_ID";

            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

      /*      OracleCommand cmd3 = new OracleCommand();
            cmd3.Connection = conn;
            bool name_exist=false;
            cmd3.CommandText = "select * from USERRTABLE";
            OracleDataReader rdr4 = new OracleDataReader();
            if (rdr4.HasRows)
            {
                while (rdr4.Read())
                {
                    if(){

                    }
                }
            }
            */
            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "UPDATEPN" ;
            cmd2.Parameters.Add("name", username);
            cmd2.Parameters.Add("PNnew", Convert.ToInt32(textBox4.Text));
            cmd2.Parameters.Add("PNold",Convert.ToInt32(firstPN));
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.ExecuteNonQuery();
           
            MessageBox.Show("Updated");
            this.Close();       
            Form6.form_exit.Close();
       //     Form6.form_exit.ShowDialog();
            

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
            this.Close();

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
