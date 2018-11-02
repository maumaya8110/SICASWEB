using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CASCO.EN.Abastos;

namespace Abastos.Models
{
    public static class SolicitudMateriales
    {
        internal static void Inserta(CASCO.EN.Abastos.SolicitudMateriales solicitud)
        {
            CASCO.DAO.Abastos.SolicitudMateriales.InsertaSolicitud(solicitud);
        }

        internal static List<ArticuloSolicitudMateriales> GetServiciosPorDivisionEmpresaDeptoAlmacen(int division, int empresa, int departamento, int? almacen)
        {
            return CASCO.DAO.Abastos.SolicitudMateriales.GetServiciosPorDivisionEmpresaDeptoAlmacen(division, empresa, departamento, almacen);
        }

        internal static object Consulta(SolicitudMaterialesConsulta sol)
        {
            return CASCO.DAO.Abastos.SolicitudMateriales.GetSolicitudDeMateriales(sol);
        }

        internal static List<Almacen> GetAlmacenesPorDivisionEmpresaDepto(int division, int empresa, int departamento)
        {
            return CASCO.DAO.Abastos.SolicitudMateriales.GetAlmacenesPorDivisionEmpresaDepto(division, empresa, departamento);
        }

        internal static bool ActualizaEstatus(string usuario_id, int? requisicionId, int? estatusId)
        {
            return CASCO.DAO.Abastos.SolicitudMateriales.ActualizaEstatus(usuario_id, requisicionId, estatusId);
        }

        internal static List<CASCO.EN.Abastos.SoporteElectronicoSolicitudMateriales> ConsultaSoportes(int division, int empresa, int departamento)
        {
            return CASCO.DAO.Abastos.SolicitudMateriales.GetSoportes(division, empresa, departamento);
        }

        internal static string AgregaComentarioASolicitud(int idSolicitud, string comentario, string user)
        {
            try
            {
                if (CASCO.DAO.Abastos.SolicitudMateriales.AgregaComentarioASolicitud(idSolicitud, comentario, user))
                    return CASCO.DAO.Abastos.SolicitudMateriales.ObtieneComentarioDeSolicitud(idSolicitud);
                else
                    throw new Exception("Ocurrió un error al Insertar el comentario.");
            }
            catch (Exception ex) { throw ex; }
        }

        internal static string ObtieneComentarioDeSolicitud(int idSolicitud)
        {
            return CASCO.DAO.Abastos.SolicitudMateriales.ObtieneComentarioDeSolicitud(idSolicitud);
        }

        internal static bool InsertaDocumentoSoporte(int solicitud_id, CASCO.EN.Abastos.SoporteElectronicoSolicitudMateriales soporte)
        {
            return CASCO.DAO.Abastos.SolicitudMateriales.InsertaDocumentoSoporte(solicitud_id, soporte);
        }

        internal static bool EliminaRegistroDetalle(int idSolicitud, int idx)
        {
            return CASCO.DAO.Abastos.SolicitudMateriales.EliminaRegistroDetalle(idSolicitud, idx);
        }
    }
}