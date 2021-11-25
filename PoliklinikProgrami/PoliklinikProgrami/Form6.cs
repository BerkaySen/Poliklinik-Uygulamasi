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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Hastahane.mdb");
        OleDbCommand kmt = new OleDbCommand();
        OleDbDataAdapter kayit = new OleDbDataAdapter();
        DataTable tablo = new DataTable();
        DataSet ds1 = new DataSet();
        // private bool _kaydedilsinmi; 
        private void Form6_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            button3.Visible=false;
            button4.Visible = false;
           // textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            dateTimePicker1.Visible = false;
            maskedTextBox1.Visible = false;
            pictureBox1.Visible = false;
            //label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            button2.Focus();
            comboBox2.Items.Add("Enfeksiyon Hastalıkları");
            comboBox2.Items.Add("Ortopedi");
            comboBox2.Items.Add("Nöroloji");
            comboBox2.Items.Add("Kalp Ve Damar Cerrahi");
            comboBox2.Items.Add("Kardiyoloji");
            comboBox2.Items.Add("Beyin Cerrahi");
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult giriskapanis = MessageBox.Show("Programı kapatmak istediğinizden eminmisiniz ? ", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);
            if (giriskapanis == DialogResult.No)
            {
                e.Cancel = true;
                return;

            }
            Environment.Exit(0);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (textBox1.TextLength == 11)
            {
                e.Handled = true;
                errorProvider1.SetError(textBox1, "TC Kimlik Numaranız En Fazla 11 Haneden Oluşur.");
            }
            if (textBox1.TextLength<11)
            {
                errorProvider1.Clear();
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Erkek");
            comboBox1.Items.Add("Kadın");
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen Arama Yapılacak Kriteri Giriniz", "Hata !!!");
            }
            else if (textBox1.TextLength < 11)
            {
                MessageBox.Show("Tc Kimliği Eksik Girdiniz");
            }
            else
            {
                try
                {
                    bag.Open();
                    OleDbCommand kyt = new OleDbCommand("select * from HastaBilgi where TC='" + textBox1.Text + "'", bag);
                    OleDbDataReader ddr = kyt.ExecuteReader();
                    if (ddr.Read())
                    {
                        //  textBox2.Text = ddr["TC"].ToString();
                        textBox3.Text = ddr["Ad"].ToString();
                        textBox4.Text = ddr["Soyad"].ToString();
                        maskedTextBox1.Text = ddr["TelNo"].ToString();
                        comboBox1.SelectedItem = (ddr["Cinsiyet"].ToString());
                        comboBox2.SelectedItem = ddr["Klinik"].ToString();
                        comboBox3.SelectedItem = ddr["Dadi"].ToString();
                      
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\Resimler\" + ddr["Resim"].ToString()+ ".jpeg");
                       
                        dateTimePicker1.Text = ddr["Tarih"].ToString();
                        // string cins = ddr["Cinsiyet"].ToString();
                        // textBox6.Text = ddr["TelNo"].ToString();
                        textBox5.Text = ddr["Hastalık"].ToString();
                    }
                    kyt.Dispose();
                    ddr.Dispose();
                    kmt.Dispose();
                    bag.Close();
                   // label1.Text = ad;
                    button3.Visible = true;
                    button4.Visible = true;
                    // textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = true;
                    textBox5.Visible = true;
                    comboBox1.Visible = true;
                    comboBox2.Visible = true;
                    comboBox3.Visible = true;
                    dateTimePicker1.Visible = true;
                    maskedTextBox1.Visible = true;
                    pictureBox1.Visible = true;
                    //  label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    label7.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;
                    label10.Visible = true;
                    label11.Visible = true;
                    //comboBox1.Items.Add("Erkek");
                    //comboBox1.Items.Add("Kadın");
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Kişi Düzgün Bir Şekilde Kayıt Edilemedi", "Hata !!!" + hata.Message);
                    throw;
                }
            }
          }
        private void textBox1_Leave(object sender, EventArgs e)
        {
           
            
}   
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 fr2 = new Form2();
            this.Hide();
            fr2.ShowDialog();
        }
       // string ad = "";
        private void button4_Click(object sender, EventArgs e)
        {
            /*try
            {*/
               /* openFileDialog1.ShowDialog();
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                ad = "";*/
           // pictureBox1.Dispose();
            try
            {
                
                bag.Open();
                OleDbCommand kyt = new OleDbCommand("select * from HastaBilgi where TC='" + textBox1.Text + "'", bag);
                OleDbDataReader ddr = kyt.ExecuteReader();
                string tc = textBox1.Text;
                if (ddr.Read())
                {
                    string ad = ddr["Resim"].ToString();
                    //  pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\Resimler\" + ddr["Resim"].ToString() + ".jpeg");
                    kyt.Dispose();
                    ddr.Dispose();
                    kmt.Dispose();
                    bag.Close();
                    string yol = Application.StartupPath + "\\Resimler\\" + ad + ".jpg";
                  //  File.Open(yol, FileMode.Open).Close();
                   // File.Delete(yol);
                    openFileDialog1.ShowDialog();
                    //pictureBox1.Image.Dispose();
                    pictureBox1.ImageLocation = openFileDialog1.FileName;
                    File.Delete(Application.StartupPath + @"\Resimler\" + tc + ".jpg");
                   // File.Open(Application.StartupPath + @"\Resimler\" + tc + ".jpg").Dispose();
                    File.Open(yol, FileMode.Open).Close();
                    File.Copy(openFileDialog1.FileName, Application.StartupPath + @"\Resimler\" + tc + ".jpg");
                    openFileDialog1.Dispose();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata oluştu " + hata);
                throw;
            }
               
            /*  }
              catch (Exception sikinti)
              {
                  MessageBox.Show("hata "+sikinti); // 
                  throw;
              }*/
           
           // MessageBox.Show("muhtemelen dosyayı sildik");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Text = "";
            if (comboBox2.SelectedItem == "Enfeksiyon Hastalıkları")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("Op.Dr.Hakan GÜNDOĞAN");
                comboBox3.Items.Add("Op.Dr.Mustafa N. ALİHANOĞLU");
                comboBox3.Items.Add("Op.Dr.Müjde KOYUNCU");
                comboBox3.Items.Add("Op.Dr.Pınar TOKATLIOĞLU");
            }
            else if (comboBox2.SelectedItem == "Ortopedi")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("Op.Dr.Alper BAYRAKTAR");
                comboBox3.Items.Add("Op.Dr.Şükran ÇAKMAK");
                comboBox3.Items.Add("Dr.Meltem ERHAN");
            }
            else if (comboBox2.SelectedItem == "Nöroloji")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("Dr.Erdal DUMAN");
                comboBox3.Items.Add("Op.Dr.Mustafa KARACA");
                comboBox3.Items.Add("Dr.Mine KERMALLİ");
                comboBox3.Items.Add("Dr.Ziya ÇETİN");
                comboBox3.Items.Add("Dr.Kamil TUZGÖL");
            }
            else if (comboBox2.SelectedItem == "Kalp Ve Damar Cerrahi")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("Dr.Sema KARAOĞLU");
                comboBox3.Items.Add("Dr.Ayşegül TEZCAN GERMEN");
                comboBox3.Items.Add("Dr.Ahmet BABACAN");
            }

            else if (comboBox2.SelectedItem == "Kardiyoloji")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("Op.Dr.Hakan GENCE");
                comboBox3.Items.Add("Dr.Aybars AKKOR");
                comboBox3.Items.Add("Prof.Dr.Kaya ÖZKUŞ");
            }
            else if (comboBox2.SelectedItem == "Beyin Cerrahi")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("Dr.D.Yelda DOĞAN");
                comboBox3.Items.Add("Op.Dr.Aslı VURAL SESVER");
                comboBox3.Items.Add("Dr.Sevin KARALAR");
            }
            else
                MessageBox.Show("Lütfen Poliklinlik Adını Seçiniz", "HATA !!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        private void button3_Click(object sender, EventArgs e)
        {
             if (textBox1.Text == "" ||textBox3.Text == "" || maskedTextBox1.Text == "" || textBox4.Text == "" || comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null || maskedTextBox1.Text =="")
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız !!!", "Boş Alan Uyarısı");
            }
            else
             {
                 try
                 {

                     kmt = new OleDbCommand();
                     bag.Open();
                     kmt.Connection = bag;
                     kmt.CommandText = "update HastaBilgi set Ad='" + textBox3.Text + "', Soyad='" + textBox4.Text + "', Hastalık='" + textBox5.Text + "', Cinsiyet='" + comboBox1.SelectedItem  +"', Klinik='" + comboBox2.SelectedItem + "', Dadi='" +comboBox3.SelectedItem + "', Tarih='" +dateTimePicker1.Value + "', TelNo='" + maskedTextBox1.Text + "' where TC='" + textBox1.Text + "'";
                     kmt.ExecuteNonQuery();
                     kmt.Dispose(); 
                     bag.Close();
                     ds1.Clear();
                     textBox1.Text = "";
                    // textBox2.Text = "";
                     textBox3.Text = "";
                     textBox4.Text = "";
                     textBox5.Text = "";
                     maskedTextBox1.Text = "";
                     comboBox1.Text = "";
                     //comboBox1.SelectedItem = null;
                     //comboBox2.SelectedItem = null;
                    // comboBox3.SelectedItem = null;
                     comboBox2.Text = "";
                     comboBox3.Text = "";
                     dateTimePicker1.Text = "";

                     MessageBox.Show("Hasta Başarı İle Güncellendi ...", "Güncelleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                 }
                 catch (Exception hata)
                 {
                     MessageBox.Show("Kişi Kaydedildiği Sırada Hata Oluştu " + hata.Message);
                     throw;
                 }
             }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
