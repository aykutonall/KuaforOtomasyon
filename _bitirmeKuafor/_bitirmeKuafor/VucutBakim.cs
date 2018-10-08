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
    public partial class VucutBakim : Form
    {
        public VucutBakim()
        {
            InitializeComponent();
        }

        string baglanti, sorgu;
        OleDbConnection yeni;
        OleDbCommand uygula;
        OleDbDataAdapter verial;
        DataSet al;
        public DataTable tablo = new DataTable();
        double x, y, z, a, b, c, d, e, f, g, h, i, j, k, toplam1, toplam2, sonuc;

        private void VucutBakim_Load(object sender, EventArgs e)
        {
            // Combobox Yükleniyor.
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "Select * From PERSONEL";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "PERSONEL");
            cmbPersonel.DataSource = al.Tables["PERSONEL"];
            cmbPersonel.DisplayMember = "AdiSoyadi";
            cmbPersonel.ValueMember = "id";

            //Veriler gösteriliyor.
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "Select AdiSoyadi From MUSTERI";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "MUSTERI");
            grdVucutBakim.DataSource = al.Tables["MUSTERI"];
            yeni.Close();
            grdVucutBakim.ReadOnly = true;
            mskTarih.Text = DateTime.Now.ToShortDateString();

            // Datagridview Renklendirme
            this.grdVucutBakim.RowsDefaultCellStyle.BackColor = Color.White;
            this.grdVucutBakim.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;

            // Kontroller Kapatılıyor
            mskTarih.Enabled = false;
            txtIs1.Clear();
            txtIs2.Clear();
            txtIs3.Clear();
            txtIs4.Clear();
            txtIs5.Clear();
            txtIs6.Clear();
            txtIs7.Clear();
            txtIs8.Clear();
            txtIs9.Clear();
            txtIs10.Clear();
            txtIs11.Clear();
            txtIs12.Clear();
            txtIs13.Clear();
            txtIs14.Clear();

            txtIs1.Enabled = false;
            txtIs2.Enabled = false;
            txtIs3.Enabled = false;
            txtIs4.Enabled = false;
            txtIs5.Enabled = false;
            txtIs6.Enabled = false;
            txtIs7.Enabled = false;
            txtIs8.Enabled = false;
            txtIs9.Enabled = false;
            txtIs10.Enabled = false;
            txtIs11.Enabled = false;
            txtIs12.Enabled = false;
            txtIs13.Enabled = false;
            txtIs14.Enabled = false;
        }

        private void grdVucutBakim_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAdiSoyadi.DataBindings.Clear();
            txtAdiSoyadi.DataBindings.Add("Text", al.Tables[0], "AdiSoyadi");
        }

        private void checkSagGogus_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSagGogus.Checked == true)
            {
                txtIs1.Text = "Sağ Göğüs";
                txtIs1Tutar.Text = "10";
            }
            if (checkSagGogus.Checked == false)
            {
                txtIs1.Clear();
                txtIs1Tutar.Text = "0";
            }
        }

        private void checkSolGogus_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSolGogus.Checked == true)
            {
                txtIs2.Text = "Sol Göğüs";
                txtIs2Tutar.Text = "20";
            }
            if (checkSolGogus.Checked == false)
            {
                txtIs2.Clear();
                txtIs2Tutar.Text = "0";
            }
        }

        private void checkSagKoltukAlti_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSagKoltukAlti.Checked == true)
            {
                txtIs3.Text = "Sağ Koltuk Altı";
                txtIs3Tutar.Text = "30";
            }
            if (checkSagKoltukAlti.Checked == false)
            {
                txtIs3.Clear();
                txtIs3Tutar.Text = "0";
            }
        }

        private void checkSolKoltukAlti_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSolKoltukAlti.Checked == true)
            {
                txtIs4.Text = "Sol Koltuk Altı";
                txtIs4Tutar.Text = "40";
            }
            if (checkSolKoltukAlti.Checked == false)
            {
                txtIs4.Clear();
                txtIs4Tutar.Text = "0";
            }
        }

        private void checkGobek_CheckedChanged(object sender, EventArgs e)
        {
            if (checkGobek.Checked == true)
            {
                txtIs5.Text = "Göbek";
                txtIs5Tutar.Text = "50";
            }
            if (checkGobek.Checked == false)
            {
                txtIs5.Clear();
                txtIs5Tutar.Text = "0";
            }
        }

        private void checkSagAdale_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSagAdale.Checked == true)
            {
                txtIs6.Text = "Sağ Adale";
                txtIs6Tutar.Text = "80";
            }
            if (checkSagAdale.Checked == false)
            {
                txtIs6.Clear();
                txtIs6Tutar.Text = "0";
            }
        }

        private void checkSolAdale_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSolAdale.Checked == true)
            {
                txtIs7.Text = "Sol Adale";
                txtIs7Tutar.Text = "90";
            }
            if (checkSolAdale.Checked == false)
            {
                txtIs7.Clear();
                txtIs7Tutar.Text = "0";
            }
        }

        private void checkSolSirt_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSolSirt.Checked == true)
            {
                txtIs8.Text = "Sol Arka Sırt";
                txtIs8Tutar.Text = "100";
            }
            if (checkSolSirt.Checked == false)
            {
                txtIs8.Clear();
                txtIs8Tutar.Text = "0";
            }
        }

        private void checkSagSirt_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSagSirt.Checked == true)
            {
                txtIs9.Text = "Sağ Arka Sırt";
                txtIs9Tutar.Text = "110";
            }
            if (checkSagSirt.Checked == false)
            {
                txtIs9.Clear();
                txtIs9Tutar.Text = "0";
            }
        }

        private void checkBel_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBel.Checked == true)
            {
                txtIs10.Text = "Bel";
                txtIs10Tutar.Text = "120";
            }
            if (checkBel.Checked == false)
            {
                txtIs10.Clear();
                txtIs10Tutar.Text = "0";
            }
        }

        private void checkSolKalca_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSolKalca.Checked == true)
            {
                txtIs11.Text = "Sol Kalça";
                txtIs11Tutar.Text = "130";
            }
            if (checkSolKalca.Checked == false)
            {
                txtIs11.Clear();
                txtIs11Tutar.Text = "0";
            }
        }

        private void checkSagKalca_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSagKalca.Checked == true)
            {
                txtIs12.Text = "Sağ Kalça";
                txtIs12Tutar.Text = "140";
            }
            if (checkSagKalca.Checked == false)
            {
                txtIs12.Clear();
                txtIs12Tutar.Text = "0";
            }
        }

        private void checkSolArkaAdale_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSolArkaAdale.Checked == true)
            {
                txtIs13.Text = "Sol Arka Adale";
                txtIs13Tutar.Text = "150";
            }
            if (checkSolArkaAdale.Checked == false)
            {
                txtIs13.Clear();
                txtIs13Tutar.Text = "0";
            }
        }

        private void checkSagArkaAdale_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSagArkaAdale.Checked == true)
            {
                txtIs14.Text = "Sağ Arka Adale";
                txtIs14Tutar.Text = "160";
            }
            if (checkSagArkaAdale.Checked == false)
            {
                txtIs14.Clear();
                txtIs14Tutar.Text = "0";
            }
        }

        public void toplam()
        {
            // Boş Geçersen Hesaplamıyor.

            x = Convert.ToDouble(txtIs1Tutar.Text);
            y = Convert.ToDouble(txtIs2Tutar.Text);
            z = Convert.ToDouble(txtIs3Tutar.Text);
            a = Convert.ToDouble(txtIs4Tutar.Text);
            b = Convert.ToDouble(txtIs5Tutar.Text);
            c = Convert.ToDouble(txtIs6Tutar.Text);
            d = Convert.ToDouble(txtIs7Tutar.Text);
            e = Convert.ToDouble(txtIs8Tutar.Text);
            f = Convert.ToDouble(txtIs9Tutar.Text);
            g = Convert.ToDouble(txtIs10Tutar.Text);
            h = Convert.ToDouble(txtIs11Tutar.Text);
            i = Convert.ToDouble(txtIs12Tutar.Text);
            j = Convert.ToDouble(txtIs13Tutar.Text);
            k = Convert.ToDouble(txtIs14Tutar.Text);

            toplam1 = a + b + c + d + e + f + g;
            toplam2 = h + i + j + k + x + y + z;
            sonuc = toplam1 + toplam2;

            lblToplamTutar.Text = sonuc.ToString();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "INSERT INTO VUCUT_BAKIM(Personel,Tarih,MusteriAdSoyad,YapilanIslem1,YapilanIslem2,YapilanIslem3,YapilanIslem4,YapilanIslem5,YapilanIslem6,YapilanIslem7,YapilanIslem8,YapilanIslem9,YapilanIslem10,YapilanIslem11,YapilanIslem12,YapilanIslem13,YapilanIslem14) VALUES ('" + cmbPersonel.Text + "','" + mskTarih.Text + "','" + txtAdiSoyadi.Text + "','" + txtIs1.Text + "','" + txtIs2.Text + "','" + txtIs3.Text + "','" + txtIs4.Text + "','" + txtIs5.Text + "','" + txtIs6.Text + "','" + txtIs7.Text + "','" + txtIs8.Text + "','" + txtIs9.Text + "','" + txtIs10.Text + "','" + txtIs11.Text + "','" + txtIs12.Text + "','" + txtIs13.Text + "','" + txtIs14.Text + "')";
            yeni = new OleDbConnection(baglanti);
            uygula = new OleDbCommand(sorgu, yeni);
            uygula.Connection.Open();
            uygula.ExecuteNonQuery();
            MessageBox.Show("Kayıt Başarılı....");

            txtAdiSoyadi.Clear();
            txtIs1.Clear();
            txtIs2.Clear();
            txtIs3.Clear();
            txtIs4.Clear();
            txtIs5.Clear();
            txtIs6.Clear();
            txtIs7.Clear();
            txtIs8.Clear();
            txtIs9.Clear();
            txtIs10.Clear();
            txtIs11.Clear();
            txtIs12.Clear();
            txtIs13.Clear();
            txtIs14.Clear();
            checkBel.Checked = false;
            checkGobek.Checked = false;
            checkSagAdale.Checked = false;
            checkSagArkaAdale.Checked = false;
            checkSagGogus.Checked = false;
            checkSagKalca.Checked = false;
            checkSagKoltukAlti.Checked = false;
            checkSagSirt.Checked = false;
            checkSolAdale.Checked = false;
            checkSolArkaAdale.Checked = false;
            checkSolGogus.Checked = false;
            checkSolKalca.Checked = false;
            checkSolKoltukAlti.Checked = false;
            checkSolSirt.Checked = false;
            txtIs1Tutar.Clear();
            txtIs2Tutar.Clear();
            txtIs3Tutar.Clear();
            txtIs4Tutar.Clear();
            txtIs5Tutar.Clear();
            txtIs6Tutar.Clear();
            txtIs7Tutar.Clear();
            txtIs8Tutar.Clear();
            txtIs9Tutar.Clear();
            txtIs10Tutar.Clear();
            txtIs11Tutar.Clear();
            txtIs12Tutar.Clear();
            txtIs13Tutar.Clear();
            txtIs14Tutar.Clear();
            lblToplamTutar.Text = "";
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            toplam();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtAdiSoyadi.Clear();
            txtIs1.Clear();
            txtIs2.Clear();
            txtIs3.Clear();
            txtIs4.Clear();
            txtIs5.Clear();
            txtIs6.Clear();
            txtIs7.Clear();
            txtIs8.Clear();
            txtIs9.Clear();
            txtIs10.Clear();
            txtIs11.Clear();
            txtIs12.Clear();
            txtIs13.Clear();
            txtIs14.Clear();
            checkBel.Checked = false;
            checkGobek.Checked = false;
            checkSagAdale.Checked = false;
            checkSagArkaAdale.Checked = false;
            checkSagGogus.Checked = false;
            checkSagKalca.Checked = false;
            checkSagKoltukAlti.Checked = false;
            checkSagSirt.Checked = false;
            checkSolAdale.Checked = false;
            checkSolArkaAdale.Checked = false;
            checkSolGogus.Checked = false;
            checkSolKalca.Checked = false;
            checkSolKoltukAlti.Checked = false;
            checkSolSirt.Checked = false;
            txtIs1Tutar.Text = "0";
            txtIs2Tutar.Text = "0";
            txtIs3Tutar.Text = "0";
            txtIs4Tutar.Text = "0";
            txtIs5Tutar.Text = "0";
            txtIs6Tutar.Text = "0";
            txtIs7Tutar.Text = "0";
            txtIs8Tutar.Text = "0";
            txtIs9Tutar.Text = "0";
            txtIs10Tutar.Text = "0";
            txtIs11Tutar.Text = "0";
            txtIs12Tutar.Text = "0";
            txtIs13Tutar.Text = "0";
            txtIs14Tutar.Text = "0";
            lblToplamTutar.Text = "";
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
