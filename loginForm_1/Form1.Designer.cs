namespace loginForm_1;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        txtUsername = new TextBox();
        txtPassword = new TextBox();
        lblUserN = new Label();
        lblUserPass = new Label();
        btnLogin = new Button();
        btnRegister = new Button();
        lblTitle = new Label();
        lblRegister = new Label();
        SuspendLayout();
        // 
        // txtUsername
        // 
        txtUsername.Location = new Point(259, 110);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(200, 23);
        txtUsername.TabIndex = 0;
        // 
        // txtPassword
        // 
        txtPassword.Location = new Point(259, 172);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(200, 23);
        txtPassword.TabIndex = 0;
        txtPassword.TextChanged += txtPassword_TextChanged;
        // 
        // lblUserN
        // 
        lblUserN.AutoSize = true;
        lblUserN.Location = new Point(119, 119);
        lblUserN.Name = "lblUserN";
        lblUserN.Size = new Size(62, 15);
        lblUserN.TabIndex = 1;
        lblUserN.Text = "UserName";
        // 
        // lblUserPass
        // 
        lblUserPass.AutoSize = true;
        lblUserPass.Location = new Point(119, 175);
        lblUserPass.Name = "lblUserPass";
        lblUserPass.Size = new Size(57, 15);
        lblUserPass.TabIndex = 1;
        lblUserPass.Text = "Password";
        // 
        // btnLogin
        // 
        btnLogin.Location = new Point(259, 224);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(200, 40);
        btnLogin.TabIndex = 2;
        btnLogin.Text = "Login";
        btnLogin.UseVisualStyleBackColor = true;
        btnLogin.Click += btnLogin_Click;
        // 
        // btnRegister
        // 
        btnRegister.Location = new Point(259, 326);
        btnRegister.Name = "btnRegister";
        btnRegister.Size = new Size(200, 33);
        btnRegister.TabIndex = 2;
        btnRegister.Text = "Register";
        btnRegister.UseVisualStyleBackColor = true;
        btnRegister.Click += btnRegister_Click;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold | FontStyle.Italic);
        lblTitle.Location = new Point(259, 20);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(121, 51);
        lblTitle.TabIndex = 3;
        lblTitle.Text = "Login";
        // 
        // lblRegister
        // 
        lblRegister.AutoSize = true;
        lblRegister.Location = new Point(222, 287);
        lblRegister.Name = "lblRegister";
        lblRegister.Size = new Size(225, 15);
        lblRegister.TabIndex = 4;
        lblRegister.Text = "Don't Have An Account? Click to Register";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(lblRegister);
        Controls.Add(btnRegister);
        Controls.Add(btnLogin);
        Controls.Add(lblUserPass);
        Controls.Add(lblUserN);
        Controls.Add(txtPassword);
        Controls.Add(txtUsername);
        Controls.Add(lblTitle);
        Name = "Form1";
        Text = "HertzPlayLoginPage";
        Load += Form1_Load_1;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox txtUsername;
    private TextBox txtPassword;
    private Label lblUserN;
    private Label lblUserPass;
    private Button btnLogin;
    private Button btnRegister;
    private Label lblTitle;
    private Label lblRegister;
}
