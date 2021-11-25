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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Hastahane.mdb");
        OleDbCommand kmt = new OleDbCommand();
        OleDbDataAdapter kayit = new OleDbDataAdapter();
        DataSet ds1 = new DataSet();
        OleDbDataReader dr;
        public static string kadi;
        private void button1_Click(object sender, EventArgs e)
        {
            kadi = textBox1.Text;
            string sif = textBox2.Text;
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "SELECT * FROM Şifre where KullaniciAdi='" + textBox1.Text + "' AND Sifre='" + textBox2.Text + "'";
            kmt.ExecuteNonQuery();
            dr = kmt.ExecuteReader();
            if (dr.Read())
            {
                Form2 f2 = new Form2();
                this.Hide();
                f2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ya da şifre yanlış");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            kmt.Dispose();
            bag.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult giriskapanis = MessageBox.Show("Programı kapatmak istediğinizden eminmisiniz ? ", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (giriskapanis == DialogResult.No)
            {
                e.Cancel = true;
                return;

            }
            else
               Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form7 fr7 = new Form7();
            this.Hide();
            fr7.ShowDialog();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            button1.Focus();
        }
    }
}
