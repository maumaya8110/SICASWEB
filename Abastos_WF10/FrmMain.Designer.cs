namespace Abastos_WF
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCrearDocto = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCrearDocto
            // 
            this.btnCrearDocto.Location = new System.Drawing.Point(53, 31);
            this.btnCrearDocto.Name = "btnCrearDocto";
            this.btnCrearDocto.Size = new System.Drawing.Size(189, 122);
            this.btnCrearDocto.TabIndex = 0;
            this.btnCrearDocto.Text = "Genera Ordenes de Compra Pendientes";
            this.btnCrearDocto.UseVisualStyleBackColor = true;
            this.btnCrearDocto.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.btnCrearDocto);
            this.Name = "FrmMain";
            this.Text = "Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCrearDocto;
    }
}

