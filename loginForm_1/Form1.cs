using System;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;

namespace loginForm_1;
//TEST PASSWORD: bopPass2026! & Username:u2080426
public partial class Form1 : Form
{
    private readonly HomePage _homePage;
    public Form1(HomePage homePage)
    {
        InitializeComponent();

        ApplyCustomStyling();

        _homePage = homePage ?? throw new ArgumentNullException(nameof(homePage));

    }



    private void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {

            if (this.IsLoginValid(txtUsername.Text, txtPassword.Text, out string finalUser))
            {
                MessageBox.Show($"Welcome to HertzPlay!{finalUser}😁", "Welcome🎶🎙️",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Set the current user to Homepage screen
                _homePage.setCurrentUserloggedin(finalUser);

                //Homepage window will show
                _homePage.Show();

                //Hide current window
                this.Hide();
            }
        }
        catch (Exception err)
        {

            MessageBox.Show($"An unexpected error occurred during login: {err.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private bool IsLoginValid(string username, string password, out string fullName)
    {
        fullName = string.Empty;

        //dynamically fetch the database textfile without explicitly inoutting the exact filepath
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database.txt"); 
        //make the username char lowercase incase user uses caps lock
        string inputUser = username.Trim().ToLower();
        string inputPass = password.Trim();
        try
        {
            // Check if file exists first 
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"File not found at: {filePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } 
            using (var reader = new StreamReader(filePath))
            {
                if (reader.Peek() >= 0) 
                {
                    string fileContents= reader.ReadToEnd();
                    string[] lines = fileContents.Split(new[] { "\r\n", "\r", "\n" },
                        StringSplitOptions.RemoveEmptyEntries);

                    foreach (string  line in lines)
                    {
                        string[] parts = line.Split(','); ;
                        if (parts.Length==2)
                        {
                            string fileUserName = parts[0].Trim();
                            string filePassword = parts[1].Trim();

                            if (fileUserName.Equals(inputUser, StringComparison.Ordinal) && 
                                filePassword.Equals(inputPass, StringComparison.Ordinal))
                            {
                                fullName= fileUserName;
                                return true;
                            }
                        }
                    }
                }
            }
            
            
        }
        catch (Exception err)
        {
            //Print issue to Visual studio output window for better debugging
            Debug.WriteLine($"Validation Error: {err.Message}");
            fullName = err.Message;
            return false;
        }
        MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;

    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
        RegistrationPage registrationPage = new RegistrationPage(this);

        registrationPage.Show();
        this.Hide();
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void txtPassword_TextChanged(object sender, EventArgs e)
    {

    }

    private void Form1_Load_1(object sender, EventArgs e)
    {

    }

    private void ApplyCustomStyling()
    {
        // 1. Form Window Background
        this.BackColor = Color.FromArgb(18, 12, 32);

        // 2. Big Title Label (lblTitle)
        lblTitle.Text = "Login";
        lblTitle.Font = new Font("Segoe UI", 28, FontStyle.Bold | FontStyle.Italic);
        lblTitle.ForeColor = Color.FromArgb(190, 130, 255); // Bright Neon Lavender
        lblTitle.AutoSize = true;

        // 3. Field Labels Customization
        lblUserN.ForeColor = Color.FromArgb(220, 220, 240);
        lblUserN.Font = new Font("Segoe UI", 11, FontStyle.Regular);

        lblUserPass.ForeColor = Color.FromArgb(220, 220, 240);
        lblUserPass.Font = new Font("Segoe UI", 11, FontStyle.Regular);

        // 4. TextBoxes Fill & Borders
        txtUsername.BackColor = Color.FromArgb(32, 26, 52);
        txtUsername.ForeColor = Color.White;
        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Font = new Font("Segoe UI", 11);

        //Registration button label
        lblRegister.BackColor = Color.FromArgb(32, 26, 52);
        lblRegister.ForeColor = Color.White;
        lblRegister.BorderStyle = BorderStyle.FixedSingle;
        lblRegister.Font = new Font("Segoe UI", 11);

        txtPassword.BackColor = Color.FromArgb(32, 26, 52);
        txtPassword.ForeColor = Color.White;
        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.Font = new Font("Segoe UI", 11);

        // 5. Primary Login Button Styling
        btnLogin.BackColor = Color.FromArgb(130, 50, 210);
        btnLogin.ForeColor = Color.White;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.Font = new Font("Segoe UI", 12, FontStyle.Bold);

        // 6. Register Button Styling (Matches Login Button)
        btnRegister.BackColor = Color.FromArgb(130, 50, 210);
        btnRegister.ForeColor = Color.White;
        btnRegister.FlatStyle = FlatStyle.Flat;
        btnRegister.FlatAppearance.BorderSize = 0;
        btnRegister.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        btnRegister.Size = btnLogin.Size;
    }

    private void txtUsername_TextChanged(object sender, EventArgs e)
    {

    }
}
