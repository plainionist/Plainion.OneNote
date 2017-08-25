using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Plainion.Composition;
using Plainion.Windows;

namespace Plainion.OneNote
{
    public partial class App : Application
    {
        private Composer myComposer;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            new UnhandledExceptionHook(this);

            Application.Current.Exit += OnShutdown;

            myComposer = new Composer();
            myComposer.Add(new AssemblyCatalog(GetType().Assembly));

            myComposer.Compose();

            Application.Current.MainWindow = myComposer.Resolve<Shell>();
            Application.Current.MainWindow.Show();
        }

        private void OnShutdown(object sender, ExitEventArgs e)
        {
            myComposer.Dispose();
        }
    }
}
