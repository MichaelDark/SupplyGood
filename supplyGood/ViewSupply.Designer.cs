namespace supplyGood
{
    partial class ViewSupply
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
            this.label5 = new System.Windows.Forms.Label();
            this.cbxShipped = new System.Windows.Forms.CheckBox();
            this.dateContract = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.employeeTableAdapter = new supplyGood.MainDBDataSetTableAdapters.EmployeeTableAdapter();
            this.personalInfoTableAdapter = new supplyGood.MainDBDataSetTableAdapters.PersonalInfoTableAdapter();
            this.cbxClient = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxCar = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxStorage = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPeriod = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxDelivered = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvGoods = new System.Windows.Forms.DataGridView();
            this.idsupplyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idgoodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conamountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conpriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.consignmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDBDataSet = new supplyGood.MainDBDataSet();
            this.consignmentTableAdapter = new supplyGood.MainDBDataSetTableAdapters.ConsignmentTableAdapter();
            this.lblTotalSum = new System.Windows.Forms.Label();
            this.supplyTableAdapter = new supplyGood.MainDBDataSetTableAdapters.SupplyTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.consignmentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDBDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.Location = new System.Drawing.Point(19, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(391, 37);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Поставка";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(16, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Адрес доставки";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(16, 322);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 17);
            this.label5.TabIndex = 30;
            this.label5.Text = "Дата заключения договора";
            // 
            // cbxShipped
            // 
            this.cbxShipped.AutoSize = true;
            this.cbxShipped.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbxShipped.Location = new System.Drawing.Point(19, 437);
            this.cbxShipped.Name = "cbxShipped";
            this.cbxShipped.Size = new System.Drawing.Size(214, 29);
            this.cbxShipped.TabIndex = 7;
            this.cbxShipped.Text = "Отгружено со склада";
            this.cbxShipped.UseVisualStyleBackColor = true;
            // 
            // dateContract
            // 
            this.dateContract.Location = new System.Drawing.Point(19, 342);
            this.dateContract.Name = "dateContract";
            this.dateContract.Size = new System.Drawing.Size(391, 33);
            this.dateContract.TabIndex = 5;
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
            this.txtAddress.Location = new System.Drawing.Point(19, 234);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(391, 85);
            this.txtAddress.TabIndex = 4;
            // 
            // employeeTableAdapter
            // 
            this.employeeTableAdapter.ClearBeforeFill = true;
            // 
            // personalInfoTableAdapter
            // 
            this.personalInfoTableAdapter.ClearBeforeFill = true;
            // 
            // cbxClient
            // 
            this.cbxClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxClient.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxClient.FormattingEnabled = true;
            this.cbxClient.Location = new System.Drawing.Point(19, 66);
            this.cbxClient.Name = "cbxClient";
            this.cbxClient.Size = new System.Drawing.Size(391, 33);
            this.cbxClient.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(16, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 40;
            this.label1.Text = "Заказчик";
            // 
            // cbxCar
            // 
            this.cbxCar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxCar.FormattingEnabled = true;
            this.cbxCar.Location = new System.Drawing.Point(19, 122);
            this.cbxCar.Name = "cbxCar";
            this.cbxCar.Size = new System.Drawing.Size(391, 33);
            this.cbxCar.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(16, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 42;
            this.label2.Text = "Машина";
            // 
            // cbxStorage
            // 
            this.cbxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStorage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxStorage.FormattingEnabled = true;
            this.cbxStorage.Location = new System.Drawing.Point(19, 178);
            this.cbxStorage.Name = "cbxStorage";
            this.cbxStorage.Size = new System.Drawing.Size(391, 33);
            this.cbxStorage.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(16, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 44;
            this.label3.Text = "Склад";
            // 
            // txtPeriod
            // 
            this.txtPeriod.Location = new System.Drawing.Point(19, 398);
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.Size = new System.Drawing.Size(391, 33);
            this.txtPeriod.TabIndex = 6;
            this.txtPeriod.Text = "6";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(16, 378);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 17);
            this.label4.TabIndex = 47;
            this.label4.Text = "Срок поставки, месяцев";
            // 
            // cbxDelivered
            // 
            this.cbxDelivered.AutoSize = true;
            this.cbxDelivered.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbxDelivered.Location = new System.Drawing.Point(19, 472);
            this.cbxDelivered.Name = "cbxDelivered";
            this.cbxDelivered.Size = new System.Drawing.Size(134, 29);
            this.cbxDelivered.TabIndex = 8;
            this.cbxDelivered.Text = "Доставлено";
            this.cbxDelivered.UseVisualStyleBackColor = true;
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
            this.dgvGoods.AllowUserToResizeRows = false;
            this.dgvGoods.AutoGenerateColumns = false;
            this.dgvGoods.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGoods.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvGoods.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvGoods.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGoods.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvGoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGoods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idsupplyDataGridViewTextBoxColumn,
            this.idgoodDataGridViewTextBoxColumn,
            this.conamountDataGridViewTextBoxColumn,
            this.conpriceDataGridViewTextBoxColumn});
            this.dgvGoods.DataSource = this.consignmentBindingSource;
            this.dgvGoods.Location = new System.Drawing.Point(441, 66);
            this.dgvGoods.Name = "dgvGoods";
            this.dgvGoods.RowHeadersVisible = false;
            this.dgvGoods.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvGoods.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dgvGoods.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvGoods.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvGoods.ShowCellToolTips = false;
            this.dgvGoods.Size = new System.Drawing.Size(565, 487);
            this.dgvGoods.TabIndex = 9;
            this.dgvGoods.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvGoods_DefaultValuesNeeded);
            this.dgvGoods.SelectionChanged += new System.EventHandler(this.dgvGoods_SelectionChanged);
            // 
            // idsupplyDataGridViewTextBoxColumn
            // 
            this.idsupplyDataGridViewTextBoxColumn.DataPropertyName = "id_supply";
            this.idsupplyDataGridViewTextBoxColumn.HeaderText = "ID поставки";
            this.idsupplyDataGridViewTextBoxColumn.Name = "idsupplyDataGridViewTextBoxColumn";
            this.idsupplyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idgoodDataGridViewTextBoxColumn
            // 
            this.idgoodDataGridViewTextBoxColumn.DataPropertyName = "id_good";
            this.idgoodDataGridViewTextBoxColumn.HeaderText = "ID товара";
            this.idgoodDataGridViewTextBoxColumn.Name = "idgoodDataGridViewTextBoxColumn";
            // 
            // conamountDataGridViewTextBoxColumn
            // 
            this.conamountDataGridViewTextBoxColumn.DataPropertyName = "con_amount";
            this.conamountDataGridViewTextBoxColumn.HeaderText = "Количество";
            this.conamountDataGridViewTextBoxColumn.Name = "conamountDataGridViewTextBoxColumn";
            // 
            // conpriceDataGridViewTextBoxColumn
            // 
            this.conpriceDataGridViewTextBoxColumn.DataPropertyName = "con_price";
            this.conpriceDataGridViewTextBoxColumn.HeaderText = "Цена за ед.";
            this.conpriceDataGridViewTextBoxColumn.Name = "conpriceDataGridViewTextBoxColumn";
            // 
            // consignmentBindingSource
            // 
            this.consignmentBindingSource.DataMember = "Consignment";
            this.consignmentBindingSource.DataSource = this.mainDBDataSet;
            // 
            // mainDBDataSet
            // 
            this.mainDBDataSet.DataSetName = "MainDBDataSet";
            this.mainDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // consignmentTableAdapter
            // 
            this.consignmentTableAdapter.ClearBeforeFill = true;
            // 
            // lblTotalSum
            // 
            this.lblTotalSum.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotalSum.Location = new System.Drawing.Point(498, 46);
            this.lblTotalSum.Name = "lblTotalSum";
            this.lblTotalSum.Size = new System.Drawing.Size(506, 17);
            this.lblTotalSum.TabIndex = 51;
            this.lblTotalSum.Text = "Общая сумма:";
            this.lblTotalSum.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // supplyTableAdapter
            // 
            this.supplyTableAdapter.ClearBeforeFill = true;
            // 
            // ViewSupply
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1019, 565);
            this.Controls.Add(this.lblTotalSum);
            this.Controls.Add(this.dgvGoods);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbxDelivered);
            this.Controls.Add(this.txtPeriod);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxStorage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxCar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxClient);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxShipped);
            this.Controls.Add(this.dateContract);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ViewSupply";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Просмотр - Поставка";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewSupply_FormClosing);
            this.Load += new System.EventHandler(this.ViewSupply_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.consignmentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDBDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbxShipped;
        private System.Windows.Forms.DateTimePicker dateContract;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtAddress;
        private MainDBDataSetTableAdapters.EmployeeTableAdapter employeeTableAdapter;
        private MainDBDataSetTableAdapters.PersonalInfoTableAdapter personalInfoTableAdapter;
        private System.Windows.Forms.ComboBox cbxClient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxCar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxStorage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPeriod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbxDelivered;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvGoods;
        private MainDBDataSet mainDBDataSet;
        private System.Windows.Forms.BindingSource consignmentBindingSource;
        private MainDBDataSetTableAdapters.ConsignmentTableAdapter consignmentTableAdapter;
        private System.Windows.Forms.Label lblTotalSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn idsupplyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idgoodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn conamountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn conpriceDataGridViewTextBoxColumn;
        private MainDBDataSetTableAdapters.SupplyTableAdapter supplyTableAdapter;
    }
}