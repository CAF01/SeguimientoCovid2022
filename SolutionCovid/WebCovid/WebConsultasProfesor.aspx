<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebConsultasProfesor.aspx.cs" Inherits="WebCovid.WebConsultasProfesor" %>

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
                    <a class="nav-link" aria-current="page" href="WebProfesor.aspx">Profesor</a>
                  </li>
                  <li class="nav-item">
                    <a class="nav-link" aria-current="page" href="WebGrupoProfe.aspx">Grupos de un Profesor</a>
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
                    <a class="nav-link active" href="WebConsultasProfesor.aspx">Contagios Profesor</a>
                  </li>
                    <li class="nav-item">
                    <a class="nav-link" href="WebIncapacidad.aspx">Incapadidad Profesor</a>
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
                        <h1 class="display-4 text-primary">Verificación de casos para profesores</h1>
                        <br />
                </div>
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        
                        <h3 class="fw-bold ">Mostrar todos los profesores contagiados de un programa educativo en un cuatrimestre especifico</h3>
      <%--                  <div class="text-center">
                        </div>--%>
                        <div class="form-group">
                            <div class="mb-3">
                                    <p class="fs-3"><mark>Selecciona </mark> un programa educativo</p>
                                    <div class="form-floating mb-3">
                                                <asp:DropDownList ID="DDLProgram" class="form-select" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="DDLProgram_SelectedIndexChanged"></asp:DropDownList>
                                                <label for="DDLProgram">Programas educativos</label>
                                    </div>
                            </div>
                            <div class="mb-3">
                                    <p class="fs-3"><mark>Selecciona </mark> un cuatrimestre</p>
                                    <div class="form-floating mb-3">
                                            <asp:DropDownList ID="DDLCuatri" class="form-select" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="DDLCuatri_SelectedIndexChanged"></asp:DropDownList>
                                            <label for="DDLCuatri">Cuatrimestres</label>
                                    </div>
                                                                       
                            </div>
                            <div class="mb-3">
                                     <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                         <asp:Button ID="BTNMostrarDatos" class="btn btn-primary btn-lg" runat="server" Text="Mostrar lista de contagios" OnClick="BTNMostrarDatos_Click" />
                                    </div>
                            </div>
                            <br />
                            <div class="mb-3">
                                    <div class="text-center">
                                        <h4 class="text-danger">Registro de Casos Positivos</h4>
                                    </div>
                                    <asp:GridView ID="GVContagios" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None">
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#33276A" />
                                    </asp:GridView>
                            </div>
                            <hr />
                            <%--Actualización--%>
                            <br />
                            <h3 class="fw-bold ">Mostrar los contagios de un profesor, y conocer los detalles de cada contagio (pruebas covid, periodos de incapacidad, etc.)</h3>
                            <br />    
                                <div class="text-center">
                                    <h4 class="text-danger">Lista de profesores registrados</h4>
                                </div>
                                
                            <div class="form-group">
                                    <div class="mb-3">
                                            <p class="fs-3"><mark>Selecciona </mark> un registro de profesor para <mark> verificar </mark> información de contagios.</p>
                                            <asp:GridView ID="GVProfesores" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GVProfesores_SelectedIndexChanged">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:CommandField ButtonType="Button" SelectText="Ver información" ShowSelectButton="True" />
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
                                    <div class="mb-3">
                                            <asp:Label ID="LBL1" class="fs-3" runat="server" Text="Label">Seleccionar caso Covid.</asp:Label>
                                            <asp:GridView ID="GVCasos" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="GVCasos_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" ButtonType="Button" />
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
                                            <br />
                                            <asp:Label ID="LBL2" class="fs-3" runat="server" Text="Label">Ver prueba Covid registrada.</asp:Label>
                                            <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                                     <asp:Button ID="BTNPrueba" class="btn btn-dark btn-lg" OnClientClick="openInNewTab();" runat="server" Text="Abrir Prueba covid" OnClick="BTNPrueba_Click" />
                                            </div>
                                            <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                                     <asp:Image ID="Img1" class="img-fluid" runat="server" />
                                            </div>
                                            

                                    </div>
                                    <br />
                                    <div class="mb-3">
                                            <asp:Label ID="LBL3" class="fs-3" runat="server" Text="Verificar Periodos de Incapacidad registrados."></asp:Label>
                                            <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                                    <asp:Button ID="BTNInca" class="btn btn-secondary btn-lg" runat="server" Text="Ver Periodos de incapacidad" OnClick="BTNInca_Click" />
                                            </div>
                                            
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList ID="DDLInca" class="form-select" runat="server" OnSelectedIndexChanged="DDLInca_SelectedIndexChanged"></asp:DropDownList>
                                                <%--<label for="DDLInca">Periodos de incapacidad</label>--%>
                                            </div>
                                            <br />

                                    </div>
                                    <div class="mb-3">
                                            <asp:Label ID="LBL4" class="fs-3" runat="server" Text="Abrir comprobante de incapacidad."></asp:Label>
                                            <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                                    <asp:Button ID="BTNShowInca" runat="server" class="btn btn-secondary btn-lg" OnClientClick="openInNewTab();" Text="Ver Comprobante de Incapacidad" OnClick="BTNShowInca_Click" />
                                            </div>
                                             <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                                   <asp:Image ID="Img2" class="img-fluid" runat="server" />
                                            </div>
                                    </div>
                                    <div class="mb-3">
                                        <asp:Label ID="LBL5" class="fs-3" runat="server" Text="Seguimientos de caso."></asp:Label>
                                        <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                            <asp:Button ID="BTNShowSegui" class="btn btn-primary btn-lg" runat="server" Text="Mostrar seguimientos" OnClick="BTNShowSegui_Click" />
                                        </div>       
                                        <br />
                                        <asp:GridView ID="GVSeguimientos" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                                        </asp:GridView>
                                      
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
