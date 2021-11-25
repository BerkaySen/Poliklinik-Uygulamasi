using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace PoliklinikProgrami
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
            OleDbConnection bag = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Hastahane.mdb");
            OleDbCommand kmt = new OleDbCommand();
            OleDbDataAdapter kayit = new OleDbDataAdapter();
            DataTable tablo = new DataTable();
            DataSet ds1 = new DataSet();
            //OleDbDataReader dr = new OleDbDataReader();
        public void hastalis(string sorgu)
        {
            bag.Open();
            ds1.Clear();
            kayit = new OleDbDataAdapter(sorgu, bag);
            kayit.Fill(ds1, "HastaBilgi");
            dataGridView1.DataSource = ds1.Tables["HastaBilgi"];
            bag.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.ShowDialog();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView1.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {
                bag.Open();
                OleDbCommand kyt = new OleDbCommand("select * from HastaBilgi where TC='"+textBox1.Text+"'",bag);
                OleDbDataReader ddr = kyt.ExecuteReader();
                if (ddr.Read())
                {
                    label4.Text = ddr["TC"].ToString();
                    label5.Text = ddr["Ad"].ToString();
                    label6.Text = ddr["Soyad"].ToString();
                    label7.Text = ddr["Cinsiyet"].ToString();
                    label8.Text = ddr["TelNo"].ToString();
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\Resimler\" + ddr["Resim"].ToString()+".jpeg");
                    bag.Close();
                }
                else if (textBox1.TextLength < 11 || textBox1.TextLength > 11)
                {
                    MessageBox.Show("TC niz 11 haneden az yada daha fazla olamaz !!");
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    label7.Visible = false;
                    label8.Visible = false;
                }
                label4.ForeColor = Color.Red;
                label5.ForeColor = Color.Red;
                label6.ForeColor = Color.Red;
                label7.ForeColor = Color.Red;
                label8.ForeColor = Color.Red;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                /*hastalis("select * from HastaBilgi where TC='" + textBox1.Text + "'");
                textBox1.Text = "";
                textBox2.Text = "";*/

            }
           
            else
            {
                MessageBox.Show("Lütfen Hangi Başlıga Göre Kişi Araması Yapmak İstediğinizi Belirtiniz !!!","Eksik Veri Hatası");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //label1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); datagriddeki tıklanan hücredekini labela atar. 
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            
        }
    }
}

