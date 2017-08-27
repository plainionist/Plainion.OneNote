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
        private DocumentStore myDocumentStore;

        [ImportingConstructor]
        public NoteBookViewModel(ProjectService projectService)
        {
            DocumentStore = projectService.DocumentStore;
        }

        public DocumentStore DocumentStore
        {
            get { return myDocumentStore; }
            private set { SetProperty(ref myDocumentStore, value); }
        }
    }
}
