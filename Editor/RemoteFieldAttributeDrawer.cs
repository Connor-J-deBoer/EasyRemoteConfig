//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using UnityEditor;
using UnityEngine;

namespace MQG.EasyRemoteConfig.Editor
{
    [CustomPropertyDrawer(typeof(Runtime.RemoteFieldAttribute))]
    public class RemoteFieldAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
            style.normal.textColor = Color.green;
            EditorGUI.LabelField(position, new GUIContent("Remote Field:"), style);
            position.y += EditorGUIUtility.singleLineHeight + 2.5f;

            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, new GUIContent($"{label.text}"), true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }
    }
}