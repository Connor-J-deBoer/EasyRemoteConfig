//=================================================================\\
//===Copyright (C) 2023 FORGOTTEN_FILES VFS, All Rights Reserved===\\
//=================================================================\\

using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;

namespace RemoteConfigHelper
{
    public class Connection
    {
        private static Config _config => (Config)Resources.Load("Environment");
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
