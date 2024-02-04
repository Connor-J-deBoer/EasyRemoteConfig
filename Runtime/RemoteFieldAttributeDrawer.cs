//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Connor.RemoteConfigHelper
{
    [CustomPropertyDrawer(typeof(RemoteFieldAttribute))]
    public class RemoteFieldAttributeDrawer : PropertyDrawer
    {
        private static ServiceAccountConfig _serviceConfig => (ServiceAccountConfig)Resources.Load("ServiceAccount");
        private static EnvironmentConfig _environmentConfig => (EnvironmentConfig)Resources.Load("Environment");
        public async override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, label, true);
            if (EditorGUI.EndChangeCheck())
            {
                if (!_serviceConfig || !_environmentConfig || label.text.Contains("Element")) return;
                await HandleUGSApi.PushToRemote(_serviceConfig, _environmentConfig, property);
            }
        }
    }
}
