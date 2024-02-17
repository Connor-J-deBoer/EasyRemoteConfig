//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using UnityEngine;

namespace Connor.RemoteConfigHelper.Runtime
{
    [CreateAssetMenu(menuName = "Remote Config Setup/Service Account Config")]
    public class ServiceAccountConfig : ScriptableObject 
    {
        [Header("Project Related Settings")]
        [SerializeField] private string _projectID = "";
        [Space]
        [Header("Service Account Related Settings")]
        [SerializeField] private string _secretKey = "";
        [SerializeField] private string _key = "";

        internal string ProjectID => _projectID;
        internal string Key => $"{_key}:{_secretKey}";
    }
}