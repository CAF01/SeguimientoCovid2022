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
    public partial class WebProfesor : System.Web.UI.Page
    {
        LogicaNegociosProfesor NegociosProfesor;
        List<EstadoCivil> Civils;
        List<Profesor> Profesors;
        protected void Page_Load(object sender, EventArgs e)
        {
            LBR.Visible = false;
            DDLProfs.Visible = false;
            if (!IsPostBack)
            {
                if (Session["NegociosProf"] != null)
                {
                    this.NegociosProfesor = (LogicaNegociosProfesor)Session["NegociosProf"];
                    this.Civils = (List<EstadoCivil>)Session["Civils"];
                    this.Profesors = (List<Profesor>)Session["Profs"];
                }

                else
                {
                    this.NegociosProfesor = new LogicaNegociosProfesor(ConfigurationManager.ConnectionStrings["BaseSqlChris"].ConnectionString);
                    Session["NegociosProf"] = this.NegociosProfesor;
                    this.Civils= this.NegociosProfesor.DevolverEstadoCivil();
                    Session["Civils"] = this.Civils;
                    this.Profesors = this.NegociosProfesor.MostrarProfesores();
                    Session["Profs"]= this.Profesors;
                }

            }
            else
            {
                this.NegociosProfesor = (LogicaNegociosProfesor)Session["NegociosProf"];
                this.Civils = (List<EstadoCivil>)Session["Civils"];
                this.Profesors = (List<Profesor>) Session["Profs"];
            }
            if (Civils != null && Civils.Count > 0)
            {
                this.EstablecerListEstadoCivil();
            }

        }

        public void EnviaAlertas(string titulo, string msg, string tipo)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), titulo, String.Format("registro('{0}','{1}','{2}')", titulo, msg, tipo), true);
        }


        protected void BTN1_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(TB1.Text) && !String.IsNullOrEmpty(TB2.Text) && !String.IsNullOrEmpty(TB3.Text)
                && !String.IsNullOrEmpty(TB4.Text))
            {
                if(DDLCat.SelectedIndex<0 || DDLEDO.SelectedIndex <0 || DDLGen.SelectedIndex <0)
                {
                    this.EnviaAlertas("Error!", "Es necesario completar los campos de selección para la Categoria, Estado Civil y Genero", "error");
                }
                else
                {
                    string Cat = "";
                    _ = DDLCat.SelectedItem.Text == "Profesor de tiempo Completo" ? Cat = "PTC" : Cat = "PA";
                    bool flag = this.NegociosProfesor.AgregarProfesor(new Profesor()
                    {
                        RegistroEmpleado = Convert.ToInt32(TB1.Text),
                        Nombre = TB2.Text,
                        ap_pat = TB3.Text,
                        ap_mat = TB4.Text,
                        Genero = DDLGen.SelectedItem.Text,
                        Categoria = Cat,
                        Correo = TB5.Text,
                        Celular = TB6.Text,
                        F_EdoCivil = Convert.ToByte(DDLEDO.SelectedValue)
                    });
                    if (flag)
                        this.EnviaAlertas("Correcto!", "¡Nuevo Profesor agregado!", "success");
                    else
                        this.EnviaAlertas("OOps!", "¡Algo fallo con el registro!", "info");
                    TB1.Text = ""; TB2.Text = ""; TB3.Text = ""; TB4.Text = ""; TB5.Text = ""; TB6.Text = "";
                    DDLCat.SelectedIndex = 0; DDLEDO.SelectedIndex = 0; DDLGen.SelectedIndex = 0;

                }
            }
            else
            {
                this.EnviaAlertas("Error!", "Es necesario llenar los campos obligatorios, excepto CORREO y TELEFONO", "error");
            }
        }

        protected void BTNTabla_Click(object sender, EventArgs e)
        {
            string msg = "";
            this.Profesors = this.NegociosProfesor.MostrarProfesores();
            GVProfesor.DataSource = this.NegociosProfesor.MostrarProfesoresPocaInfo(ref msg);
            GVProfesor.DataBind();
        }

        protected void BTNS_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(TBNom.Text) && String.IsNullOrEmpty(TBAp.Text))
            {
                this.EnviaAlertas("Fallo al buscar", "Es necesario al menos el nombre o el apellido para realizar la busqueda", "info");
            }
            else
            {
                Profesor profesor = new Profesor();
                if (TBNom.Text == "")
                {
                    profesor.Nombre = TBAp.Text;
                    profesor.ap_pat = TBAp.Text;
                }
                if(TBAp.Text=="")
                {
                    profesor.Nombre = TBNom.Text;
                    profesor.ap_pat = TBNom.Text;
                }
                else
                {
                    profesor.Nombre=TBNom.Text;
                    profesor.ap_pat = TBAp.Text;
                }
                  
                List<Profesor> list = this.NegociosProfesor.BuscarProfesorPorNombres(profesor);
                if (list != null && list.Count > 0)
                {
                    AparecerControlesdeResultado();
                    ListItem Item;
                    foreach (Profesor profesorList in list)
                    {
                        Item = new ListItem();
                        Item.Text = String.Format("Profesor: {0} {1}, Registro de Empleado: {2} ", profesorList.Nombre, profesorList.ap_pat, profesorList.RegistroEmpleado);
                        Item.Value = profesorList.ID_Profe.ToString();
                        DDLProfs.Items.Add(Item);
                    }
                    this.EventoSeleccionProfBuscado(Convert.ToInt32(DDLProfs.Items[0].Value));
                    this.EnviaAlertas("Busqueda exitosa!", "Los resultados estan disponibles en la tarjeta de actualización!", "success");
                }
                else
                {
                    this.EnviaAlertas("Error", "No se hallaron resultados con la busqueda indicada", "error");
                    LBR.Visible = false;
                    DDLProfs.Visible = false;
                }
                    
            }
        }

        public void AparecerControlesdeResultado()
        {
            LBR.Visible = true;
            DDLProfs.Visible = true;
            DDLProfs.Items.Clear();
        }

        public void EstablecerListEstadoCivil()
        {
            DDLEDO.Items.Clear();
            DDLEDO2.Items.Clear();
            ListItem listItem;
            foreach (EstadoCivil item in Civils)
            {
                listItem = new ListItem();
                listItem.Text = item.Estado;
                listItem.Value = item.Id_Edo.ToString();
                DDLEDO.Items.Add(listItem);
                DDLEDO2.Items.Add(listItem);
            }
        }

        protected void DDLProfs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DDLProfs.SelectedIndex >=0)
            {
                this.EventoSeleccionProfBuscado(Convert.ToInt32(DDLProfs.SelectedValue));
                this.EnviaAlertas("Correcto!", "Datos obtenidos y listos para actualizar o eliminar en la tarjeta de actualización", "success");
            }
        }

        public void EventoSeleccionProfBuscado(int index)
        {
            Profesor p = Profesors.Find(x => x.ID_Profe == Convert.ToInt32(index));
            TB12.Text = p.RegistroEmpleado.ToString();
            TB22.Text = p.Nombre;
            TB32.Text = p.ap_pat;
            TB42.Text = p.ap_mat;
            TB52.Text = p.Correo;
            TB62.Text = p.Celular;
            _ = p.Genero == "Masculino" ? DDL2.SelectedIndex = 0 : DDL2.SelectedIndex = 1;
            _ = p.Categoria == "Profesor por asignatura" ? DDLCat2.SelectedIndex = 0 : DDLCat2.SelectedIndex = 1;
            DDLEDO2.SelectedIndex = DDLEDO2.Items.IndexOf(DDLEDO2.Items.FindByValue(p.F_EdoCivil.ToString()));
        }

        public void LimpiarControlesMod()
        {
            TB12.Text = "";
            TB22.Text = "";
            TB32.Text = "";
            TB42.Text = "";
            TB52.Text = "";
            TB62.Text = "";
            DDL2.SelectedIndex = 0;
            DDLCat2.SelectedIndex = 0;
            DDLEDO2.SelectedIndex = 0;
            LBR.Visible = false;
            DDLProfs.Visible = false;
        }

        protected void BTNDel_Click(object sender, EventArgs e)
        {
            if(DDLProfs.SelectedIndex>=0)
            {
                int idProf = Convert.ToInt32(DDLProfs.SelectedValue);
                if(this.NegociosProfesor.EliminarProfesor(idProf))
                {
                    this.LimpiarControlesMod();
                    this.EnviaAlertas("Eliminado!", "El profesor se eliminó correctamente", "success");
                }
                else
                {
                    this.EnviaAlertas("OOps!!", "No se pudo ejecutar la eliminación", "error");
                }
            }
            else
            {
                this.EnviaAlertas("Información!", "Es necesario seleccionar un profesor del resultado de busqueda", "info");
            }
        }

        protected void BTNMod_Click(object sender, EventArgs e)
        {
            if(DDLProfs.SelectedIndex>=0)
            {
                int idProf = Convert.ToInt32(DDLProfs.SelectedValue);

                if (!String.IsNullOrEmpty(TB12.Text) && !String.IsNullOrEmpty(TB22.Text) && !String.IsNullOrEmpty(TB32.Text)
                && !String.IsNullOrEmpty(TB42.Text))
                {
                    if (DDLCat2.SelectedIndex < 0 || DDLEDO2.SelectedIndex < 0 || DDL2.SelectedIndex < 0)
                    {
                        this.EnviaAlertas("Error!", "Es necesario completar los campos de selección para la Categoria, Estado Civil y Genero", "error");
                    }
                    {
                        string Cat = "";
                        _=DDLCat2.SelectedItem.Text == "Profesor de tiempo Completo" ? Cat = "PTC" : Cat = "PA";
                        Profesor profesorUpdated = new Profesor()
                        {
                            ID_Profe=idProf,
                            RegistroEmpleado = Convert.ToInt32(TB12.Text),
                            Nombre = TB22.Text,
                            ap_pat = TB32.Text,
                            ap_mat = TB42.Text,
                            Genero = DDL2.SelectedItem.Text,
                            Categoria = Cat,
                            Correo = TB52.Text,
                            Celular = TB62.Text,
                            F_EdoCivil = Convert.ToByte(DDLEDO2.SelectedValue)
                        };
                        if (this.NegociosProfesor.ActualizarProfesor(profesorUpdated))
                            this.EnviaAlertas("Correcto!", "Datos de profesor actualizados correctamente", "success");
                        else
                            this.EnviaAlertas("Error", "No se pudo eliminar al profesor seleccionado", "error");

                        this.LimpiarControlesMod();
                    }
                }
                else
                {
                    this.EnviaAlertas("Info!", "Es necesario completar los campos obligatorios, excepto CORREO y TELEFONO", "info");
                }
            }
            else
            {
                this.EnviaAlertas("Info!", "Es necesario seleccionar a un profesor de la lista de resultados", "info");
            }
        }
    }
}