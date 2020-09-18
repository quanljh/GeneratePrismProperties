using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Quan;

namespace GeneratePrismProperties
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Setup the Quan Framework
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddFileLogger()
                .Build();

            var mainWindow = new MainWindow();

            mainWindow.Show();
        }
    }
}
