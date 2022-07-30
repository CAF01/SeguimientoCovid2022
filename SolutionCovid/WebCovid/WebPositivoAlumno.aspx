<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebPositivoAlumno.aspx.cs" Inherits="WebCovid.WebPositivoAlumno" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Sistema Seguimiento Covid</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/sweetalert2.min.js"></script>
    <link rel="Content/stylesheet" href="sweetalert2.min.css"/>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script type="text/javascript">
        function Alert(t, m, tipo) {
            Swal.fire(t, m, tipo)
        }
    </script>
    <link href="https://fonts.googleapis.com/css2?family=Sen&display=swap" rel="stylesheet"/>
    <style>
      body {
        font-family: 'Sen', serif;
      }
    </style>
</head>
<body>
    <form id="formPositivoAlumno" runat="server">
        
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
                    <a class="nav-link active" href="WebPositivoAlumno.aspx">Positivo Alumno</a>
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
            <div class="container-xl">
                    <div class="text-center">
                        <h1 class="display-4 text-primary">Gestión de Casos Positivos de Alumnos</h1>
                    </div>                    
                    <br />
                    <div class="row">
                        <!---Añadir-->
                        <h3>Nuevo caso positivo de alumno</h3>
                        <label for="ddlAlumno">Alumno</label><br />
                        <asp:DropDownList ID="ddlAlumno" runat="server" Height="50px" Width="440px" CssClass="form-select" AppendDataBoundItems="True"></asp:DropDownList> 
                        <br />
                        <br />
                        <br />
                        <div class="col-12">
                        <label for="calFechaConfirm">Fecha de confirmación</label>
                        <asp:Calendar ID="calFechaConfirm" runat="server"></asp:Calendar>
                        </div>
                        <br />
                        <br />
                        <div>
                            <br />
                            <br />
                        </div>
                        <br />
                        <br />
                        <label for="txtConfirmacion">Tipo comprobante</label><br />
                        <asp:TextBox ID="txtConfirmacion" runat="server" Height="50px" Width="440px" CssClass="form-control" placeholder="PCR"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <label for="txtAnteced">Antecedentes</label><br />
                        <asp:TextBox ID="txtAnteced" runat="server" Height="50px" Width="440px" CssClass="form-control" placeholder="Tos, Fiebre, Dolor de cabeza"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <label for="txtRiesgo">Riesgo</label><br />
                        <asp:TextBox ID="txtRiesgo" runat="server" Height="50px" Width="440px" CssClass="form-control" placeholder="Alto, Medio, Bajo"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <label for="txtNumContagio">Número de contagio</label><br />
                        <asp:TextBox ID="txtNumContagio" runat="server" Height="50px" Width="440px" CssClass="form-control" placeholder="1, 2, 3, 4"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <label for="txtExtra">Extra</label><br />
                        <asp:TextBox ID="txtExtra" runat="server" Height="50px" Width="440px" CssClass="form-control" placeholder="Anotación extra"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <div class="col-12">
                        <asp:Button ID="btnInsertarPositivo" runat="server" Text="Añadir caso positivo de alumno" class="btn btn-primary btn-lg" OnClick="btnInsertarPositivo_Click"/>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <!---Ver-->
                        <h3>Casos positivos de alumnos</h3>
                        <p>Para actualizar, selecciona el registro dando clic sobre <b>Elegir caso positivo</b> y modifica los datos en el formulario, después da clic en el botón <b>Editar</b> correspondiente al registro del caso positivo.</p>
                        <p>Para eliminar un caso positivo, da clic en el botón <b>Eliminar</b> correspondiente al registro del caso positivo que se quiere eliminar.</p>
                        <asp:GridView ID="gvPositivosAlumnos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="gvPositivosAlumnos_RowDeleting" OnRowUpdating="gvPositivosAlumnos_RowUpdating" OnSelectedIndexChanging="gvPositivosAlumnos_SelectedIndexChanging">
                            <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Selecciona" ShowHeader="True" Text="Elegir caso positivo" ControlStyle-CssClass="btn btn-outline-primary"/>
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
                        <br />
                        <br />
                        <br />
                 </div>
            </div>                      
        </div>
    </form>
</body>
</html>
