﻿using System;
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
    public partial class Form5 : Form
    {
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;
        DataSet ds1;
        DataSet ds2;
        string s = "Data Source=orcl;User Id=scott;Password=tiger;";
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
          
            ds = new DataSet();
            ds1 = new DataSet();
            ds2 = new DataSet();
            adapter = new OracleDataAdapter("select * from PRODUCT", s);
          
            adapter.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];

            OracleDataAdapter adapter1 = new OracleDataAdapter("select * from USERRTABLE", s);
           
            adapter1.Fill(ds1);
            dataGridView1.DataSource = ds1.Tables[0];
            OracleDataAdapter adapter2 = new OracleDataAdapter("select * from CATEGORY", s);
            
            adapter2.Fill(ds2);
            dataGridView2.DataSource=ds2.Tables[0];

   /*         if (radioButton1.Checked==true)
            {

                DataSet masterset = new DataSet();
                adapter1 = new OracleDataAdapter("select * from USERRTABLE", s);
                adapter1.Fill(masterset, "FK1");
                adapter.Fill(masterset, "FK2");

                DataRelation r = new DataRelation("fk", masterset.Tables[0].Columns["USERNAME"], masterset.Tables[1].Columns["USER_NAME"]);
                masterset.Relations.Add(r);

                BindingSource master = new BindingSource(masterset, "FK1");
                BindingSource detail = new BindingSource(master, "fk");

                dataGridView1.DataSource = master;
                dataGridView3.DataSource = detail;
            }
            else if (radioButton2.Checked==true)
            {

                DataSet masterset = new DataSet();
                adapter1 = new OracleDataAdapter("select * from CATEGORY", s);
                adapter1.Fill(masterset, "FK1");
                adapter.Fill(masterset, "FK2");

                DataRelation r = new DataRelation("fk", masterset.Tables[0].Columns["CATEGORY_ID"], masterset.Tables[1].Columns["CATEGORY_NO"]);
                masterset.Relations.Add(r);

                BindingSource master = new BindingSource(masterset, "FK1");
                BindingSource detail = new BindingSource(master, "fk");

                dataGridView2.DataSource = master;
                dataGridView3.DataSource = detail;

            }
    * */

        }

        private void button1_Click(object sender, EventArgs e)
        {
            builder = new OracleCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);
            MessageBox.Show("done");
        }


        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
  
      
          
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
           
    

           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            DataSet masterset = new DataSet();
           OracleDataAdapter adapter1 = new OracleDataAdapter("select * from USERRTABLE", s);
            adapter1.Fill(masterset, "FK1");
            adapter.Fill(masterset, "FK2");

            DataRelation r = new DataRelation("fk", masterset.Tables[0].Columns["USERNAME"], masterset.Tables[1].Columns["USER_NAME"]);
            masterset.Relations.Add(r);

            BindingSource master = new BindingSource(masterset, "FK1");
            BindingSource detail = new BindingSource(master, "fk");

            dataGridView1.DataSource = master;
            dataGridView3.DataSource = detail;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            DataSet masterset = new DataSet();
            OracleDataAdapter adapter1 = new OracleDataAdapter("select * from CATEGORY", s);
            adapter1.Fill(masterset, "FK1");
            adapter.Fill(masterset, "FK2");

            DataRelation r = new DataRelation("fk", masterset.Tables[0].Columns["CATEGORY_ID"], masterset.Tables[1].Columns["CATEGORY_NO"]);
            masterset.Relations.Add(r);

            BindingSource master = new BindingSource(masterset, "FK1");
            BindingSource detail = new BindingSource(master, "fk");

            dataGridView2.DataSource = master;
            dataGridView3.DataSource = detail;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
