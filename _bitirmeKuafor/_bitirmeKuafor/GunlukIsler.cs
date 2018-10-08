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
    public partial class GunlukIsler : Form
    {
        public GunlukIsler()
        {
            InitializeComponent();
        }

        string baglanti, sorgu;
        OleDbConnection yeni;
        OleDbCommand uygula;
        OleDbDataAdapter verial;
        DataSet al;
        public DataTable tablo = new DataTable();

        private void GunlukIsler_Load(object sender, EventArgs e)
        {
            //Personel combobox ına personel tablosundan personelleri çekiyoruz..
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "SELECT * FROM PERSONEL";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "PERSONEL");
            cmbPersonelAdSoyad.DataSource = al.Tables["PERSONEL"];
            cmbPersonelAdSoyad.DisplayMember = "AdiSoyadi";
            cmbPersonelAdSoyad.ValueMember = "id";
            cmbPersonelAdSoyad.Text = "";

            //DataGridview'de kayıtlı müşteriler gösteriliyor...
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "SELECT MusteriNo,AdiSoyadi,Meslek,DogumTarihi FROM MUSTERI";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "MUSTERI");
            grdGunlukIs.DataSource = al.Tables["MUSTERI"];
            yeni.Close();
            grdGunlukIs.ReadOnly = true;
            dtTarih.Text = DateTime.Now.ToShortDateString();

            //Yapılan İş Combobox Itemler Ekleniyor..
            cmbYapilanIs.Items.Add("");
            cmbYapilanIs.Items.Add("Saç");
            cmbYapilanIs.Items.Add("Sakal");
            cmbYapilanIs.Items.Add("Saç + Sakal");
            cmbYapilanIs.Items.Add("Cilt Bakımı");
            cmbYapilanIs.Items.Add("Boya");
            cmbYapilanIs.Items.Add("Yıkama");
            cmbYapilanIs.Items.Add("Damat Traşı");
            cmbYapilanIs.Items.Add("Ağda");
            cmbYapilanIs.Items.Add("Epilasyon");
            cmbYapilanIs.Items.Add("Solaryum");
            cmbYapilanIs.Items.Add("Maske");

            //DataGridwiev Rengi
            this.grdGunlukIs.RowsDefaultCellStyle.BackColor = Color.White;
            this.grdGunlukIs.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;

            //DataGridwiev Biçim
            grdGunlukIs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            grdGunlukIs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grdGunlukIs.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            grdGunlukIs.GridColor = Color.Black;
            grdGunlukIs.RowHeadersVisible = false;

            dtTarih.Enabled = false;
            txtMusteriAdSoyad.Enabled = false;
        }

        public void tikla()
        {
            txtMusteriAdSoyad.DataBindings.Clear();
            txtMusteriAdSoyad.DataBindings.Add("Text", al.Tables[0], "AdiSoyadi");
        }

        private void grdGunlukIs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tikla();
        }

        private void cmbYapılanIs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbYapilanIs.Text == "")
            {
                txtTutar.Text = "0";
            }

            if (cmbYapilanIs.Text == "Saç")
            {
                txtTutar.Text = "20";
            }
            if (cmbYapilanIs.Text == "Sakal")
            {
                txtTutar.Text = "5";
            }

            if (cmbYapilanIs.Text == "Saç + Sakal")
            {
                txtTutar.Text = "25";
            }
            if (cmbYapilanIs.Text == "Cilt Bakımı")
            {
                txtTutar.Text = "50";
            }
            if (cmbYapilanIs.Text == "Boya")
            {
                txtTutar.Text = "100";
            }
            if (cmbYapilanIs.Text == "Yıkama")
            {
                txtTutar.Text = "5";
            }
            if (cmbYapilanIs.Text == "Damat Traşı")
            {
                txtTutar.Text = "100";
            }
            if (cmbYapilanIs.Text == "Ağda")
            {
                txtTutar.Text = "80";
            }
            if (cmbYapilanIs.Text == "Epilasyon")
            {
                txtTutar.Text = "150";
            }
            if (cmbYapilanIs.Text == "Solaryum")
            {
                txtTutar.Text = "200";
            }
            if (cmbYapilanIs.Text == "Maske")
            {
                txtTutar.Text = "30";
            }
        }

        private void btnYapılanIsListesi_Click(object sender, EventArgs e)
        {
            GunlukYapilanIs ac = new GunlukYapilanIs();
            ac.Show();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "INSERT INTO YAPILAN_ISLER(Tarih,MusteriAdiSoyadi,PersonelAdiSoyadi,YapilanIs,Tutar) VALUES ('" + dtTarih.Text + "','" + txtMusteriAdSoyad.Text + "','" + cmbPersonelAdSoyad.Text + "','" + cmbYapilanIs.Text + "','" + txtTutar.Text + "')";
            yeni = new OleDbConnection(baglanti);
            uygula = new OleDbCommand(sorgu, yeni);
            uygula.Connection.Open();
            uygula.ExecuteNonQuery();
            MessageBox.Show("Kayıt Başarılı!");

            //Temizle
            dtTarih.Text = "";
            txtMusteriAdSoyad.Clear();
            cmbYapilanIs.Text = "";
            txtTutar.Clear();
            cmbPersonelAdSoyad.Text = "";
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
