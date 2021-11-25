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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        OleDbConnection bag = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Hastahane.mdb");
        OleDbCommand kmt = new OleDbCommand();
        OleDbDataAdapter kayit = new OleDbDataAdapter();
        OleDbDataReader dr;
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fr1 = new Form1();
            this.Hide();
            fr1.ShowDialog();
        }
        public void rastgele()
        {
            Random rnd = new Random();
            x = rnd.Next(1000, 9999);
            label5.Text = x.ToString();
        }
        string kadi;
        int x;
        private void button2_Click(object sender, EventArgs e)
        {
            kadi = textBox1.Text;
            string sif = textBox2.Text;
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "SELECT * FROM Şifre where KullaniciAdi='" + textBox1.Text + "' AND Sifre='" + textBox2.Text + "'";
            // kmt.Parameters.AddWithValue("@num", no);
            kmt.ExecuteNonQuery();
            dr = kmt.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("onaylandı");
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                button2.Visible = false;
                button3.Visible = true;
                label6.Visible = true;
                textBox1.Visible = false;
                textBox2.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                rastgele();
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

        private void Form8_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            label6.Visible = false;
            button3.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ErrorProvider er = new ErrorProvider();
            if (textBox3.Text != textBox4.Text)
            {
                errorProvider1.SetError(textBox3, "Şifreler Uyuşmuyor");
                er.SetError(textBox4, "Şifreler Uyuşmuyor");
            }
            else if (textBox5.Text != x.ToString())
            {
                er.SetError(textBox5, "Captcha Dogru Biçimde Degil Veya Yanlış");
                rastgele();
            }
            else if (textBox4.Text =="" || textBox3.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
            else
            {
                try
                {
                    kmt = new OleDbCommand();
                    bag.Open();
                    kmt.Connection = bag;
                    kmt.CommandText = "update Şifre set Sifre='" + textBox3.Text + "' where KullaniciAdi='" + textBox1.Text + "'"; ;
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    bag.Close();
                    textBox1.Text = "";
                    // textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    MessageBox.Show("Hasta Başarı İle Güncellendi ...", "Güncelleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Kişi Kaydedildiği Sırada Hata Oluştu " + hata.Message);
                }
            }
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult giriskapanis = MessageBox.Show("Programı kapatmak istediğinizden eminmisiniz ? ", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (giriskapanis == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            Environment.Exit(0);
        }
    }
}
