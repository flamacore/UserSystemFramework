using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

namespace UserSystemFramework.Scripts.System.Utilities
{
    /// <summary>
    /// Very simple struct for making a UnityWebRequest that plays nice with the Task async/await system.
    /// No changes needed to use it. Can just be used like <code>await UnityWebRequestAsyncOperation.SendWebRequest</code>.
    /// </summary>
    public struct AwaitableUnityWebRequest : INotifyCompletion
    {
        private readonly UnityWebRequestAsyncOperation _asyncOperation;
        private Action _continuation;

        public AwaitableUnityWebRequest(UnityWebRequestAsyncOperation asyncOperation)
        {
            this._asyncOperation = asyncOperation;
            _continuation = null;
        }
        public bool IsCompleted => _asyncOperation.isDone;
        public void GetResult() { }
        public void OnCompleted(Action continuation)
        {
            this._continuation = continuation;
            _asyncOperation.completed += OnRequestCompleted;
        }

        private void OnRequestCompleted(AsyncOperation obj)
        {
            _continuation?.Invoke();
        }
    }
    /// <summary>
    /// Contains the extension method to make the async operation of UnityWebRequest awaitable.
    /// </summary>
    public static class ExtensionMethods
    {
        public static AwaitableUnityWebRequest GetAwaiter(this UnityWebRequestAsyncOperation asyncOperation)
        {
            return new AwaitableUnityWebRequest(asyncOperation);
        }
    }
}