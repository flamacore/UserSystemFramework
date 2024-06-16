using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Services.Classes
{
    public class TimeService : Service
    {
        public override ServicePriority Priority => ServicePriority.Low;

        public delegate void DailyReset();

        public static event DailyReset OnDailyReset;

        public delegate void WeeklyReset();

        public static event WeeklyReset OnWeeklyReset;

        public delegate void MonthlyReset();

        public static event MonthlyReset OnMonthlyReset;

        private static DateTime _lastDate;
        private static DateTime _currentDate;
        private static bool _getServerInfoCompleted = false;
        private static bool IsGetServerInfoCompleted() => _getServerInfoCompleted;

        public static DateTime ServerTime => GetServerTime().Result;
        private static DateTime _serverTime;
        private static IRequest _getTimeRequest;

        public static async Task<DateTime> GetServerTime()
        {
            _getServerInfoCompleted = false;
            _getTimeRequest = ServerRequestGetterService.Get(RequestType.GetTimeRequest);
            await ServiceHandler.Locator.Get<ServerRequestSenderService>().SendRequest(_getTimeRequest, request =>
            {
                switch (request.ResultType)
                {
                    case RequestResultType.Success:
                        _serverTime = DateTime.Parse(request.ConnectionResponseHeaders["Time"]);
                        break;
                    case RequestResultType.Neutral:
                        break;
                    case RequestResultType.Fail:
                        break;
                    case RequestResultType.Undefined:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                _getServerInfoCompleted = true;
            });
            DebugService.Log("Server time is " + _serverTime.ToString("dd/MM/yyyy HH:mm:ss"), DebuggingLevel.WarningsAndErrors);
            return _serverTime;
        }

        public static void TriggerDailyReset()
            {
                OnDailyReset?.Invoke();
            }

            public static void TriggerWeeklyReset()
            {
                OnWeeklyReset?.Invoke();
            }

            public static void TriggerMonthlyReset()
            {
                OnMonthlyReset?.Invoke();
            }

        public override void Init()
        {
            base.Init();
            EventPublisher.OnLoginComplete += OnLoginComplete;
        }

        private async void OnLoginComplete(IRequest completedRequest)
        {
            _lastDate = LocalAccountController.CurrentLocalUser.LastUpdated;
            //_serverTime = await GetServerTime();
            //await Task.Run(TimeTick);
        }

        private static async void TimeTick()
        {
            while (true)
            {
                _currentDate = ServerTime;
                await Task.Delay(1000);
                if (_currentDate.Day != _lastDate.Day)
                {
                    DebugService.Log("Daily reset triggered", DebuggingLevel.Everything);
                    TriggerDailyReset();
                }
                if (_currentDate.DayOfWeek < _lastDate.DayOfWeek && _currentDate.Month == _lastDate.Month)
                {
                    DebugService.Log("Weekly reset triggered", DebuggingLevel.Everything);
                    TriggerWeeklyReset();
                }
                if (_currentDate.Month != _lastDate.Month)
                {
                    DebugService.Log("Monthly reset triggered", DebuggingLevel.Everything);
                    TriggerMonthlyReset();
                }
                _lastDate = _currentDate;
            }
        }
    }
}