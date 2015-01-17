namespace ConvenienceFormClient
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textLog = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userscompleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.pricesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pricescompleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.keydatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.activityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.last10EntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sinceLastKeydateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.allTakesTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyChangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addKeydateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Event Log";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textLog);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 354);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 195);
            this.panel1.TabIndex = 1;
            // 
            // textLog
            // 
            this.textLog.BackColor = System.Drawing.SystemColors.Info;
            this.textLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.textLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textLog.Location = new System.Drawing.Point(0, 21);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.Size = new System.Drawing.Size(560, 174);
            this.textLog.TabIndex = 6;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.dataGridView1);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 30);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(563, 318);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(557, 288);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.showToolStripMenuItem,
            this.editToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.systemToolStripMenuItem.Text = "System";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usersToolStripMenuItem,
            this.userscompleteToolStripMenuItem,
            this.toolStripSeparator3,
            this.pricesToolStripMenuItem,
            this.pricescompleteToolStripMenuItem,
            this.toolStripSeparator4,
            this.keydatesToolStripMenuItem,
            this.toolStripSeparator5,
            this.activityToolStripMenuItem});
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.usersToolStripMenuItem.Text = "Users";
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.usersToolStripMenuItem_Click);
            // 
            // userscompleteToolStripMenuItem
            // 
            this.userscompleteToolStripMenuItem.Name = "userscompleteToolStripMenuItem";
            this.userscompleteToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.userscompleteToolStripMenuItem.Text = "Users (complete)";
            this.userscompleteToolStripMenuItem.Click += new System.EventHandler(this.userscompleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(163, 6);
            // 
            // pricesToolStripMenuItem
            // 
            this.pricesToolStripMenuItem.Name = "pricesToolStripMenuItem";
            this.pricesToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.pricesToolStripMenuItem.Text = "Prices";
            this.pricesToolStripMenuItem.Click += new System.EventHandler(this.pricesToolStripMenuItem_Click);
            // 
            // pricescompleteToolStripMenuItem
            // 
            this.pricescompleteToolStripMenuItem.Name = "pricescompleteToolStripMenuItem";
            this.pricescompleteToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.pricescompleteToolStripMenuItem.Text = "Prices (complete)";
            this.pricescompleteToolStripMenuItem.Click += new System.EventHandler(this.pricescompleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(163, 6);
            // 
            // keydatesToolStripMenuItem
            // 
            this.keydatesToolStripMenuItem.Name = "keydatesToolStripMenuItem";
            this.keydatesToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.keydatesToolStripMenuItem.Text = "Keydates";
            this.keydatesToolStripMenuItem.Click += new System.EventHandler(this.keydatesToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(163, 6);
            // 
            // activityToolStripMenuItem
            // 
            this.activityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.last10EntriesToolStripMenuItem,
            this.sinceLastKeydateToolStripMenuItem,
            this.toolStripSeparator2,
            this.allTakesTimeToolStripMenuItem});
            this.activityToolStripMenuItem.Name = "activityToolStripMenuItem";
            this.activityToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.activityToolStripMenuItem.Text = "Activity";
            // 
            // last10EntriesToolStripMenuItem
            // 
            this.last10EntriesToolStripMenuItem.Name = "last10EntriesToolStripMenuItem";
            this.last10EntriesToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.last10EntriesToolStripMenuItem.Text = "Last 10 Entries";
            this.last10EntriesToolStripMenuItem.Click += new System.EventHandler(this.last10EntriesToolStripMenuItem_Click);
            // 
            // sinceLastKeydateToolStripMenuItem
            // 
            this.sinceLastKeydateToolStripMenuItem.Name = "sinceLastKeydateToolStripMenuItem";
            this.sinceLastKeydateToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.sinceLastKeydateToolStripMenuItem.Text = "Since last Keydate";
            this.sinceLastKeydateToolStripMenuItem.Click += new System.EventHandler(this.sinceLastKeydateToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(165, 6);
            // 
            // allTakesTimeToolStripMenuItem
            // 
            this.allTakesTimeToolStripMenuItem.Name = "allTakesTimeToolStripMenuItem";
            this.allTakesTimeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.allTakesTimeToolStripMenuItem.Text = "All (Takes time!)";
            this.allTakesTimeToolStripMenuItem.Click += new System.EventHandler(this.allTakesTimeToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applyChangesToolStripMenuItem,
            this.addKeydateToolStripMenuItem});
            this.editToolStripMenuItem.Enabled = false;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // applyChangesToolStripMenuItem
            // 
            this.applyChangesToolStripMenuItem.Name = "applyChangesToolStripMenuItem";
            this.applyChangesToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.applyChangesToolStripMenuItem.Text = "Apply Changes";
            this.applyChangesToolStripMenuItem.Click += new System.EventHandler(this.applyChangesToolStripMenuItem_Click);
            // 
            // addKeydateToolStripMenuItem
            // 
            this.addKeydateToolStripMenuItem.Name = "addKeydateToolStripMenuItem";
            this.addKeydateToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.addKeydateToolStripMenuItem.Text = "Add Keydate";
            this.addKeydateToolStripMenuItem.Click += new System.EventHandler(this.addKeydateToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 600);
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "Form1";
            this.Text = "Convenience AdminClient";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pricesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keydatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applyChangesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem last10EntriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sinceLastKeydateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem allTakesTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userscompleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem pricescompleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem addKeydateToolStripMenuItem;
    }
}

