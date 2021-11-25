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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Hastahane.mdb");
        OleDbCommand kmt = new OleDbCommand();
        OleDbDataAdapter kayit = new OleDbDataAdapter();
        DataTable tablo = new DataTable();
        DataSet ds1 = new DataSet();
        private void liste(string sor)
        {
            bag.Open();
            ds1.Clear();
            kayit = new OleDbDataAdapter(sor, bag);
            kayit.Fill(ds1, "HastaBilgi");
            dataGridView1.DataSource = ds1.Tables["HastaBilgi"];
            bag.Close();
           
        }
        private void button6_Click(object sender, EventArgs e)
        {
            liste("select * from HastaBilgi");
            dataGridView1.Columns[9].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            this.Hide();
            f4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 f7 = new Form6();
            this.Hide();
            f7.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            this.Hide();
            f3.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            this.Hide();
            f5.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        NotifyIcon icon = new NotifyIcon();
        string mesaj;
        private void Form2_Load(object sender, EventArgs e)
        {
            label5.ForeColor = Color.DarkRed;
            timer1.Enabled = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            button7.Visible = false;
            string görevli_adi = Form1.kadi;
            label4.ForeColor = Color.Blue;
            label4.Text ="Hoşgeldin "+ görevli_adi;
            label3.ForeColor = Color.Red;
            dateTimePicker1.Enabled = false;
            label2.Visible = false;
            label1.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            pictureBox1.ImageLocation = @"logo.jpeg";
            icon.Icon = new Icon(@"favicon.ico");
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            icon.Visible = false;
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult giriskapanis = MessageBox.Show("Programı kapatmak istediğinizden eminmisiniz ? ", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (giriskapanis == DialogResult.No)
            {
                e.Cancel = true;
                return;

            }
            Environment.Exit(0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Visible = false;
            button7.Visible = true;
            label2.Visible = true;
            label1.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            button8.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            liste("SELECT * from HastaBilgi where Ad like'"+textBox1.Text+"%'" );
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            liste("SELECT * from HastaBilgi where Soyad like'" + textBox2.Text + "%'");
        }
        int seçilensatır;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dateTimePicker1.Text = ds1.Tables["HastaBilgi"].Rows[e.RowIndex]["Tarih"].ToString();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            button8.Visible = true;
            button7.Visible = false;
        }
        int ran1 = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] sehir = new string[5];
            if (label5.Left > -340)
            {
                label5.Left -= 10;
            }
            else
            {
                string görevli =Form1.kadi;
                label5.Left = 600;
                Random rnd = new Random();
                int ran= rnd.Next(0, 2);
                
                if (ran1 ==0)
                {
                    label5.Text="Bir Berkay Şen Klasiği ...";
                    ran1++;
                }
                else if (ran1==1)
                {
                    label5.Text="Uyarı Lütfen Kullanıcı ve Hasta Güvenliği için Şifrenizi kimseyle paylaşmayınız";
                    ran1++;
                }
                else if(ran1==2)
                {
                    label5.Text = "Maaşınız Bu Ay Gecikmeli Yatacaktır Sayın " + görevli;
                    ran1++;
                }
                else if (ran1==3)
                {
                    label5.Text = "Kolay Gelsin";
                    ran1 = 0;
                }   
            }
        }

        private void label5_Move(object sender, EventArgs e)
        {

        }

        private void label5_MouseMove(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

    }
}