using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MVVMClass1.Model
{
    public class RecordatorioM
    {

        public List<ClRecordatorioEM> GetAllTaskByUserMail(string correo)
        {
            ClProcesos objSQL = new ClProcesos();
            string procediemiento = "getTaskBymail '" + correo + "'";
            DataTable datos = objSQL.mtdConsultas(procediemiento);
            List<ClRecordatorioEM> listaRecordatorios = new List<ClRecordatorioEM>();


            for (int i = 0; i < datos.Rows.Count; i++)
            {
                ClRecordatorioEM objRecordatorio = new ClRecordatorioEM();
                objRecordatorio.IdRecordatorio = int.Parse(datos.Rows[i]["IdRecordatorio"].ToString());
                objRecordatorio.Recordatorio = datos.Rows[i]["Recordatorio"].ToString();
                objRecordatorio.Fecha = datos.Rows[i]["Fecha"].ToString();
                objRecordatorio.Llamado = int.Parse(datos.Rows[i]["Llamado"].ToString());
                listaRecordatorios.Add(objRecordatorio);

            }
            return listaRecordatorios;

        }

        public List<List<object>> mtdGetTaskByMailNOENTITY(string correo)
        {
            ClProcesos objSQL = new ClProcesos();
            string procediemiento = "getTaskBymail '" + correo + "'";
            DataTable datos = objSQL.mtdConsultas(procediemiento);

            List<List<object>> listaRecordatorio = new List<List<object>>();
            for (int i = 0; i < datos.Rows.Count; i++)
            {

                List<object> Recordatorio = new List<object>();
                Recordatorio.Add(int.Parse(datos.Rows[i]["IdRecordatorio"].ToString()));
                Recordatorio.Add(datos.Rows[i]["Recordatorio"].ToString());
                Recordatorio.Add(datos.Rows[i]["Fecha"].ToString());
                Recordatorio.Add(int.Parse(datos.Rows[i]["Llamado"].ToString()));
                listaRecordatorio.Add(Recordatorio);

            }
            return listaRecordatorio;
        }


        public int mtdAddTaskWithMail(string correo, ClRecordatorioEM clRecordatorioEM)
        {

            ClProcesos objSQL = new ClProcesos();
            string insert = "InsertTasks'" + correo + "' , '" + clRecordatorioEM.Recordatorio + "' , '" + clRecordatorioEM.Fecha + ".000" + "'";
            int res = objSQL.mtdComandos(insert);
            return res;


        }

        public int mtdEditTaskWithid(int id, ClRecordatorioEM objRecordatorioEM)
        {


            ClProcesos objSQL = new ClProcesos();
            string edit = "EditTaskWithId " + id + " , '" + objRecordatorioEM.Recordatorio + "' , '" + objRecordatorioEM.Fecha + ".000" + "'";
            int res = objSQL.mtdComandos(edit);
            return res;
        }
         
        public int mtdDeleteTaskWithId(int id)
        {

            ClProcesos objSQL = new ClProcesos();
            string delete = "DeleteTaskWithId" + id;
            int res = objSQL.mtdComandos(delete);
            return res;

        }
    }
}

