namespace iMapper.Forms
{
    partial class ValidationForm
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
            this.IsPascalize = new System.Windows.Forms.CheckBox();
            this.ColumnNumber = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.IsReplace = new System.Windows.Forms.CheckBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Options = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FileName = new System.Windows.Forms.TextBox();
            this.Columns = new System.Windows.Forms.DataGridView();
            this.Tables = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Columns)).BeginInit();
            this.SuspendLayout();
            // 
            // IsPascalize
            // 
            this.IsPascalize.AutoSize = true;
            this.IsPascalize.Checked = true;
            this.IsPascalize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IsPascalize.Location = new System.Drawing.Point(12, 446);
            this.IsPascalize.Name = "IsPascalize";
            this.IsPascalize.Size = new System.Drawing.Size(71, 17);
            this.IsPascalize.TabIndex = 26;
            this.IsPascalize.Text = "Pascalize";
            this.IsPascalize.UseVisualStyleBackColor = true;
            // 
            // ColumnNumber
            // 
            this.ColumnNumber.AutoSize = true;
            this.ColumnNumber.Location = new System.Drawing.Point(48, 97);
            this.ColumnNumber.Name = "ColumnNumber";
            this.ColumnNumber.Size = new System.Drawing.Size(56, 13);
            this.ColumnNumber.TabIndex = 25;
            this.ColumnNumber.Text = "0 Columns";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Found";
            // 
            // IsReplace
            // 
            this.IsReplace.AutoSize = true;
            this.IsReplace.Checked = true;
            this.IsReplace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IsReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.IsReplace.ForeColor = System.Drawing.Color.Red;
            this.IsReplace.Location = new System.Drawing.Point(12, 424);
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
            this.CloseButton.Location = new System.Drawing.Point(239, 412);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(88, 38);
            this.CloseButton.TabIndex = 22;
            this.CloseButton.Text = "Close";
            this.CloseButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(12, 367);
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
            this.Options.Location = new System.Drawing.Point(12, 386);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(316, 24);
            this.Options.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(9, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "File name";
            // 
            // FileName
            // 
            this.FileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FileName.Location = new System.Drawing.Point(12, 68);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(316, 22);
            this.FileName.TabIndex = 18;
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
            this.Columns.Location = new System.Drawing.Point(12, 117);
            this.Columns.Name = "Columns";
            this.Columns.Size = new System.Drawing.Size(315, 237);
            this.Columns.TabIndex = 17;
            // 
            // Tables
            // 
            this.Tables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tables.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Tables.FormattingEnabled = true;
            this.Tables.Location = new System.Drawing.Point(12, 24);
            this.Tables.Name = "Tables";
            this.Tables.Size = new System.Drawing.Size(316, 24);
            this.Tables.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Table";
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.SaveButton.Image = global::iMapper.Properties.Resources.Terminal;
            this.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveButton.Location = new System.Drawing.Point(143, 412);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(90, 38);
            this.SaveButton.TabIndex = 14;
            this.SaveButton.Text = "Save";
            this.SaveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // ValidationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 483);
            this.Controls.Add(this.IsPascalize);
            this.Controls.Add(this.ColumnNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IsReplace);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Options);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.Columns);
            this.Controls.Add(this.Tables);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ValidationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Validation";
            ((System.ComponentModel.ISupportInitialize)(this.Columns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox IsPascalize;
        private System.Windows.Forms.Label ColumnNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox IsReplace;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Options;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.DataGridView Columns;
        private System.Windows.Forms.ComboBox Tables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveButton;
    }
}