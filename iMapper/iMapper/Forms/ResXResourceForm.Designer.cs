namespace iMapper.Forms
{
    partial class ResXResourceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResXResourceForm));
            this.label1 = new System.Windows.Forms.Label();
            this.Tables = new System.Windows.Forms.ComboBox();
            this.resourceGrid = new System.Windows.Forms.DataGridView();
            this.saveButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ResourceFileName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.resourceGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Table";
            // 
            // Tables
            // 
            this.Tables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tables.FormattingEnabled = true;
            this.Tables.Location = new System.Drawing.Point(16, 34);
            this.Tables.Name = "Tables";
            this.Tables.Size = new System.Drawing.Size(472, 25);
            this.Tables.TabIndex = 1;
            this.Tables.SelectedIndexChanged += new System.EventHandler(this.OnSelectedTableChanged);
            // 
            // resourceGrid
            // 
            this.resourceGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resourceGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resourceGrid.Location = new System.Drawing.Point(16, 95);
            this.resourceGrid.Name = "resourceGrid";
            this.resourceGrid.Size = new System.Drawing.Size(472, 227);
            this.resourceGrid.TabIndex = 2;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(288, 328);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(200, 34);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.OnClickSaveButton);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "File ";
            // 
            // ResourceFileName
            // 
            this.ResourceFileName.AutoSize = true;
            this.ResourceFileName.Location = new System.Drawing.Point(54, 75);
            this.ResourceFileName.Name = "ResourceFileName";
            this.ResourceFileName.Size = new System.Drawing.Size(30, 17);
            this.ResourceFileName.TabIndex = 5;
            this.ResourceFileName.Text = "N/A";
            this.ResourceFileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ResXResourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 372);
            this.Controls.Add(this.ResourceFileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.resourceGrid);
            this.Controls.Add(this.Tables);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ResXResourceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ResX Resource";
            this.Load += new System.EventHandler(this.OnLoadResXResourceForm);
            ((System.ComponentModel.ISupportInitialize)(this.resourceGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Tables;
        private System.Windows.Forms.DataGridView resourceGrid;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ResourceFileName;
    }
}