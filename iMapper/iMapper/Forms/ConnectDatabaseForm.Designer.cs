namespace iMapper.Forms
{
    partial class ConnectDatabaseForm
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
            this.LoadButton = new System.Windows.Forms.Button();
            this.ServerName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ConfigurationGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TableGrid = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.DatabaseName = new System.Windows.Forms.TextBox();
            this.IsWindowsAuthentication = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigurationGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(742, 29);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(132, 71);
            this.LoadButton.TabIndex = 0;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // ServerName
            // 
            this.ServerName.Location = new System.Drawing.Point(12, 29);
            this.ServerName.Name = "ServerName";
            this.ServerName.Size = new System.Drawing.Size(321, 20);
            this.ServerName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ConfigurationGrid);
            this.groupBox1.Location = new System.Drawing.Point(13, 334);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(867, 180);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // ConfigurationGrid
            // 
            this.ConfigurationGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConfigurationGrid.Location = new System.Drawing.Point(6, 19);
            this.ConfigurationGrid.Name = "ConfigurationGrid";
            this.ConfigurationGrid.ReadOnly = true;
            this.ConfigurationGrid.Size = new System.Drawing.Size(855, 150);
            this.ConfigurationGrid.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Username";
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(174, 76);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(298, 20);
            this.Username.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(475, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Password";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(478, 76);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(255, 20);
            this.Password.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TableGrid);
            this.groupBox2.Location = new System.Drawing.Point(13, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(867, 222);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tables";
            // 
            // TableGrid
            // 
            this.TableGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableGrid.Location = new System.Drawing.Point(6, 19);
            this.TableGrid.Name = "TableGrid";
            this.TableGrid.ReadOnly = true;
            this.TableGrid.Size = new System.Drawing.Size(855, 197);
            this.TableGrid.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(336, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Database Name";
            // 
            // DatabaseName
            // 
            this.DatabaseName.Location = new System.Drawing.Point(339, 29);
            this.DatabaseName.Name = "DatabaseName";
            this.DatabaseName.Size = new System.Drawing.Size(397, 20);
            this.DatabaseName.TabIndex = 8;
            // 
            // IsWindowsAuthentication
            // 
            this.IsWindowsAuthentication.AutoSize = true;
            this.IsWindowsAuthentication.Location = new System.Drawing.Point(12, 76);
            this.IsWindowsAuthentication.Name = "IsWindowsAuthentication";
            this.IsWindowsAuthentication.Size = new System.Drawing.Size(141, 17);
            this.IsWindowsAuthentication.TabIndex = 10;
            this.IsWindowsAuthentication.Text = "Windows Authentication";
            this.IsWindowsAuthentication.UseVisualStyleBackColor = true;
            this.IsWindowsAuthentication.CheckedChanged += new System.EventHandler(this.IsWindowsAuthentication_CheckedChanged);
            // 
            // ConnectDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 526);
            this.Controls.Add(this.IsWindowsAuthentication);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DatabaseName);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ServerName);
            this.Controls.Add(this.LoadButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ConnectDatabaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect Database";
            this.Load += new System.EventHandler(this.ConnectDatabaseForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ConfigurationGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.TextBox ServerName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView ConfigurationGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView TableGrid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DatabaseName;
        private System.Windows.Forms.CheckBox IsWindowsAuthentication;
    }
}