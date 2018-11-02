using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using CASCO.EN.Contabilidad;
using CASCO.EN;
using System.Web.UI.WebControls;

namespace CASCO.DAO.Contabilidad
{
    public static class ComprasPorPagar
    {
        public static List<CASCO.EN.Abastos.Proveedor> ObtieneListaProveedores(List<CASCO.EN.Abastos.Empresa> empresas)
        {
            string sempresas = "";
            if (empresas != null && empresas.Count > 0)
            {
                foreach (EN.Abastos.Empresa e in empresas)
                {
                    sempresas += e.id.ToString() + ",";
                }
                sempresas = sempresas.Substring(0, sempresas.Length - 1);
            }

            List<EN.Abastos.Proveedor> le = new List<EN.Abastos.Proveedor>();
            string sqlstr = "[Contabilidad].[usp_Proveedores_Consulta_PorEmpresa]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@Empresa_ID", sempresas);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                EN.Abastos.Proveedor r = new EN.Abastos.Proveedor();
                r.id = Convert.ToInt32(dr["CIDPROVEEDOR"]);
                r.descripcion = dr["CRAZONSOCIAL"].ToString();
                r.CIDPROVEEDOR = dr["CIDPROVEEDOR"].ToString();
                r.CCODIGOPROVEEDOR = dr["CCODIGOPROVEEDOR"].ToString();
                r.aux = dr["BaseDeDatos"].ToString();
                r.CDIASCREDITO = Convert.ToInt32(dr["CDIASCREDITO"]);
                r.empresa.id = Convert.ToInt32(dr["Empresa_ID"]);
                r.empresa.descripcion = dr["Empresa_Descripcion"].ToString();
                le.Add(r);
            }
            return le;
        }

        public static List<EN.Contabilidad.ComprasPorPagar> ObtieneCompras(EN.Contabilidad.ParametrosComprasPorPagar parametros)
        {
            List<EN.Contabilidad.ComprasPorPagar> lcp = new List<EN.Contabilidad.ComprasPorPagar>();
            string sqlstr = "[contabilidad].[usp_ComprasPorPagar_Consulta]";
            Hashtable m_params = new Hashtable();

            string empresas = "";
            string proveedores = "";
            string estatus = "";
            string tiposproveedor = "";

            if (parametros.empresa != null && parametros.empresa.Count > 0)
            {
                foreach (EN.Abastos.Empresa e in parametros.empresa)
                {
                    empresas += e.id.ToString() + ",";
                }
                empresas = empresas.Substring(0, empresas.Length - 1);
            }

            if (parametros.proveedor != null && parametros.proveedor.Count > 0)
            {
                foreach (EN.Abastos.Proveedor p in parametros.proveedor)
                {
                    proveedores += p.id.ToString() + ",";
                }
                proveedores = proveedores.Substring(0, proveedores.Length - 1);
            }

            if (parametros.estatus != null && parametros.estatus.Count > 0)
            {
                foreach (EN.Item p in parametros.estatus)
                {
                    estatus += p.id.ToString() + ",";
                }
                estatus = estatus.Substring(0, estatus.Length - 1);
            }

            if (parametros.tiposproveedor != null && parametros.tiposproveedor.Count > 0)
            {
                foreach (EN.Item p in parametros.tiposproveedor)
                {
                    tiposproveedor += p.aux.ToString() + ",";
                }
                tiposproveedor = tiposproveedor.Substring(0, tiposproveedor.Length - 1);
            }

            m_params.Add("@Empresa_ID", empresas);
            m_params.Add("@Proveedor_ID", proveedores);
            if (parametros.fechainicio != null)
                m_params.Add("@FechaInicio", DateTime.ParseExact(parametros.fechainicio, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            if (parametros.fechafin != null)
                m_params.Add("@FechaFin", DateTime.ParseExact(parametros.fechafin, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            m_params.Add("@EstatusCompraPorPagar_ID", estatus);
            m_params.Add("@TipoProveedor", tiposproveedor);
            m_params.Add("@Todas", parametros.Todas);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            
            return GetComprasPorPagarList(dt, parametros.proveedor);
        }

        public static object ObtieneDocumentosAEntregar(ParametrosComprasPorPagar parametros)
            {
            List<EN.Contabilidad.ComprasPorPagar> lcp = new List<EN.Contabilidad.ComprasPorPagar>();
            string sqlstr = "[contabilidad].[usp_ComprasPorPagar_ConsultaDocumentosEntregados]";
            Hashtable m_params = new Hashtable();

            string empresas = "";
            string proveedores = "";
            string estatus = "";

            if (parametros.empresa != null && parametros.empresa.Count > 0)
            {
                foreach (EN.Abastos.Empresa e in parametros.empresa)
                {
                    empresas += e.id.ToString() + ",";
                }
                empresas = empresas.Substring(0, empresas.Length - 1);
            }

            if (parametros.proveedor != null && parametros.proveedor.Count > 0)
            {
                foreach (EN.Abastos.Proveedor p in parametros.proveedor)
                {
                    proveedores += p.id.ToString() + ",";
                }
                proveedores = proveedores.Substring(0, proveedores.Length - 1);
            }

            if (parametros.estatus != null && parametros.estatus.Count > 0)
            {
                foreach (EN.Item p in parametros.estatus)
                {
                    estatus += p.id.ToString() + ",";
                }
                estatus = estatus.Substring(0, estatus.Length - 1);
            }

            m_params.Add("@Empresa_ID", empresas);
            m_params.Add("@Proveedor_ID", proveedores);
            if (parametros.fechainicio != null)
                m_params.Add("@FechaInicio", DateTime.ParseExact(parametros.fechainicio, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            if (parametros.fechafin != null)
                m_params.Add("@FechaFin", DateTime.ParseExact(parametros.fechafin, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            m_params.Add("@EstatusCompraPorPagar_ID", estatus);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

            return GetComprasPorPagarList(dt, parametros.proveedor);
        }

        public static object ObtieneComprasProgramacionPago(ParametrosComprasPorPagar parametros)
        {
            List<EN.Contabilidad.ComprasPorPagar> lcp = new List<EN.Contabilidad.ComprasPorPagar>();
            string sqlstr = "[contabilidad].[usp_ComprasPorPagar_ConsultaProgramacionPago]";
            Hashtable m_params = new Hashtable();

            string empresas = "";
            string proveedores = "";
            string estatus = "";
            string tiposproveedor = "";

            if (parametros.empresa != null && parametros.empresa.Count > 0)
            {
                foreach (EN.Abastos.Empresa e in parametros.empresa)
                {
                    empresas += e.id.ToString() + ",";
                }
                empresas = empresas.Substring(0, empresas.Length - 1);
            }

            if (parametros.proveedor != null && parametros.proveedor.Count > 0)
            {
                foreach (EN.Abastos.Proveedor p in parametros.proveedor)
                {
                    proveedores += p.id.ToString() + ",";
                }
                proveedores = proveedores.Substring(0, proveedores.Length - 1);
            }

            if (parametros.estatus != null && parametros.estatus.Count > 0)
            {
                foreach (EN.Item p in parametros.estatus)
                {
                    estatus += p.id.ToString() + ",";
                }
                estatus = estatus.Substring(0, estatus.Length - 1);
            }

            if (parametros.tiposproveedor != null && parametros.tiposproveedor.Count > 0)
            {
                foreach (EN.Item p in parametros.tiposproveedor)
                {
                    tiposproveedor += p.aux.ToString() + ",";
                }
                tiposproveedor = tiposproveedor.Substring(0, tiposproveedor.Length - 1);
            }

            m_params.Add("@Empresa_ID", empresas);
            m_params.Add("@Proveedor_ID", proveedores);
            if (parametros.fechainicio != null)
                m_params.Add("@FechaInicio", DateTime.ParseExact(parametros.fechainicio, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            if (parametros.fechafin != null)
                m_params.Add("@FechaFin", DateTime.ParseExact(parametros.fechafin, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            m_params.Add("@EstatusCompraPorPagar_ID", estatus);
            m_params.Add("@TipoProveedor", tiposproveedor);
            m_params.Add("@Todas", parametros.Todas);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

            return GetComprasPorPagarList(dt, parametros.proveedor);
        }

        public static object ObtieneComprasPorFechaProgPago(ParametrosComprasPorPagar parametros)
        {
            
            string sqlstr = "[contabilidad].[usp_ComprasPorPagar_ConsultaPorFechaProgPago]";
            Hashtable m_params = new Hashtable();

            string empresas = "";
            string proveedores = "";
            string estatus = "";
            string tiposproveedor = "";

            if (parametros.empresa != null && parametros.empresa.Count > 0)
            {
                foreach (EN.Abastos.Empresa e in parametros.empresa)
                {
                    empresas += e.id.ToString() + ",";
                }
                empresas = empresas.Substring(0, empresas.Length - 1);
            }

            if (parametros.proveedor != null && parametros.proveedor.Count > 0)
            {
                foreach (EN.Abastos.Proveedor p in parametros.proveedor)
                {
                    proveedores += p.id.ToString() + ",";
                }
                proveedores = proveedores.Substring(0, proveedores.Length - 1);
            }

            if (parametros.estatus != null && parametros.estatus.Count > 0)
            {
                foreach (EN.Item p in parametros.estatus)
                {
                    estatus += p.id.ToString() + ",";
                }
                estatus = estatus.Substring(0, estatus.Length - 1);
            }

            if (parametros.tiposproveedor != null && parametros.tiposproveedor.Count > 0)
            {
                foreach (EN.Item p in parametros.tiposproveedor)
                {
                    tiposproveedor += p.aux.ToString() + ",";
                }
                tiposproveedor = tiposproveedor.Substring(0, tiposproveedor.Length - 1);
            }

            m_params.Add("@Empresa_ID", empresas);
            m_params.Add("@Proveedor_ID", proveedores);
            m_params.Add("@EstatusCompraPorPagar_ID", estatus);
            m_params.Add("@Todas", parametros.Todas);
            if (parametros.FechaProgPago != null)
                m_params.Add("@FechaPagoProg", DateTime.ParseExact(parametros.FechaProgPago, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

            return GetComprasPorPagarList(dt, parametros.proveedor);
            
        }

        private static List<EN.Contabilidad.ComprasPorPagar> GetComprasPorPagarList(DataTable dt, List<EN.Abastos.Proveedor> proveedores)
        {
            List<EN.Contabilidad.ComprasPorPagar> lcp = new List<EN.Contabilidad.ComprasPorPagar>();
            foreach (DataRow dr in dt.Rows)
            {
                EN.Contabilidad.ComprasPorPagar cp = new EN.Contabilidad.ComprasPorPagar();
                cp.CIDDOCUMENTO = Convert.ToInt32(dr["CIDDOCUMENTO"]);
                cp.CSERIEDOCUMENTO = dr["CSERIEDOCUMENTO"].ToString();
                cp.CFOLIO = dr["CFOLIO"].ToString();
                cp.CFECHA = Convert.ToDateTime(dr["CFECHA"]);
                cp.CRAZONSOCIAL = dr["CRAZONSOCIAL"].ToString();
                cp.CFECHAVENCIMIENTO = Convert.ToDateTime(dr["CFECHAVENCIMIENTO"]);
                cp.CFECHAENTREGARECEPCION = Convert.ToDateTime(dr["CFECHAENTREGARECEPCION"]);
                cp.CIDMONEDA = Convert.ToInt32(dr["CIDMONEDA"]);
                cp.CNETO = Convert.ToDouble(dr["CNETO"]);
                cp.CIMPUESTO1 = Convert.ToDouble(dr["CIMPUESTO1"]);
                cp.CTOTAL = Convert.ToDouble(dr["CTOTAL"]);
                cp.CPAGOPARCIALIDAD = Convert.ToDouble(dr["CPAGOPARCIALIDAD"]);
                cp.CPENDIENTE = Convert.ToDouble(dr["CPENDIENTE"]);
                cp.CTEXTOEXTRA1 = dr["CTEXTOEXTRA1"].ToString();
                cp.CBANOBSERVACIONES = dr["CBANOBSERVACIONES"].ToString();
                cp.CAFECTADO = Convert.ToInt32(dr["CAFECTADO"]);
                cp.CIMPRESO = Convert.ToBoolean(dr["CIMPRESO"]);
                cp.CCANCELADO = Convert.ToInt32(dr["CCANCELADO"]);
                cp.CDEVUELTO = Convert.ToInt32(dr["CDEVUELTO"]);
                cp.CIDPREPOLIZA = Convert.ToInt32(dr["CIDPREPOLIZA"]);
                cp.CIDPREPOLIZACANCELACION = Convert.ToInt32(dr["CIDPREPOLIZACANCELACION"]);
                cp.CESTADOCONTABLE = Convert.ToInt32(dr["CESTADOCONTABLE"]);
                cp.Estatus = dr["Estatus"].ToString();
                cp.CIDCONCEPTODOCUMENTO = Convert.ToInt32(dr["CIDCONCEPTODOCUMENTO"]);
                cp.CNOMBRECONCEPTO = dr["CNOMBRECONCEPTO"].ToString();
                cp.BaseDeDatos = dr["BaseDeDatos"].ToString();
                cp.CIDPROVEEDOR = Convert.ToInt32(dr["CIDPROVEEDOR"]);
                if (!DB.IsNullOrEmpty(dr["FechaPagoProgramado"]))
                    cp.FechaProgPago = Convert.ToDateTime(dr["FechaPagoProgramado"]);
                if (!DB.IsNullOrEmpty(dr["FechaPago"]))
                    cp.FechaPago = Convert.ToDateTime(dr["FechaPago"]);

                if (!DB.IsNullOrEmpty(dr["Empresa_EmpresaID"]))
                    cp.empresa.id = Convert.ToInt32(dr["Empresa_EmpresaID"]);
                if (!DB.IsNullOrEmpty(dr["Empresa_Descripcion"]))
                    cp.empresa.descripcion = dr["Empresa_Descripcion"].ToString();
                if (!DB.IsNullOrEmpty(dr["Empresa_BaseDeDatos"]))
                    cp.empresa.aux = dr["Empresa_BaseDeDatos"].ToString();
                if (!DB.IsNullOrEmpty(dr["Empresa_ADD"]))
                    cp.empresa.ADD = dr["Empresa_ADD"].ToString();

                if (!DB.IsNullOrEmpty(dr["CREFERENCIA"]))
                    cp.CREFERENCIA = dr["CREFERENCIA"].ToString();

                if (!DB.IsNullOrEmpty(dr["FechaEntregaDocumentos"]))
                    cp.FechaEntregaDoctos = Convert.ToDateTime(dr["FechaEntregaDocumentos"]);

                if (!DB.IsNullOrEmpty(dr["DiasVencimiento"]))
                    cp.DiasVencimiento = Convert.ToInt32(dr["DiasVencimiento"]);

                if (!DB.IsNullOrEmpty(dr["FechaEntregaDocumentos"]))
                    cp.FechaEntregaDocumentos = Convert.ToDateTime(dr["FechaEntregaDocumentos"]);

            


                //if (!DB.IsNullOrEmpty(dr["DIASVENCIMIENTO"]))
                //    cp.CREFERENCIA = dr["DIASVENCIMIENTO"].ToString();


                //if (!DB.IsNullOrEmpty(dr["CFECHAENTREGARECEPCION"]))
                //    cp.FechaEntregaDoctos = Convert.ToDateTime(dr["CFECHAENTREGARECEPCION"]);



                foreach (EN.Abastos.Proveedor p in proveedores)
                {
                    if (p.id == cp.CIDPROVEEDOR)
                        cp.CDIASCREDITO = p.CDIASCREDITO;
                }
                lcp.Add(cp);
            }
            return lcp;
        }

        public static object SetFechaProgPago(
                                                int ciddocumento
                                                , string empresa
                                                , string proveedor
                                                , DateTime dtFechaProgPago
                                                , string usuario_id
                                                , double MontoTotal
                                                , double MontoProgramado
                                                , double SaldoPendiente
                                                )
        {
            string sql = "[contabilidad].[usp_ComprasPorPagar_SetFechaProgPago]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@CIDDOCUMENTO", ciddocumento);
            m_params.Add("@Empresa_ID", empresa);
            m_params.Add("@Proveedor_ID", proveedor);
            m_params.Add("@FechaProgPago", dtFechaProgPago);
            m_params.Add("@Usuario_ID", usuario_id);

            m_params.Add("@MontoTotal", MontoTotal);
            m_params.Add("@MontoProgramado", MontoProgramado);
            m_params.Add("@SaldoPendiente", SaldoPendiente);

            object obj = DB.ExecuteCommandSP_With_Return(sql, m_params);
            return Convert.ToBoolean(obj);
        }

        public static List<EN.Item> ObtieneListaTiposDeProveedor()
        {
            List<EN.Item> lcp = new List<EN.Item>();
            string sqlstr = "[contabilidad].[usp_ComprasPorPagar_ClasificacionProveedores]";
            Hashtable m_params = new Hashtable();
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                EN.Item cp = new EN.Item();
                cp.id = Convert.ToInt32(dr["ID"]);
                cp.descripcion = dr["Descripcion"].ToString();
                cp.aux = dr["Aux"].ToString();
                lcp.Add(cp);
            }
            return lcp;
        }

        public static object SetFechPago(
                                            int ciddocumento
                                            , string empresa
                                            , string proveedor
                                            , DateTime dtFechaPago
                                            , string usuario_id
                                            , double MontoTotal
                                            , double MontoPagado
                                            , double SaldoPendiente
                                        )
        {
            string sql = "[contabilidad].[usp_ComprasPorPagar_SetFechaPago]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@CIDDOCUMENTO", ciddocumento);
            m_params.Add("@Empresa_ID", empresa);
            m_params.Add("@Proveedor_ID", proveedor);
            m_params.Add("@FechaPago", dtFechaPago);
            m_params.Add("@Usuario_ID", usuario_id);
            m_params.Add("@MontoTotal", MontoTotal);
            m_params.Add("@MontoPagado", MontoPagado);
            m_params.Add("@SaldoPendiente", SaldoPendiente);
            object obj = DB.ExecuteCommandSP_With_Return(sql, m_params);
            return Convert.ToBoolean(obj);
        }

        public static List<EN.Item> ObtieneEstatusComprasPendientes()
        {
            List<EN.Item> lcp = new List<EN.Item>();
            string sqlstr = "[contabilidad].[usp_ComprasPorPagar_Estatus_Consulta]";
            Hashtable m_params = new Hashtable();
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                EN.Item cp = new EN.Item();
                cp.id = Convert.ToInt32(dr["EstatusCompraPorPagar_ID"]);
                cp.descripcion = dr["Descripcion"].ToString();
                cp.aux = dr["EstatusCompraPorPagar_ID"].ToString();
                lcp.Add(cp);
            }
            return lcp;
        }

        public static bool ActualizaNoModificable(int ciddocumento, string empresa_id, string proveedor_id, int valor, string usuario_id)
        {
            string sql = "[contabilidad].[usp_ComprasPorPagar_UpdateNoModificable]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@CIDDOCUMENTO", ciddocumento);
            m_params.Add("@Empresa_ID", empresa_id);
            m_params.Add("@Proveedor_ID", proveedor_id);
            m_params.Add("@Value", valor);
            m_params.Add("@Usuario_ID", usuario_id);
            object obj = DB.ExecuteCommandSP_With_Return(sql, m_params);
            return Convert.ToBoolean(obj);
        }

        public static List<EN.Item> GetMontoProgPago(string empresa, string proveedor, DateTime dtFechaProgPago)
        {
            List<EN.Item> lcp = new List<EN.Item>();
            string sqlstr = "contabilidad.usp_ComprasPorPagar_MontoProgramadoParaPago_PorFecha";
            Hashtable m_params = new Hashtable();
            m_params.Add("@Fecha", dtFechaProgPago);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                EN.Item cp = new EN.Item();
                cp.id = Convert.ToInt32(dr["ID"]);
                cp.descripcion = dr["FechaProgPago"].ToString();
                cp.aux = dr["Monto"].ToString();
                lcp.Add(cp);
            }
            return lcp;
        }

        public static List<EN.Item> GetMontoPagado(string empresa, string proveedor, DateTime dtFechaPago)
        {
            List<EN.Item> lcp = new List<EN.Item>();
            string sqlstr = "contabilidad.usp_ComprasPorPagar_MontoPago_PorFecha";
            Hashtable m_params = new Hashtable();
            m_params.Add("@Fecha", dtFechaPago);
            m_params.Add("@Proveedor", proveedor);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                EN.Item cp = new EN.Item();
                cp.id = Convert.ToInt32(dr["ID"]);
                cp.descripcion = dr["FechaPago"].ToString();
                cp.aux = dr["Monto"].ToString();
                lcp.Add(cp);
            }
            return lcp;
        }

        public static List<EN.Abastos.Empresa> ObtieneListaEmpresas()
        {
            List<EN.Abastos.Empresa> le = new List<EN.Abastos.Empresa>();
            string sqlstr = "[Contabilidad].[usp_Empresas_Consulta]";
            Hashtable m_params = new Hashtable();
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                EN.Abastos.Empresa r = new EN.Abastos.Empresa();
                r.id = Convert.ToInt32(dr["Empresa_ID"]);
                r.descripcion = dr["Empresa"].ToString();
                r.aux = dr["EmpresaDB"].ToString();
                r.ADD = dr["EmpresaADD"].ToString();
                le.Add(r);
            }
            return le;
        }

        public static DataTable GetPagosProgramados()
        {
            string sqlstr = "[Contabilidad].[usp_ComprasPorPagar_ReportePagosProgramados]";
            Hashtable m_params = new Hashtable();
            DataSet ds = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut);
            DataTable dt = null;
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
             

            return dt;
        }

        public static bool SetEstatus(int ciddocumento, string empresa, string proveedor, int? estatus, string name)
        {
            string sql = "[contabilidad].[usp_ComprasPorPagar_UpdateEstatusCompra]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@CIDDOCUMENTO", ciddocumento);
            m_params.Add("@Empresa_ID", empresa);
            m_params.Add("@Proveedor_ID", proveedor);
            m_params.Add("@Estatus_ID", estatus);
            m_params.Add("@Usuario_ID", name);
            object obj = DB.ExecuteCommandSP_With_Return(sql, m_params);
            return Convert.ToBoolean(obj);
        }

        public static bool SetFechaEntregaDoctos(int ciddocumento, string empresa, string proveedor, DateTime dtFechaEntregaDoctos, string usuario_id)
        {
            string sql = "[contabilidad].[usp_ComprasPorPagar_SetFechaEntregaDoctos]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@CIDDOCUMENTO", ciddocumento);
            m_params.Add("@Empresa_ID", empresa);
            m_params.Add("@Proveedor_ID", proveedor);
            m_params.Add("@FechaEntregaDoctos", dtFechaEntregaDoctos);
            m_params.Add("@Usuario_ID", usuario_id);
            object obj = DB.ExecuteCommandSP_With_Return(sql, m_params);
            return Convert.ToBoolean(obj);
        }
    }
}