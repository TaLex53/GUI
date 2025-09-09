using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;


namespace GUI_MODERNISTA
{
    public partial class Eventos : Form
    {
        public Eventos()
        {
            InitializeComponent();
        }

        private void Btncargar_Click(object sender, EventArgs e)
        {
            DateTime D1 = dateTimePicker1.Value;
            DateTime D2 = dateTimePicker2.Value;

            if ((D2 - D1).Days <= 7)
            {
                if (D1.ToString("yyyy-MM-dd") == D2.ToString("yyyy-MM-dd"))
                {
                    D2 = D2.AddDays(1);
                }
                dataGridView1.DataSource = conexionDB.get_logs(D1, D2).Tables[0];
            }
            else
            {
                MessageBox.Show("Rango no permitido");
            }
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            //Btn Excel
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            String ruta;

            ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GUILLOTINAS";
            string ruta_crear = ruta + "\\EVENTO " + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

            if (Directory.Exists(ruta) == false) { Directory.CreateDirectory(ruta); }

            // Cabeceras
            string tmp = Application.StartupPath + "\\Plantilla.xlsx";
            var wb = new XLWorkbook(tmp);
            var sheet = wb.Worksheet("Reporte");

            sheet.Cell(3, 2).Value = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            sheet.Cell(3, 4).Value = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            // Valores
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {

                    sheet.Cell(i + 6, j + 1).Value = dataGridView1.Rows[i].Cells[j].Value.ToString();

                }
            }


            wb.SaveAs(ruta_crear);
            wb.Dispose();



        }
    }
}
