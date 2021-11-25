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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Hastahane.mdb");
        OleDbCommand kmt = new OleDbCommand();
        OleDbDataAdapter kayit = new OleDbDataAdapter();
        int güv;
        private void rnd()
        {
            Random rnd = new Random();
            güv = rnd.Next(1000, 9999);
            label4.ForeColor = Color.Red;
            label4.Text = güv.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text !=textBox3.Text)
            {
                MessageBox.Show("Şifreler Uyuşmuyor");
                textBox2.Text = "";
                textBox3.Text = "";
            }
            else if (textBox4.Text != güv.ToString())
            {
                MessageBox.Show("Güvenlik sorusunu bilemediniz","Tekrar Deneyiniz");
                rnd();
                textBox2.Text = "";
                textBox3.Text = "";
            }
            else if (textBox1.Text != "" && textBox2.Text == textBox3.Text && textBox4.Text == güv.ToString())
            {
                bag.Open();
                kmt.Connection = bag;
                //kmt.CommandText = "insert into ogrenci(numara,adi,soyadi,sinifi,telno) values (" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "'," + textBox5.Text + ")";
                kmt.CommandText = "insert into Şifre(KullaniciAdi,Sifre) values(@kullanici,@Sifre)";
                kmt.Parameters.AddWithValue(@"kullanici", textBox1.Text);
                kmt.Parameters.AddWithValue(@"Sifre", textBox2.Text);
                kmt.ExecuteNonQuery();
                kmt.Dispose();
                bag.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }
        
        private void Form7_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            rnd();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 fr1 = new Form1();
            this.Hide();
            fr1.ShowDialog();
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult giriskapanis = MessageBox.Show("Programı kapatmak istediğinizden eminmisiniz ? ", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (giriskapanis == DialogResult.No)
            {
                e.Cancel = true;
                return;

            }
            Environment.Exit(0);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            DialogResult cikis = new DialogResult();
            //bura yazılacak
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "select count(*) from Şifre where KullaniciAdi=@kadi";
            kmt.Parameters.AddWithValue("@kadi", textBox1.Text);
            if (Convert.ToInt32(kmt.ExecuteScalar()) > 0)
            {
                cikis= MessageBox.Show("Bu kayıt veritabanında zaten var. Şifrenizi değiştirelim mi ?","Hata",MessageBoxButtons.YesNo);
                if (DialogResult.Yes==cikis)
                {
                    Form8 fr8 = new Form8();
                    this.Hide();
                    fr8.ShowDialog();
                }
                else 
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
                
            }
            kmt.Dispose();
            bag.Close();
        }
    }
}
