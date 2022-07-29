using ClassAccesoDatos;
using ClassEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ClassLogicaNegocios
{
    public class LogicaNegociosProfesor
    {
        private AccesoDatos AccesoDatosSql = null;
        public LogicaNegociosProfesor(string Cad)
        {
            this.AccesoDatosSql = new AccesoDatos(Cad);
        }

        /*Métodos para profesor*/
        public bool AgregarProfesor(Profesor profesor)
        {
            string querySql = "INSERT INTO Profesor (RegistroEmpleado,Nombre,Ap_pat,Ap_Mat,Genero,Categoria,Correo,Celular,F_EdoCivil)" +
                "VALUES (@RegEm,@Nom,@APP,@APM,@Gen,@Cat,@mail,@Cel,@Fed)";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("RegEm",profesor.RegistroEmpleado),
                new SqlParameter("Nom",profesor.Nombre),
                new SqlParameter("APP",profesor.ap_pat),
                new SqlParameter("APM",profesor.ap_mat),
                new SqlParameter("Gen",profesor.Genero),
                new SqlParameter("Cat",profesor.Categoria),
                new SqlParameter("mail",profesor.Correo),
                new SqlParameter("Cel",profesor.Celular),
                new SqlParameter("Fed",profesor.F_EdoCivil)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }
        public bool ActualizarProfesor(Profesor profesor)
        {
            string querySql = "UPDATE Profesor SET RegistroEmpleado=@RegEm,Nombre=@Nom,ap_pat=@APP,ap_mat=@APM,Genero=@Gen,Categoria=@Cat," +
                "Correo=@mail,Celular=@Cel,F_EdoCivil=@Fed WHERE ID_Profe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",profesor.ID_Profe),
                new SqlParameter("RegEm",profesor.RegistroEmpleado),
                new SqlParameter("Nom",profesor.Nombre),
                new SqlParameter("APP",profesor.ap_pat),
                new SqlParameter("APM",profesor.ap_mat),
                new SqlParameter("Gen",profesor.Genero),
                new SqlParameter("Cat",profesor.Categoria),
                new SqlParameter("mail",profesor.Correo),
                new SqlParameter("Cel",profesor.Celular),
                new SqlParameter("Fed",profesor.F_EdoCivil)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public List<Profesor> BuscarProfesorPorNombres(Profesor profesorSearch)
        {
            List<Profesor> profesors = null;
            string querySql = "SELECT * FROM PROFESOR where (Nombre like '%'+@nom+'%' OR Ap_pat like @app+'%' or Ap_Mat like @app+'%')";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("nom",profesorSearch.Nombre),
                new SqlParameter("app",profesorSearch.ap_pat)
            };
            SqlDataReader reader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);
            if (reader != null && reader.HasRows)
            {
                profesors = new List<Profesor>();
                while(reader.Read())
                {
                    string Correo = "";
                    string Celular = "";
                    _= string.IsNullOrEmpty(((object)reader[7]).ToString()) ? Correo= "" : Correo = (string)reader[7];
                    _= string.IsNullOrEmpty(((object)reader[8]).ToString()) ? Celular = "" : Celular = (string)reader[8];
                    profesors.Add(new Profesor()
                    {
                        ID_Profe = (int)reader[0],
                        RegistroEmpleado = (int)reader[1],
                        Nombre = (string)reader[2],
                        ap_pat = (string)reader[3],
                        ap_mat = (string)reader[4],
                        Genero = (string)reader[5],
                        Correo=Correo,
                        Celular=Celular,
                        Categoria = (string)reader[6],
                        F_EdoCivil = (byte)reader[9],
                    });
                }
                
            }
            this.AccesoDatosSql.CerrarConexion();
            return profesors;
        }

        public Profesor BuscarProfesor(int IdProfesor)
        {
            Profesor profesor = null;
            string querySql = "SELECT * FROM PROFESOR WHERE ID_Profe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",IdProfesor)
            };
            SqlDataReader reader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);
            if (reader != null && reader.HasRows)
            {
                profesor = new Profesor();
                profesor.ID_Profe = (int)reader[0];
                profesor.RegistroEmpleado = (int)reader[1];
                profesor.Nombre = (string)reader[2];
                profesor.ap_pat = (string)reader[3];
                profesor.ap_mat = (string)reader[4];
                profesor.Genero = (string)reader[5];
                profesor.Categoria = (string)reader[6];
                profesor.Correo = (string)reader[7];
                profesor.Celular = (string)reader[8];
                profesor.F_EdoCivil = (byte)reader[9];
            }
            this.AccesoDatosSql.CerrarConexion();
            return profesor;
        }

        public bool EliminarProfesor(int IdProfesor)
        {
            string querySql = "DELETE FROM PROFESOR WHERE ID_Profe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",IdProfesor)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public List<Profesor> MostrarProfesores()
        {
            List<Profesor> list = null;
            string querySql = "SELECT * FROM PROFESOR";
            SqlParameter[] sqlParameters = null;
            SqlDataReader sqlDataReader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);
            if (sqlDataReader != null && sqlDataReader.HasRows)
            {
                list = new List<Profesor>();
                while (sqlDataReader.Read())
                {
                    string Correo = "";
                    string Celular = "";
                    _ = string.IsNullOrEmpty(((object)sqlDataReader[7]).ToString()) ? Correo = "" : Correo = (string)sqlDataReader[7];
                    _ = string.IsNullOrEmpty(((object)sqlDataReader[8]).ToString()) ? Celular = "" : Celular = (string)sqlDataReader[8];
                    list.Add(new Profesor()
                    {
                        ID_Profe = sqlDataReader.GetInt32(0),
                        RegistroEmpleado = sqlDataReader.GetInt32(1),
                        Nombre = sqlDataReader.GetString(2),
                        ap_pat = sqlDataReader.GetString(3),
                        ap_mat = sqlDataReader.GetString(4),
                        Genero = sqlDataReader.GetString(5),
                        Correo = Correo,
                        Celular=Celular,
                        Categoria = sqlDataReader.GetString(6),
                        F_EdoCivil = sqlDataReader.GetByte(9)
                    });
                }
            }
            this.AccesoDatosSql.CerrarConexion();
            return list;

        }

        public DataSet MostrarProfesoresPocaInfo(ref string msg)
        {
            string querySql = "SELECT Nombre,Ap_pat + ' ' +Ap_Mat as Apellido, Genero,Categoria FROM PROFESOR";
            SqlParameter[] sqlParameters = null;
            return this.AccesoDatosSql.ConsultaDS(querySql, sqlParameters, ref msg);
        }


        /*Métodos para ProfeGRupo*/

        public bool AgregarProfeGrupo(ProfeGrupo profeGrupo)
        {
            string querySql = "INSERT INTO ProfeGRupo (F_Profe,F_GruCuat,Extra,Extra2) VALUES (@FProf,@FGrup,@Ex,Exx)";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("FProf",profeGrupo.F_Profe),
                new SqlParameter("Ex",profeGrupo.Extra),
                new SqlParameter("Exx",profeGrupo.Extra_dos),
                new SqlParameter("FGrup",profeGrupo.F_GrupoCuatrimestre)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public bool ModificarProfeGrupo(ProfeGrupo profeGrupo)
        {
            string querySql = "UPDATE ProfeGRupo SET F_Profe=@FProf,F_GruCuat=@FGrup,Extra=@Ex,Extra2=@Exx WHERE ID_ProfeGru=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",profeGrupo.Id_ProfeGrupo),
                new SqlParameter("FProf",profeGrupo.F_Profe),
                new SqlParameter("Ex",profeGrupo.Extra),
                new SqlParameter("Exx",profeGrupo.Extra_dos),
                new SqlParameter("FGrup",profeGrupo.F_GrupoCuatrimestre)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public bool EliminarProfeGrupo(int idProfeGrupo)
        {
            string querySql = "DELETE FROM ProfeGRupo WHERE ID_ProfeGru=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",idProfeGrupo)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public List<FiltroGrupoProfesor> BuscarGruposdeProfesor(int IdProfesor)
        {
            List<FiltroGrupoProfesor> list = null;
            string querySql = "SELECT PG.ID_ProfeGru,P.Nombre +' '+ P.Ap_pat as Profesor, " +
                "GC.Turno,GC.Modalidad,G.Grado,G.Letra,G.Id_grupo,C.Inicio,C.Fin,C.Periodo, " +
                "C.Anio  FROM ProfeGRupo PG INNER JOIN Profesor P ON P.ID_Profe=PG.F_Profe " +
                "INNER JOIN GrupoCuatrimestre GC ON GC.Id_GruCuat=PG.F_GruCuat " +
                "INNER JOIN Grupo G ON G.Id_grupo=GC.F_Grupo " +
                "INNER JOIN Cuatrimestre C ON C.id_Cuatrimestre=GC.F_Cuatri " +
                "WHERE P.ID_Profe=@id AND (C.Inicio<GETDATE() AND C.Fin>GETDATE())";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",IdProfesor)
            };
            SqlDataReader reader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);
            if (reader != null && reader.HasRows)
            {
                list = new List<FiltroGrupoProfesor>();
                while (reader.Read())
                {
                    list.Add(new FiltroGrupoProfesor()
                    {
                        ID_ProfeGru=reader.GetInt32(0),
                        Profesor=reader.GetString(1),
                        Turno=reader.GetString(2),
                        Modalidad=reader.GetString(3),
                        Grado=reader.GetString(4),
                        Letra=reader.GetString(5),
                        Id_Grupo=reader.GetInt32(6),
                        Inicio=reader.GetDateTime(7),
                        Fin=reader.GetDateTime(8),
                        Periodo=reader.GetString(9),
                        Anio=reader.GetString(10)
                    });
                }
            }
            this.AccesoDatosSql.CerrarConexion();
            return list;
        }
        //Mostrar con Joins



        /*Métodos para PositivoProfe*/

        public bool AgregarCasoPositivo(PositivoProfe positivoProfe)
        {
            string querySql = "INSERT INTO PositivoProfe (FechaConfirmado,Comprobacion,Antecedentes,NumContagio,Extra,F_Profe,Reisgo) " +
                "VALUES (@Fech,@Comp,@Ante,@NumC,@Ext,@FProf,@Riesgo)";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("Fech",positivoProfe.FechaConfirmado),
                new SqlParameter("Comp",positivoProfe.Comprobacion),
                new SqlParameter("Ante",positivoProfe.Antecedentes),
                new SqlParameter("NumC",positivoProfe.NumContagio),
                new SqlParameter("Ext",positivoProfe.Extra),
                new SqlParameter("FProf",positivoProfe.F_Profe),
                new SqlParameter("Riesgo",positivoProfe.Riesgo)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public bool ModificarCasoPositivo(PositivoProfe positivo)
        {
            string querySql = "UPDATE PositivoProfe SET FechaConfirmado=@Fech,Comprobacion=@Comp,Antecedentes=@Ante," +
                "NumContagio=@NumC,Extra=@Ext,F_Profe=@FProf,Reisgo=@Riesgo WHERE Id_posProfe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",positivo.Id_posProfe),
                new SqlParameter("Fech",positivo.FechaConfirmado),
                new SqlParameter("Comp",positivo.Comprobacion),
                new SqlParameter("Ante",positivo.Antecedentes),
                new SqlParameter("NumC",positivo.NumContagio),
                new SqlParameter("Ext",positivo.Extra),
                new SqlParameter("FProf",positivo.F_Profe),
                new SqlParameter("Riesgo",positivo.Riesgo)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public bool EliminarCasoPositivo(int idPositivo)
        {
            string querySql = "DELETE FROM SeguimientoPRO WHERE F_positivoProfe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",idPositivo)
            };
            this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
            querySql = "DELETE FROM Incapacidad WHERE id_posProfe=@id";
            SqlParameter[] sqlParameters1 = new SqlParameter[]
 {
                new SqlParameter("id",idPositivo)
 };

            this.AccesoDatosSql.Modificar(querySql, sqlParameters1, ref querySql);
            querySql = "DELETE FROM PositivoProfe WHERE ID_ProfeGru=@id";
            SqlParameter[] sqlParameters2 = new SqlParameter[]
 {
                new SqlParameter("id",idPositivo)
 };

            bool result = this.AccesoDatosSql.Modificar(querySql, sqlParameters2, ref querySql);

            this.AccesoDatosSql.CerrarConexion();

            return result;


        }

        public DataSet DevolverCasosPositivosCovid()
        {
            string msg="";
            string querySql = "SELECT PP.Id_posProfe as Num_Registro, PP.FechaConfirmado as Fecha_confirmacion,PP.Antecedentes,PP.NumContagio,PP.Extra,PP.F_Profe as Registro_Profesor,PP.Reisgo as Nivel_Riesgo, (P.Nombre+' '+P.Ap_pat) AS Profesor, P.Genero,P.RegistroEmpleado FROM PositivoProfe PP INNER JOIN Profesor P ON P.ID_Profe=PP.F_Profe ORDER BY Id_posProfe DESC";
            SqlParameter[] sqlParameters = null;
            return this.AccesoDatosSql.ConsultaDS(querySql, sqlParameters, ref msg);
        }

        public List<string> DevolverRutasdeCasosCovid()
        {
            string msg = "";
            List<string> list=null;
            string querySql = "SELECT Comprobacion FROM PositivoProfe ORDER BY Id_posProfe DESC";
            SqlParameter[] sqlParameters = null;
            SqlDataReader sqlDataReader= this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref msg);
            if(sqlDataReader.HasRows)
            {
                list = new List<string>();
                while(sqlDataReader.Read())
                {
                    list.Add(sqlDataReader.GetString(0));
                }
            }
            this.AccesoDatosSql.CerrarConexion();
            return list;
        }



        /* Métodos para SeguimientoProfesor */

        public DataSet MostrarCasosPositivosConFiltro()
        {
            string querySql = "SELECT PP.Id_posProfe AS Registro_Positivo, P.ID_Profe AS Registro_Profesor, (P.Nombre+' '+P.Ap_pat+' '+P.Ap_Mat) AS Profesor,P.Genero, PP.FechaConfirmado AS Caso_Confirmado,PP.Reisgo AS Nivel_Riesgo,PP.Antecedentes,PP.NumContagio,PP.Extra FROM PositivoProfe PP INNER JOIN Profesor P ON P.ID_Profe=PP.F_Profe";
            SqlParameter[] sqlParameters = null;
            return this.AccesoDatosSql.ConsultaDS(querySql, sqlParameters, ref querySql);
        }


        public bool AgregarSeguimientoCaso(SeguimientoProfesor seguimientoProfesor)
        {
            string querySql = "INSERT INTO SeguimientoPRO (F_positivoProfe,F_medico,Fecha,Form_Comunica,Reporte,Entrevista,Extra) " +
                "VALUES (@PosProf,@Med,@Fech,@FormC,@Report,@Entre,@Ex)";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("PosProf",seguimientoProfesor.F_positivoProfe),
                new SqlParameter("Med",seguimientoProfesor.F_medico),
                new SqlParameter("Fech",seguimientoProfesor.Fecha),
                new SqlParameter("FormC",seguimientoProfesor.Form_Comunica),
                new SqlParameter("Report",seguimientoProfesor.Reporte),
                new SqlParameter("Entre",seguimientoProfesor.Entrevista),
                new SqlParameter("Ex",seguimientoProfesor.Extra)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public bool ModificarSeguimientoCaso(SeguimientoProfesor seguimientoProfesor)
        {
            string querySql = "UPDATE SeguimientoPRO SET F_medico=@Med,Fecha=@Fech," +
                "Form_Comunica=@FormC,Reporte=@Report,Entrevista=@Entre,Extra=@Ex WHERE id_Segui=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",seguimientoProfesor.id_Segui),
                new SqlParameter("Med",seguimientoProfesor.F_medico),
                new SqlParameter("Fech",seguimientoProfesor.Fecha),
                new SqlParameter("FormC",seguimientoProfesor.Form_Comunica),
                new SqlParameter("Report",seguimientoProfesor.Reporte),
                new SqlParameter("Entre",seguimientoProfesor.Entrevista),
                new SqlParameter("Ex",seguimientoProfesor.Extra)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public bool EliminarSeguimientoCaso(int idSeguimiento) // Es necesario pensar los controles para el método
        {
            string querySql = "DELETE FROM SeguimientoPRO WHERE id_Segui=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",idSeguimiento)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public List<SeguimientoProfesor> MostrarSeguimientoDeCaso(int idCasoPositivo)
        {
            List<SeguimientoProfesor> list = null;
            string querySql = "SELECT * FROM SeguimientoPRO WHERE F_positivoProfe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",idCasoPositivo)
            };
            SqlDataReader sqlDataReader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);

            if (sqlDataReader != null && sqlDataReader.HasRows)
            {
                list = new List<SeguimientoProfesor>();
                while (sqlDataReader.Read())
                {
                    list.Add(new SeguimientoProfesor()
                    {
                        id_Segui = sqlDataReader.GetInt32(0),
                        F_positivoProfe = sqlDataReader.GetInt32(1),
                        F_medico = sqlDataReader.GetInt32(2),
                        Fecha = sqlDataReader.GetDateTime(3),
                        Form_Comunica = sqlDataReader.GetString(4),
                        Reporte = sqlDataReader.GetString(5),
                        Entrevista = sqlDataReader.GetString(6),
                        Extra = sqlDataReader.GetString(7)
                    });
                }
            }
            this.AccesoDatosSql.CerrarConexion();
            return list;

        }

        // Mostrar todos los profesores contagiados de un programa educativo en un cuatrimestre 
        //especifico
        public List<FiltroProgramaPeriodo> MostrarContagiadosPorFiltroCuatrimestre(Cuatrimestre cuatrimestre)
        {
            List<FiltroProgramaPeriodo> list = null;
            string querySql = "SELECT Pr.ProgramaEd,C.id_Cuatrimestre,C.Periodo,C.Anio,P.RegistroEmpleado,(P.Nombre +' '+P.Ap_pat +' '+ Ap_mat) as Profesor," +
                "Pos.Id_posProfe,Pos.FechaConfirmado FROM GrupoCuatrimestre GC " +
                "INNER JOIN Cuatrimestre C ON C.id_Cuatrimestre = GC.F_Cuatri " +
                "INNER JOIN ProgramaEducativo Pr ON Pr.Id_pe = GC.F_ProgEd INNER JOIN ProfeGRupo PG ON PG.F_GruCuat = GC.Id_GruCuat " +
                "INNER JOIN Profesor P ON P.ID_Profe = PG.F_Profe INNER JOIN PositivoProfe Pos ON Pos.F_Profe = P.ID_Profe " +
                "WHERE Pos.FechaConfirmado BETWEEN @start AND @end";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("start",cuatrimestre.fechaInicio),
                new SqlParameter("end",cuatrimestre.fechaFin)
            };
            SqlDataReader sqlDataReader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);

            if (sqlDataReader != null && sqlDataReader.HasRows)
            {
                list = new List<FiltroProgramaPeriodo>();
                while (sqlDataReader.Read())
                {
                    list.Add(new FiltroProgramaPeriodo()
                    {
                        ProgramaEd = sqlDataReader.GetString(0),
                        id_Cuatrimestre = sqlDataReader.GetInt32(1),
                        Periodo = sqlDataReader.GetString(2),
                        Anio = sqlDataReader.GetString(3),
                        RegistroEmpleado = sqlDataReader.GetInt32(4),
                        Profesor = sqlDataReader.GetString(5),
                        Id_posProfe = sqlDataReader.GetInt32(6),
                        FechaConfirmado = sqlDataReader.GetDateTime(7)
                    });
                }
            }
            this.AccesoDatosSql.CerrarConexion();
            return list;

        }

        //Mostrar los contagios de un profesor
        public List<PositivoProfe> BuscarCasosPositivoDeProfesor(int idProfe, int RegistroEmpleado)
        {
            List<PositivoProfe> list = null;
            string querySql = "SELECT * FROM PositivoProfe WHERE RegistroEmpleado=@RegistroEmpleado OR F_Profe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("RegistroEmpleado",RegistroEmpleado),
                new SqlParameter("id",idProfe)
            };
            SqlDataReader reader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);
            if (reader != null && reader.HasRows)
            {
                list = new List<PositivoProfe>();
                while (reader.Read())
                {
                    list.Add(new PositivoProfe()
                    {
                        Id_posProfe = reader.GetInt32(0),
                        FechaConfirmado = reader.GetDateTime(1),
                        Comprobacion = reader.GetString(2),
                        Antecedentes = reader.GetString(3),
                        NumContagio = reader.GetInt32(4),
                        Extra = reader.GetString(5),
                        F_Profe = reader.GetInt32(6),
                        Riesgo = reader.GetString(7)
                    });
                }
            }
            this.AccesoDatosSql.CerrarConexion();
            return list;
        }

        //Periodos incapacidad por caso Positivo
        public List<Incapacidad> MostrarPeriodosIncapacidadPorCaso(int idPositivo)
        {
            List<Incapacidad> list = null;
            string querySql = "SELECT * FROM Incapacidad WHERE id_posProfe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",idPositivo)
            };
            SqlDataReader reader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);
            if (reader != null && reader.HasRows)
            {
                list = new List<Incapacidad>();
                while (reader.Read())
                {
                    list.Add(new Incapacidad()
                    {
                        id_Incapacidad = reader.GetInt32(0),
                        Fecha_otorga = reader.GetDateTime(1),
                        Fecha_finalizacion = reader.GetDateTime(2),
                        IncapacidadUrl = reader.GetString(3),
                        id_posProfe = idPositivo
                    });
                }
            }
            this.AccesoDatosSql.CerrarConexion();
            return list;
        }

        //Seguimientos por caso Positivo
        public List<FiltroSeguimientoProfesor> MostrarSeguimientosPorCaso(int idPositivo)
        {
            List<FiltroSeguimientoProfesor> list = null;
            string querySql = "SELECT S.id_Segui,(M.Nombre + ' ' + M.App + ' ' + M.Apm) as Doc, " +
                "M.telefono, M.correo, S.Fecha, S.Form_Comunica,S.Reporte,S.Entrevista,S.Extra " +
                "FROM SeguimientoPRO S " +
                "INNER JOIN Medico M ON M.ID_Dr = S.F_medico " +
                "WHERE S.F_positivoProfe = @id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",idPositivo)
            };
            SqlDataReader reader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);
            if (reader != null && reader.HasRows)
            {
                list = new List<FiltroSeguimientoProfesor>();
                while (reader.Read())
                {
                    //Va a tronar valores nulos
                    list.Add(new FiltroSeguimientoProfesor()
                    {
                        id_Segui = reader.GetInt32(0),
                        Doctor = reader.GetString(1),
                        telefono = reader.GetString(2),
                        correo = reader.GetString(3),
                        FechaSeguimiento = reader.GetDateTime(4),
                        FormComunica = reader.GetString(5),
                        Reporte = reader.GetString(6),
                        Entrevista = reader.GetString(7),
                        Extra = reader.GetString(8)
                    });
                }
            }
            this.AccesoDatosSql.CerrarConexion();
            return list;
        }


        //Lista de estado civil
        public List<EstadoCivil> DevolverEstadoCivil()
        {
            List<EstadoCivil> list = null;
            string querySql = "SELECT * FROM EstadoCivil";
            SqlParameter[] sqlParameters = null;
            SqlDataReader reader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);
            if (reader != null && reader.HasRows)
            {
                list = new List<EstadoCivil>();
                while (reader.Read())
                {
                    list.Add(new EstadoCivil()
                    {
                        Id_Edo=reader.GetByte(0),
                        Estado=reader.GetString(1).ToString()
                    });
                }
            }
            this.AccesoDatosSql.CerrarConexion();
            return list;
        }

        //Metodos para incapacidad
        public bool AgregarIncapacidad(Incapacidad incapacidad)
        {
            string querySql = "INSERT INTO Incapacidad (Fecha_otorga,Fecha_finalizacion,IncapacidadUrl,id_posProfe) VALUES (@FO,@FF,@Url,@id);";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("FO",incapacidad.Fecha_otorga),
                new SqlParameter("FF",incapacidad.Fecha_finalizacion),
                new SqlParameter("Url",incapacidad.IncapacidadUrl),
                new SqlParameter("id",incapacidad.id_posProfe),
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public bool ModificarIncapacidad(Incapacidad incapacidad)
        {
            string querySql = "UPDATE Incapacidad SET Fecha_otorga=@FO,Fecha_finalizacion=@FF," +
                "IncapacidadUrl=@Url WHERE id_Incapacidad=@id;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("FO",incapacidad.Fecha_otorga),
                new SqlParameter("FF",incapacidad.Fecha_finalizacion),
                new SqlParameter("Url",incapacidad.IncapacidadUrl),
                new SqlParameter("id",incapacidad.id_Incapacidad),
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public DataSet MostrarIncapacidadesPorCaso(int idCasoPositivo,List<string> urls)
        {
            string querySql = "SELECT IncapacidadUrl FROM Incapacidad WHERE id_posProfe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",idCasoPositivo)
            };
            SqlDataReader sqlDataReader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);
            if(sqlDataReader.HasRows)
            {
                while(sqlDataReader.Read())
                {
                    urls.Add(sqlDataReader.GetString(0));
                }
            }
            this.AccesoDatosSql.CerrarConexion();
            querySql = "SELECT I.id_Incapacidad Registro_Incapacidad, I.Fecha_otorga,I.Fecha_finalizacion,PP.FechaConfirmado Fecha_Caso_Confirmado,PP.Reisgo Nivel_Riesgo FROM Incapacidad I INNER JOIN PositivoProfe PP ON PP.Id_posProfe = I.id_posProfe WHERE PP.Id_posProfe = @id";
            SqlParameter[] sqlParameters1 = new SqlParameter[]
            {
                new SqlParameter("id",idCasoPositivo)
            };
            return this.AccesoDatosSql.ConsultaDS(querySql, sqlParameters1, ref querySql);
        }

        public bool EliminarIncapacidad(int idIncapacidad)
        {
            string querySql = "DELETE FROM INCAPACIDAD WHERE id_Incapacidad=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",idIncapacidad),
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }






    }
}
