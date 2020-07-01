using System;
using System.Linq;
using Caliburn.Micro;
using FluentValidation;
using Hoursly.Common.Extensions.Models;
using Hoursly.Common.Extensions.Validations;
using Hoursly.Entities;
using Hoursly.Infrastructure.Emails;
using Hoursly.Infrastructure.Messages;
using Hoursly.Mappers.Common;
using Hoursly.Models;
using Hoursly.Repositories.Projects;

namespace Hoursly.ViewModels
{
    public class ReportsViewModel : Screen
    {
        private readonly IEmailSender _emailSender;
        private readonly IMapper<Project, ProjectTimeLogsModel> _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly ISystemMessageSender _systemMessageSender;
        private readonly IValidator<TimeLogModel> _timeLogsValidator;

        private ProjectTimeLogsModel _selectedProject;

        public ReportsViewModel(
            IProjectRepository projectRepository,
            IMapper<Project, ProjectTimeLogsModel> mapper,
            IValidator<TimeLogModel> timeLogsValidator,
            ISystemMessageSender systemMessageSender,
            IEmailSender emailSender)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _timeLogsValidator = timeLogsValidator;
            _systemMessageSender = systemMessageSender;
            _emailSender = emailSender;
        }

        public BindableCollection<ProjectTimeLogsModel> Projects => LoadProjects();

        public ProjectTimeLogsModel SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                NotifyOfPropertyChange(() => SelectedProject);
            }
        }

        public TimeLogModel LogTimeForm
        {
            get => SelectedTimeLogToDelete;
            set
            {
                SelectedTimeLogToDelete = value;
                NotifyOfPropertyChange(() => LogTimeForm.StartDate);
                NotifyOfPropertyChange(() => LogTimeForm.EndDate);
            }
        }

        public TimeLogModel SelectedTimeLogToDelete { get; set; } = TimeLogModel.Empty();


        public DateTime StartDate
        {
            get => SelectedTimeLogToDelete.StartDate;
            set
            {
                SelectedTimeLogToDelete.StartDate = value;
                NotifyOfPropertyChange(() => SelectedTimeLogToDelete.StartDate);
            }
        }

        public DateTime EndDate
        {
            get => SelectedTimeLogToDelete.EndDate;
            set
            {
                SelectedTimeLogToDelete.EndDate = value;
                NotifyOfPropertyChange(() => SelectedTimeLogToDelete.EndDate);
            }
        }

        private BindableCollection<ProjectTimeLogsModel> LoadProjects()
        {
            var projects = _projectRepository.GetAll();
            var projectTimeLogsModels = projects.Select(project => _mapper.MapFrom(project));

            return new BindableCollection<ProjectTimeLogsModel>(projectTimeLogsModels);
        }

        public void Create()
        {
            var validationResult = _timeLogsValidator.Validate(LogTimeForm);
            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.GetErrorsSummary();
                _systemMessageSender.Send(errorMessage);
                return;
            }

            var project = _projectRepository.Get(_selectedProject.PublicId);
            project.LogTime(SelectedTimeLogToDelete.StartDate, SelectedTimeLogToDelete.EndDate);
            _projectRepository.Commit();

            _emailSender.Send(project.SupervisorEmail, LogTimeForm.StartDate, LogTimeForm.EndDate);

            NotifyOfPropertyChange(() => Projects);

            LogTimeForm = TimeLogModel.Empty();
            _systemMessageSender.Send("Time logged");
        }

        public void Delete()
        {
            SelectedTimeLogToDelete.ThrowIfPublicIdIsNullOrEmpty();
            SelectedProject.ThrowIfPublicIdIsNullOrEmpty();

            var project = _projectRepository.Get(_selectedProject.PublicId);
            project.RemoveLog(SelectedTimeLogToDelete.PublicId);
            _projectRepository.Commit();
            _systemMessageSender.Send("Log Removed removed");
            NotifyOfPropertyChange(() => Projects);
        }
    }
}