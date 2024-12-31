using Archivist.Tests.BaseClasses;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Archivist.Tests.ExtensionMethods
{
    public class ServiceCollectionExtensionMethodsTests : TestBaseClass
    {
        protected override Type? ObjectType { get; } = typeof(ServiceCollectionExtensionMethods);

        [Fact]
        public void CanCallAddArchivist()
        {
            // Arrange
            IServiceCollection Services = new ServiceCollection();

            // Act
            IServiceCollection? Result = Services.AddArchivist();

            // Assert
            Assert.NotNull(Result);
        }
    }
}