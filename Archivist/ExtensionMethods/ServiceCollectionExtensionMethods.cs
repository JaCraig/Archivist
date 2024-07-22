using Archivist.Converters;
using Archivist.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Archivist.ExtensionMethods
{
    /// <summary>
    /// Extension methods for IServiceCollection to add Archivist services.
    /// </summary>
    public static class ServiceCollectionExtensionMethods
    {
        /// <summary>
        /// Adds Archivist services to the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <returns>The modified IServiceCollection.</returns>
        public static IServiceCollection? AddArchivist(this IServiceCollection? services)
        {
            return services?.AddSingleton<Convertinator>()
                ?.AddAllSingleton<IDataConverter>()
                ?.AddAllSingleton<IFormat>();
        }
    }
}