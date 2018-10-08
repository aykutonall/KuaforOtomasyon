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
    public partial class Personel : Form
    {
        public Personel()
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

        public void veriGoster()
        {
            //Veriler gösteriliyor..
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "SELECT * FROM PERSONEL";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "PERSONEL");
            grdPersonel.DataSource = al.Tables["PERSONEL"];
            yeni.Close();
            grdPersonel.ReadOnly = true;
        }

        private void Personel_Load(object sender, EventArgs e)
        {
            this.Text = "** Önal Kuaför ve Güzellik Salonu ** www.onalkuafor.com.tr";
            timer1.Interval = 100;
            timer1.Enabled = true;
            txtAra.Focus();
            grdPersonel.ColumnHeadersVisible = true;

            veriGoster();

            //Gridview Renklendirme
            this.grdPersonel.RowsDefaultCellStyle.BackColor = Color.White;
            this.grdPersonel.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;

            //Gridview Biçimlendirme
            grdPersonel.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            grdPersonel.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grdPersonel.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            grdPersonel.GridColor = Color.Black;
            grdPersonel.RowHeadersVisible = false;

            //Kontroller Kapatılıyor..
            txtPersonelNo.Enabled = false;
            mskTCKimlik.Enabled = false;
            txtAdiSoyadi.Enabled = false;
            mskDogumTarihi.Enabled = false;
            txtAdres.Enabled = false;
            mskCepTel.Enabled = false;
            mskEvTel.Enabled = false;
            txtEposta.Enabled = false;
            cmbKanGrubu.Enabled = false;
            txtMeslek.Enabled = false;
            txtMaas.Enabled = false;
            txtNot.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(1) + this.Text.Substring(0, 1);
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {

            //Kan Grubu combobox itemlerini veritabanındaki kan grubu tablosundan çekiyoruz.
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "SELECT * FROM KANGRUBU";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "KANGRUBU");
            cmbKanGrubu.DataSource = al.Tables["KANGRUBU"];
            cmbKanGrubu.DisplayMember = "KanGrubu";
            cmbKanGrubu.ValueMember = "id";

            //Kontroller Açılıyor..
            txtPersonelNo.Enabled = true;
            mskTCKimlik.Enabled = true;
            txtAdiSoyadi.Enabled = true;
            mskDogumTarihi.Enabled = true;
            txtAdres.Enabled = true;
            mskCepTel.Enabled = true;
            mskEvTel.Enabled = true;
            txtEposta.Enabled = true;
            cmbKanGrubu.Enabled = true;
            txtMeslek.Enabled = true;
            txtMaas.Enabled = true;
            txtNot.Enabled = true;
            txtPersonelNo.Focus();

            //Temizleniyor..
            /*
            txtPersonelNo.Clear();
            mskTCKimlik.Clear();
            txtAdiSoyadi.Clear();
            mskDogumTarihi.Clear();
            txtAdres.Clear();
            mskCepTel.Clear();
            mskEvTel.Clear();
            txtEposta.Clear();
            cmbKanGrubu.Text = "";
            txtMeslek.Clear();
            txtMaas.Clear();
            txtNot.Clear();
            */
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "INSERT INTO PERSONEL(PersonelNo,TC,AdiSoyadi,DogumTarihi,Adres,CepTelefon,EvTelefon,Email,KanGrubu,Meslek,Maas,Aciklama) VALUES ('" + txtPersonelNo.Text + "','" + mskTCKimlik.Text + "','" + txtAdiSoyadi.Text + "','" + mskDogumTarihi.Text + "','" + txtAdres.Text + "','" + mskCepTel.Text + "','" + mskEvTel.Text + "','" + txtEposta.Text + "','" + cmbKanGrubu.Text + "','" + txtMeslek.Text + "','" + txtMaas.Text + "','" + txtNot.Text + "')";
            yeni = new OleDbConnection(baglanti);
            uygula = new OleDbCommand(sorgu, yeni);
            uygula.Connection.Open();
            uygula.ExecuteNonQuery();
            MessageBox.Show("Kayıt Başarılı !");
            veriGoster();

            //Temizleniyor..
            txtPersonelNo.Clear();
            mskTCKimlik.Clear();
            txtAdiSoyadi.Clear();
            mskDogumTarihi.Clear();
            txtAdres.Clear();
            mskCepTel.Clear();
            mskEvTel.Clear();
            txtEposta.Clear();
            cmbKanGrubu.Text = "";
            txtMeslek.Clear();
            txtMaas.Clear();
            txtNot.Clear();
            txtPersonelNo.Focus();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            CurrencyManager sonraki =(CurrencyManager)this.BindingContext[al.Tables["PERSONEL"]];
            sonraki.EndCurrentEdit();
            cb = new OleDbCommandBuilder(verial); //OleDbDataAdapter'i silme güncelleme kayda hazır hale getirmek için.
            verial.Update(al, "PERSONEL");
            al.Clear();
            verial.Fill(al.Tables[0]);
            grdPersonel.DataSource = al.Tables[0];
            MessageBox.Show("Güncelleme Tamamlandı !", "İŞLEM TAMAM");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            CurrencyManager sonraki =(CurrencyManager)this.BindingContext[al.Tables["PERSONEL"]];
            DialogResult uyar;
            uyar = MessageBox.Show(this, txtPersonelNo.Text + "Personel No'lu Kaydı Silmek istiyor musunuz?", "SİLME UYARISI", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2);
            if (uyar == DialogResult.Yes)
            {
                cb = new OleDbCommandBuilder(verial); //OleDbDataAdapter'i silme güncelleme kayda hazır hale getirmek için.
                sonraki.RemoveAt(sonraki.Position);
                verial.Update(al, "PERSONEL");
                al.Clear();
                verial.Fill(al.Tables[0]);
                grdPersonel.DataSource = al.Tables[0];
                MessageBox.Show("Kayıt Silindi !", "İŞLEM TAMAM");
            }
            else
            {
                MessageBox.Show("Silme İşlemi İptal Edildi !");
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ciftTikla()
        {
            //Temizleniyor..
            txtPersonelNo.DataBindings.Clear();
            mskTCKimlik.DataBindings.Clear();
            txtAdiSoyadi.DataBindings.Clear();
            mskDogumTarihi.DataBindings.Clear();
            txtAdres.DataBindings.Clear();
            mskCepTel.DataBindings.Clear();
            mskEvTel.DataBindings.Clear();
            txtEposta.DataBindings.Clear();
            cmbKanGrubu.DataBindings.Clear();
            txtMeslek.DataBindings.Clear();
            txtMaas.DataBindings.Clear();
            txtNot.DataBindings.Clear();
            txtPersonelNo.Focus();


            txtPersonelNo.DataBindings.Add("Text", al.Tables[0], "PersonelNo");
            mskTCKimlik.DataBindings.Add("Text", al.Tables[0], "TC");
            txtAdiSoyadi.DataBindings.Add("Text", al.Tables[0], "AdiSoyadi");
            mskDogumTarihi.DataBindings.Add("Text", al.Tables[0], "DogumTarihi");
            txtAdres.DataBindings.Add("Text", al.Tables[0], "Adres");
            mskCepTel.DataBindings.Add("Text", al.Tables[0], "CepTelefon");
            mskEvTel.DataBindings.Add("Text", al.Tables[0], "EvTelefon");
            txtEposta.DataBindings.Add("Text", al.Tables[0], "Email");
            cmbKanGrubu.DataBindings.Add("Text", al.Tables[0], "KanGrubu");
            txtMeslek.DataBindings.Add("Text", al.Tables[0], "Meslek");
            txtMaas.DataBindings.Add("Text", al.Tables[0], "Maas");
            txtNot.DataBindings.Add("Text", al.Tables[0], "Aciklama");

            //Kontroller Açılıyor..
            txtPersonelNo.Enabled = true;
            mskTCKimlik.Enabled = true;
            txtAdiSoyadi.Enabled = true;
            mskDogumTarihi.Enabled = true;
            txtAdres.Enabled = true;
            mskCepTel.Enabled = true;
            mskEvTel.Enabled = true;
            txtEposta.Enabled = true;
            cmbKanGrubu.Enabled = true;
            txtMeslek.Enabled = true;
            txtMaas.Enabled = true;
            txtNot.Enabled = true;
        }

        private void grdPersonel_DoubleClick(object sender, EventArgs e)
        {
            ciftTikla();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (txtAra.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL" + " where(AdiSoyadi like '%" + txtAra.Text + "%')";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnA.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'A%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnB.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'B%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnC.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'C%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnÇ_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnÇ.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'Ç%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnD.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'D%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnE.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'E%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnF_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnF.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'F%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnG_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnG.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'G%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnH_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnH.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'H%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnI_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnI.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'I%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnİ_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnİ.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'İ%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnJ_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnJ.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'J%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnK_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnK.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'K%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnL_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnL.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'L%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnM_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnM.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'M%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnN_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnN.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'N%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnO_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnO.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'O%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnÖ_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnÖ.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'Ö%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnP_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnP.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'P%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnR.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'R%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnS_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnS.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'S%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnŞ_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnŞ.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'Ş%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnT_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnT.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'T%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnU_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnU.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'U%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnÜ_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnÜ.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'Ü%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnV_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnV.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'V%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnY_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnY.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'Y%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnZ_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "SELECT * FROM PERSONEL", yeni);
            if (btnZ.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM PERSONEL";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "PERSONEL");
                grdPersonel.DataSource = al.Tables["PERSONEL"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "SELECT * FROM PERSONEL where AdiSoyadi like 'Z%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grdPersonel.DataSource = tablo;
            yeni.Close();
        }

        private void btnTumu_Click(object sender, EventArgs e)
        {
            veriGoster();
        }


    }
}
