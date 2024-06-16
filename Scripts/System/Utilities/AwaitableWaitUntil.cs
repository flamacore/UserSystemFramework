using System;
using System.Threading.Tasks;

namespace UserSystemFramework.Scripts.System.Utilities
{
    public static class AwaitableWaitUntil
    {
        public static async Task WaitUntil(Func<bool> predicate, int sleep = 50)
        {
            while (!predicate())
            {
                await Task.Delay(sleep);
            }
        }
    }
}