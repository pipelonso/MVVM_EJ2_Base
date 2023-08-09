using MVVMClass1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MVVMClass1.View
{
    public partial class Agenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Usuario"].ToString() == "") {

                ScriptManager.RegisterStartupScript(this, GetType(), "HideUser", "document.getElementById('UserBox').style.display = 'none'; hideContent();", true);

            }
            else
            {

                ClUsuarioVM objUsuarioVM = new ClUsuarioVM();
                ClUsuarioEVM objUsuarioEVM = objUsuarioVM.mtdGetUserByMail(Session["Usuario"].ToString());
                lblUserName.Text = objUsuarioEVM.Nombre;
                UserPic.ImageUrl = objUsuarioEVM.Imagen;

                ScriptManager.RegisterStartupScript(this, GetType(), "HideSession", "document.getElementById('SesionControls').style.display = 'none'; hideDelete(); HideEdit();", true);

                if (!IsPostBack)
                {

                    txtFecha.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");



                    ClRecordatorioVM objRecordatorioVM = new ClRecordatorioVM();
                    List<ClRecordatorioEVM> listaRecordatorio = objRecordatorioVM.mtdGetTaskByUserMail(Session["Usuario"].ToString());
                    RpRecordatorio.DataSource = listaRecordatorio;
                    RpRecordatorio.DataBind();
                }


            }


        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Register.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Login.aspx");
        }

        protected void btnEditTask_Click(object sender, EventArgs e)
        {
            ClRecordatorioVM objRecordatorioVM = new ClRecordatorioVM();
            ClRecordatorioEVM objRecordatorioEVM = new ClRecordatorioEVM();
            objRecordatorioEVM.Recordatorio = txtNotaEdit.Text;

            string textofecha = txtFechaEdit.Text;

            if (textofecha == "")
            {
                textofecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            }
            DateTime Fechaformat;
            DateTime.TryParseExact(textofecha, "yyyy-MM-ddTHH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out Fechaformat);

            string newfecha = Fechaformat.ToString("yyyy-MM-dd HH:mm:ss");

            objRecordatorioEVM.Fecha = newfecha;

            int res = objRecordatorioVM.mtdEditTaskWithId(int.Parse(lblEditId.Text), objRecordatorioEVM);

            if (res == 1)
            {
                List<ClRecordatorioEVM> listaRecordatorio = objRecordatorioVM.mtdGetTaskByUserMail(Session["Usuario"].ToString());
                RpRecordatorio.DataSource = listaRecordatorio;
                RpRecordatorio.DataBind();
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {

            ClRecordatorioVM objRecordatorio = new ClRecordatorioVM();
            int res = objRecordatorio.mtdDeleteWithId(int.Parse(lblDeleteId.Text));
            if (res == 1)
            {
                List<ClRecordatorioEVM> listaRecordatorio = objRecordatorio.mtdGetTaskByUserMail(Session["Usuario"].ToString());
                RpRecordatorio.DataSource = listaRecordatorio;
                RpRecordatorio.DataBind();
            }



        }

        protected void lkbEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            Label lblIdRecordatorio = (Label)item.FindControl("lblidRecordatorioRP");
            Label txtRecordatorio = (Label)item.FindControl("lblRecordatorioRP");

            Label txtFecha = (Label)item.FindControl("lblFechaRP");

            txtFechaEdit.Text = DateTime.Parse(txtFecha.Text).ToString("yyyy-MM-ddTHH:mm");

            txtNotaEdit.Text = txtRecordatorio.Text;

            int idRecordatorio = Convert.ToInt32(lblIdRecordatorio.Text);
            lblEditId.Text = idRecordatorio.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "showEdit", "ShowEdit();", true);
        }

        protected void lkbDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            Label lblIdRecordatorio = (Label)item.FindControl("lblidRecordatorioRP");
            int idRecordatorio = Convert.ToInt32(lblIdRecordatorio.Text);
            lblDeleteId.Text = idRecordatorio.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "showDelete", "showDelete();", true);
        }

        protected void btnAddTask_Click(object sender, EventArgs e)
        {
            ClRecordatorioVM objRecordatorioVM = new ClRecordatorioVM();
            ClRecordatorioEVM objRecordatorioEVM = new ClRecordatorioEVM();
            objRecordatorioEVM.Recordatorio = txtNota.Text;

            string textofecha = txtFecha.Text;

            if (textofecha == "")
            {
                textofecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            }

            DateTime FechaFormat;
            DateTime.TryParseExact(textofecha, "yyy-MM-ddTHH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out FechaFormat);
            string newfecha = FechaFormat.ToString("yyy-MM-dd HH:mm:ss");

            objRecordatorioEVM.Fecha = newfecha;

            int res = objRecordatorioVM.mtdAddTaskByMail(Session["Usuario"].ToString(), objRecordatorioEVM);

            if (res== 1)
            {
                List<ClRecordatorioEVM> listaRecordatorio = objRecordatorioVM.mtdGetTaskByUserMail(Session["Usuario"].ToString());
                RpRecordatorio.DataSource = listaRecordatorio;
                RpRecordatorio.DataBind();
            }
        }
    }
}