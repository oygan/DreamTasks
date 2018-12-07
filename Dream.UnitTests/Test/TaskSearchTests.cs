using Dream.Business.TransferModels;
using Dream.UnitTests.Common;
using Xunit;

namespace Dream.UnitTests.Test
{
    public class TaskSearchTests : BaseTest
    {
        [Fact]
        public void FilterTasksTest()
        {
            // == arrange
            var projectEditorService = ServiceFactory.CreateProjectEditorService();
            var project = projectEditorService.AddProjectAsync(new NewProjectDto() {Title = "Title", Description = "Description"}).Result;

            var taskEditorService = ServiceFactory.CreateTaskEditorService();
            for (int i = 0; i < 10; i++)
            {
                var added = taskEditorService.AddTaskAsync(new NewTaskDto()
                {
                    ProjectId = project.Id,
                    Description = "TaskDescription",
                    Title = "TaskTitle",
                    Priority = i % 2
                }).Result;
            }

            // == act
            var searchService = ServiceFactory.CreateTaskSearchService();
            var page0 = searchService.GetTasksAsync(new TaskSearchFilterDto() { ProjectId = project.Id, PageSize = 7, PageNumber = 0 }).Result;
            var page1 = searchService.GetTasksAsync(new TaskSearchFilterDto() { ProjectId = project.Id, PageSize = 7, PageNumber = 1 }).Result;
            var page2 = searchService.GetTasksAsync(new TaskSearchFilterDto() { ProjectId = project.Id, PageSize = 7, PageNumber = 2 }).Result;

            var priorityPage = searchService.GetTasksAsync(new TaskSearchFilterDto() { ProjectId = project.Id, Priority = 1, PageSize = 100, PageNumber = 0 }).Result;


            // == assert
            Assert.Equal(7, page0.Tasks.Count);
            Assert.Equal(3, page1.Tasks.Count);
            Assert.Equal(0, page2.Tasks.Count);

            Assert.Equal(5, priorityPage.Tasks.Count);
        }

        [Fact]
        public void OrderTasksTest()
        {
            // == arrange
            var projectEditorService = ServiceFactory.CreateProjectEditorService();
            var project = projectEditorService.AddProjectAsync(new NewProjectDto() { Title = "Title", Description = "Description" }).Result;

            var taskEditorService = ServiceFactory.CreateTaskEditorService();
            for (int i = 0; i < 10; i++)
            {
                var added = taskEditorService.AddTaskAsync(new NewTaskDto()
                {
                    ProjectId = project.Id,
                    Description = "TaskDescription",
                    Title = "TaskTitle",
                    Priority = 10 - i
                }).Result;
            }

            // == act
            var searchService = ServiceFactory.CreateTaskSearchService();
            var orderedByPriority = searchService.GetTasksAsync(new TaskSearchFilterDto() { Order = OrderTaskEnum.Priority, ProjectId = project.Id, PageSize = 10, PageNumber = 0 }).Result;
            var orderedByDate = searchService.GetTasksAsync(new TaskSearchFilterDto() { Order = OrderTaskEnum.CreateDate, ProjectId = project.Id, PageSize = 10, PageNumber = 0 }).Result;
            
            // == assert
            Assert.Equal(10, orderedByPriority.Tasks.Count);
            Assert.Equal(1, orderedByPriority.Tasks[0].Priority);
            Assert.Equal(10, orderedByPriority.Tasks[9].Priority);

            Assert.Equal(10, orderedByDate.Tasks.Count);
            Assert.Equal(10, orderedByDate.Tasks[0].Priority);
            Assert.Equal(1, orderedByDate.Tasks[9].Priority);
        }
    }
}