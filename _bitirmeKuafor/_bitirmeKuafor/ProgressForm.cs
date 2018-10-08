using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _bitirmeKuafor
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        private void ProgressForm_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Eğer Progress Bar nesnesinin valuesi 100 değilse aşağıdaki if bloğundaki kod çalışacak.
            if (progressBar1.Value != 100)
            {
                //Progress Bar valuesi 10 artacak.
                progressBar1.Value = progressBar1.Value += 4;
            }
            //ProgressBar nesnesinin valuesi 100 olursa aşağıdaki kodlar çalışacak
            else
            {
                //timer1'in enabled özelliğini false yaparak timeri durduruyoruz.
                timer1.Enabled = false;
                //Anaforma gidiyor.
                this.Hide();
                AnaForm frm2 = new AnaForm();
                frm2.Show();
            }
        }
    }
}
