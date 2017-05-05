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
            this.label1 = new System.Windows.Forms.Label();
            this.Tables = new System.Windows.Forms.ComboBox();
            this.Columns = new System.Windows.Forms.DataGridView();
            this.FileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Options = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Columns)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Table";
            // 
            // Tables
            // 
            this.Tables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tables.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Tables.FormattingEnabled = true;
            this.Tables.Location = new System.Drawing.Point(21, 37);
            this.Tables.Margin = new System.Windows.Forms.Padding(4);
            this.Tables.Name = "Tables";
            this.Tables.Size = new System.Drawing.Size(420, 24);
            this.Tables.TabIndex = 2;
            this.Tables.SelectedIndexChanged += new System.EventHandler(this.Tables_SelectedIndexChanged);
            // 
            // Columns
            // 
            this.Columns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Columns.Location = new System.Drawing.Point(21, 126);
            this.Columns.Margin = new System.Windows.Forms.Padding(4);
            this.Columns.Name = "Columns";
            this.Columns.Size = new System.Drawing.Size(420, 318);
            this.Columns.TabIndex = 3;
            // 
            // FileName
            // 
            this.FileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FileName.Location = new System.Drawing.Point(21, 91);
            this.FileName.Margin = new System.Windows.Forms.Padding(4);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(420, 22);
            this.FileName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(17, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "File name";
            // 
            // Options
            // 
            this.Options.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Options.FormattingEnabled = true;
            this.Options.Location = new System.Drawing.Point(21, 482);
            this.Options.Margin = new System.Windows.Forms.Padding(4);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(420, 24);
            this.Options.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(21, 459);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Option";
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.CloseButton.Image = global::iMapper.Properties.Resources.Close;
            this.CloseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CloseButton.Location = new System.Drawing.Point(324, 514);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(4);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(117, 33);
            this.CloseButton.TabIndex = 9;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.SaveButton.Image = global::iMapper.Properties.Resources.Terminal;
            this.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveButton.Location = new System.Drawing.Point(196, 514);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(4);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(120, 33);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // MapViewModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 554);
            this.Controls.Add(this.CloseButton);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MapViewModelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map View Model";
            this.Load += new System.EventHandler(this.MapViewModelForm_Load);
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
        private System.Windows.Forms.Button CloseButton;
    }
}