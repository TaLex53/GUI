using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_MODERNISTA
{
    public partial class Diagnóstico : Form
    {
        public Diagnóstico()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker1.CancellationPending)
            {
                int RFval = GUI.RFvalue;
                int RFSSi = GUI.RSSI;

                int[] diag = { RFval, RFSSi };
                backgroundWorker1.ReportProgress(1, diag);
                System.Threading.Thread.Sleep(500);
            }
            

        }

        private void Diagnóstico_Load(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                int[] val = (int[])e.UserState;

                label1.Text = "RF Timeout: " + val[0];
                label2.Text = "RSCI Value: " + val[1];
            }
        }

        private void Diagnóstico_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }

        }

        private void Diagnóstico_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
