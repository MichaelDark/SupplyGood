namespace supplyGood
{
    partial class UserBox
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblLoginCaption = new System.Windows.Forms.Label();
            this.lblPasswordCaption = new System.Windows.Forms.Label();
            this.lblRightsCaption = new System.Windows.Forms.Label();
            this.Login = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.Label();
            this.Rights = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLoginCaption
            // 
            this.lblLoginCaption.AutoSize = true;
            this.lblLoginCaption.CausesValidation = false;
            this.lblLoginCaption.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLoginCaption.Location = new System.Drawing.Point(25, 14);
            this.lblLoginCaption.Name = "lblLoginCaption";
            this.lblLoginCaption.Size = new System.Drawing.Size(56, 21);
            this.lblLoginCaption.TabIndex = 0;
            this.lblLoginCaption.Text = "Логин:";
            // 
            // lblPasswordCaption
            // 
            this.lblPasswordCaption.AutoSize = true;
            this.lblPasswordCaption.CausesValidation = false;
            this.lblPasswordCaption.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPasswordCaption.Location = new System.Drawing.Point(15, 39);
            this.lblPasswordCaption.Name = "lblPasswordCaption";
            this.lblPasswordCaption.Size = new System.Drawing.Size(66, 21);
            this.lblPasswordCaption.TabIndex = 1;
            this.lblPasswordCaption.Text = "Пароль:";
            // 
            // lblRightsCaption
            // 
            this.lblRightsCaption.AutoSize = true;
            this.lblRightsCaption.CausesValidation = false;
            this.lblRightsCaption.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRightsCaption.Location = new System.Drawing.Point(23, 64);
            this.lblRightsCaption.Name = "lblRightsCaption";
            this.lblRightsCaption.Size = new System.Drawing.Size(58, 21);
            this.lblRightsCaption.TabIndex = 2;
            this.lblRightsCaption.Text = "Права:";
            // 
            // Login
            // 
            this.Login.AutoSize = true;
            this.Login.CausesValidation = false;
            this.Login.Location = new System.Drawing.Point(87, 11);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(55, 25);
            this.Login.TabIndex = 3;
            this.Login.Text = "login";
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.CausesValidation = false;
            this.Password.Location = new System.Drawing.Point(87, 36);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(49, 25);
            this.Password.TabIndex = 4;
            this.Password.Text = "pass";
            // 
            // Rights
            // 
            this.Rights.AutoSize = true;
            this.Rights.CausesValidation = false;
            this.Rights.Location = new System.Drawing.Point(87, 61);
            this.Rights.Name = "Rights";
            this.Rights.Size = new System.Drawing.Size(60, 25);
            this.Rights.TabIndex = 5;
            this.Rights.Text = "rights";
            // 
            // UserBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.Rights);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.lblRightsCaption);
            this.Controls.Add(this.lblPasswordCaption);
            this.Controls.Add(this.lblLoginCaption);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "UserBox";
            this.Size = new System.Drawing.Size(278, 98);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLoginCaption;
        private System.Windows.Forms.Label lblPasswordCaption;
        private System.Windows.Forms.Label lblRightsCaption;
        public System.Windows.Forms.Label Login;
        public System.Windows.Forms.Label Password;
        public System.Windows.Forms.Label Rights;
    }
}
