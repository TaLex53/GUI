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
    public partial class Configuración : Form
    {
        public Configuración()
        {
            InitializeComponent();
        }

        private void Configuración_Load(object sender, EventArgs e)
        {
            textBox1.Text = GUI_MODERNISTA.conexionDB.get_pulso().ToString();
           
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (isInt32(textBox1.Text))
            {
                GUI_MODERNISTA.conexionDB.set_pulso(textBox1.Text);
                System.Threading.Thread.Sleep(300);
                GUI.tpulso = Convert.ToInt32(textBox1.Text);
                this.Close();
            }
            else { MessageBox.Show("Ingres un dato númerico"); }
            
        }
        public bool isInt32(String num)
        {
            try
            {
                Int32.Parse(num);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
