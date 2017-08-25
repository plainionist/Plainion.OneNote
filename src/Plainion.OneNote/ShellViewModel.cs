using System.ComponentModel.Composition;
using System.IO;
using Plainion.OneNote.Services;
using Plainion.OneNote.ViewModels;
using Plainion.Windows.Mvvm;

namespace Plainion.OneNote
{
    [Export]
    class ShellViewModel : BindableBase
    {
        private string myTitle;

        [ImportingConstructor]
        public ShellViewModel(ProjectService projectService)
        {
            Title = string.Format("Project: {0}", Path.GetFileName(projectService.Name));
        }

        public string Title
        {
            get { return myTitle; }
            set { SetProperty(ref myTitle, value); }
        }

        [Import]
        public NoteBookViewModel NoteBookViewModel { get; private set; }
    }
}
