using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CASCO.EN.Abastos;
using System.Collections;
using System.Data;
using CASCO.DAO.Contpaqi.Comercial.SDK_2_0_3;
using System.Globalization;

namespace CASCO.DAO.Abastos
{
    public static class SolicitudMateriales
    {
        public static List<Almacen> GetAlmacenesPorDivisionEmpresaDepto(int division, int empresa, int departamento)
        {
            List<Almacen> la = new List<Almacen>();
            string sqlstr = "[Abastos].[usp_Almacenes_ConsultaPorDivisionEmpresaDepartamento]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@Division_ID", division);
            m_params.Add("@Empresa_ID", empresa);
            m_params.Add("@Departamento_ID", departamento);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                Almacen ar = new Almacen();
                ar.id = Convert.ToInt32(dr["Almacen_ID"]);
                ar.descripcion = dr["Descripcion"].ToString();
                ar.aux = dr["CIDALMACEN"].ToString();
                ar.Orden = Convert.ToInt32(dr["ORDEN"]);
                la.Add(ar);
            }

            return la;
        }

        public static bool ActualizaCatalogos()
        {
            string sql = "[Abastos].[usp_job_Servicios_SincronizaInformacion]";
            Hashtable m_params = new Hashtable();
            object obj = DB.ExecuteCommandSP(sql, m_params);
            return Convert.ToBoolean(obj);
        }

        public static List<SoporteElectronicoSolicitudMateriales> GetSoportes(int division, int empresa, int departamento)
        {
            List<SoporteElectronicoSolicitudMateriales> la = new List<SoporteElectronicoSolicitudMateriales>();
            string sqlstr = "[Abastos].[SolicitudDeMaterialesSoportes_Consulta]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@Division_ID", division);
            m_params.Add("@Empresa_ID", empresa);
            m_params.Add("@Departamento_ID", departamento);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            return ObtieneListaSoportes(dt);
        }

        public static bool AgregaComentarioASolicitud(int idSolicitud, string comentario, string user)
        {
            string sql = "Abastos.SolicitudDeMaterialesComentarios_Inserta";
            Hashtable m_params = new Hashtable();
            m_params.Add("@SolicitudDeMaterial_ID", idSolicitud);
            m_params.Add("@Comentario", comentario);
            m_params.Add("@Usuario_ID", user);
            object obj = DB.ExecuteCommandSP_With_Return(sql, m_params);
            return Convert.ToBoolean(obj);
        }

        public static bool EliminaRegistroDetalle(int idSolicitud, int idx)
        {
            string sql = "[Abastos].[SolicitudDeMaterialesDetalle_Elimina]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@SolicitudDeMaterial_ID", idSolicitud);
            m_params.Add("@SolicitudDeMaterialDetalle_ID", idx);
            object obj = DB.ExecuteCommandSP_With_Return(sql, m_params);
            return Convert.ToBoolean(obj);
            throw new NotImplementedException();
        }

        public static string ObtieneComentarioDeSolicitud(int idSolicitud)
        {
            string sqlstr = "[Abastos].[SolicitudDeMaterialesComentarios_Consulta]";
            string scomentarios = "";
            Hashtable m_params = new Hashtable();
            m_params.Add("@SolicitudDeMaterial_ID", idSolicitud);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                scomentarios += dr["Comentarios"].ToString();
            }
            return scomentarios;
        }

        public static bool InsertaDocumentoSoporte(int solicitud_id, SoporteElectronicoSolicitudMateriales soporte)
        {
            string sqlstr = "[Abastos].[SolicitudDeMaterialesSoportes_Inserta]";
            Hashtable m_params = new Hashtable();
            if (soporte.id > 0)
                m_params.Add("@SolicitudSoporteE_ID", soporte.id);
            m_params.Add("@SolicitudDeMaterial_ID", solicitud_id);
            m_params.Add("@SoporteE_ID", soporte.soporte.id);
            m_params.Add("@Req_Para_Autorizacion", soporte.soporte.Req_Para_Autorizacion);
            m_params.Add("@EstatusSoporteE_ID", soporte.Estatus);
            m_params.Add("@Orden", soporte.soporte.Orden);
            m_params.Add("@RutaDocto", soporte.Ruta_Documento);
            soporte.id = (int) DB.ExecuteCommandSP_With_Return(sqlstr, m_params);
            return soporte.id > 0;
        }

        public static List<ArticuloSolicitudMateriales> GetServiciosPorDivisionEmpresaDeptoAlmacen(int division, int empresa, int departamento, int? almacen)
        {
            List<ArticuloSolicitudMateriales> lar = new List<ArticuloSolicitudMateriales>();
            string sqlstr = "[Abastos].[usp_Servicios_ConsultaPorDivisionEmpresaDepartamentoAlmacen]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@Division_ID", division);
            m_params.Add("@Empresa_ID", empresa);
            m_params.Add("@Departamento_ID", departamento);
            if (almacen != null)
                m_params.Add("@Almacen_ID", almacen);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                ArticuloSolicitudMateriales ar = new ArticuloSolicitudMateriales();
                ar.SERVICIO_ID = Convert.ToInt32(dr["SERVICIO_ID"]);
                ar.id = ar.SERVICIO_ID;
                ar.CIDPRODUCTO = dr["CIDPRODUCTO"].ToString();
                ar.CIDPROVEEDOR = dr["CIDPROVEEDOR"].ToString();
                ar.CODIGOPRODUCTO = dr["CCODIGOPRODUCTO"].ToString();
                ar.CNOMBREPRODUCTO = dr["CNOMBREPRODUCTO"].ToString();
                ar.descripcion = ar.CNOMBREPRODUCTO;
                ar.preciocompra = Convert.ToDouble(dr["CPRECIOCOMPRA"]);
                ar.aux = ar.preciocompra.ToString();
                if (dr["KILOMETRAJE_MILES"] != DBNull.Value)
                    ar.KILOMETRAJE_MILES = Convert.ToInt32(dr["KILOMETRAJE_MILES"]);
                lar.Add(ar);
            }

            return lar;
        }

        public static bool ActualizaEstatus(string usuario_id, int? SolicitudId, int? estatusId)
        {
            string sql = "[Abastos].[SolicitudDeMateriales_ActualizaEstatus]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@SolicitudDeMaterial_ID", SolicitudId);
            m_params.Add("@EstatusSolicitud_ID", estatusId);
            m_params.Add("@Usuario_ID", usuario_id);
            object obj = DB.ExecuteCommandSP_With_Return(sql, m_params);
            return Convert.ToBoolean(obj);
        }

        public static void GeneraOrdenesDeCompraPendientes()
        {

            string fhoy = DateTime.Now.ToShortDateString();

            SolicitudMaterialesConsulta sol = new SolicitudMaterialesConsulta();
            sol.Estatus.id = 6; // 6 = Por generar orden de compra
            sol.FechaDesde = Convert.ToDateTime(fhoy);
            sol.FechaHasta = Convert.ToDateTime(fhoy);
            List<EN.Abastos.SolicitudMateriales> ls = GetSolicitudDeMateriales(sol);
            if (ls != null && ls.Count > 0)
            {
                SDK.Inicializa();
                bool Siguiente = false;
                foreach (EN.Abastos.SolicitudMateriales solicitud in ls)
                {
                    if (SDK.AbreEmpresa(solicitud.BaseDeDatos))
                    {
                        List<EN.Abastos.SolicitudMateriales> ls_aux = new List<EN.Abastos.SolicitudMateriales>();
                        int cidprov = 0;
                        EN.Abastos.SolicitudMateriales saux = null;

                        //Verifica la cantidad de ordenes de compra a generar
                        // por proveedor de acuerdo a los artículos elegidos.
                        foreach (DetalleSolicitudMateriales ds in solicitud.articulos)
                        {
                            if (cidprov != ds.item.CIDPROVEEDOR)
                            {
                                cidprov = ds.item.CIDPROVEEDOR;
                                saux = new EN.Abastos.SolicitudMateriales();
                                saux = (EN.Abastos.SolicitudMateriales) solicitud.Clone();
                                saux.articulos = new List<DetalleSolicitudMateriales>();
                                saux.articulos.Add(ds);
                                ls_aux.Add(saux);
                            }
                            else
                            {
                                saux.articulos.Add(ds);
                            }
                        }

                        //Por cada diferente proveedor en la misma solicitud
                        // se genera una orden de compra en el sistema comercial
                        foreach (EN.Abastos.SolicitudMateriales sol_aux in ls_aux)
                        {
                            Siguiente = false;
                            try
                            {

                                Documento documento = new Documento();
                                string codProveedor = documento.BuscaProveedorPorId(solicitud.CIDProveedor); //339
                                int idmoneda = documento.ObtieneIdMoneda(solicitud.Moneda);
                                double importe = solicitud.total;
                                string sconcepto = documento.ObtieneCodigoConcepto(solicitud.ConceptoDocumento);
                                if (sconcepto.Trim().Length > 0)
                                {
                                    string folio = documento.ObtieneFolioSiguiente(sconcepto);
                                    solicitud.FolioDocumento = folio;
                                    solicitud.SerieDocumento = "";
                                    tDocumento docto = new tDocumento();
                                    docto.aCodConcepto = sconcepto;
                                    docto.aSerie = "";
                                    docto.aFecha = DateTime.Now.ToString("MM/dd/yyyy");
                                    docto.aFolio = Convert.ToDouble(documento.Folio);
                                    docto.aCodigoCteProv = codProveedor;
                                    docto.aNumMoneda = idmoneda;
                                    docto.aImporte = importe;
                                    docto.aDescuentoDoc1 = 0;
                                    docto.aDescuentoDoc2 = 0;
                                    docto.aSistemaOrigen = 0;
                                    docto.aAfecta = 0;
                                    docto.aReferencia = "";

                                    bool bCreado = false;
                                    try
                                    {
                                        bCreado = documento.AltaDeDocumento(docto);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }

                                    if (bCreado)
                                    {
                                        //Inserta información adicional
                                        bCreado = documento.SetDatoDocumento("CTEXTOEXTRA1", new StringBuilder(solicitud.Usuario));
                                    }

                                    if (bCreado)
                                    {
                                        int c = 1;
                                        foreach (DetalleSolicitudMateriales fila in solicitud.articulos)
                                        {
                                            tMovimiento tm = new tMovimiento();
                                            tm.aConsecutivo = c;
                                            tm.aCodProdSer = fila.item.CCODIGOPRODUCTO.ToString();
                                            tm.aCosto = 0;
                                            tm.aPrecio = fila.item.preciocompra;
                                            tm.aReferencia = "";
                                            tm.aCodClasificacion = "";
                                            tm.aCodAlmacen = "1";
                                            tm.aUnidades = fila.cantidad;
                                            bCreado = false;
                                            try
                                            {
                                                Int32 idMov = documento.AltaMovimiento(tm);
                                                if (idMov > 0)
                                                {
                                                    documento.SetDatoMovimiento("CIMPUESTO1", new StringBuilder(((fila.item.preciocompra * fila.cantidad) * 0.16).ToString()));
                                                    documento.SetDatoMovimiento("CPORCENTAJEIMPUESTO1", new StringBuilder("16"));
                                                    fila.CIDMOVIMIENTO = idMov;
                                                    fila.FolioDocumento = folio;
                                                    fila.SerieDocumento = "";
                                                }
                                                else
                                                    throw new Exception(string.Format("Revise el log {0} en el visor de eventos del sistema", EN.General.Utils.EventLog));
                                            }
                                            catch (Exception ex)
                                            {
                                                throw ex;
                                            }
                                            c++;
                                        }
                                    }
                                }
                                else
                                    throw new Exception("No se econtró el codigo del concepto " + solicitud.ConceptoDocumento);

                                InsertaSolicitud(solicitud); //Actualiza información de la solicitud en DB
                                Siguiente = true;
                            }
                            catch (Exception ex)
                            {
                                EN.General.EventLog.WriteError(ex.ToString());
                                Siguiente = false;
                                break;
                            }
                        }

                        if (SDK.EmpresaAbierta)
                            SDK.CerrarEmpresa(solicitud.BaseDeDatos);

                        if (!Siguiente)
                            break;

                    }
                    else
                    {
                        EN.General.EventLog.WriteError("Error al procesar la solicitud con id: " + solicitud.id);
                        EN.General.EventLog.WriteError("Error al abrir Base de Datos " + solicitud.BaseDeDatos);
                    }

                    ActualizaEstatus("SICAS", solicitud.id, 4);
                }
                SDK.TerminaSDK();
            }
        }

        public static Usuario GetInformacionUsuario(string name)
        {
            Usuario u = new Usuario();
            u.descripcion = name;
            string sqlstr = "[Abastos].[usp_Usuario_DivisionesEmpresasDeptos]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@Usuario_ID", u.descripcion);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

            int idx = 0;
            int NewDiv = 0;
            int NewEmp = 0;
            while (idx < dt.Rows.Count)
            {
                Division d = new Division();
                NewDiv = Convert.ToInt32(dt.Rows[idx]["Division_ID"]);
                d.id = NewDiv;
                d.descripcion = dt.Rows[idx]["Division"].ToString();
                u.divisiones.Add(d);

                while (idx < dt.Rows.Count && NewDiv == Convert.ToInt32(dt.Rows[idx]["Division_ID"]))
                {
                    Empresa e = new Empresa();
                    NewEmp = Convert.ToInt32(dt.Rows[idx]["Empresa_ID"]);
                    e.id = NewEmp;
                    e.descripcion = dt.Rows[idx]["Empresa"].ToString();
                    d.empresas.Add(e);

                    while (idx < dt.Rows.Count && NewEmp == Convert.ToInt32(dt.Rows[idx]["Empresa_ID"]))
                    {
                        Departamento depto = new Departamento();
                        depto.id = Convert.ToInt32(dt.Rows[idx]["Departamento_ID"]);
                        depto.descripcion = dt.Rows[idx]["Departamento"].ToString();
                        depto.NivelAcceso = Convert.ToInt32(dt.Rows[idx]["Nivel_ID"]);
                        e.departamentos.Add(depto);
                        idx++;
                    }
                }
            }
            return u;
        }

        public static void InsertaSolicitud(EN.Abastos.SolicitudMateriales solicitud)
        {
            CASCO.DAO.DB.DoTransactions(delegate
            {
                AlmacenaEncabezadoSolicitud(solicitud);
                AlmacenaDetalleSolicitud(solicitud);
                AlmacenaSoportesSolicitud(solicitud);
            });
        }

        private static void AlmacenaSoportesSolicitud(EN.Abastos.SolicitudMateriales solicitud)
        {
            // EliminaSoportesSolicitud(solicitud.id);
            foreach (SoporteElectronicoSolicitudMateriales registro in solicitud.soportes)
            {
                AlmacenaRegistroSoporteSolicitud(registro, solicitud);
            }
        }

        private static void AlmacenaRegistroSoporteSolicitud(SoporteElectronicoSolicitudMateriales registro, EN.Abastos.SolicitudMateriales requisicion)
        {
            string sql = "[Abastos].[SolicitudDeMaterialesSoportes_Inserta]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@SolicitudSoporteE_ID", registro.id);
            m_params.Add("@SolicitudDeMaterial_ID", requisicion.id);
            m_params.Add("@SoporteE_ID", registro.soporte.id);
            m_params.Add("@Req_Para_Autorizacion", registro.soporte.Req_Para_Autorizacion);
            m_params.Add("@EstatusSoporteE_ID", registro.Estatus);
            m_params.Add("@Orden", registro.Orden);
            m_params.Add("@RutaDocto", registro.Ruta_Documento);
            registro.id = (int) DB.ExecuteCommandSP_With_Return(sql, m_params);
        }

        private static void EliminaSoportesSolicitud(int? id)
        {
            string sql = "[Abastos].[SolicitudDeMaterialesSoportes_Elimina]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@SolicitudDeMaterial_ID", id);
            DB.ExecuteCommand(sql, m_params);
        }

        private static void AlmacenaDetalleSolicitud(EN.Abastos.SolicitudMateriales solicitud)
        {
            foreach (DetalleSolicitudMateriales registro in solicitud.articulos)
            {
                AlmacenaRegistroDetalleSolicitud(registro, solicitud);
            }
        }

        private static void EliminaDetalleSolicitud(int? solicitud_id)
        {
            string sql = "[Abastos].[SolicitudDeMaterialesDetalle_Elimina]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@SolicitudDeMaterial_ID", solicitud_id);
            DB.ExecuteCommand(sql, m_params);
        }

        private static void AlmacenaRegistroDetalleSolicitud(DetalleSolicitudMateriales servicio, EN.Abastos.SolicitudMateriales requisicion)
        {
            string sql = "[Abastos].[SolicitudDeMaterialesDetalle_Inserta]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@SolicitudDeMaterialDetalle_ID", servicio.id);
            m_params.Add("@SolicitudDeMaterial_ID", requisicion.id);
            m_params.Add("@Division_ID", requisicion.Division.id);
            m_params.Add("@Empresa_ID", requisicion.Empresa.id);
            m_params.Add("@Departamento_ID", requisicion.Departamento.id);
            m_params.Add("@Servicio_ID", servicio.item.id);
            m_params.Add("@Cantidad", servicio.cantidad);
            m_params.Add("@CPRECIOCOMPRA", servicio.item.aux);
            m_params.Add("@CIDPROVEEDOR", servicio.item.CIDPROVEEDOR);
            m_params.Add("@CIDDOCUMENTO", servicio.CIDDOCUMENTO);
            m_params.Add("@FolioDocumento", servicio.FolioDocumento);
            m_params.Add("@Serie", servicio.SerieDocumento);
            servicio.id = (int) DB.ExecuteCommandSP_With_Return(sql, m_params);
        }

        private static void AlmacenaEncabezadoSolicitud(EN.Abastos.SolicitudMateriales sol)
        {
            string sql = "[Abastos].[SolicitudDeMaterialesEncabezado_Inserta]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@SolicitudDeMaterial_ID", sol.id);
            m_params.Add("@Usuario_ID", sol.Usuario);
            m_params.Add("@Division_ID", sol.Division.id);
            m_params.Add("@Empresa_ID", sol.Empresa.id);
            m_params.Add("@Departamento_ID", sol.Departamento.id);
            m_params.Add("@Almacen_ID", sol.Almacen.id);
            m_params.Add("@EstatusSolicitud_ID", sol.Estatus.id);
            m_params.Add("@Moneda", sol.Moneda);
            sol.id = (int) DB.ExecuteCommandSP_With_Return(sql, m_params);
        }

        public static List<EN.Abastos.SolicitudMateriales> GetSolicitudDeMateriales(SolicitudMaterialesConsulta sol)
        {
            List<EN.Abastos.SolicitudMateriales> lr = new List<EN.Abastos.SolicitudMateriales>();
            string sqlstr = "[Abastos].[SolicitudDeMaterialesEncabezado_Consulta]";
            Hashtable m_params = new Hashtable();
            if (sol.id > 0)
                m_params.Add("@Solicitud_ID", sol.id);
            if (sol.Division.id > 0)
                m_params.Add("@Division_ID", sol.Division.id);
            if (sol.Empresa.id > 0)
                m_params.Add("@Empresa_ID", sol.Empresa.id);
            if (sol.Departamento.id > 0)
                m_params.Add("@Departamento_ID", sol.Departamento.id);
            if (sol.Almacen.id > 0)
                m_params.Add("@Almacen_ID", sol.Almacen.id);
            if (sol.Estatus.id > 0)
                m_params.Add("@EstatusSolicitud_ID", sol.Estatus.id);
            m_params.Add("@FechaDesde", DateTime.ParseExact(sol.FechaDesde.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture));
            m_params.Add("@FechaHasta", DateTime.ParseExact(sol.FechaHasta.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture));

            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                EN.Abastos.SolicitudMateriales r = new EN.Abastos.SolicitudMateriales();
                r.id = Convert.ToInt32(dr["SolicitudDeMaterial_ID"]);
                r.Division.id = Convert.ToInt32(dr["Division_ID"]);
                r.Division.descripcion = dr["Division"].ToString();
                r.Empresa.id = Convert.ToInt32(dr["Empresa_ID"]);
                r.Empresa.descripcion = dr["Empresa"].ToString();
                r.Departamento.id = Convert.ToInt32(dr["Departamento_ID"]);
                r.Departamento.descripcion = dr["Departamento"].ToString();
                r.Almacen.id = Convert.ToInt32(dr["Almacen_ID"]);
                r.Almacen.descripcion = dr["Almacen"].ToString();
                r.Estatus.id = Convert.ToInt32(dr["EstatusSolicitud_ID"]);
                r.Estatus.descripcion = dr["EstatusSolicitud"].ToString();
                r.FechaElabora = Convert.ToDateTime(dr["FechaElaboracion"]).ToString("dd/MM/yyyy HH:mm");
                if (dr["Comentarios"] != DBNull.Value)
                    r.Comentarios = dr["Comentarios"].ToString();
                if (dr["BaseDeDatos"] != DBNull.Value)
                    r.BaseDeDatos = dr["BaseDeDatos"].ToString();
                r.ConceptoDocumento = dr["ConceptoDocumento"].ToString();
                r.Moneda = dr["Moneda"].ToString();
                r.Usuario = dr["Usuario_ID"].ToString();
                SetSolicitudDeMaterialesDetalle(r);
                SetSolicitudDeMaterialesSoportes(r);
                lr.Add(r);
            }
            return lr;
        }

        private static void SetSolicitudDeMaterialesSoportes(EN.Abastos.SolicitudMateriales req)
        {
            List<SoporteElectronicoSolicitudMateriales> lr = new List<SoporteElectronicoSolicitudMateriales>();
            string sqlstr = "[Abastos].[SolicitudDeMaterialesSoportes_Consulta]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@SolicitudDeMaterial_ID", req.id);
            m_params.Add("@Division_ID", req.Division.id);
            m_params.Add("@Empresa_ID", req.Empresa.id);
            m_params.Add("@Departamento_ID", req.Departamento.id);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            req.soportes = ObtieneListaSoportes(dt);
        }

        private static List<SoporteElectronicoSolicitudMateriales> ObtieneListaSoportes(DataTable dt)
        {
            List<SoporteElectronicoSolicitudMateriales> ls = new List<SoporteElectronicoSolicitudMateriales>();
            foreach (DataRow dr in dt.Rows)
            {
                SoporteElectronicoSolicitudMateriales r = new SoporteElectronicoSolicitudMateriales();
                r.id = Convert.ToInt32(dr["SolicitudSoporteE_ID"]);
                r.Orden = Convert.ToInt32(dr["Orden"]);
                r.Solicitud_Id = Convert.ToInt32(dr["SolicitudDeMaterial_ID"]);
                r.soporte.id = Convert.ToInt32(dr["SoporteE_ID"]);
                r.soporte.descripcion = dr["SoporteE_Descripcion"].ToString();
                r.soporte.Orden = Convert.ToInt32(dr["SoporteE_Orden"]);
                r.soporte.Req_Para_Autorizacion = Convert.ToBoolean(dr["SoporteE_Req_Para_Autorizacion"]);
                r.soporte.Estatus = Convert.ToInt32(dr["SoporteE_Estatus"]);
                r.soporte.Tipo.id = Convert.ToInt32(dr["TipoSoporteE_ID"]);
                r.soporte.Tipo.descripcion = dr["TipoSoporte_Descripcion"].ToString();
                r.soporte.Tipo.Abreviacion = dr["TipoSoperteE_Abreviatura"].ToString();
                r.Estatus = Convert.ToInt32(dr["EstatusSoporteE_ID"]);
                r.Ruta_Documento = dr["RutaDocto"].ToString();
                ls.Add(r);
            }
            return ls;
        }
        public static void SetSolicitudDeMaterialesDetalle(EN.Abastos.SolicitudMateriales req)
        {
            List<DetalleSolicitudMateriales> lr = new List<DetalleSolicitudMateriales>();
            string sqlstr = "[Abastos].[SolicitudDeMaterialesDetalle_Consulta]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@SolicitudDeMaterial_ID", req.id);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                DetalleSolicitudMateriales r = new DetalleSolicitudMateriales();
                r.id = Convert.ToInt32(dr["SolicitudDeMaterialDetalle_ID"]);
                r.cantidad = Convert.ToInt32(dr["Cantidad"]);
                r.item.id = Convert.ToInt32(dr["Servicio_ID"]);
                r.item.aux = Convert.ToDouble(dr["CPRECIOCOMPRA"]).ToString("C2");
                r.item.descripcion = dr["CNOMBREPRODUCTO"].ToString();
                r.item.preciocompra = Convert.ToDouble(dr["CPRECIOCOMPRA"]);
                r.item.CIDPRODUCTO = Convert.ToInt32(dr["CIDPRODUCTO"]);
                r.item.CIDPROVEEDOR = Convert.ToInt32(dr["CIDPROVEEDOR"]);
                r.item.CCODIGOPRODUCTO = Convert.ToInt32(dr["CCODIGOPRODUCTO"]);
                if (!Convert.IsDBNull(dr["CIDDOCUMENTO"]))
                    r.CIDDOCUMENTO = Convert.ToInt32(dr["CIDDOCUMENTO"]);
                if (!Convert.IsDBNull(dr["FOLIODOCUMENTO"]))
                    r.FolioDocumento = dr["FOLIODOCUMENTO"].ToString();
                if (!Convert.IsDBNull(dr["SERIEDOCUMENTO"]))
                    r.SerieDocumento = dr["SERIEDOCUMENTO"].ToString();
                lr.Add(r);
            }
            req.articulos = lr;
        }

        public static List<Empresa> ObtieneListaEmpresas()
        {
            List<Empresa> le = new List<Empresa>();
            string sqlstr = "[Abastos].[usp_Empresas_Consulta]";
            Hashtable m_params = new Hashtable();
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Empresa r = new Empresa();
                r.id = Convert.ToInt32(dr["Empresa_ID"]);
                r.descripcion = dr["Empresa"].ToString();
                r.aux = dr["EmpresaDB"].ToString();
                r.ADD = dr["EmpresaADD"].ToString();
                le.Add(r);
            }
            return le;
        }

        public static List<Proveedor> ObtieneListaProveedores(int empresa_id)
        {
            List<Proveedor> le = new List<Proveedor>();
            string sqlstr = "[Abastos].[usp_Proveedores_Consulta_PorEmpresa]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@Empresa_ID", empresa_id);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Proveedor r = new Proveedor();
                r.id = Convert.ToInt32(dr["CIDPROVEEDOR"]);
                r.descripcion = dr["CRAZONSOCIAL"].ToString();
                r.CIDPROVEEDOR = dr["CIDPROVEEDOR"].ToString();
                r.CCODIGOPROVEEDOR = dr["CCODIGOPROVEEDOR"].ToString();
                r.aux = dr["BaseDeDatos"].ToString();
                r.CDIASCREDITO = Convert.ToInt32(dr["CDIASCREDITO"]);
                le.Add(r);
            }
            return le;
        }
    }
}
