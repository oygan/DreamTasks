using Dream.Business.Abstract;
using Dream.Business.Services;

namespace Dream.UnitTests.Common
{
    /// <summary>
    /// Create test services
    /// </summary>
    public class ServiceFactory
    {
        public static IProjectEditorService CreateProjectEditorService()
        {
            return new ProjectEditorService(TestContextFactory.CreateTestContext());
        }

        public static ITaskEditorService CreateTaskEditorService()
        {
            return new TaskEditorService(TestContextFactory.CreateTestContext());
        }

        public static IWorkflowService CreateWorkflowService()
        {
            return new WorkflowService(TestContextFactory.CreateTestContext());
        }

        public static ITaskSearchService CreateTaskSearchService()
        {
            return new TaskSearchService(TestContextFactory.CreateTestContext());
        }
    }
}