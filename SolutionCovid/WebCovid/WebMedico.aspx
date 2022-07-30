<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebMedico.aspx.cs" Inherits="WebCovid.WebMedico" %>

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
                    <a class="nav-link active" href="WebMedico.aspx">Médico</a>
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
            <div class="container-fluid">
                    <div class="text-center">
                        <h1 class="display-4 text-primary">Gestión para Médicos</h1>
                    </div>
                    

                    <div class="row">
                        <div class="col-md-3"></div>

                        <div class="col-md-6">
                            <br />
                            <div class="mb-3">

                                <div class="text-center">                               

                                    <nav>
                                          <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                            <button class="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home" type="button" role="tab" aria-controls="nav-home" aria-selected="true">Añadir</button>
                                              <button class="nav-link" id="nav-docs-tab" data-bs-toggle="tab" data-bs-target="#nav-docs" type="button" role="tab" aria-controls="nav-prof" aria-selected="true">Información de Médicos</button>
                                          </div>
                                    </nav>
                                        <div class="tab-content" id="nav-tabContent">
                                              <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab" tabindex="0">
                                                  <br />
                                                      <div class="text-center">
                                                              <div class="shadow-lg p-3 mb-5 bg-body rounded">
                                                                  <label for="" class="form-label fs-3 fw-semibold">Añadir nuevo Médico</label>
                                                                      <div class="mb-3">

                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TB1" runat="server" type="text" class="form-control"></asp:TextBox>
                                                                                    <label for="TB1">Nombre</label>
                                                                                </div>
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TB2" runat="server" type="text" class="form-control"></asp:TextBox>
                                                                                    <label for="TB2">Apellido Paterno</label>
                                                                                </div>
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TB3" runat="server" type="text" class="form-control"></asp:TextBox>
                                                                                    <label for="TB3">Apellido Materno</label>
                                                                                </div>
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TB4" runat="server" type="number" class="form-control" TextMode="Number"></asp:TextBox>
                                                                                    <label for="TB4">Telefono</label>
                                                                                </div>   
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TB5" runat="server" type="text" class="form-control"></asp:TextBox>
                                                                                    <label for="TB5">Correo</label>
                                                                                </div> 
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TB6" runat="server" class="form-control" ></asp:TextBox>
                                                                                    <label for="TB6">Horario</label>
                                                                                </div> 
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TB7" runat="server" class="form-control" ></asp:TextBox>
                                                                                    <label for="TB7">Especilidad</label>
                                                                                </div> 
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TB8" runat="server" class="form-control" ></asp:TextBox>
                                                                                    <label for="TB8">Extra</label>
                                                                                </div> 

                                                                                <hr />

                                                                                <div class="d-grid gap-2 col-4 mx-auto">
                                                                                    <asp:Button ID="BTN1" runat="server" type="button" class="btn text-light bg-success btn-lg" Text="Agregar Médico" OnClick="BTN1_Click"  />
                                                                                </div>
                        
                                                                      </div>


                                                              </div>
                                              
                                               

                                                      </div>
                                         

                                      
                                              </div>




                                            <%--SeparadorTarjetas--%>

                                            <div class="tab-pane fade show" id="nav-docs" role="tabpanel" aria-labelledby="nav-docs-tab" tabindex="0">
                                                  <br />
                                                      <div class="text-center">
                                                              <div class="shadow-lg p-3 mb-5 bg-body rounded">
                                                                  <label for="" class="form-label fs-3 fw-semibold">Lista de Médicos</label>
                                                                  <div class="form-floating mb-3">
                                                                        <div class="row align-items-start">
                                                                              <div class="d-grid gap-2 col-4 mx-auto">
                                                                                <asp:Button ID="BTNGrid" class="btn text-light bg-secondary mb-2 btn-lg" runat="server" Text="Actualizar datos" OnClick="BTNGrid_Click"/>
                                                                              </div>
                                                                         </div>

                                                                        <br />
                                                                        <asp:GridView ID="GVMedico" class="table table-striped table-hover" runat="server" OnSelectedIndexChanged="GVMedico_SelectedIndexChanged" >
                                                                            <Columns>
                                                                                <asp:CommandField ButtonType="Button" HeaderText="Selección de registro" SelectText="Seleccionar" ShowHeader="True" ShowSelectButton="True">
                                                                                <ItemStyle ForeColor="#0066FF" />
                                                                                </asp:CommandField>
                                                                            </Columns>
                                                                            <SelectedRowStyle BackColor="#6699FF" BorderColor="#6666FF" ForeColor="White" />
                                                                        </asp:GridView>
                                                            

                                                                  </div>
                                                                  <br />

                                                                  <label for="" class="form-label fs-3 fw-semibold">Actualizar Médico</label>
                                                                      <div class="mb-3">

                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TBM1" runat="server" type="text" class="form-control"></asp:TextBox>
                                                                                    <label for="TBM1">Nombre</label>
                                                                                </div>
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TBM2" runat="server" type="text" class="form-control"></asp:TextBox>
                                                                                    <label for="TBM2">Apellido Paterno</label>
                                                                                </div>
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TBM3" runat="server" type="text" class="form-control"></asp:TextBox>
                                                                                    <label for="TBM3">Apellido Materno</label>
                                                                                </div>
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TBM4" runat="server" type="number" class="form-control" TextMode="Number"></asp:TextBox>
                                                                                    <label for="TBM4">Telefono</label>
                                                                                </div>   
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TBM5" runat="server" type="text" class="form-control"></asp:TextBox>
                                                                                    <label for="TBM5">Correo</label>
                                                                                </div> 
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TBM6" runat="server" class="form-control" ></asp:TextBox>
                                                                                    <label for="TBM6">Horario</label>
                                                                                </div> 
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TBM7" runat="server" class="form-control" ></asp:TextBox>
                                                                                    <label for="TBM7">Especilidad</label>
                                                                                </div> 
                                                                                <div class="form-floating mb-3">
                                                                                    <asp:TextBox ID="TBM8" runat="server" class="form-control" ></asp:TextBox>
                                                                                    <label for="TBM8">Extra</label>
                                                                                </div> 

                                                                                <hr />

                                                                                <div class="d-grid gap-2 col-4 mx-auto">
                                                                                    <asp:Button ID="BTNMod" runat="server" type="button" class="btn text-light bg-warning btn-lg" Text="Actualizar datos de Médico" OnClick="BTNMod_Click"  />
                                                                                </div>

                                                                                <br />
                                                                                <hr />
                                                                                <label for="BTNDel" class="form-label fs-3 fw-semibold">Eliminar Médico</label>
                                                                                <div class="d-grid gap-2 col-4 mx-auto">
                                                                                    <asp:Button ID="BTNDel" runat="server" type="button" class="btn text-light bg-danger btn-lg" Text="Eliminar Médico" OnClick="BTNDel_Click"  />
                                                                                </div>
                        
                                                                      </div>


                                                              </div>
                                              
                                               

                                                      </div>
                                         

                                      
                                              </div>

                                                                                    

                                          </div>

                                                                                 

                                       


                                        </div>

                                </div>

                            </div>
                          
                            

                        </div>


                        <div class="col-md-3"></div>
                    </div>

                     
            </div>






        <div>
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
