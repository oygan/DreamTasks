using Xunit;

namespace Dream.UnitTests.Common
{
    [CollectionDefinition("Test collection")]
    public class CollectionFixtureExecutor : ICollectionFixture<CollectionFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}