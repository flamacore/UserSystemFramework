using Unity.Collections;
using UnityEditor;

namespace UserSystemFramework.Editor
{
    public static class LeakDetectionHelper
    {
        [MenuItem("Jobs/Leak Detection")]
        private static void LeakDetection()
        {
            NativeLeakDetection.Mode = NativeLeakDetectionMode.Enabled;
        }

        [MenuItem("Jobs/Leak Detection With Stack Trace")]
        private static void LeakDetectionWithStackTrace()
        {
            NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;
        }

        [MenuItem("Jobs/No Leak Detection")]
        private static void NoLeakDetection()
        {
            NativeLeakDetection.Mode = NativeLeakDetectionMode.Disabled;
        }
    }
}