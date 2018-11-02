using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.Controllers
{
    public class ComprasPorPagarController : Controller
    {
        // GET: ComprasPorPagar
        public ActionResult Index()
        {
            CASCO.EN.General.Permisos permisos = CASCO.DAO.General.GetPermisosUsuario(User.Identity.Name);
            ViewBag.Permisos = permisos.LPermisos;
            return View();
        }

        public ActionResult PorValidar()
        {
            CASCO.EN.General.Permisos permisos = CASCO.DAO.General.GetPermisosUsuario(User.Identity.Name);
            if (!permisos.LPermisos["VerValidacion"])
                return Redirect(Url.Action("Index"));
            return View();
        }

        public ActionResult ProgramacionPago()
        {
            CASCO.EN.General.Permisos permisos = CASCO.DAO.General.GetPermisosUsuario(User.Identity.Name);
            if (!permisos.LPermisos["VerPorProgramar"])
                return Redirect(Url.Action("Index"));
            return View();
        }

        public ActionResult PorPagar()
        {
            CASCO.EN.General.Permisos permisos = CASCO.DAO.General.GetPermisosUsuario(User.Identity.Name);
            if (!permisos.LPermisos["VerProgramados"])
                return Redirect(Url.Action("Index"));
            return View();
        }

        public ActionResult Consultas()
        {
            return View();
        }

        public ActionResult PreValidacion()
        {
            CASCO.EN.General.Permisos permisos = CASCO.DAO.General.GetPermisosUsuario(User.Identity.Name);
            if (!permisos.LPermisos["VerPreValidacion"])
                return Redirect(Url.Action("Index"));
            return View();
        }

        [HttpPost]
        public JsonResult Consulta(CASCO.EN.Contabilidad.ParametrosComprasPorPagar parametros)
        {
            JsonResult j = new JsonResult();
            if (CASCO.EN.General.Utils.LogEnable)
            {
                string prs = "";
                prs = string.Format("TipoProveedor: {0}, Estatus: {1}, Fecha Inicio: {2}, Fecha Fin: {3}, Usuario: {4}", parametros.tipoProveedor, parametros.estatus, parametros.fechainicio, parametros.fechafin, User.Identity.Name);
                CASCO.EN.General.EventLog.WriteInfo(prs);
            }
            try {
                j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.ObtieneCompras(parametros);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ConsultaDocumentosAEntregar(CASCO.EN.Contabilidad.ParametrosComprasPorPagar parametros)
        {
            JsonResult j = new JsonResult();
            if (CASCO.EN.General.Utils.LogEnable)
            {
                string prs = "";
                prs = string.Format("TipoProveedor: {0}, Estatus: {1}, Fecha Inicio: {2}, Fecha Fin: {3}, Usuario: {4}", parametros.tipoProveedor, parametros.estatus, parametros.fechainicio, parametros.fechafin, User.Identity.Name);
                CASCO.EN.General.EventLog.WriteInfo(prs);
            }
            try
            {
                j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.ObtieneDocumentosAEntregar(parametros);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ConsultaProgramacionPago(CASCO.EN.Contabilidad.ParametrosComprasPorPagar parametros)
        {
            JsonResult j = new JsonResult();
            if (CASCO.EN.General.Utils.LogEnable)
            {
                string prs = "";
                prs = string.Format("TipoProveedor: {0}, Estatus: {1}, Fecha Inicio: {2}, Fecha Fin: {3}, Usuario: {4}", parametros.tipoProveedor, parametros.estatus, parametros.fechainicio, parametros.fechafin, User.Identity.Name);
                CASCO.EN.General.EventLog.WriteInfo(prs);
            }
            try
            {
                j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.ObtieneComprasProgramacionPago(parametros);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ConsultaPorFechaProgPago(CASCO.EN.Contabilidad.ParametrosComprasPorPagar parametros)
        {
            JsonResult j = new JsonResult();
            if (CASCO.EN.General.Utils.LogEnable)
            {
                string prs = "";
                prs = string.Format("TipoProveedor: {0}, Estatus: {1}, Fecha Inicio: {2}, Fecha Fin: {3}, Usuario: {4}", parametros.tipoProveedor, parametros.estatus, parametros.fechainicio, parametros.fechafin, User.Identity.Name);
                CASCO.EN.General.EventLog.WriteInfo(prs);
            }
            try
            {
                j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.ObtieneComprasPorFechaProgPago(parametros);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ObtieneListaEmpresas()
        {
            JsonResult j = new JsonResult();
            j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.ObtieneListaEmpresas();
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ObtieneListaProveedores(List<CASCO.EN.Abastos.Empresa> empresas)
        {
            JsonResult j = new JsonResult();
            j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.ObtieneListaProveedores(empresas);
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ObtieneListaTiposDeProveedor()
        {
            JsonResult j = new JsonResult();
            j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.ObtieneListaTiposDeProveedor();
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ObtieneListaEstatusComprasPorPagar()
        {
            JsonResult j = new JsonResult();
            j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.ObtieneEstatusComprasPendientes();
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ActualizaNoModificable(CASCO.EN.Contabilidad.ParametrosComprasPorPagar parametros)
        {
            string empresa = "";
            string proveedor = "";
            try
            {
                foreach (CASCO.EN.Abastos.Empresa e in parametros.empresa)
                {
                    empresa += e.id.ToString() + ",";
                }
                if (empresa.Length > 0)
                    empresa = empresa.Substring(0, empresa.Length - 1);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }

            try
            {
                foreach (CASCO.EN.Abastos.Proveedor p in parametros.proveedor)
                {
                    proveedor += p.id.ToString() + ",";
                }
                if (proveedor.Length > 0)
                    proveedor = proveedor.Substring(0, proveedor.Length - 1);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }

            if (CASCO.EN.General.Utils.LogEnable)
            {
                string prs = "";
                prs = string.Format("CIDDOCUMENTO: {0}, EmpresaID: {1}, ProveedorID: {2}, Valor: {3}, Usuario: {4}", parametros.ciddocumento, empresa, proveedor, parametros.valor, User.Identity.Name);
                CASCO.EN.General.EventLog.WriteInfo(prs);
            }

            JsonResult j = new JsonResult();
            try
            {
                j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.ActualizaNoModificable(Convert.ToInt32(parametros.ciddocumento), empresa, proveedor, Convert.ToInt32(parametros.valor), User.Identity.Name);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }

            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult SetFechaProgPago(CASCO.EN.Contabilidad.ParametrosComprasPorPagar parametros)
        {
            string empresa = "";
            string proveedor = "";
            if (parametros.empresa != null && parametros.empresa.Count > 0)
            {
                try
                {
                    foreach (CASCO.EN.Abastos.Empresa e in parametros.empresa)
                    {
                        empresa += e.id.ToString() + ",";
                    }
                    if (empresa.Length > 0)
                        empresa = empresa.Substring(0, empresa.Length - 1);
                }
                catch (Exception ex)
                {
                    CASCO.EN.General.EventLog.WriteError(ex.ToString());
                    throw ex;
                }
            }

            if (parametros.proveedor != null && parametros.proveedor.Count > 0)
            {
                try
                {
                    foreach (CASCO.EN.Abastos.Proveedor p in parametros.proveedor)
                    {
                        proveedor += p.id.ToString() + ",";
                    }
                    if (proveedor.Length > 0)
                        proveedor = proveedor.Substring(0, proveedor.Length - 1);
                }
                catch (Exception ex)
                {
                    CASCO.EN.General.EventLog.WriteError(ex.ToString());
                    throw ex;
                }
            }

            if (CASCO.EN.General.Utils.LogEnable)
            {
                string prs = "";
                prs = string.Format("CIDDOCUMENTO: {0}, EmpresaID: {1}, ProveedorID: {2}, Valor: {3}, Usuario: {4}", parametros.ciddocumento, empresa, proveedor, parametros.valor, User.Identity.Name);
                CASCO.EN.General.EventLog.WriteInfo(prs);
            }

            JsonResult j = new JsonResult();
            try
            {
                DateTime dtFechaProgPago = DateTime.ParseExact(parametros.FechaProgPago, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.SetFechaProgPago(Convert.ToInt32(parametros.ciddocumento), empresa, proveedor, dtFechaProgPago, User.Identity.Name, parametros.MontoTotal, parametros.MontoProgramado, parametros.SaldoPendiente);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }

            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult SetFechaPago(CASCO.EN.Contabilidad.ParametrosComprasPorPagar parametros)
        {
            string empresa = "";
            string proveedor = "";
            if (parametros.empresa != null && parametros.empresa.Count > 0)
            {
                try
                {
                    foreach (CASCO.EN.Abastos.Empresa e in parametros.empresa)
                    {
                        empresa += e.id.ToString() + ",";
                    }
                    if (empresa.Length > 0)
                        empresa = empresa.Substring(0, empresa.Length - 1);
                }
                catch (Exception ex)
                {
                    CASCO.EN.General.EventLog.WriteError(ex.ToString());
                    throw ex;
                }
            }

            if (parametros.proveedor != null && parametros.proveedor.Count > 0)
            {
                try
                {
                    foreach (CASCO.EN.Abastos.Proveedor p in parametros.proveedor)
                    {
                        proveedor += p.id.ToString() + ",";
                    }
                    if (proveedor.Length > 0)
                        proveedor = proveedor.Substring(0, proveedor.Length - 1);
                }
                catch (Exception ex)
                {
                    CASCO.EN.General.EventLog.WriteError(ex.ToString());
                    throw ex;
                }
            }

            if (CASCO.EN.General.Utils.LogEnable)
            {
                string prs = "";
                prs = string.Format("CIDDOCUMENTO: {0}, EmpresaID: {1}, ProveedorID: {2}, Valor: {3}, Usuario: {4}", parametros.ciddocumento, empresa, proveedor, parametros.valor, User.Identity.Name);
                CASCO.EN.General.EventLog.WriteInfo(prs);
            }

            JsonResult j = new JsonResult();
            try
            {
                DateTime dtFechaPago = DateTime.ParseExact(parametros.FechaPago, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.SetFechPago(Convert.ToInt32(parametros.ciddocumento), empresa, proveedor, dtFechaPago, User.Identity.Name, parametros.MontoTotal, parametros.MontoPagado, parametros.SaldoPendiente);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }

            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult GetMontoProgPago(CASCO.EN.Contabilidad.ParametrosComprasPorPagar parametros)
        {
            string empresa = "";
            string proveedor = "";
            if (parametros.empresa != null && parametros.empresa.Count > 0)
            {
                try
                {
                    foreach (CASCO.EN.Abastos.Empresa e in parametros.empresa)
                    {
                        empresa += e.id.ToString() + ",";
                    }
                    if (empresa.Length > 0)
                        empresa = empresa.Substring(0, empresa.Length - 1);
                }
                catch (Exception ex)
                {
                    CASCO.EN.General.EventLog.WriteError(ex.ToString());
                    throw ex;
                }
            }

            if (parametros.proveedor != null && parametros.proveedor.Count > 0)
            {
                try
                {
                    foreach (CASCO.EN.Abastos.Proveedor p in parametros.proveedor)
                    {
                        proveedor += p.id.ToString() + ",";
                    }
                    if (proveedor.Length > 0)
                        proveedor = proveedor.Substring(0, proveedor.Length - 1);
                }
                catch (Exception ex)
                {
                    CASCO.EN.General.EventLog.WriteError(ex.ToString());
                    throw ex;
                }
            }

            if (CASCO.EN.General.Utils.LogEnable)
            {
                string prs = "";
                prs = string.Format("CIDDOCUMENTO: {0}, EmpresaID: {1}, ProveedorID: {2}, Valor: {3}, Usuario: {4}", parametros.ciddocumento, empresa, proveedor, parametros.valor, User.Identity.Name);
                CASCO.EN.General.EventLog.WriteInfo(prs);
            }

            JsonResult j = new JsonResult();
            try
            {
                DateTime dtFechaProgPago = DateTime.ParseExact(parametros.FechaProgPago, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.GetMontoProgPago(empresa, proveedor, dtFechaProgPago);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }

            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult GetMontoPagado(CASCO.EN.Contabilidad.ParametrosComprasPorPagar parametros)
        {
            string empresa = "";
            string proveedor = "";
            if (parametros.empresa != null && parametros.empresa.Count > 0)
            {
                try
                {
                    foreach (CASCO.EN.Abastos.Empresa e in parametros.empresa)
                    {
                        empresa += e.id.ToString() + ",";
                    }
                    if (empresa.Length > 0)
                        empresa = empresa.Substring(0, empresa.Length - 1);
                }
                catch (Exception ex)
                {
                    CASCO.EN.General.EventLog.WriteError(ex.ToString());
                    throw ex;
                }
            }

            if (parametros.proveedor != null && parametros.proveedor.Count > 0)
            {
                try
                {
                    foreach (CASCO.EN.Abastos.Proveedor p in parametros.proveedor)
                    {
                        proveedor += p.id.ToString() + ",";
                    }
                    if (proveedor.Length > 0)
                        proveedor = proveedor.Substring(0, proveedor.Length - 1);
                }
                catch (Exception ex)
                {
                    CASCO.EN.General.EventLog.WriteError(ex.ToString());
                    throw ex;
                }
            }

            if (CASCO.EN.General.Utils.LogEnable)
            {
                string prs = "";
                prs = string.Format("CIDDOCUMENTO: {0}, EmpresaID: {1}, ProveedorID: {2}, Valor: {3}, Usuario: {4}", parametros.ciddocumento, empresa, proveedor, parametros.valor, User.Identity.Name);
                CASCO.EN.General.EventLog.WriteInfo(prs);
            }

            JsonResult j = new JsonResult();
            try
            {
                DateTime dtFechaPago = DateTime.ParseExact(parametros.FechaPago, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.GetMontoPagado(empresa, proveedor, dtFechaPago);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }

            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }
        
        [HttpPost]
        public JsonResult GetPagosProgramados()
        {
            JsonResult j = new JsonResult();
            j.Data = null;
            try
            {
                DataTable dt = CASCO.DAO.Contabilidad.ComprasPorPagar.GetPagosProgramados();
                j.Data = JsonConvert.SerializeObject(dt);


                Session["dt"] = dt;



            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;



           


            return j;
        }

        public JsonResult SetEstatus(CASCO.EN.Contabilidad.ParametrosComprasPorPagar parametros)
        {
            string empresa = "";
            string proveedor = "";
            if (parametros.empresa != null && parametros.empresa.Count > 0)
            {
                try
                {
                    foreach (CASCO.EN.Abastos.Empresa e in parametros.empresa)
                    {
                        empresa += e.id.ToString() + ",";
                    }
                    if (empresa.Length > 0)
                        empresa = empresa.Substring(0, empresa.Length - 1);
                }
                catch (Exception ex)
                {
                    CASCO.EN.General.EventLog.WriteError(ex.ToString());
                    throw ex;
                }
            }

            if (parametros.proveedor != null && parametros.proveedor.Count > 0)
            {
                try
                {
                    foreach (CASCO.EN.Abastos.Proveedor p in parametros.proveedor)
                    {
                        proveedor += p.id.ToString() + ",";
                    }
                    if (proveedor.Length > 0)
                        proveedor = proveedor.Substring(0, proveedor.Length - 1);
                }
                catch (Exception ex)
                {
                    CASCO.EN.General.EventLog.WriteError(ex.ToString());
                    throw ex;
                }
            }

            if (CASCO.EN.General.Utils.LogEnable)
            {
                string prs = "";
                prs = string.Format("CIDDOCUMENTO: {0}, EmpresaID: {1}, ProveedorID: {2}, Valor: {3}, Usuario: {4}", parametros.ciddocumento, empresa, proveedor, parametros.valor, User.Identity.Name);
                CASCO.EN.General.EventLog.WriteInfo(prs);
            }

            JsonResult j = new JsonResult();
            try
            {
                int? iestatus = 0;
                if (parametros.estatus != null && parametros.estatus.Count > 0)
                    iestatus = parametros.estatus[0].id;
                j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.SetEstatus(Convert.ToInt32(parametros.ciddocumento), empresa, proveedor, iestatus, User.Identity.Name);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }

            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult SetFechaEntregaDocto(CASCO.EN.Contabilidad.ParametrosComprasPorPagar parametros) {
            string empresa = "";
            string proveedor = "";
            if (parametros.empresa != null && parametros.empresa.Count > 0)
            {
                try
                {
                    foreach (CASCO.EN.Abastos.Empresa e in parametros.empresa)
                    {
                        empresa += e.id.ToString() + ",";
                    }
                    if (empresa.Length > 0)
                        empresa = empresa.Substring(0, empresa.Length - 1);
                }
                catch (Exception ex)
                {
                    CASCO.EN.General.EventLog.WriteError(ex.ToString());
                    throw ex;
                }
            }

            if (parametros.proveedor != null && parametros.proveedor.Count > 0)
            {
                try
                {
                    foreach (CASCO.EN.Abastos.Proveedor p in parametros.proveedor)
                    {
                        proveedor += p.id.ToString() + ",";
                    }
                    if (proveedor.Length > 0)
                        proveedor = proveedor.Substring(0, proveedor.Length - 1);
                }
                catch (Exception ex)
                {
                    CASCO.EN.General.EventLog.WriteError(ex.ToString());
                    throw ex;
                }
            }

            if (CASCO.EN.General.Utils.LogEnable)
            {
                string prs = "";
                prs = string.Format("CIDDOCUMENTO: {0}, EmpresaID: {1}, ProveedorID: {2}, Valor: {3}, Usuario: {4}", parametros.ciddocumento, empresa, proveedor, parametros.valor, User.Identity.Name);
                CASCO.EN.General.EventLog.WriteInfo(prs);
            }

            JsonResult j = new JsonResult();
            try
            {           
                DateTime dtFechaEntregaDoctos = DateTime.ParseExact(parametros.FechaEntregaDocto, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                j.Data = CASCO.DAO.Contabilidad.ComprasPorPagar.SetFechaEntregaDoctos(Convert.ToInt32(parametros.ciddocumento), empresa, proveedor, dtFechaEntregaDoctos, User.Identity.Name);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }

            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }
    }
}