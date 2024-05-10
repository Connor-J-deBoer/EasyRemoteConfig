//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using UnityEditor;
using UnityEngine;

namespace Connor.RemoteConfigHelper.Editor
{
    [CustomPropertyDrawer(typeof(Runtime.RemoteFieldAttribute))]
    public class RemoteFieldAttributeDrawer : PropertyDrawer
    {
        private static ServiceAccountConfig _serviceConfig => (ServiceAccountConfig)Resources.Load("ServiceAccount");
        private static EnvironmentConfig _environmentConfig => (EnvironmentConfig)Resources.Load("Environment");
        public async override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
            style.normal.textColor = Color.green;
            EditorGUI.LabelField(position, new GUIContent("Remote Field:"), style);
            position.y += EditorGUIUtility.singleLineHeight + 2.5f;

            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, new GUIContent($"{label.text}"), true);
            if (EditorGUI.EndChangeCheck())
            {
                if (!_serviceConfig || !_environmentConfig || label.text.Contains("Element")) return;
                await HandleUGSApi.PushToRemote(_serviceConfig, _environmentConfig, property);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }
    }
}