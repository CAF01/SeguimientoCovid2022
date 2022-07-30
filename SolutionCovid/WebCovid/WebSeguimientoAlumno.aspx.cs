using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using ClassEntidades;
using ClassLogicaNegocios;

namespace WebCovid
{
    public partial class WebSeguimientoAlumno : System.Web.UI.Page
    {
        LogicaNegociosAlumno objBLAlumno = null;
        LogicaNegociosCuatrimestre objBLCuatrimestre = null;
        List<FiltroPositivoAlumno> listPosAl;
        List<Medico> listMedico;
        List<ProgramaEducativo> listProgEd;
        List<Cuatrimestre> listCuatri;
        List<Grupo> listGrupo;
        List<Alumno> listAlumno;
        string msj = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // ConnectionString Server = Server=20.187.117.181,1434;Database=SeguimientoCovidServer;User ID=sa;Password=admin1234;
                objBLAlumno = new LogicaNegociosAlumno(ConfigurationManager.ConnectionStrings["conexSql"].ConnectionString);
                //objBLAlumno = new LogicaNegociosAlumno(ConfigurationManager.ConnectionStrings["conexSql"].ConnectionString);
                Session["objBLAlumno"] = objBLAlumno;
                objBLCuatrimestre = new LogicaNegociosCuatrimestre(ConfigurationManager.ConnectionStrings["conexSql"].ConnectionString);
                //objBLCuatrimestre = new LogicaNegociosCuatrimestre(ConfigurationManager.ConnectionStrings["conexSql"].ConnectionString);
                Session["objBLCuatrimestre"] = objBLCuatrimestre;
                listPosAl = objBLAlumno.obtenerColeccionPositivoAlumno(ref msj);
                Session["listPosAl"] = listPosAl;
                listMedico = objBLAlumno.obtenerListaMedicos(ref msj);
                Session["listMedico"] = listMedico;
                listProgEd = objBLCuatrimestre.obtenerColeccionProgrEd(ref msj);
                Session["listProgEd"] = listProgEd;
                listGrupo = objBLCuatrimestre.obtenerColeccionGrupo(ref msj);
                Session["listGrupo"] = listGrupo;
                listCuatri = objBLCuatrimestre.obtenerListaCuatrimestres(ref msj);
                Session["listCuatri"] = listCuatri;
                listAlumno = objBLAlumno.obtenerColeccionAlumnos(ref msj);
                Session["listAlumno"] = listAlumno;

                if (objBLAlumno != null && listPosAl.Count >= 1 && listMedico.Count >= 1 && listProgEd.Count >= 1
                && listCuatri.Count >= 1 && listGrupo.Count >= 1 && listAlumno.Count >= 1)
                {
                    generarListPosAl();
                    generarListMedicos();
                    generarListProgEd();
                    generarListGrupo();
                    generarListCuatri();
                    generarListAlumno();
                }
            }
            else
            {
                objBLAlumno = (LogicaNegociosAlumno)Session["objBLAlumno"];
                objBLCuatrimestre = (LogicaNegociosCuatrimestre)Session["objBLCuatrimestre"];
                listPosAl = (List<FiltroPositivoAlumno>)Session["listPosAl"];
                listMedico = (List<Medico>)Session["listMedico"];
                listProgEd=(List<ProgramaEducativo>)Session["listProgEd"];
                listCuatri=(List<Cuatrimestre>)Session["listCuatri"];
                listGrupo=(List<Grupo>)Session["listGrupo"];
                listAlumno = (List<Alumno>)Session["listAlumno"];
            }           
           
