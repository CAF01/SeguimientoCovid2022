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
    public partial class WebCuatrimestre : System.Web.UI.Page
    {
        LogicaNegociosCuatrimestre objBLCuatrimestre = null;
        List<ProgramaEducativo> listProgEd;
        List<Grupo> listGrupo;
        List<Cuatrimestre> listCuatri;
        string msj = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // primera carga - cargar
            {
                // ConnectionString Server = Server=20.187.117.181,1434;Database=SeguimientoCovidServer;User ID=sa;Password=admin1234;               
                objBLCuatrimestre = new LogicaNegociosCuatrimestre("Server=20.187.117.181,1434;Database=SeguimientoCovidServer;User ID=sa;Password=admin1234;");
                //objBLCuatrimestre = new LogicaNegociosCuatrimestre(ConfigurationManager.ConnectionStrings["conexSql"].ConnectionString);
                Session["objBLCuatrimestre"] = objBLCuatrimestre;
                listProgEd = objBLCuatrimestre.obtenerColeccionProgrEd(ref msj);
                Session["listProgEd"] = listProgEd;
                listGrupo = objBLCuatrimestre.obtenerColeccionGrupo(ref msj);
                Session["listGrupo"] = listGrupo;
                listCuatri = objBLCuatrimestre.obtenerListaCuatrimestres(ref msj);
                Session["listCuatri"] = listCuatri;

                if (objBLCuatrimestre != null && listProgEd.Count >= 1 && listGrupo.Count >= 1 && listCuatri.Count >= 1)
                {
                    generarListProgEd();
                    generarListGrupo();
                    generarListCuatri();
                }
            }
            else // segunda carga - actualizar
            {
                objBLCuatrimestre = (LogicaNegociosCuatrimestre)Session["objBLCuatrimestre"];
                listProgEd = (List<ProgramaEducativo>)Session["listProgEd"];
                listGrupo = (List<Grupo>)Session["listGrupo"];
                listCuatri = (List<Cuatrimestre>)Session["listCuatri"];
            }

            // cargar gv cuatrimestres (ver)
            gvCuatrimestres.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj);
            gvCuatrimestres.DataBind();
            // cargar gv grupo_cuatrimestre (ver)
            gvGruposCuatris.DataSource = objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
            gvGruposCuatris.DataBind();
            // cargar gv cuatrimestres (actualizar/eliminar)
            gvCuatriAE.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj);
            gvCuatriAE.DataBind();
            // cargar gv grupo_cuatrimestre (actualizar/eliminar)
            gvGruCuatAE.DataSource= objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
            gvGruCuatAE.DataBind();
        }

        //----------------------------------------Cuatrimestre------------------------------------------------
        protected void btnInsertarCuatri_Click(object sender, EventArgs e)
        {
            Cuatrimestre nuevo = null;
            string msj = "";
            if (!string.IsNullOrEmpty(txtAnio.Text)&&(ddlPeriodo.SelectedIndex>=0))
            {
                nuevo = new Cuatrimestre()
                {
                    periodo = ddlPeriodo.SelectedValue,
                    anio = Convert.ToInt32(txtAnio.Text),
                    fechaInicio = calInicio.SelectedDate,
                    fechaFin = calFin.SelectedDate,
                    extra = txtExtraCuatri.Text
                };
                bool result = objBLCuatrimestre.insertarCuatrimestre(nuevo, ref msj);
                if (result)
                {
                    // msj añadido
                    alert("Hecho", "Registro añadido", 2);
                    ddlPeriodo.SelectedValue = null;
                    txtAnio.Text = "";
                    txtExtraCuatri.Text = "";
                    calInicio.SelectedDate = DateTime.MinValue;
                    calFin.SelectedDate = DateTime.MinValue;
                    // actualizar gv cuatrimestres
                    gvCuatrimestres.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj);
                    gvCuatrimestres.DataBind();
                    gvCuatriAE.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj);
                    gvCuatriAE.DataBind();
                    // actualizar ddl
                    listCuatri = objBLCuatrimestre.obtenerListaCuatrimestres(ref msj);
                    Session["listCuatri"] = listCuatri;
                    generarListCuatri();
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

        protected void btnInsertarGrupoCuatri_Click(object sender, EventArgs e)
        {
            GrupoCuatrimestre nuevo = null;
            string msj = "";
            if (!string.IsNullOrEmpty(txtModalidad.Text) && (ddlProEd.SelectedIndex >= 0) && (ddlGrupo.SelectedIndex >= 0)
                && (ddlCuatri.SelectedIndex >= 0) && (ddlTurno.SelectedIndex >= 0))
            { 
                nuevo = new GrupoCuatrimestre()
                {
                    f_progEdu = Convert.ToByte(ddlProEd.SelectedValue),
                    f_grupo = Convert.ToInt16(ddlGrupo.SelectedValue),
                    f_cuatri = Convert.ToInt16(ddlCuatri.SelectedValue),
                    turno = ddlTurno.SelectedValue,
                    modalidad = txtModalidad.Text,
                    extra = txtExtraGruCuat.Text
                };
                bool result = objBLCuatrimestre.insertarGrupoCuatrimestre(nuevo, ref msj);
                if (result)
                {
                    // msj añadido
                    alert("Hecho", "Registro añadido", 2);
                    ddlProEd.SelectedValue = null;
                    ddlGrupo.SelectedValue = null;
                    ddlCuatri.SelectedValue = null;
                    ddlTurno.SelectedValue = null;
                    txtModalidad.Text = "";
                    txtExtraGruCuat.Text = "";
                    // actualizar gv grupo_cuatrimestre
                    gvGruposCuatris.DataSource = objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
                    gvGruposCuatris.DataBind();
                    gvGruCuatAE.DataSource = objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
                    gvGruCuatAE.DataBind();
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

        // eliminar 
        protected void gvCuatriAE_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string msj = "";
            if (e.RowIndex >= 0) // se seleccionó registro
            {
                bool result = objBLCuatrimestre.eliminarCuatrimestre(Convert.ToInt32(gvCuatriAE.Rows[e.RowIndex].Cells[3].Text), ref msj);
                if (result)
                {
                    // msj eliminado
                    alert("Hecho", "Registro eliminado", 2);
                    // actualizar gv
                    gvCuatrimestres.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj); // (ver)
                    gvCuatrimestres.DataBind();
                    gvCuatriAE.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj); // (actualizar/eliminar)
                    gvCuatriAE.DataBind();
                    // actualizar ddl
                    listCuatri = objBLCuatrimestre.obtenerListaCuatrimestres(ref msj);
                    Session["listCuatri"] = listCuatri;
                    generarListCuatri();
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

        // mostrar datos en controles de formulario
        protected void gvCuatriAE_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Cuatrimestre recuperado = null;
            string msj = "";
            if(e.NewSelectedIndex >= 0) // se seleccionó registro
            {
                recuperado = objBLCuatrimestre.buscarCuatrimestre(Convert.ToInt32(gvCuatriAE.Rows[e.NewSelectedIndex].Cells[3].Text), ref msj);
                dllPeriodoA.SelectedValue = recuperado.periodo;
                txtAnioA.Text = recuperado.anio.ToString();
                calInicioA.SelectedDate = recuperado.fechaInicio;
                calFinA.SelectedDate = recuperado.fechaFin;
                txtExtraCuatriA.Text = recuperado.extra;
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
        protected void gvCuatriAE_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Cuatrimestre editado = null;
            string msj = "";
            int idCuatrimestre = 0;
            if (!string.IsNullOrEmpty(txtAnioA.Text) && (dllPeriodoA.SelectedIndex >= 0))
            {
                editado = new Cuatrimestre()
                {
                    periodo = dllPeriodoA.SelectedValue,
                    anio = Convert.ToInt32(txtAnioA.Text),
                    fechaInicio = calInicioA.SelectedDate,
                    fechaFin = calFinA.SelectedDate,
                    extra = txtExtraCuatriA.Text
                };

                if (gvCuatriAE.SelectedIndex >= 0) // se seleccionó registro
                {
                    idCuatrimestre = Convert.ToInt32(gvCuatriAE.Rows[gvCuatriAE.SelectedIndex].Cells[3].Text);
                    bool result = objBLCuatrimestre.editarCuatrimestre(idCuatrimestre, editado, ref msj);
                    if (result)
                    {
                        // msj Registro actualizado
                        alert("Hecho", "Registro actualizado", 2);
                        dllPeriodoA.SelectedValue = null;
                        txtAnioA.Text = "";
                        txtExtraCuatriA.Text = "";
                        calInicioA.SelectedDate = DateTime.MinValue;
                        calFinA.SelectedDate = DateTime.MinValue;
                        // actualizar gv
                        gvCuatrimestres.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj); // (ver)
                        gvCuatrimestres.DataBind();
                        gvCuatriAE.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj); // (actualizar/eliminar)
                        gvCuatriAE.DataBind();
                        // actualizar ddl
                        listCuatri = objBLCuatrimestre.obtenerListaCuatrimestres(ref msj);
                        Session["listCuatri"] = listCuatri;
                        generarListCuatri();
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

        //----------------------------------------GrupoCuatrimestre-------------------------------------------

        // eliminar 
        protected void gvGruCuatAE_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string msj = "";
            if (e.RowIndex >= 0) // se seleccionó registro
            {
                bool result = objBLCuatrimestre.eliminarGrupoCuatrimestre(Convert.ToInt32(gvGruCuatAE.Rows[e.RowIndex].Cells[3].Text), ref msj);
                if (result)
                {
                    // msj eliminado
                    alert("Hecho", "Registro eliminado", 2);
                    // actualizar gv 
                    gvGruposCuatris.DataSource = objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
                    gvGruposCuatris.DataBind();
                    gvGruCuatAE.DataSource = objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
                    gvGruCuatAE.DataBind();
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

        // mostrar datos en controles de formulario
        protected void gvGruCuatAE_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GrupoCuatrimestre recuperado = null;
            string msj = "";
            if (e.NewSelectedIndex >= 0) // se seleccionó registro
            {
                recuperado = objBLCuatrimestre.buscarGrupoCuatrimestre(Convert.ToInt32(gvGruCuatAE.Rows[e.NewSelectedIndex].Cells[3].Text), ref msj);
                ddlProgEdA.SelectedValue = recuperado.f_progEdu.ToString();
                ddlGrupoA.SelectedValue = recuperado.f_grupo.ToString();
                ddlCuatriA.SelectedValue = recuperado.f_cuatri.ToString();
                ddlTurnoA.SelectedValue = recuperado.turno;
                txtModalidadA.Text = recuperado.modalidad;
                txtExtraGruCuatA.Text = recuperado.extra;
                // msj recuperado
                alert("Hecho", "Datos recuperados", 2);
            }
            else
            {
                // msj Error: Selecciona un registro
                alert("Error", "Selecciona un registro", 3);
            }
        }

        // editar
        protected void gvGruCuatAE_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GrupoCuatrimestre editado = null;
            string msj = "";
            int idGruCuat = 0;
            if (!string.IsNullOrEmpty(txtModalidadA.Text) && (ddlProgEdA.SelectedIndex >= 0) && (ddlGrupoA.SelectedIndex >= 0)
                && (ddlCuatriA.SelectedIndex >= 0) && (ddlTurnoA.SelectedIndex >= 0))
            {
                editado = new GrupoCuatrimestre()
                {
                    f_progEdu = Convert.ToByte(ddlProgEdA.SelectedValue),
                    f_grupo = Convert.ToInt16(ddlGrupoA.SelectedValue),
                    f_cuatri = Convert.ToInt16(ddlCuatriA.SelectedValue),
                    turno = ddlTurnoA.SelectedValue,
                    modalidad = txtModalidadA.Text,
                    extra = txtExtraGruCuatA.Text
                };

                if (gvGruCuatAE.SelectedIndex >= 0) // se seleccionó registro
                {
                    idGruCuat = Convert.ToInt32(gvGruCuatAE.Rows[gvGruCuatAE.SelectedIndex].Cells[3].Text);
                    bool result = objBLCuatrimestre.editarGrupoCuatrimestre(idGruCuat, editado, ref msj);
                    if (result)
                    {
                        // msj Registro actualizado
                        alert("Hecho", "Registro actualizado", 2);
                        ddlProgEdA.SelectedValue = null;
                        ddlGrupoA.SelectedValue = null;
                        ddlCuatriA.SelectedValue = null;
                        ddlTurnoA.SelectedValue = null;
                        txtModalidadA.Text = "";
                        txtExtraGruCuatA.Text = "";
                        // actualizar gv
                        gvGruposCuatris.DataSource = objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
                        gvGruposCuatris.DataBind();
                        gvGruCuatAE.DataSource = objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
                        gvGruCuatAE.DataBind();
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

        //--------------------------------- Métodos para llenar ddl---------------------------------------------        
        public void generarListProgEd()
        {
            ddlProEd.Items.Clear();
            ddlProgEdA.Items.Clear();
            ListItem listItem;
            foreach (ProgramaEducativo item in listProgEd)
            {
                listItem = new ListItem();
                listItem.Text = item.programaEd;
                listItem.Value = item.id.ToString();
                ddlProEd.Items.Add(listItem);
                ddlProgEdA.Items.Add(listItem);
            }
        }            
        public void generarListGrupo()
        {
            ddlGrupo.Items.Clear();
            ddlGrupoA.Items.Clear();
            ListItem listItem;
            foreach (Grupo item in listGrupo)
            {
                listItem = new ListItem();
                listItem.Text = item.grado.ToString() + item.letra;
                listItem.Value = item.id.ToString();
                ddlGrupo.Items.Add(listItem);
                ddlGrupoA.Items.Add(listItem);
            }
        }
        public void generarListCuatri()
        {
            ddlCuatri.Items.Clear();
            ddlCuatriA.Items.Clear();
            ListItem listItem;
            foreach (Cuatrimestre item in listCuatri)
            {
                listItem = new ListItem();
                listItem.Text = item.periodo + item.anio.ToString();
                listItem.Value = item.id.ToString();
                ddlCuatri.Items.Add(listItem);
                ddlCuatriA.Items.Add(listItem);
            }
        }

        //-----------------------------------------------Alert--------------------------------------------------
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