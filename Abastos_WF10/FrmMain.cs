using System;
using System.Windows.Forms;

namespace Abastos_WF
{
	public partial class FrmMain : Form
	{
		public FrmMain()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
                CASCO.DAO.Abastos.SolicitudMateriales.ActualizaCatalogos();
                CASCO.DAO.Abastos.SolicitudMateriales.GeneraOrdenesDeCompraPendientes();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}