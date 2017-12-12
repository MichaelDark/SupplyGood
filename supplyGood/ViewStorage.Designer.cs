namespace supplyGood
{
    partial class ViewStorage
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvGoods = new System.Windows.Forms.DataGridView();
            this.idstorageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDBDataSet = new supplyGood.MainDBDataSet();
            this.btnGoodAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtGoodName = new System.Windows.Forms.TextBox();
            this.btnGoodSave = new System.Windows.Forms.Button();
            this.lblUnit = new System.Windows.Forms.TextBox();
            this.goodBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxStorekeeper = new System.Windows.Forms.ComboBox();
            this.goodTableAdapter = new supplyGood.MainDBDataSetTableAdapters.GoodTableAdapter();
            this.goodAddBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stockTableAdapter = new supplyGood.MainDBDataSetTableAdapters.StockTableAdapter();
            this.stockUFTableAdapter = new supplyGood.MainDBDataSetTableAdapters.StockUFTableAdapter();
            this.storageTableAdapter = new supplyGood.MainDBDataSetTableAdapters.StorageTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDBDataSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.goodBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.goodAddBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.Location = new System.Drawing.Point(19, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(391, 37);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Склад";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(16, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Адрес склада *";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(19, 507);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(391, 46);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Редактировать";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(19, 122);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(391, 85);
            this.txtAddress.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(16, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 42;
            this.label2.Text = "Кладовщик";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(438, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 17);
            this.label7.TabIndex = 49;
            this.label7.Text = "Товары";
            // 
            // dgvGoods
            // 
            this.dgvGoods.AllowUserToAddRows = false;
            this.dgvGoods.AllowUserToResizeRows = false;
            this.dgvGoods.AutoGenerateColumns = false;
            this.dgvGoods.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGoods.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvGoods.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvGoods.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGoods.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvGoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGoods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idstorageDataGridViewTextBoxColumn,
            this.goodDataGridViewTextBoxColumn,
            this.amountDataGridViewTextBoxColumn});
            this.dgvGoods.DataSource = this.stockBindingSource;
            this.dgvGoods.Location = new System.Drawing.Point(441, 106);
            this.dgvGoods.MultiSelect = false;
            this.dgvGoods.Name = "dgvGoods";
            this.dgvGoods.ReadOnly = true;
            this.dgvGoods.RowHeadersVisible = false;
            this.dgvGoods.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvGoods.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dgvGoods.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvGoods.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGoods.ShowCellToolTips = false;
            this.dgvGoods.Size = new System.Drawing.Size(565, 325);
            this.dgvGoods.TabIndex = 9;
            this.dgvGoods.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvGoods_DefaultValuesNeeded);
            this.dgvGoods.SelectionChanged += new System.EventHandler(this.dgvGoods_SelectionChanged);
            // 
            // idstorageDataGridViewTextBoxColumn
            // 
            this.idstorageDataGridViewTextBoxColumn.DataPropertyName = "id_storage";
            this.idstorageDataGridViewTextBoxColumn.HeaderText = "ID склада";
            this.idstorageDataGridViewTextBoxColumn.Name = "idstorageDataGridViewTextBoxColumn";
            this.idstorageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // goodDataGridViewTextBoxColumn
            // 
            this.goodDataGridViewTextBoxColumn.DataPropertyName = "good";
            this.goodDataGridViewTextBoxColumn.HeaderText = "Товар";
            this.goodDataGridViewTextBoxColumn.Name = "goodDataGridViewTextBoxColumn";
            this.goodDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // amountDataGridViewTextBoxColumn
            // 
            this.amountDataGridViewTextBoxColumn.DataPropertyName = "amount";
            this.amountDataGridViewTextBoxColumn.HeaderText = "В наличии, ед.";
            this.amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
            this.amountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stockBindingSource
            // 
            this.stockBindingSource.DataMember = "StockUF";
            this.stockBindingSource.DataSource = this.mainDBDataSet;
            // 
            // mainDBDataSet
            // 
            this.mainDBDataSet.DataSetName = "MainDBDataSet";
            this.mainDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnGoodAdd
            // 
            this.btnGoodAdd.BackColor = System.Drawing.Color.Gainsboro;
            this.btnGoodAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGoodAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnGoodAdd.Location = new System.Drawing.Point(441, 66);
            this.btnGoodAdd.Name = "btnGoodAdd";
            this.btnGoodAdd.Size = new System.Drawing.Size(565, 33);
            this.btnGoodAdd.TabIndex = 52;
            this.btnGoodAdd.Text = "Добавить товар";
            this.btnGoodAdd.UseVisualStyleBackColor = false;
            this.btnGoodAdd.Click += new System.EventHandler(this.btnGoodAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtGoodName);
            this.groupBox1.Controls.Add(this.btnGoodSave);
            this.groupBox1.Controls.Add(this.lblUnit);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(441, 437);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 116);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            // 
            // txtGoodName
            // 
            this.txtGoodName.Location = new System.Drawing.Point(77, 32);
            this.txtGoodName.Name = "txtGoodName";
            this.txtGoodName.ReadOnly = true;
            this.txtGoodName.Size = new System.Drawing.Size(319, 33);
            this.txtGoodName.TabIndex = 60;
            // 
            // btnGoodSave
            // 
            this.btnGoodSave.BackColor = System.Drawing.Color.Gainsboro;
            this.btnGoodSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGoodSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnGoodSave.Location = new System.Drawing.Point(414, 72);
            this.btnGoodSave.Name = "btnGoodSave";
            this.btnGoodSave.Size = new System.Drawing.Size(145, 34);
            this.btnGoodSave.TabIndex = 56;
            this.btnGoodSave.Text = "Сохранить";
            this.btnGoodSave.UseVisualStyleBackColor = false;
            this.btnGoodSave.Visible = false;
            this.btnGoodSave.Click += new System.EventHandler(this.btnGoodSave_Click);
            // 
            // lblUnit
            // 
            this.lblUnit.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.goodBindingSource, "g_unit", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblUnit.Location = new System.Drawing.Point(414, 32);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.ReadOnly = true;
            this.lblUnit.Size = new System.Drawing.Size(145, 33);
            this.lblUnit.TabIndex = 59;
            // 
            // goodBindingSource
            // 
            this.goodBindingSource.DataMember = "Good";
            this.goodBindingSource.DataSource = this.mainDBDataSet;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(192, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 25);
            this.label10.TabIndex = 58;
            this.label10.Text = "Количество";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(309, 72);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(87, 33);
            this.txtAmount.TabIndex = 57;
            this.txtAmount.TextChanged += new System.EventHandler(this.GoodAdd_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(7, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 25);
            this.label9.TabIndex = 56;
            this.label9.Text = "Товар";
            // 
            // cbxStorekeeper
            // 
            this.cbxStorekeeper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStorekeeper.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxStorekeeper.FormattingEnabled = true;
            this.cbxStorekeeper.Location = new System.Drawing.Point(19, 66);
            this.cbxStorekeeper.Name = "cbxStorekeeper";
            this.cbxStorekeeper.Size = new System.Drawing.Size(391, 33);
            this.cbxStorekeeper.TabIndex = 2;
            // 
            // goodTableAdapter
            // 
            this.goodTableAdapter.ClearBeforeFill = true;
            // 
            // goodAddBindingSource
            // 
            this.goodAddBindingSource.DataMember = "Good";
            this.goodAddBindingSource.DataSource = this.mainDBDataSet;
            // 
            // stockTableAdapter
            // 
            this.stockTableAdapter.ClearBeforeFill = true;
            // 
            // stockUFTableAdapter
            // 
            this.stockUFTableAdapter.ClearBeforeFill = true;
            // 
            // storageTableAdapter
            // 
            this.storageTableAdapter.ClearBeforeFill = true;
            // 
            // ViewStorage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1019, 565);
            this.Controls.Add(this.dgvGoods);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnGoodAdd);
            this.Controls.Add(this.cbxStorekeeper);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ViewStorage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Просмотр - Склад";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewSupply_FormClosing);
            this.Load += new System.EventHandler(this.ViewSupply_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDBDataSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.goodBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.goodAddBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvGoods;
        private MainDBDataSet mainDBDataSet;
        private System.Windows.Forms.BindingSource goodBindingSource;
        private MainDBDataSetTableAdapters.GoodTableAdapter goodTableAdapter;
        private System.Windows.Forms.Button btnGoodAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox lblUnit;
        private System.Windows.Forms.BindingSource goodAddBindingSource;
        private System.Windows.Forms.Button btnGoodSave;
        private System.Windows.Forms.TextBox txtGoodName;
        private System.Windows.Forms.ComboBox cbxStorekeeper;
        private System.Windows.Forms.BindingSource stockBindingSource;
        private MainDBDataSetTableAdapters.StockTableAdapter stockTableAdapter;
        private MainDBDataSetTableAdapters.StockUFTableAdapter stockUFTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idstorageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;
        private MainDBDataSetTableAdapters.StorageTableAdapter storageTableAdapter;
    }
}