using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Input;
using Plainion.OneNote.Services;
using Plainion.OneNote.ViewModels;
using Plainion.Windows.Mvvm;

namespace Plainion.OneNote
{
    [Export]
    class ShellViewModel : BindableBase
    {
        private string myTitle;
        private bool mySaved;

        [ImportingConstructor]
        public ShellViewModel(ProjectService projectService)
        {
            Title = string.Format("Project: {0}", Path.GetFileName(projectService.Name));

            SaveCommand = new DelegateCommand(() =>
            {
                projectService.Save();
                Saved = true;
                Saved = false;
            });
        }

        public string Title
        {
            get { return myTitle; }
            set { SetProperty(ref myTitle, value); }
        }

        [Import]
        public NoteBookViewModel NoteBookViewModel { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public bool Saved
        {
            get { return mySaved; }
            set { SetProperty(ref mySaved, value); }
        }
    }
}
