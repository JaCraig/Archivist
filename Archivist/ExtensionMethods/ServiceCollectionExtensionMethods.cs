using Archivist.Converters;
using Archivist.Interfaces;
using ObjectCartographer.ExtensionMethods;

namespace Microsoft.Extensions.DependencyInjection
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
            if (services.Exists<Convertinator>())
                return services;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            return services?.AddSingleton<Convertinator>()
                ?.AddAllSingleton<IDataConverter>()
                ?.AddAllSingleton<IFormat>()
                ?.RegisterObjectCartographer();
        }
    }
}