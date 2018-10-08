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
    public partial class KayitListele : Form
    {
        public KayitListele()
        {
            InitializeComponent();
        }

        string baglanti, sorgu;
        OleDbConnection yeni;
        OleDbDataAdapter verial;
        DataSet al;
        OleDbCommandBuilder cb;
        public DataTable tablo = new DataTable();
        OleDbParameter parametre = new OleDbParameter();

        private void KayitListele_Load(object sender, EventArgs e)
        {
            this.Text = "** Önal Kuaför ve Güzellik Salonu ** www.onalkuafor.com.tr";
            timer1.Interval = 100;
            timer1.Enabled = true;
            txtAra.Focus();
            grddatalistele.ColumnHeadersVisible = true;

            cmbcinsiyet.Items.Add("");
            cmbcinsiyet.Items.Add("Bay");
            cmbcinsiyet.Items.Add("Bayan");

            cmbmusteritip.Items.Add("");
            cmbmusteritip.Items.Add("Genel");
            cmbmusteritip.Items.Add("Özel");

            //////Kan grubu combobox ın itemleri veritabanındaki kangrubu tablosundan cekiliyor.
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "Select * From KANGRUBU";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "KANGRUBU");
            cmbkangrubu.DataSource = al.Tables["KANGRUBU"];
            cmbkangrubu.DisplayMember = "KanGrubu";
            cmbkangrubu.ValueMember = "id";
            
            //Veriler gösteriliyor
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "select * from MUSTERI";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "MUSTERI");
            grddatalistele.DataSource = al.Tables["MUSTERI"];
            yeni.Close();
            grddatalistele.ReadOnly = true;

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

            //DataGridwiev Rengi
            this.grddatalistele.RowsDefaultCellStyle.BackColor = Color.White;
            this.grddatalistele.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;

            //DataGridwiev Biçim
            grddatalistele.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            grddatalistele.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grddatalistele.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            grddatalistele.GridColor = Color.Black;
            grddatalistele.RowHeadersVisible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(1) + this.Text.Substring(0, 1);
        }

        public void ciftTikla()
        {
            txtmusno.DataBindings.Clear();
            txtadsoyad.DataBindings.Clear();
            mskdogumtrh.DataBindings.Clear();
            mskevliliktrh.DataBindings.Clear();
            mskilkziytrh.DataBindings.Clear();
            cmbcinsiyet.DataBindings.Clear();
            cmbmusteritip.DataBindings.Clear();
            cmbkangrubu.DataBindings.Clear();
            txtoneren.DataBindings.Clear();
            txtemail.DataBindings.Clear();
            txtmeslek.DataBindings.Clear();
            mskcep.DataBindings.Clear();
            txtsac.DataBindings.Clear();
            txtnot.DataBindings.Clear();
            txtEvAdres.DataBindings.Clear();
            mskEvTel.DataBindings.Clear();
            txtEvSemt.DataBindings.Clear();
            txtIsAdres.DataBindings.Clear();
            mskIsTel.DataBindings.Clear();
            txtIsSemt.DataBindings.Clear();
            txtmusno.Focus();


            txtmusno.DataBindings.Add("Text", al.Tables[0], "MusteriNo");
            txtadsoyad.DataBindings.Add("Text", al.Tables[0], "AdiSoyadi");
            mskdogumtrh.DataBindings.Add("Text", al.Tables[0], "DogumTarihi");
            mskevliliktrh.DataBindings.Add("Text", al.Tables[0], "EvlilikTarihi");
            mskilkziytrh.DataBindings.Add("Text", al.Tables[0], "IlkZiyaretTarihi");
            cmbcinsiyet.DataBindings.Add("Text", al.Tables[0], "Cinsiyet");
            cmbmusteritip.DataBindings.Add("Text", al.Tables[0], "MusteriTipi");
            cmbkangrubu.DataBindings.Add("Text", al.Tables[0], "KanGrubu");
            txtoneren.DataBindings.Add("Text", al.Tables[0], "OnerenKisi");
            txtemail.DataBindings.Add("Text", al.Tables[0], "Email");
            txtmeslek.DataBindings.Add("Text", al.Tables[0], "Meslek");
            mskcep.DataBindings.Add("Text", al.Tables[0], "CepTel");
            txtsac.DataBindings.Add("Text", al.Tables[0], "SacRengi");
            txtnot.DataBindings.Add("Text", al.Tables[0], "Aciklama");
            txtEvAdres.DataBindings.Add("Text", al.Tables[0], "EvAdres");
            mskEvTel.DataBindings.Add("Text", al.Tables[0], "EvTelefon");
            txtEvSemt.DataBindings.Add("Text", al.Tables[0], "EvSemt");
            txtIsAdres.DataBindings.Add("Text", al.Tables[0], "IsAdres");
            mskIsTel.DataBindings.Add("Text", al.Tables[0], "IsTelefon");
            txtIsSemt.DataBindings.Add("Text", al.Tables[0], "IsSemt");

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

        private void grddatalistele_DoubleClick(object sender, EventArgs e)
        {
            ciftTikla();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            CurrencyManager sonraki = (CurrencyManager)this.BindingContext[al.Tables["MUSTERI"]];
            sonraki.EndCurrentEdit();
            cb = new OleDbCommandBuilder(verial);
            verial.Update(al, "MUSTERI");
            al.Clear();
            verial.Fill(al.Tables[0]);
            grddatalistele.DataSource = al.Tables[0];
            MessageBox.Show("Güncelleme Tamamlandı!");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            CurrencyManager sonraki = (CurrencyManager)this.BindingContext[al.Tables["MUSTERI"]];
            DialogResult uyar;
            uyar = MessageBox.Show(this, txtmusno.Text + " Müşteri No'lu kaydı silmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2);
            if (uyar == DialogResult.Yes)
            {
                cb = new OleDbCommandBuilder(verial);
                sonraki.RemoveAt(sonraki.Position);
                verial.Update(al, "MUSTERI");
                al.Clear();
                verial.Fill(al.Tables[0]);
                grddatalistele.DataSource = al.Tables[0];
                MessageBox.Show("Kayıt Silindi!");
            }
            else
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI",yeni);
            if (txtAra.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "Select * From MUSTERI" + " where(AdiSoyadi like '%" + txtAra.Text + "%')";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnA.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'A%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnB.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'B%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnC.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'C%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnÇ_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnÇ.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'Ç%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnD.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'D%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnE.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'E%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnF_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnF.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'F%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnG_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnG.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'G%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnH_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnH.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'H%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnI_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnI.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'I%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnii_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnii.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'İ%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnJ_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnJ.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'J%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnK_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnK.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'K%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnL_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnL.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'L%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnM_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnM.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'M%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnN_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnN.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'N%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnO_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnO.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'O%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnÖ_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnÖ.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'Ö%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnP_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnP.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'P%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnR.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'R%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnS_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnS.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'S%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnŞ_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
            if (btnŞ.Text == "")
            {
                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "select * from MUSTERI";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "MUSTERI");
                grddatalistele.DataSource = al.Tables["MUSTERI"];
            }
            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }
            verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'Ş%' ";
            tablo.Clear();
            verial.Fill(tablo);
            grddatalistele.DataSource = tablo;
            yeni.Close();
        }

        private void btnT_Click(object sender, EventArgs e)
        {
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
                if (btnT.Text == "")
                {
                    baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                    sorgu = "select * from MUSTERI";
                    yeni = new OleDbConnection(baglanti);
                    verial = new OleDbDataAdapter(sorgu, yeni);
                    al = new DataSet();
                    verial.Fill(al, "MUSTERI");
                    grddatalistele.DataSource = al.Tables["MUSTERI"];
                }
                if (Convert.ToBoolean(yeni.State) == false)
                {
                    yeni.Open();
                }
                verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'T%' ";
                tablo.Clear();
                verial.Fill(tablo);
                grddatalistele.DataSource = tablo;
                yeni.Close();
            }
        }

        private void btnU_Click(object sender, EventArgs e)
        {
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
                if (btnU.Text == "")
                {
                    baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                    sorgu = "select * from MUSTERI";
                    yeni = new OleDbConnection(baglanti);
                    verial = new OleDbDataAdapter(sorgu, yeni);
                    al = new DataSet();
                    verial.Fill(al, "MUSTERI");
                    grddatalistele.DataSource = al.Tables["MUSTERI"];
                }
                if (Convert.ToBoolean(yeni.State) == false)
                {
                    yeni.Open();
                }
                verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'U%' ";
                tablo.Clear();
                verial.Fill(tablo);
                grddatalistele.DataSource = tablo;
                yeni.Close();
            }
        }

        private void btnÜ_Click(object sender, EventArgs e)
        {
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
                if (btnÜ.Text == "")
                {
                    baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                    sorgu = "select * from MUSTERI";
                    yeni = new OleDbConnection(baglanti);
                    verial = new OleDbDataAdapter(sorgu, yeni);
                    al = new DataSet();
                    verial.Fill(al, "MUSTERI");
                    grddatalistele.DataSource = al.Tables["MUSTERI"];
                }
                if (Convert.ToBoolean(yeni.State) == false)
                {
                    yeni.Open();
                }
                verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'Ü%' ";
                tablo.Clear();
                verial.Fill(tablo);
                grddatalistele.DataSource = tablo;
                yeni.Close();
            }
        }

        private void btnV_Click(object sender, EventArgs e)
        {
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
                if (btnV.Text == "")
                {
                    baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                    sorgu = "select * from MUSTERI";
                    yeni = new OleDbConnection(baglanti);
                    verial = new OleDbDataAdapter(sorgu, yeni);
                    al = new DataSet();
                    verial.Fill(al, "MUSTERI");
                    grddatalistele.DataSource = al.Tables["MUSTERI"];
                }
                if (Convert.ToBoolean(yeni.State) == false)
                {
                    yeni.Open();
                }
                verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'V%' ";
                tablo.Clear();
                verial.Fill(tablo);
                grddatalistele.DataSource = tablo;
                yeni.Close();
            }
        }

        private void btnY_Click(object sender, EventArgs e)
        {
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
                if (btnY.Text == "")
                {
                    baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                    sorgu = "select * from MUSTERI";
                    yeni = new OleDbConnection(baglanti);
                    verial = new OleDbDataAdapter(sorgu, yeni);
                    al = new DataSet();
                    verial.Fill(al, "MUSTERI");
                    grddatalistele.DataSource = al.Tables["MUSTERI"];
                }
                if (Convert.ToBoolean(yeni.State) == false)
                {
                    yeni.Open();
                }
                verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'Y%' ";
                tablo.Clear();
                verial.Fill(tablo);
                grddatalistele.DataSource = tablo;
                yeni.Close();
            }
        }

        private void btnZ_Click(object sender, EventArgs e)
        {
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu = "select * from MUSTERI", yeni);
                if (btnZ.Text == "")
                {
                    baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                    sorgu = "select * from MUSTERI";
                    yeni = new OleDbConnection(baglanti);
                    verial = new OleDbDataAdapter(sorgu, yeni);
                    al = new DataSet();
                    verial.Fill(al, "MUSTERI");
                    grddatalistele.DataSource = al.Tables["MUSTERI"];
                }
                if (Convert.ToBoolean(yeni.State) == false)
                {
                    yeni.Open();
                }
                verial.SelectCommand.CommandText = sorgu = "select * from MUSTERI where AdiSoyadi like 'Z%' ";
                tablo.Clear();
                verial.Fill(tablo);
                grddatalistele.DataSource = tablo;
                yeni.Close();
            }
        }

        private void btnTumu_Click(object sender, EventArgs e)
        {
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "select * from MUSTERI";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "MUSTERI");
            grddatalistele.DataSource = al.Tables["MUSTERI"];
            yeni.Close();
            grddatalistele.ReadOnly = true;
        }
    }
}
