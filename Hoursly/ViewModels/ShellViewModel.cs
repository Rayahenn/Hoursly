using Caliburn.Micro;

namespace Hoursly.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {
            ShowReportsView();
        }

        public void ShowReportsView()
        {
            ActivateItem(new ReportsViewModel());
        }

        public void ShowProjectsView()
        {
            ActivateItem(new ProjectsViewModel());
        }
    }
}