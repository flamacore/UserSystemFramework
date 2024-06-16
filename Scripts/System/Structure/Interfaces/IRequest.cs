using System;
using System.Collections.Generic;
using UserSystemFramework.Scripts.System.Config;
using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.System.Structure.Interfaces
{
    /// <summary>
    /// Interface for creating requests to communicate with the server/backend.
    /// </summary>
    public interface IRequest
    {
        public string Token { get; }
        public DateTime ConnectionStartTime { get; set; }
        public DateTime ConnectionFinishTime { get; set; }
        public float ConnectionTotalTime { get; }
        public string ConnectionEndpoint { get; set; }
        public RequestType RequestEnum { get; set; }
        public bool IsSet { get; set; }
        
        public ServerConfiguration Configuration { get; set; }
        public Dictionary<string, string> ConnectionParameters { get; set; }
        public string ConnectionResultText { get; set; }
        public Dictionary<string, string> ConnectionResponseHeaders { get; set; }
        public string DebugRequest();
        public void CopyTokenFromRequest(IRequest requestToCopy);
        public void GetTokenFromLocalUser();
        public void SetToken(string token);
        public RequestResultType ResultType { get; }
    }
}