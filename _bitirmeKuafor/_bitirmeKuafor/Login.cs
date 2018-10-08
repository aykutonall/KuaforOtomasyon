using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace _bitirmeKuafor
{
    public partial class Login : Form
    {

        public AnaForm frm1;

        public Login()
        {
            InitializeComponent();
        }

        public OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb");
        public OleDbCommand komut = new OleDbCommand();
        public OleDbDataAdapter adptr = new OleDbDataAdapter();
        public DataSet dtset = new DataSet();

        private void btngiris_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(txtguvenlik.Text) == int.Parse(lblguvenlik.Text))
                {
                    int kayit_no;
                    dataView1.Sort = "KullaniciAdi"; //Dataview1'i kullanici adina göre sıralar.
                    kayit_no = dataView1.Find(txtkullanici.Text); //textbox2 e girilen kullanıcı adının dataview de(veritabanında) arar.
                    if (kayit_no != -1) //arama ile gelen sonuç -1 den farklıysa yani o kullanıcı varsa
                    {
                        dataView1.Sort = "Sifre";
                        kayit_no = dataView1.Find(txtsifre.Text);
                        if (kayit_no != -1)
                        {
                            ProgressForm prog = new ProgressForm();
                            prog.Show();
                            this.Hide();
                        }
                        else lbletiket.Text = "Kullanıcı Adı veya Şifre Hatalı !!"; //eğer aramada -1 üretilirse hata mesajının görüntülenmesini sağlıyor.
                    }
                    else lbletiket.Text = "Kullanıcı Adı veya Şifre Hatalı !!";
                }
                else lbletiket.Text = "Güvenlik Kodu Hatalı !!";
            }
            catch
            {               
                ;//hata oluşursa hiçbirşey yapma
            }
        }

        public void liste()
        {
            baglanti.Open();
            OleDbDataAdapter adptr = new OleDbDataAdapter("select * from LOGIN", baglanti);
            adptr.Fill(dtset, "LOGIN");
            dataView1.Table = dtset.Tables["LOGIN"];
            adptr.Dispose();
            baglanti.Close();
        }

        Random rand = new Random();

        public void randomsayi()
        {
            lblguvenlik.Text = "";
            lblguvenlik.Text = rand.Next(10).ToString() + rand.Next(10).ToString() + rand.Next(10).ToString() + rand.Next(10).ToString();
            //4 haneli 0 ile 10 arasında rastgele sayı üretilip label'a yazdırılıyor.
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.Text = "Önal Kuaför ve Güzellik Salonu";
            timer1.Interval = 100;
            timer1.Enabled = true;

            txtkullanici.Enabled = false;
            txtsifre.Enabled = false;
            liste();
            randomsayi();
            txtguvenlik.Focus();
        }

        private void btnyenile_Click(object sender, EventArgs e)
        {
            lblguvenlik.Text = "";
            randomsayi();
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Programdan Çıkış Yapılsın mı?", "Çıkış", MessageBoxButtons.YesNoCancel);
            Application.Exit();
        }
    }
}
