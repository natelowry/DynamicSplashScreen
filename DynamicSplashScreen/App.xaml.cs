using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DynamicSplashScreen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ISplashScreen splashScreen;

        private ManualResetEvent ResetSplashCreated;
        private Thread SplashThread;
        protected override void OnStartup(StartupEventArgs e)
        {
            ResetSplashCreated = new ManualResetEvent(false);
            SplashThread = new Thread(ShowSplash);
            SplashThread.SetApartmentState(ApartmentState.STA);
            SplashThread.IsBackground = true;
            SplashThread.Name = "Splash Screen";
            SplashThread.Start();

            ResetSplashCreated.WaitOne();
            base.OnStartup(e);
        }

        private void ShowSplash()
        {
            AnimatedSplashScreenWindow animatedSplashScreenWindow = new AnimatedSplashScreenWindow();
            splashScreen = animatedSplashScreenWindow;

            animatedSplashScreenWindow.Show();
            ResetSplashCreated.Set();
            System.Windows.Threading.Dispatcher.Run();
        }
    }
}
