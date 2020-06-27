using System;
using System.Linq;
using Caliburn.Micro;
using FluentValidation;
using Hoursly.Common.Extensions.Models;
using Hoursly.Common.Extensions.Validations;
using Hoursly.Common.Helpers;
using Hoursly.Common.Messages;
using Hoursly.Entities;
using Hoursly.Mappers.Common;
using Hoursly.Models;
using Hoursly.Repositories.Projects;

namespace Hoursly.ViewModels
{
    public class ProjectsViewModel : Screen
    {
        private readonly IMapper<Project, ProjectModel> _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IValidator<ProjectModel> _projectValidator;
        private readonly ISystemMessageSender _systemMessageSender;

        private bool _editMode;
        private string _editOrCreateText = TextConstants.Create;
        private ProjectModel _selectedProject = ProjectModel.Empty();

        public ProjectsViewModel(
            IProjectRepository projectRepository,
            IMapper<Project, ProjectModel> mapper,
            IValidator<ProjectModel> projectValidator,
            ISystemMessageSender systemMessageSender)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectValidator = projectValidator;
            _systemMessageSender = systemMessageSender;
        }

        public BindableCollection<ProjectModel> Projects => LoadProjects();

        public bool EditMode
        {
            get => _editMode;
            set
            {
                _editMode = value;
                EditOrCreateText = _editMode ? TextConstants.Update : TextConstants.Create;
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

        public string SupervisorEmail
        {
            get => _selectedProject.SupervisorEmail;
            set
            {
                _selectedProject.SupervisorEmail = value;
                NotifyOfPropertyChange(() => _selectedProject.SupervisorEmail);
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
                _selectedProject.TaskLimit,
                _selectedProject.SupervisorEmail);
            _projectRepository.Update(project);
            _projectRepository.Commit();

            NotifyOfPropertyChange(() => Projects);
            _systemMessageSender.Send($"Project {SelectedProject.Name} updated");
        }

        private void Create()
        {
            var validationResult = _projectValidator.Validate(SelectedProject);
            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.GetErrorsSummary();
                _systemMessageSender.Send(errorMessage);
                return;
            }

            var project = Project.Create(
                SelectedProject.Name,
                SelectedProject.StartDate,
                SelectedProject.EndDate,
                SelectedProject.Priority,
                SelectedProject.TaskLimit,
                SelectedProject.SupervisorEmail);
            _projectRepository.Create(project);
            _projectRepository.Commit();

            NotifyOfPropertyChange(() => Projects);
            _systemMessageSender.Send($"Project {SelectedProject.Name} added");
        }

        public void Delete()
        {
            SelectedProject.ThrowIfPublicIdIsNullOrEmpty();
            _projectRepository.Delete(_selectedProject.PublicId);
            _projectRepository.Commit();
            _systemMessageSender.Send($"Project {SelectedProject.Name} removed");
            NotifyOfPropertyChange(() => Projects);
        }

        public void ClearSelection()
        {
            SelectedProject = ProjectModel.Empty();
            EditMode = false;
        }


        private BindableCollection<ProjectModel> LoadProjects()
        {
            var projects = _projectRepository.GetAll();
            var projectModels = projects.Select(project => _mapper.MapFrom(project));

            return new BindableCollection<ProjectModel>(projectModels);
        }
    }
}