using System.Collections.Generic;
using Kostom.Style;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kostom.Style
{
    [CustomEditor(typeof(WhirlCompilerSettings)), CanEditMultipleObjects]
    internal class WhirlCompilerSettingsEditor : Editor
    {
        private SerializedProperty pathPickers, styleSheet;
        private void OnEnable()
        {
            pathPickers = serializedObject.FindProperty("paths");
            styleSheet = serializedObject.FindProperty("styleSheet");
        }

        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();

            if (pathPickers != null)
            {
                container.Add(pathPickers.MakePropertyField());
            }

            container.Add(new PropertyField(styleSheet));
            return container;
        }
    }

    [CustomPropertyDrawer(typeof(PathPicker))]
    internal class PathPickerEditor : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement
            {
                style =
            {
                flexDirection = FlexDirection.Row,
                flexGrow = 1,
                justifyContent = Justify.Center,
                alignItems = Align.Center
            }
            };

            if (string.IsNullOrEmpty(property.FindPropertyRelative("path").stringValue))
            {
                property.FindPropertyRelative("path").stringValue = "Assets";
            }

            container.Add(new PropertyField(property.FindPropertyRelative("path"))
            {
                style =
            {
                flexGrow = 1
            }
            });
            var btn = new Button { text = "Browse" };

            btn.clicked += () =>
            {
                string selectedPath = EditorUtility.OpenFolderPanel("Select Root Folder", "Assets", "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    if (selectedPath.Split("Assets").Length == 2)
                        property.FindPropertyRelative("path").stringValue = selectedPath[selectedPath.LastIndexOf("Assets")..];
                    else if (selectedPath.Split("Assets").Length > 2)
                        property.FindPropertyRelative("path").stringValue = selectedPath[selectedPath.IndexOf("Assets")..];

                    property.serializedObject.ApplyModifiedProperties();
                }
            };

            container.Add(btn);
            return container;
        }
    }
}