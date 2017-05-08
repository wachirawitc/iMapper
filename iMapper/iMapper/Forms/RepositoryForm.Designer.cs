namespace iMapper.Forms
{
    partial class RepositoryForm
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
            this.ColumnNumber = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.IsReplace = new System.Windows.Forms.CheckBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Options = new System.Windows.Forms.ComboBox();
            this.Columns = new System.Windows.Forms.DataGridView();
            this.Tables = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveInterfaceButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.EntityFrameworkName = new System.Windows.Forms.TextBox();
            this.IsPluralize = new System.Windows.Forms.CheckBox();
            this.SaveImplementButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Columns)).BeginInit();
            this.SuspendLayout();
            // 
            // ColumnNumber
            // 
            this.ColumnNumber.AutoSize = true;
            this.ColumnNumber.Location = new System.Drawing.Point(50, 57);
            this.ColumnNumber.Name = "ColumnNumber";
            this.ColumnNumber.Size = new System.Drawing.Size(56, 13);
            this.ColumnNumber.TabIndex = 25;
            this.ColumnNumber.Text = "0 Columns";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Found";
            // 
            // IsReplace
            // 
            this.IsReplace.AutoSize = true;
            this.IsReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.IsReplace.ForeColor = System.Drawing.Color.Red;
            this.IsReplace.Location = new System.Drawing.Point(14, 453);
            this.IsReplace.Margin = new System.Windows.Forms.Padding(2);
            this.IsReplace.Name = "IsReplace";
            this.IsReplace.Size = new System.Drawing.Size(131, 17);
            this.IsReplace.TabIndex = 23;
            this.IsReplace.Text = "Replace if existing";
            this.IsReplace.UseVisualStyleBackColor = true;
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.CloseButton.Image = global::iMapper.Properties.Resources.Close;
            this.CloseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CloseButton.Location = new System.Drawing.Point(345, 485);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(78, 38);
            this.CloseButton.TabIndex = 22;
            this.CloseButton.Text = "Close";
            this.CloseButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.OnClickClose);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(14, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 16);
            this.label3.TabIndex = 21;
            this.label3.Text = "Option";
            // 
            // Options
            // 
            this.Options.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Options.FormattingEnabled = true;
            this.Options.Location = new System.Drawing.Point(14, 346);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(409, 24);
            this.Options.TabIndex = 20;
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
            this.Columns.Location = new System.Drawing.Point(0, 77);
            this.Columns.Name = "Columns";
            this.Columns.ReadOnly = true;
            this.Columns.Size = new System.Drawing.Size(435, 237);
            this.Columns.TabIndex = 17;
            // 
            // Tables
            // 
            this.Tables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tables.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Tables.FormattingEnabled = true;
            this.Tables.Location = new System.Drawing.Point(12, 21);
            this.Tables.Name = "Tables";
            this.Tables.Size = new System.Drawing.Size(417, 24);
            this.Tables.TabIndex = 16;
            this.Tables.SelectedIndexChanged += new System.EventHandler(this.OnSelectedTables);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(9, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Table";
            // 
            // SaveInterfaceButton
            // 
            this.SaveInterfaceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.SaveInterfaceButton.Image = global::iMapper.Properties.Resources.command;
            this.SaveInterfaceButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveInterfaceButton.Location = new System.Drawing.Point(9, 485);
            this.SaveInterfaceButton.Name = "SaveInterfaceButton";
            this.SaveInterfaceButton.Size = new System.Drawing.Size(97, 38);
            this.SaveInterfaceButton.TabIndex = 14;
            this.SaveInterfaceButton.Text = "Interface";
            this.SaveInterfaceButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveInterfaceButton.UseVisualStyleBackColor = true;
            this.SaveInterfaceButton.Click += new System.EventHandler(this.OnClickInterfaceSave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(11, 376);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(163, 16);
            this.label5.TabIndex = 27;
            this.label5.Text = "Entity framework name";
            // 
            // EntityFrameworkName
            // 
            this.EntityFrameworkName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.EntityFrameworkName.Location = new System.Drawing.Point(14, 392);
            this.EntityFrameworkName.Name = "EntityFrameworkName";
            this.EntityFrameworkName.Size = new System.Drawing.Size(409, 22);
            this.EntityFrameworkName.TabIndex = 26;
            // 
            // IsPluralize
            // 
            this.IsPluralize.AutoSize = true;
            this.IsPluralize.Location = new System.Drawing.Point(14, 420);
            this.IsPluralize.Name = "IsPluralize";
            this.IsPluralize.Size = new System.Drawing.Size(65, 17);
            this.IsPluralize.TabIndex = 30;
            this.IsPluralize.Text = "Pluralize";
            this.IsPluralize.UseVisualStyleBackColor = true;
            // 
            // SaveImplementButton
            // 
            this.SaveImplementButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.SaveImplementButton.Image = global::iMapper.Properties.Resources.command;
            this.SaveImplementButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveImplementButton.Location = new System.Drawing.Point(112, 485);
            this.SaveImplementButton.Name = "SaveImplementButton";
            this.SaveImplementButton.Size = new System.Drawing.Size(112, 38);
            this.SaveImplementButton.TabIndex = 31;
            this.SaveImplementButton.Text = "Implement";
            this.SaveImplementButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveImplementButton.UseVisualStyleBackColor = true;
            this.SaveImplementButton.Click += new System.EventHandler(this.OnClickImplementSave);
            // 
            // RepositoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 535);
            this.Controls.Add(this.SaveImplementButton);
            this.Controls.Add(this.IsPluralize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.EntityFrameworkName);
            this.Controls.Add(this.ColumnNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IsReplace);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Options);
            this.Controls.Add(this.Columns);
            this.Controls.Add(this.Tables);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveInterfaceButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RepositoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Repository";
            this.Load += new System.EventHandler(this.OnLoadRepositoryForm);
            ((System.ComponentModel.ISupportInitialize)(this.Columns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ColumnNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox IsReplace;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Options;
        private System.Windows.Forms.DataGridView Columns;
        private System.Windows.Forms.ComboBox Tables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveInterfaceButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox EntityFrameworkName;
        private System.Windows.Forms.CheckBox IsPluralize;
        private System.Windows.Forms.Button SaveImplementButton;
    }
}