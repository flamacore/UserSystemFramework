using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UserSystemFramework.Scripts.API.Interfaces;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using NotImplementedException = System.NotImplementedException;

namespace UserSystemFramework.Scripts.API.ObjectUpdaters
{
    public class BaseComponentUpdater : MonoBehaviour, IComponentUpdater
    {
        public virtual void InitializeComponentUpdater() { }
    }
}