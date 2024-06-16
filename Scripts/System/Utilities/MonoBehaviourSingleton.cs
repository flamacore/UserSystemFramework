// ReSharper disable StaticMemberInGenericType
// ReSharper disable InvalidXmlDocComment
using UnityEngine;
using UserSystemFramework.Scripts.System.Controllers.Classes;

namespace UserSystemFramework.Scripts.System.Utilities
{
    /// <summary>
    /// Simple singleton pattern. Ensures only one instance of a MonoBehaviour exists on runtime.
    /// See <see cref="BaseController&lt;&gt;">Base Controller</see>
    /// for most common usage or <see cref="ServiceDependenciesController"> Service Scene Dependencies Controller </see> as a single example.
    /// </summary>
    /// <typeparam name="T">The MonoBehaviour to be used as a singleton.</typeparam>
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component {
    
        private static T _instance;
        private static readonly object InstanceLock = new object();
        private static bool _quitting = false;

        public static T Instance {
            get {
                lock(InstanceLock){
                    if(_instance==null && !_quitting){

                        _instance = FindObjectOfType<T>();
                        if(_instance==null){
                            return null;
                        }
                    }

                    return _instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if(_instance==null) _instance = gameObject.GetComponent<T>();
            else if(_instance.GetInstanceID()!=GetInstanceID()){
                Destroy(gameObject);
                throw new global::System.Exception(
                    $"Instance of {GetType().FullName} already exists, removing {ToString()}");
            }
        }

        protected virtual void OnApplicationQuit() 
        {
            _quitting = true;
        }
    }
}