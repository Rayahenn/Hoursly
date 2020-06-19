using System;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Hoursly.Common.Extensions.Validations;
using Hoursly.Entities;
using Hoursly.Mappers.Common;
using Hoursly.Models;
using Hoursly.Repositories;
using Hoursly.Validators;

namespace Hoursly.ViewModels
{
    public class ProjectsViewModel : Screen
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper<Project, ProjectModel> _mapper;
        private DateTime? _endDate;
        private string _name;
        private ProjectPriority _priority;
        private BindableCollection<ProjectModel> _projects = new BindableCollection<ProjectModel>();
        private DateTime _startDate = DateTime.Now;
        private int? _taskLimit;

        public ProjectsViewModel(
            IProjectRepository projectRepository,
            IMapper<Project, ProjectModel> mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            LoadProjects();
        }

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
            var projectModel = new ProjectModel(
                Guid.NewGuid(), 
                Name,
                StartDate,
                EndDate,
                Priority,
                TaskLimit);

            var validator = new ProjectValidator();
            var validationResult = validator.Validate(projectModel);
            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.GetErrorsSummary();
                MessageBox.Show(errorMessage);
                return;
            }

            var project = Project.Create(
                projectModel.PublicId,
                projectModel.Name, 
                projectModel.StartDate,
                projectModel.EndDate,
                projectModel.Priority, 
                projectModel.TaskLimit);
            _projectRepository.Create(project);

            LoadProjects();
            MessageBox.Show($"Project {projectModel.Name} added");
        }

        private void LoadProjects()
        {
            var projects = _projectRepository.GetAll();
            var projectModels = projects.Select(project => _mapper.MapFrom(project));
            _projects.Clear();
            _projects.AddRange(projectModels);
        }
    }
}