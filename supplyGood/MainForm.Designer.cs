namespace supplyGood
{
    partial class MainForm
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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suppliesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goodsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.employeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMain = new System.Windows.Forms.Label();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.contextDGV = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFunc = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.lblFilter9 = new System.Windows.Forms.Label();
            this.txtFilter9 = new System.Windows.Forms.TextBox();
            this.lblFilter8 = new System.Windows.Forms.Label();
            this.txtFilter8 = new System.Windows.Forms.TextBox();
            this.lblFilter7 = new System.Windows.Forms.Label();
            this.txtFilter7 = new System.Windows.Forms.TextBox();
            this.lblFilter6 = new System.Windows.Forms.Label();
            this.txtFilter6 = new System.Windows.Forms.TextBox();
            this.lblFilter5 = new System.Windows.Forms.Label();
            this.txtFilter5 = new System.Windows.Forms.TextBox();
            this.lblFilter4 = new System.Windows.Forms.Label();
            this.txtFilter4 = new System.Windows.Forms.TextBox();
            this.lblFilter3 = new System.Windows.Forms.Label();
            this.txtFilter3 = new System.Windows.Forms.TextBox();
            this.lblFilter2 = new System.Windows.Forms.Label();
            this.txtFilter2 = new System.Windows.Forms.TextBox();
            this.lblFilter1 = new System.Windows.Forms.Label();
            this.txtFilter1 = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.employeeTableAdapter = new supplyGood.MainDBDataSetTableAdapters.EmployeeTableAdapter();
            this.personalInfoTableAdapter = new supplyGood.MainDBDataSetTableAdapters.PersonalInfoTableAdapter();
            this.goodTableAdapter = new supplyGood.MainDBDataSetTableAdapters.GoodTableAdapter();
            this.carTableAdapter = new supplyGood.MainDBDataSetTableAdapters.CarTableAdapter();
            this.mainDBDataSet = new supplyGood.MainDBDataSet();
            this.userBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.userTableAdapter = new supplyGood.MainDBDataSetTableAdapters.UserTableAdapter();
            this.supplyTableAdapter = new supplyGood.MainDBDataSetTableAdapters.SupplyTableAdapter();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.contextDGV.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mainMenuStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.managementToolStripMenuItem,
            this.usersToolStripMenuItem});
            this.mainMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mainMenuStrip.Size = new System.Drawing.Size(1204, 24);
            this.mainMenuStrip.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // managementToolStripMenuItem
            // 
            this.managementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.suppliesToolStripMenuItem,
            this.goodsToolStripMenuItem,
            this.carsToolStripMenuItem,
            this.storagesToolStripMenuItem,
            this.clientsToolStripMenuItem,
            this.employeeToolStripMenuItem});
            this.managementToolStripMenuItem.Name = "managementToolStripMenuItem";
            this.managementToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.managementToolStripMenuItem.Text = "Просмотр";
            // 
            // suppliesToolStripMenuItem
            // 
            this.suppliesToolStripMenuItem.Name = "suppliesToolStripMenuItem";
            this.suppliesToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.suppliesToolStripMenuItem.Text = "Поставки";
            this.suppliesToolStripMenuItem.Click += new System.EventHandler(this.SuppliesToolStripMenuItem_Click);
            // 
            // goodsToolStripMenuItem
            // 
            this.goodsToolStripMenuItem.Name = "goodsToolStripMenuItem";
            this.goodsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.goodsToolStripMenuItem.Text = "Товары";
            this.goodsToolStripMenuItem.Click += new System.EventHandler(this.GoodsToolStripMenuItem_Click);
            // 
            // carsToolStripMenuItem
            // 
            this.carsToolStripMenuItem.Name = "carsToolStripMenuItem";
            this.carsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.carsToolStripMenuItem.Text = "Машины";
            this.carsToolStripMenuItem.Click += new System.EventHandler(this.CarsToolStripMenuItem_Click);
            // 
            // storagesToolStripMenuItem
            // 
            this.storagesToolStripMenuItem.Name = "storagesToolStripMenuItem";
            this.storagesToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.storagesToolStripMenuItem.Text = "-Склады";
            // 
            // clientsToolStripMenuItem
            // 
            this.clientsToolStripMenuItem.Name = "clientsToolStripMenuItem";
            this.clientsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.clientsToolStripMenuItem.Text = "-Заказчики";
            // 
            // employeeToolStripMenuItem
            // 
            this.employeeToolStripMenuItem.Name = "employeeToolStripMenuItem";
            this.employeeToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.employeeToolStripMenuItem.Text = "Сотрудники";
            this.employeeToolStripMenuItem.Click += new System.EventHandler(this.EmployeeToolStripMenuItem_Click);
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.usersToolStripMenuItem.Text = "Пользователи";
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.UsersToolStripMenuItem_Click);
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMain.Location = new System.Drawing.Point(12, 24);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(330, 47);
            this.lblMain.TabIndex = 3;
            this.lblMain.Text = "Выберите действие";
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AllowUserToResizeRows = false;
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMain.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMain.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Location = new System.Drawing.Point(12, 74);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersVisible = false;
            this.dgvMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMain.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.ShowCellToolTips = false;
            this.dgvMain.Size = new System.Drawing.Size(966, 422);
            this.dgvMain.TabIndex = 9;
            this.dgvMain.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMain_CellMouseDown);
            this.dgvMain.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMain_DataError);
            this.dgvMain.DoubleClick += new System.EventHandler(this.dgvMain_DoubleClick);
            // 
            // contextDGV
            // 
            this.contextDGV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailsToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextDGV.Name = "cmsViewEditDelete";
            this.contextDGV.Size = new System.Drawing.Size(155, 70);
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.detailsToolStripMenuItem.Text = "Просмотреть";
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.Context_DetailsToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.editToolStripMenuItem.Text = "Редактировать";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.Context_EditToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.deleteToolStripMenuItem.Text = "Удалить";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.Context_DeleteToolStripMenuItem_Click);
            // 
            // btnFunc
            // 
            this.btnFunc.BackColor = System.Drawing.Color.Azure;
            this.btnFunc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFunc.Location = new System.Drawing.Point(740, 502);
            this.btnFunc.Name = "btnFunc";
            this.btnFunc.Size = new System.Drawing.Size(238, 48);
            this.btnFunc.TabIndex = 10;
            this.btnFunc.Text = "Добавить пользователя";
            this.btnFunc.UseVisualStyleBackColor = false;
            this.btnFunc.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblHint
            // 
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHint.Location = new System.Drawing.Point(12, 499);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(722, 63);
            this.lblHint.TabIndex = 11;
            this.lblHint.Text = "Подсказка";
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.Honeydew;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFilter.Location = new System.Drawing.Point(862, 27);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(110, 30);
            this.btnFilter.TabIndex = 12;
            this.btnFilter.Text = "Фильтры >>";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClearFilters);
            this.panel1.Controls.Add(this.lblFilter9);
            this.panel1.Controls.Add(this.txtFilter9);
            this.panel1.Controls.Add(this.lblFilter8);
            this.panel1.Controls.Add(this.txtFilter8);
            this.panel1.Controls.Add(this.lblFilter7);
            this.panel1.Controls.Add(this.txtFilter7);
            this.panel1.Controls.Add(this.lblFilter6);
            this.panel1.Controls.Add(this.txtFilter6);
            this.panel1.Controls.Add(this.lblFilter5);
            this.panel1.Controls.Add(this.txtFilter5);
            this.panel1.Controls.Add(this.lblFilter4);
            this.panel1.Controls.Add(this.txtFilter4);
            this.panel1.Controls.Add(this.lblFilter3);
            this.panel1.Controls.Add(this.txtFilter3);
            this.panel1.Controls.Add(this.lblFilter2);
            this.panel1.Controls.Add(this.txtFilter2);
            this.panel1.Controls.Add(this.lblFilter1);
            this.panel1.Controls.Add(this.txtFilter1);
            this.panel1.Controls.Add(this.lbl1);
            this.panel1.Location = new System.Drawing.Point(1000, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 538);
            this.panel1.TabIndex = 13;
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClearFilters.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearFilters.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClearFilters.Location = new System.Drawing.Point(0, 478);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(188, 48);
            this.btnClearFilters.TabIndex = 19;
            this.btnClearFilters.Text = "Сбросить фильтры";
            this.btnClearFilters.UseVisualStyleBackColor = false;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // lblFilter9
            // 
            this.lblFilter9.AutoSize = true;
            this.lblFilter9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFilter9.Location = new System.Drawing.Point(3, 424);
            this.lblFilter9.Name = "lblFilter9";
            this.lblFilter9.Size = new System.Drawing.Size(43, 17);
            this.lblFilter9.TabIndex = 18;
            this.lblFilter9.Text = "label9";
            // 
            // txtFilter9
            // 
            this.txtFilter9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFilter9.Location = new System.Drawing.Point(3, 441);
            this.txtFilter9.Name = "txtFilter9";
            this.txtFilter9.Size = new System.Drawing.Size(182, 29);
            this.txtFilter9.TabIndex = 17;
            this.txtFilter9.TextChanged += new System.EventHandler(this.Filter_TextChanged);
            // 
            // lblFilter8
            // 
            this.lblFilter8.AutoSize = true;
            this.lblFilter8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFilter8.Location = new System.Drawing.Point(3, 375);
            this.lblFilter8.Name = "lblFilter8";
            this.lblFilter8.Size = new System.Drawing.Size(43, 17);
            this.lblFilter8.TabIndex = 16;
            this.lblFilter8.Text = "label8";
            // 
            // txtFilter8
            // 
            this.txtFilter8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFilter8.Location = new System.Drawing.Point(3, 392);
            this.txtFilter8.Name = "txtFilter8";
            this.txtFilter8.Size = new System.Drawing.Size(182, 29);
            this.txtFilter8.TabIndex = 15;
            this.txtFilter8.TextChanged += new System.EventHandler(this.Filter_TextChanged);
            // 
            // lblFilter7
            // 
            this.lblFilter7.AutoSize = true;
            this.lblFilter7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFilter7.Location = new System.Drawing.Point(3, 326);
            this.lblFilter7.Name = "lblFilter7";
            this.lblFilter7.Size = new System.Drawing.Size(43, 17);
            this.lblFilter7.TabIndex = 14;
            this.lblFilter7.Text = "label7";
            // 
            // txtFilter7
            // 
            this.txtFilter7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFilter7.Location = new System.Drawing.Point(3, 343);
            this.txtFilter7.Name = "txtFilter7";
            this.txtFilter7.Size = new System.Drawing.Size(182, 29);
            this.txtFilter7.TabIndex = 13;
            this.txtFilter7.TextChanged += new System.EventHandler(this.Filter_TextChanged);
            // 
            // lblFilter6
            // 
            this.lblFilter6.AutoSize = true;
            this.lblFilter6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFilter6.Location = new System.Drawing.Point(3, 277);
            this.lblFilter6.Name = "lblFilter6";
            this.lblFilter6.Size = new System.Drawing.Size(43, 17);
            this.lblFilter6.TabIndex = 12;
            this.lblFilter6.Text = "label6";
            // 
            // txtFilter6
            // 
            this.txtFilter6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFilter6.Location = new System.Drawing.Point(3, 294);
            this.txtFilter6.Name = "txtFilter6";
            this.txtFilter6.Size = new System.Drawing.Size(182, 29);
            this.txtFilter6.TabIndex = 11;
            this.txtFilter6.TextChanged += new System.EventHandler(this.Filter_TextChanged);
            // 
            // lblFilter5
            // 
            this.lblFilter5.AutoSize = true;
            this.lblFilter5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFilter5.Location = new System.Drawing.Point(3, 228);
            this.lblFilter5.Name = "lblFilter5";
            this.lblFilter5.Size = new System.Drawing.Size(43, 17);
            this.lblFilter5.TabIndex = 10;
            this.lblFilter5.Text = "label5";
            // 
            // txtFilter5
            // 
            this.txtFilter5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFilter5.Location = new System.Drawing.Point(3, 245);
            this.txtFilter5.Name = "txtFilter5";
            this.txtFilter5.Size = new System.Drawing.Size(182, 29);
            this.txtFilter5.TabIndex = 9;
            this.txtFilter5.TextChanged += new System.EventHandler(this.Filter_TextChanged);
            // 
            // lblFilter4
            // 
            this.lblFilter4.AutoSize = true;
            this.lblFilter4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFilter4.Location = new System.Drawing.Point(3, 179);
            this.lblFilter4.Name = "lblFilter4";
            this.lblFilter4.Size = new System.Drawing.Size(43, 17);
            this.lblFilter4.TabIndex = 8;
            this.lblFilter4.Text = "label4";
            // 
            // txtFilter4
            // 
            this.txtFilter4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFilter4.Location = new System.Drawing.Point(3, 196);
            this.txtFilter4.Name = "txtFilter4";
            this.txtFilter4.Size = new System.Drawing.Size(182, 29);
            this.txtFilter4.TabIndex = 7;
            this.txtFilter4.TextChanged += new System.EventHandler(this.Filter_TextChanged);
            // 
            // lblFilter3
            // 
            this.lblFilter3.AutoSize = true;
            this.lblFilter3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFilter3.Location = new System.Drawing.Point(3, 130);
            this.lblFilter3.Name = "lblFilter3";
            this.lblFilter3.Size = new System.Drawing.Size(43, 17);
            this.lblFilter3.TabIndex = 6;
            this.lblFilter3.Text = "label3";
            // 
            // txtFilter3
            // 
            this.txtFilter3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFilter3.Location = new System.Drawing.Point(3, 147);
            this.txtFilter3.Name = "txtFilter3";
            this.txtFilter3.Size = new System.Drawing.Size(182, 29);
            this.txtFilter3.TabIndex = 5;
            this.txtFilter3.TextChanged += new System.EventHandler(this.Filter_TextChanged);
            // 
            // lblFilter2
            // 
            this.lblFilter2.AutoSize = true;
            this.lblFilter2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFilter2.Location = new System.Drawing.Point(3, 81);
            this.lblFilter2.Name = "lblFilter2";
            this.lblFilter2.Size = new System.Drawing.Size(43, 17);
            this.lblFilter2.TabIndex = 4;
            this.lblFilter2.Text = "label2";
            // 
            // txtFilter2
            // 
            this.txtFilter2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFilter2.Location = new System.Drawing.Point(3, 98);
            this.txtFilter2.Name = "txtFilter2";
            this.txtFilter2.Size = new System.Drawing.Size(182, 29);
            this.txtFilter2.TabIndex = 3;
            this.txtFilter2.TextChanged += new System.EventHandler(this.Filter_TextChanged);
            // 
            // lblFilter1
            // 
            this.lblFilter1.AutoSize = true;
            this.lblFilter1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFilter1.Location = new System.Drawing.Point(3, 32);
            this.lblFilter1.Name = "lblFilter1";
            this.lblFilter1.Size = new System.Drawing.Size(43, 17);
            this.lblFilter1.TabIndex = 2;
            this.lblFilter1.Text = "label1";
            // 
            // txtFilter1
            // 
            this.txtFilter1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFilter1.Location = new System.Drawing.Point(3, 49);
            this.txtFilter1.Name = "txtFilter1";
            this.txtFilter1.Size = new System.Drawing.Size(182, 29);
            this.txtFilter1.TabIndex = 1;
            this.txtFilter1.TextChanged += new System.EventHandler(this.Filter_TextChanged);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl1.Location = new System.Drawing.Point(47, 2);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(98, 30);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Фильтры";
            // 
            // employeeTableAdapter
            // 
            this.employeeTableAdapter.ClearBeforeFill = true;
            // 
            // personalInfoTableAdapter
            // 
            this.personalInfoTableAdapter.ClearBeforeFill = true;
            // 
            // goodTableAdapter
            // 
            this.goodTableAdapter.ClearBeforeFill = true;
            // 
            // carTableAdapter
            // 
            this.carTableAdapter.ClearBeforeFill = true;
            // 
            // mainDBDataSet
            // 
            this.mainDBDataSet.DataSetName = "MainDBDataSet";
            this.mainDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // userBindingSource
            // 
            this.userBindingSource.DataMember = "User";
            this.userBindingSource.DataSource = this.mainDBDataSet;
            // 
            // userTableAdapter
            // 
            this.userTableAdapter.ClearBeforeFill = true;
            // 
            // supplyTableAdapter
            // 
            this.supplyTableAdapter.ClearBeforeFill = true;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1204, 562);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnFunc);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.lblMain);
            this.Controls.Add(this.mainMenuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Администратор";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.contextDGV.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem managementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem employeeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suppliesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goodsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storagesToolStripMenuItem;
        private System.Windows.Forms.Label lblMain;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.ContextMenuStrip contextDGV;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button btnFunc;
        private System.Windows.Forms.Label lblHint;
        private MainDBDataSetTableAdapters.EmployeeTableAdapter employeeTableAdapter;
        private MainDBDataSetTableAdapters.PersonalInfoTableAdapter personalInfoTableAdapter;
        private System.Windows.Forms.ToolStripMenuItem clientsToolStripMenuItem;
        private MainDBDataSetTableAdapters.GoodTableAdapter goodTableAdapter;
        private MainDBDataSetTableAdapters.CarTableAdapter carTableAdapter;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Panel panel1;
        private MainDBDataSet mainDBDataSet;
        private System.Windows.Forms.BindingSource userBindingSource;
        private MainDBDataSetTableAdapters.UserTableAdapter userTableAdapter;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.Label lblFilter9;
        private System.Windows.Forms.TextBox txtFilter9;
        private System.Windows.Forms.Label lblFilter8;
        private System.Windows.Forms.TextBox txtFilter8;
        private System.Windows.Forms.Label lblFilter7;
        private System.Windows.Forms.TextBox txtFilter7;
        private System.Windows.Forms.Label lblFilter6;
        private System.Windows.Forms.TextBox txtFilter6;
        private System.Windows.Forms.Label lblFilter5;
        private System.Windows.Forms.TextBox txtFilter5;
        private System.Windows.Forms.Label lblFilter4;
        private System.Windows.Forms.TextBox txtFilter4;
        private System.Windows.Forms.Label lblFilter3;
        private System.Windows.Forms.TextBox txtFilter3;
        private System.Windows.Forms.Label lblFilter2;
        private System.Windows.Forms.TextBox txtFilter2;
        private System.Windows.Forms.Label lblFilter1;
        private System.Windows.Forms.TextBox txtFilter1;
        private System.Windows.Forms.Label lbl1;
        private MainDBDataSetTableAdapters.SupplyTableAdapter supplyTableAdapter;
    }
}