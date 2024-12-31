using Archivist.Modules;
using Archivist.Tests.BaseClasses;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Archivist.Tests.Modules
{
    public class CanisterModuleTests : TestBaseClass<CanisterModule>
    {
        public CanisterModuleTests()
        {
            _TestClass = new CanisterModule();
            TestObject = new CanisterModule();
        }

        private readonly CanisterModule _TestClass;

        [Fact]
        public void CanCallLoad()
        {
            // Arrange
            IServiceCollection ServiceDescriptors = new ServiceCollection();

            // Act
            _TestClass.Load(ServiceDescriptors);

            // Assert
            Assert.NotNull(ServiceDescriptors);
        }

        [Fact]
        public void CanCallLoadWithNullServiceDescriptors() => _TestClass.Load(default);

        [Fact]
        public void CanGetOrder()
        {
            // Assert
            _ = Assert.IsType<int>(_TestClass.Order);

            Assert.Equal(0, _TestClass.Order);
        }
    }
}