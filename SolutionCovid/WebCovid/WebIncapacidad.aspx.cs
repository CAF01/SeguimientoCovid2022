using ClassEntidades;
using ClassLogicaNegocios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCovid
{
    public partial class WebIncapacidad : System.Web.UI.Page
    {
        LogicaNegociosProfesor LogicaNegociosProfesor;
        LogicaNegociosMedico LogicaNegociosMedico;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["NegCovProfIncap"] != null && Session["NegCovMedIncap"] != null)
                {
                    this.LogicaNegociosProfesor = (LogicaNegociosProfesor)Session["NegCovProfIncap"];
                    this.LogicaNegociosMedico = (LogicaNegociosMedico)Session["NegCovMedIncap"];
                }

                else
                {
                    this.LogicaNegociosProfesor = new LogicaNegociosProfesor(ConfigurationManager.ConnectionStrings["BaseSqlChris"].ConnectionString);
                    this.LogicaNegociosMedico = new LogicaNegociosMedico(ConfigurationManager.ConnectionStrings["BaseSqlChris"].ConnectionString);
                    Session["NegCovProfIncap"] = this.LogicaNegociosProfesor;
                    Session["NegCovMedIncap"] = this.LogicaNegociosMedico;
                }
            }
            else
            {
                this.LogicaNegociosProfesor = (LogicaNegociosProfesor)Session["NegCovProfIncap"];
                this.LogicaNegociosMedico = (LogicaNegociosMedico)Session["NegCovMedIncap"];
            }
            if (this.LogicaNegociosProfesor != null)
            {
                DataSet data = this.LogicaNegociosProfesor.MostrarCasosPositivosConFiltro();
                GVPos2.DataSource = data;
                GVPos.DataSource = data;
                GVPos.DataBind();
                GVPos2.DataBind();
                
            }
            if (!(GVInca.DataSource != null) && !(GVInca.SelectedIndex >= 0))
            {
                Img1.Visible = false;
                BTNWatch.Visible = false;
            }
        }

        public void EnviaAlertas(string titulo, string msg, string tipo)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), titulo, String.Format("registro('{0}','{1}','{2}')", titulo, msg, tipo), true);
        }

        protected void BTNR_Click(object sender, EventArgs e)
        {
            if(GVPos.SelectedIndex>=0)
            {
                if(Cal1.SelectedDate < Cal2.SelectedDate)
                {
                    string url = this.UploadFile();
                    if (url != "")
                    {
                        if (url == "LARGE")
                            EnviaAlertas("¡Error!", "La primera imagen es demasiado grande", "error");
                        if (url == "FAKE")
                            EnviaAlertas("¡Error!", "Solo se admiten archivos .pdf / .png / .jpg", "error");
                        if(url!="LARGE" && url!="FAKE")
                        {
                            int idPos = Convert.ToInt32(GVPos.SelectedRow.Cells[1].Text);
                            Incapacidad incapacidad = new Incapacidad()
                            {
                                Fecha_otorga = Cal1.SelectedDate,
                                Fecha_finalizacion = Cal2.SelectedDate,
                                IncapacidadUrl = url,
                                id_posProfe = idPos
                            };
                            if (this.LogicaNegociosProfesor.AgregarIncapacidad(incapacidad))
                            {
                                this.EnviaAlertas("Correcto!", "Incapacidad registrada", "success");
                            }
                            GVPos.SelectedIndex = -1; Cal1.SelectedDate = DateTime.Today;
                            Cal2.SelectedDate = DateTime.Today.AddDays(1);
                        }
                    }
                }
                else
                {
                    this.EnviaAlertas("error", "La fecha en que se otorga de ser menor a la fecha de finalización","error");
                }
            }
            else
            {
                this.EnviaAlertas("error", "Debes seleccionar un caso Positivo de la tabla", "error");
            }
        }

        public string UploadFile()
        {
            string extension;
            string Url = "";
            string FileName;
            string imgPath;

            int fileSize;

            if (FU1.HasFile)
            {
                extension = Path.GetExtension(FU1.PostedFile.FileName);
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                {
                    FileName = FU1.FileName;
                    fileSize = FU1.PostedFile.ContentLength;
                    if (fileSize > 1002400)
                        Url = "LARGE";
                    else
                    {
                        imgPath = "incapacidad/imgPruebas/";
                        Url = imgPath + FileName;
                        FU1.SaveAs(Server.MapPath(Url));
                    }
                }
                if (extension.ToLower() == ".pdf")
                {
                    FileName = FU1.FileName;
                    fileSize = FU1.PostedFile.ContentLength;
                    if (fileSize > 1002400)
                        Url = "LARGE";
                    else
                    {
                        imgPath = "incapacidad/pdfPruebas/";
                        Url = imgPath + FileName;
                        FU1.SaveAs(Server.MapPath(Url));
                    }
                }
                else
                    Url = "FAKE";
            }
            return Url;
        }

        public string UploadFileV2()
        {
            string extension;
            string Url = "";
            string FileName;
            string imgPath;

            int fileSize;

            if (FU2.HasFile)
            {
                extension = Path.GetExtension(FU2.PostedFile.FileName);
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                {
                    FileName = FU2.FileName;
                    fileSize = FU2.PostedFile.ContentLength;
                    if (fileSize > 1002400)
                    {
                        Url = "LARGE";
                    }
                    else
                    {
                        imgPath = "incapacidad/imgPruebas/";
                        Url = imgPath + FileName;
                        FU2.SaveAs(Server.MapPath(Url));
                    }
                }
                if (extension.ToLower() == ".pdf")
                {
                    FileName = FU2.FileName;
                    fileSize = FU2.PostedFile.ContentLength;
                    if (fileSize > 1002400)
                        Url = "LARGE";
                    else
                    {
                        imgPath = "incapacidad/pdfPruebas/";
                        Url = imgPath + FileName;
                        FU2.SaveAs(Server.MapPath(Url));
                    }
                }
                if (extension.ToLower() != ".pdf" && extension.ToLower() != ".png" && extension.ToLower() != ".jpg")
                    Url = "FAKE";
            }
            else
            {
                Url = lblurlS.Text;
            }
            return Url;
        }

        protected void BTNMod_Click(object sender, EventArgs e)
        {
            if(GVInca.SelectedIndex>=0 && GVPos2.SelectedIndex>=0)
            {
                if (Cal3.SelectedDate < Cal4.SelectedDate)
                {
                    string url = this.UploadFileV2();
                    if (url != "")
                    {
                        if(url=="LARGE")
                            EnviaAlertas("¡Error!", "La primera imagen es demasiado grande", "error");
                        if(url=="FAKE")
                            EnviaAlertas("¡Error!", "Solo se admiten archivos .pdf / .png / .jpg", "error");
                        if (url != "LARGE" && url != "FAKE")
                        {
                            int idPos = Convert.ToInt32(GVPos2.SelectedRow.Cells[1].Text);
                            Incapacidad incapacidad = new Incapacidad()
                            {
                                Fecha_otorga = Cal3.SelectedDate,
                                Fecha_finalizacion = Cal4.SelectedDate,
                                IncapacidadUrl = url,
                                id_Incapacidad = idPos
                            };
                            if (this.LogicaNegociosProfesor.ModificarIncapacidad(incapacidad))
                            {
                                this.EnviaAlertas("Correcto!", "Registro de Incapacidad Actualizada correctamente", "success");
                                LimpiarGVIncapacidad();

                            }
                            GVPos2.SelectedIndex = -1; Cal3.SelectedDate = DateTime.Today;
                            Cal4.SelectedDate = DateTime.Today.AddDays(1);
                        }
                    }
                }
                else
                {
                    this.EnviaAlertas("error", "La fecha en que se otorga debe ser menor a la fecha de finalización", "error");
                }
            }
            else
            {
                this.EnviaAlertas("Algo fallo", "Debes seleccionar un registro de incapacidad", "info");
            }
        }

        protected void BTNDel_Click(object sender, EventArgs e)
        {
            if (GVInca.SelectedIndex >= 0 && GVPos2.SelectedIndex >= 0)
            {
                int idPos = Convert.ToInt32(GVPos2.SelectedRow.Cells[1].Text);
                if (this.LogicaNegociosProfesor.EliminarIncapacidad(idPos))
                {
                    this.EnviaAlertas("Correcto!", "Registro de Incapacidad eliminado Correctamente", "success");
                    LimpiarGVIncapacidad();
                }
                GVPos2.SelectedIndex = -1; Cal3.SelectedDate = DateTime.Today;
                Cal4.SelectedDate = DateTime.Today.AddDays(1);
            }
            else
            {
                this.EnviaAlertas("Error", "Debe seleccionar un registro de incapacidad", "error");
            }
        }

        protected void GVInca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GVInca.SelectedIndex >= 0)
            {
                Cal3.SelectedDate = Convert.ToDateTime(GVInca.SelectedRow.Cells[2].Text);
                Cal4.SelectedDate= Convert.ToDateTime(GVInca.SelectedRow.Cells[3].Text);
                string[] url = lblurl.Text.Split('|');//Podria fallar jeje
                lblurlS.Text = url[GVInca.SelectedIndex];
                Img1.Visible = true;
                BTNWatch.Visible = true;
                this.EnviaAlertas("Correcto", "Se ha seleccionado un registro de incapacidad, ya se pueden realizar modifaciones o eliminación", "success");
            }
        }

        protected void GVPos2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(GVPos2.SelectedIndex>=0)
            {
                ActualizarIncapacidadesList();
            }
        }

        public void ActualizarIncapacidadesList()
        {
            List<string> list = new List<string>();
            int id = Convert.ToInt32(GVPos2.SelectedRow.Cells[1].Text);
            GVInca.DataSource = this.LogicaNegociosProfesor.MostrarIncapacidadesPorCaso(id, list);
            GVInca.DataBind();
            if (list.Count >= 1)
            {
                foreach (string text in list)
                {
                    lblurl.Text += text + "|";
                }
            }
            GVInca.SelectedIndex = -1;
        }

        public void LimpiarGVIncapacidad()
        {
            GVInca.DataSource = null;
            GVInca.DataBind();
            lblurl.Text = "";
            lblurlS.Text = "";
            Img1 = new Image();
            Img1.Visible = false;
            BTNWatch.Visible = false;
        }

        protected void BTNWatch_Click(object sender, EventArgs e)
        {
            if (lblurlS.Text.Contains(".pdf"))
            {
                Img1.ImageUrl = "";
                string FilePath = Server.MapPath(lblurlS.Text);
                System.IO.FileStream fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] ar = new byte[(int)fs.Length];
                fs.Read(ar, 0, (int)fs.Length);
                fs.Close();

                Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + FilePath);

                Response.BinaryWrite(ar);
            }
            else
            {
                Img1.Visible = true;
                Img1.ImageUrl = "~/" + lblurlS.Text;
            }
        }
    }
}