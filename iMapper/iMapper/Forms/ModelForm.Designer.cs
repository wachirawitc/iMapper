namespace iMapper.Forms
{
    partial class ModelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelForm));
            this.label1 = new System.Windows.Forms.Label();
            this.Tables = new System.Windows.Forms.ComboBox();
            this.Columns = new System.Windows.Forms.DataGridView();
            this.FileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Options = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.IsReplace = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ColumnNumber = new System.Windows.Forms.Label();
            this.IsPascalize = new System.Windows.Forms.CheckBox();
            this.SaveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Columns)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Table";
            // 
            // Tables
            // 
            this.Tables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tables.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tables.FormattingEnabled = true;
            this.Tables.Location = new System.Drawing.Point(13, 34);
            this.Tables.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tables.Name = "Tables";
            this.Tables.Size = new System.Drawing.Size(300, 25);
            this.Tables.TabIndex = 2;
            this.Tables.SelectedIndexChanged += new System.EventHandler(this.OnSelectedTables);
            // 
            // Columns
            // 
            this.Columns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Columns.Location = new System.Drawing.Point(321, 34);
            this.Columns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Columns.Name = "Columns";
            this.Columns.ReadOnly = true;
            this.Columns.Size = new System.Drawing.Size(400, 220);
            this.Columns.TabIndex = 3;
            // 
            // FileName
            // 
            this.FileName.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileName.Location = new System.Drawing.Point(13, 84);
            this.FileName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(300, 25);
            this.FileName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "File name";
            // 
            // Options
            // 
            this.Options.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Options.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Options.FormattingEnabled = true;
            this.Options.Location = new System.Drawing.Point(13, 134);
            this.Options.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(300, 25);
            this.Options.TabIndex = 7;
            this.Options.SelectedIndexChanged += new System.EventHandler(this.OnSelectedOptions);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 113);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Option";
            // 
            // IsReplace
            // 
            this.IsReplace.AutoSize = true;
            this.IsReplace.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsReplace.ForeColor = System.Drawing.Color.Red;
            this.IsReplace.Location = new System.Drawing.Point(12, 165);
            this.IsReplace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IsReplace.Name = "IsReplace";
            this.IsReplace.Size = new System.Drawing.Size(126, 21);
            this.IsReplace.TabIndex = 10;
            this.IsReplace.Text = "Replace if existing";
            this.IsReplace.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(318, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Found";
            // 
            // ColumnNumber
            // 
            this.ColumnNumber.AutoSize = true;
            this.ColumnNumber.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnNumber.Location = new System.Drawing.Point(368, 9);
            this.ColumnNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ColumnNumber.Name = "ColumnNumber";
            this.ColumnNumber.Size = new System.Drawing.Size(67, 17);
            this.ColumnNumber.TabIndex = 12;
            this.ColumnNumber.Text = "0 Columns";
            // 
            // IsPascalize
            // 
            this.IsPascalize.AutoSize = true;
            this.IsPascalize.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsPascalize.Location = new System.Drawing.Point(12, 192);
            this.IsPascalize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.IsPascalize.Name = "IsPascalize";
            this.IsPascalize.Size = new System.Drawing.Size(75, 21);
            this.IsPascalize.TabIndex = 13;
            this.IsPascalize.Text = "Pascalize";
            this.IsPascalize.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveButton.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.Location = new System.Drawing.Point(521, 262);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(200, 34);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.OnClickSaveButton);
            // 
            // ModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 303);
            this.Controls.Add(this.IsPascalize);
            this.Controls.Add(this.ColumnNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IsReplace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Options);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.Columns);
            this.Controls.Add(this.Tables);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ModelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Model";
            this.Load += new System.EventHandler(this.OnLoadModelForm);
            ((System.ComponentModel.ISupportInitialize)(this.Columns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Tables;
        private System.Windows.Forms.DataGridView Columns;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Options;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox IsReplace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label ColumnNumber;
        private System.Windows.Forms.CheckBox IsPascalize;
    }
}