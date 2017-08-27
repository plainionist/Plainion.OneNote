using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;
using Microsoft.Win32;
using Plainion.IO.RealFS;
using Plainion.Windows.Controls.Text;

namespace Plainion.OneNote.Services
{
    [Export]
    class ProjectService
    {
        private FileSystemDocumentStore myDocumentStore;
        private DispatcherTimer myAutoSaveTimer;

        public ProjectService()
        {
            Location = Path.GetFullPath(GetProject());

            if (!File.Exists(Location))
            {
                File.WriteAllText(Location, "placeholder");
            }

            Name = Path.GetFileNameWithoutExtension(Location);
            DocumentStoreFolder = Path.Combine(Path.GetDirectoryName(Location), "." + Name);

            myDocumentStore = InitializeDocumentStore(DocumentStoreFolder);

            myAutoSaveTimer = new DispatcherTimer(TimeSpan.FromSeconds(60), DispatcherPriority.Background, new EventHandler(DoAutoSave), Application.Current.Dispatcher);
        }

        private static string GetProject()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                if (File.Exists(args[1]))
                {
                    return args[1];
                }

                throw new FileNotFoundException("Project does not exist: " + args[1]);
            }

            var dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.Filter = "Plainion.OneNote project (*.p1n)|*.p1n";
            dlg.FilterIndex = 0;
            dlg.DefaultExt = ".p1n";
            dlg.CheckPathExists = true;
            dlg.CheckFileExists = false;
            dlg.AddExtension = true;

            if (dlg.ShowDialog() == true)
            {
                return dlg.FileName;
            }

            Environment.Exit(1);
            return null;
        }

        private static FileSystemDocumentStore InitializeDocumentStore(string folder)
        {
            var fs = new FileSystemImpl();
            var storeFolder = fs.Directory(folder);
            var isNewStore = !storeFolder.Exists;
            if (!storeFolder.Exists)
            {
                storeFolder.Create();
            }

            var store = new FileSystemDocumentStore(storeFolder);
            store.Initialize();

            if (isNewStore)
            {
                var doc = store.Create("/Welcome");
                doc.Body.Blocks.Add(new Paragraph(new Run("Welcome!")));

                store.SaveChanges();
            }

            return store;
        }

        private void DoAutoSave(object sender, EventArgs e)
        {
            Debug.WriteLine("Auto saving ...");
            myDocumentStore.SaveChanges();
        }

        public DocumentStore DocumentStore { get { return myDocumentStore; } }

        public string Location { get; private set; }

        public string Name { get; private set; }

        public string DocumentStoreFolder { get; private set; }

        public void Save()
        {
            myDocumentStore.SaveChanges();
        }
    }
}
