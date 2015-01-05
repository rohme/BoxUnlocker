namespace BoxUnlocker
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cmbPol = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnExec = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTargets = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMumTryCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMumGameId = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkExecAbyssea = new System.Windows.Forms.CheckBox();
            this.chkExecMum = new System.Windows.Forms.CheckBox();
            this.chkExecField = new System.Windows.Forms.CheckBox();
            this.chkUseEnternity = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbPol
            // 
            this.cmbPol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPol.FormattingEnabled = true;
            this.cmbPol.Location = new System.Drawing.Point(3, 289);
            this.cmbPol.Name = "cmbPol";
            this.cmbPol.Size = new System.Drawing.Size(253, 20);
            this.cmbPol.TabIndex = 7;
            this.cmbPol.TabStop = false;
            this.cmbPol.SelectedIndexChanged += new System.EventHandler(this.cmbPol_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 313);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(263, 23);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.SystemColors.Control;
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(68, 18);
            this.lblMessage.Text = "メッセージ";
            // 
            // btnExec
            // 
            this.btnExec.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnExec.Location = new System.Drawing.Point(5, 249);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(251, 34);
            this.btnExec.TabIndex = 6;
            this.btnExec.Text = "開　　始";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "候補";
            // 
            // txtTargets
            // 
            this.txtTargets.Location = new System.Drawing.Point(5, 144);
            this.txtTargets.Multiline = true;
            this.txtTargets.Name = "txtTargets";
            this.txtTargets.ReadOnly = true;
            this.txtTargets.Size = new System.Drawing.Size(251, 79);
            this.txtTargets.TabIndex = 5;
            this.txtTargets.TabStop = false;
            this.txtTargets.Text = resources.GetString("txtTargets.Text");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMumTryCount);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbMumGameId);
            this.groupBox1.Location = new System.Drawing.Point(5, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 65);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MUM設定";
            // 
            // txtMumTryCount
            // 
            this.txtMumTryCount.Location = new System.Drawing.Point(40, 37);
            this.txtMumTryCount.MaxLength = 4;
            this.txtMumTryCount.Name = "txtMumTryCount";
            this.txtMumTryCount.Size = new System.Drawing.Size(31, 19);
            this.txtMumTryCount.TabIndex = 4;
            this.txtMumTryCount.Text = "9999";
            this.txtMumTryCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMumTryCount_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(77, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "(0で指定無し)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "回数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "ゲーム種類：";
            // 
            // cmbMumGameId
            // 
            this.cmbMumGameId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMumGameId.FormattingEnabled = true;
            this.cmbMumGameId.Location = new System.Drawing.Point(77, 15);
            this.cmbMumGameId.Name = "cmbMumGameId";
            this.cmbMumGameId.Size = new System.Drawing.Size(152, 20);
            this.cmbMumGameId.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkExecAbyssea);
            this.groupBox2.Controls.Add(this.chkExecMum);
            this.groupBox2.Controls.Add(this.chkExecField);
            this.groupBox2.Location = new System.Drawing.Point(5, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(251, 43);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "監視対象";
            // 
            // chkExecAbyssea
            // 
            this.chkExecAbyssea.AutoSize = true;
            this.chkExecAbyssea.Location = new System.Drawing.Point(138, 18);
            this.chkExecAbyssea.Name = "chkExecAbyssea";
            this.chkExecAbyssea.Size = new System.Drawing.Size(60, 16);
            this.chkExecAbyssea.TabIndex = 2;
            this.chkExecAbyssea.Text = "アビセア";
            this.chkExecAbyssea.UseVisualStyleBackColor = true;
            this.chkExecAbyssea.Visible = false;
            // 
            // chkExecMum
            // 
            this.chkExecMum.AutoSize = true;
            this.chkExecMum.Location = new System.Drawing.Point(82, 18);
            this.chkExecMum.Name = "chkExecMum";
            this.chkExecMum.Size = new System.Drawing.Size(50, 16);
            this.chkExecMum.TabIndex = 1;
            this.chkExecMum.Text = "MUM";
            this.chkExecMum.UseVisualStyleBackColor = true;
            // 
            // chkExecField
            // 
            this.chkExecField.AutoSize = true;
            this.chkExecField.Location = new System.Drawing.Point(8, 18);
            this.chkExecField.Name = "chkExecField";
            this.chkExecField.Size = new System.Drawing.Size(68, 16);
            this.chkExecField.TabIndex = 0;
            this.chkExecField.Text = "フィールド";
            this.chkExecField.UseVisualStyleBackColor = true;
            // 
            // chkUseEnternity
            // 
            this.chkUseEnternity.AutoSize = true;
            this.chkUseEnternity.Location = new System.Drawing.Point(5, 227);
            this.chkUseEnternity.Name = "chkUseEnternity";
            this.chkUseEnternity.Size = new System.Drawing.Size(139, 16);
            this.chkUseEnternity.TabIndex = 8;
            this.chkUseEnternity.Text = "enternityを使用している";
            this.chkUseEnternity.UseVisualStyleBackColor = true;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "BoxUnlocker";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(263, 336);
            this.Controls.Add(this.chkUseEnternity);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtTargets);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmbPol);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "BoxUnlocker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ClientSizeChanged += new System.EventHandler(this.frmMain_ClientSizeChanged);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPol;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.ToolStripStatusLabel lblMessage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTargets;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMumTryCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMumGameId;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkExecField;
        private System.Windows.Forms.CheckBox chkExecAbyssea;
        private System.Windows.Forms.CheckBox chkExecMum;
        private System.Windows.Forms.CheckBox chkUseEnternity;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

