using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kostom.Style
{
    [CustomPropertyDrawer(typeof(Component))]
    internal class ComponentEditor : PropertyDrawer
    {
        static readonly Dictionary<string, bool> ComponentFoldoutStates = new Dictionary<string, bool>();

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var selector = property.FindPropertyRelative("selector");
            var styles = property.FindPropertyRelative("utilities");

            bool expanded = ComponentFoldoutStates.ContainsKey(property.propertyPath) && ComponentFoldoutStates[property.propertyPath];
            var container = new Foldout()
            {
                text = string.IsNullOrEmpty(selector.stringValue) ? property.displayName : selector.stringValue,
                value = expanded,
                style =
                {
                    paddingTop = 5,
                    paddingBottom = 5
                }
            };

            container.RegisterValueChangedCallback(evt =>
            {
                ComponentFoldoutStates[property.propertyPath] = evt.newValue;
            });

            var selectorTF = new TextField() { isDelayed = true, label = selector.displayName, value = selector.stringValue };

            selectorTF.RegisterValueChangedCallback((x) =>
            {
                selector.stringValue = x.newValue;
                selector.serializedObject.ApplyModifiedProperties();
            });
            var selectorTE = selectorTF.Q(className: "unity-text-element");
            selectorTE.style.minWidth = 10;
            selectorTE.style.paddingRight = 0;
            selectorTE.style.marginRight = 5;
            

            var selectorLabelContainer = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    flexGrow = 1,
                    marginTop = 10
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
            var utilityLabel = new Label { text = "class utility", style = { width = 100 } };
            selectorLabelContainer.Add(utilityLabel);

            //main
            var styleAdderBtn = new Button { text = "Add" };
            var styleAdderUtilityTF = new AutoComplete()
            {
                autoCompleteValue = () => Helper.BaseUtilities.ToList(),
                style = { flexGrow = 1 }
            };
            styleAdderBtn.RemoveRadius();
            styleAdderBtn.RemoveMargin();
            styleAdderUtilityTF.RemoveMargin();

            selectorContainer.Add(styleAdderUtilityTF);
            selectorContainer.Add(styleAdderBtn);
            styleAdderBtn.clicked += () =>
            {
                styleAdderUtilityTF.CloseAutoComplete();
                if (string.IsNullOrEmpty(styleAdderUtilityTF.value)) return;
                styles.InsertArrayElementAtIndex(styles.arraySize);
                styles.GetArrayElementAtIndex(styles.arraySize - 1).stringValue = styleAdderUtilityTF.value;
                styles.serializedObject.ApplyModifiedProperties();
            };

            var stylesContainer = new VisualElement
            {
                style =
            {
                flexGrow = 1,
                flexWrap = Wrap.Wrap,
                flexDirection = FlexDirection.Row
            }
            };

            for (int i = 0; i < styles.arraySize; i++)
            {
                stylesContainer.Add(AddMargin(AddPadding(DrawPill(i, styles))));
            }

            container.Add(selectorTF);
            container.Add(selectorLabelContainer);
            container.Add(selectorContainer);
            container.Add(stylesContainer);
            return container;
        }

        private static VisualElement AddPadding(VisualElement element)
        {
            element.style.paddingBottom = 5;
            element.style.paddingTop = 5;
            element.style.paddingRight = 5;
            element.style.paddingLeft = 5;

            return element;
        }

        private static VisualElement AddMargin(VisualElement element)
        {
            element.style.marginBottom = 2;
            element.style.marginTop = 2;
            element.style.marginRight = 2;
            element.style.marginLeft = 2;

            return element;
        }

        private static VisualElement DrawPill(int index, SerializedProperty prop)
        {
            var container = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row
                }
            };
            container.AddBorder();
            container.Add(new Label
            {
                text = $"{prop.GetArrayElementAtIndex(index).stringValue}",
                style =
                {
                    unityTextAlign = TextAnchor.MiddleCenter
                }
            });

            var btn = new Button
            {
                text = "x",
                style =
                {
                    backgroundColor = Color.clear
                }
            };
            btn.RemoveMargin();
            btn.RemoveRadius();
            btn.AddBorder();


            btn.clicked += () =>
            {
                prop.DeleteArrayElementAtIndex(index);
                prop.serializedObject.ApplyModifiedProperties();
            };

            container.Add(btn);
            return container;
        }
    }
}