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
    public partial class SaticiFirmalar : Form
    {
        public SaticiFirmalar()
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

        private void SaticiFirmalar_Load(object sender, EventArgs e)
        {
            this.Text = " ** Önal Kuaför ve Güzellik Salonu ** www.onalkuafor.com.tr ";
            timer1.Interval = 100;
            timer1.Enabled = true;

            txtFirmaAdi.Enabled = false;
            txtYetkili.Enabled = false;
            txtAdres.Enabled = false;
            mskIsTel.Enabled = false;
            mskCepTel.Enabled = false;


            // Veriler gösteriliyor.

            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "Select * From SATICI";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "SATICI");
            grdSatici.DataSource = al.Tables["SATICI"];
            yeni.Close();
            grdSatici.ReadOnly = true;
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            txtFirmaAdi.Enabled = true;
            txtYetkili.Enabled = true;
            txtAdres.Enabled = true;
            mskIsTel.Enabled = true;
            mskCepTel.Enabled = true;

            //temizle
            txtFirmaAdi.Clear();
            txtYetkili.Clear();
            txtAdres.Clear();
            mskIsTel.Clear();
            mskCepTel.Clear();
            txtFirmaAdi.Focus();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "INSERT INTO SATICI(FirmaAdi,Yetkili,Adres,IsTelefon,CepTel) VALUES ('" + txtFirmaAdi.Text + "','" + txtYetkili.Text + "','" + txtAdres.Text + "','" + mskIsTel.Text + "','" + mskCepTel.Text + "')";
            yeni = new OleDbConnection(baglanti);
            uygula = new OleDbCommand(sorgu, yeni);
            uygula.Connection.Open();
            uygula.ExecuteNonQuery();
            MessageBox.Show("Kayıt Başarılı..");
            
            // Veriler gösteriliyor.
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "Select * From SATICI";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "SATICI");
            grdSatici.DataSource = al.Tables["SATICI"];
            yeni.Close();
            grdSatici.ReadOnly = true;

            // Temizle
            txtFirmaAdi.Clear();
            txtYetkili.Clear();
            txtAdres.Clear();
            mskIsTel.Clear();
            mskCepTel.Clear();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            CurrencyManager sonraki = (CurrencyManager)this.BindingContext[al.Tables["SATICI"]];
            sonraki.EndCurrentEdit();
            cb = new OleDbCommandBuilder(verial); // OleDbDataAdapter'i silme, güncelleme ve kayıt'a hazır hale getirmek için
            verial.Update(al, "SATICI");
            al.Clear();
            verial.Fill(al.Tables[0]);

            grdSatici.DataSource = al.Tables[0];
            MessageBox.Show(" Güncelleme tamamlandı!", "İŞLEM TAMAM");

            // Temizle
            txtFirmaAdi.Clear();
            txtYetkili.Clear();
            txtAdres.Clear();
            mskIsTel.Clear();
            mskCepTel.Clear();

            txtFirmaAdi.Enabled = false;
            txtYetkili.Enabled = false;
            txtAdres.Enabled = false;
            mskIsTel.Enabled = false;
            mskCepTel.Enabled = false;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // Grid Üzerinden Silme İşlemi Yapılıyor.
            CurrencyManager sonraki = (CurrencyManager)this.BindingContext[al.Tables["SATICI"]];
            DialogResult uyar;
            uyar = MessageBox.Show(this, txtFirmaAdi.Text + " Kaydı Silmek istiyor musunuz?", "SİLME UYARISI", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2);
            if (uyar == DialogResult.Yes)
            {
                cb = new OleDbCommandBuilder(verial); // OleDbDataAdapter'i silme, güncelleme ve kayıt'a hazır hale getirmek için
                sonraki.RemoveAt(sonraki.Position);
                verial.Update(al, "SATICI");
                al.Clear();
                verial.Fill(al.Tables[0]);
                grdSatici.DataSource = al.Tables[0];
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
            txtFirmaAdi.Clear();
            txtYetkili.Clear();
            txtAdres.Clear();
            mskIsTel.Clear();
            mskCepTel.Clear();

            txtFirmaAdi.Enabled = false;
            txtYetkili.Enabled = false;
            txtAdres.Enabled = false;
            mskIsTel.Enabled = false;
            mskCepTel.Enabled = false;
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ciftTikla()
        {
            // Çift Clickle Textboxlar Yükleniyor.
            txtFirmaAdi.DataBindings.Clear();
            txtYetkili.DataBindings.Clear();
            txtAdres.DataBindings.Clear();
            mskIsTel.DataBindings.Clear();
            mskCepTel.DataBindings.Clear();

            txtFirmaAdi.DataBindings.Add("Text", al.Tables[0], "FirmaAdi");
            txtYetkili.DataBindings.Add("Text", al.Tables[0], "Yetkili");
            txtAdres.DataBindings.Add("Text", al.Tables[0], "Adres");
            mskIsTel.DataBindings.Add("Text", al.Tables[0], "IsTelefon");
            mskCepTel.DataBindings.Add("Text", al.Tables[0], "CepTel");
        }

        private void grdSatici_DoubleClick(object sender, EventArgs e)
        {
            ciftTikla();

            txtFirmaAdi.Enabled = true;
            txtYetkili.Enabled = true;
            txtAdres.Enabled = true;
            mskIsTel.Enabled = true;
            mskCepTel.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(1) + this.Text.Substring(0, 1);
        }

        private void txtFirmaAra_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select id,FirmaAdi,Yetkili,Adres,IsTelefon,CepTel From SATICI", yeni);

            if (txtFirmaAra.Text == "")
            {

                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "Select id,FirmaAdi,Yetkili,Adres,IsTelefon,CepTel From SATICI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "SATICI");
                grdSatici.DataSource = al.Tables["SATICI"];
            }

            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }

            verial.SelectCommand.CommandText = "Select id,FirmaAdi,Yetkili,Adres,IsTelefon,CepTel From SATICI" + " where(FirmaAdi like '%" + txtFirmaAra.Text + "%' )";
            tablo.Clear();
            verial.Fill(tablo);
            grdSatici.DataSource = tablo;
            yeni.Close();
        }
    }
}
