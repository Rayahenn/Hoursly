using System;
using Caliburn.Micro;
using Hoursly.Models;

namespace Hoursly.ViewModels
{
    public class ProjectsViewModel : Screen
    {
        private DateTime? _endDate;
        private string _name;
        private ProjectPriority _priority;

        private BindableCollection<ProjectModel> _projects = new BindableCollection<ProjectModel>
        {
            new ProjectModel("test 1", new DateTime(2011, 1, 1)),
            new ProjectModel("test 2", new DateTime(2011, 1, 1))
        };

        private DateTime _startDate = DateTime.Now;
        private int? _taskLimit;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => _name);
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                NotifyOfPropertyChange(() => _startDate);
            }
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                NotifyOfPropertyChange(() => _endDate);
            }
        }

        public ProjectPriority Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                NotifyOfPropertyChange(() => _priority);
            }
        }

        public BindableCollection<ProjectModel> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                NotifyOfPropertyChange(() => Projects);
            }
        }

        public int? TaskLimit
        {
            get => _taskLimit;
            set
            {
                _taskLimit = value;
                NotifyOfPropertyChange(() => _taskLimit);
            }
        }

        public void Create()
        {
            var project = new ProjectModel(Name, StartDate, EndDate, Priority, TaskLimit);
            Projects.Add(project);
        }
    }
}