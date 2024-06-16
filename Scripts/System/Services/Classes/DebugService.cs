using UnityEngine;
using UserSystemFramework.Scripts.System.Config;
using UserSystemFramework.Scripts.System.Data.Enums;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;
using UserSystemFramework.Scripts.System.Utilities;

namespace UserSystemFramework.Scripts.System.Services.Classes
{
    
    public enum DebuggingLevel
    {
        VeryImportantOnly,
        ErrorsOnly,
        WarningsAndErrors,
        AllDebug,
        Everything,
        None
    }
    public class DebugService : Service
    {
        public override ServicePriority Priority => ServicePriority.Medium;
        private static readonly string LOGPrefix = ("|--[User System Framework]--| ").Italic().Size(14).Color("#3a5ed6");
        private static readonly string LOGPrefixImportant = ("!!--[User System Framework]--!! ").Italic().Size(14).Color("#109e0b");
        private static readonly string LOGPrefixError = ("|--[User System Framework]--| ").Italic().Size(14).Color("#a60c0c");
        private static readonly string LOGPrefixTest = ("|--[USF TEST]--| ").Italic().Size(14).Color("#3a5ed6");
        public static void Log(object message, DebuggingLevel level)
        {
            if (ConfigService.ProjectConfig.manageLocalEnvironment && 
                (ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.Development 
                 || ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.Staging
                 || ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.QA)
               )
            {
                if(level <= ConfigService.ProjectConfig.debugLevel)
                {
                    Debug.Log(LOGPrefix + message.ToString().StylizeError());
                }
            }
            else if (!ConfigService.ProjectConfig.manageLocalEnvironment)
            {
                if(level <= ConfigService.ProjectConfig.debugLevel)
                {
                    Debug.Log(LOGPrefix + message.ToString().StylizeError());
                }
            }
        }
        
        public static void LogImportant(object message, DebuggingLevel level)
        {
            if (ConfigService.ProjectConfig.manageLocalEnvironment && 
                (ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.Development 
                 || ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.Staging
                 || ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.QA)
               )
            {
                if(level <= ConfigService.ProjectConfig.debugLevel)
                {
                    Debug.Log(LOGPrefixImportant + message.ToString().Bold().Color("#109e0b").StylizeError());
                }
            }
            else if (!ConfigService.ProjectConfig.manageLocalEnvironment)
            {
                if(level <= ConfigService.ProjectConfig.debugLevel)
                {
                    Debug.Log(LOGPrefixImportant + message.ToString().Bold().Color("#109e0b").StylizeError());
                }
            }
        }
        public static void LogWarning(object message, DebuggingLevel level)
        {
            if (ConfigService.ProjectConfig.manageLocalEnvironment && 
                (ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.Development 
                 || ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.Staging
                 || ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.QA)
                )
            {
                if(level <= ConfigService.ProjectConfig.debugLevel)
                {
                    Debug.LogWarning(LOGPrefix + message.ToString().StylizeError());
                }
            }
            else if (!ConfigService.ProjectConfig.manageLocalEnvironment)
            {
                if(level <= ConfigService.ProjectConfig.debugLevel)
                {
                    Debug.LogWarning(LOGPrefix + message.ToString().StylizeError());
                }
            }
        }
        public static void LogError(object message, DebuggingLevel level)
        {
            if (ConfigService.ProjectConfig.manageLocalEnvironment && 
                (ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.Development 
                 || ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.Staging
                 || ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.QA)
               )
            {
                if(level <= ConfigService.ProjectConfig.debugLevel)
                {
                    Debug.LogError(LOGPrefixError + message.ToString().StylizeError().ToUpper());
                }
            }
            else if (!ConfigService.ProjectConfig.manageLocalEnvironment)
            {
                if(level <= ConfigService.ProjectConfig.debugLevel)
                {
                    Debug.LogError(LOGPrefixError + message.ToString().StylizeError().ToUpper());
                }
            }
        }
        public static void LogTest(object message, DebuggingLevel level)
        {
            if (ConfigService.ProjectConfig.manageLocalEnvironment && 
                (ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.Development 
                 || ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.Staging
                 || ConfigService.ServerConfig.databaseEnvironment == DatabaseEnvironment.QA)
               )
            {
                if(level <= ConfigService.ProjectConfig.debugLevel)
                {
                    Debug.Log(LOGPrefixTest + message.ToString().StylizeError());
                }
            }
            else if (!ConfigService.ProjectConfig.manageLocalEnvironment)
            {
                if(level <= ConfigService.ProjectConfig.debugLevel)
                {
                    Debug.Log(LOGPrefixTest + message.ToString().StylizeError());
                }
            }
        }
    }
}