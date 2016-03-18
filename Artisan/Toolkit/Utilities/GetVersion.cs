using System.Reflection;

namespace Artisan.Toolkit.Utilities
{
    /// <summary>
    /// Get some types of version.
    /// </summary>
    public static class GetVersion
    {
        /// <summary>
        /// Get the AssemblyVersion of the main app.
        /// </summary>
        /// <returns></returns>
        public static string AssemblyVersion()
        {
            return typeof(App).GetTypeInfo().Assembly.GetName().Version.ToString();
        }
    }
}