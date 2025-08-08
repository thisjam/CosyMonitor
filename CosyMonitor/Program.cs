 
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

            // ���ø� DPI ֧��
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            // ���ߣ�HighDpiMode.PerMonitorV2���Ƽ����� .NET 4.7+ / .NET Core��
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();


            Application.Run(new MainForm());
           
        }
    }
}