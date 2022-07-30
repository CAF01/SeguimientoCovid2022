<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebSeguimientoAlumno.aspx.cs" Inherits="WebCovid.WebSeguimientoAlumno" %>

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
    <form id="formSeguimientoAlumno" runat="server">
        
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
                  <li class="nav-item">
                    <a class="nav-link" href="WebPositivoAlumno.aspx">Positivo Alumno</a>
                  </li>
                  <li class="nav-item">
                    <a class="nav-link active" href="WebSeguimientoAlumno.aspx">Seguimiento Alumno</a>
                  </li>
                  <li class="nav-item">
                    <a class="nav-link" href="WebPositivosProfe.aspx">Positivo Profesor</a>
                  </li>
                  <li class="nav-item">
                    <a class="nav-link" href="WebSeguimientoCasoProfe.aspx">Seguimiento Profesor</a>
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
                        <h1 class="display-4 text-primary">Gestión de Seguimientos a Casos Positivos de Alumnos</h1>
                    </div>                    
                    
                    <div class="row">
                        <!---Añadir-->
                        <h3>Nuevo seguimiento a caso positivo de alumno</h3>
                        <label for="ddlPosAl">Caso positivo de alumno</label><br />
                        <asp:DropDownList ID="ddlPosAl" runat="server" Height="50px" Width="440px" CssClass="form-select" AppendDataBoundItems="True"></asp:DropDownList>  
                        <br />
                        <br />
                        <br />
                        <label for="ddlMedico">Médico responsable</label><br />
                        <asp:DropDownList ID="ddlMedico" runat="server" Height="50px" Width="440px" CssClass="form-select" AppendDataBoundItems="True"></asp:DropDownList>  
                        <br />
                        <br />
                        <br />
                        <div class="col-12">
                        <label for="calFechaSeg">Fecha de seguimiento</label>
                        <asp:Calendar ID="calFechaSeg" runat="server"></asp:Calendar>
                        </div>
                        <br />
                        <br />
                        <div>
                            <br />
                        </div>
                        <label for="txtComunc">Forma de comunicación</label><br />
                        <asp:TextBox ID="txtComunic" runat="server" Height="50px" Width="440px" CssClass="form-control" placeholder="Correo institucional, Correo personal, WhatsApp, Messenger"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <label for="txtReporte">Reporte</label><br />
                        <asp:TextBox ID="txtReporte" runat="server" Height="50px" Width="440px" CssClass="form-control" placeholder="Los síntomas disminuyeron, únicamente presenta tos seca"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <label for="txtEntrevista">Entrevista</label><br />
                        <asp:TextBox ID="txtEntrevista" runat="server" Height="50px" Width="440px" CssClass="form-control" placeholder="Fecha a acordar"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <label for="txtExtra">Extra</label><br />
                        <asp:TextBox ID="txtExtra" runat="server" Height="50px" Width="440px" CssClass="form-control" placeholder="Contagio por un familiar"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <div class="col-12">
                        <asp:Button ID="btnInsertarSegAl" runat="server" Text="Añadir seguimiento" class="btn btn-primary btn-lg" OnClick="btnInsertarSegAl_Click"/>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <!---Ver-->
                        <h2>Seguimientos a casos positivos de alumnos</h2>
                        <p>Para actualizar, selecciona el registro dando clic sobre <b>Elegir seguimiento</b> y modifica los datos en el formulario, después da clic en el botón <b>Editar</b> correspondiente al registro del seguimiento.</p>
                        <p>Para eliminar el seguimiento de un caso positivo, da clic en el botón <b>Eliminar</b> correspondiente al registro del seguimiento que se quiere eliminar.</p>
                        <asp:GridView ID="gvSeguimientoAlumno" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="gvSeguimientoAlumno_RowDeleting" OnRowUpdating="gvSeguimientoAlumno_RowUpdating" OnSelectedIndexChanging="gvSeguimientoAlumno_SelectedIndexChanging">
                            <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Selecciona" ShowHeader="True" Text="Elegir seguimiento" ControlStyle-CssClass="btn btn-outline-primary"/>
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
                        <div>
                            <br />
                            <br />
                        </div>
                        <br />
                        <br />
                        <!---Seguimiento en un prog ed y un cuatrimestre-->
                        <h2>Seguimientos a casos positivos de alumnos con filtros: <b>Programa educativo</b> y <b>Cuatrimestre</b></h2>
                        <p>Selecciona el <b>Programa Educativo</b> y el <b>Cuatrimestre</b> del que quieras consultar los seguimientos a casos positivos de alumnos.</p>
                        <label for="ddlProgEd">Programa Educativo</label><br />
                        <asp:DropDownList ID="ddlProgEd" runat="server" Height="50px" Width="440px" CssClass="form-select" AppendDataBoundItems="True" ></asp:DropDownList>  
                        <br />
                        <br />
                        <br />
                        <label for="ddlCuatrimestre">Cuatrimestre</label><br />
                        <asp:DropDownList ID="ddlCuatrimestre1" runat="server" Height="50px" Width="440px" CssClass="form-select" AppendDataBoundItems="True" ></asp:DropDownList>  
                        <br />
                        <br />
                        <br />
                        <div class="col-12">
                        <asp:Button ID="btnConsComp1" runat="server" Text="Consultar datos" class="btn btn-primary btn-lg" OnClick="btnConsComp1_Click"/>
                        </div>
                        <br />
                        <br />
                        <br />
                        <h4>Resultados</h4>
                        <br />
                        <br />
                        <asp:GridView ID="gvSegProgEdCuat" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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
                        <br />
                        <br />
                        <div>
                            <br />
                            <br />
                        </div>
                        <br />
                        <br />
                        <!---Alumnos contagiados de un programa educativo, en un cuatrimestre específico y de un grupo en partícular-->
                        <h2>Seguimientos a casos positivos de alumnos con filtros: <b>Programa Educativo</b>, <b>Cuatrimestre</b> y <b>Grupo</b></h2>
                        <p>Selecciona el <b>Programa Educativo</b>, el <b>Cuatrimestre</b> y el <b>Grupo</b> del que quieras consultar los seguimientos de casos positivos de alumnos.</p>
                        <label for="ddlProgEd2">Programa Educativo</label><br />
                        <asp:DropDownList ID="ddlProgEd2" runat="server" Height="50px" Width="440px" CssClass="form-select" AppendDataBoundItems="True" ></asp:DropDownList>  
                        <br />
                        <br />
                        <br />
                        <label for="ddlCuatrimestre2">Cuatrimestre</label><br />
                        <asp:DropDownList ID="ddlCuatrimestre2" runat="server" Height="50px" Width="440px" CssClass="form-select" AppendDataBoundItems="True" ></asp:DropDownList>  
                        <br />
                        <br />
                        <br />
                        <label for="ddlGrupo">Grupo</label><br />
                        <asp:DropDownList ID="ddlGrupo" runat="server" Height="50px" Width="440px" CssClass="form-select" AppendDataBoundItems="True" ></asp:DropDownList>  
                        <br />
                        <br />
                        <br />
                        <div class="col-12">
                        <asp:Button ID="btnConsComp2" runat="server" Text="Consultar datos" class="btn btn-primary btn-lg" OnClick="btnConsComp2_Click"/>
                        </div>
                        <br />
                        <br />
                        <br />
                        <h4>Resultados</h4>
                        <br />
                        <br />
                        <asp:GridView ID="gvSegProgEdCuatGru" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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
                        <br />
                        <br />
                        <div>
                            <br />
                            <br />
                        </div>
                        <br />
                        <br />
                        <!---Seguimiento de un alumno (por su registro) en un cuatrimestre específico-->
                        <h2>Seguimientos a casos positivos de <b>un alumno</b> con filtros: <b>Cuatrimestre</b></h2>
                        <p>Selecciona la <b>Matrícula del alumno</b> y el <b>Cuatrimestre</b> en que quieras consultar sus seguimientos a casos positivos.</p>
                        <label for="ddlAlumno">Matrícula del alumno</label><br />
                        <asp:DropDownList ID="ddlAlumno" runat="server" Height="50px" Width="440px" CssClass="form-select" AppendDataBoundItems="True" ></asp:DropDownList>  
                        <br />
                        <br />
                        <br />
                        <label for="ddlCuatrimestre3">Cuatrimestre</label><br />
                        <asp:DropDownList ID="ddlCuatrimestre3" runat="server" Height="50px" Width="440px" CssClass="form-select" AppendDataBoundItems="True" ></asp:DropDownList>  
                        <br />
                        <br />
                        <br />
                        <div class="col-12">
                        <asp:Button ID="btnConsComp3" runat="server" Text="Consultar datos" class="btn btn-primary btn-lg" OnClick="btnConsComp3_Click"/>
                        </div>
                        <br />
                        <br />
                        <br />
                        <h4>Resultados</h4>
                        <br />
                        <br />
                        <asp:GridView ID="gvSegAlCuatri" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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
                        <br />
                        <br />
                        <div>
                            <br />
                            <br />
                        </div>
                        <br />
                        <br />
                 </div>
            </div>                      
        </div>
    </form>
</body>
</html>
