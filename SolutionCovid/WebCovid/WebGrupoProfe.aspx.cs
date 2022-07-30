using ClassEntidades;
using ClassLogicaNegocios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCovid
{
    public partial class WebGrupoProfe : System.Web.UI.Page
    {
        LogicaNegociosCuatrimestre logicaNegociosCuatrimestre;
        LogicaNegociosProfesor LogicaNegociosProfesor;
        List<ProgramaEducativo> programaEducativos;
        List<Profesor> profesors;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                logicaNegociosCuatrimestre = new LogicaNegociosCuatrimestre(ConfigurationManager.ConnectionStrings["conexSql"].ConnectionString);
                LogicaNegociosProfesor = new LogicaNegociosProfesor(ConfigurationManager.ConnectionStrings["conexSql"].ConnectionString);
                Session["logCuatrGrupo"] = logicaNegociosCuatrimestre;
                Session["logProfGrp"] = LogicaNegociosProfesor;

                if(logicaNegociosCuatrimestre!=null)
                {
                    string msg = "";
                    programaEducativos = logicaNegociosCuatrimestre.obtenerColeccionProgrEd(ref msg);
                    profesors = LogicaNegociosProfesor.MostrarProfesoresFiltro();
                    if(programaEducativos.Count>0 && profesors.Count>0)
                    {
                        DDLProgramas.Items.Clear();
                        DDLProf.Items.Clear();
                        ListItem listItem;
                        foreach (ProgramaEducativo programaEducativo in programaEducativos)
                        {
                            listItem= new ListItem();
                            listItem.Text=programaEducativo.programaEd.ToString();
                            listItem.Value = programaEducativo.id.ToString();
                            DDLProgramas.Items.Add(listItem);
                        }
                        foreach(Profesor item in profesors)
                        {
                            listItem = new ListItem();
                            listItem.Text = String.Format("{0}, Registro Trabajador:{1}, {2}, {3}", item.Nombre, item.RegistroEmpleado.ToString(), item.Genero, item.Categoria);
                            listItem.Value = item.ID_Profe.ToString();
                            DDLProf.Items.Add(listItem);
                        }
                        BTNR.Visible = false;
                        Label1.Visible = false;
                        TB1.Visible = false;
                        TB2.Visible = false;
                        BTNM.Visible = false;
                        BTND.Visible = false;
                        Label2.Visible = false;
                        Label3.Visible = false;
                        LBLH.Visible = false;
                    }
                }
            }
            else
            {
                logicaNegociosCuatrimestre=(LogicaNegociosCuatrimestre)Session["logCuatrGrupo"];
                LogicaNegociosProfesor=(LogicaNegociosProfesor)Session["logProfGrp"];
            }
        }

        public void EnviaAlertas(string titulo, string msg, string tipo)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), titulo, String.Format("registro('{0}','{1}','{2}')", titulo, msg, tipo), true);
        }

        protected void BTNSG_Click(object sender, EventArgs e)
        {
            if(DDLProgramas.SelectedIndex>=0)
            {
                int idProgram = Convert.ToInt32(DDLProgramas.SelectedValue);
                GVGrupos.DataSource=LogicaNegociosProfesor.ObtenerRegistrosGrupos(idProgram);
                GVGrupos.DataBind();

                GVGrupos.SelectedIndex = -1;
                //Ocultar Botones Inserción, Actualización y Eliminación
                BTNR.Visible = false;
                Label1.Visible = false;
                TB1.Visible = false;
                TB2.Visible = false;
                BTNM.Visible = false;
                BTND.Visible = false;
                Label2.Visible = false;
                Label3.Visible = false;
            }
        }

        protected void GVGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(GVGrupos.SelectedIndex>=0)
            {
                int idGrupoCuat = Convert.ToInt32(GVGrupos.SelectedRow.Cells[1].Text);
                string[] Info = LogicaNegociosProfesor.ConocerProfesordeGrupoProfe(idGrupoCuat);
                if (Info != null)
                {
                    LBLH.Text = Info[0];
                    ListItem listItem = DDLProf.Items.FindByValue(Info[5]);
                    DDLProf.SelectedIndex= DDLProf.Items.IndexOf(listItem);
                    EnviaAlertas("Información", String.Format("Este grupo cuenta con el profesor:{0}, {1}", Info[1], Info[2]), "info");
                    TB1.Text = Info[3];
                    TB2.Text = Info[4];
                    TB1.Visible = true;
                    TB2.Visible = true;
                    //Ocultar botones de registro
                    BTNR.Visible = false;
                    Label1.Visible = false;
                    //Mostrar Botones Actualización y Eliminación
                    BTNM.Visible = true;
                    BTND.Visible = true;
                    Label2.Visible = true;
                    Label3.Visible = true;
                }
                else
                {
                    TB1.Text = "";
                    TB2.Text = "";
                    //Mostrar controles insertar
                    BTNR.Visible = true;
                    Label1.Visible = true;
                    TB1.Visible = true;
                    TB2.Visible = true;
                    //Ocultar Botones Actualización y Eliminación
                    BTNM.Visible = false;
                    BTND.Visible = false;
                    Label2.Visible = false;
                    Label3.Visible = false;
                    LBLH.Text = "";
                    DDLProf.SelectedIndex = 0;
                    EnviaAlertas("Información", "Este grupo NO cuenta con profesor asignado", "info");
                }
                
            }
        }

        protected void BTNR_Click(object sender, EventArgs e)
        {
            if(GVGrupos.SelectedIndex>=0 && DDLProf.SelectedIndex>=0)
            {
                if(LogicaNegociosProfesor.AgregarProfeGrupo(new ProfeGrupo()
                {
                    F_GrupoCuatrimestre= Convert.ToInt32(GVGrupos.SelectedRow.Cells[1].Text),
                    F_Profe= Convert.ToInt32(DDLProf.SelectedValue),
                    Extra=TB1.Text,
                    Extra_dos=TB2.Text
                }))
                {
                    EnviaAlertas("Correcto", "Profesor asignado a grupo correctamente", "success");
                    BTNR.Visible = false;
                    Label1.Visible = false;
                    TB1.Visible = false;
                    TB2.Visible = false;
                    TB1.Text = ""; TB2.Text = ""; LBLH.Text = "";
                    BTNM.Visible = false;
                    BTND.Visible = false;
                    Label2.Visible = false;
                    Label3.Visible = false;
                    GVGrupos.SelectedIndex = -1;
                    DDLProf.SelectedIndex = 0;
                }
                else
                {
                    EnviaAlertas("Error", "No se pudo llevar a cabo la asignación", "error");
                }
            }
            else
            {
                EnviaAlertas("Error", "Debe seleccionar un grupo disponible y a un profesor para la asignación", "error");
            }
        }

        protected void BTNM_Click(object sender, EventArgs e)
        {
            if (GVGrupos.SelectedIndex >= 0 && DDLProf.SelectedIndex >= 0 && LBLH.Text != "")
            {
                if (LogicaNegociosProfesor.ModificarProfeGrupo(new ProfeGrupo()
                {
                    F_GrupoCuatrimestre = Convert.ToInt32(GVGrupos.SelectedRow.Cells[1].Text),
                    F_Profe = Convert.ToInt32(DDLProf.SelectedValue),
                    Extra = TB1.Text,
                    Extra_dos = TB2.Text,
                    Id_ProfeGrupo= Convert.ToInt32(LBLH.Text),
                }))
                {
                    EnviaAlertas("Correcto", "Datos de asignación modificados correctamente", "success");
                    BTNR.Visible = false;
                    Label1.Visible = false;
                    TB1.Visible = false;
                    TB2.Visible = false;
                    TB1.Text = ""; TB2.Text = ""; LBLH.Text = "";
                    BTNM.Visible = false;
                    BTND.Visible = false;
                    Label2.Visible = false;
                    Label3.Visible = false;
                    GVGrupos.SelectedIndex = -1;
                    DDLProf.SelectedIndex = 0;
                }
                else
                {
                    EnviaAlertas("Error", "No se pudo llevar a cabo la asignación", "error");
                }
            }
            else
            {
                //SELECCIONAR MSG
            }
        }

        protected void BTND_Click(object sender, EventArgs e)
        {
            if (GVGrupos.SelectedIndex >= 0 && DDLProf.SelectedIndex >= 0 && LBLH.Text != "")
            {
                if (LogicaNegociosProfesor.EliminarProfeGrupo(Convert.ToInt32(LBLH.Text)))
                {
                    EnviaAlertas("Correcto", "Asignación de grupo eliminada correctamente", "success");
                    BTNR.Visible = false;
                    Label1.Visible = false;
                    TB1.Visible = false;
                    TB2.Visible = false;
                    TB1.Text = ""; TB2.Text = ""; LBLH.Text = "";
                    BTNM.Visible = false;
                    BTND.Visible = false;
                    Label2.Visible = false;
                    Label3.Visible = false;
                    GVGrupos.SelectedIndex = -1;
                    DDLProf.SelectedIndex = 0;
                }
                else
                {
                    EnviaAlertas("Error", "No se pudo eliminar el registro", "error");
                }
            }
            else
            {
                //SELECCIONAR MSG
            }
        }
    }
}