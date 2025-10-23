//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections.Generic;
using UnityEngine;

namespace Connor.EasyRemoteConfig
{
    [CreateAssetMenu(menuName = "Remote Config Setup/Environment Config")]
    public class EnvironmentConfig : ScriptableObject
    {
        [SerializeField] private Environments _currentEnvironment;

        [SerializeField] private string _production;
        [SerializeField] private string _development;
        [SerializeField] private string _demo;

        private Dictionary<Environments, string> _environments => new Dictionary<Environments, string>
        {
            { Environments.Production, _production },
            { Environments.Development, _development },
            { Environments.Demo, _demo },
        };

        public string CurrentEnvironment => _environments[_currentEnvironment];
    }

    public enum Environments
    {
        Production = 0,
        Development = 1,
        Demo = 2
    }
}