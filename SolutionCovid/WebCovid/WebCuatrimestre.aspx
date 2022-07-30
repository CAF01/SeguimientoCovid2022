<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebCuatrimestre.aspx.cs" Inherits="WebCovid.WebCuatrimestre" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Sistema Seguimiento Covid</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/sweetalert2.min.js"></script>
    <link rel="Content/stylesheet" href="sweetalert2.min.css"/>
    <script type="text/javascript">
        function Alert(t, m, tipo) {
            Swal.fire(t, m, tipo)
        }
    </script>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="https://fonts.googleapis.com/css2?family=Sen&display=swap" rel="stylesheet"/>
    <style>
      body {
        font-family: 'Sen', serif;
      }
    </style>
</head>
<body>
    <form id="formCuatrimestre" runat="server">
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <div class="container-fluid">
                <a class="navbar-brand" href="WebMedico.aspx">
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
                            <a class="nav-link" aria-current="page" href="WebGrupoProfe.aspx">Grupos de un Profesor</a>
                          </li>
                          <li class="nav-item">
                            <a class="nav-link active" href="WebAlumno.aspx">Alumno</a>
                          </li>
                          <li class="nav-item">
                            <a class="nav-link active" href="WebCuatrimestre.aspx">Cuatrimestre</a>
                          </li>
                          <li class="nav-item">
                            <a class="nav-link" href="WebMedico.aspx">Médico</a>
                          </li>
                          <li class="nav-item">
                            <a class="nav-link" href="WebPositivoAlumno.aspx">Positivo Alumno</a>
                          </li>
                          <li class="nav-item">
                            <a class="nav-link" href="WebSeguimientoAlumno.aspx">Seguimiento Alumno</a>
                          </li>
                          <li class="nav-item">
                            <a class="nav-link" href="WebPositivosProfe.aspx">Positivo Profesor</a>
                          </li>
                          <li class="nav-item">
                            <a class="nav-link" href="WebSeguimientoCasoProfe.aspx">Seguimiento Profesor</a>
                          </li>
                          <li class="nav-item">
                            <a class="nav-link" href="WebConsultasProfesor.aspx">Contagios Profesor</a>
                          </li>
                            <li class="nav-item">
                            <a class="nav-link" href="WebIncapacidad.aspx">Incapadidad Profesor</a>
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
                        <h1 class="display-4 text-primary">Gestión de Cuatrimestres</h1>
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
                                              <!--Añadir cuatrimestre-->
                                              <div class="shadow-lg p-3 mb-5 bg-body rounded">
                                                  <label for="DropDownList1" class="form-label fs-3 fw-semibold">Añadir cuatrimestre</label>
                                                  <div class="mb-3">
                                                      <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlPeriodo" runat="server" class="form-select">
                                                                <asp:ListItem>Enero-Abril</asp:ListItem>
                                                                <asp:ListItem>Mayo-Agosto</asp:ListItem>
                                                                <asp:ListItem>Septiembre-Diciembre</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label for="ddlPeriodo">Periodo</label>
                                                        </div>
                                                      <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtAnio" runat="server" type="text" class="form-control" placeholder="2022"></asp:TextBox>
                                                            <label for="txtAnio">Año</label>
                                                        </div>
                                                        <div class="row align-items-start">
                                                            <label for="calInicio">Fecha Inicio</label>
                                                            <asp:Calendar ID="calInicio" runat="server"></asp:Calendar>        
                                                        </div><br />
                                                        <div class="row align-items-start">
                                                            <label for="calFin">Fecha Fin</label>
                                                            <asp:Calendar ID="calFin" runat="server"></asp:Calendar>
                                                        </div><br />
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtExtraCuatri" runat="server" type="text" class="form-control" placeholder="Texto extra"></asp:TextBox>
                                                            <label for="txtExtraCuatri">Extra</label>
                                                        </div>
                                                        <div class="row align-items-start">
                                                            <div class="form-floating mb-3">
                                                                <asp:Button ID="btnInsertarCuatri" runat="server" class="btn btn-primary btn-lg" Text="Añadir cuatrimestre" OnClick="btnInsertarCuatri_Click"/>
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
                                              
                                              <!--Añadir grupo a cuatrimestre-->
                                              <div class="shadow-lg p-3 mb-5 bg-body rounded">
                                                  <label for="DropDownList1" class="form-label fs-3 fw-semibold">Añadir grupo a cuatrimestre</label>
                                                  <div class="mb-3">
                                                      <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlProEd" runat="server" class="form-select" data-toggle="dropdown" AppendDataBoundItems="True"></asp:DropDownList> 
                                                            <label for="ddlProEd">Programa Educativo</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlGrupo" runat="server" class="form-select" AppendDataBoundItems="True"></asp:DropDownList>
                                                            <label for="ddlGrupo">Grupo</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlCuatri" runat="server" class="form-select" AppendDataBoundItems="True"></asp:DropDownList>
                                                            <label for="ddlCuatri">Cuatrimestre</label>   
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlTurno" runat="server" class="form-select">
                                                                <asp:ListItem>Matutino</asp:ListItem>
                                                                <asp:ListItem>Vespertino</asp:ListItem>
                                                            </asp:DropDownList>                                                         
                                                            <label for="ddlTurno">Turno</label>
                                                        </div>                              
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtModalidad" runat="server" type="text" class="form-control" placeholder="Texto extra"></asp:TextBox>
                                                            <label for="txtModalidad">Modalidad</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtExtraGruCuat" runat="server" type="text" class="form-control" placeholder="Texto extra"></asp:TextBox>
                                                            <label for="txtExtraGruCuat">Extra</label>
                                                        </div>
                                                  </div>
                                                  <div class="row align-items-start">
                                                        <div class="form-floating mb-3">
                                                            <asp:Button ID="btnInsertarGrupoCuatri" runat="server" class="btn btn-primary btn-lg" Text="Añadir grupo a cuatrimestre" OnClick="btnInsertarGrupoCuatri_Click"/>
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
                                            <!--Lista cuatrimestres-->
                                            <div class="shadow-lg p-3 mb-5 bg-body rounded">                                
                                                <label for="TB1" class="form-label fs-3 fw-semibold">Listado de cuatrimestres</label>
                                                <div class="mb-5">
                                                        <asp:GridView ID="gvCuatrimestres" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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

                                            <!--Lista grupos_cuatrimestres-->
                                            <div class="shadow-lg p-3 mb-5 bg-body rounded">                                
                                                <label for="TB1" class="form-label fs-3 fw-semibold">Listado de grupos de cuatrimestres</label>
                                                <div class="mb-3">
                                                        <asp:GridView ID="gvGruposCuatris" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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
                                            <!--Cuatrimestre-->
                                            <div class="shadow-lg p-3 mb-5 bg-body rounded">                                
                                                <label for="TB1" class="form-label fs-3 fw-semibold">Actualizar/Eliminar cuatrimestre</label>
                                                <div class="mb-3">
                                                    <h6><b>Datos del cuatrimestre</b></h6>
                                                    <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="dllPeriodoA" runat="server" class="form-select">
                                                                 <asp:ListItem>Enero-Abril</asp:ListItem>
                                                                 <asp:ListItem>Mayo-Agosto</asp:ListItem>
                                                                 <asp:ListItem>Septiembre-Diciembre</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label for="ddlPeriodoA">Periodo</label>
                                                        </div>
                                                      <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtAnioA" runat="server" type="text" class="form-control" placeholder="2022"></asp:TextBox>
                                                            <label for="txtAnioA">Año</label>
                                                        </div>
                                                        <div class="row align-items-start">
                                                            <label for="calInicioA">Fecha Inicio</label>
                                                            <asp:Calendar ID="calInicioA" runat="server"></asp:Calendar>        
                                                        </div><br />
                                                        <div class="row align-items-start">
                                                            <label for="calFinA">Fecha Fin</label>
                                                            <asp:Calendar ID="calFinA" runat="server"></asp:Calendar>
                                                        </div><br />
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtExtraCuatriA" runat="server" type="text" class="form-control" placeholder="Texto extra"></asp:TextBox>
                                                            <label for="txtExtraCuatriA">Extra</label>
                                                        </div>
                                                    <p>Para actualizar, selecciona el cuatrimestre dando clic sobre <b>Elegir cuatrimestre</b> y modifica los datos en el formulario, después da clic en el botón <b>Editar</b> correspondiente al registro del cuatriemstre.</p>
                                                    <p>Para eliminar un cuatrimestre, da clic en el botón <b>Eliminar</b> correspondiente al registro del cuatrimestre.</p>
                                                    <asp:GridView ID="gvCuatriAE" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="gvCuatriAE_RowDeleting" OnRowUpdating="gvCuatriAE_RowUpdating" OnSelectedIndexChanging="gvCuatriAE_SelectedIndexChanging">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Selecciona" ShowHeader="True" Text="Elegir cuatrimestre" ControlStyle-CssClass="btn btn-outline-primary"/>
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

                                            <!--Grupo_Cuatrimestre-->
                                            <div class="shadow-lg p-3 mb-5 bg-body rounded">                                
                                                <label for="TB1" class="form-label fs-3 fw-semibold">Actualizar/Eliminar grupo de cuatrimestre</label>
                                                <div class="mb-3">
                                                    <h6><b>Datos del grupo de un cuatrimestre</b></h6>
                                                    <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlProgEdA" runat="server" class="form-select" data-toggle="dropdown" AppendDataBoundItems="True"></asp:DropDownList> 
                                                            <label for="ddlProgEdA">Programa Educativo</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlGrupoA" runat="server" class="form-select" AppendDataBoundItems="True"></asp:DropDownList>
                                                            <label for="ddlGrupoA">Grupo</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlCuatriA" runat="server" class="form-select" AppendDataBoundItems="True"></asp:DropDownList>
                                                            <label for="ddlCuatriA">Cuatrimestre</label>   
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="ddlTurnoA" runat="server" class="form-select" data-toggle="dropdown">
                                                                 <asp:ListItem>Matutino</asp:ListItem>
                                                                 <asp:ListItem>Vespertino</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label for="ddlTurnoA">Turno</label>
                                                        </div>                              
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtModalidadA" runat="server" type="text" class="form-control" placeholder="Texto extra"></asp:TextBox>
                                                            <label for="txtModalidadA">Modalidad</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox ID="txtExtraGruCuatA" runat="server" type="text" class="form-control" placeholder="2022"></asp:TextBox>
                                                            <label for="txtExtraA">Extra</label>
                                                        </div>
                                                        <p>Para actualizar, selecciona el grupo de un cuatrimestre dando clic sobre <b>Elegir grupo-cuatrimestre</b> y modifica los datos en el formulario, después da clic en el botón <b>Editar</b> correspondiente al registro del grupo de un cuatrimestre.</p>
                                                        <p>Para eliminar un grupo de un cuatrimestre, da clic en el botón <b>Eliminar</b> correspondiente al registro del grupo de un cuatrimestre.</p>
                                                        <asp:GridView ID="gvGruCuatAE" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="gvGruCuatAE_RowDeleting" OnRowUpdating="gvGruCuatAE_RowUpdating" OnSelectedIndexChanging="gvGruCuatAE_SelectedIndexChanging">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Selecciona" ShowHeader="True" Text="Elegir grupo-cuatrimestre" ControlStyle-CssClass="btn btn-outline-primary"/>
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
