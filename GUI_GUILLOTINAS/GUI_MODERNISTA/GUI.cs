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
    public partial class GUI : Form
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
        bool[] ArrEstLinea4 = new bool[22];
        bool[] ArrEstLinea3 = new bool[22];
        int JaulaActiva = 1;
        int[] ArrJaula1 = new int[] { 0, 1, 2, 3 };
        int[] ArrJaula2 = new int[] { 4, 5, 6, 7 };
        int[] ArrJaula3 = new int[] { 8, 9, 10, 11 };
        int[] ArrJaula4 = new int[] { 12, 13, 14, 15 };
        int[] ArrJaula5 = new int[] { 16, 17, 18, 19, };




        public static int tpulso = 5000;
        public GUI()
        {
            InitializeComponent();

            //Titulo-----------
            label1.Location = new Point(105, 30);
            label1.Font = new Font(label1.Font.FontFamily, 24);
            label1.Font = new Font(label1.Font, FontStyle.Bold);

            label3.Location = new Point(105, 30);
            label3.Font = new Font(label3.Font.FontFamily, 24);
            label3.Font = new Font(label3.Font, FontStyle.Bold);

            //Paneles
            panel16.Size = new Size(195, 305);
            panel13.Size = new Size(195, 305);
            panel16.Location = new Point(180, 40);
            panel13.Location = new Point(180, 30);

            //Botones linea 4-----------
            BtnOn1.Size = new Size(147, 100);
            BtnOff1.Size = new Size(147, 100);
            BtnManual.Size = new Size(147, 100);
            BtnRemoto.Size = new Size(147, 100);

            BtnOn1.Location = new Point(20, 40);
            BtnOff1.Location = new Point(20, 206);

            BtnManual.Location = new Point(390, 40);
            BtnRemoto.Location = new Point(390, 206);
            //Textos linea 4
            label5.Location = new Point(67, 260);
            label6.Location = new Point(65, 430);

            label11.Location = new Point(425, 260);
            label14.Location = new Point(425, 430);

            label5.Font = new Font(label5.Font.FontFamily, 16);
            label5.Font = new Font(label5.Font, FontStyle.Bold);

            label6.Font = new Font(label6.Font.FontFamily, 16);
            label6.Font = new Font(label6.Font, FontStyle.Bold);

            label11.Font = new Font(label11.Font.FontFamily, 16);
            label11.Font = new Font(label11.Font, FontStyle.Bold);

            label14.Font = new Font(label14.Font.FontFamily, 16);
            label14.Font = new Font(label14.Font, FontStyle.Bold);

            //Botones linea 3-----------
            BtnOn2.Location = new Point(20, 25);
            BtnOff2.Location = new Point(20, 200);
            BtnManual2.Location = new Point(390, 25);
            BtnRemoto2.Location = new Point(390, 200);

            BtnOn2.Size = new Size(147, 100);
            BtnOff2.Size = new Size(147, 100);
            BtnManual2.Size = new Size(147, 100);
            BtnRemoto2.Size = new Size(147, 100);
            //Textos linea 3
            label10.Location = new Point(65, 250);
            label9.Location = new Point(62, 419);
            label12.Location = new Point(422, 250);
            label13.Location = new Point(422, 419);

            label10.Font = new Font(label10.Font.FontFamily, 16);
            label10.Font = new Font(label10.Font, FontStyle.Bold);

            label9.Font = new Font(label9.Font.FontFamily, 16);
            label9.Font = new Font(label9.Font, FontStyle.Bold);

            label12.Font = new Font(label12.Font.FontFamily, 16);
            label12.Font = new Font(label12.Font, FontStyle.Bold);

            label13.Font = new Font(label13.Font.FontFamily, 16);
            label13.Font = new Font(label13.Font, FontStyle.Bold);


            //circulitos rojos
            pb01.Location = new Point(300, 200);
            pb01.Size = new Size(30, 30);
            pb02.Location = new Point(398, 270);
            pb02.Size = new Size(30, 30);
            pb03.Location = new Point(403, 123);
            pb03.Size = new Size(30, 30);
            pb04.Location = new Point(502, 193);
            pb04.Size = new Size(30, 30);
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
            //Titulo-----------
            label1.Location = new Point(105, 30);
            label1.Font = new Font(label1.Font.FontFamily, 24);
            label1.Font = new Font(label1.Font, FontStyle.Bold);

            label3.Location = new Point(105, 30);
            label3.Font = new Font(label3.Font.FontFamily, 24);
            label3.Font = new Font(label3.Font, FontStyle.Bold);
            //paneles-------
            panel16.Size = new Size(195, 305);
            panel13.Size = new Size(195, 305);
            panel16.Location = new Point(180, 145);
            panel13.Location = new Point(180, 140);

            //botones linea 4 
            BtnOn1.Size = new Size(147, 100);
            BtnOff1.Size = new Size(147, 100);

            BtnManual.Size = new Size(147, 100);
            BtnRemoto.Size = new Size(147, 100);

            BtnOn1.Location = new Point(20, 135);
            BtnOff1.Location = new Point(20, 310);

            BtnManual.Location = new Point(390, 135);
            BtnRemoto.Location = new Point(390, 310);

            //Textos linea 4
            label5.Location = new Point(67, 260);
            label6.Location = new Point(65, 430);

            label11.Location = new Point(425, 260);
            label14.Location = new Point(425, 430);

            label5.Font = new Font(label5.Font.FontFamily, 16);
            label5.Font = new Font(label5.Font, FontStyle.Bold);

            label6.Font = new Font(label6.Font.FontFamily, 16);
            label6.Font = new Font(label6.Font, FontStyle.Bold);

            label11.Font = new Font(label11.Font.FontFamily, 16);
            label11.Font = new Font(label11.Font, FontStyle.Bold);

            label14.Font = new Font(label14.Font.FontFamily, 16);
            label14.Font = new Font(label14.Font, FontStyle.Bold);


            //Botones linea 3-----------
            BtnOn2.Location = new Point(20, 130);
            BtnOff2.Location = new Point(20, 300);
            BtnManual2.Location = new Point(390, 130);
            BtnRemoto2.Location = new Point(390, 300);

            BtnOn2.Size = new Size(147, 100);
            BtnOff2.Size = new Size(147, 100);
            BtnManual2.Size = new Size(147, 100);
            BtnRemoto2.Size = new Size(147, 100);
            //Textos linea 3
            label10.Location = new Point(65, 250);
            label9.Location = new Point(62, 419);
            label12.Location = new Point(422, 250);
            label13.Location = new Point(422, 419);

            label10.Font = new Font(label10.Font.FontFamily, 16);
            label10.Font = new Font(label10.Font, FontStyle.Bold);

            label9.Font = new Font(label9.Font.FontFamily, 16);
            label9.Font = new Font(label9.Font, FontStyle.Bold);

            label12.Font = new Font(label12.Font.FontFamily, 16);
            label12.Font = new Font(label12.Font, FontStyle.Bold);

            label13.Font = new Font(label13.Font.FontFamily, 16);
            label13.Font = new Font(label13.Font, FontStyle.Bold);

            //circulitos rojos
            pb01.Location = new Point(300, 200);
            pb01.Size = new Size(30, 30);
            pb02.Location = new Point(398, 270);
            pb02.Size = new Size(30, 30);
            pb03.Location = new Point(403, 123);
            pb03.Size = new Size(30, 30);
            pb04.Location = new Point(502, 193);
            pb04.Size = new Size(30, 30);
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;

            //Titulo-----------
            label1.Location = new Point(95, 11);
            label1.Font = new Font(label1.Font.FontFamily, 18);
            label1.Font = new Font(label1.Font, FontStyle.Bold);

            label3.Location = new Point(96, 10);
            label3.Font = new Font(label3.Font.FontFamily, 18);
            label3.Font = new Font(label3.Font, FontStyle.Bold);

            //paneles-------
            panel16.Size = new Size(122, 203);
            panel13.Size = new Size(122, 191);
            panel16.Location = new Point(127, 66);
            panel13.Location = new Point(96, 64);
            //Botones linea 4-----------
            BtnOn1.Location = new Point(14, 71);
            BtnOff1.Location = new Point(14, 176);
            BtnManual.Location = new Point(255, 71);
            BtnRemoto.Location = new Point(255, 176);
            BtnOn1.Size = new Size(92, 63);
            BtnOff1.Size = new Size(92, 63);
            BtnManual.Size = new Size(92, 63);
            BtnRemoto.Size = new Size(92, 63);
            //Textos linea 4
            label5.Location = new Point(37, 139);
            label6.Location = new Point(30, 245);
            label11.Location = new Point(271, 139);
            label14.Location = new Point(271, 245);

            label5.Font = new Font(label5.Font.FontFamily, 12);
            label5.Font = new Font(label5.Font, FontStyle.Bold);

            label6.Font = new Font(label6.Font.FontFamily, 12);
            label6.Font = new Font(label6.Font, FontStyle.Bold);

            label11.Font = new Font(label11.Font.FontFamily, 12);
            label11.Font = new Font(label11.Font, FontStyle.Bold);

            label14.Font = new Font(label14.Font.FontFamily, 12);
            label14.Font = new Font(label14.Font, FontStyle.Bold);

            //Botones linea 3-----------
            BtnOn2.Location = new Point(3, 64);
            BtnOff2.Location = new Point(3, 171);
            BtnManual2.Location = new Point(238, 64);
            BtnRemoto2.Location = new Point(238, 171);
            BtnOn2.Size = new Size(92, 63);
            BtnOff2.Size = new Size(92, 63);
            BtnManual2.Size = new Size(92, 63);
            BtnRemoto2.Size = new Size(92, 63);
            //Textos linea 3
            label10.Location = new Point(30, 130);
            label9.Location = new Point(22, 236);
            label12.Location = new Point(255, 129);
            label13.Location = new Point(255, 236);

            label10.Font = new Font(label10.Font.FontFamily, 12);
            label10.Font = new Font(label10.Font, FontStyle.Bold);

            label9.Font = new Font(label9.Font.FontFamily, 12);
            label9.Font = new Font(label9.Font, FontStyle.Bold);

            label12.Font = new Font(label12.Font.FontFamily, 12);
            label12.Font = new Font(label12.Font, FontStyle.Bold);

            label13.Font = new Font(label13.Font.FontFamily, 12);
            label13.Font = new Font(label13.Font, FontStyle.Bold);

            //circulitos rojos
            pb01.Location = new Point(186, 114);
            pb01.Size = new Size(15, 15);
            pb02.Location = new Point(251, 147);
            pb02.Size = new Size(15, 15);
            pb03.Location = new Point(251, 69);
            pb03.Size = new Size(15, 15);
            pb04.Location = new Point(314, 114);
            pb04.Size = new Size(15, 15);
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
        private bool conectar()
        {
            try
            {
                modbusClientTCP.IPAddress = "192.168.0.253";
                modbusClientTCP.Parity = System.IO.Ports.Parity.None;
                modbusClientTCP.StopBits = System.IO.Ports.StopBits.One;
                modbusClientTCP.Baudrate = 9600;
                modbusClientTCP.ConnectionTimeout = 2000;

                modbusClientTCP.Connect();

                if (modbusClientTCP.Connected)
                { Console.WriteLine("Connectado"); }

                else
                { Console.WriteLine("NO Connectado"); }

                return modbusClientTCP.Connected;
            }
            catch (Exception r)
            {
                Console.WriteLine("No conectado " + r.ToString());
                return false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            conectar();
            //   Cargar default
            tpulso = conexionDB.get_pulso();
            Btn_J1.PerformClick();
            BtnV3_02.PerformClick();
            BtnV4_01.PerformClick();

            for (int i = 0; i <= 21; i++)
            {
                ArrEstLinea4[i] = false;
                ArrEstLinea3[i] = false;

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



        public static int RFvalue;
        public static int RSSI;

        //bool cliente_conectado = conectar();
        //Thread.Sleep(1000);
        //while (cliente_conectado)
        //{
        //    try
        //    {   //LEER DATOS DE ESCRITURA HACIA EL PLC
        //        int[] datoRead = modbusClientTCP.ReadInputRegisters(3001, 15);
        //        //diagnostico
        //        RFvalue = datoRead[11];
        //        RSSI = datoRead[12];
        //        Console.WriteLine("RADIO VAL_11 : " + datoRead[11]);
        //        Console.WriteLine("RADIO VAL_12 : " + datoRead[12]);

        //        backgroundWorker1.ReportProgress(1, datoRead);

        //        Thread.Sleep(1000);
        //    }
        //    catch
        //    {
        //        modbusClientTCP.Disconnect();
        //        Thread.Sleep(200);
        //        break;
        //    }

        //}


        //ESTADO ON/OFF  LINEA4
        //if (linea == "Linea4")
        //{

        //    if (close == false && open == false)
        //    {
        //        panel16.BackgroundImage = Properties.Resources.ind_guillotina_off;
        //    }
        //    else if (close == false && open == true)
        //    {
        //        panel16.BackgroundImage = Properties.Resources.ind_guillotina_abierta;

        //    }
        //    else if (close == true && open == false)
        //    {
        //        panel16.BackgroundImage = Properties.Resources.ind_guillotina_cerrada;

        //    }

        //    //Estado Remoto

        //    if (remoto)
        //    {
        //        BtnRemoto.BackgroundImage = Properties.Resources.btn_auto;
        //        BtnManual.BackgroundImage = Properties.Resources.btn_off;
        //    }
        //    else
        //    {
        //        BtnRemoto.BackgroundImage = Properties.Resources.btn_off;
        //        BtnManual.BackgroundImage = Properties.Resources.btn_manual;
        //    }


        //}

        ////ESTADO ON/OFF  LINEA3
        //if (linea == "Linea3")
        //{
        //    if (close == false && open == false)
        //    {
        //        panel13.BackgroundImage = Properties.Resources.ind_guillotina_off;
        //    }
        //    else if (close == false && open == true)
        //    {
        //        panel13.BackgroundImage = Properties.Resources.ind_guillotina_abierta;

        //    }
        //    else if (close == true && open == false)
        //    {
        //        panel13.BackgroundImage = Properties.Resources.ind_guillotina_cerrada;

        //    }

        //    //Estado Remoto

        //    if (remoto)
        //    {
        //        BtnRemoto2.BackgroundImage = Properties.Resources.btn_auto;
        //        BtnManual2.BackgroundImage = Properties.Resources.btn_off;
        //    }
        //    else
        //    {
        //        BtnRemoto2.BackgroundImage = Properties.Resources.btn_off;
        //        BtnManual2.BackgroundImage = Properties.Resources.btn_manual;
        //    }
        //}         



        public static byte[] ConvertInt32ToByteArray(Int32 I32)
        {
            return BitConverter.GetBytes(I32);
        }
        private void Btn_JaulaSelected_Click(object sender, EventArgs e)
        {
            BtnV3_04.BackColor = Color.Transparent;
            BtnV3_02.BackColor = Color.Transparent;
            BtnV4_03.BackColor = Color.Transparent;
            BtnV4_05.BackColor = Color.Transparent;
            BtnV4_01.BackColor = Color.Transparent;

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
                changetext(j);
                JaulaActiva = 1;



            }
            else if (n == "Btn_J2")
            {
                panel14.BackgroundImage = Properties.Resources.zoom_out_jaula05_08;
                panel8.BackgroundImage = Properties.Resources.jaula05_08;
                j = 2;

                vect = new int[] { 15, 16, 17, 18, 19 };
                changetext(j);
                JaulaActiva = 2;
            }
            else if (n == "Btn_J3")
            {
                panel14.BackgroundImage = Properties.Resources.zoom_out_jaula09_12;
                panel8.BackgroundImage = Properties.Resources.jaula09_12;
                j = 3;
                vect = new int[] { 24, 25, 26, 27, 28 };
                changetext(j);
                JaulaActiva = 3;
            }
            else if (n == "Btn_J4")
            {
                panel14.BackgroundImage = Properties.Resources.zoom_out_jaula13_16;
                panel8.BackgroundImage = Properties.Resources.jaula13_16;
                j = 4;
                vect = new int[] { 33, 34, 35, 36, 37 };
                changetext(j);
                JaulaActiva = 4;
            }
            else if (n == "Btn_J5")
            {
                panel14.BackgroundImage = Properties.Resources.zoom_out_jaula19_20;
                panel8.BackgroundImage = Properties.Resources.jaula19_20;
                j = 5;
                vect = new int[] { 42, 43, 44, 45, 46 };
                changetext(j);
                JaulaActiva = 5;
            }


            if (JaulaActiva == 1)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (label15.Text == "Linea 4")
                    {
                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea4[ArrJaula1[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea4[ArrJaula1[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea4[ArrJaula1[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea4[ArrJaula1[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea3[ArrJaula1[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea3[ArrJaula1[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea3[ArrJaula1[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea3[ArrJaula1[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }
                    }
                }
            }

            if (JaulaActiva == 2)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (label15.Text == "Linea 4")
                    {
                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea4[ArrJaula2[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea4[ArrJaula2[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea4[ArrJaula2[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea4[ArrJaula2[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea3[ArrJaula2[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea3[ArrJaula2[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea3[ArrJaula2[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea3[ArrJaula2[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }
                    }
                }
            }

            if (JaulaActiva == 3)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (label15.Text == "Linea 4")
                    {
                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea4[ArrJaula3[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea4[ArrJaula3[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea4[ArrJaula3[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea4[ArrJaula3[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea3[ArrJaula3[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea3[ArrJaula3[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea3[ArrJaula3[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea3[ArrJaula3[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }
                    }
                }
            }

            if (JaulaActiva == 4)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (label15.Text == "Linea 4")
                    {
                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea4[ArrJaula4[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea4[ArrJaula4[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea4[ArrJaula4[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea4[ArrJaula4[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea3[ArrJaula4[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea3[ArrJaula4[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea3[ArrJaula4[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea3[ArrJaula4[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }
                    }
                }
            }

            if (JaulaActiva == 5)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (label15.Text == "Linea 4")
                    {
                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea4[ArrJaula5[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea4[ArrJaula5[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea4[ArrJaula5[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea4[ArrJaula5[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea3[ArrJaula5[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea3[ArrJaula5[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea3[ArrJaula5[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea3[ArrJaula5[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }
                    }
                }
            }

        }
        private void changetext(int j)
        {
            BtnV4_03.Visible = true;
            BtnV4_05.Visible = true;
            BtnV3_04.Visible = true;
            if (j == 1)
            {
                BtnV4_01.Text = "V01";
                BtnV4_03.Text = "V03";
                BtnV4_05.Text = "V05";

                BtnV3_02.Text = "V02";
                BtnV3_04.Text = "V04";

            }
            if (j == 2)
            {
                BtnV4_01.Text = "V06";
                BtnV4_03.Text = "V08";
                BtnV4_05.Text = "V10";

                BtnV3_02.Text = "V07";
                BtnV3_04.Text = "V09";
            }
            if (j == 3)
            {
                BtnV4_01.Text = "V11";
                BtnV4_03.Text = "V13";
                BtnV4_05.Text = "V15";

                BtnV3_02.Text = "V12";
                BtnV3_04.Text = "V14";
            }

            if (j == 4)
            {
                BtnV4_01.Text = "V16";
                BtnV4_03.Text = "V18";
                BtnV4_05.Text = "V20";

                BtnV3_02.Text = "V17";
                BtnV3_04.Text = "V19";
            }
            if (j == 5)
            {
                BtnV4_01.Text = "V21";
                BtnV4_03.Visible = false;
                BtnV4_05.Visible = false;

                BtnV3_02.Text = "V22";
                BtnV3_04.Visible = false;
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
                Jselected.BackColor = Color.FromArgb(0, 80, 200);
                label15.Text = "Linea 3";

                if (JaulaActiva == 1)
                {
                    for (int i = 0; i <= 3; i++)
                    {

                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea3[ArrJaula1[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea3[ArrJaula1[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea3[ArrJaula1[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea3[ArrJaula1[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }
                    }

                }

                if (JaulaActiva == 2)
                {
                    for (int i = 0; i <= 3; i++)
                    {

                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea3[ArrJaula2[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea3[ArrJaula2[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea3[ArrJaula2[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea3[ArrJaula2[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }

                    }
                }

                if (JaulaActiva == 3)
                {
                    for (int i = 0; i <= 3; i++)
                    {

                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea3[ArrJaula3[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea3[ArrJaula3[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea3[ArrJaula3[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea3[ArrJaula3[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }
                    }

                }

                if (JaulaActiva == 4)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea3[ArrJaula4[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea3[ArrJaula4[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea3[ArrJaula4[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea3[ArrJaula4[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }

                    }
                }

                if (JaulaActiva == 5)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                if (ArrEstLinea3[ArrJaula5[i]] == false)
                                {
                                    pb01.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 1:
                                if (ArrEstLinea3[ArrJaula5[i]] == false)
                                {
                                    pb02.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 2:
                                if (ArrEstLinea3[ArrJaula5[i]] == false)
                                {
                                    pb03.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                            case 3:
                                if (ArrEstLinea3[ArrJaula5[i]] == false)
                                {
                                    pb04.BackgroundImage = Properties.Resources.RojoVector;
                                }
                                else
                                {
                                    pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                }
                                break;
                        }

                    }
                }

            }
            else
            {
                indice = index_all;
                Jselected.BackColor = Color.FromArgb(255, 206, 82);
                label15.Text = "Linea 4";

                if (JaulaActiva == 1)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        if (label15.Text == "Linea 4")
                        {
                            switch (i)
                            {
                                case 0:
                                    if (ArrEstLinea4[ArrJaula1[i]] == false)
                                    {
                                        pb01.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 1:
                                    if (ArrEstLinea4[ArrJaula1[i]] == false)
                                    {
                                        pb02.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 2:
                                    if (ArrEstLinea4[ArrJaula1[i]] == false)
                                    {
                                        pb03.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 3:
                                    if (ArrEstLinea4[ArrJaula1[i]] == false)
                                    {
                                        pb04.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                            }
                        }

                    }
                }

                if (JaulaActiva == 2)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        if (label15.Text == "Linea 4")
                        {
                            switch (i)
                            {
                                case 0:
                                    if (ArrEstLinea4[ArrJaula2[i]] == false)
                                    {
                                        pb01.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 1:
                                    if (ArrEstLinea4[ArrJaula2[i]] == false)
                                    {
                                        pb02.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 2:
                                    if (ArrEstLinea4[ArrJaula2[i]] == false)
                                    {
                                        pb03.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 3:
                                    if (ArrEstLinea4[ArrJaula2[i]] == false)
                                    {
                                        pb04.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                            }
                        }

                    }
                }

                if (JaulaActiva == 3)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        if (label15.Text == "Linea 4")
                        {
                            switch (i)
                            {
                                case 0:
                                    if (ArrEstLinea4[ArrJaula3[i]] == false)
                                    {
                                        pb01.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 1:
                                    if (ArrEstLinea4[ArrJaula3[i]] == false)
                                    {
                                        pb02.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 2:
                                    if (ArrEstLinea4[ArrJaula3[i]] == false)
                                    {
                                        pb03.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 3:
                                    if (ArrEstLinea4[ArrJaula3[i]] == false)
                                    {
                                        pb04.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                            }
                        }

                    }
                }

                if (JaulaActiva == 4)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        if (label15.Text == "Linea 4")
                        {
                            switch (i)
                            {
                                case 0:
                                    if (ArrEstLinea4[ArrJaula4[i]] == false)
                                    {
                                        pb01.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 1:
                                    if (ArrEstLinea4[ArrJaula4[i]] == false)
                                    {
                                        pb02.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 2:
                                    if (ArrEstLinea4[ArrJaula4[i]] == false)
                                    {
                                        pb03.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 3:
                                    if (ArrEstLinea4[ArrJaula4[i]] == false)
                                    {
                                        pb04.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                            }
                        }
                    }
                }

                if (JaulaActiva == 5)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        if (label15.Text == "Linea 4")
                        {
                            switch (i)
                            {
                                case 0:
                                    if (ArrEstLinea4[ArrJaula5[i]] == false)
                                    {
                                        pb01.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb01.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 1:
                                    if (ArrEstLinea4[ArrJaula5[i]] == false)
                                    {
                                        pb02.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb02.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 2:
                                    if (ArrEstLinea4[ArrJaula5[i]] == false)
                                    {
                                        pb03.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb03.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                                case 3:
                                    if (ArrEstLinea4[ArrJaula5[i]] == false)
                                    {
                                        pb04.BackgroundImage = Properties.Resources.RojoVector;
                                    }
                                    else
                                    {
                                        pb04.BackgroundImage = Properties.Resources.VerdeVector;
                                    }
                                    break;
                            }
                        }

                    }
                }
            }

            if (indice2 == 2)
            {
                BtnV3_04.BackColor = Color.Transparent;
            }
            else if (indice2 == 4)
            {
                BtnV3_02.BackColor = Color.Transparent;
            }

            if (indice == 1)
            {

                BtnV4_03.BackColor = Color.Transparent;
                BtnV4_05.BackColor = Color.Transparent;
            }
            else if (indice == 3)
            {
                BtnV4_01.BackColor = Color.Transparent;
                BtnV4_05.BackColor = Color.Transparent;

            }
            else if (indice == 5)
            {
                BtnV4_01.BackColor = Color.Transparent;
                BtnV4_03.BackColor = Color.Transparent;
                //  BtnV4_05.BackColor = Color.FromArgb(26, 32, 40);
            }
        }

        private void BtnOn_Click(object sender, EventArgs e)
        {

            switch (JaulaActiva)
            {
                case 1:
                    switch (indice)
                    {
                        case 1:
                            modbusClientTCP.WriteSingleRegister(3152, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3153, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V01_Abrir", 1);
                            break;
                        case 3:
                            modbusClientTCP.WriteSingleRegister(3156, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3157, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V03_Abrir", 3);
                            break;
                        case 5:
                            modbusClientTCP.WriteSingleRegister(3160, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3161, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V05_Abrir", 5);
                            break;
                    }
                    break;
                case 2:
                    switch (indice)
                    {
                        case 1://6
                            modbusClientTCP.WriteSingleRegister(3162, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3163, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V06_Abrir", 6);
                            break;
                        case 3://8
                            modbusClientTCP.WriteSingleRegister(3166, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3167, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V08_Abrir", 8);
                            break;
                        case 5://10
                            modbusClientTCP.WriteSingleRegister(3170, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3171, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V010_Abrir", 10);
                            break;
                    }
                    break;
                case 3:
                    switch (indice)
                    {
                        case 1://11

                            Thread thread = new Thread(() => dasdsa());
                            thread.Start();
                            GUI_MODERNISTA.conexionDB.set_logs("V11_Abrir", 11);
                            break;
                        case 3://13
                            modbusClientTCP.WriteSingleRegister(3176, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3177, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V13_Abrir", 13);
                            break;
                        case 5://15
                            modbusClientTCP.WriteSingleRegister(3180, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3181, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V15_Abrir", 15);
                            break;
                    }
                    break;
                case 4:
                    switch (indice)
                    {
                        case 1://16
                            modbusClientTCP.WriteSingleRegister(3182, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3183, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V16_Abrir", 16);
                            break;
                        case 3://18
                            modbusClientTCP.WriteSingleRegister(3186, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3187, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V18_Abrir", 18);
                            break;
                        case 5://20
                            modbusClientTCP.WriteSingleRegister(3190, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3191, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V20_Abrir", 20);
                            break;
                    }
                    break;
                case 5:
                    switch (indice)
                    {
                        case 1://21
                            modbusClientTCP.WriteSingleRegister(3192, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3193, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V21_Abrir", 21);
                            break;
                    }
                    break;
            }


        }
        private void BtnOff_Click(object sender, EventArgs e)
        {

            switch (JaulaActiva)
            {
                case 1:
                    switch (indice)
                    {
                        case 1:
                            modbusClientTCP.WriteSingleRegister(3152, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3153, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V01_Cerrar", 1);
                            break;
                        case 3:
                            modbusClientTCP.WriteSingleRegister(3156, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3157, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V03_Cerrar", 3);
                            break;
                        case 5:
                            modbusClientTCP.WriteSingleRegister(3160, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3161, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V05_Cerrar", 5);
                            break;
                    }
                    break;
                case 2:
                    switch (indice)
                    {
                        case 1://6
                            modbusClientTCP.WriteSingleRegister(3162, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3163, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V06_Cerrar", 6);
                            break;
                        case 3://8
                            modbusClientTCP.WriteSingleRegister(3166, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3167, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V08_Cerrar", 8);
                            break;
                        case 5://10
                            modbusClientTCP.WriteSingleRegister(3170, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3171, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V10_Cerrar", 10);
                            break;
                    }
                    break;
                case 3:
                    switch (indice)
                    {
                        case 1://11

                            modbusClientTCP.WriteSingleRegister(3172, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3173, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V11_Cerrar", 11);
                            break;
                        case 3://13
                            modbusClientTCP.WriteSingleRegister(3176, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3177, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V13_Cerrar", 13);
                            break;
                        case 5://15
                            modbusClientTCP.WriteSingleRegister(3180, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3181, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V15_Cerrar", 15);
                            break;
                    }
                    break;
                case 4:
                    switch (indice)
                    {
                        case 1://16
                            modbusClientTCP.WriteSingleRegister(3182, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3183, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V16_Cerrar", 16);
                            break;
                        case 3://18
                            modbusClientTCP.WriteSingleRegister(3186, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3187, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V18_Cerrar", 18);
                            break;
                        case 5://20
                            modbusClientTCP.WriteSingleRegister(3190, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3191, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V20_Cerrar", 20);
                            break;
                    }
                    break;
                case 5:
                    switch (indice)
                    {
                        case 1://21
                            modbusClientTCP.WriteSingleRegister(3192, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3193, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V21_Cerrar", 21);
                            break;
                    }
                    break;
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modbusClientTCP.Connected)
            {
                modbusClientTCP.Disconnect();
            }


        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (modbusClientTCP.Connected)
            {
                modbusClientTCP.Disconnect();
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Diagnóstico dia = new Diagnóstico();
            dia.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Configuración cn = new Configuración();
            cn.Show();

        }



        private void pb01_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (label15.Text == "Linea 4")
            {
                switch (JaulaActiva)
                {
                    case 1:
                        if (ArrEstLinea4[ArrJaula1[0]] == false)
                        {
                            pb01.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula1[0]] = true;
                        }
                        else
                        {
                            pb01.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula1[0]] = false;
                        }
                        break;
                    case 2:
                        if (ArrEstLinea4[ArrJaula2[0]] == false)
                        {
                            pb01.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula2[0]] = true;
                        }
                        else
                        {
                            pb01.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula2[0]] = false;
                        }
                        break;
                    case 3:
                        if (ArrEstLinea4[ArrJaula3[0]] == false)
                        {
                            pb01.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula3[0]] = true;
                        }
                        else
                        {
                            pb01.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula3[0]] = false;
                        }
                        break;
                    case 4:
                        if (ArrEstLinea4[ArrJaula4[0]] == false)
                        {
                            pb01.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula4[0]] = true;
                        }
                        else
                        {
                            pb01.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula4[0]] = false;
                        }
                        break;
                    case 5:
                        if (ArrEstLinea4[ArrJaula5[0]] == false)
                        {
                            pb01.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula5[0]] = true;
                        }
                        else
                        {
                            pb01.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula5[0]] = false;
                        }
                        break;
                }
            }
            else
            {
                switch (JaulaActiva)
                {
                    case 1:
                        if (ArrEstLinea3[ArrJaula1[0]] == false)
                        {
                            pb01.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula1[0]] = true;
                        }
                        else
                        {
                            pb01.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula1[0]] = false;
                        }
                        break;
                    case 2:
                        if (ArrEstLinea3[ArrJaula2[0]] == false)
                        {
                            pb01.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula2[0]] = true;
                        }
                        else
                        {
                            pb01.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula2[0]] = false;
                        }
                        break;
                    case 3:
                        if (ArrEstLinea3[ArrJaula3[0]] == false)
                        {
                            pb01.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula3[0]] = true;
                        }
                        else
                        {
                            pb01.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula3[0]] = false;
                        }
                        break;
                    case 4:
                        if (ArrEstLinea3[ArrJaula4[0]] == false)
                        {
                            pb01.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula4[0]] = true;
                        }
                        else
                        {
                            pb01.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula4[0]] = false;
                        }
                        break;
                    case 5:
                        if (ArrEstLinea3[ArrJaula5[0]] == false)
                        {
                            pb01.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula5[0]] = true;
                        }
                        else
                        {
                            pb01.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula5[0]] = false;
                        }
                        break;
                }
            }


        }

        private void pb02_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (label15.Text == "Linea 4")
            {
                switch (JaulaActiva)
                {
                    case 1:
                        if (ArrEstLinea4[ArrJaula1[1]] == false)
                        {
                            pb02.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula1[1]] = true;
                        }
                        else
                        {
                            pb02.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula1[1]] = false;
                        }
                        break;
                    case 2:
                        if (ArrEstLinea4[ArrJaula2[1]] == false)
                        {
                            pb02.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula2[1]] = true;
                        }
                        else
                        {
                            pb02.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula2[1]] = false;
                        }
                        break;
                    case 3:
                        if (ArrEstLinea4[ArrJaula3[1]] == false)
                        {
                            pb02.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula3[1]] = true;
                        }
                        else
                        {
                            pb02.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula3[1]] = false;
                        }
                        break;
                    case 4:
                        if (ArrEstLinea4[ArrJaula4[1]] == false)
                        {
                            pb02.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula4[1]] = true;
                        }
                        else
                        {
                            pb02.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula4[1]] = false;
                        }
                        break;
                    case 5:
                        if (ArrEstLinea4[ArrJaula5[1]] == false)
                        {
                            pb02.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula5[1]] = true;
                        }
                        else
                        {
                            pb02.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula5[1]] = false;
                        }
                        break;
                }
            }
            else
            {
                switch (JaulaActiva)
                {
                    case 1:
                        if (ArrEstLinea3[ArrJaula1[1]] == false)
                        {
                            pb02.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula1[1]] = true;
                        }
                        else
                        {
                            pb02.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula1[1]] = false;
                        }
                        break;
                    case 2:
                        if (ArrEstLinea3[ArrJaula2[1]] == false)
                        {
                            pb02.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula2[1]] = true;
                        }
                        else
                        {
                            pb02.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula2[1]] = false;
                        }
                        break;
                    case 3:
                        if (ArrEstLinea3[ArrJaula3[1]] == false)
                        {
                            pb02.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula3[1]] = true;
                        }
                        else
                        {
                            pb02.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula3[1]] = false;
                        }
                        break;
                    case 4:
                        if (ArrEstLinea3[ArrJaula4[1]] == false)
                        {
                            pb02.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula4[1]] = true;
                        }
                        else
                        {
                            pb02.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula4[1]] = false;
                        }
                        break;
                    case 5:
                        if (ArrEstLinea3[ArrJaula5[1]] == false)
                        {
                            pb02.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula5[1]] = true;
                        }
                        else
                        {
                            pb02.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula5[1]] = false;
                        }
                        break;
                }
            }

        }

        private void pb03_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (label15.Text == "Linea 4")
            {
                switch (JaulaActiva)
                {
                    case 1:
                        if (ArrEstLinea4[ArrJaula1[2]] == false)
                        {
                            pb03.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula1[2]] = true;
                        }
                        else
                        {
                            pb03.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula1[2]] = false;
                        }
                        break;
                    case 2:
                        if (ArrEstLinea4[ArrJaula2[2]] == false)
                        {
                            pb03.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula2[2]] = true;
                        }
                        else
                        {
                            pb03.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula2[2]] = false;
                        }
                        break;
                    case 3:
                        if (ArrEstLinea4[ArrJaula3[2]] == false)
                        {
                            pb03.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula3[2]] = true;
                        }
                        else
                        {
                            pb03.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula3[2]] = false;
                        }
                        break;
                    case 4:
                        if (ArrEstLinea4[ArrJaula4[2]] == false)
                        {
                            pb03.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula4[2]] = true;
                        }
                        else
                        {
                            pb03.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula4[2]] = false;
                        }
                        break;
                    case 5:
                        if (ArrEstLinea4[ArrJaula5[2]] == false)
                        {
                            pb03.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula5[2]] = true;
                        }
                        else
                        {
                            pb03.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula5[2]] = false;
                        }
                        break;
                }
            }
            else
            {
                switch (JaulaActiva)
                {
                    case 1:
                        if (ArrEstLinea3[ArrJaula1[2]] == false)
                        {
                            pb03.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula1[2]] = true;
                        }
                        else
                        {
                            pb03.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula1[2]] = false;
                        }
                        break;
                    case 2:
                        if (ArrEstLinea3[ArrJaula2[2]] == false)
                        {
                            pb03.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula2[2]] = true;
                        }
                        else
                        {
                            pb03.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula2[2]] = false;
                        }
                        break;
                    case 3:
                        if (ArrEstLinea3[ArrJaula3[2]] == false)
                        {
                            pb03.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula3[2]] = true;
                        }
                        else
                        {
                            pb03.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula3[2]] = false;
                        }
                        break;
                    case 4:
                        if (ArrEstLinea3[ArrJaula4[2]] == false)
                        {
                            pb03.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula4[2]] = true;
                        }
                        else
                        {
                            pb03.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula4[2]] = false;
                        }
                        break;
                    case 5:
                        if (ArrEstLinea3[ArrJaula5[2]] == false)
                        {
                            pb03.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula5[2]] = true;
                        }
                        else
                        {
                            pb03.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula5[2]] = false;
                        }
                        break;
                }
            }

        }

        private void pb04_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (label15.Text == "Linea 4")
            {
                switch (JaulaActiva)
                {
                    case 1:
                        if (ArrEstLinea4[ArrJaula1[3]] == false)
                        {
                            pb04.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula1[3]] = true;
                        }
                        else
                        {
                            pb04.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula1[3]] = false;
                        }
                        break;
                    case 2:
                        if (ArrEstLinea4[ArrJaula2[3]] == false)
                        {
                            pb04.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula2[3]] = true;
                        }
                        else
                        {
                            pb04.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula2[3]] = false;
                        }
                        break;
                    case 3:
                        if (ArrEstLinea4[ArrJaula3[3]] == false)
                        {
                            pb04.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula3[3]] = true;
                        }
                        else
                        {
                            pb04.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula3[3]] = false;
                        }
                        break;
                    case 4:
                        if (ArrEstLinea4[ArrJaula4[3]] == false)
                        {
                            pb04.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula4[3]] = true;
                        }
                        else
                        {
                            pb04.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula4[3]] = false;
                        }
                        break;
                    case 5:
                        if (ArrEstLinea4[ArrJaula5[3]] == false)
                        {
                            pb04.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea4[ArrJaula5[3]] = true;
                        }
                        else
                        {
                            pb04.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea4[ArrJaula5[3]] = false;
                        }
                        break;
                }
            }
            else
            {
                switch (JaulaActiva)
                {
                    case 1:
                        if (ArrEstLinea3[ArrJaula1[3]] == false)
                        {
                            pb04.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula1[3]] = true;
                        }
                        else
                        {
                            pb04.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula1[3]] = false;
                        }
                        break;
                    case 2:
                        if (ArrEstLinea3[ArrJaula2[3]] == false)
                        {
                            pb04.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula2[3]] = true;
                        }
                        else
                        {
                            pb04.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula2[3]] = false;
                        }
                        break;
                    case 3:
                        if (ArrEstLinea3[ArrJaula3[3]] == false)
                        {
                            pb04.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula3[3]] = true;
                        }
                        else
                        {
                            pb04.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula3[3]] = false;
                        }
                        break;
                    case 4:
                        if (ArrEstLinea3[ArrJaula4[3]] == false)
                        {
                            pb04.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula4[3]] = true;
                        }
                        else
                        {
                            pb04.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula4[3]] = false;
                        }
                        break;
                    case 5:
                        if (ArrEstLinea3[ArrJaula5[3]] == false)
                        {
                            pb04.BackgroundImage = Properties.Resources.VerdeVector;
                            ArrEstLinea3[ArrJaula5[3]] = true;
                        }
                        else
                        {
                            pb04.BackgroundImage = Properties.Resources.RojoVector;
                            ArrEstLinea3[ArrJaula5[3]] = false;
                        }
                        break;
                }
            }

        }
        private void EstadoGui(int abierto, int cerrado)
        {
            panel16.BackgroundImage = ((abierto == 0 & cerrado == 0) ? Properties.Resources.ind_guillotina_off : ((abierto == 1) ? Properties.Resources.ind_guillotina_abierta : panel16.BackgroundImage));
            panel16.BackgroundImage = ((cerrado == 1) ? Properties.Resources.ind_guillotina_cerrada : panel16.BackgroundImage);
        }
        private void Estadorem(int remoto)
        {
            BtnRemoto.BackgroundImage = ((remoto == 1) ? Properties.Resources.btn_auto : Properties.Resources.btn_off);
            BtnManual.BackgroundImage = ((remoto == 0) ? Properties.Resources.btn_manual : Properties.Resources.btn_off);
        }
        private void EstadoGui2(int abierto, int cerrado)
        {
            panel13.BackgroundImage = ((abierto == 0 & cerrado == 0) ? Properties.Resources.ind_guillotina_off : ((abierto == 1) ? Properties.Resources.ind_guillotina_abierta : panel13.BackgroundImage));
            panel13.BackgroundImage = ((cerrado == 1) ? Properties.Resources.ind_guillotina_cerrada : panel13.BackgroundImage);
        }
        private void Estadorem2(int remoto)
        {
            BtnRemoto2.BackgroundImage = ((remoto == 1) ? Properties.Resources.btn_auto : Properties.Resources.btn_off);
            BtnManual2.BackgroundImage = ((remoto == 0) ? Properties.Resources.btn_manual : Properties.Resources.btn_off);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool cliente_conectado = conectar();
            int[] datoRead;

            switch (JaulaActiva)
            {
                case 1:
                    datoRead = modbusClientTCP.ReadInputRegisters(3086, 15);
                    switch (indice)
                    {
                        case 1:
                            Estadorem(datoRead[2]);
                            EstadoGui(datoRead[0], datoRead[1]);
                            break;
                        case 3:
                            Estadorem(datoRead[8]);
                            EstadoGui(datoRead[6], datoRead[7]);

                            break;
                        case 5:
                            Estadorem(datoRead[14]);
                            EstadoGui(datoRead[12], datoRead[13]);
                            break;
                    }
                    switch (indice2)
                    {
                        case 2:

                            Estadorem2(datoRead[5]);
                            EstadoGui2(datoRead[3], datoRead[4]);
                            break;
                        case 4:
                            Estadorem2(datoRead[11]);
                            EstadoGui2(datoRead[9], datoRead[10]);

                            break;
                    }
                    break;
                case 2:
                    datoRead = modbusClientTCP.ReadInputRegisters(3101, 15);
                    switch (indice)
                    {
                        case 1:
                            Estadorem(datoRead[2]);
                            EstadoGui(datoRead[0], datoRead[1]);
                            break;
                        case 3:
                            Estadorem(datoRead[8]);
                            EstadoGui(datoRead[6], datoRead[7]);

                            break;
                        case 5:
                            Estadorem(datoRead[14]);
                            EstadoGui(datoRead[12], datoRead[13]);
                            break;
                    }
                    switch (indice2)
                    {
                        case 2:

                            Estadorem2(datoRead[5]);
                            EstadoGui2(datoRead[3], datoRead[4]);
                            break;
                        case 4:
                            Estadorem2(datoRead[11]);
                            EstadoGui2(datoRead[9], datoRead[10]);

                            break;
                    }
                    break;
                case 3:
                    datoRead = modbusClientTCP.ReadInputRegisters(3116, 15);
                    switch (indice)
                    {
                        case 1:
                            Estadorem(datoRead[2]);
                            EstadoGui(datoRead[0], datoRead[1]);
                            break;
                        case 3:
                            Estadorem(datoRead[8]);
                            EstadoGui(datoRead[6], datoRead[7]);

                            break;
                        case 5:
                            Estadorem(datoRead[14]);
                            EstadoGui(datoRead[12], datoRead[13]);
                            break;
                    }
                    switch (indice2)
                    {
                        case 2:

                            Estadorem2(datoRead[5]);
                            EstadoGui2(datoRead[3], datoRead[4]);
                            break;
                        case 4:
                            Estadorem2(datoRead[11]);
                            EstadoGui2(datoRead[9], datoRead[10]);

                            break;
                    }
                    break;
                case 4:
                    datoRead = modbusClientTCP.ReadInputRegisters(3131, 15);
                    switch (indice)
                    {
                        case 1:
                            Estadorem(datoRead[2]);
                            EstadoGui(datoRead[0], datoRead[1]);
                            break;
                        case 3:
                            Estadorem(datoRead[8]);
                            EstadoGui(datoRead[6], datoRead[7]);

                            break;
                        case 5:
                            Estadorem(datoRead[14]);
                            EstadoGui(datoRead[12], datoRead[13]);
                            break;
                    }
                    switch (indice2)
                    {
                        case 2:

                            Estadorem2(datoRead[5]);
                            EstadoGui2(datoRead[3], datoRead[4]);
                            break;
                        case 4:
                            Estadorem2(datoRead[11]);
                            EstadoGui2(datoRead[9], datoRead[10]);

                            break;
                    }
                    break;
                case 5:
                    datoRead = modbusClientTCP.ReadInputRegisters(3146, 6);
                    switch (indice)
                    {
                        case 1:
                            Estadorem(datoRead[2]);
                            EstadoGui(datoRead[0], datoRead[1]);
                            break;
                    }
                    switch (indice2)
                    {
                        case 2:
                            Estadorem2(datoRead[5]);
                            EstadoGui2(datoRead[3], datoRead[4]);
                            break;
                    }
                    break;
            }
        }

        private void BtnOn2_Click(object sender, EventArgs e)
        {
            switch (JaulaActiva)
            {
                case 1:
                    switch (indice2)
                    {
                        case 2:
                            modbusClientTCP.WriteSingleRegister(3154, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3155, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V02_Abrir", 2);
                            break;
                        case 4:
                            modbusClientTCP.WriteSingleRegister(3158, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3159, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V04_Abrir", 4);
                            break;
                    }
                    break;
                case 2:
                    switch (indice2)
                    {
                        case 2://7
                            modbusClientTCP.WriteSingleRegister(3164, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3165, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V07_Abrir", 7);
                            break;
                        case 4://9
                            modbusClientTCP.WriteSingleRegister(3168, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3169, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V09_Abrir", 9);
                            break;
                    }
                    break;
                case 3:
                    switch (indice2)
                    {
                        case 2://12
                            modbusClientTCP.WriteSingleRegister(3174, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3175, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V12_Abrir", 12);
                            break;
                        case 4://14
                            modbusClientTCP.WriteSingleRegister(3178, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3179, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V14_Abrir", 14);
                            break;
                    }
                    break;
                case 4:
                    switch (indice2)
                    {
                        case 2://17
                            modbusClientTCP.WriteSingleRegister(3184, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3185, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V17_Abrir", 17);
                            break;
                        case 4://19
                            modbusClientTCP.WriteSingleRegister(3188, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3189, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V19_Abrir", 19);
                            break;
                    }
                    break;
                case 5:
                    switch (indice2)
                    {
                        case 2://22
                            modbusClientTCP.WriteSingleRegister(3194, 1);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3195, 0);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V22_Abrir", 22);
                            break;
                    }
                    break;
            }
        }

        private void dasdsa()
        {
            modbusClientTCP.WriteSingleRegister(3172, 1);
            Thread.Sleep(1000);
            modbusClientTCP.WriteSingleRegister(3173, 0);
            Thread.Sleep(500);
        }

        private void asdasdw()
        {
            modbusClientTCP.WriteSingleRegister(3172, 0);
            Thread.Sleep(1000);
            modbusClientTCP.WriteSingleRegister(3173, 1);
            Thread.Sleep(500);

        }

            private void BtnOff2_Click(object sender, EventArgs e)
        {
            switch (JaulaActiva)
            {
                case 1:
                    switch (indice2)
                    {
                        case 2:
                            modbusClientTCP.WriteSingleRegister(3154, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3155, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V02_Cerrar", 2);
                            break;
                        case 4:
                            modbusClientTCP.WriteSingleRegister(3158, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3159, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V04_Cerrar", 4);
                            break;
                    }
                    break;
                case 2:
                    switch (indice2)
                    {
                        case 2://7
                            modbusClientTCP.WriteSingleRegister(3164, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3165, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V07_Cerrar", 7);
                            break;
                        case 4://9
                            modbusClientTCP.WriteSingleRegister(3168, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3169, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V09_Cerrar", 9);
                            break;
                    }
                    break;
                case 3:
                    switch (indice2)
                    {
                        case 2://12

                            modbusClientTCP.WriteSingleRegister(3174, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3175, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V12_Cerrar", 12);
                            break;
                        case 4://14
                            modbusClientTCP.WriteSingleRegister(3178, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3179, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V14_Cerrar", 14);
                            break;
                    }
                    break;
                case 4:
                    switch (indice2)
                    {
                        case 2://17
                            modbusClientTCP.WriteSingleRegister(3184, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3185, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V17_Cerrar", 17);
                            break;
                        case 4://19
                            modbusClientTCP.WriteSingleRegister(3188, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3189, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V19_Cerrar", 19);
                            break;
                    }
                    break;
                case 5:
                    switch (indice2)
                    {
                        case 2://22
                            modbusClientTCP.WriteSingleRegister(3194, 0);
                            Thread.Sleep(500);
                            modbusClientTCP.WriteSingleRegister(3195, 1);
                            Thread.Sleep(500);
                            GUI_MODERNISTA.conexionDB.set_logs("V22_Cerrar", 22);
                            break;
                    }
                    break;
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
