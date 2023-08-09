using MVVMClass1.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MVVMClass1.ViewModel
{
    public class ClRecordatorioVM
    {

        public List<ClRecordatorioEVM> mtdGetTaskByUserMail(String correo)
        {
            RecordatorioM objRecordatorioM = new RecordatorioM();
            List<ClRecordatorioEM> ListaRecordatorioEM = objRecordatorioM.GetAllTaskByUserMail(correo);
            List<ClRecordatorioEVM> ListaRecordatorioEVM = new List<ClRecordatorioEVM>();

            for (int i = 0; i < ListaRecordatorioEM.Count; i++)
            {
                ClRecordatorioEVM objRecordatorioEVM = new ClRecordatorioEVM();
                objRecordatorioEVM.IdRecordatorio = ListaRecordatorioEM[i].IdRecordatorio;
                objRecordatorioEVM.Recordatorio = ListaRecordatorioEM[i].Recordatorio;
                objRecordatorioEVM.Fecha = ListaRecordatorioEM[i].Fecha;
                objRecordatorioEVM.Llamado = ListaRecordatorioEM[i].Llamado;

                ListaRecordatorioEVM.Add(objRecordatorioEVM);
            }
            return ListaRecordatorioEVM;
        }

        public List<ClRecordatorioEVM> mtdGetAllTaskByMailNOENTITY(string correo)
        {
            RecordatorioM objRecordatorio = new RecordatorioM();
            List<List<object>> ListaRecordatorio = objRecordatorio.mtdGetTaskByMailNOENTITY(correo);

            List<ClRecordatorioEVM> ListaRecordatorioEVM = new List<ClRecordatorioEVM>();

            for (int i = 0; i < ListaRecordatorio.Count; i++)
            {
                ClRecordatorioEVM objRecordatorioEVM = new ClRecordatorioEVM();
                objRecordatorioEVM.IdRecordatorio = int.Parse(ListaRecordatorio[i][0].ToString());
                objRecordatorioEVM.Recordatorio = ListaRecordatorio[i][1].ToString();
                objRecordatorioEVM.Fecha = ListaRecordatorio[i][2].ToString();
                objRecordatorioEVM.Llamado = int.Parse(ListaRecordatorio[i][3].ToString());

                ListaRecordatorioEVM.Add(objRecordatorioEVM);
            }
            return ListaRecordatorioEVM;
        }

        public int mtdAddTaskByMail(string correo , ClRecordatorioEVM objRecordatorioEVM)
        {
            RecordatorioM objRecordatorioM = new RecordatorioM();
            ClRecordatorioEM objRecordatorioEM = new ClRecordatorioEM();
            objRecordatorioEM.Recordatorio = objRecordatorioEVM.Recordatorio;
            objRecordatorioEM.Fecha = objRecordatorioEVM.Fecha;

            int res = objRecordatorioM.mtdAddTaskWithMail(correo, objRecordatorioEM);
            return res;
        }

        public int mtdEditTaskWithId (int id , ClRecordatorioEVM objRecordatorioEVM)
        {

            ClRecordatorioEM objRecordatorioEM = new ClRecordatorioEM();
            objRecordatorioEM.Recordatorio = objRecordatorioEVM.Recordatorio;
            objRecordatorioEM.Fecha = objRecordatorioEVM.Fecha;

            RecordatorioM objRecordatorioM = new RecordatorioM();
            int res = objRecordatorioM.mtdEditTaskWithid(id, objRecordatorioEM);
            return res;
        }

        public int mtdDeleteWithId(int id)
        {
            RecordatorioM objRecordatorioM = new RecordatorioM();
            int res = objRecordatorioM.mtdDeleteTaskWithId(id);
            return res;
        }

        public void mtdGetExpired(string correo)
        {

            string proc = "GetExpired '" + correo + "'";
            ClProcesos objSQL = new ClProcesos();
            DataTable datos = objSQL.mtdConsultas(proc);

            List<ClRecordatorioEM> listaRedcordatorio = new List<ClRecordatorioEM>();
            for (int i = 0; i < datos.Rows.Count; i++)
            {

                ClRecordatorioEM recordatorio = new ClRecordatorioEM();
                recordatorio.IdRecordatorio = int.Parse(datos.Rows[i][]


            }
        }
    }
}





