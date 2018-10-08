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
    public partial class GunlukYapilanIs : Form
    {
        public GunlukYapilanIs()
        {
            InitializeComponent();
        }

        string baglanti, sorgu;
        OleDbConnection yeni;
        OleDbCommand uygula;
        OleDbDataAdapter verial;
        DataSet al;
        public DataTable tablo = new DataTable();

        private void GunlukYapilanIs_Load(object sender, EventArgs e)
        {
            // Datagridview Renklendirme
            this.grdYapilanIs.RowsDefaultCellStyle.BackColor = Color.White;
            this.grdYapilanIs.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;
            
            //DataGridwiev Biçimlendirme..
            grdYapilanIs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            grdYapilanIs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grdYapilanIs.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            grdYapilanIs.GridColor = Color.Black;
            grdYapilanIs.RowHeadersVisible = false;

            //Veriler Gösteriliyor...
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "SELECT * FROM YAPILAN_ISLER";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "YAPILAN_ISLER");
            grdYapilanIs.DataSource = al.Tables["YAPILAN_ISLER"];
            yeni.Close();
            grdYapilanIs.ReadOnly = true;
            dtAra.Text = DateTime.Now.ToShortDateString();

            //Columns Toplamı
            if (grdYapilanIs.Rows.Count > 0)
                lblToplam.Text = Total().ToString("c");
                Total();
        }

        private double Total()
        {
            double tot = 0;
            int i = 0;
            for (i = 0; i < grdYapilanIs.Rows.Count; i++)
            {
                tot = tot + Convert.ToDouble(grdYapilanIs.Rows[i].Cells["Tutar"].Value);
            }
            return tot;
        }

        private void dtAra_ValueChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("SELECT AdiSoyadi,CepTel,Email,Meslek,DogumTarihi FROM MUSTERİ", yeni);

            if (dtAra.Text == "")
            {

                baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
                sorgu = "SELECT * FROM YAPILAN_ISLER";
                yeni = new OleDbConnection(baglanti);
                verial = new OleDbDataAdapter(sorgu, yeni);
                al = new DataSet();
                verial.Fill(al, "YAPILAN_ISLER");
                grdYapilanIs.DataSource = al.Tables["YAPILAN_ISLER"];

            }

            if (Convert.ToBoolean(yeni.State) == false)
            {
                yeni.Open();
            }

            verial.SelectCommand.CommandText = "SELECT * FROM YAPILAN_ISLER" + " WHERE(Tarih like '%" + dtAra.Text + "%' )";
            tablo.Clear();
            verial.Fill(tablo);
            grdYapilanIs.DataSource = tablo;
            yeni.Close();

            // Coloumns Toplamı 
            if (grdYapilanIs.Rows.Count > 0)
                lblToplam.Text = Total().ToString("c");
        }

        private void btnTumu_Click(object sender, EventArgs e)
        {
            //Veriler Gösteriliyor...
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb";
            sorgu = "SELECT * FROM YAPILAN_ISLER";
            yeni = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, yeni);
            al = new DataSet();
            verial.Fill(al, "YAPILAN_ISLER");
            grdYapilanIs.DataSource = al.Tables["YAPILAN_ISLER"];
            yeni.Close();
            grdYapilanIs.ReadOnly = true;
            dtAra.Text = DateTime.Now.ToShortDateString();

            //Columns Toplamı
            if (grdYapilanIs.Rows.Count > 0)
                lblToplam.Text = Total().ToString("c");
            Total();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
