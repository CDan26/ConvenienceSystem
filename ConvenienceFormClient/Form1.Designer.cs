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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textLog = new System.Windows.Forms.TextBox();
            this.button_close = new System.Windows.Forms.Button();
            this.button_prices = new System.Windows.Forms.Button();
            this.button_users = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_connect = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Menue";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textLog);
            this.panel1.Controls.Add(this.button_close);
            this.panel1.Controls.Add(this.button_prices);
            this.panel1.Controls.Add(this.button_users);
            this.panel1.Controls.Add(this.button_update);
            this.panel1.Controls.Add(this.button_connect);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(146, 432);
            this.panel1.TabIndex = 1;
            // 
            // textLog
            // 
            this.textLog.BackColor = System.Drawing.SystemColors.Info;
            this.textLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.textLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textLog.Location = new System.Drawing.Point(0, 258);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.Size = new System.Drawing.Size(146, 174);
            this.textLog.TabIndex = 6;
            // 
            // button_close
            // 
            this.button_close.Enabled = false;
            this.button_close.Location = new System.Drawing.Point(3, 159);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(140, 23);
            this.button_close.TabIndex = 5;
            this.button_close.Text = "Close Connection";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_prices
            // 
            this.button_prices.Enabled = false;
            this.button_prices.Location = new System.Drawing.Point(3, 130);
            this.button_prices.Name = "button_prices";
            this.button_prices.Size = new System.Drawing.Size(140, 23);
            this.button_prices.TabIndex = 4;
            this.button_prices.Text = "Show Prices";
            this.button_prices.UseVisualStyleBackColor = true;
            this.button_prices.Click += new System.EventHandler(this.button_prices_Click);
            // 
            // button_users
            // 
            this.button_users.Enabled = false;
            this.button_users.Location = new System.Drawing.Point(3, 101);
            this.button_users.Name = "button_users";
            this.button_users.Size = new System.Drawing.Size(140, 23);
            this.button_users.TabIndex = 3;
            this.button_users.Text = "Show Users";
            this.button_users.UseVisualStyleBackColor = true;
            this.button_users.Click += new System.EventHandler(this.button_users_Click);
            // 
            // button_update
            // 
            this.button_update.Enabled = false;
            this.button_update.Location = new System.Drawing.Point(3, 72);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(140, 23);
            this.button_update.TabIndex = 2;
            this.button_update.Text = "Update (S and C)";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(3, 43);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(140, 23);
            this.button_connect.TabIndex = 1;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.dataGridView1);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(165, 13);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(363, 431);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(360, 229);
            this.dataGridView1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 456);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Getraenkekassen-AdminClient";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Button button_prices;
        private System.Windows.Forms.Button button_users;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

