using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Net.Mail;

namespace _bitirmeKuafor
{
    public partial class Mail : Form
    {
        public Mail()
        {
            InitializeComponent();
        }

        string baglanti, sorgu;
        OleDbConnection yeni;
        OleDbCommand uygula;
        OleDbDataAdapter verial;
        DataSet al;
        DataView goster;
        OleDbDataReader oku;

        private void Mail_Load(object sender, EventArgs e)
        {
            this.Text = "** Önal Kuaför ve Güzellik Salonu ** www.onalkuafor.com.tr";
            timer1.Interval = 100;
            timer1.Enabled = true;

            txtMailAdresim.Enabled = false;
            txtMailAdresim.Text = "aykutonall@hotmail.com";

            //Konu combobox yükleniyor..
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu= "Select * From KONU";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "KONU");
            cmbKonu.DataSource = al.Tables["KONU"];
            cmbKonu.DisplayMember = "Konu";
            cmbKonu.ValueMember = "id";

            //Veriler gösteriliyor..
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "Select AdiSoyadi, MusteriTipi, Email From MUSTERI";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "MUSTERI");
            grdMail.DataSource = al.Tables["MUSTERI"];
            yeni.Close();
            grdMail.ReadOnly = true;

            //DataGridwiev Rengi
            this.grdMail.RowsDefaultCellStyle.BackColor = Color.White;
            this.grdMail.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;

            //DataGridwiev Biçim
            grdMail.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            grdMail.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grdMail.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            grdMail.GridColor = Color.Black;
            grdMail.RowHeadersVisible = false;
        }

        void mailGonder()
        {
            MailMessage _mail = new MailMessage(); // MailMessage nesnemizi oluşturuyoruz.
            _mail.From = new MailAddress(txtMailAdresim.Text); // Burada kimden geldiği olarak nitelendirilen sizin mail adresiniz yazılması gerekmektedir.

            _mail.To.Add(txtKime.Text); // hangi mail adresine gidecegi yazılmalıdır.Add metodu içerisine MailAddress almaktadır. MailAddress te tanımlayarak buraya giriş yapabilirsiniz.
            _mail.Subject = cmbKonu.Text; // Mail konusu
            _mail.Priority = MailPriority.High; // Ek bir özelliktir mail öncelik değeri belirtilmektedir.
            _mail.Body = txtMesaj.Text; // Mail içeriği buraya yazılmaktadır.
            SmtpClient _client = new SmtpClient(); // smtp sunucusuna bağlanmak için kullanacağımız nesnemizi oluşturuyoruz.
            _client.Credentials = new System.Net.NetworkCredential("aykutonall@hotmail.com", "165316"); // Mail adresi ve şifremizi gösteriyoruz.
            _client.Host = "smtp.live.com"; // hotmail smtp sunucu ismi 
            _client.Timeout = 50000; // timeout
            _client.Port = 587; // kullanılacak olan port burada hotmailın kullandığı porttur.

            _client.EnableSsl = true; // ssl 'i aktifleştiriyoruz.
            string userState = "Mail Gönderiliyor";
            _client.SendAsync(_mail, userState); // Gönderme olayı
            MessageBox.Show("Mail Gönderildi...", "Önal Kuaför", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            // Temizle
            cmbKonu.Text = "";
            txtKime.Text = "";
            txtMesaj.Text = "";
        }

        private void cmbKonu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKonu.Text == "Boş")
            {
                txtMesaj.Text = "";

            }
            if (cmbKonu.Text == "Doğum Günü")
            {
                txtMesaj.Text = "" + "Doğum Gününüz Kutlu Olsun..";

            }

            if (cmbKonu.Text == "Yeni Yıl")
            {
                txtMesaj.Text = "" + "Yeni Yılınız Kutlu Olsun..";

            }
            if (cmbKonu.Text == "Özel Kutlama")
            {

                txtMesaj.Text = "" + "Özel Kutlama Metnidir..";

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(1) + this.Text.Substring(0, 1);
        }

        private void grdMail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKime.DataBindings.Clear();
            txtKime.DataBindings.Add("Text", al.Tables[0], "Email");
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            mailGonder();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtKime.Text = "";
            txtMesaj.Text = "";
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
