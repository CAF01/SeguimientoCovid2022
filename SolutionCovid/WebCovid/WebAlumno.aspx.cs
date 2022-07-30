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
    public partial class WebAlumno : System.Web.UI.Page
    {
        LogicaNegociosAlumno objBLAlumno = null;
        List<EstadoCivil> listEdoCivil;
        List<Alumno> listAlumno;
        List<FiltroGrupoCuatri> listGruCuat;
        string msj = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                objBLAlumno = new LogicaNegociosAlumno(ConfigurationManager.ConnectionStrings["conexSql"].ConnectionString);
                //objBLAlumno = new LogicaNegociosAlumno("Data Source=LAPTOP-KVRVONP6; Initial Catalog=SeguimientoCovid; Integrated Security=True;");
                Session["objBLAlumno"] = objBLAlumno;
                listEdoCivil = objBLAlumno.obtenerColeccionEstadoCivil();
                Session["listEdoCivil"] = listEdoCivil;
                listAlumno = objBLAlumno.obtenerColeccionAlumnos(ref msj);
                Session["listAlumno"] = listAlumno;
                listGruCuat = objBLAlumno.obtenerColeccionGrupoCuatri(ref msj);
                Session["listGruCuat"] = listGruCuat;

                if (objBLAlumno != null && listEdoCivil.Count >= 1 && listAlumno.Count >= 1 && listGruCuat.Count >= 1)
                {
                    EstablecerListEstadoCivil();
                    generarListAlumno();
                    generarListGruCuat();
                }
            }
            else
            {
                objBLAlumno = (LogicaNegociosAlumno)Session["objBLAlumno"];
                listEdoCivil = (List<EstadoCivil>)Session["listEdoCivil"];
                listAlumno= (List<Alumno>)Session["listAlumno"];
                listGruCuat = (List<FiltroGrupoCuatri>)Session["listGruCuat"];
            }                       

            // cargar gv alumnos (ver)
            gvAlumnos.DataSource = objBLAlumno.consultarAlumnos(ref msj);
            gvAlumnos.DataBind();
            // cargar gv alumnos (editar/eliminar)
            gvAlumnosA.DataSource = objBLAlumno.consultarAlumnos(ref msj);
            gvAlumnosA.DataBind();
            // cargar gv alumno_grupocuatri (ver)
            gvAlumnosGruCuatri.DataSource = objBLAlumno.consultarAlumnoGrupo(ref msj);
            gvAlumnosGruCuatri.DataBind();
            // cargar gv alumno_grupocuatri (ver)
            gvAlumnosGruCuatriA.DataSource = objBLAlumno.consultarAlumnoGrupo(ref msj);
            gvAlumnosGruCuatriA.DataBind();
        }

        //-----------------------------------------Alumno----------------------------------------------------
        protected void btnInsertarAlumno_Click(object sender, EventArgs e)
        {           
            Alumno nuevo = null;
            string msj = "";
            if (!string.IsNullOrEmpty(txtMatricula.Text) && !string.IsNullOrEmpty(txtNombre.Text)
                && !string.IsNullOrEmpty(txtApp.Text) && !string.IsNullOrEmpty(txtApm.Text)
                && (ddlGenero.SelectedIndex >= 0) && !string.IsNullOrEmpty(txtCelular.Text)
                && (ddlEdoCivil.SelectedIndex >= 0))
            {
                nuevo = new Alumno()
                {
                    matricula = txtMatricula.Text,
                    nombre = txtNombre.Text,
                    apPat=txtApp.Text,
                    apMat=txtApm.Text,
                    genero=ddlGenero.SelectedValue,
                    correo=txtCorreo.Text,
                    celular=txtCelular.Text,
                    f_edoCivil= Convert.ToInt32(ddlEdoCivil.SelectedValue)
                };
                bool result = objBLAlumno.insertarAlumno(nuevo, ref msj);
                if(result)
                {
                    // msj  añadido
                    alert("Hecho", "Registro añadido", 2);
                    txtMatricula.Text = "";
                    txtNombre.Text = "";
                    txtApp.Text = "";
                    txtApm.Text = "";
                    ddlGenero.SelectedValue = null;
                    txtCorreo.Text = "";
                    txtCelular.Text = "";
                    ddlEdoCivil.SelectedValue = null;
                    // actualizar gv 
                    gvAlumnos.DataSource = objBLAlumno.consultarAlumnos(ref msj);
                    gvAlumnos.DataBind();
                    gvAlumnosA.DataSource = objBLAlumno.consultarAlumnos(ref msj);
                    gvAlumnosA.DataBind();
                    // actualizar ddl
                    listAlumno = objBLAlumno.obtenerColeccionAlumnos(ref msj);
                    Session["listAlumno"] = listAlumno;
                    generarListAlumno();
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

        protected void btnInsertarAlGruCuatri_Click(object sender, EventArgs e)
        {
            AlumnoGrupo nuevo = null;
            string msj = "";
            if ((ddlAlumno.SelectedIndex >= 0) && (ddlGrupoCuatri.SelectedIndex >= 0))
            {
                nuevo = new AlumnoGrupo()
                {
                    f_alumno=Convert.ToInt32(ddlAlumno.SelectedValue),
                    f_grupoCuatri= Convert.ToInt32(ddlGrupoCuatri.SelectedValue),
                    extra=txtExtra1.Text,
                    extra2=txtExtra2.Text
                };
                bool result = objBLAlumno.insertarAlumnoGrupo(nuevo, ref msj);
                if(result)
                {
                    // msj añadido
                    alert("Hecho", "Registro añadido", 2);
                    ddlAlumno.SelectedValue = null;
                    ddlGrupoCuatri.SelectedValue = null;
                    txtExtra1.Text = "";
                    txtExtra2.Text = "";
                    // actualizar gv 
                    gvAlumnosGruCuatri.DataSource = objBLAlumno.consultarAlumnoGrupo(ref msj);
                    gvAlumnosGruCuatri.DataBind();
                    gvAlumnosGruCuatriA.DataSource = objBLAlumno.consultarAlumnoGrupo(ref msj);
                    gvAlumnosGruCuatriA.DataBind();
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

        // mostrar datos en controles de formulario
        protected void gvAlumnosA_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Alumno recuperado = null;
            string msj = "";
            if (e.NewSelectedIndex >= 0) // se seleccionó registro
            {
                recuperado = objBLAlumno.buscarAlumno(Convert.ToInt32(gvAlumnosA.Rows[e.NewSelectedIndex].Cells[3].Text), ref msj);
                txtMatriculaA.Text = recuperado.matricula;
                txtNombreA.Text = recuperado.nombre;
                txtAppA.Text = recuperado.apPat;
                txtApmA.Text = recuperado.apMat;
                ddlGeneroA.SelectedValue = recuperado.genero;
                txtCorreoA.Text = recuperado.correo;
                txtCelularA.Text = recuperado.celular;
                ddlEdoCivilA.SelectedValue = recuperado.f_edoCivil.ToString();
                // msj recuperado
                alert("Hecho", "Datos recuperados", 2);
            }
            else
            {
                //msj Error: Selecciona un registro
                alert("Error", "Selecciona un registro", 3);
            }
        }

        // editar
        protected void gvAlumnosA_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Alumno editado = null;
            string msj = "";
            int idAlumno = 0;
            if (!string.IsNullOrEmpty(txtMatriculaA.Text) && !string.IsNullOrEmpty(txtNombreA.Text)
                && !string.IsNullOrEmpty(txtAppA.Text) && !string.IsNullOrEmpty(txtApmA.Text)
                && (ddlGeneroA.SelectedIndex >= 0) && !string.IsNullOrEmpty(txtCelularA.Text)
                && (ddlEdoCivilA.SelectedIndex >= 0))
            {
                editado = new Alumno()
                {
                    matricula = txtMatriculaA.Text,
                    nombre = txtNombreA.Text,
                    apPat = txtAppA.Text,
                    apMat = txtApmA.Text,
                    genero = ddlGeneroA.SelectedValue,
                    correo = txtCorreoA.Text,
                    celular = txtCelularA.Text,
                    f_edoCivil = Convert.ToInt32(ddlEdoCivilA.SelectedValue)
                };

                if (gvAlumnosA.SelectedIndex >= 0) // se seleccionó registro
                {
                    idAlumno = Convert.ToInt32(gvAlumnosA.Rows[gvAlumnosA.SelectedIndex].Cells[3].Text);
                    bool result = objBLAlumno.editarAlumno(idAlumno, editado, ref msj);
                    if(result)
                    {
                        // msj Registro actualizado
                        alert("Hecho", "Registro actualizado", 2);
                        txtMatriculaA.Text = "";
                        txtNombreA.Text = "";
                        txtAppA.Text = "";
                        txtApmA.Text = "";
                        ddlGeneroA.SelectedValue = null;
                        txtCorreoA.Text = "";
                        txtCelularA.Text = "";
                        ddlEdoCivilA.SelectedValue = null;
                        // actualizar gv 
                        gvAlumnos.DataSource = objBLAlumno.consultarAlumnos(ref msj);
                        gvAlumnos.DataBind();
                        gvAlumnosA.DataSource = objBLAlumno.consultarAlumnos(ref msj);
                        gvAlumnosA.DataBind();
                        // actualizar ddl
                        listAlumno = objBLAlumno.obtenerColeccionAlumnos(ref msj);
                        Session["listAlumno"] = listAlumno;
                        generarListAlumno();
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

        // eliminar
        protected void gvAlumnosA_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string msj = "";
            if (e.RowIndex >= 0) // se seleccionó registro
            {
                bool result = objBLAlumno.eliminarAlumno(Convert.ToInt32(gvAlumnosA.Rows[e.RowIndex].Cells[3].Text), ref msj);
                if (result)
                {
                    // msj eliminado
                    alert("Hecho", "Registro eliminado", 2);
                    // actualizar gv
                    gvAlumnos.DataSource = objBLAlumno.consultarAlumnos(ref msj);
                    gvAlumnos.DataBind();
                    gvAlumnosA.DataSource = objBLAlumno.consultarAlumnos(ref msj);
                    gvAlumnosA.DataBind();
                    // actualizar ddl
                    listAlumno = objBLAlumno.obtenerColeccionAlumnos(ref msj);
                    Session["listAlumno"] = listAlumno;
                    generarListAlumno();
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

        //-------------------------------------------AlumnoGrupo---------------------------------------------

        // mostrar datos en controles de formulario
        protected void gvAlumnosGruCuatriA_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            AlumnoGrupo recuperado = null;
            string msj = "";
            if (e.NewSelectedIndex >= 0) // se seleccionó registro
            {
                recuperado = objBLAlumno.buscarAlumnoGrupo(Convert.ToInt32(gvAlumnosGruCuatriA.Rows[e.NewSelectedIndex].Cells[3].Text), ref msj);
                ddlAlumnoA.SelectedValue = recuperado.f_alumno.ToString();
                ddlGrupoCuatriA.SelectedValue = recuperado.f_grupoCuatri.ToString();
                txtExtra1A.Text = recuperado.extra;
                txtExtra2A.Text = recuperado.extra2;
                // msj recuperado
                alert("Hecho", "Datos recuperados", 2);
            }
            else
            {
                //msj Error: Selecciona un registro
                alert("Error", "Selecciona un registro", 3);
            }
        }

        protected void gvAlumnosGruCuatriA_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            AlumnoGrupo editado = null;
            string msj = "";
            int idAlumnoGrupo = 0;
            if ((ddlAlumnoA.SelectedIndex >= 0) && (ddlGrupoCuatriA.SelectedIndex >= 0))
            {
                editado = new AlumnoGrupo()
                {
                    f_alumno = Convert.ToInt32(ddlAlumnoA.SelectedValue),
                    f_grupoCuatri = Convert.ToInt32(ddlGrupoCuatriA.SelectedValue),
                    extra = txtExtra1A.Text,
                    extra2 = txtExtra2A.Text
                };

                if (gvAlumnosGruCuatriA.SelectedIndex >= 0) // se seleccionó registro
                {
                    idAlumnoGrupo = Convert.ToInt32(gvAlumnosGruCuatriA.Rows[gvAlumnosGruCuatriA.SelectedIndex].Cells[3].Text);
                    bool result = objBLAlumno.editarAlumnoGrupo(idAlumnoGrupo, editado, ref msj);
                    if(result)
                    {
                        // msj Registro actualizado
                        alert("Hecho", "Registro actualizado", 2);
                        ddlAlumnoA.SelectedValue = null;
                        ddlGrupoCuatriA.SelectedValue = null;
                        txtExtra1A.Text = "";
                        txtExtra2A.Text = "";
                        // actualizar gv
                        gvAlumnosGruCuatri.DataSource = objBLAlumno.consultarAlumnoGrupo(ref msj);
                        gvAlumnosGruCuatri.DataBind();
                        gvAlumnosGruCuatriA.DataSource = objBLAlumno.consultarAlumnoGrupo(ref msj);
                        gvAlumnosGruCuatriA.DataBind();
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

        protected void gvAlumnosGruCuatriA_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string msj = "";
            if (e.RowIndex >= 0) // se seleccionó registro
            {
                bool result = objBLAlumno.eliminarAlumnoGrupo(Convert.ToInt32(gvAlumnosGruCuatriA.Rows[e.RowIndex].Cells[3].Text), ref msj);
                if (result)
                {
                    // msj eliminado
                    alert("Hecho", "Registro eliminado", 2);
                    // actualizar gv alumnos_grupo-cuatri
                    gvAlumnosGruCuatri.DataSource = objBLAlumno.consultarAlumnoGrupo(ref msj);
                    gvAlumnosGruCuatri.DataBind();
                    gvAlumnosGruCuatriA.DataSource = objBLAlumno.consultarAlumnoGrupo(ref msj);
                    gvAlumnosGruCuatriA.DataBind();
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

        protected void ddlEdoCivil_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        //--------------------------------- Métodos para llenar ddl---------------------------------------------
        public void EstablecerListEstadoCivil()
        {
            ddlEdoCivil.Items.Clear();
            ddlEdoCivilA.Items.Clear();
            ListItem listItem;
            foreach (EstadoCivil item in listEdoCivil)
            {
                listItem = new ListItem();
                listItem.Text = item.Estado;
                listItem.Value = item.Id_Edo.ToString();
                ddlEdoCivil.Items.Add(listItem);
                ddlEdoCivilA.Items.Add(listItem);
            }
        }
        public void generarListAlumno()
        {
            ddlAlumno.Items.Clear();
            ddlAlumnoA.Items.Clear();
            ListItem listItem;
            foreach (Alumno item in listAlumno)
            {
                listItem = new ListItem();
                listItem.Text = item.matricula + " " + item.nombre + " " + item.apPat + " " + item.apMat;
                listItem.Value = item.id.ToString();
                ddlAlumno.Items.Add(listItem);
                ddlAlumnoA.Items.Add(listItem);
            }
        }
        public void generarListGruCuat()
        {
            ddlGrupoCuatri.Items.Clear();
            ddlGrupoCuatriA.Items.Clear();
            ListItem listItem;
            foreach (FiltroGrupoCuatri item in listGruCuat)
            {
                listItem = new ListItem();
                listItem.Text = item.progEd + " " + item.grupo + " " + item.periodo + " " + item.anio;
                listItem.Value = item.id.ToString();
                ddlGrupoCuatri.Items.Add(listItem);
                ddlGrupoCuatriA.Items.Add(listItem);
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