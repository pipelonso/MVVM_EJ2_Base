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



        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {




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
    }
}