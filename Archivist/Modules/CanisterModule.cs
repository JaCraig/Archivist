using Archivist.ExtensionMethods;
using Canister.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Archivist.Modules
{
    /// <summary>
    /// Represents a module for registering services related to Canister.
    /// </summary>
    public class CanisterModule : IModule
    {
        /// <summary>
        /// Gets the order in which the module should be loaded.
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Loads the services into the service collection.
        /// </summary>
        /// <param name="serviceDescriptors">The service collection to load the services into.</param>
        public void Load(IServiceCollection? serviceDescriptors) => serviceDescriptors?.AddArchivist();
    }
}