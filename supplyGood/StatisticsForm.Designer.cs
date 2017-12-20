namespace supplyGood
{
    partial class StatisticsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.goodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goodSellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goodProfitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сотрудникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.employmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заказчикToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientSumSupplyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.cbxOptions = new System.Windows.Forms.ComboBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.btnReport = new System.Windows.Forms.Button();
            this.pictureMain = new System.Windows.Forms.PictureBox();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMain)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mainMenuStrip.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainMenuStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goodToolStripMenuItem,
            this.сотрудникиToolStripMenuItem,
            this.заказчикToolStripMenuItem});
            this.mainMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mainMenuStrip.Size = new System.Drawing.Size(784, 33);
            this.mainMenuStrip.TabIndex = 0;
            // 
            // goodToolStripMenuItem
            // 
            this.goodToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goodSellToolStripMenuItem,
            this.goodProfitToolStripMenuItem});
            this.goodToolStripMenuItem.Image = global::supplyGood.Properties.Resources.good;
            this.goodToolStripMenuItem.Name = "goodToolStripMenuItem";
            this.goodToolStripMenuItem.Size = new System.Drawing.Size(92, 29);
            this.goodToolStripMenuItem.Text = "Товар";
            // 
            // goodSellToolStripMenuItem
            // 
            this.goodSellToolStripMenuItem.Image = global::supplyGood.Properties.Resources.stat_popularity;
            this.goodSellToolStripMenuItem.Name = "goodSellToolStripMenuItem";
            this.goodSellToolStripMenuItem.Size = new System.Drawing.Size(213, 30);
            this.goodSellToolStripMenuItem.Text = "Популярность";
            this.goodSellToolStripMenuItem.Click += new System.EventHandler(this.GoodSellToolStripMenuItem_Click);
            // 
            // goodProfitToolStripMenuItem
            // 
            this.goodProfitToolStripMenuItem.Image = global::supplyGood.Properties.Resources.stat_profit;
            this.goodProfitToolStripMenuItem.Name = "goodProfitToolStripMenuItem";
            this.goodProfitToolStripMenuItem.Size = new System.Drawing.Size(213, 30);
            this.goodProfitToolStripMenuItem.Text = "Прибыльность";
            this.goodProfitToolStripMenuItem.Click += new System.EventHandler(this.GoodProfitToolStripMenuItem_Click);
            // 
            // сотрудникиToolStripMenuItem
            // 
            this.сотрудникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.employmentToolStripMenuItem});
            this.сотрудникиToolStripMenuItem.Image = global::supplyGood.Properties.Resources.employee;
            this.сотрудникиToolStripMenuItem.Name = "сотрудникиToolStripMenuItem";
            this.сотрудникиToolStripMenuItem.Size = new System.Drawing.Size(143, 29);
            this.сотрудникиToolStripMenuItem.Text = "Сотрудники";
            // 
            // employmentToolStripMenuItem
            // 
            this.employmentToolStripMenuItem.Image = global::supplyGood.Properties.Resources.stat_employment;
            this.employmentToolStripMenuItem.Name = "employmentToolStripMenuItem";
            this.employmentToolStripMenuItem.Size = new System.Drawing.Size(171, 30);
            this.employmentToolStripMenuItem.Text = "Занятость";
            this.employmentToolStripMenuItem.Click += new System.EventHandler(this.EmploymentToolStripMenuItem_Click);
            // 
            // заказчикToolStripMenuItem
            // 
            this.заказчикToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientSumSupplyToolStripMenuItem});
            this.заказчикToolStripMenuItem.Image = global::supplyGood.Properties.Resources.client;
            this.заказчикToolStripMenuItem.Name = "заказчикToolStripMenuItem";
            this.заказчикToolStripMenuItem.Size = new System.Drawing.Size(118, 29);
            this.заказчикToolStripMenuItem.Text = "Заказчик";
            // 
            // clientSumSupplyToolStripMenuItem
            // 
            this.clientSumSupplyToolStripMenuItem.Image = global::supplyGood.Properties.Resources.stat_profit;
            this.clientSumSupplyToolStripMenuItem.Name = "clientSumSupplyToolStripMenuItem";
            this.clientSumSupplyToolStripMenuItem.Size = new System.Drawing.Size(225, 30);
            this.clientSumSupplyToolStripMenuItem.Text = "Сумма поставок";
            this.clientSumSupplyToolStripMenuItem.Click += new System.EventHandler(this.ClientSumSupplyToolStripMenuItem_Click);
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
            this.dgvMain.Location = new System.Drawing.Point(12, 114);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersVisible = false;
            this.dgvMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMain.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMain.ShowCellToolTips = false;
            this.dgvMain.Size = new System.Drawing.Size(760, 436);
            this.dgvMain.TabIndex = 4;
            this.dgvMain.Sorted += new System.EventHandler(this.dgvMain_Sorted);
            // 
            // cbxOptions
            // 
            this.cbxOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOptions.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxOptions.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbxOptions.FormattingEnabled = true;
            this.cbxOptions.Location = new System.Drawing.Point(484, 43);
            this.cbxOptions.Name = "cbxOptions";
            this.cbxOptions.Size = new System.Drawing.Size(288, 29);
            this.cbxOptions.TabIndex = 5;
            this.cbxOptions.Visible = false;
            this.cbxOptions.SelectedIndexChanged += new System.EventHandler(this.CbxOptions_SelectedIndexChanged);
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCaption.ForeColor = System.Drawing.Color.Black;
            this.lblCaption.Location = new System.Drawing.Point(52, 33);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(275, 37);
            this.lblCaption.TabIndex = 7;
            this.lblCaption.Text = "Название статистики";
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.Gainsboro;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReport.Location = new System.Drawing.Point(12, 73);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(188, 35);
            this.btnReport.TabIndex = 20;
            this.btnReport.Text = "Сформировать отчет";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // pictureMain
            // 
            this.pictureMain.Image = global::supplyGood.Properties.Resources.statistics;
            this.pictureMain.InitialImage = null;
            this.pictureMain.Location = new System.Drawing.Point(12, 36);
            this.pictureMain.Name = "pictureMain";
            this.pictureMain.Size = new System.Drawing.Size(34, 34);
            this.pictureMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureMain.TabIndex = 57;
            this.pictureMain.TabStop = false;
            // 
            // StatisticsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.pictureMain);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.cbxOptions);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.mainMenuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "StatisticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Статистика";
            this.Load += new System.EventHandler(this.Statistics_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem goodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goodSellToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goodProfitToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.ComboBox cbxOptions;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.PictureBox pictureMain;
        private System.Windows.Forms.ToolStripMenuItem сотрудникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem employmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заказчикToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientSumSupplyToolStripMenuItem;
    }
}