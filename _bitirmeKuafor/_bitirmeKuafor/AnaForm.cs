using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Calendar;
using System.Data.OleDb;
using System.IO;
using System.Xml.Serialization;
using System.Threading;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace _bitirmeKuafor
{
    public partial class AnaForm : Form
    {
        List<CalendarItem> _items = new List<CalendarItem>();
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\DATA\\kuafor_programi.accdb");

        public AnaForm()
        {
            InitializeComponent();
            
            //Monthview colors
            monthView1.MonthTitleColor = monthView1.MonthTitleColorInactive = CalendarColorTable.FromHex("#C2DAFC");
            monthView1.ArrowsColor = CalendarColorTable.FromHex("#77A1D3");
            monthView1.DaySelectedBackgroundColor = CalendarColorTable.FromHex("#F4CC52");
            monthView1.DaySelectedTextColor = monthView1.ForeColor;
        }

        private void PlaceItems()
        {
            foreach (CalendarItem item in _items)
            {
                if (calendar1.ViewIntersects(item))
                {
                    calendar1.Items.Add(item);
                }
            }
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
            ShowForm();
            
            timer1.Interval = 1000;
            timer1.Start();
            saatGoster();
            
            calendar1.TimeUnitsOffset = -16;
            
            lbltarih.Text = DateTime.Now.ToShortDateString();
            this.Text = "** Önal Kuaför ve Güzellik Salonu ** www.onalkuafor.com.tr";

            //NotifyIcon başlıyor.
            onalKuafor.ShowBalloonTip(2000, "Önal Kuaför Programı", "Hoşgeldiniz...", ToolTipIcon.Info);
            
            if (DateTime.Now.DayOfWeek.ToString() == "Friday")
            {
                timer1.Start();
            }

            //Ajanda

            if (ItemsFile.Exists)
            {
                List<ItemInfo> lst = new List<ItemInfo>();
                XmlSerializer xml = new XmlSerializer(lst.GetType());
                using (Stream s = ItemsFile.OpenRead())
                {
                    lst = xml.Deserialize(s) as List<ItemInfo>;
                }
                foreach (ItemInfo item in lst)
                {
                    CalendarItem cal = new CalendarItem(calendar1, item.StartTime, item.EndTime, item.Text);
                    if (!(item.R == 0 && item.G == 0 && item.B == 0))
                    {
                        cal.ApplyColor(Color.FromArgb(item.A, item.R, item.G, item.B));
                    }
                    _items.Add(cal);
                }
                PlaceItems();
            }
        }

        public FileInfo ItemsFile
        {
            get
            {
                return new FileInfo(Path.Combine(Application.StartupPath, "items.xml"));
            }
        }

        #region Saat Göster
        private void tmrSaat_Tick(object sender, EventArgs e)
        {
            saatGoster();
        }
        private void saatGoster()
        {
            lblsaat.Text = DateTime.Now.ToLongTimeString();
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            saatGoster();
        }

        public void veri_oku()
        {
            OleDbCommand veri = new OleDbCommand("select * from GUNLUK order by Tarih", baglan);
            OleDbDataReader oku = null;
            baglan.Open();
            oku = veri.ExecuteReader();
            listtarih.Items.Clear();
            while (oku.Read())
            {
                ListViewItem kayit = new ListViewItem(oku["id"].ToString());
                kayit.SubItems.Add(oku["Tarih"].ToString());
                listtarih.Items.Add(kayit);
            }
            oku.Close();
            baglan.Close();
        }

        private void ShowForm()
        {
            //Hatırlatma Kontrol Kodları
            OleDbCommand veri = new OleDbCommand("select HatirlatmaNotu from HATIRLATMA where Tarih='" + dateTimePicker1.Text + "'", baglan);
            OleDbDataReader oku = null;
            baglan.Open();
            oku = veri.ExecuteReader();
            while (oku.Read())
            {
                MessageBox.Show("Hatırlattım : " + oku.GetString(0));
            }
            oku.Close();
            baglan.Close();
            //------------------------------------------
            veri_oku();
            dateTimePicker1.Text = DateTime.Now.ToShortDateString();
        }

        private void yeniKayıtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YeniKayit yenikayit = new YeniKayit();
            yenikayit.Show();
        }

        private void çıkışToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Önal Kuaför Programını Tercih Ettiğiniz İçin Teşekkür Ederiz..", "Çıkış", MessageBoxButtons.OK);
            Application.Exit();
        }

        private void çıkışToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Önal Kuaför Programını Tercih Ettiğiniz İçin Teşekkür Ederiz..", "Çıkış", MessageBoxButtons.OK);
            Application.Exit();
        }

        private void hakkındaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Hakkinda hakkinda = new Hakkinda();
            hakkinda.Show();
        }

        private void firmaBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaticiFirmalar saticifirma = new SaticiFirmalar();
            saticifirma.Show();
        }

        private void malzemeBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Malzemeler malzemelistesi = new Malzemeler();
            malzemelistesi.Show();
        }

        private void personelBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Personel personel = new Personel();
            personel.Show();
        }

        private void KayitListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KayitListele kayitlistele = new KayitListele();
            kayitlistele.Show();
        }

        private void mailGönderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mail mailgonder = new Mail();
            mailgonder.Show();
        }

        private void muzikcalarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MuzikCalar muzikcalar = new MuzikCalar();
            muzikcalar.Show();
        }

        private void günlükİşListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunlukIsler gunlukisler = new GunlukIsler();
            gunlukisler.Show();
        }

        private void vücutBakımıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VucutBakim vucutbakimi = new VucutBakim();
            vucutbakimi.Show();
        }

        private void renkDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            panel1.BackColor = colorDialog1.Color;
        }

        private void btnYeniKayit_Click(object sender, EventArgs e)
        {
            YeniKayit yenikayit = new YeniKayit();
            yenikayit.Show();
        }

        private void btnKayitListele_Click(object sender, EventArgs e)
        {
            KayitListele kayitlistele = new KayitListele();
            kayitlistele.Show();
        }

        private void btnGunlukIsler_Click(object sender, EventArgs e)
        {
            GunlukIsler gunlukisler = new GunlukIsler();
            gunlukisler.Show();
        }

        private void btnHatirlatma_Click(object sender, EventArgs e)
        {
            Hatirlatma hatirlatma = new Hatirlatma();
            hatirlatma.Show();
        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            Mail mailgonder = new Mail();
            mailgonder.Show();
        }

        private void btnVucutBakim_Click(object sender, EventArgs e)
        {
            VucutBakim vucutbakimi = new VucutBakim();
            vucutbakimi.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Önal Kuaför Programını Tercih Ettiğiniz İçin Teşekkür Ederiz..", "Çıkış", MessageBoxButtons.OK);
            Application.Exit();
        }

        private void AnaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Önal Kuaför Programını Tercih Ettiğiniz İçin Teşekkür Ederiz..", "Çıkış", MessageBoxButtons.OK);
            Application.Exit();
        }

        private void monthView1_SelectionChanged(object sender, EventArgs e)
        {
            calendar1.SetViewRange(monthView1.SelectionStart, monthView1.SelectionEnd);
        }

        private void calendar1_DayHeaderClick(object sender, CalendarDayEventArgs e)
        {
            calendar1.SetViewRange(e.CalendarDay.Date, e.CalendarDay.Date);
        }

        private void calendar1_ItemCreated(object sender, CalendarItemCancelEventArgs e)
        {
            _items.Add(e.Item);
        }

        private void calendar1_ItemDeleted(object sender, CalendarItemEventArgs e)
        {
            _items.Remove(e.Item);
        }

        private void calendar1_ItemMouseHover(object sender, CalendarItemEventArgs e)
        {
            Text = e.Item.Text;
        }

        private void calendar1_LoadItems(object sender, CalendarLoadEventArgs e)
        {
            PlaceItems();
        }

        private void AnaForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState) 
            { 
                Hide();
                //onalKuafor.ShowBalloonTip(2000, "Önal Kuaför Programı", "Program simge haline dönüştü. Açmak için simgeye çift tıklayın..", ToolTipIcon.Info);
                onalKuafor.BalloonTipText = "Program simge haline dönüştü. Programa dönmek için simgeye çift tıklayın..";
                onalKuafor.ShowBalloonTip(5); //Mesajı 5sn. goruntuler 
            }
        }

        private void onalKuafor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Maximized;
        }

    }
}
