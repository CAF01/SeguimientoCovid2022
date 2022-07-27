using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ClassEntidades;
using ClassLogicaNegocios;

namespace WebCovid
{
    public partial class WebAlumno : System.Web.UI.Page
    {
        LogicaNegociosAlumno objBLAlumno = null;
        string msj = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                objBLAlumno = new LogicaNegociosAlumno("Data Source=LAPTOP-KVRVONP6; Initial Catalog=SeguimientoCovid; Integrated Security=True;");
                //ConfigurationManager.ConnectionStrings["conexSql"].ConnectionString
                Session["objBLAlumno"] = objBLAlumno;
            }
            else
            {
                objBLAlumno = (LogicaNegociosAlumno)Session["objBLAlumno"];
            }
           
            //ddlEdoCivil
            /*Session["listEdoCivil"] = objBLAlumno.obtenerColeccionEdoCivil(ref msj);
            List<EstadoCivil> listEdoCivil = (List<EstadoCivil>)Session["listEdoCivil"];
            ddlEdoCivil.Items.Clear();
            ddlEdoCivilA.Items.Clear();
            for (int i = 0; i < listEdoCivil.Count; i++)
            {
                if (!ddlEdoCivil.Items.Contains(new ListItem(
                        listEdoCivil[i].programaEd, listEdoCivil[i].id.ToString()
                    )))
                {
                    ddlEdoCivil.Items.Add(new ListItem(
                        listEdoCivil[i].programaEd, listEdoCivil[i].id.ToString()
                    ));
                }

                if (!ddlEdoCivilA.Items.Contains(new ListItem(
                        listEdoCivil[i].programaEd, listEdoCivil[i].id.ToString()
                    )))
                {
                    ddlEdoCivilA.Items.Add(new ListItem(
                        listEdoCivil[i].programaEd, listEdoCivil[i].id.ToString()
                    ));
                }
            }*/

            // cargar gv alumnos (ver)
            gvAlumnos.DataSource = objBLAlumno.consultarAlumnos(ref msj);
            gvAlumnos.DataBind();
            // cargar gv alumnos (editar/eliminar)
            gvAlumnosAE.DataSource = objBLAlumno.consultarAlumnos(ref msj);
            gvAlumnosAE.DataBind();

        }

        protected void btnInsertarAlumno_Click(object sender, EventArgs e)
        {

        }

        protected void btnInsertarAlGruCuatri_Click(object sender, EventArgs e)
        {

        }

        protected void gvAlumnosA_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void gvAlumnosA_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvAlumnosA_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvAlumnosGruCuatriA_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void gvAlumnosGruCuatriA_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvAlumnosGruCuatriA_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}