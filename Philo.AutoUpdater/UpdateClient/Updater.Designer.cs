namespace UpdateClient
{
    partial class Updater
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtUpdateDetail = new System.Windows.Forms.TextBox();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.lblNewVersion = new System.Windows.Forms.Label();
            this.dgvUpdateFileList = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblCurrentFile = new System.Windows.Forms.Label();
            this.pgbCurrentFile = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdateFileList)).BeginInit();
            this.panel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtUpdateDetail);
            this.panel1.Controls.Add(this.lblCurrentVersion);
            this.panel1.Controls.Add(this.lblNewVersion);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 109);
            this.panel1.TabIndex = 0;
            // 
            // txtUpdateDetail
            // 
            this.txtUpdateDetail.Location = new System.Drawing.Point(33, 28);
            this.txtUpdateDetail.Multiline = true;
            this.txtUpdateDetail.Name = "txtUpdateDetail";
            this.txtUpdateDetail.ReadOnly = true;
            this.txtUpdateDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUpdateDetail.Size = new System.Drawing.Size(738, 72);
            this.txtUpdateDetail.TabIndex = 2;
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Location = new System.Drawing.Point(403, 13);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(113, 12);
            this.lblCurrentVersion.TabIndex = 1;
            this.lblCurrentVersion.Text = "未能读取本地版本号";
            // 
            // lblNewVersion
            // 
            this.lblNewVersion.AutoSize = true;
            this.lblNewVersion.Location = new System.Drawing.Point(31, 13);
            this.lblNewVersion.Name = "lblNewVersion";
            this.lblNewVersion.Size = new System.Drawing.Size(125, 12);
            this.lblNewVersion.TabIndex = 0;
            this.lblNewVersion.Text = "未能读取服务器版本号";
            // 
            // dgvUpdateFileList
            // 
            this.dgvUpdateFileList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUpdateFileList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvUpdateFileList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUpdateFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUpdateFileList.Location = new System.Drawing.Point(0, 109);
            this.dgvUpdateFileList.Name = "dgvUpdateFileList";
            this.dgvUpdateFileList.ReadOnly = true;
            this.dgvUpdateFileList.RowTemplate.Height = 23;
            this.dgvUpdateFileList.Size = new System.Drawing.Size(784, 370);
            this.dgvUpdateFileList.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnExit);
            this.panel3.Controls.Add(this.btnUpdate);
            this.panel3.Controls.Add(this.lblCurrentFile);
            this.panel3.Controls.Add(this.pgbCurrentFile);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 479);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(784, 58);
            this.panel3.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(692, 30);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "退 出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(692, 5);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "开始更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblCurrentFile
            // 
            this.lblCurrentFile.AutoSize = true;
            this.lblCurrentFile.Location = new System.Drawing.Point(12, 35);
            this.lblCurrentFile.Name = "lblCurrentFile";
            this.lblCurrentFile.Size = new System.Drawing.Size(53, 12);
            this.lblCurrentFile.TabIndex = 1;
            this.lblCurrentFile.Text = "准备就绪";
            // 
            // pgbCurrentFile
            // 
            this.pgbCurrentFile.Location = new System.Drawing.Point(13, 4);
            this.pgbCurrentFile.Name = "pgbCurrentFile";
            this.pgbCurrentFile.Size = new System.Drawing.Size(654, 23);
            this.pgbCurrentFile.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(44, 21);
            this.tsmiFile.Text = "文件";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvUpdateFileList);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 25);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(784, 537);
            this.panel4.TabIndex = 1;
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Updater";
            this.Text = "自动升级";
            this.Load += new System.EventHandler(this.Updater_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdateFileList)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtUpdateDetail;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Label lblNewVersion;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblCurrentFile;
        private System.Windows.Forms.ProgressBar pgbCurrentFile;
        private System.Windows.Forms.DataGridView dgvUpdateFileList;
    }
}