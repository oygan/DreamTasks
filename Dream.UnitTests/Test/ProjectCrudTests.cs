using Dream.Business.TransferModels;
using Dream.UnitTests.Common;
using Xunit;

namespace Dream.UnitTests.Test
{
    public class ProjectCrudTests : BaseTest
    {
        [Fact]
        public void AddProjectTest()
        {
            // == arrange
            var editor = ServiceFactory.CreateProjectEditorService();

            // == act
            var project = editor.AddProjectAsync(new NewProjectDto() {Title = "Title", Description = "Description" }).Result;

            // == assert
            Assert.Equal("Title", project.Title);
            Assert.Equal("Description", project.Description);
        }

        [Fact]
        public void GetProjectTest()
        {
            // == arrange
            var editor = ServiceFactory.CreateProjectEditorService();

            // == act
            var project = editor.AddProjectAsync(new NewProjectDto() { Title = "TitleGet", Description = "DescriptionGet" }).Result;
            var getResult = editor.GetProjectAsync(project.Id).Result;

            // == assert
            Assert.Equal("TitleGet", getResult.project.Title);
            Assert.Equal("DescriptionGet", getResult.project.Description);
            Assert.Equal(project.CreateDate, getResult.project.CreateDate);
            Assert.Null(getResult.error);
        }

        [Fact]
        public void NotFoundProjectTest()
        {
            // == arrange
            var editor = ServiceFactory.CreateProjectEditorService();

            // == act
            var getResult = editor.GetProjectAsync(-1).Result;

            // == assert
            Assert.Null(getResult.project);
            Assert.Equal("project not found", getResult.error);
        }

        [Fact]
        public void RemoveProjectTest()
        {
            // == arrange
            var editor = ServiceFactory.CreateProjectEditorService();

            // == act
            var project = editor.AddProjectAsync(new NewProjectDto() { Title = "TitleGet", Description = "DescriptionGet" }).Result;
            var deleteResult = editor.DeleteProjectAsync(project.Id).Result;
            var getResult = editor.GetProjectAsync(deleteResult.project.Id).Result;

            // == assert
            Assert.Null(getResult.project);
            Assert.Equal("project not found", getResult.error);
        }

        [Fact]
        public void UpdateProjectTest()
        {
            // == arrange
            var editor = ServiceFactory.CreateProjectEditorService();

            // == act
            var project = editor.AddProjectAsync(new NewProjectDto() { Title = "Title1", Description = "Description1" }).Result;
            var updateResult = editor.UpdateProjectAsync(project.Id, new NewProjectDto() { Title = "Title2", Description = "Description2" }).Result;
            var getResult = editor.GetProjectAsync(project.Id).Result;

            // == assert
            Assert.Equal("Title2", updateResult.project.Title);
            Assert.Equal("Description2", updateResult.project.Description);
            Assert.Equal("Title2", getResult.project.Title);
            Assert.Equal("Description2", getResult.project.Description);
            Assert.Equal(project.CreateDate, updateResult.project.CreateDate);
            Assert.NotEqual(project.ModifyDate, updateResult.project.ModifyDate);
            Assert.Null(updateResult.error);
        }
    }
}
