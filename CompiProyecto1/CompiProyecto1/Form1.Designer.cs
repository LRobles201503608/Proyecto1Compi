namespace CompiProyecto1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aRCHIVOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aBRIRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gUARDARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sALIRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sALIRToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eJECUCIONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eJECUTARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eRRORESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEPORTESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aYUDAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mANUALESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hECHOPORToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.lblconteo = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aRCHIVOToolStripMenuItem,
            this.eJECUCIONToolStripMenuItem,
            this.aYUDAToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1078, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aRCHIVOToolStripMenuItem
            // 
            this.aRCHIVOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aBRIRToolStripMenuItem,
            this.gUARDARToolStripMenuItem,
            this.sALIRToolStripMenuItem,
            this.sALIRToolStripMenuItem1});
            this.aRCHIVOToolStripMenuItem.Name = "aRCHIVOToolStripMenuItem";
            this.aRCHIVOToolStripMenuItem.Size = new System.Drawing.Size(86, 24);
            this.aRCHIVOToolStripMenuItem.Text = "ARCHIVO";
            // 
            // aBRIRToolStripMenuItem
            // 
            this.aBRIRToolStripMenuItem.Name = "aBRIRToolStripMenuItem";
            this.aBRIRToolStripMenuItem.Size = new System.Drawing.Size(161, 26);
            this.aBRIRToolStripMenuItem.Text = "NUEVO";
            this.aBRIRToolStripMenuItem.Click += new System.EventHandler(this.ABRIRToolStripMenuItem_Click);
            // 
            // gUARDARToolStripMenuItem
            // 
            this.gUARDARToolStripMenuItem.Name = "gUARDARToolStripMenuItem";
            this.gUARDARToolStripMenuItem.Size = new System.Drawing.Size(161, 26);
            this.gUARDARToolStripMenuItem.Text = "ABRIR";
            this.gUARDARToolStripMenuItem.Click += new System.EventHandler(this.GUARDARToolStripMenuItem_Click);
            // 
            // sALIRToolStripMenuItem
            // 
            this.sALIRToolStripMenuItem.Name = "sALIRToolStripMenuItem";
            this.sALIRToolStripMenuItem.Size = new System.Drawing.Size(161, 26);
            this.sALIRToolStripMenuItem.Text = "GUARDAR";
            this.sALIRToolStripMenuItem.Click += new System.EventHandler(this.SALIRToolStripMenuItem_Click);
            // 
            // sALIRToolStripMenuItem1
            // 
            this.sALIRToolStripMenuItem1.Name = "sALIRToolStripMenuItem1";
            this.sALIRToolStripMenuItem1.Size = new System.Drawing.Size(161, 26);
            this.sALIRToolStripMenuItem1.Text = "SALIR";
            this.sALIRToolStripMenuItem1.Click += new System.EventHandler(this.SALIRToolStripMenuItem1_Click);
            // 
            // eJECUCIONToolStripMenuItem
            // 
            this.eJECUCIONToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eJECUTARToolStripMenuItem,
            this.eRRORESToolStripMenuItem,
            this.rEPORTESToolStripMenuItem});
            this.eJECUCIONToolStripMenuItem.Name = "eJECUCIONToolStripMenuItem";
            this.eJECUCIONToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.eJECUCIONToolStripMenuItem.Text = "EJECUCION";
            // 
            // eJECUTARToolStripMenuItem
            // 
            this.eJECUTARToolStripMenuItem.Name = "eJECUTARToolStripMenuItem";
            this.eJECUTARToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.eJECUTARToolStripMenuItem.Text = "EJECUTAR";
            this.eJECUTARToolStripMenuItem.Click += new System.EventHandler(this.EJECUTARToolStripMenuItem_Click);
            // 
            // eRRORESToolStripMenuItem
            // 
            this.eRRORESToolStripMenuItem.Name = "eRRORESToolStripMenuItem";
            this.eRRORESToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.eRRORESToolStripMenuItem.Text = "ERRORES";
            this.eRRORESToolStripMenuItem.Click += new System.EventHandler(this.ERRORESToolStripMenuItem_Click);
            // 
            // rEPORTESToolStripMenuItem
            // 
            this.rEPORTESToolStripMenuItem.Name = "rEPORTESToolStripMenuItem";
            this.rEPORTESToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.rEPORTESToolStripMenuItem.Text = "REPORTES";
            this.rEPORTESToolStripMenuItem.Click += new System.EventHandler(this.REPORTESToolStripMenuItem_Click);
            // 
            // aYUDAToolStripMenuItem
            // 
            this.aYUDAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mANUALESToolStripMenuItem,
            this.hECHOPORToolStripMenuItem});
            this.aYUDAToolStripMenuItem.Name = "aYUDAToolStripMenuItem";
            this.aYUDAToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.aYUDAToolStripMenuItem.Text = "AYUDA";
            // 
            // mANUALESToolStripMenuItem
            // 
            this.mANUALESToolStripMenuItem.Name = "mANUALESToolStripMenuItem";
            this.mANUALESToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.mANUALESToolStripMenuItem.Text = "MANUALES";
            // 
            // hECHOPORToolStripMenuItem
            // 
            this.hECHOPORToolStripMenuItem.Name = "hECHOPORToolStripMenuItem";
            this.hECHOPORToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.hECHOPORToolStripMenuItem.Text = "HECHO POR...";
            this.hECHOPORToolStripMenuItem.Click += new System.EventHandler(this.HECHOPORToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(26, 46);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1016, 279);
            this.tabControl1.TabIndex = 1;
            // 
            // lblconteo
            // 
            this.lblconteo.AutoSize = true;
            this.lblconteo.Location = new System.Drawing.Point(440, 351);
            this.lblconteo.Name = "lblconteo";
            this.lblconteo.Size = new System.Drawing.Size(53, 17);
            this.lblconteo.TabIndex = 2;
            this.lblconteo.Text = "Conteo";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(26, 401);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1016, 250);
            this.webBrowser1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 673);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.lblconteo);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aRCHIVOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aBRIRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gUARDARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sALIRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eJECUCIONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aYUDAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sALIRToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eJECUTARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eRRORESToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEPORTESToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mANUALESToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hECHOPORToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label lblconteo;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

