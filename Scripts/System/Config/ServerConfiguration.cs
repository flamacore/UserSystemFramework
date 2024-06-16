using UnityEngine;
using UserSystemFramework.Scripts.System.Data.Enums;

namespace UserSystemFramework.Scripts.System.Config
{
    
    [CreateAssetMenu(fileName = "UserSystemFramework_ServerConfiguration", menuName = "USF/UserSystemFramework_ServerConfiguration", order = 0)]
    public class ServerConfiguration : ScriptableObject
    {
        [Header("This is the Server URL to connect to")]
        [Tooltip("This is where you've installed the server files. Check the documentation for more info.")]
        public string serverUrl;
        
        [Header("This is the name to use for db")]
        [Tooltip("If it does not exist, it will be auto-created.")]
        public string databaseName = "GameDatabase";

        [Header("This is the environment to use")]
        [Tooltip("Usually you'd like to keep this in Development until you start testing/releasing your game")]
        public DatabaseEnvironment databaseEnvironment;

        [Header("Version to check for different databases")]
        [Tooltip("Leave empty if you aim to allow different versions playing together.")]
        public string databaseVersion;

        [Header("Automatically log the user in at start or register an account if none exists. HIGHLY RECOMMENDED.")]
        [Tooltip("This provides a seamless user experience without almost any downside. Only thing is this can create many guest accounts if the user just plays the game one time and leaves. If you want to gate that mechanism behind a login/register UI first, uncheck this.")]
        public bool autoLoginOrRegister = true;

        [Header("Prefix to add guest account numbers")]
        [Tooltip("You can leave this as Guest safely.")]
        public string guestAccountPrefix = "Guest";

        [Header("Interval for sending the heartbeat to database. (in milliseconds)")]
        [Tooltip("10-20 seconds is recommended. 30-40 would be more optimized but can be a prone window to malicious memory modification. 5 seconds would be most secure but could be overkill for performance.")]
        public int heartbeatInterval;
    }
}