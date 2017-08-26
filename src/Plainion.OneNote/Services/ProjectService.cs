using System;
using System.ComponentModel.Composition;
using System.IO;
using Microsoft.Win32;

namespace Plainion.OneNote.Services
{
    [Export]
    class ProjectService
    {
        public ProjectService()
        {
            Location = Path.GetFullPath(GetProject());

            if (!File.Exists(Location))
            {
                File.WriteAllText(Location, "placeholder");
            }

            Name = Path.GetFileNameWithoutExtension(Location);
            DocumentStoreFolder = Path.Combine(Path.GetDirectoryName(Location), "." + Name);
        }

        private string GetProject()
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

        public string Location { get; private set; }

        public string Name { get; private set; }

        public string DocumentStoreFolder { get; private set; }
    }
}
