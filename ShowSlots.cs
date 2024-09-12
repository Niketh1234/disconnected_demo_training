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

namespace DisconnectedDemo
{
    public partial class ShowSlots : Form
    {
        DataSet dataSet;
        SqlDataAdapter dataAdapter;
        SqlCommandBuilder commandBuilder;
        DataTable dt;
        public ShowSlots()
        {
            unsafe { int* ptr; }; // native code or unmanaged code
            InitializeComponent();
            dataSet = new DataSet();
            dataAdapter = new SqlDataAdapter("select * from slots", @"server=.\sqlexpress;initial catalog=adodemo;user id=sa;password=Pass@123");
            dataAdapter.Fill(dataSet, "slots");
            dt = dataSet.Tables["slots"];
            dt.PrimaryKey = new[] { dt.Columns["id"] }; 
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataGridView1.DataSource = dataSet.Tables["slots"];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataAdapter.Update(dt);
            MessageBox.Show("Saved into Database");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow row = dt.NewRow();
            row[1] = textBox1.Text;
            row[2] = textBox2.Text;
            row[3] = textBox3.Text;
            dt.Rows.Add(row);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow row = dt.Rows.Find(1);
            dt.Rows.Remove(row);
        }
        
    }
}
