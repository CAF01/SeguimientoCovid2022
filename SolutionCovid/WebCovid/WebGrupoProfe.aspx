<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebGrupoProfe.aspx.cs" Inherits="WebCovid.WebGrupoProfe" %>

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
    <form id="form1" runat="server">
         <script>
                        function registro(alerta,mensaje, tipo)
                        {
                            Swal.fire(
                                alerta,
                                mensaje,
                                tipo
                            )
                        }
         </script>
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
                    <a class="nav-link" href="WebMedico.aspx">Gestión de Médicos</a>
                  </li>
                  <li class="nav-item dropdown">
                      <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Gestión de Profesores
                      </a>
                      <ul class="dropdown-menu">
                        <li><a class="dropdown-item" href="WebProfesor.aspx">Registros</a></li>
                        <li><hr class="dropdown-divider"/></li>
                        <li><a class="dropdown-item" href="WebPositivosProfe.aspx">Casos Positivos</a></li>
                        <li><a class="dropdown-item" href="WebSeguimientoCasoProfe.aspx">Seguimiento de casos</a></li>
                        <li><a class="dropdown-item" href="WebIncapacidad.aspx">Incapacidades</a></li>
                        <li><hr class="dropdown-divider"/></li>
                        <li><a class="dropdown-item" href="WebConsultasProfesor.aspx">Estadísticas de contagios</a></li>
                      </ul>
                  </li>
                  <li class="nav-item dropdown">
                      <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Gestión de Alumnos
                      </a>
                      <ul class="dropdown-menu">
                        <li><a class="dropdown-item" href="WebAlumno.aspx">Registros</a></li>
                        <li><hr class="dropdown-divider"/></li>
                        <li><a class="dropdown-item" href="WebPositivoAlumno.aspx">Casos Positivos</a></li>
                        <li><a class="dropdown-item" href="WebSeguimientoAlumno.aspx">Seguimiento de casos y Estadísticas</a></li>
                      </ul>
                  </li>  
                  <li class="nav-item dropdown">
                      <a class="nav-link active dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Grupos
                      </a>
                      <ul class="dropdown-menu">
                        <li><a class="dropdown-item" href="WebCuatrimestre.aspx">Manejo de Cuatrimestres</a></li>
                        <li><hr class="dropdown-divider"/></li>
                        <li><a class="dropdown-item" href="WebGrupoProfe.aspx">Asignar profesores</a></li>
                        <li><hr class="dropdown-divider"/></li>
                        <li><a class="dropdown-item" href="WebAlumno.aspx">Asignar alumnos</a></li>
                      </ul>
                  </li>    
                </ul>
              </div>
            </div>
          </nav>


        <%--separador--%>

        <div class="form-group">
            <br />
            <div class="container-fluid">
                <div class="text-center">
                        <h1 class="display-4 text-primary">Gestión de grupos para profesores</h1>
                        <br />
                </div>
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        
                        <h3 class="fw-bold ">Agregar profesor a un grupo</h3>
                        <div class="text-center">
                            <h4 class="text-danger">Listado de programas educativos - 2022</h4>
                        </div>
                        <div class="form-group">
                            <div class="mb-3">
                                    <p class="fs-3"><mark>Selecciona </mark> un Programa educativo para mostrar la lista de grupos para el cuatrimestre.</p>
                                    <div class="form-floating mb-3">
                                              <asp:DropDownList ID="DDLProgramas" class="form-select" runat="server"></asp:DropDownList>
                                              <label for="DDLProgramas">Programas educativos</label>
                                    </div>                                    
                            </div>
                            <div class="mb-3">
                                    <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                           <asp:Button ID="Button1" class="btn btn-dark btn-lg" runat="server" Text="Mostrar grupos" OnClick="BTNSG_Click" />
                                    </div>
                                    <br />
                                    <asp:GridView ID="GVGrupos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GVGrupos_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Button" SelectText="Seleccionar" ShowSelectButton="True" />
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                            </div>
                            <div class="mb-3">
                                    <p class="fs-3"><mark>Lista </mark> de profesores.</p>
                                    <div class="form-floating mb-3">
                                              <asp:DropDownList ID="DDLProf" class="form-select" runat="server"></asp:DropDownList>
                                              <label for="DDLProf">Profesores</label>
                                    </div>    
                            </div>
                            <div class="mb-3">
                                    <asp:Label ID="Label1" class="fs-3" runat="server" Text="Registro profesor a grupo"></asp:Label>
                                    <asp:Label ID="Label2" class="fs-3" runat="server" Text="Actualización de profesor grupo"></asp:Label>
                                    <div class="form-floating mb-3">
                                            <asp:TextBox ID="TB1" class="form-control" runat="server"></asp:TextBox>
                                            <label for="TB1">Nota extra</label>
                                            
                                    </div> 
                                    <div class="form-floating mb-3">
                                            <asp:TextBox ID="TB2" class="form-control" runat="server"></asp:TextBox>
                                            <label for="TB2">Segunda nota extra</label>
                                    </div>  
                                    <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                            <asp:Button ID="BTNR" class="btn btn-success btn-lg" runat="server" Text="Agregar" OnClick="BTNR_Click" />
                                            <asp:Button ID="BTNM" class="btn btn-warning btn-lg" runat="server" Text="Actualizar" OnClick="BTNM_Click" />
                                    </div>
                            </div>
                            <br />
                            <hr />
                            <%--Actualización--%>
                            <br />
                            <div class="form-group">
                                            <asp:Label ID="Label3" class="fs-3" runat="server" Text="Eliminar registro seleccionado"></asp:Label>
                                <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                        <asp:Button ID="BTND" class="btn btn-danger btn-lg" runat="server" Text="Eliminar" OnClick="BTND_Click" />
                                </div>
                                <br />
                                
                            </div>
                            <asp:Label ID="LBLH" runat="server" Text="Label"></asp:Label>


                        </div>
                        
                    </div>
                    <div class="col-md-3"></div>

                </div>
                 

            </div>
        </div>


            


        <%--separador--%>





                 <asp:ScriptManager ID="smrTemplate" ScriptMode="Release" AsyncPostBackTimeout="360000" EnablePageMethods="true" runat="server"> 
    
                        <Scripts>
                            <asp:ScriptReference Path="~/js/jquery-3.4.1.min.js" />
                            <asp:ScriptReference Path="~/Scripts/bootstrap.min.js" />

                        </Scripts>

                </asp:ScriptManager>
    </form>
</body>
</html>
