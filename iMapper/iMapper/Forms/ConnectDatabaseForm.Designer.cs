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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectDatabaseForm));
            this.LoadButton = new System.Windows.Forms.Button();
            this.ServerName = new System.Windows.Forms.TextBox();
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
            this.label5 = new System.Windows.Forms.Label();
            this.KDiff = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(794, 61);
            this.LoadButton.Margin = new System.Windows.Forms.Padding(4);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 28);
            this.LoadButton.TabIndex = 0;
            this.LoadButton.Text = "Load...";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // ServerName
            // 
            this.ServerName.Location = new System.Drawing.Point(16, 64);
            this.ServerName.Margin = new System.Windows.Forms.Padding(4);
            this.ServerName.Name = "ServerName";
            this.ServerName.Size = new System.Drawing.Size(217, 22);
            this.ServerName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(463, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Username";
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(467, 64);
            this.Username.Margin = new System.Windows.Forms.Padding(4);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(155, 22);
            this.Username.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(626, 44);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Password";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(630, 64);
            this.Password.Margin = new System.Windows.Forms.Padding(4);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(156, 22);
            this.Password.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TableGrid);
            this.groupBox2.Location = new System.Drawing.Point(17, 94);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(852, 273);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tables";
            // 
            // TableGrid
            // 
            this.TableGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableGrid.Location = new System.Drawing.Point(8, 23);
            this.TableGrid.Margin = new System.Windows.Forms.Padding(4);
            this.TableGrid.Name = "TableGrid";
            this.TableGrid.ReadOnly = true;
            this.TableGrid.Size = new System.Drawing.Size(836, 242);
            this.TableGrid.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 44);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Database Name";
            // 
            // DatabaseName
            // 
            this.DatabaseName.Location = new System.Drawing.Point(241, 64);
            this.DatabaseName.Margin = new System.Windows.Forms.Padding(4);
            this.DatabaseName.Name = "DatabaseName";
            this.DatabaseName.Size = new System.Drawing.Size(218, 22);
            this.DatabaseName.TabIndex = 8;
            // 
            // IsWindowsAuthentication
            // 
            this.IsWindowsAuthentication.AutoSize = true;
            this.IsWindowsAuthentication.Location = new System.Drawing.Point(13, 13);
            this.IsWindowsAuthentication.Margin = new System.Windows.Forms.Padding(4);
            this.IsWindowsAuthentication.Name = "IsWindowsAuthentication";
            this.IsWindowsAuthentication.Size = new System.Drawing.Size(168, 20);
            this.IsWindowsAuthentication.TabIndex = 10;
            this.IsWindowsAuthentication.Text = "Windows Authentication";
            this.IsWindowsAuthentication.UseVisualStyleBackColor = true;
            this.IsWindowsAuthentication.CheckedChanged += new System.EventHandler(this.IsWindowsAuthentication_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 378);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "KDiff3";
            // 
            // KDiff
            // 
            this.KDiff.Location = new System.Drawing.Point(64, 375);
            this.KDiff.Margin = new System.Windows.Forms.Padding(4);
            this.KDiff.Name = "KDiff";
            this.KDiff.Size = new System.Drawing.Size(723, 22);
            this.KDiff.TabIndex = 12;
            this.KDiff.Text = "C:\\Program Files\\KDiff3\\kdiff3.exe";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(794, 374);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Browse...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnLoadKDiff);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "kdiff3.exe";
            this.openFileDialog.InitialDirectory = "C:\\Program Files (x86)\\KDiff3";
            // 
            // ConnectDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 403);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.KDiff);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.IsWindowsAuthentication);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DatabaseName);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ServerName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ConnectDatabaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect Database";
            this.Load += new System.EventHandler(this.ConnectDatabaseForm_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.TextBox ServerName;
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox KDiff;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}