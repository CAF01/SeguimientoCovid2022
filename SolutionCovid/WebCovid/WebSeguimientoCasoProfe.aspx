<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebSeguimientoCasoProfe.aspx.cs" Inherits="WebCovid.WebSeguimientoCasoProfe" %>

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
                      <a class="nav-link active dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
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
                      <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
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
                        <h1 class="display-4 text-primary">Gestión de seguimiento de casos positivos</h1>
                        <br />
                </div>
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        
                        <h3 class="fw-bold ">Crear nuevo seguimiento</h3>
                        <div class="text-center">
                            <h4 class="text-danger">Registros de casos Positivos de Covid - 2022</h4>
                        </div>
                        <div class="form-group">
                            <div class="mb-3">
                                    <p class="fs-3"><mark>Selecciona </mark> un registro de caso positivo de profesor.</p>
                                            <asp:GridView ID="GVPositivos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GVPositivos_SelectedIndexChanged">
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
                            <div class="text-center">
                                    <h4 class="text-danger">Tabla de médicos registrados</h4>
                            </div>
                            <div class="mb-3">
                                    <p class="fs-3"><mark>Selecciona </mark> al médico encargado del seguimiento.</p>
                                    <asp:GridView ID="GVMedicos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:CommandField ButtonType="Button" SelectText="Elegir médico" ShowSelectButton="True" />
                                                </Columns>
                                                <EditRowStyle BackColor="#7C6F57" />
                                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#E3EAEB" />
                                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                                    </asp:GridView>
                            </div>
                            <div class="mb-3">
                                    <p class="fs-3"><mark>Completa </mark> los campos para el seguimiento.</p>
<%--                                    <div class="form-floating mb-3">
                                                <asp:TextBox ID="TextBox1" class="form-control" runat="server"></asp:TextBox>
                                                <label for="TB1">Antecedentes</label>
                                    </div>--%>
                                    <%-- SELECT --%>
                                    <div class="form-floating mb-3">
                                                    <asp:DropDownList ID="DDLComunica" class="form-select" runat="server">
                                                        <asp:ListItem>Vía Whatsapp</asp:ListItem>
                                                        <asp:ListItem>Personal</asp:ListItem>
                                                        <asp:ListItem>Vía teléfonica</asp:ListItem>
                                                        <asp:ListItem>Correo Electrónico</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <label for="DDLComunica">Forma de comunicación</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                                <asp:TextBox ID="TB1" class="form-control" runat="server"></asp:TextBox>
                                                <label for="TB1">Reporte General</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                                 <asp:TextBox ID="TB2" class="form-control" runat="server"></asp:TextBox>
                                                <label for="TB2">Resultados de entrevista</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                                <asp:TextBox ID="TB3" class="form-control" runat="server"></asp:TextBox>
                                                <label for="TB3">Nota extra (opcional)</label>
                                    </div>
                            </div>
                            <div class="mb-3">
                                    
                                    <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                        <asp:Button ID="BTNRegist" runat="server" class="btn btn-success btn-lg" Text="Agregar seguimiento" OnClick="BTNRegist_Click" />
                                    </div>
                            </div>
                            <hr />
                            <%--Actualización--%>
                            <br />
                            <h3 class="fw-bold ">Actualización de seguimientos de caso</h3>
                                <div class="text-center">
                                    <h4 class="text-danger">Registro de Casos Positivos de Covid- 2022</h4>
                                </div>
                            <div class="form-group">
                                    <div class="mb-3">
                                           <p class="fs-3"><mark>Selecciona </mark> un registro de caso positivo de profesor.</p>
                                            <asp:GridView ID="GVPositivos2" runat="server" CellPadding="3" GridLines="Horizontal" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" OnSelectedIndexChanged="GVPositivos2_SelectedIndexChanged">
                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                <Columns>
                                                    <asp:CommandField ButtonType="Button" SelectText="Buscar seguimiento caso" ShowSelectButton="True" />
                                                </Columns>
                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                            </asp:GridView>
                                    </div>
                                    <div class="mb-3">
                                                <asp:Label ID="Label1" class="fs-3" runat="server" Text="Seleccionar seguimiento"></asp:Label>
                                                <asp:DropDownList ID="DDLSeguimientos" class="form-select" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLSeguimientos_SelectedIndexChanged1"></asp:DropDownList>
                                                <%--<label for="DDLSeguimientos">Seguimiento de caso</label>--%>
                                    </div>
                                    <div class="mb-3">
                                            <asp:Label ID="LbMed" class="fs-3" runat="server" Text="Selecciona al médico encargado del seguimiento"></asp:Label>
                                            <%--<p ><mark>Selecciona </mark> al médico encargado del seguimiento.</p>--%>
                                            <asp:GridView ID="GVMed2" runat="server" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnSelectedIndexChanged="GVMed2_SelectedIndexChanged">
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Button" SelectText="Seleccionar Médico" ShowSelectButton="True" />
                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <RowStyle ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                            </asp:GridView>
                                    </div>
                                    <div class="mb-3">
                                            <p class="fs-3"><mark>Actualizar </mark> fecha de seguimiento.</p>
                                             <asp:Calendar ID="Calendar" runat="server" OnSelectionChanged="Calendar_SelectionChanged" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px">
                                                 <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                                                 <DayStyle BackColor="#CCCCCC" />
                                                 <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                                 <OtherMonthDayStyle ForeColor="#999999" />
                                                 <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                 <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                                                 <TodayDayStyle BackColor="#999999" ForeColor="White" />
                                            </asp:Calendar>
                                    </div>
                                    <div class="mb-3">
                                        <p class="fs-3"><mark>Completa </mark> los campos para el seguimiento.</p>
                                        <%-- SELECT --%>
                                            <div class="form-floating mb-3">
                                                            <asp:DropDownList ID="DDLComunica2" class="form-select" runat="server">
                                                                <asp:ListItem>Vía Whatsapp</asp:ListItem>
                                                                <asp:ListItem>Personal</asp:ListItem>
                                                                <asp:ListItem>Vía teléfonica</asp:ListItem>
                                                                <asp:ListItem>Correo Electrónico</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label for="DDLComunica2">Forma de comunicación</label>
                                            </div>
                                            <div class="form-floating mb-3">
                                                        <asp:TextBox ID="TB12" class="form-control" runat="server"></asp:TextBox>
                                                        <label for="TB12">Reporte General</label>
                                            </div>
                                            <div class="form-floating mb-3">
                                                        <asp:TextBox ID="TB22" class="form-control" runat="server"></asp:TextBox>
                                                        <label for="TB22">Resultados de entrevista</label>
                                            </div>
                                            <div class="form-floating mb-3">
                                                        <asp:TextBox ID="TB32" class="form-control" runat="server"></asp:TextBox>
                                                        <label for="TB32">Nota extra (opcional)</label>
                                            </div>
                                        
                                    </div>
                                    <div class="mb-3">
                                        <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                            <asp:Button ID="BTNMod" runat="server" class="btn btn-warning btn-lg" Text="Actualizar registro de seguimiento" OnClick="BTNMod_Click" />
                                        </div>
                                    </div>


                            </div>
                            <hr />
                            <br />
                            <h3 class="fw-bold ">Eliminación de registros de incapacidad</h3>
                            <div class="form-group">
                                    <div class="mb-3">
                                            <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                                <asp:Button ID="BTNDel" class="btn btn-danger btn-lg" runat="server" Text="Borrar Registro de Seguimiento de caso" OnClick="BTNDel_Click" />
                                            </div>
                                    </div>


                                
                            </div>



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
