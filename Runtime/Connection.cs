//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;

namespace Connor.RemoteConfigHelper
{
    public class Connection
    {
        private static EnvironmentConfig _config => (EnvironmentConfig)Resources.Load("Environment");
        private static Connection _instance = null;

        public string RemoteID => _config.CurrentEnvironment;
        private IAuthenticationService Auth => AuthenticationService.Instance;

        private Connection() { }
        public static Connection Service
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Connection();
                }
                return _instance;
            }
        }

        public async Task Authenticate()
        {
            // Let the developer know they are connected
            await UnityServices.InitializeAsync();
            if (!Auth.IsSignedIn)
            {
                await Auth.SignInAnonymouslyAsync();
            }
        }
    }
}
