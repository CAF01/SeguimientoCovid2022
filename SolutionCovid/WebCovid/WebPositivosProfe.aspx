<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebPositivosProfe.aspx.cs" Inherits="WebCovid.WebPositivosProfe" %>

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
                        function openInNewTab() {
                            window.document.forms[0].target = '_blank';
                            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
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
                        <h1 class="display-4 text-primary">Gestión de Casos Covid</h1>
                        <br />
                </div>
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        
                        <h3 class="fw-bold ">Registro de caso Positivo</h3>
                        <div class="text-center">
                            <h4 class="text-danger">Lista de Profesores - 2022</h4>
                        </div>
                        <div class="form-group">
                            <div class="mb-3">
                                    <p class="fs-3"><mark>Selecciona </mark> a un profesor para el registro.</p>
                                     <asp:GridView ID="GVProfesor" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GVProfesor_SelectedIndexChanged">
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
                            <br />
                            <div class="mb-3">
                                    <p class="fs-3"><mark>Indica </mark> la fecha en que se confirmó el caso.</p>
                                        <asp:Calendar ID="CalConfirma" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px">
                                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                                            <DayStyle BackColor="#CCCCCC" />
                                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                            <OtherMonthDayStyle ForeColor="#999999" />
                                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                            <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                                            <TodayDayStyle BackColor="#999999" ForeColor="White" />
                                        </asp:Calendar>                               
                            </div>
                            <br />
                            <div class="mb-3">
                                    <p class="fs-3">Archivo <mark>PDF O Imagen </mark> para validar incapacidad</p>
                                    <div class="input-group mb-3">
                                        <asp:FileUpload ID="FilePDFImg" class="form-control btn btn-outline-secondary" runat="server" />
                                    </div>

                            </div>
                            <br />
                            <div class="mb-3">
                                    <div class="form-floating mb-3">
                                                <asp:TextBox ID="TB1" class="form-control" runat="server"></asp:TextBox>
                                                <label for="TB1">Antecedentes</label>
                                    </div>
                                    <%-- SELECT --%>
                                    <div class="form-floating mb-3">
                                                    <asp:DropDownList ID="DDRiesgo" class="form-select" runat="server">
                                                        <asp:ListItem>Bajo</asp:ListItem>
                                                        <asp:ListItem>Medio</asp:ListItem>
                                                        <asp:ListItem>Alto</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <label for="DDRiesgo">Nivel de riesgo</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                                <asp:TextBox ID="TB2" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                <label for="TB2">Número de contagio</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                                <asp:TextBox ID="TB3" class="form-control" runat="server"></asp:TextBox>
                                                <label for="TB3">Nota Extra</label>
                                    </div>
                            </div>
                            <div class="mb-3">
                                    <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                        <asp:Button ID="BTNR" class="btn btn-success btn-lg" runat="server" Text="Registrar caso" OnClick="BTNR_Click" />
                                    </div>
                            </div>
                            <hr />
                            <%--Actualización--%>
                            <br />
                            <h3 class="fw-bold ">Modificación de Casos Covid</h3>
                                <div class="text-center">
                                    <h4 class="text-danger">Lista de Casos Positivos Registrados - 2022</h4>
                                </div>
                            <div class="form-group">
                                    <div class="mb-3">
                                            <p class="fs-3"><mark>Selecciona </mark> un caso positivo de profesor para <mark> modificar o eliminar </mark>.</p>
                                            <asp:GridView ID="GVPositivos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GVPositivos_SelectedIndexChanged">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Button" SelectText="Seleccionar" ShowSelectButton="True" />
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
                                            <br />
                                            <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                                    <asp:Button ID="BTNTabla" class="btn btn-dark btn-lg" runat="server" Text="Actualizar información" OnClick="BTNTabla_Click" />
                                            </div>
                                            <br />
                                            <p class="fs-4"><mark>Cargar </mark> documento adjunto </p>

                                            <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                                    <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-lg" OnClientClick="openInNewTab();" OnClick="Button1_Click" Text="Visualizar comprobante" />
                                            </div>
                                            
                                            <br />
                                            <p class="fs-5"><mark>Imagen </mark> de comprobante</p>
                                            <asp:Image ID="Image1" class="img-fluid" runat="server" />
                                            <br />
                                    </div>
                                    <hr />
                                    <br />
                                    <div class="mb-3">
                                            <h4 class="fw-bold ">Modificación de campos de caso</h4>
                                            <br />    
                                            <p class="fs-3"><mark>Indica </mark> la fecha en que se confirmo la enfermedad.</p>
                                            <asp:Calendar ID="Cal2" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px">
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

                                            <p class="fs-3">Archivo <mark>PDF O Imagen </mark> para validar incapacidad</p>
                                            <div class="input-group mb-3">
                                                 <asp:FileUpload ID="FileUp2" class="form-control btn btn-outline-primary" runat="server" />
                                            </div>
                                            
                                    </div>
                                    <div class="mb-3">
                                            <div class="form-floating mb-3">
                                                        <asp:TextBox ID="TB12" class="form-control" runat="server"></asp:TextBox>
                                                        <label for="TB12">Antecedentes</label>
                                            </div>
                                            <%-- SELECT --%>
                                            <div class="form-floating mb-3">
                                                        <asp:DropDownList ID="DDL12" class="form-select" runat="server">
                                                            <asp:ListItem>Bajo</asp:ListItem>
                                                            <asp:ListItem>Medio</asp:ListItem>
                                                            <asp:ListItem>Alto</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <label for="DDL12">Nivel de riesgo</label>
                                            </div>
                                            <div class="form-floating mb-3">
                                                        <asp:TextBox ID="TB22" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                        <label for="TB22">Número de contagio</label>
                                            </div>
                                            <div class="form-floating mb-3">
                                                        <asp:TextBox ID="TB33" class="form-control" runat="server"></asp:TextBox>      
                                                        <label for="TB33">Nota Extra</label>
                                            </div>
                                            <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                                <asp:Button ID="Button2" runat="server" class="btn btn-warning btn-lg" Text="Modificar registro" OnClick="BTNMod_Click"  />
                                            </div>
                                    </div>

                            </div>
                            <hr />
                            <br />
                            <h3 class="fw-bold ">Eliminación de Casos registrados</h3>
                            <div class="form-group">
                                <br />

                                <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                    <asp:Button ID="BTNDel" class="btn btn-danger btn-lg" runat="server" OnClick="BTNDel_Click" Text="Eliminar Registro" />
                                </div>
                                <br />
                                
                            </div>




                        </div>
                        
                    </div>
                    <div class="col-md-3"></div>

                </div>
                 

            </div>
        </div>

        
            <asp:Label ID="lblurl" runat="server" Visible="False"></asp:Label>



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
