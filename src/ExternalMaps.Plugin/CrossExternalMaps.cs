using Plugin.ExternalMaps.Abstractions;
using System;

namespace Plugin.ExternalMaps
{
    /// <summary>
    /// Cross platform ExternalMaps implemenations
    /// </summary>
    public class CrossExternalMaps
    {
        static Lazy<IExternalMaps> implementation = new Lazy<IExternalMaps>(() => CreateExternalMaps(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
		/// Gets if the plugin is supported on the current platform.
		/// </summary>
		public static bool IsSupported => implementation.Value == null ? false : true;

        /// <summary>
        /// Current plugin implementation to use
        /// </summary>
        public static IExternalMaps Current
        {
            get
            {
                var ret = implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        static IExternalMaps CreateExternalMaps()
        {
#if NETSTANDARD1_0
            return null;
#else
            return new ExternalMapsImplementation();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        
    }
}
