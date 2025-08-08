 
namespace CosyMonitor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            // 启用高 DPI 支持
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            // 或者：HighDpiMode.PerMonitorV2（推荐，需 .NET 4.7+ / .NET Core）
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();


            Application.Run(new MainForm());
           
        }
    }
}