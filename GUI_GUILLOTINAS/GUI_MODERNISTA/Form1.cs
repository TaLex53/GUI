using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using EasyModbus;
using System.Threading;

namespace GUI_MODERNISTA
{
    public partial class Form1 : Form
    {
        List<modelo_register> Lregistro;
        ModbusClient modbusClientTCP = new ModbusClient();
        int j = 1, k = 1;
        int indice;
        int indice2;
        int action;
        int registro = 3001;
        int[] vect;
        int[] vect2 = new int[] { 6, 7, 8, 9, 10 };
        int dir_bit = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //   Cargar default
            Btn_J1.PerformClick();
            BtnV4_01.PerformClick();
            BtnV3_02.PerformClick();
            Lregistro=  GUI_MODERNISTA.Registros.fill_regsiter();


            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }



        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = true;
        }

        private void btnrptventa_Click(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;
            //Abrir menu de reportes
            Eventos ev = new Eventos();
            ev.Show();

        }

        private void btnrptcompra_Click(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;
        }

        private void btnrptpagos_Click(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;
        }

        private bool conectar()
        {

            try
            { 
                modbusClientTCP.IPAddress = "192.168.0.254";
               // modbusClientTCP.IPAddress = "127.0.0.1";
                modbusClientTCP.Parity = System.IO.Ports.Parity.None;
                modbusClientTCP.StopBits = System.IO.Ports.StopBits.One;
                modbusClientTCP.Baudrate = 9600;
                //modbusClientTCP.UnitIdentifier = 2;
                modbusClientTCP.ConnectionTimeout = 2000;
                modbusClientTCP.Connect();

                if (modbusClientTCP.Connected)
                {
                    Console.WriteLine("Connectado");
                }
                else
                {
                    Console.WriteLine("NO Connectado");
                }

                return modbusClientTCP.Connected;

            }
            catch (Exception r)
            {
                Console.WriteLine("No conectado "+r.ToString());
                return false;
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }


        public static int RFvalue;
        public static int RSSI;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker1.CancellationPending)
            {
              bool cliente_conectado = conectar();
                Thread.Sleep(1000);
                while (cliente_conectado)
                {
                    try
                    {   //LEER DATOS DE ESCRITURA HACIA EL PLC
                        int[] datoRead = modbusClientTCP.ReadInputRegisters(3001, 15);
                        //diagnostico
                        RFvalue = datoRead[11];
                        RSSI= datoRead[12];
                        Console.WriteLine("RADIO VAL_11 : " + datoRead[11]);
                        Console.WriteLine("RADIO VAL_12 : " + datoRead[12]);

                        backgroundWorker1.ReportProgress(1, datoRead);
                        

                        Thread.Sleep(500);
                    }
                    catch
                    {
                        modbusClientTCP.Disconnect();
                        Thread.Sleep(250);
                        break;
                    }

                }

                Thread.Sleep(1000);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                //PARA LINEA 4               
                int[] datoRead = (int[])(e.UserState);
                proceso(datoRead, indice, "Linea4");
                proceso(datoRead, indice2, "Linea3");


            }
        }
        private void proceso(int[] datoRead, int indice, string linea)
        {
            int inicio_lect = 5;
            int bit_open = (((j + j + vect2[indice - 1]) + (j * 3)) - 11) * 3;
            Console.WriteLine("BIT A LEER " + bit_open);

            #region bit_open

            if (bit_open >= 0 && bit_open <= 15)
            {
                inicio_lect = inicio_lect;
                bit_open = bit_open;
                Console.WriteLine("1");
            }
            if (bit_open >= 16 && bit_open <= 31)
            {
                inicio_lect = 6;
                bit_open = bit_open - 16;
                Console.WriteLine("2");
            }
            if (bit_open >= 32 && bit_open <= 47)
            {
                inicio_lect = 7;
                bit_open = bit_open - 32;
                Console.WriteLine("3");
            }
            if (bit_open >= 48 && bit_open <= 63)
            {
                inicio_lect = 8;
                bit_open = bit_open - 48;
                Console.WriteLine("4");
            }
            if (bit_open >= 64 && bit_open <= 79)
            {
                inicio_lect = 9;
                bit_open = bit_open - 64;
                Console.WriteLine("5");
            }
            #endregion bit_open
            Console.WriteLine("OBTENER OPEN");
            bool open = get_value(datoRead, inicio_lect, bit_open);

            #region bit_close
            int bit_close = bit_open + 1;

            if (bit_close >= 0 && bit_close <= 15)
            {
                inicio_lect = inicio_lect;
                bit_close = bit_close;
            }
            if (bit_close >= 16 && bit_close <= 30)
            {
                inicio_lect = inicio_lect+1;
                bit_close = bit_close - 16;
            }
            if (bit_close >= 31 && bit_close <= 45)
            {
                inicio_lect = 7;
                bit_close = bit_close - 31;
            }
            if (bit_close >= 46 && bit_close <= 60)
            {
                inicio_lect = 8;
                bit_close = bit_close - 47;
            }
            if (bit_close >= 61 && bit_close <= 75)
            {
                inicio_lect = 9;
                bit_close = bit_close - 63;
            }
            #endregion bit_close
            Console.WriteLine("OBTENER CLOSE");
            bool close = get_value(datoRead, inicio_lect, bit_close);

            #region bit_remoto
            int bit_remoto = bit_close + 1;
            if (bit_remoto >= 0 && bit_remoto <= 15)
            {
                inicio_lect = inicio_lect;
                bit_remoto = bit_remoto;
            }
            if (bit_remoto >= 16 && bit_remoto <= 30)
            {
                inicio_lect = inicio_lect + 1;
                bit_remoto = bit_remoto - 16;
            }
            if (bit_remoto >= 31 && bit_remoto <= 45)
            {
                inicio_lect = inicio_lect + 1;
                bit_remoto = bit_remoto - 31;
            }
            if (bit_remoto >= 46 && bit_remoto <= 60)
            {
                inicio_lect = inicio_lect + 1;
                bit_remoto = bit_remoto - 47;
            }
            if (bit_remoto >= 61 && bit_remoto <= 75)
            {
                inicio_lect = inicio_lect + 1;
                bit_remoto = bit_remoto - 63;
            }
            #endregion
            Console.WriteLine("OBTENER REMOTO");
            bool remoto = get_value(datoRead, inicio_lect, bit_remoto);

            Console.WriteLine("V abierta: " + bit_open);
            Console.WriteLine("V cerrada: " + bit_close);
            Console.WriteLine("V Remoto: " + bit_remoto);
            Console.WriteLine("Estados: " + Convert.ToString(datoRead[inicio_lect], 2));
            Console.WriteLine("Leyendo : " + inicio_lect);
            //ESTADO ON/OFF  LINEA4
            if (linea == "Linea4")
            {
                if (close == false && open == false)
                {
                    panel16.BackgroundImage = Properties.Resources.ind_guillotina_off;
                }
                else if (close == false && open == true)
                {
                    panel16.BackgroundImage = Properties.Resources.ind_guillotina_abierta;

                }
                else if (close == true && open == false)
                {
                    panel16.BackgroundImage = Properties.Resources.ind_guillotina_cerrada;

                }

                //Estado Remoto

                if (remoto)
                {
                    BtnRemoto.BackgroundImage = Properties.Resources.btn_auto;
                    BtnManual.BackgroundImage = Properties.Resources.btn_off;
                }
                else
                {
                    BtnRemoto.BackgroundImage = Properties.Resources.btn_off;
                    BtnManual.BackgroundImage = Properties.Resources.btn_manual;
                }
                Console.WriteLine("Cerrada: " + close);
                Console.WriteLine("Abierta: " + open);
                Console.WriteLine("Remoto: " + remoto);

            }

            //ESTADO ON/OFF  LINEA3
            if (linea == "Linea3")
            {
                if (close == false && open == false)
                {
                    panel13.BackgroundImage = Properties.Resources.ind_guillotina_off;
                }
                else if (close == false && open == true)
                {
                    panel13.BackgroundImage = Properties.Resources.ind_guillotina_abierta;

                }
                else if (close == true && open == false)
                {
                    panel13.BackgroundImage = Properties.Resources.ind_guillotina_cerrada;

                }

                //Estado Remoto

                if (remoto)
                {
                    BtnRemoto2.BackgroundImage = Properties.Resources.btn_auto;
                    BtnManual2.BackgroundImage = Properties.Resources.btn_off;
                }
                else
                {
                    BtnRemoto2.BackgroundImage = Properties.Resources.btn_off;
                    BtnManual2.BackgroundImage = Properties.Resources.btn_manual;
                }
                Console.WriteLine("Cerrada: " + close);
                Console.WriteLine("Abierta: " + open);
                Console.WriteLine("Remoto: " + remoto);

            }

        }
        private bool get_value(int[] datoRead, int inicio_lect, int bit_read)
        {

            //Leer_reg: 7Num bit : 14
            //  Leer_reg: 7Num bit : 46
            string variable = Convert.ToString(datoRead[inicio_lect], 2);
            int i = variable.Length;
            int dif = 16 - i;

            Console.WriteLine("Leer_reg: " + inicio_lect + "Num bit : " + bit_read);
            for (i = 0; i < dif; i++)
            {
                variable = "0" + variable;
            }
            string[] valores1 = variable.ToCharArray().Select(c => c.ToString()).ToArray();

            string[] valores = valores1.Reverse().ToArray();

            bool value = Convert.ToBoolean(int.Parse(valores[bit_read]));

            return value;

        }
        public static byte[] ConvertInt32ToByteArray(Int32 I32)
        {
            return BitConverter.GetBytes(I32);
        }
        private void Btn_JaulaSelected_Click(object sender, EventArgs e)
        {

            Btn_J1.BackColor = Color.FromArgb(26, 32, 40);
            Btn_J2.BackColor = Color.FromArgb(26, 32, 40);
            Btn_J3.BackColor = Color.FromArgb(26, 32, 40);
            Btn_J4.BackColor = Color.FromArgb(26, 32, 40);
            Btn_J5.BackColor = Color.FromArgb(26, 32, 40);
            //26; 32; 40
            Button Jselected = (Button)sender;
            Jselected.BackColor = Color.Orange;
            string n = Jselected.Name;
            j = int.Parse(n.Substring(5, 1));
            if (n == "Btn_J1")
            {
                // registro = 3001;
                panel14.BackgroundImage = Properties.Resources.zoom_out_jaula_1;
                panel8.BackgroundImage = Properties.Resources.jaula01_04;
                j = 1;
                vect = new int[] { 6, 7, 8, 9, 10 };

            }
            else if (n == "Btn_J2")
            {
                panel14.BackgroundImage = Properties.Resources.zoom_out_jaula05_08;
                panel8.BackgroundImage = Properties.Resources.jaula05_08;
                j = 2;

                vect = new int[] { 15, 16, 17, 18, 19 };

            }
            else if (n == "Btn_J3")
            {
                panel14.BackgroundImage = Properties.Resources.zoom_out_jaula09_12;
                panel8.BackgroundImage = Properties.Resources.jaula09_12;
                j = 3;
                vect = new int[] { 24, 25, 26, 27, 28 };

            }
            else if (n == "Btn_J4")
            {
                panel14.BackgroundImage = Properties.Resources.zoom_out_jaula13_16;
                panel8.BackgroundImage = Properties.Resources.jaula13_16;
                j = 4;
                vect = new int[] { 33, 34, 35, 36, 37 };

            }
            else if (n == "Btn_J5")
            {
                panel14.BackgroundImage = Properties.Resources.zoom_out_jaula19_20;
                panel8.BackgroundImage = Properties.Resources.jaula19_20;
                j = 5;
                vect = new int[] { 42, 43, 44, 45, 46 };

            }



        }
        int index_all;
        private void Btn_VX_Click(object sender, EventArgs e)
        {
            Button Jselected = (Button)sender;

            string n = Jselected.Name;
            index_all = int.Parse(n.Substring(7, 1));



            if (index_all == 2 || index_all == 4)
            {
                indice2 = index_all;
            }
            else
            {
                indice = index_all;

            }

            if (indice2 == 2)
            {
                BtnV3_04.BackColor = Color.FromArgb(26, 32, 40);
            }
            else if (indice2 == 4)
            {
                BtnV3_02.BackColor = Color.FromArgb(26, 32, 40);
            }

            if (indice == 1)
            {

                BtnV4_03.BackColor = Color.FromArgb(26, 32, 40);
                BtnV4_05.BackColor = Color.FromArgb(26, 32, 40);
            }
            else if (indice == 3)
            {
                BtnV4_01.BackColor = Color.FromArgb(26, 32, 40);

                BtnV4_05.BackColor = Color.FromArgb(26, 32, 40);

            }
            else if (indice == 5)
            {
                BtnV4_01.BackColor = Color.FromArgb(26, 32, 40);
                BtnV4_03.BackColor = Color.FromArgb(26, 32, 40);
                //  BtnV4_05.BackColor = Color.FromArgb(26, 32, 40);
            }

            Jselected.BackColor = Color.FromArgb(0, 80, 200);

        }

        private void BtnOn_Click(object sender, EventArgs e)
        {
            Button Bon = (Button)sender;
            string linea = Bon.Name;

            action = 11;

            int direccion = 0;
            int dir_bit = j + vect[indice - 1] + indice + action - 19;
            if (dir_bit >= 0 && dir_bit <= 15)
            {
                registro = 3001;
                direccion = dir_bit;
            }
            if (dir_bit >= 16 && dir_bit <= 30)
            {
                registro = 3002;
                direccion = dir_bit - 16;
            }
            if (dir_bit >= 31 && dir_bit <= 45)
            {
                registro = 3003;
                direccion = dir_bit - 32;
            }
            if (dir_bit >= 46 && dir_bit <= 60)
            {
                registro = 3004;
                direccion = dir_bit - 47;
            }

            Console.WriteLine("Escribe reg: " + registro + " Bit: " + direccion);
            string v ;
            if (direccion < 10)
            {
                v = "X" + direccion;
            }
            else
            {
                v = "X"+direccion.ToString();
            }

            string valve = (from modelo_register t in Lregistro where t.id == registro && t.xbit == v select t.vname).Last();
            string temps = valve.Split('_')[0];
            string temps1 = temps.Substring(1, temps.Length-1);
            int temp = Convert.ToInt32(temps1);
            GUI_MODERNISTA.conexionDB.set_logs(valve, temp);

            Thread thread = new Thread(() => PulsoTiempoSet(direccion.ToString(), 3000, registro));
            thread.Start();
        }
        private void BtnOff_Click(object sender, EventArgs e)
        {
            action = 12;
            int direccion = 0;
            dir_bit = j + vect[indice - 1] + indice + action - 19;
            if (dir_bit >= 0 && dir_bit <= 15)
            {
                registro = 3001;
                direccion = dir_bit;
            }
            if (dir_bit >= 16 && dir_bit <= 31)
            {
                registro = 3002;
                direccion = dir_bit - 16;
            }

            if (dir_bit >= 32 && dir_bit <= 45)
            {
                registro = 3003;
                direccion = dir_bit - 32;
            }
            if (dir_bit >= 46 && dir_bit <= 60)
            {
                registro = 3004;
                direccion = dir_bit - 47;
            }

            Console.WriteLine("Escribe reg: " + registro + " Bit: " + direccion);

            string v;
            if (direccion < 10)
            {
                v = "X" + direccion;
            }
            else
            {
                v = "X" + direccion.ToString();
            }

            string valve = (from modelo_register t in Lregistro where t.id == registro && t.xbit == v select t.vname).Last();
            string temps = valve.Split('_')[0];
            string temps1 = temps.Substring(1, temps.Length - 1);
            int temp = Convert.ToInt32(temps1);
            GUI_MODERNISTA.conexionDB.set_logs(valve, temp);

            Thread thread = new Thread(() => PulsoTiempoSet(direccion.ToString(), 3000, registro));
            thread.Start();
        }
        private void BtnOn1_Click(object sender, EventArgs e)
        {
            Button Bon = (Button)sender;
            string linea = Bon.Name;

            action = 11;

            int direccion = 0;
            int dir_bit = j + vect[indice2 - 1] + indice2 + action - 19;
            if (dir_bit >= 0 && dir_bit <= 15)
            {
                registro = 3001;
                direccion = dir_bit;
            }
            if (dir_bit >= 16 && dir_bit <= 30)
            {
                registro = 3002;
                direccion = dir_bit - 16;
            }
            //if (dir_bit >= 31 && dir_bit <= 45)
            //{
            //    registro = 3003;
            //    direccion = dir_bit - 31;
            //}
            if (dir_bit >= 32 && dir_bit <= 45)
            {
                registro = 3003;
                direccion = dir_bit - 32;
            }


            if (dir_bit >= 46 && dir_bit <= 60)
            {
                registro = 3004;
                direccion = dir_bit - 47;
            }

            Console.WriteLine("Escribe reg: " + registro + " Bit: " + direccion);
            string v;
            if (direccion < 10)
            {
                v = "X" + direccion;
            }
            else
            {
                v = "X" + direccion.ToString();
            }

            string valve = (from modelo_register t in Lregistro where t.id == registro && t.xbit == v select t.vname).Last();
            string temps = valve.Split('_')[0];
            string temps1 = temps.Substring(1, temps.Length - 1);
            int temp = Convert.ToInt32(temps1);
            GUI_MODERNISTA.conexionDB.set_logs(valve, temp);

            Thread thread = new Thread(() => PulsoTiempoSet(direccion.ToString(), 10000, registro));
            thread.Start();
        }

        private void BtnOff1_Click(object sender, EventArgs e)
        {
            action = 12;
            int direccion = 0;
            dir_bit = j + vect[indice2 - 1] + indice2 + action - 19;
            if (dir_bit >= 0 && dir_bit <= 15)
            {
                registro = 3001;
                direccion = dir_bit;
            }
            if (dir_bit >= 16 && dir_bit <= 30)
            {
                registro = 3002;
                direccion = dir_bit - 16;
            }

            //if (dir_bit >= 31 && dir_bit <= 45)
            //{
            //    registro = 3003;
            //    direccion = dir_bit - 31;
            //}

            if (dir_bit >= 32 && dir_bit <= 45)
            {
                registro = 3003;
                direccion = dir_bit - 32;
            }
            if (dir_bit >= 46 && dir_bit <= 60)
            {
                registro = 3004;
                direccion = dir_bit - 47;
            }

            Console.WriteLine("Escribe reg: " + registro + " Bit: " + direccion);
            string v;
            if (direccion < 10)
            {
                v = "X" + direccion;
            }
            else
            {
                v = "X" + direccion.ToString();
            }

            string valve = (from modelo_register t in Lregistro where t.id == registro && t.xbit == v select t.vname).Last();
            string temps = valve.Split('_')[0];
            string temps1 = temps.Substring(1, temps.Length - 1);
            int temp = Convert.ToInt32(temps1);
            GUI_MODERNISTA.conexionDB.set_logs(valve, temp);

            Thread thread = new Thread(() => PulsoTiempoSet(direccion.ToString(), 3000, registro));
            thread.Start();
        }

        
        private void PulsoTiempoSet(string position, int tiempo, int registro)
        {

            // private void Escribir(string p, string v, int dir)
            Escribir(position, "1", registro);
            Thread.Sleep(tiempo);
            Escribir(position, "0", registro);

        }
        long num = 0;
        private void Escribir(string p, string v, int registro)
        {

            byte posicion = Convert.ToByte(p);
            byte valor = Convert.ToByte(v);
            if (valor == 0)
            {
                long mask = ~(1 << posicion);
                Console.WriteLine(Convert.ToString(mask, 2));
                num = num & mask;
            }
            else
            {
                long mask = 1 << posicion;
                Console.WriteLine(Convert.ToString(mask, 2));
                num = num | mask;
            }

            Console.WriteLine("El nuevo número (representado en binario) es: " + Convert.ToString(num, 2) + " y representado en decimal es: " + num);
            modbusClientTCP.WriteSingleRegister(registro, Convert.ToInt32(num));
        }


        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BarraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MenuVertical_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modbusClientTCP.Connected)
            {
                modbusClientTCP.Disconnect();
            }
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
           
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (modbusClientTCP.Connected)
            {
                modbusClientTCP.Disconnect();
            }
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Diagnóstico dia = new Diagnóstico();
            dia.Show();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        //INICIA CODIGO




    }
}
