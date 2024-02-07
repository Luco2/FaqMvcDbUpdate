using System.Globalization;
using System.Reflection;
using System.Resources;

namespace GptWeb.Resources
{
    public class LocalizedResourceManager
    {
        private ResourceManager rm;

        public LocalizedResourceManager()
        {
            rm = new ResourceManager("GptWeb.Resources.Strings", Assembly.GetExecutingAssembly());
        }

        public string GetLocalizedString(string resourceKey)
        {
            return rm.GetString(resourceKey, CultureInfo.CurrentCulture);
        }
    }
}
