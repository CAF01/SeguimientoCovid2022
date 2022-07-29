using ClassEntidades;
using ClassLogicaNegocios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCovid
{
    public partial class WebSeguimientoCasoProfe : System.Web.UI.Page
    {
        LogicaNegociosProfesor LogicaNegociosProfesor;
        LogicaNegociosMedico LogicaNegociosMedico;
        List<SeguimientoProfesor> seguimientoProfesors1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["NegociosCovidProfSeguimiento"] != null && Session["NegociosMedSeguimiento"]!=null)
                {
                    this.LogicaNegociosProfesor = (LogicaNegociosProfesor)Session["NegociosCovidProfSeguimiento"];
                    this.LogicaNegociosMedico = (LogicaNegociosMedico)Session["NegociosMedSeguimiento"];
                    this.seguimientoProfesors1 = (List<SeguimientoProfesor>)Session["SeguimientosList"];
                }

                else
                {
                    this.LogicaNegociosProfesor = new LogicaNegociosProfesor(ConfigurationManager.ConnectionStrings["BaseSqlChris"].ConnectionString);
                    this.LogicaNegociosMedico = new LogicaNegociosMedico(ConfigurationManager.ConnectionStrings["BaseSqlChris"].ConnectionString);
                    Session["NegociosCovidProfSeguimiento"] = this.LogicaNegociosProfesor;
                    Session["NegociosMedSeguimiento"] = this.LogicaNegociosMedico;
                    Session["SeguimientosList"] = this.seguimientoProfesors1;
                }
            }
            else
            {
                this.LogicaNegociosProfesor = (LogicaNegociosProfesor)Session["NegociosCovidProfSeguimiento"];
                this.LogicaNegociosMedico = (LogicaNegociosMedico)Session["NegociosMedSeguimiento"];
                this.seguimientoProfesors1 = (List<SeguimientoProfesor>)Session["SeguimientosList"];

            }
            if(this.LogicaNegociosProfesor != null)
            {
                GVPositivos.DataSource = this.LogicaNegociosProfesor.MostrarCasosPositivosConFiltro();
                string msg = "";
                GVMedicos.DataSource = this.LogicaNegociosMedico.consultarMedicos(ref msg);
                GVPositivos.DataBind();
                GVMedicos.DataBind();
            }
        }

        public void EnviaAlertas(string titulo, string msg, string tipo)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), titulo, String.Format("registro('{0}','{1}','{2}')", titulo, msg, tipo), true);
        }

        protected void BTNRegist_Click(object sender, EventArgs e)
        {
            if(GVMedicos!=null && GVPositivos!=null && (GVPositivos.SelectedIndex>=0 && GVMedicos.SelectedIndex>=0))
            {
                if(DDLComunica.SelectedIndex>=0)
                {
                    if (this.LogicaNegociosProfesor.AgregarSeguimientoCaso(new SeguimientoProfesor()
                    {
                        F_positivoProfe = Convert.ToInt32(GVPositivos.SelectedRow.Cells[1].Text),
                        F_medico = Convert.ToInt32(GVMedicos.SelectedRow.Cells[1].Text),
                        Fecha = DateTime.Today,
                        Form_Comunica = DDLComunica.SelectedItem.ToString(),
                        Reporte = TB1.Text,
                        Entrevista = TB2.Text,
                        Extra = TB3.Text
                    }))
                    {
                        this.EnviaAlertas("Correcto", "Seguimiento al caso agregado correctamente", "success");
                    }
                    else
                    {
                        this.EnviaAlertas("erorr", "Algo falló con la inserción", "error");
                    }
                    TB1.Text = ""; TB2.Text = ""; TB3.Text = ""; DDLComunica.SelectedIndex = 0;
                    GVMedicos.SelectedIndex = -1; GVPositivos.SelectedIndex = -1;
                }
                else
                {
                    this.EnviaAlertas("Info", "Debe seleccionar una forma de comunicación", "error");
                }
            }
            else
            {
                this.EnviaAlertas("Info","Debe seleccionar a un médico y un caso POSITIVO", "error");
            }
        }

        protected void GVPositivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(GVPositivos.SelectedIndex>=0)
            {
                DDLSeguimientos.Items.Clear();
                List<SeguimientoProfesor> seguimientoProfesors = this.LogicaNegociosProfesor.MostrarSeguimientoDeCaso(
                    Convert.ToInt32(GVPositivos.SelectedRow.Cells[1].Text));
                if (seguimientoProfesors!=null && seguimientoProfesors.Count >= 1)
                {
                    this.seguimientoProfesors1 = seguimientoProfesors;
                    Session["SeguimientosList"] = this.seguimientoProfesors1;
                    ListItem listItem = new ListItem();
                    listItem.Text = "--SELECCIONAR CASO DE SEGUIMIENTO";
                    listItem.Value = "-1";
                    DDLSeguimientos.Items.Add(listItem);
                    foreach (SeguimientoProfesor seguimientoProfesor in seguimientoProfesors)
                    {
                        listItem = new ListItem();
                        string[] dateFixed = seguimientoProfesor.Fecha.ToString().Split(' ');
                        listItem.Text = String.Format("Registro: {0}, fecha:{1}, reporte:{2}", seguimientoProfesor.id_Segui, dateFixed[0], seguimientoProfesor.Reporte);
                        listItem.Value = seguimientoProfesor.id_Segui.ToString();
                        DDLSeguimientos.Items.Add(listItem);
                    }
                    DDLSeguimientos.Visible = true;
                }
                else
                {
                    DDLSeguimientos.Items.Clear();
                    DDLSeguimientos.Visible = false;
                    GVMedicos.SelectedIndex = -1;
                    DDLComunica2.SelectedIndex = 0;
                    Calendar.SelectedDate = DateTime.Today;
                    TB12.Text = "";
                    TB22.Text = "";
                    TB32.Text = "";
                }
            }
            DDLSeguimientos.SelectedIndex = -1;
        }

        protected void DDLSeguimientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DDLSeguimientos.SelectedIndex>=1)
            {
                int index=DDLSeguimientos.SelectedIndex;
                SeguimientoProfesor seguimiento = this.seguimientoProfesors1[index-1];
                if (seguimiento.Form_Comunica == "Vía Whatsapp")
                    DDLComunica2.SelectedIndex = 0;
                if (seguimiento.Form_Comunica == "Personal")
                    DDLComunica2.SelectedIndex = 1;
                if (seguimiento.Form_Comunica == "Vía teléfonica")
                    DDLComunica2.SelectedIndex = 2;
                if (seguimiento.Form_Comunica == "Correo Electrónico")
                    DDLComunica2.SelectedIndex = 3;
                TB12.Text = seguimiento.Reporte;
                TB22.Text = seguimiento.Entrevista;
                TB32.Text = seguimiento.Extra;
                Calendar.SelectedDate = seguimiento.Fecha;
            }
            else
            {
                TB12.Text = "";
                TB22.Text = "";
                TB32.Text = "";
                DDLComunica2.SelectedIndex = 0;
                Calendar.SelectedDate = DateTime.Today;
            }
        }

        protected void BTNMod_Click(object sender, EventArgs e)
        {
            if(DDLSeguimientos.SelectedIndex>=1 && GVMedicos.SelectedIndex>=0)
            {
                if(!String.IsNullOrEmpty(TB12.Text) && DDLComunica2.SelectedIndex>=0)
                {
                    if(Calendar.SelectedDate > DateTime.Today)
                    {
                        this.EnviaAlertas("info", "La fecha no puede ser del futuro", "info");
                    }
                    else
                    {
                        if(this.LogicaNegociosProfesor.ModificarSeguimientoCaso(new SeguimientoProfesor()
                        {
                            id_Segui = Convert.ToInt32(DDLSeguimientos.SelectedValue),
                            F_medico = Convert.ToInt32(GVMedicos.SelectedRow.Cells[1].Text),
                            Form_Comunica = DDLComunica2.SelectedItem.Text,
                            Fecha = Calendar.SelectedDate,
                            Reporte = TB12.Text,
                            Entrevista = TB22.Text,
                            Extra = TB32.Text
                        }))
                        {
                            DDLSeguimientos.Items.Clear();
                            DDLSeguimientos.Visible = false;
                            GVMedicos.SelectedIndex = -1;
                            GVPositivos.SelectedIndex = -1;
                            DDLComunica2.SelectedIndex = 0;
                            Calendar.SelectedDate=DateTime.Today;
                            TB12.Text = "";
                            TB22.Text = "";
                            TB32.Text = "";
                            this.EnviaAlertas("Correcto", "Datos de seguimiento actualizados", "success");
                        }
                    }

                }
            }
            else
            {
                this.EnviaAlertas("error", "Debe seleccionar un seguimiento de caso y a un doctor", "error");
            }
        }

        protected void Calendar_SelectionChanged(object sender, EventArgs e)
        {

        }

        protected void BTNDel_Click(object sender, EventArgs e)
        {
            if (DDLSeguimientos.SelectedIndex >= 1)
            {
                if (this.LogicaNegociosProfesor.EliminarSeguimientoCaso(Convert.ToInt32(DDLSeguimientos.SelectedValue)))
                {
                    DDLSeguimientos.Items.Clear();
                    DDLSeguimientos.Visible = false;
                    GVMedicos.SelectedIndex = -1;
                    GVPositivos.SelectedIndex = -1;
                    DDLComunica2.SelectedIndex = 0;
                    Calendar.SelectedDate = DateTime.Today;
                    TB12.Text = "";
                    TB22.Text = "";
                    TB32.Text = "";
                    this.EnviaAlertas("Correcto", "Seguimiento de caso eliminado correctamente", "success");
                }
                else
                {
                    this.EnviaAlertas("error", "No se pudo eliminar el seguimiento", "error");
                }
            }
            else
                this.EnviaAlertas("Error","Seleccione un seguimiento de caso válido","info");
        }
    }
}