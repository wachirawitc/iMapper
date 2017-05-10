namespace iMapper.Forms
{
    partial class ServiceForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceForm));
            this.SaveImplementButton = new System.Windows.Forms.Button();
            this.IsPluralize = new System.Windows.Forms.CheckBox();
            this.ColumnNumber = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.IsReplace = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Options = new System.Windows.Forms.ComboBox();
            this.Columns = new System.Windows.Forms.DataGridView();
            this.Tables = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveInterfaceButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Columns)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveImplementButton
            // 
            this.SaveImplementButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveImplementButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.SaveImplementButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveImplementButton.Location = new System.Drawing.Point(811, 217);
            this.SaveImplementButton.Name = "SaveImplementButton";
            this.SaveImplementButton.Size = new System.Drawing.Size(160, 38);
            this.SaveImplementButton.TabIndex = 45;
            this.SaveImplementButton.Text = "Implement";
            this.SaveImplementButton.UseVisualStyleBackColor = true;
            this.SaveImplementButton.Click += new System.EventHandler(this.OnClickSaveImplement);
            // 
            // IsPluralize
            // 
            this.IsPluralize.AutoSize = true;
            this.IsPluralize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.IsPluralize.Location = new System.Drawing.Point(214, 140);
            this.IsPluralize.Name = "IsPluralize";
            this.IsPluralize.Size = new System.Drawing.Size(100, 28);
            this.IsPluralize.TabIndex = 44;
            this.IsPluralize.Text = "Pluralize";
            this.IsPluralize.UseVisualStyleBackColor = true;
            // 
            // ColumnNumber
            // 
            this.ColumnNumber.AutoSize = true;
            this.ColumnNumber.Location = new System.Drawing.Point(457, 9);
            this.ColumnNumber.Name = "ColumnNumber";
            this.ColumnNumber.Size = new System.Drawing.Size(56, 13);
            this.ColumnNumber.TabIndex = 41;
            this.ColumnNumber.Text = "0 Columns";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(421, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Found";
            // 
            // IsReplace
            // 
            this.IsReplace.AutoSize = true;
            this.IsReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.IsReplace.ForeColor = System.Drawing.Color.Red;
            this.IsReplace.Location = new System.Drawing.Point(14, 140);
            this.IsReplace.Margin = new System.Windows.Forms.Padding(2);
            this.IsReplace.Name = "IsReplace";
            this.IsReplace.Size = new System.Drawing.Size(181, 28);
            this.IsReplace.TabIndex = 39;
            this.IsReplace.Text = "Replace if existing";
            this.IsReplace.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(14, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 16);
            this.label3.TabIndex = 37;
            this.label3.Text = "Option";
            // 
            // Options
            // 
            this.Options.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Options.FormattingEnabled = true;
            this.Options.Location = new System.Drawing.Point(14, 92);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(404, 32);
            this.Options.TabIndex = 36;
            // 
            // Columns
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Columns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Columns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Columns.DefaultCellStyle = dataGridViewCellStyle2;
            this.Columns.Location = new System.Drawing.Point(424, 25);
            this.Columns.Name = "Columns";
            this.Columns.ReadOnly = true;
            this.Columns.Size = new System.Drawing.Size(547, 186);
            this.Columns.TabIndex = 35;
            // 
            // Tables
            // 
            this.Tables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tables.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Tables.FormattingEnabled = true;
            this.Tables.Location = new System.Drawing.Point(17, 25);
            this.Tables.Name = "Tables";
            this.Tables.Size = new System.Drawing.Size(401, 32);
            this.Tables.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 33;
            this.label1.Text = "Table";
            // 
            // SaveInterfaceButton
            // 
            this.SaveInterfaceButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveInterfaceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.SaveInterfaceButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveInterfaceButton.Location = new System.Drawing.Point(645, 217);
            this.SaveInterfaceButton.Name = "SaveInterfaceButton";
            this.SaveInterfaceButton.Size = new System.Drawing.Size(160, 38);
            this.SaveInterfaceButton.TabIndex = 32;
            this.SaveInterfaceButton.Text = "Interface";
            this.SaveInterfaceButton.UseVisualStyleBackColor = true;
            this.SaveInterfaceButton.Click += new System.EventHandler(this.OnClickSaveInterface);
            // 
            // ServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 275);
            this.Controls.Add(this.SaveImplementButton);
            this.Controls.Add(this.IsPluralize);
            this.Controls.Add(this.ColumnNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IsReplace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Options);
            this.Controls.Add(this.Columns);
            this.Controls.Add(this.Tables);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveInterfaceButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service";
            this.Load += new System.EventHandler(this.OnLoadServiceForm);
            ((System.ComponentModel.ISupportInitialize)(this.Columns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveImplementButton;
        private System.Windows.Forms.CheckBox IsPluralize;
        private System.Windows.Forms.Label ColumnNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox IsReplace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Options;
        private System.Windows.Forms.DataGridView Columns;
        private System.Windows.Forms.ComboBox Tables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveInterfaceButton;
    }
}