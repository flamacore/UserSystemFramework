using UnityEngine;
using UserSystemFramework.Scripts.System.Services.Classes;

namespace UserSystemFramework.Scripts.System.Config
{
    [CreateAssetMenu(fileName = "UserSystemFramework_ProjectConfiguration", menuName = "USF/UserSystemFramework_ProjectConfiguration", order = 1)]
    public class ProjectConfiguration : ScriptableObject
    {
        [Header("Level of debug messages")]
        [Tooltip("Select the level of debug messages. Warning, selecting Everything will really flood the console.")]
        public DebuggingLevel debugLevel;

        [Header("Auto include/omit environment dependencies")]
        [Tooltip("If you set this to yes, things like debug logs, test information etc. will be left out when you're building for production or live for example. Check the documentation for more info.")]
        public bool manageLocalEnvironment;
        
        [Header("Enable or disable the loading overlay canvas for backend requests")]
        [Tooltip("This will show a black, semi-transparent overlay over the screen with a loading spinner in front if set to enable.")]
        public bool loadingOverlayCanvas;
        
        [Header("Add a delay to show the loading canvas in milliseconds")]
        [Tooltip("The loading canvas will show only if the request takes more than this value.")]
        public float loadingOverlayCanvasDelay;

        [Header("Version to check for different databases")]
        [Tooltip("Leave empty if you aim to allow different versions playing together.")]
        public string databaseVersion;
    }
}