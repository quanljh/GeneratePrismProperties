using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quan;

namespace GeneratePrismProperties
{
    public static class IoC
    {
        /// <summary>
        /// A shortcut to access the <see cref="ILogger"/>
        /// </summary>
        public static ILogger Logger => Framework.Service<ILogger>();

        /// <summary>s
        /// A shortcut to access the <see cref="IConfiguration"/>
        /// </summary>
        public static IConfiguration Configuration => Framework.Service<IConfiguration>();
    }
}
