using System.ComponentModel.Composition;
using System.Windows.Documents;
using Plainion.IO.MemoryFS;
using Plainion.Windows.Controls.Text;
using Plainion.Windows.Mvvm;

namespace Plainion.OneNote.ViewModels
{
    [Export]
    class NoteBookViewModel : BindableBase
    {
        private FileSystemDocumentStore myDocumentStore;

        public NoteBookViewModel()
        {
            var fs = new FileSystemImpl();

            DocumentStore = new FileSystemDocumentStore(fs.Directory("/x"));
            DocumentStore.Initialize();

            DocumentStore.Create("/User documentation/Installation").Body.AddText("Installation");
            DocumentStore.Create("/User documentation/Getting started").Body.AddText("Getting started");
            DocumentStore.Create("/User documentation/FAQ").Body.AddText("Frequenty Asked Questions");
            DocumentStore.Create("/Developer documentation/Getting started").Body.AddText("Getting started as a developer");
            DocumentStore.Create("/Developer documentation/HowTos/MVC with F#").Body.AddText("MVC with F#");
            DocumentStore.Create("/Developer documentation/HowTos/WebApi with F#").Body.AddText("WebApi with F#");
        }
        public FileSystemDocumentStore DocumentStore
        {
            get { return myDocumentStore; }
            private set { SetProperty(ref myDocumentStore, value); }
        }
    }
    static class FlowDocumentExtensions
    {
        public static FlowDocument AddText(this FlowDocument self, string text)
        {
            self.Blocks.Add(new Paragraph(new Run(text)));
            return self;
        }
    }
}
