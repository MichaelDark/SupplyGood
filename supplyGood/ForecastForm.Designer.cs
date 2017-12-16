namespace supplyGood
{
    partial class ForecastForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForecastForm));
            this.cbxOptions = new System.Windows.Forms.ComboBox();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.lblCaption = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureMain = new System.Windows.Forms.PictureBox();
            this.btnReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMain)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxOptions
            // 
            this.cbxOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOptions.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxOptions.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbxOptions.FormattingEnabled = true;
            this.cbxOptions.Location = new System.Drawing.Point(812, 10);
            this.cbxOptions.Name = "cbxOptions";
            this.cbxOptions.Size = new System.Drawing.Size(270, 29);
            this.cbxOptions.TabIndex = 5;
            this.cbxOptions.SelectedIndexChanged += new System.EventHandler(this.CbxOptions_SelectedIndexChanged);
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AllowUserToResizeRows = false;
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMain.BackgroundColor = System.Drawing.Color.White;
            this.dgvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMain.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Location = new System.Drawing.Point(12, 45);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersVisible = false;
            this.dgvMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMain.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMain.ShowCellToolTips = false;
            this.dgvMain.Size = new System.Drawing.Size(1070, 559);
            this.dgvMain.TabIndex = 4;
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCaption.ForeColor = System.Drawing.Color.Black;
            this.lblCaption.Location = new System.Drawing.Point(47, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(214, 37);
            this.lblCaption.TabIndex = 8;
            this.lblCaption.Text = "Прогноз товара";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(587, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Анализировать данные:";
            // 
            // pictureMain
            // 
            this.pictureMain.Image = global::supplyGood.Properties.Resources.forecast;
            this.pictureMain.InitialImage = global::supplyGood.Properties.Resources.forecast;
            this.pictureMain.Location = new System.Drawing.Point(12, 5);
            this.pictureMain.Name = "pictureMain";
            this.pictureMain.Size = new System.Drawing.Size(34, 34);
            this.pictureMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureMain.TabIndex = 57;
            this.pictureMain.TabStop = false;
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.Gainsboro;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReport.Location = new System.Drawing.Point(366, 4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(215, 35);
            this.btnReport.TabIndex = 58;
            this.btnReport.Text = "Составить запрос на поставку";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // ForecastForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1094, 616);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.pictureMain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.cbxOptions);
            this.Controls.Add(this.dgvMain);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ForecastForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Прогноз товара";
            this.Load += new System.EventHandler(this.PredictForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbxOptions;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureMain;
        private System.Windows.Forms.Button btnReport;
    }
}