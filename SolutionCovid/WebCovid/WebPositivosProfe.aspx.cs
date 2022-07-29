using ClassEntidades;
using ClassLogicaNegocios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;

namespace WebCovid
{
    public partial class WebPositivosProfe : System.Web.UI.Page
    {
        LogicaNegociosProfesor NegociosProfesor;
        List<Profesor> Profesors;
        List<string> comprobaciones;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["NegociosCovidProf"] != null)
                {
                    this.NegociosProfesor = (LogicaNegociosProfesor)Session["NegociosCovidProf"];
                    this.Profesors = (List<Profesor>)Session["ProfsCovid"];
                    this.comprobaciones = (List<string>)Session["ListaComp"];
                }

                else
                {
                    this.NegociosProfesor = new LogicaNegociosProfesor(ConfigurationManager.ConnectionStrings["BaseSqlChris"].ConnectionString);
                    Session["NegociosCovidProf"] = this.NegociosProfesor;
                    this.Profesors = this.NegociosProfesor.MostrarProfesores();
                    Session["ProfsCovid"] = this.Profesors;
                    this.comprobaciones = this.NegociosProfesor.DevolverRutasdeCasosCovid();
                    Session["ListaComp"] = this.comprobaciones;
                    MostrarDatosTabla();
                    Button1.Visible = false;
                }
            }
            else
            {
                this.NegociosProfesor = (LogicaNegociosProfesor)Session["NegociosCovidProf"];
                this.Profesors = (List<Profesor>)Session["ProfsCovid"];
                this.comprobaciones = (List<string>)Session["ListaComp"];
            }
            if (this.NegociosProfesor != null)
            {
                string msg = "";
                this.GVProfesor.DataSource = this.NegociosProfesor.MostrarProfesoresPocaInfo(ref msg);
                this.GVProfesor.DataBind();
            }
            
        }

        protected void BTNR_Click(object sender, EventArgs e)
        {
            if (GVProfesor.SelectedIndex >= 0)
            {
                if (CalConfirma.SelectedDate > DateTime.Today)
                {
                    this.EnviaAlertas("error", "La fecha no puede ser del futuro", "error");
                }
                else
                {
                    if (DDRiesgo.SelectedIndex >= 0)
                    {
                        if (!String.IsNullOrEmpty(TB2.Text))
                        {
                            string url = this.UploadFile();
                            if (url != "")
                            {
                                if(url=="FAKE")
                                    this.EnviaAlertas("error", "Solo se admiten archivos .png / .jpg / .pdf", "error");
                                else
                                {
                                      int idProf = this.Profesors[GVProfesor.SelectedIndex].ID_Profe;
                                      PositivoProfe positivoProfe = new PositivoProfe()
                                      {
                                          FechaConfirmado = CalConfirma.SelectedDate,
                                          Comprobacion = url,
                                          Antecedentes = TB1.Text,
                                          NumContagio = Convert.ToByte(TB2.Text),
                                          Extra = TB3.Text,
                                          F_Profe = idProf,
                                          Riesgo = DDRiesgo.SelectedItem.Text
                                      };
                                      if (this.NegociosProfesor.AgregarCasoPositivo(positivoProfe))
                                      {
                                          this.EnviaAlertas("Correcto!", "Caso positivo agregado", "success");
                                          this.comprobaciones = this.NegociosProfesor.DevolverRutasdeCasosCovid();
                                          Session["ListaComp"] = this.comprobaciones;
                                      }
                                      TB1.Text = ""; TB2.Text = ""; TB3.Text = ""; GVProfesor.SelectedIndex = -1;
                                }
                                
                            }
                            else
                            {
                                this.EnviaAlertas("error", "Debe subir el archivo o imagen de validez", "error");
                            }
                        }
                        else
                        {
                            this.EnviaAlertas("error", "Es necesario indicar el número de contagio", "error");
                        }

                    }
                    else
                        this.EnviaAlertas("error", "Debes seleccionar el nivel de riesgo", "error");
                }
            }
            else
                this.EnviaAlertas("error", "Debes seleccionar a un profesor del listado", "error");
        }

        public string UploadFile()
        {
            string extension;
            string Url = "";
            string FileName;
            string imgPath;

            int fileSize;

            if (FilePDFImg.HasFile)
            {
                extension = Path.GetExtension(FilePDFImg.PostedFile.FileName);
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                {
                    FileName = FilePDFImg.FileName;
                    fileSize = FilePDFImg.PostedFile.ContentLength;
                    if (fileSize > 1002400)
                    {
                        this.EnviaAlertas("¡Error!", "La primera imagen es demasiado grande", "error");
                    }
                    else
                    {
                        imgPath = "comprobacion/imgPruebas/";
                        Url = imgPath + FileName;
                        FilePDFImg.SaveAs(Server.MapPath(Url));
                    }
                }
                if (extension.ToLower() == ".pdf")
                {
                    FileName = FilePDFImg.FileName;
                    fileSize = FilePDFImg.PostedFile.ContentLength;
                    if (fileSize > 1002400)
                        this.EnviaAlertas("¡Error!", "El archivo PDF es demasiado grande", "error");
                    else
                    {
                        imgPath = "comprobacion/pdfPruebas/";
                        Url = imgPath + FileName;
                        FilePDFImg.SaveAs(Server.MapPath(Url));
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

            if (FilePDFImg.HasFile)
            {
                extension = Path.GetExtension(FilePDFImg.PostedFile.FileName);
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                {
                    FileName = FilePDFImg.FileName;
                    fileSize = FilePDFImg.PostedFile.ContentLength;
                    if (fileSize > 1002400)
                    {
                        this.EnviaAlertas("¡Error!", "La primera imagen es demasiado grande", "error");
                    }
                    else
                    {
                        imgPath = "comprobacion/imgPruebas/";
                        Url = imgPath + FileName;
                        FilePDFImg.SaveAs(Server.MapPath(Url));
                    }
                }
                if (extension.ToLower() == ".pdf")
                {
                    FileName = FilePDFImg.FileName;
                    fileSize = FilePDFImg.PostedFile.ContentLength;
                    if (fileSize > 1002400)
                        this.EnviaAlertas("¡Error!", "El archivo PDF es demasiado grande", "error");
                    else
                    {
                        imgPath = "comprobacion/pdfPruebas/";
                        Url = imgPath + FileName;
                        FilePDFImg.SaveAs(Server.MapPath(Url));
                    }
                }
                else
                    this.EnviaAlertas("¡Info!", "Solo se aceptan extensiones de imagen .jpg / .png / .pdf", "info");
            }
            else
            {
                Url = lblurl.Text;
            }
            return Url;
        }

        public void EnviaAlertas(string titulo, string msg, string tipo)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), titulo, String.Format("registro('{0}','{1}','{2}')", titulo, msg, tipo), true);
        }

        protected void BTNTabla_Click(object sender, EventArgs e)
        {
            if (this.NegociosProfesor != null)
            {
                bool flag = MostrarDatosTabla();
                if (flag)
                    this.EnviaAlertas("Correcto", "Datos actualizados", "success");
                else
                    this.EnviaAlertas("Oops!", "No se halló información registrada", "info");
            }
        }

        public bool MostrarDatosTabla()
        {
            DataSet dataSet = this.NegociosProfesor.DevolverCasosPositivosCovid();
            if(dataSet!=null)
            {
                GVPositivos.DataSource = dataSet;
                GVPositivos.DataBind();
                for (int a = 0; a < GVPositivos.Rows.Count; a++)
                {
                    string[] fecha = GVPositivos.Rows[a].Cells[2].Text.Split(' ');
                    GVPositivos.Rows[a].Cells[2].Text = fecha[0];
                }
                GVPositivos.SelectedIndex = -1;
                return true;
            }
            return false;
            
        }

        protected void BTNMod_Click(object sender, EventArgs e)
        {
            if (GVPositivos.SelectedIndex >= 0)
            {
                if (Cal2.SelectedDate > DateTime.Today)
                {
                    this.EnviaAlertas("error", "La fecha no puede ser del futuro", "error");
                }
                else
                {
                    if (DDL12.SelectedIndex >= 0)
                    {
                        if (!String.IsNullOrEmpty(TB22.Text))
                        {
                            string url = this.UploadFileV2();
                            if (url != "")
                            {
                                int idProf = Convert.ToInt32(this.GVPositivos.SelectedRow.Cells[1].Text);
                                PositivoProfe positivoProfe = new PositivoProfe()
                                {
                                    Id_posProfe = idProf,
                                    FechaConfirmado = Cal2.SelectedDate,
                                    Comprobacion = url,
                                    Antecedentes = TB12.Text,
                                    NumContagio = Convert.ToByte(TB22.Text),
                                    Extra = TB33.Text,
                                    F_Profe = Convert.ToInt32(this.GVPositivos.SelectedRow.Cells[6].Text),
                                    Riesgo = DDL12.SelectedItem.Text
                                };
                                if (this.NegociosProfesor.ModificarCasoPositivo(positivoProfe))
                                {
                                    this.EnviaAlertas("Correcto!", "Caso Positivo, Actualizado correctamente", "success");
                                    this.comprobaciones = this.NegociosProfesor.DevolverRutasdeCasosCovid();
                                    Session["ListaComp"] = this.comprobaciones;
                                }
                                GVPositivos.Rows[GVPositivos.SelectedIndex].Cells[2].Text = Cal2.SelectedDate.ToString();
                                GVPositivos.Rows[GVPositivos.SelectedIndex].Cells[3].Text = TB12.Text;
                                GVPositivos.Rows[GVPositivos.SelectedIndex].Cells[4].Text = TB22.Text;
                                GVPositivos.Rows[GVPositivos.SelectedIndex].Cells[5].Text = TB33.Text;
                                GVPositivos.Rows[GVPositivos.SelectedIndex].Cells[7].Text = DDL12.SelectedItem.Text;
                                DDL12.SelectedIndex = 0; Cal2.SelectedDate = DateTime.Today;
                                TB12.Text = ""; TB22.Text = ""; TB33.Text = "";
                                Image1.ImageUrl = null;
                                MostrarDatosTabla();
                                Button1.Visible = false;
                            }
                        }
                    }
                    else
                        this.EnviaAlertas("error", "Debes seleccionar el nivel de riesgo", "error");
                }
            }
            else
            {
                this.EnviaAlertas("error", "Debes seleccionar un caso Positivo", "error");
            }

        }

        protected void GVPositivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GVPositivos.SelectedIndex >= 0)
            {
                Button1.Visible = true;
                lblurl.Text = this.comprobaciones[GVPositivos.SelectedIndex];
                Cal2.SelectedDate = Convert.ToDateTime(GVPositivos.Rows[GVPositivos.SelectedIndex].Cells[2].Text);
                TB12.Text = GVPositivos.Rows[GVPositivos.SelectedIndex].Cells[3].Text;
                TB22.Text = GVPositivos.Rows[GVPositivos.SelectedIndex].Cells[4].Text;
                TB33.Text = GVPositivos.Rows[GVPositivos.SelectedIndex].Cells[5].Text;
                if (GVPositivos.Rows[GVPositivos.SelectedIndex].Cells[7].Text == "Bajo")
                    DDL12.SelectedIndex = 0;
                if (GVPositivos.Rows[GVPositivos.SelectedIndex].Cells[7].Text == "Medio")
                    DDL12.SelectedIndex = 1;
                if (GVPositivos.Rows[GVPositivos.SelectedIndex].Cells[7].Text == "Alto")
                    DDL12.SelectedIndex = 2;
            }
        }

        protected void BTNDel_Click(object sender, EventArgs e)
        {
            if (GVPositivos.SelectedIndex >= 0)
            {
                int CasPositivoId = Convert.ToInt32(this.GVPositivos.SelectedRow.Cells[1].Text);
                if (this.NegociosProfesor.EliminarCasoPositivo(CasPositivoId))
                {
                    comprobaciones.RemoveAt(comprobaciones.FindIndex(x => x == lblurl.Text));
                    this.EnviaAlertas("Eliminado!", "Se eliminó el caso positivo", "success");
                    DDL12.SelectedIndex = 0; Cal2.SelectedDate = DateTime.Today;
                    TB12.Text = ""; TB22.Text = ""; TB33.Text = ""; Image1.ImageUrl = null;
                    MostrarDatosTabla();
                    Button1.Visible = false;
                }
                else
                {
                    this.EnviaAlertas("error", "Algo falló con la eliminación", "info");
                }
            }
            else
            {
                this.EnviaAlertas("error", "Debes seleccionar un caso Positivo", "error");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(lblurl.Text.Contains(".pdf"))
            {
                Image1.ImageUrl = "";
                string FilePath = Server.MapPath(lblurl.Text);
                System.IO.FileStream fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] ar = new byte[(int)fs.Length];
                fs.Read(ar, 0, (int)fs.Length);
                fs.Close();

                Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + FilePath);

                Response.BinaryWrite(ar);

                //WebClient User = new WebClient();

                //Byte[] FileBuffer = User.DownloadData(FilePath);
                //if (FileBuffer != null)
                //{
                //    Response.ContentType = "application/pdf";
                //    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                //    Response.TransmitFile(FileBuffer.ToString());
                //}
            }
            else
            {
                Image1.ImageUrl = "~/" + lblurl.Text;
            }
            
        }
    }
}