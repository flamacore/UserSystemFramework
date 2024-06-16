using UserSystemFramework.Scripts.System.Config;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Services.Classes
{
    public class ConfigService : Service
    {
        public override ServicePriority Priority => ServicePriority.High;
        public static ProjectConfiguration ProjectConfig;
        public static ServerConfiguration ServerConfig;
        public static CustomFieldsConfiguration CustomFieldsConfig;
    }
}