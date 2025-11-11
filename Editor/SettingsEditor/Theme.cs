using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kostom.Style
{
    [CustomEditor(typeof(Theme)), CanEditMultipleObjects]
    internal class ThemeEditor : Editor
    {
        private SerializedProperty styleProperties, isActive, isDefault;

        private void OnEnable()
        {
            isActive = serializedObject.FindProperty("isActive");
            isDefault = serializedObject.FindProperty("isDefault");
            styleProperties = serializedObject.FindProperty("styleProperties");
        }

        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            bool _isDefault = isDefault.boolValue;
            var _styleProperties = styleProperties.MakePropertyFieldWithUpdateAction(false, !_isDefault);

            container.Add(new PropertyField(isActive)
            {
                tooltip = "If it can be used as part of the stylesheet compilation",
                style =
                {
                    marginTop = 0,
                    marginBottom = 0
                }
            });

            if (!_isDefault)
            {
                var propContainer = new VisualElement() { style = { flexGrow = 1, marginTop = 2, marginBottom = 5 } };
                var selectorLabelContainer = new VisualElement
                {
                    style =
            {
                flexDirection = FlexDirection.Row,
                flexGrow = 1,
            }
                };
                var selectorContainer = new VisualElement()
                {
                    style =
            {
                flexDirection = FlexDirection.Row,
                flexGrow = 1,
            }
                };


                //label
                var propLabel = new Label { text = "property", style = { width = 100 } };
                var valueLabel = new Label { text = "value", style = { flexGrow = 1f } };
                selectorLabelContainer.Add(propLabel);
                selectorLabelContainer.Add(new Label { text = " ", style = { unityTextAlign = TextAnchor.MiddleCenter, unityFontStyleAndWeight = FontStyle.Bold } });
                selectorLabelContainer.Add(valueLabel);

                //main
                var styleAdderBtn = new Button { text = "Add" };
                var styleAdderTFProp = new TextField()
                {
                    style = { width = 100 },
                };
                var styleAdderTFVal = new TextField()
                {
                    style = { flexGrow = 1f },
                };
                styleAdderBtn.RemoveRadius();
                styleAdderBtn.RemoveMargin();
                styleAdderTFProp.RemoveMargin();
                styleAdderTFVal.RemoveMargin();

                selectorContainer.Add(styleAdderTFProp);
                selectorContainer.Add(new Label { text = ":", style = { unityTextAlign = TextAnchor.MiddleCenter, unityFontStyleAndWeight = FontStyle.Bold } });
                selectorContainer.Add(styleAdderTFVal);
                selectorContainer.Add(styleAdderBtn);
                styleAdderBtn.clicked += () =>
                {
                    if (string.IsNullOrEmpty(styleAdderTFProp.value) || string.IsNullOrEmpty(styleAdderTFVal.value)) return;
                    styleProperties.InsertArrayElementAtIndex(styleProperties.arraySize);
                    var element = styleProperties.GetArrayElementAtIndex(styleProperties.arraySize - 1);
                    element.FindPropertyRelative("property").stringValue = styleAdderTFProp.value;
                    element.FindPropertyRelative("value").stringValue = styleAdderTFVal.value;

                    styleProperties.serializedObject.ApplyModifiedProperties();
                    styleAdderTFProp.value = "";
                    styleAdderTFVal.value = "";
                    _styleProperties.action?.Invoke();
                };


                propContainer.Add(selectorLabelContainer);
                propContainer.Add(selectorContainer);

                container.Add(propContainer);
            }

            container.Add(_styleProperties.element);
            return container;
        }
    }

}