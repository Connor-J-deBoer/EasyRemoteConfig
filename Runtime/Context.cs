//=================================================================\\
//======Copyright (C) 2025 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using UnityEngine;

namespace MQG.EasyRemoteConfig.Runtime
{
    [CreateAssetMenu(menuName = "Remote Config Setup/Remote Config Context", fileName = "Context")]
    public class Context : ScriptableObject
    {
        public string CurrentEnvironment = "Development";
        [SerializeField, HideInInspector]
        private string _remoteUID = "";

        public string GetRemoteUID()
        {
            if (_remoteUID != "")
                return _remoteUID;
            
            _remoteUID = System.Guid.NewGuid().ToString();
            return _remoteUID;
        }
    }
}