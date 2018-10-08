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
    public partial class Malzemeler : Form
    {
        public Malzemeler()
        {
            InitializeComponent();
        }

        string baglanti, sorgu;
        OleDbConnection yeni;
        OleDbCommand uygula;
        OleDbDataAdapter verial;
        DataSet al;
        OleDbCommandBuilder cb;
        public DataTable tablo = new DataTable();

        private void Malzemeler_Load(object sender, EventArgs e)
        {
            txtUrunAra.Focus();
            this.Text = " ** Önal Kuaför ve Güzellik Salonu ** www.onalkuafor.com.tr ";
            timer1.Interval = 100;
            timer1.Enabled = true;

            txtUrunNo.Enabled = false;
            txtUrunAdi.Enabled = false;
            txtAciklama.Enabled = false;

            // Veriler gösteriliyor.
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "Select * From URUNLER";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "URUNLER");
            grdUrun.DataSource = al.Tables["URUNLER"];
            yeni.Close();
            grdUrun.ReadOnly = true;
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            txtUrunNo.Clear();
            txtUrunAdi.Clear();
            txtAciklama.Clear();

            txtUrunNo.Enabled = true;
            txtUrunAdi.Enabled = true;
            txtAciklama.Enabled = true;
            txtUrunNo.Focus();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "INSERT INTO URUNLER(UrunNo,UrunAdi,Aciklama) VALUES ('" + txtUrunNo.Text + "','" + txtUrunAdi.Text + "','" + txtAciklama.Text + "')";
            yeni = new OleDbConnection(baglanti);
            uygula = new OleDbCommand(sorgu, yeni);
            uygula.Connection.Open();
            uygula.ExecuteNonQuery();
            MessageBox.Show("Kayıt Başarılı");

            // Veriler gösteriliyor.
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "Select * From URUNLER";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "URUNLER");
            grdUrun.DataSource = al.Tables["URUNLER"];
            yeni.Close();
            grdUrun.ReadOnly = true;

            //Temizle
            txtUrunNo.Clear();
            txtUrunAdi.Clear();
            txtAciklama.Clear();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            CurrencyManager sonraki = (CurrencyManager)this.BindingContext[al.Tables["URUNLER"]];
            sonraki.EndCurrentEdit();
            cb = new OleDbCommandBuilder(verial); // OleDURUNbDataAdapter'i silme, güncelleme ve kayıt'a hazır hale getirmek için
            verial.Update(al, "URUNLER");
            al.Clear();
            verial.Fill(al.Tables[0]);

            grdUrun.DataSource = al.Tables[0];
            MessageBox.Show(" Güncelleme tamamlandı!", "İŞLEM TAMAM");

            // Temizle
            txtUrunNo.Clear();
            txtUrunAdi.Clear();
            txtAciklama.Clear();

            txtUrunNo.Enabled = false;
            txtUrunAdi.Enabled = false;
            txtAciklama.Enabled = false;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // Grid Üzerinden Silme İşlemi Yapılıyor.
            CurrencyManager sonraki = (CurrencyManager)this.BindingContext[al.Tables["URUNLER"]];
            DialogResult uyar;
            uyar = MessageBox.Show(this, txtUrunNo.Text + " Kaydı Silmek istiyor musunuz?", "SİLME UYARISI", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2);
            if (uyar == DialogResult.Yes)
            {
                cb = new OleDbCommandBuilder(verial); // OleDbDataAdapter'i silme, güncelleme ve kayıt'a hazır hale getirmek için
                sonraki.RemoveAt(sonraki.Position);
                verial.Update(al, "URUNLER");
                al.Clear();
                verial.Fill(al.Tables[0]);
                grdUrun.DataSource = al.Tables[0];
                MessageBox.Show(" Kayıt silindi!", "İŞLEM TAMAM");
            }
            else
            {
                MessageBox.Show("  Silme işlemi İptal edildi!");
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            // Temizle
            txtUrunNo.Clear();
            txtUrunAdi.Clear();
            txtAciklama.Clear();

            txtUrunNo.Enabled = false;
            txtUrunAdi.Enabled = false;
            txtAciklama.Enabled = false;
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(1) + this.Text.Substring(0, 1);
        }

        public void ciftTikla()
        {
            // Çift Clickle Textboxlar Yükleniyor.
            txtUrunNo.DataBindings.Clear();
            txtUrunAdi.DataBindings.Clear();
            txtAciklama.DataBindings.Clear();

            txtUrunNo.DataBindings.Add("Text", al.Tables[0], "UrunNo");
            txtUrunAdi.DataBindings.Add("Text", al.Tables[0], "UrunAdi");
            txtAciklama.DataBindings.Add("Text", al.Tables[0], "Aciklama");

            txtUrunNo.Enabled = true;
            txtUrunAdi.Enabled = true;
            txtAciklama.Enabled = true;
        }

        private void grdUrun_DoubleClick(object sender, EventArgs e)
        {
            ciftTikla();
        }

        private void txtUrunAra_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select id,UrunNo,UrunAdi,Aciklama From URUNLER", yeni);

            if (txtUrunAra.Text == "")
            {

                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "Select id,UrunNo,UrunAdi,Aciklama From URUNLER";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "URUNLER");
                grdUrun.DataSource = al.Tables["URUNLER"];
            }

            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }

            verial.SelectCommand.CommandText = "Select id,UrunNo,UrunAdi,Aciklama From URUNLER" + " where(UrunAdi like '%" + txtUrunAra.Text + "%' )";
            tablo.Clear();
            verial.Fill(tablo);
            grdUrun.DataSource = tablo;
            yeni.Close();
        }
    }
}
