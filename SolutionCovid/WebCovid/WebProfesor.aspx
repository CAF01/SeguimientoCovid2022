<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebProfesor.aspx.cs" Inherits="WebCovid.WebProfesor" %>

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
                    <a class="nav-link active" aria-current="page" href="WebProfesor.aspx">Profesor</a>
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


        

            <div class="form-group">
            <br />
            <div class="container-fluid">
                    <div class="text-center">
                        <h1 class="display-4 text-primary">Gestión para profesores</h1>
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
                                              <button class="nav-link" id="nav-search-tab" data-bs-toggle="tab" data-bs-target="#nav-search" type="button" role="tab" aria-controls="nav-home" aria-selected="true">Mostrar listado</button>
                                            <button class="nav-link" id="nav-profile-tab" data-bs-toggle="tab" data-bs-target="#nav-profile" type="button" role="tab" aria-controls="nav-profile" aria-selected="false">Actualizar/Eliminar</button>
                                          </div>
                                    </nav>
                                    <div class="tab-content" id="nav-tabContent">
                                      <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab" tabindex="0">
                                          <br />
                                          <div class="text-center">
                                              <div class="shadow-lg p-3 mb-5 bg-body rounded">
                                                  <label for="" class="form-label fs-3 fw-semibold">Añadir nuevo profesor</label>
                                                  <div class="mb-3">

                    
             <%--                                               <div class="text-center">
                                                            <h1 class="display-4 text-primary">Agregar componentes</h1>

                                                            <br />
                                                            </div>--%>
                                                            <div class="form-floating mb-3">
                                                                <asp:TextBox ID="TB1" runat="server" type="number" class="form-control" placeholder="15" TextMode="Number"></asp:TextBox>
                                                                <label for="TB1">Registro de Empleado</label>
                                                            </div>
                                                            <div class="form-floating mb-3">
                                                                <asp:TextBox ID="TB2" runat="server" type="text" class="form-control" placeholder="Perífericos"></asp:TextBox>
                                                                <label for="TB2">Nombre</label>
                                                            </div>
                                                            <div class="form-floating mb-3">
                                                                <asp:TextBox ID="TB3" runat="server" type="text" class="form-control" placeholder="Corsair"></asp:TextBox>
                                                                <label for="floatingMarca">Apellido Paterno</label>
                                                            </div>
                                                            <div class="form-floating mb-3">
                                                                <asp:TextBox ID="TB4" runat="server" type="text" class="form-control" placeholder="HS-45"></asp:TextBox>
                                                                <label for="TB4">Apellido Materno</label>
                                                            </div>
                                                            <div class="form-floating mb-3">
                                                                <asp:DropDownList ID="DDLGen" class="form-select" runat="server">
                                                                    <asp:ListItem Value="0">Masculino</asp:ListItem>
                                                                    <asp:ListItem Value="1">Femenino</asp:ListItem>
                                                                </asp:DropDownList>  
                                                                <label for="DDL">Genero</label>
                                                            </div>
                                                            <div class="form-floating mb-3">
                                                                <asp:DropDownList ID="DDLCat" class="form-select" runat="server">
                                                                    <asp:ListItem Value="0">Profesor por asignatura</asp:ListItem>
                                                                    <asp:ListItem Value="1">Profesor de tiempo Completo</asp:ListItem>
                                                                </asp:DropDownList>  
                                                                <label for="DDLCat">Categoria</label>
                                                            </div>   
                                                            <div class="form-floating mb-3">
                                                                <asp:TextBox ID="TB5" runat="server" type="email" class="form-control" placeholder="Auriculares de Diadema para PC con Sonido Envolvente 7.1 Virtual Surround con USB " TextMode="Email"></asp:TextBox>
                                                                <label for="T5">Correo</label>
                                                            </div> 
                                                            <div class="form-floating mb-3">
                                                                <asp:TextBox ID="TB6" runat="server" type="number" class="form-control" placeholder="Auriculares de Diadema para PC con Sonido Envolvente 7.1 Virtual Surround con USB " TextMode="Number"></asp:TextBox>
                                                                <label for="TB6">Celular</label>
                                                            </div>   
                                                            <div class="form-floating mb-3">
                                                                <asp:DropDownList ID="DDLEDO" class="form-select" runat="server" AppendDataBoundItems="True">
                                                                </asp:DropDownList>            
                                                                <label for="DDLEDO">Estado Civil</label>
                                                            </div>   
                                                            <hr />

                                                            <div class="d-grid gap-2 col-4 mx-auto">
                                                                <asp:Button ID="BTN1" runat="server" type="button" class="btn text-light bg-success btn-lg" Text="Agregar Profesor" OnClick="BTN1_Click" />
                                                            </div>
                        
                                                        </div>



                                                  <div class="row align-items-start">
                                                                <div class="d-grid gap-2 col-4 mx-auto">
                                                                </div> 
                                                  </div>

                                              </div>
                                              
                                               

                                          </div>
                                         

                                      
                                      </div>


                                       <div class="tab-pane fade" id="nav-search" role="tabpanel" aria-labelledby="nav-search-tab" tabindex="0">
                                          <br />

                                          <div class="text-center"></div>
                                          <div class="shadow-lg p-3 mb-5 bg-body rounded">
                                                  
                                                <label for="TB1" class="form-label fs-3 fw-semibold">Lista de profesores registrados</label>
                                                <div class="form-floating mb-3">
                                                    <div class="row align-items-start">
                                                          <div class="d-grid gap-2 col-4 mx-auto">
                                                            <asp:Button ID="BTNTabla" class="btn text-light bg-secondary mb-2 btn-lg" runat="server" Text="Actualizar datos" OnClick="BTNTabla_Click"/>
                                                            <br />
                                                              <%--aqui va--%> 
                                                             <asp:GridView ID="GVProfesor" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" PageSize="5" ShowFooter="True" OnSelectedIndexChanged="GVProfesor_SelectedIndexChanged">
                                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                                <Columns>
                                                                    <asp:CommandField ButtonType="Button" SelectText="Más información" ShowSelectButton="True" />
                                                                </Columns>
                                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                                                <SelectedRowStyle BackColor="#6666FF" Font-Bold="True" ForeColor="White" />
                                                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                                            </asp:GridView>   
                                                          </div>
                                                     </div>

                                                    <br />

   
                                                </div>
                                               


                                          </div>

                                                                                    

                                      </div>




                                      <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab" tabindex="0">
                                          <br />

                                          <div class="text-center"></div>
                                          <div class="shadow-lg p-3 mb-5 bg-body rounded">
                                                  
                                                <label for="TB1" class="form-label fs-3 fw-semibold">Buscar profesor</label>
                                                <div class="form-floating mb-3">
                                                    <div class="input-group">
                                                      <span class="input-group-text">Nombre y apellido</span>
                                                        <asp:TextBox ID="TBNom" runat="server" type="text" aria-label="Nombre" class="form-control"></asp:TextBox>
                                                        <asp:TextBox ID="TBAp" runat="server" type="text" aria-label="Apellido" class="form-control"></asp:TextBox>
                                                        <asp:Button ID="BTNS" runat="server" Text="Buscar" type="button" class="btn btn-primary btn-lg" OnClick="BTNS_Click" />
                                                    </div>
                                                    
                                                </div>
                                              <div class="text-center">

                                                                    <asp:Label ID="LBR" class="form-floating mb-3 fs-2 fw-semibold" runat="server" Text="Resultados de busqueda"></asp:Label>    
                                                                    <asp:DropDownList ID="DDLProfs" class="form-select" runat="server" OnSelectedIndexChanged="DDLProfs_SelectedIndexChanged"></asp:DropDownList>

                                             </div>
                                                    <hr />

                                                      <label for="" class="form-label fs-3 fw-semibold">Actualizar información</label>
                                                      <div class="mb-3">

                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="TB12" runat="server" type="number" class="form-control" TextMode="Number"></asp:TextBox>
                                                                    <label for="TB12">Registro de Empleado</label>
                                                                </div>
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="TB22" runat="server" type="text" class="form-control"></asp:TextBox>
                                                                    <label for="TB22">Nombre</label>
                                                                </div>
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="TB32" runat="server" type="text" class="form-control"></asp:TextBox>
                                                                    <label for="TB32">Apellido Paterno</label>
                                                                </div>
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="TB42" runat="server" type="text" class="form-control"></asp:TextBox>
                                                                    <label for="TB42">Apellido Materno</label>
                                                                </div>
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="DDL2" class="form-select" runat="server">
                                                                        <asp:ListItem Value="0">Masculino</asp:ListItem>
                                                                        <asp:ListItem Value="1">Femenino</asp:ListItem>
                                                                    </asp:DropDownList>  
                                                                    <label for="DDL2">Genero</label>
                                                                </div>
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="DDLCat2" class="form-select" runat="server">
                                                                        <asp:ListItem Value="0">Profesor por asignatura</asp:ListItem>
                                                                        <asp:ListItem Value="1">Profesor de tiempo Completo</asp:ListItem>
                                                                    </asp:DropDownList>  
                                                                    <label for="DDLCat2">Categoria</label>
                                                                </div>   
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="TB52" runat="server" class="form-control" ></asp:TextBox>
                                                                    <label for="TB52">Correo</label>
                                                                </div> 
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="TB62" runat="server" type="number" class="form-control" TextMode="Number"></asp:TextBox>
                                                                    <label for="TB62">Celular</label>
                                                                </div>   
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="DDLEDO2" class="form-select" runat="server">
                                                                    </asp:DropDownList>            
                                                                    <label for="DDLEDO2">Estado Civil</label>
                                                                </div>   

                                                                <div class="d-grid gap-2 col-4 mx-auto">
                                                                    <asp:Button ID="BTNMod" runat="server" type="button" class="btn text-light bg-warning btn-lg" Text="Modificar Profesor" OnClick="BTNMod_Click"/>
                                                                </div>
                        
                                                      </div>

                                                      <hr />

                                                      <label for="TB1" class="form-label fs-3 fw-semibold">Eliminar profesor</label>
                                                        <div class="form-floating mb-3">
                                                            <div class="text-center">
                                                                <asp:Button ID="BTNDel" runat="server" type="button" class="btn btn-danger btn-lg" Text="Eliminar Profesor" OnClick="BTNDel_Click" />
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
           


        </div>






<%--            <div class="form-group">
                <br />
                <br />
                <div class="row-cols-2">
                    <div></div>
                    

                    <div></div>

                </div>
                
            </div>--%>


            

                

                
  

         <asp:ScriptManager ID="smrTemplate" ScriptMode="Release" AsyncPostBackTimeout="360000" EnablePageMethods="true" runat="server"> 
    
                        <Scripts>
                            <asp:ScriptReference Path="~/js/jquery-3.4.1.min.js" />
                            <asp:ScriptReference Path="~/Scripts/bootstrap.min.js" />

                        </Scripts>

         </asp:ScriptManager>
        
    </form>
</body>
</html>
