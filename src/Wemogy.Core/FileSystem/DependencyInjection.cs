using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Wemogy.Core.FileSystem
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the default file system implementation to the DI
        /// Can be overwritten with a mock implementation for unit testing
        /// </summary>
        public static void AddDefaultFileSystem(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IFileSystem>(new System.IO.Abstractions.FileSystem());
        }
    }
}
