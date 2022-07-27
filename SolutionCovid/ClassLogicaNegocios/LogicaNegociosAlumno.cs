using ClassAccesoDatos;
using ClassEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ClassLogicaNegocios
{
    public class LogicaNegociosAlumno
    {
        private AccesoDatos AccesoDatosSql = null; //objeto acceso a datos
        public LogicaNegociosAlumno(string Cad)
        {
            this.AccesoDatosSql = new AccesoDatos(Cad);
        }

        //----------------------------------------------Alumno-------------------------------------------------

        // regla para consultar datos de todos los alumnos
        public DataSet consultarAlumnos(ref string mensaje)
        {
            string query = "select a.ID_Alumno,a.Matricula,a.Nombre,A.Ap_pat,a.Ap_mat,a.Genero,a.Correo," +
                "a.Celular,ec.Estado from Alumno a inner join EstadoCivil ec on a.F_EdoCivil = ec.Id_Edo;";
            SqlParameter[] sqlParameters = null;
            DataSet result = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (result != null) { return result; }
            return null;
        }

        // regla para insertar un nuevo alumno
        public Boolean insertarAlumno(Alumno alumno, ref string mensaje)
        {
            string queryInsert = "INSERT INTO Alumno(Matricula,Nombre,Ap_pat,Ap_mat,Genero,Correo,Celular,F_EdoCivil)" +
                "VALUES(@matricula,@nom,@app,@apm,@genero,@correo,@cel,@fedocivil);";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("matricula", alumno.matricula),
                new SqlParameter("nom", alumno.nombre),
                new SqlParameter("app", alumno.apPat),
                new SqlParameter("apm", alumno.apMat),
                new SqlParameter("genero", alumno.genero),
                new SqlParameter("correo", alumno.correo),
                new SqlParameter("cel", alumno.celular),
                new SqlParameter("fedocivil", alumno.f_edoCivil)
            };
            return AccesoDatosSql.Modificar(queryInsert, sqlParameters, ref mensaje);
        }

        // regla para obtener datos de un alumno
        public Alumno buscarAlumno(int idAlumno, ref string mensaje)
        {
            Alumno alumno = null;
            string query = "select * Alumno where ID_Alumno=@idAlumno;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idAlumno", idAlumno)
            };
            DataSet dataAlumno = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataAlumno != null)
            {
                alumno = new Alumno();
                foreach (DataRow row in dataAlumno.Tables[0].Rows)
                {
                    if ((object)row[6].ToString() == "")
                        row[6] = "";
                    alumno.id = (int)row[0];
                    alumno.matricula = (string)row[1];
                    alumno.nombre = (string)row[2];
                    alumno.apPat = (string)row[3];
                    alumno.apMat = (string)row[4];
                    alumno.genero = (string)row[5];
                    alumno.correo = (string)row[6];
                    alumno.celular = (string)row[7];
                    alumno.f_edoCivil = Convert.ToByte(row[8]);
                }
            }
            return alumno;
        }

        // regla para editar datos de un alumno
        public Boolean editarAlumno(int idAlumno, Alumno alumno, ref string mensaje)
        {
            string queryUpdate = "UPDATE Alumno SET Matricula=@matricula,Nombre=@nom,Ap_pat=@app,Ap_mat=@apm," +
                "Genero=@genero,Correo=@correo,Celular=@cel,F_EdoCivil=@fedocivil" +
                "WHERE ID_Alumno=@idAlumno;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idAlumno", idAlumno),
                new SqlParameter("matricula", alumno.matricula),
                new SqlParameter("nom", alumno.nombre),
                new SqlParameter("app", alumno.apPat),
                new SqlParameter("apm", alumno.apMat),
                new SqlParameter("genero", alumno.genero),
                new SqlParameter("correo", alumno.correo),
                new SqlParameter("cel", alumno.celular),
                new SqlParameter("fedocivil", alumno.f_edoCivil)
            };
            return AccesoDatosSql.Modificar(queryUpdate, sqlParameters, ref mensaje);
        }

        // regla para eliminar un alumno
        public Boolean eliminarAlumno(int idAlumno, ref string mensaje)
        {
            Boolean result = false;
            SqlParameter[] sqlParametersDep1 = new SqlParameter[]
            {
                new SqlParameter("idAlumno", idAlumno)
            };
            SqlParameter[] sqlParametersDep2 = new SqlParameter[]
            {
                new SqlParameter("idAlumno", idAlumno)
            };
            SqlParameter[] sqlParametersDel = new SqlParameter[]
            {
                new SqlParameter("idAlumno", idAlumno)
            };
            // eliminar AlumnoGrupo
            string queryDeleteAlumnoGrupo = "DELETE FROM AlumnoGrupo WHERE F_Alumn =@idAlumno;";
            AccesoDatosSql.Modificar(queryDeleteAlumnoGrupo, sqlParametersDep1, ref mensaje);
            // eliminar PositivoAlumno
            string queryDeletePositivo = "DELETE FROM PositivoAlumno WHERE F_Alumno=@idAlumno;";
            AccesoDatosSql.Modificar(queryDeletePositivo, sqlParametersDep2, ref mensaje);
            // eliminar Alumno
            string queryDeleteAlumno = "DELETE FROM Alumno WHERE ID_Alumno=@idAlumno;";
            result = AccesoDatosSql.Modificar(queryDeleteAlumno, sqlParametersDel, ref mensaje);
            return result;
        }

        // regla para obtener colección de alumnos en ListItems
        public List<Alumno> obtenerListaAlumnos(ref string mensaje)
        {
            List<Alumno> listAlumnos = null;
            string query = "SELECT * FROM Alumno;";
            SqlParameter[] sqlParameters = null;
            DataSet dataAlumnos = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataAlumnos != null)
            {
                listAlumnos = new List<Alumno>();
                foreach (DataRow row in dataAlumnos.Tables[0].Rows)
                {
                    if ((object)row[6].ToString() == "")
                        row[6] = "";
                    listAlumnos.Add(new Alumno()
                    {
                        id = (int)row[0],
                        matricula = (string)row[1],
                        nombre = (string)row[2],
                        apPat = (string)row[3],
                        apMat = (string)row[4],
                        genero = (string)row[5],
                        correo = (string)row[6],
                        celular = (string)row[7],
                        f_edoCivil = Convert.ToByte(row[8])
                    });
                }
            }
            return listAlumnos;
        }

        // regla para obtener colección de alumnos en ListItems para listbox
        public List<Alumno> obtenerColeccionAlumnos(ref string mensaje)
        {
            List<Alumno> listAlumnos = null;
            string query = "SELECT ID_Alumno,Matricula,Nombre+' '+Ap_pat+' '+Ap_mat as NombreCompleto FROM Alumno;";
            SqlParameter[] sqlParameters = null;
            DataSet dataAlumnos = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataAlumnos != null)
            {
                listAlumnos = new List<Alumno>();
                foreach (DataRow row in dataAlumnos.Tables[0].Rows)
                {
                    listAlumnos.Add(new Alumno()
                    {
                        id = (int)row[0],
                        matricula = (string)row[1],
                        nombre = (string)row[2]
                    });
                }
            }
            return listAlumnos;
        }

        //------------------------------------------AlumnoGrupo------------------------------------------------

        // regla para consultar datos de todos los alumno_grupo
        public DataSet consultarAlumnoGrupo(ref string mensaje)
        {
            string query = "SELECT AG.ID_AlumnGru,A.Matricula AS MatriculaAlumno," +
                "PE.ProgramaEd+' '+cast(G.Grado as varchar) + G.Letra as GrupoCuatrimestre,AG.Extra,AG.Extra2" +
                "FROM AlumnoGrupo AG" +
                "JOIN Alumno A ON AG.F_Alumn = A.ID_Alumno" +
                "JOIN GrupoCuatrimestre GC ON AG.F_GruCuat = GC.Id_GruCuat" +
                "JOIN ProgramaEducativo PE ON GC.F_ProgEd = PE.Id_pe" +
                "JOIN Grupo G ON GC.F_Grupo = G.Id_grupo;";
            SqlParameter[] sqlParameters = null;
            DataSet result = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (result != null) { return result; }
            return null;
        }

        // regla para insertar un nuevo alumno_grupo
        public Boolean insertarAlumnoGrupo(AlumnoGrupo alumnoGrupo, ref string mensaje)
        {
            string queryInsert = "INSERT INTO AlumnoGrupo(F_Alumn,F_GruCuat,Extra,Extra2)" +
                "VALUES(@f_alumno,@f_grucuat,@extra,@extra2);";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("f_alumno", alumnoGrupo.f_alumno),
                new SqlParameter("f_grucuat", alumnoGrupo.f_grupoCuatri),
                new SqlParameter("extra", alumnoGrupo.extra),
                new SqlParameter("extra2", alumnoGrupo.extra2)
            };
            return AccesoDatosSql.Modificar(queryInsert, sqlParameters, ref mensaje);
        }

        // regla para obtener datos de un alumno_grupo
        public AlumnoGrupo buscarAlumnoGrupo(int idAlumnoGrupo, ref string mensaje)
        {
            AlumnoGrupo alumnoGrupo = null;
            string query = "SELECT * from AlumnoGrupo where ID_AlumnGru=@idAlumnoGrupo;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idAlumnoGrupo", idAlumnoGrupo)
            };
            DataSet dataAlumnoGrupo = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataAlumnoGrupo != null)
            {
                alumnoGrupo = new AlumnoGrupo();
                foreach (DataRow row in dataAlumnoGrupo.Tables[0].Rows)
                {
                    if ((object)row[3].ToString() == "")
                        row[3] = "";
                    if ((object)row[4].ToString() == "")
                        row[4] = "";
                    alumnoGrupo.id = (int)row[0];
                    alumnoGrupo.f_alumno = (int)row[1];
                    alumnoGrupo.f_grupoCuatri = (int)row[2];
                    alumnoGrupo.extra = (string)row[3];
                    alumnoGrupo.extra2 = (string)row[4];
                }
            }
            return alumnoGrupo;
        }

        // regla para editar datos de un alumno_grupo
        public Boolean editarAlumnoGrupo(int idAlumnoGrupo, AlumnoGrupo alumnoGrupo, ref string mensaje)
        {
            string queryUpdate = "UPDATE AlumnoGrupo SET F_Alumn=@f_alumno,F_GruCuat=f_grucuat,Extra=@extra," +
                "Extra2=@extra2 WHERE ID_AlumnGru=@idAlumnoGrupo;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idAlumnoGrupo", idAlumnoGrupo),
                new SqlParameter("f_alumno", alumnoGrupo.f_alumno),
                new SqlParameter("f_grucuat", alumnoGrupo.f_grupoCuatri),
                new SqlParameter("extra", alumnoGrupo.extra),
                new SqlParameter("extra2", alumnoGrupo.extra2)
            };
            return AccesoDatosSql.Modificar(queryUpdate, sqlParameters, ref mensaje);
        }

        // regla para eliminar un alumno_grupo
        public Boolean eliminarAlumnoGrupo(int idAlumnoGrupo, ref string mensaje)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idAlumnoGrupo", idAlumnoGrupo)
            };
            string queryDeleteAlumnoGrupo = "DELETE FROM AlumnoGrupo WHERE ID_AlumnGru=@idAlumnoGrupo;";
            return AccesoDatosSql.Modificar(queryDeleteAlumnoGrupo, sqlParameters, ref mensaje);
        }

        // regla para obtener colección de alumno_grupo en ListItems
        public List<AlumnoGrupo> obtenerListaAlumnoGrupo(ref string mensaje)
        {
            List<AlumnoGrupo> listAlumnoGrupo = null;
            string query = "SELECT * FROM AlumnoGrupo;";
            SqlParameter[] sqlParameters = null;
            DataSet dataAlumnosGrupos = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataAlumnosGrupos != null)
            {
                listAlumnoGrupo = new List<AlumnoGrupo>();
                foreach (DataRow row in dataAlumnosGrupos.Tables[0].Rows)
                {
                    if ((object)row[3].ToString() == "")
                        row[3] = "";
                    if ((object)row[4].ToString() == "")
                        row[4] = "";
                    listAlumnoGrupo.Add(new AlumnoGrupo()
                    {
                        id = (int)row[0],
                        f_alumno = (int)row[1],
                        f_grupoCuatri = (int)row[2],
                        extra = (string)row[3],
                        extra2 = (string)row[4]
                    });
                }
            }
            return listAlumnoGrupo;
        }

        // regla para obtener colección de alumno_grupo en ListItems para listbox
        public List<AlumnoGrupo> obtenerColeccionAlumnoGrupo(ref string mensaje)
        {
            List<AlumnoGrupo> listAlumnoGrupo = null;
            string query = "SELECT AG.ID_AlumnGru,A.Matricula AS MatriculaAlumno," +
                "PE.ProgramaEd+' '+cast(G.Grado as varchar) + G.Letra as GrupoCuatrimestre FROM AlumnoGrupo AG" +
                "JOIN Alumno A ON AG.F_Alumn = A.ID_Alumno" +
                "JOIN GrupoCuatrimestre GC ON AG.F_GruCuat = GC.Id_GruCuat" +
                "JOIN ProgramaEducativo PE ON GC.F_ProgEd = PE.Id_pe" +
                "JOIN Grupo G ON GC.F_Grupo = G.Id_grupo;";
            SqlParameter[] sqlParameters = null;
            DataSet dataAlumnosGrupos = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataAlumnosGrupos != null)
            {
                listAlumnoGrupo = new List<AlumnoGrupo>();
                foreach (DataRow row in dataAlumnosGrupos.Tables[0].Rows)
                {
                    listAlumnoGrupo.Add(new AlumnoGrupo()
                    {
                        id = (int)row[0],
                        f_alumno = (int)row[1],
                        f_grupoCuatri = (int)row[2]

                    });
                }
            }
            return listAlumnoGrupo;
        }

        //------------------------------------------PositivoAlumno---------------------------------------------

        // regla para consultar datos de todos los positivo alumno
        public DataSet consultarPositivosAlumno(ref string mensaje)
        {
            string query = "select pa.ID_posAl, pa.FechaConfirmado, pa.Comprobacion, pa.Antecedentes, " +
                "pa.Riesgo, pa.NumContagio, pa.Extra, a.Matricula as MatriculaAlumno from PositivoAlumno pa " +
                "inner join Alumno a on pa.F_Alumno = A.ID_Alumno;";
            SqlParameter[] sqlParameters = null;
            DataSet result = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (result != null) { return result; }
            return null;
        }

        // regla para insertar un nuevo positivo alumno
        public Boolean insertarPositivoAlumno(PositivoAlumno positivoAlumno, ref string mensaje)
        {
            string queryInsert = "INSERT INTO PositivoAlumno(FechaConfirmado,Comprobacion,Antecedentes," +
                "Riesgo,NumContagio,Extra,F_Alumno)VALUES(@fechaConfirm,@comprob,@antec,@riesgo,@numCotagio," +
                "@extra,@f_alumno);";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("fechaConfirm", positivoAlumno.fechaConfirmado),
                new SqlParameter("comprob", positivoAlumno.comprobacion),
                new SqlParameter("antec", positivoAlumno.antecedentes),
                new SqlParameter("riesgo", positivoAlumno.riesgo),
                new SqlParameter("numCotagio", positivoAlumno.numContagio),
                new SqlParameter("extra", positivoAlumno.extra),
                new SqlParameter("f_alumno", positivoAlumno.f_alumno)
            };
            return AccesoDatosSql.Modificar(queryInsert, sqlParameters, ref mensaje);
        }

        // regla para obtener datos de un positivo alumno
        public PositivoAlumno buscarPositivoAlumno(int idPositivoAlumno, ref string mensaje)
        {
            PositivoAlumno positivoAlumno = null;
            string query = "SELECT * FROM PositivoAlumno WHERE ID_posAl=@idPositivoAlumno;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idPositivoAlumno", idPositivoAlumno)
            };
            DataSet dataPositivoAlumno = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataPositivoAlumno != null)
            {
                positivoAlumno = new PositivoAlumno();
                foreach (DataRow row in dataPositivoAlumno.Tables[0].Rows)
                {
                    if ((object)row[3].ToString() == "")
                        row[3] = "";
                    if ((object)row[6].ToString() == "")
                        row[6] = "";
                    positivoAlumno.id = (int)row[0];
                    positivoAlumno.fechaConfirmado = (string)row[1];
                    positivoAlumno.comprobacion = (string)row[2];
                    positivoAlumno.antecedentes = (string)row[3];
                    positivoAlumno.riesgo = (string)row[4];
                    positivoAlumno.numContagio = Convert.ToByte(row[5]);
                    positivoAlumno.extra = (string)row[6];
                    positivoAlumno.f_alumno = (int)row[7];
                }
            }
            return positivoAlumno;
        }

        // regla para editar datos de un positivo alumno
        public Boolean editarPositivoAlumno(int idPositivoAlumno, PositivoAlumno positivoAlumno, ref string mensaje)
        {
            string queryUpdate = "UPDATE PositivoAlumno SET FechaConfirmado=@fechaConfirm,Comprobacion=@comprob," +
                "Antecedentes=@antec,Riesgo=@riesgo,NumContagio=@numCotagio,Extra=@extra,F_Alumno=@f_alumno" +
                " WHERE ID_posAl=@idPositivoAlumno;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idPositivoAlumno", idPositivoAlumno),
                new SqlParameter("fechaConfirm", positivoAlumno.fechaConfirmado),
                new SqlParameter("comprob", positivoAlumno.comprobacion),
                new SqlParameter("antec", positivoAlumno.antecedentes),
                new SqlParameter("riesgo", positivoAlumno.riesgo),
                new SqlParameter("numCotagio", positivoAlumno.numContagio),
                new SqlParameter("extra", positivoAlumno.extra),
                new SqlParameter("f_alumno", positivoAlumno.f_alumno)
            };
            return AccesoDatosSql.Modificar(queryUpdate, sqlParameters, ref mensaje);
        }

        // regla para eliminar un positivo alumno
        public Boolean eliminarPositivoAlumno(int idPositivoAlumno, ref string mensaje)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idPositivoAlumno", idPositivoAlumno)
            };
            string queryDeletePositivo = "DELETE FROM PositivoAlumno WHERE ID_posAl=@idPositivoAlumno;";
            return AccesoDatosSql.Modificar(queryDeletePositivo, sqlParameters, ref mensaje);
        }

        // regla para obtener colección de positivo alumno en ListItems
        public List<PositivoAlumno> obtenerListaPositivoAlumno(ref string mensaje)
        {
            List<PositivoAlumno> listPositivoAlumno = null;
            string query = "SELECT * FROM PositivoAlumno;";
            SqlParameter[] sqlParameters = null;
            DataSet dataPostivoAlumno = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataPostivoAlumno != null)
            {
                listPositivoAlumno = new List<PositivoAlumno>();
                foreach (DataRow row in dataPostivoAlumno.Tables[0].Rows)
                {
                    if ((object)row[3].ToString() == "")
                        row[3] = "";
                    if ((object)row[6].ToString() == "")
                        row[6] = "";
                    listPositivoAlumno.Add(new PositivoAlumno()
                    {
                        id = (int)row[0],
                        fechaConfirmado = (string)row[1],
                        comprobacion = (string)row[2],
                        antecedentes = (string)row[3],
                        riesgo = (string)row[4],
                        numContagio = Convert.ToByte(row[5]),
                        extra = (string)row[6],
                        f_alumno = (int)row[7]
                    });
                }
            }
            return listPositivoAlumno;
        }

        // regla para obtener colección de positivo alumno en ListItems para listbox
        public List<PositivoAlumno> obtenerColeccionPositivoAlumno(ref string mensaje)
        {
            List<PositivoAlumno> listPositivoAlumno = null;
            string query = "SELECT PA.ID_posAl,PA.FechaConfirmado, A.Matricula FROM PositivoAlumno PA " +
                "JOIN Alumno A ON PA.F_Alumno = A.ID_Alumno;";
            SqlParameter[] sqlParameters = null;
            DataSet dataPostivoAlumno = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataPostivoAlumno != null)
            {
                listPositivoAlumno = new List<PositivoAlumno>();
                foreach (DataRow row in dataPostivoAlumno.Tables[0].Rows)
                {
                    listPositivoAlumno.Add(new PositivoAlumno()
                    {
                        id = (int)row[0],
                        fechaConfirmado = (string)row[1],
                        f_alumno = (int)row[2]
                    });
                }
            }
            return listPositivoAlumno;
        }

        //---------------------------------------SeguimientoAlumno----------------------------------------------

        // regla para consultar datos de todos los seguimiento_alumno
        public DataSet consultarSeguimientoAlumno(ref string mensaje)
        {
            string query = "select a.Matricula, a.Nombre+' '+a.Ap_pat+' '+a.Ap_mat as Alumno, " +
                "pe.ProgramaEd as ProgramaEducativo,c.Periodo + ' ' + cast(c.Anio as varchar) " +
                "as Cuatrimestre, cast(g.Grado as varchar) + g.Letra as Grupo, pa.FechaConfirmado, " +
                "pa.NumContagio,sa.Fecha as FechaSeguimiento, m.Nombre + ' ' + m.App + ' ' + m.Apm as Medico," +
                "sa.Form_Comunica as FormaComunicacion, sa.Reporte, sa.Entrevista, sa.Extra from SeguimientoAL sa " +
                "inner join PositivoAlumno pa on sa.F_PositivoAL = pa.ID_posAl " +
                "inner join Alumno a on pa.F_Alumno = a.ID_Alumno " +
                "inner join AlumnoGrupo ag on ag.F_Alumn = a.ID_Alumno " +
                "inner join GrupoCuatrimestre gc on ag.F_GruCuat = gc.Id_GruCuat " +
                "inner join ProgramaEducativo pe on gc.F_ProgEd = pe.Id_pe " +
                "inner join Cuatrimestre c on gc.F_Cuatri = c.id_Cuatrimestre " +
                "inner join Grupo g on gc.F_Grupo = g.Id_grupo " +
                "inner join Medico m on sa.F_medico = M.ID_Dr;";
            SqlParameter[] sqlParameters = null;
            DataSet result = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (result != null) { return result; }
            return null;
        }

        // regla para insertar un nuevo seguimiento_alumno
        public Boolean insertarSeguimientoAlumno(SeguimientoAlumno seguimientoAlumno, ref string mensaje)
        {
            string queryInsert = "INSERT INTO SeguimientoAL(F_PositivoAL,F_medico,Fecha,Form_Comunica,Reporte," +
                "Entrevista,Extra)VALUES(@f_positAlumno,@f_medico,@fecha,@formComunc,@reporte," +
                "@entrevista,@extra);";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("f_positAlumno", seguimientoAlumno.f_positivoAlum),
                new SqlParameter("f_medico", seguimientoAlumno.f_medico),
                new SqlParameter("fecha", seguimientoAlumno.fecha),
                new SqlParameter("formComunc", seguimientoAlumno.formaComunicacion),
                new SqlParameter("reporte", seguimientoAlumno.reporte),
                new SqlParameter("entrevista", seguimientoAlumno.entrevista),
                new SqlParameter("extra",seguimientoAlumno.extra)
            };
            return AccesoDatosSql.Modificar(queryInsert, sqlParameters, ref mensaje);
        }

        // regla para obtener datos de un seguimiento_alumno
        public SeguimientoAlumno buscarSeguimientoAlumno(int idSeguimientoAlumno, ref string mensaje)
        {
            SeguimientoAlumno seguimientoAlumno = null;
            string query = "SELECT * FROM SeguimientoAL WHERE Id_Seguimiento=@idSeguimientoAlumno;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idSeguimientoAlumno", idSeguimientoAlumno)
            };
            DataSet dataSeguimientoAlumno = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataSeguimientoAlumno != null)
            {
                seguimientoAlumno = new SeguimientoAlumno();
                foreach (DataRow row in dataSeguimientoAlumno.Tables[0].Rows)
                {
                    if ((object)row[6].ToString() == "")
                        row[6] = "";
                    if ((object)row[7].ToString() == "")
                        row[7] = "";
                    seguimientoAlumno.id = (int)row[0];
                    seguimientoAlumno.f_positivoAlum = (int)row[1];
                    seguimientoAlumno.f_medico = (int)row[2];
                    seguimientoAlumno.fecha = (string)row[3];
                    seguimientoAlumno.formaComunicacion = (string)row[4];
                    seguimientoAlumno.reporte = (string)row[5];
                    seguimientoAlumno.entrevista = (string)row[6];
                    seguimientoAlumno.extra = (string)row[7];
                }
            }
            return seguimientoAlumno;
        }

        // regla para editar datos de un seguimiento_alumno
        public Boolean editarSeguimientoAlumno(int idSeguimientoAlumno, SeguimientoAlumno seguimientoAlumno, ref string mensaje)
        {
            string queryUpdate = "UPDATE SeguimientoAL SET F_PositivoAL=@f_positAlumno,F_medico=@f_medico," +
                "Fecha=@fecha,Form_Comunica=@formComunc,Reporte=@reporte,Entrevista=@entrevista,Extra=@extra " +
                "WHERE Id_Seguimiento =@idSeguimientoAlumno;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idSeguimientoAlumno", idSeguimientoAlumno),
                new SqlParameter("f_positAlumno", seguimientoAlumno.f_positivoAlum),
                new SqlParameter("f_medico", seguimientoAlumno.f_medico),
                new SqlParameter("fecha", seguimientoAlumno.fecha),
                new SqlParameter("formComunc", seguimientoAlumno.formaComunicacion),
                new SqlParameter("reporte", seguimientoAlumno.reporte),
                new SqlParameter("entrevista", seguimientoAlumno.entrevista),
                new SqlParameter("extra", seguimientoAlumno.extra)
            };
            return AccesoDatosSql.Modificar(queryUpdate, sqlParameters, ref mensaje);
        }

        // regla para eliminar un seguimiento_alumno
        public Boolean eliminarSeguimientoAlumno(int idSeguimientoAlumno, ref string mensaje)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idSeguimientoAlumno", idSeguimientoAlumno)
            };
            string queryDeleteSeguimiento = "DELETE FROM SeguimientoAL WHERE Id_Seguimiento=@idSeguimientoAlumno;";
            return AccesoDatosSql.Modificar(queryDeleteSeguimiento, sqlParameters, ref mensaje);
        }

        // regla para obtener colección de seguimiento_alumno en ListItems
        public List<SeguimientoAlumno> obtenerListaSeguimientoAlumno(ref string mensaje)
        {
            List<SeguimientoAlumno> listSeguimientoAlumno = null;
            string query = "SELECT * FROM SeguimientoAL;";
            SqlParameter[] sqlParameters = null;
            DataSet dataSeguimientoAlumno = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataSeguimientoAlumno != null)
            {
                listSeguimientoAlumno = new List<SeguimientoAlumno>();
                foreach (DataRow row in dataSeguimientoAlumno.Tables[0].Rows)
                {
                    if ((object)row[7].ToString() == "")
                        row[7] = "";
                    if ((object)row[6].ToString() == "")
                        row[6] = "";
                    listSeguimientoAlumno.Add(new SeguimientoAlumno()
                    {
                        id = (int)row[0],
                        f_positivoAlum = (int)row[1],
                        f_medico = (int)row[2],
                        fecha = (string)row[3],
                        formaComunicacion = (string)row[4],
                        reporte = (string)row[5],
                        entrevista = (string)row[6],
                        extra = (string)row[7]
                    });
                }
            }
            return listSeguimientoAlumno;
        }

        // regla para obtener colección de seguimiento_alumno en ListItems para listbox
        public List<SeguimientoAlumno> obtenerColeccionSeguimientoAlumno(ref string mensaje)
        {
            List<SeguimientoAlumno> listSeguimientoAlumno = null;
            string query = "SELECT SA.Id_Seguimiento,A.Matricula AS MatriculaAlumno," +
                "M.Nombre+' '+M.App+' '+M.Apm AS Medico,SA.Fecha FROM SeguimientoAL SA" +
                "JOIN PositivoAlumno PA ON SA.F_PositivoAL = PA.ID_posAl" +
                "JOIN Medico M ON SA.F_medico = M.ID_Dr" +
                "JOIN Alumno A ON PA.F_Alumno = A.ID_Alumno;";
            SqlParameter[] sqlParameters = null;
            DataSet dataSeguimientoAlumno = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataSeguimientoAlumno != null)
            {
                listSeguimientoAlumno = new List<SeguimientoAlumno>();
                foreach (DataRow row in dataSeguimientoAlumno.Tables[0].Rows)
                {
                    listSeguimientoAlumno.Add(new SeguimientoAlumno()
                    {
                        id = (int)row[0],
                        f_positivoAlum = (int)row[1],
                        f_medico = (int)row[2],
                        fecha = (string)row[3]
                    });
                }
            }
            return listSeguimientoAlumno;
        }

        // regla para mostrar a todos los alumnos contagiados de
        // un programa educativo, en un cuatrimestre específico
        public DataSet mostrarAlumnosContagiadosFiltroProgEdCuatri(int idProgramaEducativo, int idCuatrimestre, ref string mensaje)
        {
            string query = "select a.Matricula, a.Nombre+' '+a.Ap_pat+' '+a.Ap_mat as Alumno, pe.ProgramaEd as " +
                "ProgramaEducativo, c.Periodo + ' ' + cast(c.Anio as varchar) as Cuatrimestre, " +
                "pa.FechaConfirmado, pa.NumContagio from PositivoAlumno pa" +
                "inner join Alumno a on pa.F_Alumno = a.ID_Alumno " +
                "inner join AlumnoGrupo ag on ag.F_Alumn = a.ID_Alumno" +
                "inner join GrupoCuatrimestre gc on ag.F_GruCuat = gc.Id_GruCuat" +
                "inner join ProgramaEducativo pe on gc.F_ProgEd = pe.Id_pe" +
                "inner join Cuatrimestre c on gc.F_Cuatri = c.id_Cuatrimestre" +
                "where gc.F_ProgEd = @idProgramaEducativo and gc.F_Cuatri = @idCuatrimestre;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idProgramaEducativo", idProgramaEducativo),
                new SqlParameter("idCuatrimestre", idCuatrimestre)
            };
            DataSet result = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (result != null) { return result; }
            return null;
        }

        // regla para mostrar a todos los alumnos contagiados de
        // un programa educativo, en un cuatrimestre específico y de un grupo en partícular
        public DataSet mostrarAlumnosContagiadosFiltroProgEdCuatriGrupo(int idProgramaEducativo, int idCuatrimestre, int idGrupo, ref string mensaje)
        {
            string query = "select a.Matricula, a.Nombre+' '+a.Ap_pat+' '+a.Ap_mat as Alumno, pe.ProgramaEd as ProgramaEducativo, " +
                "c.Periodo + ' ' + cast(c.Anio as varchar) as Cuatrimestre, cast(g.Grado as varchar) + g.Letra as Grupo, " +
                "pa.FechaConfirmado, pa.NumContagio from PositivoAlumno pa" +
                "inner join Alumno a on pa.F_Alumno = a.ID_Alumno" +
                "inner join AlumnoGrupo ag on ag.F_Alumn = a.ID_Alumno" +
                "inner join GrupoCuatrimestre gc on ag.F_GruCuat = gc.Id_GruCuat" +
                "inner join ProgramaEducativo pe on gc.F_ProgEd = pe.Id_pe" +
                "inner join Cuatrimestre c on gc.F_Cuatri = c.id_Cuatrimestre" +
                "inner join Grupo g on gc.F_Grupo = g.Id_grupo" +
                "where gc.F_ProgEd = @idProgramaEducativo and gc.F_Cuatri = @idCuatrimestre and gc.F_Grupo = @idGrupo;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idProgramaEducativo", idProgramaEducativo),
                new SqlParameter("idCuatrimestre", idCuatrimestre),
                new SqlParameter("idGrupo", idGrupo)
            };
            DataSet result = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (result != null) { return result; }
            return null;
        }

        // regla para mostrar el seguimiento de un alumno (por su registro) en un cuatrimestre específico
        public DataSet mostrarSeguimientoAlumno(int matriculaAlumno, int idCuatrimestre, ref string mensaje)
        {
            string query = "select a.Matricula, a.Nombre+' '+a.Ap_pat+' '+a.Ap_mat as Alumno, pe.ProgramaEd as ProgramaEducativo, " +
                "c.Periodo + ' ' + cast(c.Anio as varchar) as Cuatrimestre, " +
                "cast(g.Grado as varchar) + g.Letra as Grupo, pa.FechaConfirmado, pa.NumContagio, " +
                "sa.Fecha as FechaSegumiento, m.Nombre + ' ' + m.App + ' ' + m.Apm as Medico, " +
                "sa.Form_Comunica as FormaComunicacion, sa.Reporte, sa.Entrevista, sa.Extra from SeguimientoAL sa " +
                "inner join PositivoAlumno pa on sa.F_PositivoAL = pa.ID_posAl " +
                "inner join Alumno a on pa.F_Alumno = a.ID_Alumno " +
                "inner join AlumnoGrupo ag on ag.F_Alumn = a.ID_Alumno " +
                "inner join GrupoCuatrimestre gc on ag.F_GruCuat = gc.Id_GruCuat " +
                "inner join ProgramaEducativo pe on gc.F_ProgEd = pe.Id_pe " +
                "inner join Cuatrimestre c on gc.F_Cuatri = c.id_Cuatrimestre " +
                "inner join Grupo g on gc.F_Grupo = g.Id_grupo " +
                "inner join Medico m on sa.F_medico = M.ID_Dr " +
                "where a.Matricula = @matricula and gc.F_Cuatri = @idCuatrimestre;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("matricula", matriculaAlumno),
                new SqlParameter("idCuatrimestre", idCuatrimestre)
            };
            DataSet result = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (result != null) { return result; }
            return null;
        }

        // regla para obtener colección de grupos en ListItems para listbox
        public List<Grupo> obtenerColeccionGrupo(ref string mensaje)
        {
            List<Grupo> listGrupo = null;
            string query = "select * from Grupo;";
            SqlParameter[] sqlParameters = null;
            DataSet dataGrupo = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataGrupo != null)
            {
                listGrupo = new List<Grupo>();
                foreach (DataRow row in dataGrupo.Tables[0].Rows)
                {
                    if ((object)row[3].ToString() == "")
                        row[3] = "";
                    listGrupo.Add(new Grupo()
                    {
                        id = Convert.ToInt16(row[0]),
                        grado = Convert.ToByte(row[1]),
                        letra = (string)row[2],
                        extra = (string)row[3]
                    });
                }
            }
            return listGrupo;
        }
    }
}
