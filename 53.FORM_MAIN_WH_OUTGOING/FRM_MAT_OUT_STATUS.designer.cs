namespace FORM
{
    partial class FRM_MAT_OUT_STATUS
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_MAT_OUT_STATUS));
            this.tmr_reload = new System.Windows.Forms.Timer(this.components);
            this.tmr_detail = new System.Windows.Forms.Timer(this.components);
            this.txttemp = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDate = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPros = new System.Windows.Forms.Label();
            this.lblComp = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCreate = new System.Windows.Forms.Label();
            this.lblSched = new System.Windows.Forms.Label();
            this.fsp_Main = new COM.FSP();
            this.timerScroll = new System.Windows.Forms.Timer(this.components);
            this.timerChangeForm = new System.Windows.Forms.Timer(this.components);
            this.timerChangeTime = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fsp_Main)).BeginInit();
            this.SuspendLayout();
            // 
            // tmr_reload
            // 
            this.tmr_reload.Tick += new System.EventHandler(this.tmr_reload_Tick);
            // 
            // tmr_detail
            // 
            this.tmr_detail.Tick += new System.EventHandler(this.tmr_detail_Tick);
            // 
            // txttemp
            // 
            this.txttemp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txttemp.Location = new System.Drawing.Point(9, 817);
            this.txttemp.Name = "txttemp";
            this.txttemp.Size = new System.Drawing.Size(62, 13);
            this.txttemp.TabIndex = 41;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.lblCreate);
            this.splitContainer1.Panel1.Controls.Add(this.lblSched);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fsp_Main);
            this.splitContainer1.Size = new System.Drawing.Size(1900, 1054);
            this.splitContainer1.SplitterDistance = 103;
            this.splitContainer1.TabIndex = 42;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.lblPros);
            this.panel1.Controls.Add(this.lblComp);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1900, 103);
            this.panel1.TabIndex = 14;
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDate.Font = new System.Drawing.Font("Calibri", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(1665, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(235, 103);
            this.lblDate.TabIndex = 19;
            this.lblDate.Text = "label3";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblDate.Click += new System.EventHandler(this.lblDate_Click);
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Calibri", 68.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(523, 103);
            this.label10.TabIndex = 13;
            this.label10.Text = "OUTGOING";
            this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblPros
            // 
            this.lblPros.AutoSize = true;
            this.lblPros.BackColor = System.Drawing.Color.Transparent;
            this.lblPros.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPros.ForeColor = System.Drawing.Color.Yellow;
            this.lblPros.Location = new System.Drawing.Point(1291, 60);
            this.lblPros.Name = "lblPros";
            this.lblPros.Size = new System.Drawing.Size(174, 33);
            this.lblPros.TabIndex = 12;
            this.lblPros.Text = "YELLOW 0 ( 0 )";
            // 
            // lblComp
            // 
            this.lblComp.AutoSize = true;
            this.lblComp.BackColor = System.Drawing.Color.Transparent;
            this.lblComp.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComp.ForeColor = System.Drawing.Color.Lime;
            this.lblComp.Location = new System.Drawing.Point(937, 60);
            this.lblComp.Name = "lblComp";
            this.lblComp.Size = new System.Drawing.Size(159, 33);
            this.lblComp.TabIndex = 11;
            this.lblComp.Text = "GREEN 0 ( 0 )";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Location = new System.Drawing.Point(930, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(696, 100);
            this.panel2.TabIndex = 18;
            // 
            // lblCreate
            // 
            this.lblCreate.AutoSize = true;
            this.lblCreate.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreate.ForeColor = System.Drawing.Color.White;
            this.lblCreate.Location = new System.Drawing.Point(1480, 15);
            this.lblCreate.Name = "lblCreate";
            this.lblCreate.Size = new System.Drawing.Size(375, 33);
            this.lblCreate.TabIndex = 13;
            this.lblCreate.Text = "Create On : 20-09-2016 13:25:45";
            // 
            // lblSched
            // 
            this.lblSched.AutoSize = true;
            this.lblSched.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSched.ForeColor = System.Drawing.Color.White;
            this.lblSched.Location = new System.Drawing.Point(1481, 57);
            this.lblSched.Name = "lblSched";
            this.lblSched.Size = new System.Drawing.Size(128, 33);
            this.lblSched.TabIndex = 11;
            this.lblSched.Text = "RED 0 ( 0 )";
            // 
            // fsp_Main
            // 
            this.fsp_Main.BackColor = System.Drawing.Color.Transparent;
            this.fsp_Main.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.fsp_Main.ColumnInfo = "5,1,0,0,0,95,Columns:1{StyleFixed:\"Margins:1, 0, 0, 0;TextAlign:LeftCenter;TextEf" +
                "fect:Flat;TextDirection:Normal;Trimming:None;\";}\t";
            this.fsp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fsp_Main.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.fsp_Main.ForeColor = System.Drawing.Color.Black;
            this.fsp_Main.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.fsp_Main.Location = new System.Drawing.Point(0, 0);
            this.fsp_Main.Name = "fsp_Main";
            this.fsp_Main.Rows.DefaultSize = 19;
            this.fsp_Main.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.fsp_Main.Size = new System.Drawing.Size(1900, 947);
            this.fsp_Main.StyleInfo = resources.GetString("fsp_Main.StyleInfo");
            this.fsp_Main.TabIndex = 18;
            // 
            // timerScroll
            // 
            this.timerScroll.Interval = 5000;
            this.timerScroll.Tick += new System.EventHandler(this.timerScroll_Tick);
            // 
            // timerChangeForm
            // 
            this.timerChangeForm.Interval = 20000;
            this.timerChangeForm.Tick += new System.EventHandler(this.timerChangeForm_Tick);
            // 
            // timerChangeTime
            // 
            this.timerChangeTime.Interval = 1000;
            this.timerChangeTime.Tick += new System.EventHandler(this.timerChangeTime_Tick);
            // 
            // FRM_MAT_OUT_STATUS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1900, 1054);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.txttemp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRM_MAT_OUT_STATUS";
            this.Text = "FRM_MAT_OUT_STATUS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FRM_MAT_IN_STATUS_Closing);
            this.Load += new System.EventHandler(this.FRM_MAT_IN_STATUS_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fsp_Main)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmr_reload;
        private System.Windows.Forms.Timer tmr_detail;
        private System.Windows.Forms.TextBox txttemp;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer timerScroll;
        private System.Windows.Forms.Label lblCreate;
        private System.Windows.Forms.Label lblSched;
        private COM.FSP fsp_Main;
        private System.Windows.Forms.Timer timerChangeForm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPros;
        private System.Windows.Forms.Label lblComp;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Timer timerChangeTime;

    }
}