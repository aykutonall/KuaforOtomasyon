using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace _bitirmeKuafor
{
    public partial class YeniKayit : Form
    {
        public YeniKayit()
        {
            InitializeComponent();
        }

        string connect, sorgu;
        OleDbConnection yeni;
        OleDbDataAdapter verial;
        DataSet al;
        public DataTable tablo = new DataTable();

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb");

        private void YeniKayit_Load(object sender, EventArgs e)
        {
            this.Text = "** Önal Kuaför ve Güzellik Salonu ** www.onalkuafor.com.tr";
            timer1.Interval = 100;
            timer1.Enabled = true;

            //Form açıldığında tüm nesneler kapalı olacak.
            txtmusno.Enabled = false;
            txtadsoyad.Enabled = false;
            mskdogumtrh.Enabled = false;
            mskevliliktrh.Enabled = false;
            mskilkziytrh.Enabled = false;
            cmbcinsiyet.Enabled = false;
            cmbmusteritip.Enabled = false;
            cmbkangrubu.Enabled = false;
            txtoneren.Enabled = false;
            txtemail.Enabled = false;
            txtmeslek.Enabled = false;
            mskcep.Enabled = false;
            txtsac.Enabled = false;
            txtnot.Enabled = false;
            txtEvAdres.Enabled = false;
            mskEvTel.Enabled = false;
            txtEvSemt.Enabled = false;
            txtIsAdres.Enabled = false;
            mskIsTel.Enabled = false;
            txtIsSemt.Enabled = false;
            txtmusno.Focus();

            //Combobox itemleri ekleniyor..
            cmbcinsiyet.Items.Add("");
            cmbcinsiyet.Items.Add("Bay");
            cmbcinsiyet.Items.Add("Bayan");

            cmbmusteritip.Items.Add("");
            cmbmusteritip.Items.Add("Genel");
            cmbmusteritip.Items.Add("Özel");
            /*
            cmbkangrubu.Items.Add("");
            cmbkangrubu.Items.Add("A Rh  (+)");
            cmbkangrubu.Items.Add("A Rh  (-)");
            cmbkangrubu.Items.Add("B Rh  (+)");
            cmbkangrubu.Items.Add("B Rh  (-)");
            cmbkangrubu.Items.Add("AB Rh (+)");
            cmbkangrubu.Items.Add("AB Rh (-)");
            cmbkangrubu.Items.Add("0 Rh  (+)");
            cmbkangrubu.Items.Add("0 Rh  (-)");
            */
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(1) + this.Text.Substring(0, 1);
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            //Kan Grubu combobox itemlerini veritabanındaki kan grubu tablosundan çekiyoruz.
            connect = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "SELECT * FROM KANGRUBU";
            yeni = new OleDbConnection(connect);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "KANGRUBU");
            cmbkangrubu.DataSource = al.Tables["KANGRUBU"];
            cmbkangrubu.DisplayMember = "KanGrubu";
            cmbkangrubu.ValueMember = "id";

            //Kontroller açılıyor..
            txtmusno.Enabled = true;
            txtadsoyad.Enabled = true;
            mskdogumtrh.Enabled = true;
            mskevliliktrh.Enabled = true;
            mskilkziytrh.Enabled = true;
            cmbcinsiyet.Enabled = true;
            cmbmusteritip.Enabled = true;
            cmbkangrubu.Enabled = true;
            txtoneren.Enabled = true;
            txtemail.Enabled = true;
            txtmeslek.Enabled = true;
            mskcep.Enabled = true;
            txtsac.Enabled = true;
            txtnot.Enabled = true;
            txtEvAdres.Enabled = true;
            mskEvTel.Enabled = true;
            txtEvSemt.Enabled = true;
            txtIsAdres.Enabled = true;
            mskIsTel.Enabled = true;
            txtIsSemt.Enabled = true;
            txtmusno.Focus();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if(baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                string sorgu = "insert into MUSTERI(MusteriNo,AdiSoyadi,DogumTarihi,EvlilikTarihi,Cinsiyet,IlkZiyaretTarihi,MusteriTipi,Email,Meslek,KanGrubu,CepTel,OnerenKisi,SacRengi,EvAdres,EvTelefon,EvSemt,IsAdres,IsTelefon,IsSemt,Aciklama) values ('" + txtmusno.Text + "','" + txtadsoyad.Text + "','" + mskdogumtrh.Text + "','" + mskevliliktrh.Text + "','" + cmbcinsiyet.Text + "','" + mskilkziytrh.Text + "','" + cmbmusteritip.Text + "','" + txtemail.Text + "','" + txtmeslek.Text + "','" + cmbkangrubu.Text + "','" + mskcep.Text + "','" + txtoneren.Text + "','" + txtsac.Text + "','" + txtEvAdres.Text + "','" + mskEvTel.Text + "','" + txtEvSemt.Text + "','" + txtIsAdres.Text + "','" + mskIsTel.Text + "','" + txtIsSemt.Text + "','" + txtnot.Text + "')";

                OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
                /*
                komut.Parameters.Add("@MusteriNo", txtmusno.Text);
                komut.Parameters.Add("@AdiSoyadi", txtadsoyad.Text);
                komut.Parameters.Add("@DogumTarihi", mskdogumtrh.Text);
                komut.Parameters.Add("@EvlilikTarihi", mskevliliktrh.Text);
                komut.Parameters.Add("@Cinsiyet", cmbcinsiyet.Text);
                komut.Parameters.Add("@IlkZiyaretTarihi", mskilkziytrh.Text);
                komut.Parameters.Add("@MusteriTipi", cmbmusteritip.Text);
                komut.Parameters.Add("@Email", txtemail.Text);
                komut.Parameters.Add("@Meslek", txtmeslek.Text);
                komut.Parameters.Add("@KanGrubu", cmbkangrubu.Text);
                komut.Parameters.Add("@CepTel", mskcep.Text);
                komut.Parameters.Add("@OnerenKisi", txtoneren.Text);
                komut.Parameters.Add("@SacRengi", txtsac.Text);
                komut.Parameters.Add("@EvAdres", txtEvAdres.Text);
                komut.Parameters.Add("@EvTelefon", mskEvTel.Text);
                komut.Parameters.Add("@EvSemt", txtEvSemt.Text);
                komut.Parameters.Add("@IsAdres", txtIsAdres.Text);
                komut.Parameters.Add("@IsTelefon", mskIsTel.Text);
                komut.Parameters.Add("@IsSemt", txtIsSemt.Text);
                komut.Parameters.Add("@Aciklama", txtnot.Text);
                */
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt işlemi başarıyla gerçekleşti.");
            }
            

            //Temizle
            txtmusno.Clear();
            txtadsoyad.Clear();
            mskdogumtrh.Clear();
            mskevliliktrh.Clear();
            mskilkziytrh.Clear();
            cmbcinsiyet.Text = "";
            cmbmusteritip.Text = "";
            cmbkangrubu.Text = "";
            txtoneren.Clear();
            txtemail.Clear();
            txtmeslek.Clear();
            mskcep.Clear();
            txtsac.Clear();
            txtnot.Clear();
            txtEvAdres.Clear();
            mskEvTel.Clear();
            txtEvSemt.Clear();
            txtIsAdres.Clear();
            mskIsTel.Clear();
            txtIsSemt.Clear();

            //Kayıt yapıldıktan sonra nesnelerimizi tekrar kapatıyoruz.
            txtmusno.Enabled = false;
            txtadsoyad.Enabled = false;
            mskdogumtrh.Enabled = false;
            mskevliliktrh.Enabled = false;
            mskilkziytrh.Enabled = false;
            cmbcinsiyet.Enabled = false;
            cmbmusteritip.Enabled = false;
            cmbkangrubu.Enabled = false;
            txtoneren.Enabled = false;
            txtemail.Enabled = false;
            txtmeslek.Enabled = false;
            mskcep.Enabled = false;
            txtsac.Enabled = false;
            txtnot.Enabled = false;
            txtEvAdres.Enabled = false;
            mskEvTel.Enabled = false;
            txtEvSemt.Enabled = false;
            txtIsAdres.Enabled = false;
            mskIsTel.Enabled = false;
            txtIsSemt.Enabled = false;
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            //Temizle
            txtmusno.Clear();
            txtadsoyad.Clear();
            mskdogumtrh.Clear();
            mskevliliktrh.Clear();
            mskilkziytrh.Clear();
            cmbcinsiyet.Text = "";
            cmbmusteritip.Text = "";
            cmbkangrubu.Text = "";
            txtoneren.Clear();
            txtemail.Clear();
            txtmeslek.Clear();
            mskcep.Clear();
            txtsac.Clear();
            txtnot.Clear();
            txtEvAdres.Clear();
            mskEvTel.Clear();
            txtEvSemt.Clear();
            txtIsAdres.Clear();
            mskIsTel.Clear();
            txtIsSemt.Clear();

            //Kayıt yapıldıktan sonra nesnelerimizi tekrar kapatıyoruz.
            txtmusno.Enabled = false;
            txtadsoyad.Enabled = false;
            mskdogumtrh.Enabled = false;
            mskevliliktrh.Enabled = false;
            mskilkziytrh.Enabled = false;
            cmbcinsiyet.Enabled = false;
            cmbmusteritip.Enabled = false;
            cmbkangrubu.Enabled = false;
            txtoneren.Enabled = false;
            txtemail.Enabled = false;
            txtmeslek.Enabled = false;
            mskcep.Enabled = false;
            txtsac.Enabled = false;
            txtnot.Enabled = false;
            txtEvAdres.Enabled = false;
            mskEvTel.Enabled = false;
            txtEvSemt.Enabled = false;
            txtIsAdres.Enabled = false;
            mskIsTel.Enabled = false;
            txtIsSemt.Enabled = false;
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
