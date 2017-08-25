using System.ComponentModel.Composition;
using Plainion.OneNote.ViewModels;
using Plainion.Windows.Mvvm;

namespace Plainion.OneNote
{
    [Export]
    class ShellViewModel : BindableBase
    {
        private string myTitle;

        [ImportingConstructor]
        public ShellViewModel()
        {
            //var args = Environment.GetCommandLineArgs();
            //if (args.Length > 1)
            //{
            //    myBuildService.InitializeBuildDefinition(args[1]);
            //}
            //else
            //{
            //    myBuildService.InitializeBuildDefinition(null);
            //}

            Title = string.Format("Project: {0}", "<unknown>");
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
