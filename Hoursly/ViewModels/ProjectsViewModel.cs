using System;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using FluentValidation;
using Hoursly.Common.Extensions.Models;
using Hoursly.Common.Extensions.Validations;
using Hoursly.Entities;
using Hoursly.Mappers.Common;
using Hoursly.Models;
using Hoursly.Repositories;

namespace Hoursly.ViewModels
{
    public class ProjectsViewModel : Screen
    {
        private readonly IMapper<Project, ProjectModel> _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IValidator<ProjectModel> _projectValidator;

        private bool _editMode;


        private string _editOrCreateText;
        private ProjectModel _selectedProject = ProjectModel.Empty();

        public ProjectsViewModel(
            IProjectRepository projectRepository,
            IMapper<Project, ProjectModel> mapper,
            IValidator<ProjectModel> projectValidator)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectValidator = projectValidator;
        }

        public BindableCollection<ProjectModel> Projects => LoadProjects();

        public bool EditMode
        {
            get => _editMode;
            set
            {
                _editMode = value;
                EditOrCreateText = _editMode ? "Edit" : "Create";
                NotifyOfPropertyChange(() => EditMode);
            }
        }

        public string EditOrCreateText
        {
            get => _editOrCreateText;
            set
            {
                _editOrCreateText = value;
                NotifyOfPropertyChange(() => EditOrCreateText);
            }
        }

        public string Name
        {
            get => _selectedProject.Name;
            set
            {
                _selectedProject.Name = value;
                NotifyOfPropertyChange(() => _selectedProject.Name);
            }
        }

        public ProjectModel SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value ?? ProjectModel.Empty();
                NotifyOfPropertyChange(() => _selectedProject.PublicId);
                NotifyOfPropertyChange(() => _selectedProject.Name);
                NotifyOfPropertyChange(() => _selectedProject.StartDate);
                NotifyOfPropertyChange(() => _selectedProject.Priority);
                NotifyOfPropertyChange(() => _selectedProject.EndDate);
                NotifyOfPropertyChange(() => _selectedProject.TaskLimit);
                EditMode = _selectedProject.PublicId != Guid.Empty;
            }
        }

        public Guid PublicId
        {
            get => _selectedProject.PublicId;
            set
            {
                _selectedProject.PublicId = value;
                NotifyOfPropertyChange(() => _selectedProject.PublicId);
            }
        }

        public DateTime StartDate
        {
            get => _selectedProject.StartDate;
            set
            {
                _selectedProject.StartDate = value;
                NotifyOfPropertyChange(() => _selectedProject.StartDate);
            }
        }

        public DateTime? EndDate
        {
            get => _selectedProject.EndDate;
            set
            {
                _selectedProject.EndDate = value;
                NotifyOfPropertyChange(() => _selectedProject.EndDate);
            }
        }

        public ProjectPriority Priority
        {
            get => _selectedProject.Priority;
            set
            {
                _selectedProject.Priority = value;
                NotifyOfPropertyChange(() => _selectedProject.Priority);
            }
        }

        public int? TaskLimit
        {
            get => _selectedProject.TaskLimit;
            set
            {
                _selectedProject.TaskLimit = value;
                NotifyOfPropertyChange(() => _selectedProject.TaskLimit);
            }
        }


        public void CreateOrUpdate()
        {
            if (EditMode)
                Update();
            else
                Create();
        }

        private void Update()
        {
            var project = _projectRepository.Get(_selectedProject.PublicId);
            project.Update(
                _selectedProject.Name,
                _selectedProject.StartDate,
                _selectedProject.EndDate,
                _selectedProject.Priority,
                _selectedProject.TaskLimit);
            _projectRepository.Update(project);
            _projectRepository.Commit();

            NotifyOfPropertyChange(() => Projects);
            MessageBox.Show($"Project {SelectedProject.Name} updated");
        }

        private void Create()
        {
            var validationResult = _projectValidator.Validate(SelectedProject);
            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.GetErrorsSummary();
                MessageBox.Show(errorMessage);
                return;
            }

            var project = Project.Create(
                SelectedProject.Name,
                SelectedProject.StartDate,
                SelectedProject.EndDate,
                SelectedProject.Priority,
                SelectedProject.TaskLimit);
            _projectRepository.Create(project);
            _projectRepository.Commit();

            NotifyOfPropertyChange(() => Projects);
            MessageBox.Show($"Project {SelectedProject.Name} added");
        }

        public void Delete()
        {
            SelectedProject.ThrowIfPublicIdIsNullOrEmpty();
            _projectRepository.Delete(_selectedProject.PublicId);
            _projectRepository.Commit();
            MessageBox.Show($"Project {SelectedProject.Name} Removed");
            NotifyOfPropertyChange(() => Projects);
        }

        private BindableCollection<ProjectModel> LoadProjects()
        {
            var projects = _projectRepository.GetAll();
            var projectModels = projects.Select(project => _mapper.MapFrom(project));

            return new BindableCollection<ProjectModel>(projectModels);
        }
    }
}