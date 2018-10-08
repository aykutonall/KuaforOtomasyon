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
    public partial class Hatirlatma : Form
    {
        //Veritabanı Bağlantı Cümlesi
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb");

        public Hatirlatma()
        {
            InitializeComponent();
        }

        public void veri_oku()
        { 
            //Veritabanındaki bilgilerin ListViewde gözükmesi için küçük bir alt program
            OleDbCommand veri = new OleDbCommand("select * from HATIRLATMA order by Tarih", baglan);
            OleDbDataReader oku = null;
            baglan.Open();
            oku = veri.ExecuteReader();
            listListele.Items.Clear();
            while (oku.Read())
            {
                ListViewItem kayit = new ListViewItem(oku["id"].ToString());
                kayit.SubItems.Add(oku["HatirlatmaNotu"].ToString());
                kayit.SubItems.Add(oku["Tarih"].ToString());
                listListele.Items.Add(kayit);
            }
            oku.Close();
            baglan.Close();
        }

        private void Hatirlatma_Load(object sender, EventArgs e)
        {
            veri_oku();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Eğer bugününü tarihinden eski bir tarihe hatırlatma notu eklemeye kalktığınızda uyarı verecektir.
            if (Convert.ToDateTime(dateTimePicker1.Text) < DateTime.Now)
            {
                MessageBox.Show("İlerki Bir Tarih Yazınız...");
            }

            else
            {
                try
                {
                    //Textboxlara girilen değerleri veritabanımıza kaydetme kodları
                    OleDbCommand kaydet = new OleDbCommand("insert into HATIRLATMA (HatirlatmaNotu,Tarih) values('" + txtHatirlatmaKonusu.Text + "','" + dateTimePicker1.Text + "')", baglan);
                    baglan.Open();
                    kaydet.ExecuteNonQuery();
                    baglan.Close();
                    veri_oku();
                    MessageBox.Show("Hatırlatma Notu Başarılı Bir Şekilde Kaydedildi.", "Önal Kuaför", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch
                {
                    MessageBox.Show("Kayıt Yapılamadı, Lütfen Tekrar Deneyiniz...");
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Veritabanında Silme İşlemi Yapar..
            try
            {
                int a;
                a = int.Parse(listListele.SelectedItems[0].Text);
                OleDbCommand sil = new OleDbCommand("delete from HATIRLATMA where id =" + a + "", baglan);
                baglan.Open();
                sil.ExecuteNonQuery();
                baglan.Close();
                veri_oku();
                MessageBox.Show("Silme İşlemi Başarılı...", "Önal Kuaför", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch
            {
                MessageBox.Show("Lütfen Silmek İstediğiniz Kayıtı Aşağıdaki Tablodan Seçiniz...", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Veritabanında Güncelleme Yapar...
            try
            {
                int id = 0;
                OleDbCommand guncelle = new OleDbCommand("update HATIRLATMA set HatirlatmaNotu='" + txtHatirlatmaKonusu.Text + "',Tarih='" + dateTimePicker1.Text + "' where id=" + id + "", baglan);
                baglan.Open();
                guncelle.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Güncelleme İşlemi Başarılı...", "Önal Kuaför", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                veri_oku();
            }
            catch
            {
                MessageBox.Show("Lütfen Güncellemek İstediğiniz Kayıtı Aşağıdaki Tablodan Seçiniz...", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listListele_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Listview'den seçtiğimiz kayıtın hatırlatma_notu alanındaki yazının Textbox'e gelmesini sağlar. 
            try
            {
                int id = 0;
                id = Convert.ToInt32(listListele.SelectedItems[0].SubItems[0].Text);
                OleDbCommand veri = new OleDbCommand("select HatirlatmaNotu from HATIRLATMA where Tarih='" + listListele.SelectedItems[0].SubItems[2].Text + "' and id=" + id + "", baglan);
                OleDbDataReader oku = null;
                baglan.Open();
                oku = veri.ExecuteReader();
                while (oku.Read())
                {
                    txtHatirlatmaKonusu.Text = oku.GetString(0).ToString();

                }
                oku.Close();
                baglan.Close();
            }
            catch
            {


            }
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            //Tarihe Göre Arama Yapmak İçin
            if (baglan.State == ConnectionState.Closed) baglan.Open();
            string sorgu = "select id,Tarih from HATIRLATMA where Tarih like'" + txtAra.Text + "%' order by Tarih";
            OleDbCommand komut = new OleDbCommand(sorgu, baglan);
            OleDbDataReader oku = komut.ExecuteReader();
            listListele.Items.Clear();
            while (oku.Read())
            {
                ListViewItem kayit = new ListViewItem(oku["id"].ToString());
                kayit.SubItems.Add(oku["Tarih"].ToString());

                listListele.Items.Add(kayit);
            }
            baglan.Close();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
