using Dream.Business.TransferModels;
using Dream.Models.Enums;
using Dream.UnitTests.Common;
using Xunit;

namespace Dream.UnitTests.Test
{
    public class TaskCrudTests : BaseTest
    {
        [Fact]
        public void AddTaskTest()
        {
            // == arrange
            var projectEditorService = ServiceFactory.CreateProjectEditorService();
            var taskEditorService = ServiceFactory.CreateTaskEditorService();
            var project = projectEditorService.AddProjectAsync(new NewProjectDto() { Title = "Title", Description = "Description" }).Result;

            // == act
            var added = taskEditorService.AddTaskAsync(new NewTaskDto()
                {ProjectId = project.Id, Description = "TaskDescription", Title = "TaskTitle" }).Result;

            // == assert
            Assert.Equal("TaskTitle", added.task.Title);
            Assert.Equal("TaskDescription", added.task.Description);
            Assert.Equal(project.Id, added.task.ProjectId);
            Assert.Equal(TaskStatuses.Created, added.task.FlowStatus);
        }

        [Fact]
        public void UpdateAndGetTaskTest()
        {
            // == arrange
            var projectEditorService = ServiceFactory.CreateProjectEditorService();
            var taskEditorService = ServiceFactory.CreateTaskEditorService();
            var project1 = projectEditorService.AddProjectAsync(new NewProjectDto() { Title = "Title1", Description = "Description1" }).Result;
            var project2 = projectEditorService.AddProjectAsync(new NewProjectDto() { Title = "Title2", Description = "Description2" }).Result;
            var added = taskEditorService.AddTaskAsync(new NewTaskDto()
                { ProjectId = project1.Id, Description = "TaskDescription", Title = "TaskTitle" }).Result;

            // == act
            var update = taskEditorService.UpdateTaskAsync(added.task.Id, new NewTaskDto()
                { ProjectId = project2.Id, Description = "TaskDescription2", Title = "TaskTitle2" }).Result;
            var get = taskEditorService.GetTaskAsync(update.task.Id).Result;

            // == assert
            Assert.Equal("TaskTitle2", get.task.Title);
            Assert.Equal("TaskDescription2", get.task.Description);
            Assert.Equal(project2.Id, get.task.ProjectId);
            Assert.Equal(TaskStatuses.Created, get.task.FlowStatus);
        }

        [Fact]
        public void RemoveTaskTest()
        {
            // == arrange
            var projectEditorService = ServiceFactory.CreateProjectEditorService();
            var taskEditorService = ServiceFactory.CreateTaskEditorService();
            var project = projectEditorService.AddProjectAsync(new NewProjectDto() { Title = "Title", Description = "Description" }).Result;

            // == act
            var added = taskEditorService.AddTaskAsync(new NewTaskDto()
                { ProjectId = project.Id, Description = "TaskDescription", Title = "TaskTitle" }).Result;
            var removed = taskEditorService.DeleteTaskAsync(added.task.Id).Result;
            var get = taskEditorService.GetTaskAsync(removed.task.Id).Result;

            // == assert
            Assert.Null(get.task);
            Assert.Equal("task not found", get.error);
        }
    }
}