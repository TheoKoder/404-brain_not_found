namespace loginForm_1;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        //instance of homepage
        HomePage homePage = new HomePage();
        //Injection of the homeform into the loginform, to load once successful login
        Application.Run(new Form1(homePage));
    }    
}