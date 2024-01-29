using Restro.Debugging;

namespace Restro
{
    public class RestroConsts
    {
        public const string LocalizationSourceName = "Restro";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "0f3da21d89754d8e9297645e78c45d19";
    }
}
