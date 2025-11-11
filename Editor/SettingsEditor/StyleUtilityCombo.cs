using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kostom.Style
{
    [CustomEditor(typeof(StyleUtilityCombo)), CanEditMultipleObjects]
    internal class UtilityComboSOEditor : Editor
    {
        private SerializedProperty utilities;

        private void OnEnable()
        {
            utilities = serializedObject.FindProperty("utilityCombos");
        }

        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            container.Add(utilities.MakePropertyField());
            return container;
        }
    }

}