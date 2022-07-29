<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebIncapacidad.aspx.cs" Inherits="WebCovid.WebIncapacidad" %>

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
                    <a class="nav-link" href="WebAlumno.aspx">Alumno</a>
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


        <%--separador--%>

        <div class="form-group">
            <br />
            <div class="container-fluid">
                <div class="text-center">
                        <h1 class="display-4 text-primary">Gestión de incapacidades</h1>
                        <br />
                </div>
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        
                        <h3 class="fw-bold ">Asignación de incapacidad</h3>
                        <div class="text-center">
                            <h4 class="text-danger">Registros de casos Positivos de Covid - 2022</h4>
                        </div>
                        <div class="form-group">
                            <div class="mb-3">
                                    <p class="fs-3"><mark>Selecciona </mark> un registro de caso positivo de profesor.</p>
                                     <asp:GridView ID="GVPos" class="table table-striped" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Button" SelectText="Seleccionar caso" ShowSelectButton="True" />
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
                                    <p class="fs-3"><mark>Indica </mark> la fecha de inicio para la incapacidad.</p>
                                    <asp:Calendar ID="Cal1" runat="server" BackColor="White" BorderColor="White" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" BorderWidth="1px">
                                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                            <OtherMonthDayStyle ForeColor="#999999" />
                                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                            <TodayDayStyle BackColor="#CCCCCC" />
                                    </asp:Calendar>                                       
                            </div>
                            <br />
                            <div class="mb-3">
                                    <p class="fs-3"><mark>Indica </mark> la fecha de de fin para la incapacidad.</p>
                                    <asp:Calendar ID="Cal2" runat="server" BackColor="White" BorderColor="White" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" BorderWidth="1px">
                                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                        <OtherMonthDayStyle ForeColor="#999999" />
                                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                        <TitleStyle BackColor="White" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" BorderColor="Black" BorderWidth="4px" />
                                        <TodayDayStyle BackColor="#CCCCCC" />
                                    </asp:Calendar>


                            </div>
                            <br />
                            <div class="mb-3">
                                    
                                    <p class="fs-3">Archivo <mark>PDF O Imagen </mark> para validar incapacidad</p>
                                    <div class="input-group mb-3">
                                      <asp:FileUpload ID="FU1" class="form-control btn btn-outline-secondary" runat="server" />
                                    </div>
                                    <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                        <asp:Button ID="BTNR" runat="server" class="btn btn-success btn-lg" Text="Registrar incapacidad" OnClick="BTNR_Click" />
                                    </div>
                            </div>
                            <hr />
                            <%--Actualización--%>
                            <br />
                            <h3 class="fw-bold ">Actualización de registros de incapacidad</h3>
                                <div class="text-center">
                                    <h4 class="text-danger">Registro de Casos Positivos de Covid- 2022</h4>
                                </div>
                            <div class="form-group">
                                    <div class="mb-3">
                                            <p class="fs-3"><mark>Selecciona </mark> un caso positivo de profesor para ver <mark>incapacidades registradas </mark>.</p>
                                            <asp:GridView ID="GVPos2" class="table table-success table-striped" runat="server" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" OnSelectedIndexChanged="GVPos2_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Button" SelectText="Ver incapacidades" ShowSelectButton="True" />
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="White" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#487575" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#275353" />
                                            </asp:GridView>
                                    </div>
                                    <div class="mb-3">
                                            <p class="fs-3"><mark>Incapacidades registradas.. </mark></p>
                                            <asp:GridView ID="GVInca" class="table table-striped table-hover table-bordered border-primary" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" OnSelectedIndexChanged="GVInca_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Button" SelectText="Seleccionar" ShowSelectButton="True" />
                                                </Columns>
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
                                            <br />
                                            <p class="fs-4"><mark>Cargar </mark> documento adjunto </p>

                                            <asp:Button ID="BTNWatch" runat="server" class="btn btn-primary btn-lg" OnClientClick="openInNewTab();" Text="Visualizar comprobante" OnClick="BTNWatch_Click" />
                                            <br />
                                            <br />
                                            <p class="fs-5"><mark>Imagen </mark> de comprobante</p>
                                            <asp:Image ID="Img1" class="img-fluid" runat="server" />
                                            <br />
                                    </div>
                                    <hr />
                                    <br />
                                    <div class="mb-3">
                                            <h4 class="fw-bold ">Modificación de campos de incapacidad</h4>
                                            <br />
                                            <p class="fs-3"><mark>Indica </mark> la fecha en que se otorga el periodo de incapacidad.</p>
                                            <asp:Calendar ID="Cal3" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px">
                                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                                                <DayStyle BackColor="#CCCCCC" />
                                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                                                <TodayDayStyle BackColor="#999999" ForeColor="White" />
                                            </asp:Calendar>
                                        <br />
                                    </div>
                                    <div class="mb-3">
                                            <p class="fs-3"><mark>Indica </mark> la fecha en que finaliza el periodo de incapacidad.</p>
                                            <asp:Calendar ID="Cal4" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px">
                                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                                                <DayStyle BackColor="#CCCCCC" />
                                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                                                <TodayDayStyle BackColor="#999999" ForeColor="White" />
                                            </asp:Calendar>
                                        <br />
                                            
                                    </div>
                                    <div class="mb-3">

                                        <p class="fs-3">Archivo <mark>PDF O Imagen </mark> para validar incapacidad</p>
                                        <div class="input-group mb-3">
                                          <asp:FileUpload ID="FU2" class="form-control btn btn-outline-primary" runat="server" />
                                        </div>
                                        <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                            <asp:Button ID="BTNMod" runat="server" class="btn btn-warning btn-lg" Text="Actualizar incapacidad" OnClick="BTNMod_Click" />
                                        </div>
                                    </div>


                            </div>
                            <hr />
                            <br />
                            <h3 class="fw-bold ">Eliminación de registros de incapacidad</h3>
                            <div class="form-group">
                                <br />

                                <div class="mb-3 d-grid gap-2 col-4 mx-auto">
                                        <asp:Button ID="BTNDel" runat="server" class="btn btn-danger btn-lg" Text="Eliminar incapacidad" OnClick="BTNDel_Click" />
                                </div>
                                <br />
                                
                            </div>
                            <asp:Label ID="lblurl" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblurlS" runat="server" Visible="False"></asp:Label>



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
