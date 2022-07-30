using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using ClassEntidades;
using ClassLogicaNegocios;

namespace WebCovid
{
    public partial class WebPositivoAlumno : System.Web.UI.Page
    {
        LogicaNegociosAlumno objBLAlumno = null;
        List<Alumno> listAlumno;
        string msj = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                objBLAlumno = new LogicaNegociosAlumno(ConfigurationManager.ConnectionStrings["conexSql"].ConnectionString);            
                Session["objBLAlumno"] = objBLAlumno;
                listAlumno = objBLAlumno.obtenerColeccionAlumnos(ref msj);
                Session["listAlumno"] = listAlumno;

                if (objBLAlumno != null && listAlumno.Count >= 1)
                {
                    generarListAlumno();
                }
            }
            else
            {
                objBLAlumno = (LogicaNegociosAlumno)Session["objBLAlumno"];
                listAlumno = (List<Alumno>)Session["listAlumno"];
            }

            DataSet dsPosAl = objBLAlumno.consultarPositivosAlumno(ref msj);
            if (dsPosAl.Tables[0].Rows.Count > 0)
            {
                gvPositivosAlumnos.DataSource = dsPosAl;
                gvPositivosAlumnos.DataBind();
            }
            else
            {
                alert("Aviso", "Aún no existen registros", 1);
            }          
        }

        protected void btnInsertarPositivo_Click(object sender, EventArgs e)
        {
            PositivoAlumno nuevo = null;
            string msj = "";
            //entrevista y extra
            if (!string.IsNullOrEmpty(txtConfirmacion.Text) && !string.IsNullOrEmpty(txtRiesgo.Text)
                && !string.IsNullOrEmpty(txtNumContagio.Text) && (ddlAlumno.SelectedIndex >= 0))
            {
                nuevo = new PositivoAlumno()
                {
                    f_alumno=Convert.ToInt32(ddlAlumno.SelectedValue),
                    fechaConfirmado=calFechaConfirm.SelectedDate,
                    comprobacion=txtConfirmacion.Text,
                    antecedentes=txtAnteced.Text,
                    riesgo=txtRiesgo.Text,
                    numContagio=Convert.ToInt32(txtNumContagio.Text),
                    extra = txtExtra.Text
                };
                bool result = objBLAlumno.insertarPositivoAlumno(nuevo, ref msj);
                if (result)
                {
                    // msj añadido
                    alert("Hecho", "Registro añadido", 2);
                    ddlAlumno.SelectedValue = null;
                    txtConfirmacion.Text = "";
                    txtAnteced.Text = "";
                    txtRiesgo.Text = "";
                    txtNumContagio.Text = "";
                    txtExtra.Text = "";
                    calFechaConfirm.SelectedDate = DateTime.MinValue;
                    // actualizar gv
                    gvPositivosAlumnos.DataSource = objBLAlumno.consultarPositivosAlumno(ref msj);
                    gvPositivosAlumnos.DataBind();
                }
                else
                {
                    alert("Error", "No se pudo añadir el registro", 3);
                }     
            }
            else
            {
                // msj Error: Revisa que no haya campos vacíos y vuelve a intentar
                alert("Error", "Revisa que no haya campos vacíos y vuelve a intentar", 3);
            }
        }

        protected void gvPositivosAlumnos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            PositivoAlumno recuperado = null;
            string msj = "";
            if (e.NewSelectedIndex >= 0) // se seleccionó registro
            {
                recuperado = objBLAlumno.buscarPositivoAlumno(Convert.ToInt32(gvPositivosAlumnos.Rows[e.NewSelectedIndex].Cells[3].Text), ref msj);
                ddlAlumno.SelectedValue = recuperado.f_alumno.ToString();
                calFechaConfirm.SelectedDate = recuperado.fechaConfirmado;
                txtConfirmacion.Text = recuperado.comprobacion;
                txtAnteced.Text = recuperado.antecedentes;
                txtRiesgo.Text = recuperado.riesgo;
                txtNumContagio.Text = recuperado.numContagio.ToString();
                txtExtra.Text = recuperado.extra;
                // msj recuperado
                alert("Hecho", "Datos recuperados", 2);
            }
            else
            {
                //msj Error: Selecciona un registro
                alert("Error", "Selecciona un registro", 3);
            }
        }

        protected void gvPositivosAlumnos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            PositivoAlumno editado = null;
            string msj = "";
            int idPositivoAlumno = 0;
            if (!string.IsNullOrEmpty(txtConfirmacion.Text) && !string.IsNullOrEmpty(txtRiesgo.Text)
                && !string.IsNullOrEmpty(txtNumContagio.Text) && (calFechaConfirm.SelectedDate != null)
                && (ddlAlumno.SelectedIndex >= 0))
            {
                editado = new PositivoAlumno()
                {
                    f_alumno = Convert.ToInt32(ddlAlumno.SelectedValue),
                    fechaConfirmado = calFechaConfirm.SelectedDate,
                    comprobacion = txtConfirmacion.Text,
                    antecedentes = txtAnteced.Text,
                    riesgo = txtRiesgo.Text,
                    numContagio = Convert.ToInt32(txtNumContagio.Text),
                    extra = txtExtra.Text
                };

                if (gvPositivosAlumnos.SelectedIndex >= 0) // se seleccionó registro
                {
                    idPositivoAlumno = Convert.ToInt32(gvPositivosAlumnos.Rows[gvPositivosAlumnos.SelectedIndex].Cells[3].Text);
                    bool result = objBLAlumno.editarPositivoAlumno(idPositivoAlumno, editado, ref msj);
                    if (result)
                    {
                        // msj Registro actualizado
                        alert("Hecho", "Registro actualizado", 2);
                        ddlAlumno.SelectedValue = null;
                        txtConfirmacion.Text = "";
                        txtAnteced.Text = "";
                        txtRiesgo.Text = "";
                        txtNumContagio.Text = "";
                        txtExtra.Text = "";
                        calFechaConfirm.SelectedDate = DateTime.MinValue;
                        // actualizar gv
                        gvPositivosAlumnos.DataSource = objBLAlumno.consultarPositivosAlumno(ref msj);
                        gvPositivosAlumnos.DataBind();
                    }
                    else
                    {
                        alert("Error", "No se pudo actualizar el registro", 3);
                    }          
                }
                else
                {
                    // msj Error: Selecciona un registro
                    alert("Error", "Selecciona un registro", 3);
                }
            }
            else
            {
                // msj Error: Revisa que no haya campos vacíos y vuelve a intentar
                alert("Error", "Revisa que no haya campos vacíos y vuelve a intentar", 3);
            }            
        }

        protected void gvPositivosAlumnos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string msj = "";
            if (e.RowIndex >= 0) // se seleccionó registro
            {
                bool result = objBLAlumno.eliminarPositivoAlumno(Convert.ToInt32(gvPositivosAlumnos.Rows[e.RowIndex].Cells[3].Text), ref msj);
                if (result)
                {
                    // msj eliminado
                    alert("Hecho", "Registro eliminado", 2);
                    // actualizar gv
                    gvPositivosAlumnos.DataSource = objBLAlumno.consultarPositivosAlumno(ref msj);
                    gvPositivosAlumnos.DataBind();
                }
                else
                {
                    alert("Error", "No se pudo eliminar registro", 3);
                }                
            }
            else
            {
                //msj Error: Selecciona un registro
                alert("Error", "Selecciona un registro", 3);
            }           
        }

        //--------------------------------- Métodos para llenar ddl---------------------------------------------
        public void generarListAlumno()
        {
            ddlAlumno.Items.Clear();
            ListItem listItem;
            foreach (Alumno item in listAlumno)
            {
                listItem = new ListItem();
                listItem.Text = item.matricula + " " + item.nombre + " " + item.apPat + " " + item.apMat;
                listItem.Value = item.id.ToString();
                ddlAlumno.Items.Add(listItem);
            }
        }

        //------------------------------------------------Alert-------------------------------------------------
        public void alert(string titulo, string msj, short tipo)
        {
            string icono = "";
            switch (tipo)
            {
                case 1:
                    icono = "'info'";
                    break;
                case 2:
                    icono = "'success'";
                    break;
                case 3:
                    icono = "'error'";
                    break;
                case 4:
                    icono = "'question'";
                    break;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(),
                "clav" + tipo, "Alert('" + titulo + "','" + msj + "'," + icono + ");", true);
        }
    }
}