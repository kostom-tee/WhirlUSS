using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace Kostom.Style
{
	[CustomEditor(typeof(WhirlManager))]
	public class WhirlManagerEditor_:Editor
	{
        private SerializedProperty theme;
        private void OnEnable()
        {
            theme = serializedObject.FindProperty("theme");
        }

        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            container.Add(theme.MakePropertyField());
            return container;
        }
    }
}

