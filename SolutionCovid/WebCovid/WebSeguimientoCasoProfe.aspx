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




        <div>

            <h1>Tabla de casos positivos de Covid</h1>
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

            <h2>Tabla de médicos</h2>
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

            <br />
            Forma de comunicación
            <asp:DropDownList ID="DDLComunica" runat="server">
                <asp:ListItem>Vía Whatsapp</asp:ListItem>
                <asp:ListItem>Personal</asp:ListItem>
                <asp:ListItem>Vía teléfonica</asp:ListItem>
                <asp:ListItem>Correo Electrónico</asp:ListItem>
            </asp:DropDownList>
            <br />
            Reporte General
            <asp:TextBox ID="TB1" runat="server"></asp:TextBox>
            <br />
            Resultados de entrevista
            <asp:TextBox ID="TB2" runat="server"></asp:TextBox>
            <br />
            Extra
            <asp:TextBox ID="TB3" runat="server"></asp:TextBox>
            <br />
            <br />
            <br />
            <asp:Button ID="BTNRegist" runat="server" Text="Agregar seguimiento" OnClick="BTNRegist_Click" />


            <br />
            <br />
            <br />
            <h2>Modificar un registro de seguimiento</h2>
            Lista de seguimientos de un caso Positivo *Debe seleccionar un caso de arriba*
            <br />
            <asp:DropDownList ID="DDLSeguimientos" runat="server" OnSelectedIndexChanged="DDLSeguimientos_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            <br />
            Actualizar fecha
            <asp:Calendar ID="Calendar" runat="server" OnSelectionChanged="Calendar_SelectionChanged"></asp:Calendar>
            <br />
            Forma de comunicación
            <asp:DropDownList ID="DDLComunica2" runat="server">
                <asp:ListItem>Vía Whatsapp</asp:ListItem>
                <asp:ListItem>Personal</asp:ListItem>
                <asp:ListItem>Vía teléfonica</asp:ListItem>
                <asp:ListItem>Correo Electrónico</asp:ListItem>
            </asp:DropDownList>
            <br />
            Reporte General
            <asp:TextBox ID="TB12" runat="server"></asp:TextBox>
            <br />
            Resultados de entrevista
            <asp:TextBox ID="TB22" runat="server"></asp:TextBox>
            <br />
            Extra
            <asp:TextBox ID="TB32" runat="server"></asp:TextBox>
            <br />
            <br />
            <br />
            <asp:Button ID="BTNMod" runat="server" Text="Actualizar registro de seguimiento" OnClick="BTNMod_Click" />


            <br />
            <br />

            <asp:Button ID="BTNDel" runat="server" Text="Borrar Registro de Seguimiento de caso" OnClick="BTNDel_Click" />
            <br />


        </div>
        
         <asp:ScriptManager ID="smrTemplate" ScriptMode="Release" AsyncPostBackTimeout="360000" EnablePageMethods="true" runat="server"> 
    
                        <Scripts>
                            <asp:ScriptReference Path="~/js/jquery-3.4.1.min.js" />
                            <asp:ScriptReference Path="~/Scripts/bootstrap.min.js" />

                        </Scripts>

         </asp:ScriptManager>
    </form>
</body>
</html>
