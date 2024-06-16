using System.Collections.Generic;

namespace UserSystemFramework.Scripts.System.Utilities
{
    public class DictionaryWithOnChangeEvent<T,TU>
    {
        public readonly Dictionary<T, TU> Content;
        public delegate void OnChangeDelegate(T key);
        public event OnChangeDelegate OnChange;
        public DictionaryWithOnChangeEvent()
        {
            Content = new Dictionary<T, TU>();
        }
        public void Add(T key, TU value, bool triggerOnChange = false)
        {
            Content.Add(key, value);
            if(triggerOnChange)
                OnChange?.Invoke(key);
        }
        public void Remove(T key, bool triggerOnChange = false)
        {
            Content.Remove(key);
            if(triggerOnChange)
                OnChange?.Invoke(key);
        }
        public bool ContainsKey(T key)
        {
            return Content.ContainsKey(key);
        }
        public bool ContainsValue(TU value)
        {
            return Content.ContainsValue(value);
        }
        public TU this[T key, bool triggerOnChange = false]
        {
            get => Content[key];
            set
            {
                Content[key] = value;
                if(triggerOnChange)
                    OnChange?.Invoke(key);
            }
        }
    }
}