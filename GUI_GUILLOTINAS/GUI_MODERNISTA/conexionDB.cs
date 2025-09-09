using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_MODERNISTA
{
    class conexionDB
    {
        public static string ReadConnection()
        {
            return "datasource=127.0.0.1 ;username=root;password=;database=db_guillotinas2;";
        }

        public static void set_logs(string accion, int id_g)
        {
            MySqlConnection cnn = new MySqlConnection(ReadConnection());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader leer;
            cmd.Connection = cnn;
            cnn.Open();

            string query2 = "INSERT INTO logs_gui (accion,id_guillotina) VALUES ('" + accion + "', " + id_g + ");";
            cmd.CommandText = query2;
            cmd.ExecuteNonQuery();

        }
        public static System.Data.DataSet get_logs(DateTime fecha1, DateTime fecha2)
        {
           
            MySqlConnection cnn = new MySqlConnection(ReadConnection());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader leer;
            cmd.Connection = cnn;
            cnn.Open();

            string query2 = "SELECT * FROM logs_gui WHERE fecha between '"+fecha1.ToString("yyyy-MM-dd") + "' AND '"+fecha2.ToString("yyyy-MM-dd") + "';";
            cmd.CommandText = query2;
            MySqlDataAdapter m_datos = new MySqlDataAdapter(cmd);
            System.Data.DataSet ds;
            ds = new System.Data.DataSet();
            m_datos.Fill(ds);
            cnn.Close();
            return ds;
            //cmd.ExecuteNonQuery();

        }

        public static int get_pulso()
        {
            
            string query = "SELECT * FROM Pulso;";

            MySqlConnection cnn = new MySqlConnection(ReadConnection());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader leer;
            cmd.Connection = cnn;
            cnn.Open();
            cmd.CommandText = query;
            int pulso =0;
            leer = cmd.ExecuteReader();
            if (leer.HasRows)
            {
                while (leer.Read())
                {
                    pulso = (int)leer.GetValue(0);
                    
                }

            }

            return pulso;
        }
        public static void set_pulso(string pulso)
        {
            int p = Convert.ToInt32(pulso);
            MySqlConnection cnn = new MySqlConnection(ReadConnection());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader leer;
            cmd.Connection = cnn;
            cnn.Open();

            string query2 = "UPDATE pulso set Pulso ="+ p + ";";
            cmd.CommandText = query2;
            cmd.ExecuteNonQuery();

        }
        public static void openDB()
        {
            Process[] localByName = Process.GetProcessesByName("mysqld");
            int f = localByName.Count();
            if (f == 0)
            {
                Process.Start(@"C:\xampp\xampp_start.exe");

            }
        }

    }


}
