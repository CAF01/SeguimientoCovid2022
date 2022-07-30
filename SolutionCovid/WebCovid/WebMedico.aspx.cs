using ClassEntidades;
using ClassLogicaNegocios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;

namespace WebCovid
{
    public partial class WebMedico : System.Web.UI.Page
    {
        LogicaNegociosMedico NegociosMedico;
        DataSet DataSetMedicos;
        List<Medico> Medicos;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["NegociosMed"] != null)
                {
                    this.NegociosMedico = (LogicaNegociosMedico)Session["NegociosMed"];
                }

                else
                {
                    this.NegociosMedico = new LogicaNegociosMedico(ConfigurationManager.ConnectionStrings["BaseSqlChris"].ConnectionString);
                    Session["NegociosMed"] = this.NegociosMedico;
                }
            }
            else
            {
                this.NegociosMedico = (LogicaNegociosMedico)Session["NegociosMed"];
                this.DataSetMedicos = (DataSet)Session["DataMed"];
                this.Medicos = (List<Medico>) Session["Meds"];
            }
        }

        public void EnviaAlertas(string titulo, string msg, string tipo)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), titulo, String.Format("registro('{0}','{1}','{2}')", titulo, msg, tipo), true);
        }


        protected void BTN1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TB1.Text) && !String.IsNullOrEmpty(TB2.Text) && !String.IsNullOrEmpty(TB4.Text)
                && !String.IsNullOrEmpty(TB5.Text))
            {
                
                    string msg= "";
                    bool flag = this.NegociosMedico.insertarMedico(new Medico()
                    {
                        nombre = TB1.Text,
                        app=TB2.Text,
                        apm=TB3.Text,
                        telefono=TB4.Text,
                        correo=TB5.Text,
                        horario=TB6.Text,
                        especialidad=TB7.Text,
                        extra=TB8.Text
                    },ref msg);
                    if (flag)
                        this.EnviaAlertas("Correcto!", "¡Nuevo Médico agregado!", "success");
                    else
                        this.EnviaAlertas("OOps!", "¡Algo fallo con el registro!", "info");
                    TB1.Text = ""; TB2.Text = ""; TB3.Text = ""; TB4.Text = ""; TB5.Text = ""; TB6.Text = ""; TB7.Text = ""; TB8.Text = "";

            }
            else
            {
                this.EnviaAlertas("Error!", "Es necesario llenar los campos obligatorios, excepto Apellido Materno, Horario, Especialidad", "error");
            }
        }

        protected void BTNGrid_Click(object sender, EventArgs e)
        {
            this.updateInfoTable();
            if (this.DataSetMedicos != null && Medicos.Count > 0)
                this.EnviaAlertas("Consulta", "Lista de médicos obtenida", "success");
            else
                this.EnviaAlertas("Error", "No hay información disponible", "error");
        }

        public void updateInfoTable()
        {
            string msg = "";
            this.DataSetMedicos = this.NegociosMedico.consultarMedicos(ref msg);
            Session["DataMed"] = this.DataSetMedicos;
            this.Medicos = this.NegociosMedico.obtenerListaMedicos(ref msg);
            Session["Meds"] = this.Medicos;
            GVMedico.DataSource = this.DataSetMedicos;
            GVMedico.DataBind();
        }


        protected void BTNMod_Click(object sender, EventArgs e)
        {
            if(this.DataSetMedicos!=null && GVMedico.SelectedIndex>=0)
            {
                if (!String.IsNullOrEmpty(TBM1.Text) && !String.IsNullOrEmpty(TBM2.Text) && !String.IsNullOrEmpty(TBM4.Text)
                && !String.IsNullOrEmpty(TBM5.Text))
                {

                    string msg = "";
                    Medico MedicoUpdated = new Medico()
                    {
                        id= Convert.ToInt32(GVMedico.SelectedRow.Cells[1].Text),
                        nombre = TBM1.Text,
                        app = TBM2.Text,
                        apm = TBM3.Text,
                        telefono = TBM4.Text,
                        correo = TBM5.Text,
                        horario = TBM6.Text,
                        especialidad = TBM7.Text,
                        extra = TBM8.Text
                    };
                    bool flag = this.NegociosMedico.editarMedico(Convert.ToInt32(GVMedico.SelectedRow.Cells[1].Text),MedicoUpdated , ref msg);
                    if (flag)
                    {
                        //int index = this.Medicos.FindIndex(x => x.id == Convert.ToInt32(GVMedico.SelectedRow.Cells[1].Text));
                        //this.Medicos[index] = MedicoUpdated;
                        this.updateInfoTable();
                        this.EnviaAlertas("Correcto!", "¡Médico Actualizado!", "success");
          
                    }
                    else
                        this.EnviaAlertas("OOps!", "¡Algo fallo con la actualización!", "info");
                    TBM1.Text = ""; TBM2.Text = ""; TBM3.Text = ""; TBM4.Text = ""; TBM5.Text = ""; TBM6.Text = ""; TBM7.Text = ""; TBM8.Text = "";
                    GVMedico.SelectedIndex = -1;
                }
                else
                {
                    this.EnviaAlertas("Error!", "Es necesario llenar los campos obligatorios, excepto Apellido Materno, Horario, Especialidad", "error");
                }


            }
            else
            {
                this.EnviaAlertas("Error", "Debes seleccionar un registro de la información dispoible", "error");
            }
        }

        protected void BTNDel_Click(object sender, EventArgs e)
        {
            if (this.DataSetMedicos != null && GVMedico.SelectedIndex >= 0)
            {
                string msg = "";
                bool flag = this.NegociosMedico.eliminarMedico(Convert.ToInt32(GVMedico.SelectedRow.Cells[1].Text), ref msg);
                if (flag)
                {
                    //int index = this.Medicos.FindIndex(x => x.id == Convert.ToInt32(GVMedico.SelectedRow.Cells[1].Text));
                    //this.Medicos.RemoveAt(index);
                    this.updateInfoTable();
                    this.EnviaAlertas("Correcto!", "¡Médico Eliminado!", "success");

                }
                else
                    this.EnviaAlertas("OOps!", "¡Algo fallo con la eliminación!", "info");
                TBM1.Text = ""; TBM2.Text = ""; TBM3.Text = ""; TBM4.Text = ""; TBM5.Text = ""; TBM6.Text = ""; TBM7.Text = ""; TBM8.Text = "";
                GVMedico.SelectedIndex = -1;
            }
            else
            {
                this.EnviaAlertas("Error", "Debes seleccionar un registro de la información dispoible", "error");
            }
        }

        protected void GVMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(GVMedico.SelectedIndex>=0)
            {
                int id = Convert.ToInt32(GVMedico.SelectedRow.Cells[1].Text);
                Medico FoundMedico = this.Medicos.Find(x => x.id == id);
                TBM1.Text = FoundMedico.nombre;
                TBM2.Text = FoundMedico.app;
                TBM3.Text = FoundMedico.apm;
                TBM4.Text = FoundMedico.telefono;
                TBM5.Text = FoundMedico.correo;
                TBM6.Text = FoundMedico.horario;
                TBM7.Text = FoundMedico.especialidad;
                TBM8.Text = FoundMedico.extra;
                this.EnviaAlertas("Selección!", "El registro fue correctamente seleccionado y esta lista para la actualización o eliminación de los datos.", "success");
            }
        }
    }
}