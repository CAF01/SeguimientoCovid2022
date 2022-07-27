<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebAlumno.aspx.cs" Inherits="WebCovid.WebAlumno" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Sistema Seguimiento Covid</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/sweetalert2.min.js"></script>
    <link rel="Content/stylesheet" href="sweetalert2.min.css"/>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="https://fonts.googleapis.com/css2?family=Sen&display=swap" rel="stylesheet"/>
    <style>
      body {
        font-family: 'Sen', serif;
      }
    </style>
</head>
<body>
    <form id="formAlumno" runat="server">
        
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <div class="container-fluid">
               <a class="navbar-brand" href="WebInicio.aspx">
                    <img src="Resources/icono.png" alt="" width="50" height="50" class="d-inline-block align-text-top"/>
                        SEGUIMIENTO COVID
               </a>
              <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarColor01" aria-controls="navbarColor01" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
              </button>
              <div class="collapse navbar-collapse" id="navbarColor01">
                <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                  <li class="nav-item">
                    <a class="nav-link" aria-current="page" href="WebProfesor.aspx">Profesor</a>
                  </li>
                  <li class="nav-item">
                    <a class="nav-link active" href="WebAlumno.aspx">Alumno</a>
                  </li>
                  <li class="nav-item">
                    <a class="nav-link" href="WebCuatrimestre.aspx">Cuatrimestre</a>
                  </li>
                  <li class="nav-item">
                    <a class="nav-link" href="WebMedico.aspx">Médico</a>
                  </li>
                </ul>
              </div>
            </div>
          </nav>
          
          <div class="form-group">
            <br />
            <br />
            <div class="container-fluid">
                    <div class="text-center">
                        <h1 class="display-4 text-primary">Gestión de Alumnos</h1>
                    </div>                    
                    
                    <div class="row">
                        <div class="col-md-3"></div>

                        <div class="col-md-6">
                            <br />
                            <div class="mb-3">
                                <div class="text-center">                               
                                    <nav>
                                          <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                            <button class="nav-link" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home" type="button" role="tab" aria-controls="nav-home" aria-selected="false">Añadir</button>
                                            <button class="nav-link" data-bs-toggle="tab" data-bs-target="#nav-profile" type="button" role="tab" aria-controls="nav-profile" aria-selected="false">Ver listado</button>
                                            <button class="nav-link active" data-bs-toggle="tab" data-bs-target="#nav-profile2" type="button" role="tab" aria-controls="nav-profile" aria-selected="true">Actualizar/Eliminar</button>
                                          </div>
                                    </nav>
                                    <div class="tab-content" id="nav-tabContent">
                                      <!--Añadir-->
                                      <div class="tab-pane fade" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab" tabindex="0">
                                          <br />
                                          <div class="text-center">
                                              <!--Añadir alumno-->
                                              <div class="shadow-lg p-3 mb-5 bg-body rounded">
                                                  <label for="DropDownList1" class="form-label fs-3 fw-semibold">Añadir alumno</label>
                                                  <div class="mb-3">                                                     
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtMatricula" runat="server" type="text" class="form-control" placeholder="UTP0000000"></asp:TextBox>
                                                            <label for="txtMatricula">Matrícula</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtNombre" runat="server" type="text" class="form-control" placeholder="Jimena"></asp:TextBox>
                                                            <label for="txtNombre">Nombre</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtApp" runat="server" type="text" class="form-control" placeholder="Manzano"></asp:TextBox>
                                                            <label for="txtApp">Apellido paterno</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtApm" runat="server" type="text" class="form-control" placeholder="Gutiérrez"></asp:TextBox>
                                                            <label for="txtApm">Apellido materno</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlGenero" runat="server" class="form-select">
                                                                 <asp:ListItem>Femenino</asp:ListItem>
                                                                 <asp:ListItem>Masculino</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label for="ddlGenero">Género</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtCorreo" runat="server" type="text" class="form-control" placeholder="Manzano"></asp:TextBox>
                                                            <label for="txtCorreo">Correo electrónico</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtCelular" runat="server" type="text" class="form-control" placeholder="Gutiérrez"></asp:TextBox>
                                                            <label for="txtCelular">Celular</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlEdoCivil" runat="server" class="form-select"></asp:DropDownList>
                                                            <label for="ddlEdoCivil">Estado Civil</label>
                                                        </div>
                                                        <div class="row align-items-start">
                                                            <div class="form-floating mb-3">
                                                                <asp:Button ID="btnInsertarAlumno" runat="server" class="btn btn-primary btn-lg" Text="Añadir alumno" OnClick="btnInsertarAlumno_Click"/>
                                                            </div> 
                                                        </div>


                                                      <asp:ScriptManager ID="ScriptManager1" ScriptMode="Release" AsyncPostBackTimeout="360000" EnablePageMethods="true" runat="server"> 
    
                                                            <Scripts>
                                                                <asp:ScriptReference Path="~/js/jquery-3.4.1.min.js" />
                                                                <asp:ScriptReference Path="~/Scripts/bootstrap.min.js" />

                                                            </Scripts>
                                                          
                                                      </asp:ScriptManager>
                                                      
                                                  </div>
                                              </div>
                                              
                                              <!--Añadir alumno a grupo-cuatrimestre-->
                                              <div class="shadow-lg p-3 mb-5 bg-body rounded">
                                                  <label for="DropDownList1" class="form-label fs-3 fw-semibold">Añadir alumno a grupo-cuatrimestre</label>
                                                  <div class="mb-3">
                                                      <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlAlumno" runat="server" class="form-select" data-toggle="dropdown"></asp:DropDownList> 
                                                            <label for="ddlAlumno">Alumno</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlGrupoCuatri" runat="server" class="form-select"></asp:DropDownList>
                                                            <label for="ddlGrupoCuatri">Grupo-Cuatrimestre</label>
                                                        </div>
                                                       
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtExtra1" runat="server" type="text" class="form-control" placeholder="2022"></asp:TextBox>
                                                            <label for="txtExtra1">Extra</label>
                                                        </div>                              
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtExtra2" runat="server" type="text" class="form-control" placeholder="Texto extra"></asp:TextBox>
                                                            <label for="txtExtra2">Extra 2</label>
                                                        </div>                                                   
                                                  </div>
                                                  <div class="row align-items-start">
                                                        <div class="form-floating mb-3">
                                                            <asp:Button ID="btnInsertarAlGruCuatri" runat="server" class="btn btn-primary btn-lg" Text="Añadir alumno a grupo-cuatrimestre" OnClick="btnInsertarAlGruCuatri_Click"/>
                                                        </div> 
                                                  </div>   
                                              </div>

                                          </div>

                                          <%--<div class="input-group mb-2">
                                                <asp:FileUpload ID="FileUpload1" type="file" class="form-control" runat="server" /><label class="input-group-text" for="FileUpload1">Archivo de Restauración</label>
                                          </div>--%>
                    
                                      </div>
                                      <!--Fin Añadir-->
                                      
                                      <!--Ver listado-->
                                      <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab" tabindex="0">
                                      <br />
                                        <div class="text-center">
                                            <!--Lista alumnos-->
                                            <div class="shadow-lg p-3 mb-5 bg-body rounded">                                
                                                <label for="TB1" class="form-label fs-3 fw-semibold">Listado de alumnos</label>
                                                <div class="mb-5">
                                                            <asp:GridView ID="gvAlumnos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                         </asp:GridView>          
                                                 </div>
                                             </div>

                                            <!--Lista alumnos de un grupo_cuatrimestre-->
                                            <div class="shadow-lg p-3 mb-5 bg-body rounded">                                
                                                <label for="TB1" class="form-label fs-3 fw-semibold">Listado de alumnos en un grupo-cuatrimestre</label>
                                                <div class="mb-3">
                                                        <asp:GridView ID="gvAlumnosGruCuatri" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                         </asp:GridView>         
                                                 </div>
                                             </div>
                                         </div>
                                      </div>
                                      <!--Fin listado-->

                                      <!--Actualizar/Eliminar-->
                                      <div class="tab-pane fade show active" id="nav-profile2" role="tabpanel" aria-labelledby="nav-profile-tab" tabindex="0">
                                      <br />
                                        <div class="text-center">
                                            <!--Alumno-->
                                            <div class="shadow-lg p-3 mb-5 bg-body rounded">                                
                                                <label for="TB1" class="form-label fs-3 fw-semibold">Actualizar/Eliminar alumno</label>
                                                <div class="mb-3">
                                                    <h6><b>Datos del alumno</b></h6>
                                                    <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtMatriculaA" runat="server" type="text" class="form-control" placeholder="UTP0000000"></asp:TextBox>
                                                            <label for="txtMatriculaA">Matrícula</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtNombreA" runat="server" type="text" class="form-control" placeholder="Jimena"></asp:TextBox>
                                                            <label for="txtNombreA">Nombre</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtAppA" runat="server" type="text" class="form-control" placeholder="Manzano"></asp:TextBox>
                                                            <label for="txtAppA">Apellido paterno</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtApmA" runat="server" type="text" class="form-control" placeholder="Gutiérrez"></asp:TextBox>
                                                            <label for="txtApmA">Apellido materno</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlGeneroA" runat="server" class="form-select">
                                                                 <asp:ListItem>Femenino</asp:ListItem>
                                                                 <asp:ListItem>Masculino</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label for="ddlGeneroA">Género</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtCorreoA" runat="server" type="text" class="form-control" placeholder="Manzano"></asp:TextBox>
                                                            <label for="txtCorreoA">Correo electrónico</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtCelularA" runat="server" type="text" class="form-control" placeholder="Gutiérrez"></asp:TextBox>
                                                            <label for="txtCelularA">Celular</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlEdoCivilA" runat="server" class="form-select"></asp:DropDownList>
                                                            <label for="ddlEdoCivilA">Estado Civil</label>
                                                        </div>
                                                    <p>Para actualizar, selecciona el alumno dando clic sobre <b>Elegir alumno</b> y modifica los datos en el formulario, después da clic en el botón <b>Editar</b> correspondiente al registro del alumno.</p>
                                                    <p>Para eliminar un alumno, da clic en el botón <b>Eliminar</b> correspondiente al registro del alumno.</p>
                                                    <asp:GridView ID="gvAlumnosAE" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="gvAlumnosA_RowDeleting" OnRowUpdating="gvAlumnosA_RowUpdating" OnSelectedIndexChanging="gvAlumnosA_SelectedIndexChanging">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Selecciona" ShowHeader="True" Text="Elegir alumno" ControlStyle-CssClass="btn btn-outline-primary"/>
                                                            <asp:ButtonField ButtonType="Button" CommandName="Delete" HeaderText="Elimina" ShowHeader="True" Text="Eliminar" ControlStyle-CssClass="btn btn-outline-danger"/>
                                                            <asp:ButtonField ButtonType="Button" CommandName="Update" HeaderText="Edita" ShowHeader="True" Text="Editar" ControlStyle-CssClass="btn btn-outline-warning"/>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>        
                                                 </div>
                                             </div>

                                            <!--Alumno Grupo-->
                                            <div class="shadow-lg p-3 mb-5 bg-body rounded">                                
                                                <label for="TB1" class="form-label fs-3 fw-semibold">Actualizar/Eliminar alumno de un grupo-cuatrimestre</label>
                                                <div class="mb-3">
                                                    <h6><b>Datos del alumno de un grupo-cuatrimestre</b></h6>
                                                    <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlAlumnoA" runat="server" class="form-select" data-toggle="dropdown"></asp:DropDownList> 
                                                            <label for="ddlAlumnoA">Alumno</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlGrupoCuatriA" runat="server" class="form-select"></asp:DropDownList>
                                                            <label for="ddlGrupoCuatriA">Grupo-Cuatrimestre</label>
                                                        </div>
                                                       
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtExtra1A" runat="server" type="text" class="form-control" placeholder="2022"></asp:TextBox>
                                                            <label for="txtExtra1A">Extra</label>
                                                        </div>                              
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtExtra2A" runat="server" type="text" class="form-control" placeholder="Texto extra"></asp:TextBox>
                                                            <label for="txtExtra2A">Extra 2</label>
                                                        </div>
                                                        <p>Para actualizar, selecciona el alumno de un grupo-cuatrimestre dando clic sobre <b>Elegir alumno-grupo</b> y modifica los datos en el formulario, después da clic en el botón <b>Editar</b> correspondiente al registro del alumno de un grupo-cuatrimestre.</p>
                                                        <p>Para eliminar un alumno de un grupo-cuatrimestre, da clic en el botón <b>Eliminar</b> correspondiente al registro del grupo del alumno de un grupo-cuatrimestre.</p>
                                                        <asp:GridView ID="gvAlumnosGruCuatriAE" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="gvAlumnosGruCuatriA_RowDeleting" OnRowUpdating="gvAlumnosGruCuatriA_RowUpdating" OnSelectedIndexChanging="gvAlumnosGruCuatriA_SelectedIndexChanging">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Selecciona" ShowHeader="True" Text="Elegir alumno-grupo" ControlStyle-CssClass="btn btn-outline-primary-"/>
                                                                <asp:ButtonField ButtonType="Button" CommandName="Delete" HeaderText="Elimina" ShowHeader="True" Text="Eliminar" ControlStyle-CssClass="btn btn-outline-danger"/>
                                                                <asp:ButtonField ButtonType="Button" CommandName="Update" HeaderText="Edita" ShowHeader="True" Text="Editar" ControlStyle-CssClass="btn btn-outline-warning"/>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EFF3FB" />
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>         
                                                 </div>
                                             </div>
                                         </div>
                                      </div>
                                      <!--Fin Actualizar/Eliminar-->

                                    </div>

                                </div>

                            </div>                          

                        </div>

                        <div class="col-md-3"></div>
                    </div>
                     
            </div>           

        </div>
    </form>
</body>
</html>
