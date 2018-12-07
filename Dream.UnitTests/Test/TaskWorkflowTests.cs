using Dream.Business.TransferModels;
using Dream.Models.Enums;
using Dream.UnitTests.Common;
using Xunit;

namespace Dream.UnitTests.Test
{
    public class TaskWorkflowTests : BaseTest
    {
        [Fact]
        public void StartCloseReopen_WorkflowTaskTest()
        {
            // == arrange
            var projectEditorService = ServiceFactory.CreateProjectEditorService();
            var taskEditorService = ServiceFactory.CreateTaskEditorService();
            var project = projectEditorService.AddProjectAsync(new NewProjectDto() { Title = "Title", Description = "Description" }).Result;
            var added = taskEditorService.AddTaskAsync(new NewTaskDto()
                { ProjectId = project.Id, Description = "TaskDescription", Title = "TaskTitle" }).Result;

            // == act
            var workflowService = ServiceFactory.CreateWorkflowService();
            var started = workflowService.StartTaskAsync(added.task.Id).Result;
            var closed = workflowService.CloseTaskAsync(added.task.Id).Result;
            var reopened = workflowService.ReopenTaskAsync(added.task.Id).Result;

            // == assert
            Assert.Equal(TaskStatuses.Created, added.task.FlowStatus);
            Assert.Equal(TaskStatuses.InProgress, started.task.FlowStatus);
            Assert.Equal(TaskStatuses.Closed, closed.task.FlowStatus);
            Assert.Equal("Available statuses: InProgress", reopened.error);
        }

        [Fact]
        public void StartReopenClose_WorkflowTaskTest()
        {
            // == arrange
            var projectEditorService = ServiceFactory.CreateProjectEditorService();
            var taskEditorService = ServiceFactory.CreateTaskEditorService();
            var project = projectEditorService.AddProjectAsync(new NewProjectDto() { Title = "Title", Description = "Description" }).Result;
            var added = taskEditorService.AddTaskAsync(new NewTaskDto()
                { ProjectId = project.Id, Description = "TaskDescription", Title = "TaskTitle" }).Result;

            // == act
            var workflowService = ServiceFactory.CreateWorkflowService();
            var started = workflowService.StartTaskAsync(added.task.Id).Result;
            var reopened = workflowService.ReopenTaskAsync(added.task.Id).Result;
            var closed = workflowService.CloseTaskAsync(added.task.Id).Result;

            // == assert
            Assert.Equal(TaskStatuses.Created, added.task.FlowStatus);
            Assert.Equal(TaskStatuses.InProgress, started.task.FlowStatus);
            Assert.Equal(TaskStatuses.Closed, closed.task.FlowStatus);
            Assert.Equal(TaskStatuses.Created, reopened.task.FlowStatus);
        }

        [Fact]
        public void Close_WorkflowTaskTest()
        {
            // == arrange
            var projectEditorService = ServiceFactory.CreateProjectEditorService();
            var taskEditorService = ServiceFactory.CreateTaskEditorService();
            var project = projectEditorService.AddProjectAsync(new NewProjectDto() { Title = "Title", Description = "Description" }).Result;
            var added = taskEditorService.AddTaskAsync(new NewTaskDto()
                { ProjectId = project.Id, Description = "TaskDescription", Title = "TaskTitle" }).Result;

            // == act
            var workflowService = ServiceFactory.CreateWorkflowService();
            var closed = workflowService.CloseTaskAsync(added.task.Id).Result;

            // == assert
            Assert.Equal(TaskStatuses.Created, added.task.FlowStatus);
            Assert.Equal(TaskStatuses.Closed, closed.task.FlowStatus);
        }
    }
}