            DataSet dsSeguimiento = objBLAlumno.consultarSeguimientoAlumno(ref msj);
            if(dsSeguimiento.Tables[0].Rows.Count > 0)
            {
                gvSeguimientoAlumno.DataSource = dsSeguimiento;
                gvSeguimientoAlumno.DataBind();
            }
            else
            {
                alert("Aviso", "Aún no existen registros", 1);
            }       
        }

        protected void btnInsertarSegAl_Click(object sender, EventArgs e)
        {
            SeguimientoAlumno nuevo = null;
            string msj = "";
            if (!string.IsNullOrEmpty(txtComunic.Text) && !string.IsNullOrEmpty(txtReporte.Text)
                && (ddlPosAl.SelectedIndex >= 0) && (ddlMedico.SelectedIndex >= 0))
            {
                nuevo = new SeguimientoAlumno()
                {
                    f_positivoAlum=Convert.ToInt32(ddlPosAl.SelectedValue),
                    f_medico=Convert.ToInt32(ddlMedico.SelectedValue),
                    fecha=calFechaSeg.SelectedDate,
                    formaComunicacion=txtComunic.Text,
                    reporte=txtReporte.Text,
                    entrevista=txtEntrevista.Text,
                    extra=txtExtra.Text
                };
                bool result= objBLAlumno.insertarSeguimientoAlumno(nuevo, ref msj);
                if (result)
                {
                    // msj añadido
                    alert("Hecho", "Registro añadido", 2);
                    ddlPosAl.SelectedValue = null;
                    ddlMedico.SelectedValue = null;
                    txtComunic.Text = "";
                    txtReporte.Text = "";
                    txtEntrevista.Text = "";
                    txtExtra.Text = "";
                    calFechaSeg.SelectedDate = DateTime.MinValue;
                    // actualizar gv
                    gvSeguimientoAlumno.DataSource = objBLAlumno.consultarSeguimientoAlumno(ref msj);
                    gvSeguimientoAlumno.DataBind();
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

        protected void gvSeguimientoAlumno_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            SeguimientoAlumno recuperado = null;
            string msj = "";
            if (e.NewSelectedIndex >= 0) // se seleccionó registro
            {
                recuperado = objBLAlumno.buscarSeguimientoAlumno(Convert.ToInt32(gvSeguimientoAlumno.Rows[e.NewSelectedIndex].Cells[3].Text), ref msj);
                ddlPosAl.SelectedValue = recuperado.f_positivoAlum.ToString();
                ddlMedico.SelectedValue = recuperado.f_medico.ToString();
                calFechaSeg.SelectedDate = recuperado.fecha;
                txtComunic.Text = recuperado.formaComunicacion;
                txtReporte.Text= recuperado.reporte;
                txtEntrevista.Text = recuperado.entrevista;
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

        protected void gvSeguimientoAlumno_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SeguimientoAlumno editado = null;
            string msj = "";
            int idSegAl = 0;
            if (!string.IsNullOrEmpty(txtComunic.Text) && !string.IsNullOrEmpty(txtReporte.Text)
                && (ddlPosAl.SelectedIndex >= 0) && (ddlMedico.SelectedIndex >= 0))
            {
                editado = new SeguimientoAlumno()
                {
                    f_positivoAlum = Convert.ToInt32(ddlPosAl.SelectedValue),
                    f_medico = Convert.ToInt32(ddlMedico.SelectedValue),
                    fecha = calFechaSeg.SelectedDate,
                    formaComunicacion = txtComunic.Text,
                    reporte = txtReporte.Text,
                    entrevista = txtEntrevista.Text,
                    extra = txtExtra.Text
                };

                if (gvSeguimientoAlumno.SelectedIndex >= 0) // se seleccionó registro
                {
                    idSegAl = Convert.ToInt32(gvSeguimientoAlumno.Rows[gvSeguimientoAlumno.SelectedIndex].Cells[3].Text);
                    bool result=objBLAlumno.editarSeguimientoAlumno(idSegAl, editado, ref msj);
                    if (result)
                    {
                        // msj Registro actualizado
                        alert("Hecho", "Registro actualizado", 2);
                        ddlPosAl.SelectedValue = null;
                        ddlMedico.SelectedValue = null;
                        txtComunic.Text = "";
                        txtReporte.Text = "";
                        txtEntrevista.Text = "";
                        txtExtra.Text = "";
                        calFechaSeg.SelectedDate = DateTime.MinValue;
                        // actualizar gv
                        gvSeguimientoAlumno.DataSource = objBLAlumno.consultarSeguimientoAlumno(ref msj);
                        gvSeguimientoAlumno.DataBind();
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

        protected void gvSeguimientoAlumno_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string msj = "";
            if (e.RowIndex >= 0) // se seleccionó registro
            {
                bool result = objBLAlumno.eliminarSeguimientoAlumno(Convert.ToInt32(gvSeguimientoAlumno.Rows[e.RowIndex].Cells[3].Text), ref msj);
                if (result)
                {
                    // msj eliminado
                    alert("Hecho", "Registro eliminado", 2);
                    // actualizar gv
                    gvSeguimientoAlumno.DataSource = objBLAlumno.consultarSeguimientoAlumno(ref msj);
                    gvSeguimientoAlumno.DataBind();
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

        //------------------------------------------Consultas compuestas------------------------------------------       

        // consulta compuesta 1 : mostrar a todos los alumnos contagiados de
        // un programa educativo, en un cuatrimestre específico
        protected void btnConsComp1_Click(object sender, EventArgs e)
        {
            int idProgEd = Convert.ToInt32(ddlProgEd.SelectedValue);
            int idCuatri = Convert.ToInt32(ddlCuatrimestre1.SelectedValue);
            DataSet result = objBLAlumno.mostrarAlumnosContagiadosFiltroProgEdCuatri(idProgEd, idCuatri, ref msj);
            if (result.Tables[0].Rows.Count > 0)
            {
                alert("Hecho", "Resultados obtenidos", 2);
                gvSegProgEdCuat.DataSource = result;
                gvSegProgEdCuat.DataBind();
            }
            else
            {
                alert("Aviso", "No se encontraron los datos solicitados, vuelve a intentar con otra solicitud", 1);
            }
        }

        // consulta compuesta 2 : mostrar a todos los alumnos contagiados de
        // un programa educativo, en un cuatrimestre específico y de un grupo en partícular
        protected void btnConsComp2_Click(object sender, EventArgs e)
        {
            int idProgEd = Convert.ToInt32(ddlProgEd2.SelectedValue);
            int idCuatri = Convert.ToInt32(ddlCuatrimestre2.SelectedValue);
            int idGrupo = Convert.ToInt32(ddlGrupo.SelectedValue);
            DataSet result = objBLAlumno.mostrarAlumnosContagiadosFiltroProgEdCuatriGrupo(idProgEd, idCuatri, idGrupo, ref msj);
            if (result.Tables[0].Rows.Count > 0)
            {
                alert("Hecho", "Resultados obtenidos", 2);
                gvSegProgEdCuatGru.DataSource = result;
                gvSegProgEdCuatGru.DataBind();
            }
            else
            {
                alert("Aviso", "No se encontraron los datos solicitados, vuelve a intentar con otra solicitud", 1);
            }
        }

        // consulta compuesta 3 : mostrar el seguimiento de un alumno (por su registro) en un cuatrimestre específico
        protected void btnConsComp3_Click(object sender, EventArgs e)
        {
            string matriculaAl = ddlAlumno.SelectedValue;
            int idCuatri = Convert.ToInt32(ddlCuatrimestre3.SelectedValue);
            DataSet result = objBLAlumno.mostrarSeguimientoAlumno(matriculaAl, idCuatri, ref msj);
            if (result.Tables[0].Rows.Count > 0)
            {
                alert("Hecho", "Resultados obtenidos", 2);
                gvSegAlCuatri.DataSource = result;
                gvSegAlCuatri.DataBind();
            }
            else
            {
                alert("Aviso", "No se encontraron los datos solicitados, vuelve a intentar con otra solicitud", 1);
            }
        }

        //--------------------------------- Métodos para llenar ddl---------------------------------------------
        public void generarListPosAl()
        {
            ddlPosAl.Items.Clear();
            ListItem listItem;
            foreach (FiltroPositivoAlumno item in listPosAl)
            {
                listItem = new ListItem();
                listItem.Text = item.fechaConfirmado + " " + item.matriculaAl;
                listItem.Value = item.id_posAl.ToString();
                ddlPosAl.Items.Add(listItem);
            }
        }
        public void generarListMedicos()
        {
            ddlMedico.Items.Clear();
            ListItem listItem;
            foreach (Medico item in listMedico)
            {
                listItem = new ListItem();
                listItem.Text = item.nombre + " " + item.app + " " + item.apm;
                listItem.Value = item.id.ToString();
                ddlMedico.Items.Add(listItem);
            }
        }
        public void generarListProgEd()
        {
            ddlProgEd.Items.Clear();
            ddlProgEd2.Items.Clear();
            ListItem listItem;
            foreach (ProgramaEducativo item in listProgEd)
            {
                listItem = new ListItem();
                listItem.Text = item.programaEd;
                listItem.Value = item.id.ToString();
                ddlProgEd.Items.Add(listItem);
                ddlProgEd2.Items.Add(listItem);
            }
        }
        public void generarListGrupo()
        {
            ddlGrupo.Items.Clear();
            ListItem listItem;
            foreach (Grupo item in listGrupo)
            {
                listItem = new ListItem();
                listItem.Text = item.grado.ToString() + item.letra;
                listItem.Value = item.id.ToString();
                ddlGrupo.Items.Add(listItem);
            }
        }
        public void generarListCuatri()
        {
            ddlCuatrimestre1.Items.Clear();
            ddlCuatrimestre2.Items.Clear();
            ddlCuatrimestre3.Items.Clear();
            ListItem listItem;
            foreach (Cuatrimestre item in listCuatri)
            {
                listItem = new ListItem();
                listItem.Text = item.periodo + item.anio.ToString();
                listItem.Value = item.id.ToString();
                ddlCuatrimestre1.Items.Add(listItem);
                ddlCuatrimestre2.Items.Add(listItem);
                ddlCuatrimestre3.Items.Add(listItem);
            }
        }
        public void generarListAlumno()
        {
            ddlAlumno.Items.Clear();
            ListItem listItem;
            foreach (Alumno item in listAlumno)
            {
                listItem = new ListItem();
                listItem.Text = item.matricula + " " + item.nombre + " " + item.apPat + " " + item.apMat;
                listItem.Value = item.matricula;
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