namespace alpairdotcomextractor
{
    partial class Form1
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
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            
            this.btnStartStop = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonCopyToClipboard = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            // Grid columns
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            
            //((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            
            
            // 
            // Static labels
            //
            // Microsoft developers : This kind of stuff MUST be in XML! 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(32, 25);
            this.labelUsername.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(60, 13);
            this.labelUsername.TabIndex = 2;
            this.labelUsername.Text = "User name";
            
            this.txtUsername.Location = new System.Drawing.Point(94, 25);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(60, 13);
            this.labelUsername.TabIndex = 3;
             
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(32, 80);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(60, 13);
            this.labelPassword.TabIndex = 4;
            this.labelPassword.Text = "Password";
            
            this.txtPassword.Location = new System.Drawing.Point(94, 80);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(60, 13);
            this.labelPassword.TabIndex = 5;
            
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(225, 81);
            this.btnStartStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(102, 19);
            this.btnStartStop.TabIndex = 4;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // buttonCopyToClipboard
            // 
            this.buttonCopyToClipboard.Location = new System.Drawing.Point(3, 3);
            this.buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            this.buttonCopyToClipboard.Size = new System.Drawing.Size(75, 23);
            this.buttonCopyToClipboard.TabIndex = 5;
            this.buttonCopyToClipboard.Text = "Select all";
            this.buttonCopyToClipboard.UseVisualStyleBackColor = true;
            this.buttonCopyToClipboard.Click += new System.EventHandler(this.buttonCopyToClipboard_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView);
            this.panel1.Controls.Add(this.buttonCopyToClipboard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(548, 219);
            this.panel1.TabIndex = 6;
            this.panel1.Visible = false;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.Rate});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView.Location = new System.Drawing.Point(0, 32);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(548, 187);
            this.dataGridView.TabIndex = 6;
            // 
            // Index column
            // 
            this.Index.HeaderText = "Index";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
                       
            // 
            // Rate column
            // 
            this.Price.HeaderText = "Rate";
            this.Price.Name = "Rate";
            this.Price.ReadOnly = true;
            
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 366);

            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnStartStop);
            //this.Controls.Add(this.label3);
            //this.Controls.Add(this.label2);
            //this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPassword;
        
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;

        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonCopyToClipboard;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;

    }
}

