using System;
using System.Linq;
using Caliburn.Micro;
using FluentValidation;
using Hoursly.Common.Extensions.Validations;
using Hoursly.Common.Messages;
using Hoursly.Entities;
using Hoursly.Mappers.Common;
using Hoursly.Models;
using Hoursly.Repositories.Projects;

namespace Hoursly.ViewModels
{
    public class ReportsViewModel : Screen
    {
        private readonly IMapper<Project, ProjectTimeLogsModel> _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly ISystemMessageSender _systemMessageSender;
        private readonly IValidator<TimeLogModel> _timeLogsValidator;

        private ProjectTimeLogsModel _selectedProject;
        private TimeLogModel _selectedTimeLog = TimeLogModel.Empty();

        public ReportsViewModel(
            IProjectRepository projectRepository,
            IMapper<Project, ProjectTimeLogsModel> mapper,
            IValidator<TimeLogModel> timeLogsValidator,
            ISystemMessageSender systemMessageSender)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _timeLogsValidator = timeLogsValidator;
            _systemMessageSender = systemMessageSender;
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

        public TimeLogModel SelectedTimeLog
        {
            get => _selectedTimeLog;
            set
            {
                _selectedTimeLog = value;
                NotifyOfPropertyChange(() => SelectedTimeLog.StartDate);
                NotifyOfPropertyChange(() => SelectedTimeLog.EndDate);
            }
        }

        public DateTime StartDate
        {
            get => _selectedTimeLog.StartDate;
            set
            {
                _selectedTimeLog.StartDate = value;
                NotifyOfPropertyChange(() => _selectedTimeLog.StartDate);
            }
        }

        public DateTime EndDate
        {
            get => _selectedTimeLog.EndDate;
            set
            {
                _selectedTimeLog.EndDate = value;
                NotifyOfPropertyChange(() => _selectedTimeLog.EndDate);
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
            var validationResult = _timeLogsValidator.Validate(SelectedTimeLog);
            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.GetErrorsSummary();
                _systemMessageSender.Send(errorMessage);
                return;
            }

            var project = _projectRepository.Get(_selectedProject.PublicId);
            project.LogTime(_selectedTimeLog.StartDate, _selectedTimeLog.EndDate);
            _projectRepository.Commit();

            NotifyOfPropertyChange(() => Projects);

            SelectedTimeLog = TimeLogModel.Empty();
            _systemMessageSender.Send("Time logged");
        }
    }
}