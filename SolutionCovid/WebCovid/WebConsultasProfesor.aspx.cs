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
    public partial class WebConsultasProfesor : System.Web.UI.Page
    {
        LogicaNegociosCuatrimestre logicaNegociosCuatrimestre;
        LogicaNegociosProfesor logicaNegociosProfesor;
        List<ProgramaEducativo> programaEducativos;
        List<Cuatrimestre> Cuatrimestres;
        List<Incapacidad> incapacidads1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                logicaNegociosCuatrimestre = new LogicaNegociosCuatrimestre(ConfigurationManager.ConnectionStrings["conexSql"].ConnectionString);
                logicaNegociosProfesor = new LogicaNegociosProfesor(ConfigurationManager.ConnectionStrings["conexSql"].ConnectionString);
                Session["LogCuatriConP"] = logicaNegociosCuatrimestre;
                Session["LogProfConP"] = logicaNegociosProfesor;
                string msg = "";

                programaEducativos = logicaNegociosCuatrimestre.obtenerColeccionProgrEd(ref msg);
                Cuatrimestres = logicaNegociosCuatrimestre.obtenerListaCuatrimestres(ref msg);
                GVProfesores.DataSource = logicaNegociosProfesor.MostrarProfesores();
                GVProfesores.DataBind();

                DDLProgram.Items.Clear();
                DDLCuatri.Items.Clear();
                ListItem listItem;
                foreach (ProgramaEducativo item in programaEducativos)
                {
                    listItem = new ListItem();
                    listItem.Text = item.programaEd;
                    listItem.Value = item.id.ToString();
                    DDLProgram.Items.Add(listItem);
                }
                foreach (Cuatrimestre item in Cuatrimestres)
                {
                    listItem = new ListItem();
                    listItem.Text = String.Format("{0}  -  Año: {1}", item.periodo, item.anio);
                    listItem.Value = item.id.ToString();
                    DDLCuatri.Items.Add(listItem);
                }
                DesaparecerControles();

            }
            else
            {
                logicaNegociosCuatrimestre = (LogicaNegociosCuatrimestre)Session["LogCuatriConP"];
                logicaNegociosProfesor = (LogicaNegociosProfesor)Session["LogProfConP"];
            }
        }

        public void DesaparecerControles()
        {
            LBL1.Visible = false;
            LBL2.Visible = false;
            LBL3.Visible = false;
            LBL4.Visible = false;
            LBL5.Visible = false;
            BTNPrueba.Visible = false;
            Img1.Visible = false;
            BTNInca.Visible = false;
            DDLInca.Visible = false;
            BTNShowInca.Visible = false;
            Img2.Visible = false;
            BTNShowSegui.Visible = false;
            GVSeguimientos.DataSource = null;
            GVSeguimientos.DataBind();
        }

        public void EnviaAlertas(string titulo, string msg, string tipo)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), titulo, String.Format("registro('{0}','{1}','{2}')", titulo, msg, tipo), true);
        }

        protected void BTNMostrarDatos_Click(object sender, EventArgs e)
        {
            int idP = Convert.ToInt32(DDLProgram.SelectedValue);
            int idC = Convert.ToInt32(DDLCuatri.SelectedValue);
            GVContagios.DataSource=logicaNegociosProfesor.MostrarContagiadosPorFiltroProgramaCuatrimestre(idP,idC);
            GVContagios.DataBind();
            if(GVContagios.DataSource!=null)
            {
                for(int a=0; a<GVContagios.Rows.Count;a++)
                {
                    string[] dateFixed = GVContagios.Rows[a].Cells[5].Text.Split(' ');
                    GVContagios.Rows[a].Cells[5].Text = dateFixed[0];
                }
                EnviaAlertas("Datos actualizados", "Registro de contagios, actualizado", "success");
            }
            else
            {
                EnviaAlertas("Datos no encontrados", "No se hallaron registros", "info");
            }
            
        }

        protected void GVProfesores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(GVProfesores.SelectedIndex>=0)
            {
                int id = Convert.ToInt32(GVProfesores.SelectedRow.Cells[1].Text);
                GVCasos.DataSource = logicaNegociosProfesor.BuscarCasosPositivoDeProfesor(id);
                GVCasos.DataBind();
                GVCasos.SelectedIndex = -1;
                DesaparecerControles();
                if (GVCasos.DataSource != null)
                {
                    LBL1.Visible = true;
                    GVCasos.Visible = true;
                }
                else
                    EnviaAlertas("Información", "No se encontraron registros de casos", "info");
                

            }
        }

        protected void GVCasos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(GVCasos.SelectedIndex>=0)
            {
                int idCaso=Convert.ToInt32(GVCasos.SelectedRow.Cells[1].Text);
                if(idCaso>0)
                {
                    LBL2.Visible = true;
                    LBL3.Visible = true;
                    BTNPrueba.Visible = true;
                    BTNInca.Visible = true;
                    LBL5.Visible = true;
                    BTNShowSegui.Visible = true;
                }
                else
                {
                    LBL2.Visible = false;
                    LBL3.Visible = false;
                    BTNPrueba.Visible = false;
                    BTNInca.Visible = false;
                    LBL5.Visible = false;
                    BTNShowSegui.Visible = false;
                }
            }
            else
            {
                LBL2.Visible = false;
                LBL3.Visible = false;
                BTNPrueba.Visible = false;
                BTNInca.Visible = false;
                LBL5.Visible = false;
                BTNShowSegui.Visible = false;
            }
        }

        protected void BTNPrueba_Click(object sender, EventArgs e)
        {
            if(GVCasos.SelectedIndex>=0)
            {
                string url = GVCasos.Rows[GVCasos.SelectedIndex].Cells[3].Text;
                if (url.Contains(".pdf"))
                {
                    Img1.ImageUrl = "";
                    string FilePath = Server.MapPath(url);
                    System.IO.FileStream fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    byte[] ar = new byte[(int)fs.Length];
                    fs.Read(ar, 0, (int)fs.Length);
                    fs.Close();

                    Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + FilePath);

                    Response.BinaryWrite(ar);
                }
                if(url.Contains(".png") || url.Contains(".jpg"))
                {
                    Img1.Visible = true;
                    Img1.ImageUrl = "~/" + url;
                }
            }
            
        }

        protected void BTNInca_Click(object sender, EventArgs e)
        {
            if(GVCasos.SelectedIndex>=0)
            {
                int idCaso = Convert.ToInt32(GVCasos.SelectedRow.Cells[1].Text);
                List<Incapacidad> incapacidads = logicaNegociosProfesor.MostrarPeriodosIncapacidadPorCaso(idCaso);
                if(incapacidads.Count>0)
                {
                    Session["incapacidades"] = incapacidads;
                    DDLInca.Items.Clear();
                    ListItem listItem;
                    foreach(Incapacidad item in incapacidads)
                    {
                        listItem = new ListItem();
                        listItem.Text = String.Format("Incapacidad otorgada: {0} | Finaliza: {1}", item.Fecha_otorga.ToString("dd/MM/yyyy"), item.Fecha_finalizacion.ToString("dd/MM/yyyy"));
                        listItem.Value = item.id_Incapacidad.ToString();
                        DDLInca.Items.Add(listItem);
                    }
                    DDLInca.Visible = true;
                    BTNShowInca.Visible = true;
                    EnviaAlertas("Correcto", "Incapacidades encontradas, seleccione en el cuadro", "success");
                }
                else
                {
                    DDLInca.Visible = false;
                    BTNShowInca.Visible = false;
                    Img2.Visible = false;
                    Img2.ImageUrl = null;
                    Session["incapacidades"] = null;
                    EnviaAlertas("Información", "No se encontraron incapacidades registradas", "info");
                }
            }
        }

        protected void BTNShowSegui_Click(object sender, EventArgs e)
        {
            if(GVCasos.SelectedIndex>=0)
            {
                int idCaso = Convert.ToInt32(GVCasos.SelectedRow.Cells[1].Text);
                GVSeguimientos.DataSource = logicaNegociosProfesor.MostrarSeguimientoDeCaso(idCaso);
                GVSeguimientos.DataBind();
                if(GVSeguimientos.DataSource==null)
                {
                    EnviaAlertas("Información", "El caso no cuenta con seguimientos registrados", "info");
                }
            }
        }

        protected void BTNShowInca_Click(object sender, EventArgs e)
        {
            if (DDLInca.SelectedIndex >= 0)
            {
                string url = incapacidads1[DDLInca.SelectedIndex].IncapacidadUrl;
                if (url.Contains(".pdf"))
                {
                    Img2.ImageUrl = "";
                    string FilePath = Server.MapPath(url);
                    System.IO.FileStream fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    byte[] ar = new byte[(int)fs.Length];
                    fs.Read(ar, 0, (int)fs.Length);
                    fs.Close();

                    Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + FilePath);

                    Response.BinaryWrite(ar);
                }
                if(url.Contains(".png") || url.Contains(".jpg"))
                {
                    Img2.Visible = true;
                    Img2.ImageUrl = "~/" + url;
                }
            }
        }

        protected void DDLProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DDLCuatri_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DDLInca_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}