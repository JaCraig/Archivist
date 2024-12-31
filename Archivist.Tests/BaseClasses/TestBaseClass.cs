using Mecha.Core;
using Microsoft.Extensions.DependencyInjection;
using ObjectCartographer.ExtensionMethods;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.BaseClasses
{
    /// <summary>
    /// Test base class
    /// </summary>
    /// <typeparam name="TTestObject">The type of the test object.</typeparam>
    public abstract class TestBaseClass<TTestObject> : TestBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBaseClass{TTestObject}"/> class.
        /// </summary>
        protected TestBaseClass()
        {
            ObjectType = null;
            _ = GetServiceProvider();
        }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; }

        /// <summary>
        /// Gets or sets the test object.
        /// </summary>
        /// <value>The test object.</value>
        protected TTestObject? TestObject { get; set; }

        /// <summary>
        /// Attempts to break the object.
        /// </summary>
        /// <returns>The async task.</returns>
        [Fact]
        public Task BreakObject() => Mech.BreakAsync(TestObject, new Mecha.Core.Options { MaxDuration = 100 });
    }

    /// <summary>
    /// Test base class
    /// </summary>
    public abstract class TestBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBaseClass{TTestObject}"/> class.
        /// </summary>
        protected TestBaseClass()
        {
            _ = GetServiceProvider();
            _ = Mech.Default;
        }

        /// <summary>
        /// The service provider lock
        /// </summary>
        private static readonly object _ServiceProviderLock = new();

        /// <summary>
        /// The service provider
        /// </summary>
        private static IServiceProvider? _ServiceProvider;

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected abstract Type? ObjectType { get; }

        /// <summary>
        /// Attempts to break the object.
        /// </summary>
        /// <returns>The async task.</returns>
        [Fact]
        public Task BreakType() => ObjectType is null ? Task.CompletedTask : Mech.BreakAsync(ObjectType, new Mecha.Core.Options { MaxDuration = 100 });

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <returns></returns>
        protected static IServiceProvider? GetServiceProvider()
        {
            if (_ServiceProvider is not null)
                return _ServiceProvider;
            lock (_ServiceProviderLock)
            {
                if (_ServiceProvider is not null)
                    return _ServiceProvider;
                _ServiceProvider = new ServiceCollection().AddLogging().RegisterObjectCartographer().AddCanisterModules()?.BuildServiceProvider();
            }
            return _ServiceProvider;
        }
    }
}