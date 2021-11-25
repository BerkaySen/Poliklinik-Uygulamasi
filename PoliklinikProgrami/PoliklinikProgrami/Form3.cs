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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Hastahane.mdb");
        OleDbCommand kmt = new OleDbCommand();
        OleDbDataAdapter kayit = new OleDbDataAdapter();
        DataTable tablo = new DataTable();
        DataSet ds1 = new DataSet();
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            maskedTextBox1.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || maskedTextBox1.Text == "" || textBox4.Text == "" || comboBox1.SelectedItem == "" || comboBox2.SelectedItem == "" || comboBox3.SelectedItem == "")
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız !!!", "Boş Alan Uyarısı");
            }

            else if (!_kaydedilsinmi)
            {
                DialogResult mes;
                mes = MessageBox.Show("Hasta Bilgileri Kısımından Detaylı Bilgiye Bakabilirsiniz. Hasta Bilgileri Sayfamızı Otomatik Açalım mı ?","Bu kayıt veritabanında zaten var. !!!",MessageBoxButtons.YesNoCancel);
                if (mes == DialogResult.Yes)
                {
                    Form4 fr4 = new Form4();
                    this.Hide();
                    fr4.ShowDialog();
                }
                else if (mes == DialogResult.No)
                {
                    textBox1.Text = "";
                }
                else if (mes == DialogResult.Cancel)
                {
                    
                }
                return;
            } 
            else
            {
                try
                {
                    bag.Open();
                    kmt.Connection = bag;
                    //kmt.CommandText = "insert into ogrenci(numara,adi,soyadi,sinifi,telno) values (" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "'," + textBox5.Text + ")";
                    kmt.CommandText = "insert into HastaBilgi(TC,Ad,Soyad,Cinsiyet,TelNo,Hastalık,Klinik,Dadi,Tarih,Resim) values(@tc,@ad,@soyad,@cins,@telno,@hast,@Klin,@dokadi,@tarih,Res)";
                    kmt.Parameters.AddWithValue(@"tc", textBox1.Text);
                    kmt.Parameters.AddWithValue(@"ad", textBox2.Text);
                    kmt.Parameters.AddWithValue(@"soyad", textBox3.Text);
                    kmt.Parameters.AddWithValue(@"cins", comboBox3.SelectedItem);
                    //kmt.Parameters.AddWithValue(@"telno", textBox5.Text);
                    kmt.Parameters.AddWithValue(@"telno", maskedTextBox1.Text);
                    kmt.Parameters.AddWithValue(@"hast", textBox4.Text);
                    kmt.Parameters.AddWithValue(@"klin", comboBox1.SelectedItem);
                    kmt.Parameters.AddWithValue(@"dokadi", comboBox2.SelectedItem);
                    kmt.Parameters.AddWithValue(@"tarih", dateTimePicker1.Value);//resim diye ekleme yapıcaz
                    kmt.Parameters.AddWithValue(@"Res", textBox1.Text);//
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    bag.Close();
                    File.Copy(openFileDialog1.FileName, Application.StartupPath + @"\Resimler\" + textBox1.Text + ".jpeg");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    maskedTextBox1.Text = "";
                   // comboBox1.SelectedText = "";
                    comboBox1.SelectedItem = null;
                   // comboBox1.SelectedItem = "";
                    //comboBox1.SelectedValue = "";
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    dateTimePicker1.Text = "";
                    pictureBox1.Image = null;
                    MessageBox.Show("Hasta Başarı İle Kaydedildi ...", "Kayıt Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Kişi Kaydedildiği Sırada Hata Oluştu"+hata.Message);
                    //throw;
                }
                //kayıt butonu
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.ShowDialog();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            comboBox3.Items.Add("Erkek");
            comboBox3.Items.Add("Kadın");
            comboBox1.Items.Add("Enfeksiyon Hastalıkları");
            comboBox1.Items.Add("Ortopedi");
            comboBox1.Items.Add("Nöroloji");
            comboBox1.Items.Add("Kalp Ve Damar Cerrahi");
            comboBox1.Items.Add("Kardiyoloji");
            comboBox1.Items.Add("Beyin Cerrahi");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Enfeksiyon Hastalıkları")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Op.Dr.Hakan GÜNDOĞAN");
                comboBox2.Items.Add("Op.Dr.Mustafa N. ALİHANOĞLU");
                comboBox2.Items.Add("Op.Dr.Müjde KOYUNCU");
                comboBox2.Items.Add("Op.Dr.Pınar TOKATLIOĞLU");
            }
            else if (comboBox1.SelectedItem == "Ortopedi")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Op.Dr.Alper BAYRAKTAR");
                comboBox2.Items.Add("Op.Dr.Şükran ÇAKMAK");
                comboBox2.Items.Add("Dr.Meltem ERHAN");
            }
            else if (comboBox1.SelectedItem == "Nöroloji")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Dr.Erdal DUMAN");
                comboBox2.Items.Add("Op.Dr.Mustafa KARACA");
                comboBox2.Items.Add("Dr.Mine KERMALLİ");
                comboBox2.Items.Add("Dr.Ziya ÇETİN");
                comboBox2.Items.Add("Dr.Kamil TUZGÖL");
            }
            else if (comboBox1.SelectedItem == "Kalp Ve Damar Cerrahi")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Dr.Sema KARAOĞLU");
                comboBox2.Items.Add("Dr.Ayşegül TEZCAN GERMEN");
                comboBox2.Items.Add("Dr.Ahmet BABACAN");
            }

            else if (comboBox1.SelectedItem == "Kardiyoloji")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Op.Dr.Hakan GENCE");
                comboBox2.Items.Add("Dr.Aybars AKKOR");
                comboBox2.Items.Add("Prof.Dr.Kaya ÖZKUŞ");
            }
            else if (comboBox1.SelectedItem == "Beyin Cerrahi")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Dr.D.Yelda DOĞAN");
                comboBox2.Items.Add("Op.Dr.Aslı VURAL SESVER");
                comboBox2.Items.Add("Dr.Sevin KARALAR");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Resim Dosyası (*.JPEG)|*.jpeg |Png Dosyası (*.png)|*.png |JPG Dosyası (*.jpg)|*.jpg";
            openFileDialog1.Title = "Fotoğraf Seçiniz";
            openFileDialog1.FilterIndex = 3;
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (textBox1.TextLength >= 11)
            {
                e.Handled = true;
               // MessageBox.Show(,"Hata");
                errorProvider1.SetError(textBox1,"TC Kimliğiniz 11 Haneden Daha Fazla Olamaz ");
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private bool _kaydedilsinmi; 
        private void textBox1_Leave(object sender, EventArgs e)
        {
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText="select count(*) from HastaBilgi where TC=@tc";
            kmt.Parameters.AddWithValue("@tc", textBox1.Text);
            if (Convert.ToInt32(kmt.ExecuteScalar()) > 0)
            {
                MessageBox.Show("Bu kayıt veritabanında zaten var.");
                textBox1.Text = "";
            }
            
            else if (textBox1.TextLength < 11)
            {
                errorProvider1.SetError(textBox1, "TC Kimlik Numaranız 11 Haneden Oluşmalıdır.");
              
            }
            else
            {
                errorProvider1.Clear();
                _kaydedilsinmi = true;
            }
           // kmt.ExecuteNonQuery();
            kmt.Dispose();
            bag.Close();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult giriskapanis = MessageBox.Show("Programı kapatmak istediğinizden eminmisiniz ? ", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (giriskapanis == DialogResult.No)
            {
                e.Cancel = true;
                return;

            }
            Environment.Exit(0);
        }

        private void comboBox3_DropDown(object sender, EventArgs e)
        {

        }
    }
}
