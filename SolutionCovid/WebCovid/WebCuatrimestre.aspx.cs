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
    public partial class WebCuatrimestre : System.Web.UI.Page
    {
        LogicaNegociosCuatrimestre objBLCuatrimestre = null;
        string msj = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                objBLCuatrimestre = new LogicaNegociosCuatrimestre("Data Source=LAPTOP-KVRVONP6; Initial Catalog=SeguimientoCovid; Integrated Security=True;");
                //ConfigurationManager.ConnectionStrings["conexSql"].ConnectionString
                Session["objBLCuatrimestre"] = objBLCuatrimestre;              
            }
            else
            {
                objBLCuatrimestre = (LogicaNegociosCuatrimestre)Session["objBLCuatrimestre"];
            }

            //dll ProgramaEdu
            Session["listProgEd"] = objBLCuatrimestre.obtenerColeccionProgrEd(ref msj);
            List<ProgramaEducativo> listProgEd = (List<ProgramaEducativo>)Session["listProgEd"];
            ddlProEd.Items.Clear();
            ddlProgEdA.Items.Clear();
            for(int i=0; i < listProgEd.Count; i++)
            {
                if (!ddlProEd.Items.Contains(new ListItem(
                        listProgEd[i].programaEd, listProgEd[i].id.ToString()
                    )))
                {
                    ddlProEd.Items.Add(new ListItem(
                        listProgEd[i].programaEd, listProgEd[i].id.ToString()
                    ));
                }

                if (!ddlProgEdA.Items.Contains(new ListItem(
                        listProgEd[i].programaEd, listProgEd[i].id.ToString()
                    )))
                {
                    ddlProgEdA.Items.Add(new ListItem(
                        listProgEd[i].programaEd, listProgEd[i].id.ToString()
                    ));
                }
            }

            //dll Grupo
            Session["listGrupo"] = objBLCuatrimestre.obtenerColeccionGrupo(ref msj);
            List<Grupo> listGrupo = (List<Grupo>)Session["listGrupo"];
            ddlGrupo.Items.Clear();
            ddlGrupoA.Items.Clear();
            for (int i = 0; i < listGrupo.Count; i++)
            {
                if (!ddlGrupo.Items.Contains(new ListItem(
                        listGrupo[i].grado.ToString() + listGrupo[i].letra, listGrupo[i].id.ToString()
                    )))
                {
                    ddlGrupo.Items.Add(new ListItem(
                        listGrupo[i].grado.ToString() + listGrupo[i].letra, listGrupo[i].id.ToString()
                    ));
                }

                if (!ddlGrupoA.Items.Contains(new ListItem(
                        listGrupo[i].grado.ToString() + listGrupo[i].letra, listGrupo[i].id.ToString()
                    )))
                {
                    ddlGrupoA.Items.Add(new ListItem(
                        listGrupo[i].grado.ToString() + listGrupo[i].letra, listGrupo[i].id.ToString()
                    ));
                }
            }

            //dll Cuatrimestre
            Session["listCuatri"] = objBLCuatrimestre.obtenerListaCuatrimestres(ref msj);
            List<Cuatrimestre> listCuatri = (List<Cuatrimestre>)Session["listCuatri"];
            ddlCuatri.Items.Clear();
            ddlCuatriA.Items.Clear();
            for (int i = 0; i < listCuatri.Count; i++)
            {
                if (!ddlCuatri.Items.Contains(new ListItem(
                        listCuatri[i].periodo + listCuatri[i].anio.ToString(), listCuatri[i].id.ToString()
                    )))
                {
                    ddlCuatri.Items.Add(new ListItem(
                        listCuatri[i].periodo + listCuatri[i].anio.ToString(), listCuatri[i].id.ToString()
                    ));
                }

                if (!ddlCuatriA.Items.Contains(new ListItem(
                        listCuatri[i].periodo + listCuatri[i].anio.ToString(), listCuatri[i].id.ToString()
                    )))
                {
                    ddlCuatriA.Items.Add(new ListItem(
                        listCuatri[i].periodo + listCuatri[i].anio.ToString(), listCuatri[i].id.ToString()
                    ));
                }
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
                objBLCuatrimestre.insertarCuatrimestre(nuevo, ref msj);
                ddlPeriodo.SelectedValue = null;
                txtAnio.Text = "";
                txtExtraCuatri.Text = "";
                // msj Cuatrimestre añadido
                // actualizar gv cuatrimestres
                gvCuatrimestres.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj);
                gvCuatrimestres.DataBind();
                gvCuatriAE.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj);
                gvCuatriAE.DataBind();
            }
            else
            {
                // msj Error: Revisa que no haya campos vacíos y vuelve a intentar
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
                objBLCuatrimestre.insertarGrupoCuatrimestre(nuevo, ref msj);
                ddlProEd.SelectedValue = null;
                ddlGrupo.SelectedValue = null;
                ddlCuatri.SelectedValue = null;
                ddlTurno.SelectedValue = null;
                txtModalidad.Text = "";
                txtExtraGruCuat.Text = "";
                // msj Grupo de un cuatrimestre añadido
                // actualizar gv grupo_cuatrimestre
                gvGruposCuatris.DataSource = objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
                gvGruposCuatris.DataBind();
                gvGruCuatAE.DataSource = objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
                gvGruCuatAE.DataBind();
            }
            else
            {
                // msj Error: Revisa que no haya campos vacíos y vuelve a intentar
            }
        }

        // eliminar 
        protected void gvCuatriAE_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string msj = "";
            if (e.RowIndex >= 0) // se seleccionó registro
            {
                objBLCuatrimestre.eliminarCuatrimestre(Convert.ToInt32(gvCuatriAE.Rows[e.RowIndex].Cells[3].Text), ref msj);
                // msj eliminado
            }
            else
            {
                //msj Error: Selecciona un registro
            }
            // actualizar gv de cuatrimestres
            gvCuatrimestres.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj); // (ver)
            gvCuatrimestres.DataBind();            
            gvCuatriAE.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj); // (actualizar/eliminar)
            gvCuatriAE.DataBind();
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
            }
            else
            {
                //msj Error: Selecciona un registro
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
                    objBLCuatrimestre.editarCuatrimestre(idCuatrimestre, editado, ref msj);
                    dllPeriodoA.SelectedValue = null;
                    txtAnioA.Text = "";
                    txtExtraCuatriA.Text = "";
                    // msj Registro actualizado
                }
                else
                {
                    // msj Error: Selecciona un registro
                }
            }
            else
            {
                // msj Error: Revisa que no haya campos vacíos y vuelve a intentar
            }
            // actualizar gv de cuatrimestres
            gvCuatrimestres.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj); // (ver)
            gvCuatrimestres.DataBind();
            gvCuatriAE.DataSource = objBLCuatrimestre.consultarCuatrimestres(ref msj); // (actualizar/eliminar)
            gvCuatriAE.DataBind();
        }

        //----------------------------------------GrupoCuatrimestre-------------------------------------------

        // eliminar 
        protected void gvGruCuatAE_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string msj = "";
            if (e.RowIndex >= 0) // se seleccionó registro
            {
                objBLCuatrimestre.eliminarGrupoCuatrimestre(Convert.ToInt32(gvGruCuatAE.Rows[e.RowIndex].Cells[3].Text), ref msj);
                // msj eliminado
            }
            else
            {
                //msj Error: Selecciona un registro
            }
            // actualizar gv de grupo-cuatrimestre
            gvGruposCuatris.DataSource = objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
            gvGruposCuatris.DataBind();
            gvGruCuatAE.DataSource = objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
            gvGruCuatAE.DataBind();
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
            }
            else
            {
                //msj Error: Selecciona un registro
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
                    objBLCuatrimestre.editarGrupoCuatrimestre(idGruCuat, editado, ref msj);
                    // msj Registro actualizado
                    ddlProgEdA.SelectedValue = null;
                    ddlGrupoA.SelectedValue = null;
                    ddlCuatriA.SelectedValue = null;
                    ddlTurnoA.SelectedValue = null;
                    txtModalidadA.Text = "";
                    txtExtraGruCuatA.Text = "";
                }
                else
                {
                    // msj Error: Selecciona un registro
                }
            }
            else
            {
                // msj Error: Revisa que no haya campos vacíos y vuelve a intentar
            }
            // actualizar gv de grupo-cuatrimestre
            gvGruposCuatris.DataSource = objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
            gvGruposCuatris.DataBind();
            gvGruCuatAE.DataSource = objBLCuatrimestre.consultarGruposCuatrimestres(ref msj);
            gvGruCuatAE.DataBind();
        }
    }
}