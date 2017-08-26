using System.ComponentModel.Composition;
using System.Windows.Documents;
using Plainion.IO.RealFS;
using Plainion.OneNote.Services;
using Plainion.Windows.Controls.Text;
using Plainion.Windows.Mvvm;

namespace Plainion.OneNote.ViewModels
{
    [Export]
    class NoteBookViewModel : BindableBase
    {
        private FileSystemDocumentStore myDocumentStore;

        [ImportingConstructor]
        public NoteBookViewModel(ProjectService projectService)
        {
            var fs = new FileSystemImpl();
            var storeFolder = fs.Directory(projectService.DocumentStoreFolder);
            var isNewStore = !storeFolder.Exists;
            if (!storeFolder.Exists)
            {
                storeFolder.Create();
            }

            DocumentStore = new FileSystemDocumentStore(storeFolder);
            DocumentStore.Initialize();

            if (isNewStore)
            {
                var doc = DocumentStore.Create("/Welcome");
                doc.Body.Blocks.Add(new Paragraph(new Run("Welcome!")));
            }

            DocumentStore.SaveChanges();
        }

        public FileSystemDocumentStore DocumentStore
        {
            get { return myDocumentStore; }
            private set { SetProperty(ref myDocumentStore, value); }
        }
    }
}
