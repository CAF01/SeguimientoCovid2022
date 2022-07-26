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


        protected void BTN1_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(TB1.Text) || !String.IsNullOrEmpty(TB2.Text) || !String.IsNullOrEmpty(TB3.Text)
                || !String.IsNullOrEmpty(TB4.Text))
            {
                if(DDLCat.SelectedIndex<0 || DDLEDO.SelectedIndex <0 || DDLGen.SelectedIndex <0)
                {
                    //error por selección
                }
                {
                    //insertar
                    _ = this.NegociosProfesor.AgregarProfesor(new Profesor()
                    {
                        RegistroEmpleado = Convert.ToInt32(TB1.Text),
                        Nombre = TB2.Text,
                        ap_pat = TB3.Text,
                        ap_mat = TB4.Text,
                        Genero = DDLGen.SelectedValue,
                        Categoria = DDLCat.SelectedValue,
                        Correo = TB5.Text,
                        Celular = TB6.Text,
                        F_EdoCivil = Convert.ToByte(DDLEDO.SelectedValue) // Pudiera dar eerror
                    }) ? true : false;

                    TB1.Text = ""; TB2.Text = ""; TB3.Text = ""; TB4.Text = ""; TB5.Text = ""; TB6.Text = "";
                    DDLCat.SelectedIndex = 0; DDLEDO.SelectedIndex = 0; DDLGen.SelectedIndex = 0;

                }
            }
            else
            {
                //mensaje por falta de campos
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
                //No pueden estar ambos vacios
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
                if(list!=null && list.Count > 0)
                {
                    AparecerControlesdeResultado();
                    ListItem Item;
                    foreach (Profesor profesorList in list)
                    {
                        Item = new ListItem();
                        Item.Text = String.Format("Profesor: {0} {1}, Registro de Empleado: {2} ",profesorList.Nombre, profesorList.ap_pat, profesorList.RegistroEmpleado);
                        Item.Value = profesorList.ID_Profe.ToString();
                        DDLProfs.Items.Add(Item);
                    }
                    this.EventoSeleccionProfBuscado(Convert.ToInt32(DDLProfs.Items[0].Value));
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
            //DDLEDO.SelectedIndex = 0;
            //DDLEDO2.SelectedIndex = 0;
        }

        protected void DDLProfs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DDLProfs.SelectedIndex >=0)
            {
                this.EventoSeleccionProfBuscado(Convert.ToInt32(DDLProfs.SelectedValue));
                //pendiente estadoCivil
                //Mensaje de datos obtenidos
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

        protected void BTNDel_Click(object sender, EventArgs e)
        {
            if(DDLProfs.SelectedIndex>=0)
            {
                int idProf = Convert.ToInt32(DDLProfs.SelectedValue);
                if(this.NegociosProfesor.EliminarProfesor(idProf))
                {
                    //Limpiar controles
                }
                else
                {
                    //error
                }
            }
            else
            {
                //Se debe seleccionar un profesor
            }
        }

        protected void BTNMod_Click(object sender, EventArgs e)
        {
            if(DDLProfs.SelectedIndex>=0)
            {
                int idProf = Convert.ToInt32(DDLProfs.SelectedValue);

                if (!String.IsNullOrEmpty(TB12.Text) || !String.IsNullOrEmpty(TB22.Text) || !String.IsNullOrEmpty(TB32.Text)
                || !String.IsNullOrEmpty(TB42.Text))
                {
                    if (DDLCat2.SelectedIndex < 0 || DDLEDO2.SelectedIndex < 0 || DDL2.SelectedIndex < 0)
                    {
                        //error por selección
                    }
                    {
                        string Cat = "";
                        _=DDLCat2.SelectedItem.Text == "Profesor de tiempo Completo" ? Cat = "PTC" : Cat = "PA";
                        //insertar
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
                        {
                            //Limpiar controles
                        }

                        TB1.Text = ""; TB2.Text = ""; TB3.Text = ""; TB4.Text = ""; TB5.Text = ""; TB6.Text = "";
                        DDLCat.SelectedIndex = 0; DDLEDO.SelectedIndex = 0; DDLGen.SelectedIndex = 0;

                    }
                }
                else
                {
                    //mensaje por falta de campos
                }
            }
            else
            {
                //Se debe seleccionar un profesor
            }
        }
    }
}