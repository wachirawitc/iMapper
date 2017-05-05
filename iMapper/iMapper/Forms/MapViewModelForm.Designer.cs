namespace iMapper.Forms
{
    partial class MapViewModelForm
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
            this.SaveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Tables = new System.Windows.Forms.ComboBox();
            this.Columns = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.IsFluentValidation = new System.Windows.Forms.CheckBox();
            this.IsAspNetMvc = new System.Windows.Forms.CheckBox();
            this.FileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Columns)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(308, 364);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(90, 27);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Table";
            // 
            // Tables
            // 
            this.Tables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tables.FormattingEnabled = true;
            this.Tables.Location = new System.Drawing.Point(16, 30);
            this.Tables.Name = "Tables";
            this.Tables.Size = new System.Drawing.Size(382, 21);
            this.Tables.TabIndex = 2;
            this.Tables.SelectedIndexChanged += new System.EventHandler(this.Tables_SelectedIndexChanged);
            // 
            // Columns
            // 
            this.Columns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Columns.Location = new System.Drawing.Point(16, 100);
            this.Columns.Name = "Columns";
            this.Columns.Size = new System.Drawing.Size(382, 180);
            this.Columns.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.IsFluentValidation);
            this.groupBox1.Controls.Add(this.IsAspNetMvc);
            this.groupBox1.Location = new System.Drawing.Point(16, 287);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 71);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Option";
            // 
            // IsFluentValidation
            // 
            this.IsFluentValidation.AutoSize = true;
            this.IsFluentValidation.Location = new System.Drawing.Point(7, 44);
            this.IsFluentValidation.Name = "IsFluentValidation";
            this.IsFluentValidation.Size = new System.Drawing.Size(101, 17);
            this.IsFluentValidation.TabIndex = 1;
            this.IsFluentValidation.Text = "FluentValidation";
            this.IsFluentValidation.UseVisualStyleBackColor = true;
            // 
            // IsAspNetMvc
            // 
            this.IsAspNetMvc.AutoSize = true;
            this.IsAspNetMvc.Location = new System.Drawing.Point(7, 20);
            this.IsAspNetMvc.Name = "IsAspNetMvc";
            this.IsAspNetMvc.Size = new System.Drawing.Size(147, 17);
            this.IsAspNetMvc.TabIndex = 0;
            this.IsAspNetMvc.Text = "ASP.NET MVC Validation";
            this.IsAspNetMvc.UseVisualStyleBackColor = true;
            // 
            // FileName
            // 
            this.FileName.Location = new System.Drawing.Point(16, 74);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(382, 20);
            this.FileName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "File name";
            // 
            // MapViewModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 404);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Columns);
            this.Controls.Add(this.Tables);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MapViewModelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map View Model";
            this.Load += new System.EventHandler(this.MapViewModelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Columns)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Tables;
        private System.Windows.Forms.DataGridView Columns;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox IsFluentValidation;
        private System.Windows.Forms.CheckBox IsAspNetMvc;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.Label label2;
    }
}