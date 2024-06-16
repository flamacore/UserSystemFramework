using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UserSystemFramework.Scripts.API.Interfaces;
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;
using UserSystemFramework.Scripts.System.Data.Enums;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.API.ObjectUpdaters
{
    
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProUpdater : BaseComponentUpdater
    {
        private TextMeshProUGUI _component;
        public TextData updateFrom;
        private void OnDestroy()
        {
            EventPublisher.OnLoginComplete -= UpdateComponent;
            EventPublisher.OnHeartbeat -= UpdateComponent;
        }

        private void OnEnable()
        {
            
            EventPublisher.OnLoginComplete += UpdateComponent;
            EventPublisher.OnHeartbeat += UpdateComponent;
        }

        public override void InitializeComponentUpdater()
        {
            base.InitializeComponentUpdater();
            _component = GetComponent<TextMeshProUGUI>();
        }

        private void UpdateComponent(IRequest completedrequest)
        {
            if(completedrequest.ResultType == RequestResultType.Success)
                UpdateComponent();
        }

        public void UpdateComponent()
        {
            if(!_component) _component = GetComponent<TextMeshProUGUI>();
            _component.text = updateFrom switch
            {
                TextData.UserName => LocalAccountController.CurrentLocalUser.UserName,
                TextData.UserEmail => LocalAccountController.CurrentLocalUser.Email,
                TextData.CoinBalance => LocalAccountController.CurrentLocalUser.CoinBalance.ToString(),
                TextData.UserLevel => "Lv. " + LocalAccountController.CurrentLocalUser.GetCustomUserDataInt("level")
                    .ToString(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}