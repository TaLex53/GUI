using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_MODERNISTA
{
    public class Registros
    {
        public static List<modelo_register> Lregistro = new List<modelo_register>();
        public static List<modelo_register> fill_regsiter()
        {
            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X0", vname = "V01_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X1", vname = "V01_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X2", vname = "V02_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X3", vname = "V02_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X4", vname = "V03_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X5", vname = "V03_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X6", vname = "V04_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X7", vname = "V04_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X8", vname = "V05_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X9", vname = "V05_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X10", vname = "V06_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X11", vname = "V06_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X12", vname = "V07_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X13", vname = "V07_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X14", vname = "V08_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3001, xbit = "X15", vname = "V08_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X0", vname = "V09_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X1", vname = "V09_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X2", vname = "V10_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X3", vname = "V10_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X4", vname = "V11_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X5", vname = "V11_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X6", vname = "V12_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X7", vname = "V12_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X8", vname = "V13_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X9", vname = "V13_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X10", vname = "V14_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X11", vname = "V14_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X12", vname = "V15_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X13", vname = "V15_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X14", vname = "V16_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3002, xbit = "X15", vname = "V16_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X0", vname = "V17_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X1", vname = "V17_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X2", vname = "V18_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X3", vname = "V18_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X4", vname = "V19_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X5", vname = "V19_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X6", vname = "V20_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X7", vname = "V20_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X8", vname = "V21_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X9", vname = "V21_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X10", vname = "V22_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X11", vname = "V22_Cerrar" });

            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X12", vname = "V23_Abrir" });
            Lregistro.Add(new modelo_register() { id = 3003, xbit = "X13", vname = "V23_Cerrar" });

            return Lregistro;

            // Lregistro = aux(Lregistro, 3003,33);
        }

    }

    public struct modelo_register
    {
        public int id { get; set; }
        public string xbit { get; set; }
        public string vname { get; set; }

    }

}
