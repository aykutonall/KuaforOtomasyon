using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace _bitirmeKuafor
{
    public partial class MuzikCalar : Form
    {
        public MuzikCalar()
        {
            InitializeComponent();
        }

        string dizin;

        private void MuzikCalar_Load(object sender, EventArgs e)
        {
            this.Text = " ** Önal Kuaför ve Güzellik Salonu ** www.onalkuafor.com.tr ";
            timer1.Interval = 100;
            timer1.Enabled = true;
            listBox1.Text = "MP3 Listesi";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(1) + this.Text.Substring(0, 1);
        }

        private void btnGozat_Click(object sender, EventArgs e)
        {
            /*FolderBrowserDialog penceresi açıldığında Ok butonuna basılıp basılmadığı kontrol edilir.Eğer tıklanmışsa if
           bloğu içerisindeki kodlar çalışır.*/
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                //FolderBrowserDialog penceresinde seçilen dosyanın yolunu dizin değişkenine aktarır.
                dizin = folderBrowserDialog1.SelectedPath;
                //Seçilen dosyanın bilgisini alır.
                DirectoryInfo bilgiler = new DirectoryInfo(dizin);
                //seçilen dosyaların isimlerini bir dizi değişkenine aktardık
                FileInfo[] sdosyalar = bilgiler.GetFiles();

                //Dizideki içeriğin hepsini listboxımıza aktardık.
                listBox1.Items.AddRange(sdosyalar);
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //textbox1'e \+listbox'da seçilen müziğin adını aktardık.
            textBox1.Text = "\\" + listBox1.Text;
            //Seçilen dosyanın yolu ile textbox1'in textini birleştirdik ve textbox2'ye aktardık.
            textBox2.Text = dizin + textBox1.Text;
            //mediaplayerda çalması için ise bu kodu yadık.
            axWindowsMediaPlayer1.URL = textBox2.Text;
        }
    }
}
