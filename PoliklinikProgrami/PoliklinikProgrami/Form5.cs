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
using System.IO;

namespace PoliklinikProgrami
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();

        }
        OleDbConnection bag = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Hastahane.mdb");
        OleDbCommand kmt = new OleDbCommand();
        OleDbDataAdapter kayit = new OleDbDataAdapter();
        DataTable tablo = new DataTable();
        DataSet ds1 = new DataSet();
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength < 11 || textBox1.TextLength > 11)
            {
                MessageBox.Show("T.C Numarası 11 Haneden Daha Az Olamaz Veya Daha Fazla Olamaz !!!");
            }
            else if (!_kaydedilsinmi)
            {
                DialogResult secim = MessageBox.Show("Girdiğiniz bilgilere uygun hasta mevcuttur silinsin mi ?", "Silinsinmi ?", MessageBoxButtons.YesNo);
                if (secim==DialogResult.Yes)
                {
                    try
                    {
                        kmt.Connection = bag;
                        bag.Open();
                        kmt.CommandText = "delete from HastaBilgi where TC='" + textBox1.Text + "'";
                        kmt.ExecuteNonQuery();
                        bag.Close();
                        label1.Text = "";
                        label2.Text = "";
                        label3.Text = "";
                        label4.Text = "";
                        label5.Text = "";
                        label6.Text = "";
                        label7.Text = "";
                        MessageBox.Show("Hasta Kayıdı Güvenli Bir Şeklide Silindi","Hasta Silindi",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show("Hasta Silinemedi "+ hata.Message);
                        //throw;
                    }
                    
                }
                else if (secim == DialogResult.No)
                {
                    
                }
                textBox1.Text = "";
                return;
            }
            else
            {
                MessageBox.Show("Belirtilen TC kimlikte Hasta Bulunmamaktadır.", "Tekrar Deneyiniz.");
                textBox1.Text = "";
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 fr2 = new Form2();
            this.Hide();
            fr2.ShowDialog();
        }

        private void Form5_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
                if (textBox1.TextLength == 11)
                {
                    e.Handled = true;
                    MessageBox.Show("TC Kimlik Numaranız 11 Haneden Fazla olamaz", "Hata !!!");
                    textBox1.Text = "";
                }
            }
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
           
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
           // label1.Text = (Application.StartupPath+@"\Resimler\" + "ad" + ".jpeg".ToString());
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
        }
        private bool _kaydedilsinmi;
        private void textBox1_Leave(object sender, EventArgs e)
        {
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "select count(*) from HastaBilgi where TC=@tc";
            kmt.Parameters.AddWithValue("@tc", textBox1.Text);
            if (Convert.ToInt32(kmt.ExecuteScalar()) > 0)
            {
              //  MessageBox.Show("Bu kayıt veritabanında zaten var.");
            }
            else
            {
                _kaydedilsinmi = true;
            }
            // kmt.ExecuteNonQuery();
            kmt.Dispose();
            bag.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen Arama Yapılacak Kriteri Giriniz", "Hata !!!");
            }
            else
            {
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                bag.Open();
                OleDbCommand kyt = new OleDbCommand("select * from HastaBilgi where TC='" + textBox1.Text + "'", bag);
                OleDbDataReader ddr = kyt.ExecuteReader();
                if (ddr.Read())
                {
                    label2.Text = ddr["TC"].ToString();
                    label3.Text = ddr["Ad"].ToString();
                    label4.Text = ddr["Soyad"].ToString();
                    label5.Text = ddr["Cinsiyet"].ToString();
                    label6.Text = ddr["TelNo"].ToString();
                    label7.Text = ddr["Hastalık"].ToString();

                }
                bag.Close();
            }
            
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult giriskapanis = MessageBox.Show("Programı kapatmak istediğinizden eminmisiniz ? ", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (giriskapanis == DialogResult.No)
            {
                e.Cancel = true;
                return;

            }
            Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }
    }
}
