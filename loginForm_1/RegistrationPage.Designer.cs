namespace loginForm_1
{
    partial class RegistrationPage
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
            lblUserName = new Label();
            lblPassword = new Label();
            txtNewUserN = new TextBox();
            txtNewUserP = new TextBox();
            btnRegister = new Button();
            lblErrorPassword = new Label();
            lblRegister = new Label();
            SuspendLayout();
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Location = new Point(268, 111);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(97, 15);
            lblUserName.TabIndex = 0;
            lblUserName.Text = "Create Username";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(268, 178);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(94, 15);
            lblPassword.TabIndex = 0;
            lblPassword.Text = "Create Password";
            // 
            // txtNewUserN
            // 
            txtNewUserN.Location = new Point(268, 141);
            txtNewUserN.Name = "txtNewUserN";
            txtNewUserN.Size = new Size(185, 23);
            txtNewUserN.TabIndex = 1;
            // 
            // txtNewUserP
            // 
            txtNewUserP.Location = new Point(268, 209);
            txtNewUserP.Name = "txtNewUserP";
            txtNewUserP.Size = new Size(185, 23);
            txtNewUserP.TabIndex = 1;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(268, 257);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(185, 33);
            btnRegister.TabIndex = 2;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // lblErrorPassword
            // 
            lblErrorPassword.AutoSize = true;
            lblErrorPassword.ForeColor = Color.Firebrick;
            lblErrorPassword.Location = new Point(268, 239);
            lblErrorPassword.Name = "lblErrorPassword";
            lblErrorPassword.Size = new Size(38, 15);
            lblErrorPassword.TabIndex = 3;
            lblErrorPassword.Text = "label1";
            // 
            // lblRegister
            // 
            lblRegister.AutoSize = true;
            lblRegister.Location = new Point(268, 37);
            lblRegister.Name = "lblRegister";
            lblRegister.Size = new Size(99, 15);
            lblRegister.TabIndex = 4;
            lblRegister.Text = "Registration Page";
            // 
            // RegistrationPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblRegister);
            Controls.Add(lblErrorPassword);
            Controls.Add(btnRegister);
            Controls.Add(txtNewUserP);
            Controls.Add(txtNewUserN);
            Controls.Add(lblPassword);
            Controls.Add(lblUserName);
            Name = "RegistrationPage";
            Text = "RegistrationPage";
            Load += RegistrationPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUserName;
        private Label lblPassword;
        private TextBox txtNewUserN;
        private TextBox txtNewUserP;
        private Button btnRegister;
        private Label lblErrorPassword;
        private Label lblRegister;
    }
}