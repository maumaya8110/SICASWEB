using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.Npage
{
    public partial class dttoxls : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {




            DataTable dt = Session["dt"] as DataTable;


            if (dt.Rows.Count > 0)
            {
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = " ";
                response.ContentType = "application/vnd.ms-excel";
                response.AddHeader("Content-Disposition", "attachment;filename=" + "Reporte" + ".xls;");
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                using (sw)
                {
                    using (htw)
                    {
                        GridView dg = new GridView();
                        dg.HeaderStyle.CssClass = "";
                        dg.DataSource = dt;
                        dg.DataBind();
                        dg.RenderControl(htw);
                        response.Write(sw.ToString());
                        response.End();

                    }
                }
                htw.Dispose();
                sw.Dispose();
                htw = null;
                sw = null;

            }


        }

    }
}