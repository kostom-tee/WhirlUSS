using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kostom.Style
{
    [CustomEditor(typeof(StyleUtilities)), CanEditMultipleObjects]
    internal class StyleUtilitiesEditor : Editor
    {
        private SerializedProperty utilities;

        private void OnEnable()
        {
            utilities = serializedObject.FindProperty("utilities");
        }

        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            var utilityProp = utilities.MakePropertyField();
            container.Add(utilityProp);
            return container;
        }
    }

    [CustomPropertyDrawer(typeof(Utility))]
    internal class UtilityDrawer : PropertyDrawer
    {
        static readonly Dictionary<string, bool> foldoutStates = new Dictionary<string, bool>();

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var selector = property.FindPropertyRelative("selector");
            var styleProperties = property.FindPropertyRelative("styleProperties");
            var selectorTF = new TextField() { isDelayed = true, label = selector.displayName, value = selector.stringValue };

            bool expanded = foldoutStates.ContainsKey(property.propertyPath) ? foldoutStates[property.propertyPath] : false;
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
                foldoutStates[property.propertyPath] = evt.newValue;
            });

            selectorTF.RegisterValueChangedCallback((x) =>
            {
                selector.stringValue = x.newValue;
                selector.serializedObject.ApplyModifiedProperties();
            });
            var selectorTE = selectorTF.Q(className: "unity-text-element");
            selectorTE.style.minWidth = 10;
            selectorTE.style.paddingRight = 0;
            selectorTE.style.marginRight = 5;
            container.Add(selectorTF);

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
            var propLabel = new Label { text = "property", style = { width = 100 } };
            var valueLabel = new Label { text = "value", style = { flexGrow = 1f } };
            selectorLabelContainer.Add(propLabel);
            selectorLabelContainer.Add(new Label { text = " ", style = { unityTextAlign = TextAnchor.MiddleCenter, unityFontStyleAndWeight = FontStyle.Bold } });
            selectorLabelContainer.Add(valueLabel);

            //main
            var styleAdderBtn = new Button { text = "Add" };
            var styleAdderTFProp = new AutoComplete() {
                style = { width = 100 },
                autoCompleteValue = () => {
                    return Helper.UssProps.Keys.ToList();
                }
            };
            var styleAdderTFVal = new AutoComplete() {
                style = { flexGrow = 1f },
                autoCompleteValue = () =>
                {
                    if (!string.IsNullOrEmpty(styleAdderTFProp.value) && Helper.UssProps.ContainsKey(styleAdderTFProp.value))
                    {
                        return Helper.UssProps[styleAdderTFProp.value];
                    }

                    return new List<string>();
                }
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
                styleAdderTFProp.CloseAutoComplete();
                if (string.IsNullOrEmpty(styleAdderTFProp.value) || string.IsNullOrEmpty(styleAdderTFVal.value)) return;
                styleProperties.InsertArrayElementAtIndex(styleProperties.arraySize);
                styleProperties.serializedObject.ApplyModifiedProperties();
                var element = styleProperties.GetArrayElementAtIndex(styleProperties.arraySize - 1);
                element.FindPropertyRelative("property").stringValue = styleAdderTFProp.value;
                element.FindPropertyRelative("value").stringValue = styleAdderTFVal.value;
                styleProperties.serializedObject.ApplyModifiedProperties();
            };

            container.Add(selectorLabelContainer);
            container.Add(selectorContainer);

            if (!string.IsNullOrEmpty(selector.stringValue) && styleProperties.arraySize > 0)
            {
                container.Add(new Label
                {
                    text = $".{selector.stringValue} {{",
                    style =
                {
                    marginTop = 10
                }
                });

                var styleClassBody = new VisualElement() { style = { marginLeft = 20 } };

                for (int i = 0; i < styleProperties.arraySize; i++)
                {
                    var elementContainer = new VisualElement { style = { flexDirection = FlexDirection.Row, flexGrow = 1 } };
                    elementContainer.Add(new PropertyField(styleProperties.GetArrayElementAtIndex(i)) { style = { flexGrow = 1 } });
                    var elementRemoveBtn = new Button { text = "x" };
                    elementContainer.Add(elementRemoveBtn);
                    elementRemoveBtn.RemoveRadius();
                    elementRemoveBtn.style.backgroundColor = Color.clear;
                    elementRemoveBtn.RemoveMargin();
                    elementRemoveBtn.AddBorder();
                    RegisterDeleteArray(elementRemoveBtn, i, styleProperties);

                    styleClassBody.Add(elementContainer);
                }

                container.Add(styleClassBody);
                container.Add(new Label
                {
                    text = "}"
                });
            }
            return container;

            void RegisterDeleteArray(Button btn, int index, SerializedProperty prop)
            {
                btn.clicked += () =>
                {
                    prop.DeleteArrayElementAtIndex(index);
                    prop.serializedObject.ApplyModifiedProperties();
                };
            }
        }
    }

    [CustomPropertyDrawer(typeof(StyleProperty))]
    internal class StyleDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return Helper.CreateStyleDrawerUI(property);
        }
    }

    [CustomPropertyDrawer(typeof(UtilityCombo))]
    internal class ApplyDrawer : PropertyDrawer
    {
        static readonly Dictionary<string, bool> applyFoldoutStates = new Dictionary<string, bool>();

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var selector = property.FindPropertyRelative("selector");
            var styles = property.FindPropertyRelative("utilities");

            bool expanded = applyFoldoutStates.ContainsKey(property.propertyPath) && applyFoldoutStates[property.propertyPath];
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
                applyFoldoutStates[property.propertyPath] = evt.newValue;
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
            container.Add(selectorTF);


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
            var styleAdderUtilityTF = new AutoComplete() {
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

            container.Add(selectorLabelContainer);
            container.Add(selectorContainer);


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

            container.Add(stylesContainer);
            return container;

            static VisualElement DrawPill(int index, SerializedProperty prop)
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
    }

    internal static class Helper
    {
        static float unityVersion = -1;
        public static float UnityVersion
        {
            get
            {
                if(unityVersion == -1)
                {
                    string val = Application.unityVersion;
                    if (float.TryParse(val[..val.LastIndexOf(".")], out var value)){
                        unityVersion = value;
                    }
                }
                return unityVersion;
            }
        }
        public static readonly IReadOnlyCollection<string> UnityColors = new string[]
        {
            "aliceblue",
            "antiquewhite",
            "aqua",
            "aquamarine",
            "azure",
            "beige",
            "bisque",
            "black",
            "blanchedalmond",
            "blue",
            "blueviolet",
            "brown",
            "burlywood",
            "cadetblue",
            "chartreuse",
            "chocolate",
            "coral",
            "cornflowerblue",
            "cornsilk",
            "crimson",
            "cyan",
            "darkblue",
            "darkcyan",
            "darkgoldenrod",
            "darkgray",
            "darkgreen",
            "darkgrey",
            "darkkhaki",
            "darkmagenta",
            "darkolivegreen",
            "darkorange",
            "darkorchid",
            "darkred",
            "darksalmon",
            "darkseagreen",
            "darkslateblue",
            "darkslategray",
            "darkslategrey",
            "darkturquoise",
            "darkviolet",
            "deeppink",
            "deepskyblue",
            "dimgray",
            "dimgrey",
            "dodgerblue",
            "firebrick",
            "floralwhite",
            "forestgreen",
            "gainsboro",
            "ghostwhite",
            "gold",
            "gray",
            "green",
            "goldenrod",
            "greenyellow",
            "grey",
            "honeydew",
            "hotpink",
            "indianred",
            "indigo",
            "ivory",
            "khaki",
            "lavender",
            "lavenderblush",
            "lawngreen",
            "lemonchiffon",
            "lightblue",
            "lightcoral",
            "lightcyan",
            "lightgoldenrodyellow",
            "lightgray",
            "lightgreen",
            "lightgrey",
            "lightpink",
            "lightsalmon",
            "lightseagreen",
            "lightskyblue",
            "lightslategray",
            "lightslategrey",
            "lightsteelblue",
            "lightyellow",
            "lime",
            "limegreen",
            "linen",
            "magenta",
            "maroon",
            "mediumaquamarine",
            "mediumblue",
            "mediumorchid",
            "mediumpurple",
            "mediumseagreen",
            "mediumslateblue",
            "mediumspringgreen",
            "mediumturquoise",
            "mediumvioletred",
            "midnightblue",
            "mintcream",
            "mistyrose",
            "moccasin",
            "navajowhite",
            "navy",
            "oldlace",
            "olive",
            "olivedrab",
            "orange",
            "orangered",
            "orchid",
            "palegoldenrod",
            "palegreen",
            "paleturquoise",
            "palevioletred",
            "papayawhip",
            "peachpuff",
            "peru",
            "pink",
            "plum",
            "powderblue",
            "purple",
            "rebeccapurple",
            "red",
            "rosybrown",
            "royalblue",
            "saddlebrown",
            "salmon",
            "sandybrown",
            "seagreen",
            "seashell",
            "sienna",
            "silver",
            "skyblue",
            "slateblue",
            "slategray",
            "slategrey",
            "snow",
            "springgreen",
            "steelblue",
            "tan",
            "teal",
            "thistle",
            "tomato",
            "transparent",
            "turquoise",
            "violet",
            "wheat",
            "white",
            "whitesmoke",
            "yellow",
            "yellowgreen",
        };
        public static readonly Dictionary<string, List<string>> UssProps = new Dictionary<string, List<string>>
        {
            {
                "align-content", new List<string>{ "flex-start", "flex-end", "center", "stretch" }
            },
            {
                "align-items", new List<string>{ "auto",  "flex-start", "flex-end", "center", "stretch" }
            },
            {
                "align-self", new List<string>{ "auto", "flex-start", "flex-end", "center", "stretch" }
            },
            {
                "all", new List<string>{ "initial" }
            },
            {
                "background-color", UnityColors.ToList()
            },
            {
                "background-image", new List<string>{}
            },
            {
                "background-position", new List<string>{ "top", "bottom", "left", "right", "center"}
            },
            {
                "background-position-x", new List<string>{ "left", "right", "center" }
            },
            {
                "background-position-y", new List<string>{ "top", "bottom", "center" }
            },
            {
                "background-repeat", new List<string>{ "repeat", "repeat-x", "repeat-y", "space","round","no-repeat","initial" }
            },
            {
                "background-size", new List<string>{"contain", "cover"}
            },
            {
                "border-bottom-color", UnityColors.ToList()
            },
            {
                "border-bottom-left-radius", new List<string>{}
            },
            {
                "border-bottom-right-radius", new List<string>{}
            },
            {
                "border-bottom-width", new List<string>{}
            },
            {
                "border-color", UnityColors.ToList()
            },
            {
                "border-left-color", UnityColors.ToList()
            },
            {
                "border-left-width", new List<string>{}
            },
            {
                "border-radius", new List<string>{}
            },
            {
                "border-right-color", UnityColors.ToList()
            },
            {
                "border-right-width", new List<string>{}
            },
            {
                "border-top-color", UnityColors.ToList()
            },
            {
                "border-top-left-radius", new List<string>{}
            },
            {
                "border-top-right-radius", new List<string>{}
            },
            {
                "border-top-width", new List<string>{}
            },
            {
                "border-width", new List<string>{}
            },
            {
                "bottom", new List<string>{}
            },
            {
                "color", UnityColors.ToList()
            },
            {
                "cursor", new List<string>{}
            },
            {
                "display", new List<string>{"flex", "none"}
            },
            {
                "filter", new List<string>{}
            },
            {
                "flex", new List<string>{"none"}
            },
            {
                "flex-basis", new List<string>{}
            },
            {
                "flex-direction", new List<string>{"row","row-reverse","column","column-reverse"}
            },
            {
                "flex-grow", new List<string>{}
            },
            {
                "flex-shrink", new List<string>{}
            },
            {
                "flex-wrap", new List<string>{"nowrap","wrap","wrap-reverse"}
            },
            {
                "font-size", new List<string>{}
            },
            {
                "height", new List<string>{}
            },
            {
                "justify-content", new List<string>{ "flex-start","flex-end","center","space-between", "space-around" }
            },
            {
                "left", new List<string>{}
            },
            {
                "letter-spacing", new List<string>{}
            },
            {
                "margin", new List<string>{}
            },
            {
                "margin-bottom", new List<string>{}
            },
            {
                "margin-left", new List<string>{}
            },
            {
                "margin-right", new List<string>{}
            },
            {
                "margin-top", new List<string>{}
            },
            {
                "max-height", new List<string>{}
            },
            {
                "max-width", new List<string>{}
            },
            {
                "min-height", new List<string>{}
            },
            {
                "min-width", new List<string>{}
            },
            {
                "opacity", new List<string>{}
            },
            {
                "overflow", new List<string>{"hidden","visible"}
            },
            {
                "padding", new List<string>{}
            },
            {
                "padding-bottom", new List<string>{}
            },
            {
                "padding-left", new List<string>{}
            },
            {
                "padding-right", new List<string>{}
            },
            {
                "padding-top", new List<string>{}
            },
            {
                "position", new List<string>{"absolute","relative"}
            },
            {
                "right", new List<string>{}
            },
            {
                "rotate", new List<string>{}
            },
            {
                "scale", new List<string>{}
            },
            {
                "text-overflow", new List<string>{"clip","ellipsis"}
            },
            {
                "text-shadow", new List<string>{}
            },
            {
                "top", new List<string>{}
            },
            {
                "transform-origin", new List<string>{}
            },
            {
                "transition", new List<string>{}
            },
            {
                "transition-delay", new List<string>{}
            },
            {
                "transition-duration", new List<string>{}
            },
            {
                "transition-property", new List<string>{}
            },
            {
                "transition-timing-function", new List<string>{}
            },
            {
                "translate", new List<string>{}
            },
            {
                "-unity-background-image-tint-color", UnityColors.ToList()
            },
            {
                "-unity-background-scale-mode", new List<string>{ "stretch-to-fill","scale-and-crop","scale-to-fit" }
            },
            {
                "-unity-editor-text-rendering-mode", new List<string>{}
            },
            {
                "-unity-font", new List<string>{}
            },
            {
                "-unity-font-definition", new List<string>{}
            },
            {
                "-unity-font-style", new List<string>{ "normal","italic ","bold","bold-and-italic" }
            },
            {
                "-unity-overflow-clip-box", new List<string>{"padding-box","content-box"}
            },
            {
                "-unity-paragraph-spacing", new List<string>{}
            },
            {
                "-unity-slice-bottom", new List<string>{}
            },
            {
                "-unity-slice-left", new List<string>{}
            },
            {
                "-unity-slice-right", new List<string>{}
            },
            {
                "-unity-slice-scale", new List<string>{}
            },
            {
                "-unity-slice-top", new List<string>{}
            },
            {
                "-unity-slice-type", new List<string>{"sliced","tilted"}
            },
            {
                "-unity-slice-align", new List<string>{}
            },
            {
                "-unity-text-auto-size", new List<string>{}
            },
            {
                "-unity-text-generator", new List<string>{"standard", "advanced"}
            },
            {
                "-unity-text-outline", new List<string>{ "upper-left","middle-left","lower-left","upper-center","middle-center","lower-center","upper-right","middle-right","lower-right" }
            },
            {
                "-unity-text-outline-color", UnityColors.ToList()
            },
            {
                "-unity-text-outline-width", new List<string>{}
            },
            {
                "-unity-text-overflow-position", new List<string>{ "start","middle","end" }
            },
            {
                "visibility", new List<string>{"visible","hidden"}
            },
            {
                "white-space", new List<string>{ "normal","nowrap","pre","pre-wrap" }
            },
            {
                "width", new List<string>{}
            },
            {
                "word-spacing", new List<string>{}
            }
        };
        public static readonly IReadOnlyCollection<string> BaseUtilities = new string[]
        {
            "aspect-square",
            "aspect-video",
            "aspect-auto",
            "aspect-",

            "columns-3xs",
            "columns-2xs",
            "columns-xs",
            "columns-sm",
            "columns-md",
            "columns-lg",
            "columns-xl",
            "columns-2xl",
            "columns-3xl",
            "columns-4xl",
            "columns-5xl",
            "columns-6xl",
            "columns-7xl",
            "columns-auto",
            "columns-",

            "flex",
            "hidden",

            "object-contain",
            "object-cover",
            "object-fill",
            "object-none",
            "object-scale-down",

            "object-top-left",
            "object-top",
            "object-top-right",
            "object-left",
            "object-center",
            "object-right",
            "object-bottom-left",
            "object-bottom",
            "object-bottom-right",
            "object-",

            "overflow-auto",
            "overflow-hidden",
            "overflow-visible",
            "overflow-scroll",

            "absolute",
            "relative",

            "top-px",
            "-top-px",
            "top-full",
            "-top-full",
            "top-auto",
            "-top-",
            "top-",


            "right-px",
            "-right-px",
            "right-full",
            "-right-full",
            "right-auto",
            "-right-",
            "right-",


            "bottom-px",
            "-bottom-px",
            "bottom-full",
            "-bottom-full",
            "bottom-auto",
            "-bottom-",
            "bottom-",


            "left-px",
            "-left-px",
            "left-full",
            "-left-full",
            "left-auto",
            "-left-",
            "left-",

            "visible",
            "invisible",
            "collapse",

            "basis-full",

            "basis-auto",
            "basis-3xs",
            "basis-2xs",
            "basis-xs",
            "basis-sm",
            "basis-md",
            "basis-lg",
            "basis-xl",
            "basis-2xl",
            "basis-3xl",
            "basis-4xl",
            "basis-5xl",
            "basis-6xl",
            "basis-7xl",
            "basis-",

            "flex-row",
            "flex-row-reverse",
            "flex-col",
            "flex-col-reverse",

            "flex-nowrap",
            "flex-wrap",
            "flex-wrap-reverse",

            "flex-auto",
            "flex-initial",
            "flex-none",
            "flex-",

            "grow",
            "grow-",

            "shrink",
            "shrink-",

            "grow",
            "grow-",

            "justify-start",
            "justify-end",
            "justify-center",
            "justify-between",
            "justify-around",

            "content-center",
            "content-start",
            "content-end",
            "content-stretch",

            "items-start",
            "items-end",
            "items-center",
            "items-auto",
            "items-stretch",

            "self-auto",
            "self-start",
            "self-end",
            "self-center",
            "self-stretch",

            "p-px",
            "p-",
            "px-px",
            "px-",
            "py-px",
            "py-",
            "pt-px",
            "pt-",
            "pr-px",
            "pr-",
            "pb-px",
            "pb-",
            "pl-px",
            "pl-",


            "m-px",
            "-m-px",
            "m-",
            "-m-",
            "mx-px",
            "-mx-px",
            "mx-",
            "-mx-",
            "my-px",
            "-my-px",
            "my-",
            "-my-",
            "mt-px",
            "-mt-px",
            "mt-",
            "-mt-",
            "mr-px",
            "-mr-px",
            "mr-",
            "-mr-",
            "mb-px",
            "-mb-px",
            "mb-",
            "-mb-",
            "ml-px",
            "-ml-px",
            "ml-",
            "-ml-",

            "w-",
            "w-3xs",
            "w-2xs",
            "w-xs",
            "w-sm",
            "w-md",
            "w-lg",
            "w-xl",
            "w-2xl",
            "w-3xl",
            "w-4xl",
            "w-5xl",
            "w-6xl",
            "w-7xl",
            "w-auto",
            "w-px",
            "w-full",

            "size-",
            "size-auto",
            "size-px",
            "size-full",

            "min-w-",
            "min-w-3xs",
            "min-w-2xs",
            "min-w-xs",
            "min-w-sm",
            "min-w-md",
            "min-w-lg",
            "min-w-auto",
            "min-w-xl",
            "min-w-2xl",
            "min-w-3xl",
            "min-w-4xl",
            "min-w-5xl",
            "min-w-6xl",
            "min-w-7xl",
            "min-w-px",
            "min-w-full",

            "max-w-",
            "max-w-3xs",
            "max-w-2xs",
            "max-w-xs",
            "max-w-sm",
            "max-w-md",
            "max-w-lg",
            "max-w-xl",
            "max-w-2xl",
            "max-w-3xl",
            "max-w-4xl",
            "max-w-5xl",
            "max-w-6xl",
            "max-w-7xl",
            "max-w-px",

            "h-",
            "h-auto",
            "h-px",
            "h-full",
            "h-screen",

            "min-h-",
            "min-h-px",
            "min-h-full",

            "max-h-",
            "max-h-px",
            "max-h-full",

            "text-",
            "text-xs",
            "text-sm",
            "text-base",
            "text-lg",
            "text-xl",
            "text-2xl",
            "text-3xl",
            "text-4xl",
            "text-5xl",
            "text-6xl",
            "text-7xl",
            "text-8xl",
            "text-9xl",

            "italic",
            "not-italic",

            "font-normal",
            "font-medium",
            "font-semibold",
            "font-bold",
            "font-extrabold",
            "font-black",

            "tracking-",
            "tracking-tighter",
            "tracking-tight",
            "tracking-normal",
            "tracking-wide",
            "tracking-wider",
            "tracking-widest",

            "text-left",
            "text-center",
            "text-right",
            "text-start",
            "text-end",

            "truncate",
            "text-ellipsis",
            "text-clip",


            "whitespace-normal",
            "whitespace-nowrap",
            "whitespace-pre",
            "whitespace-pre-wrap",

            "bg-none",

            "bg-top-left",
            "bg-top",
            "bg-top-right",
            "bg-left",
            "bg-center",
            "bg-right",
            "bg-bottom-left",
            "bg-bottom",
            "bg-bottom-right",

            "bg-repeat",
            "bg-repeat-x",
            "bg-repeat-y",
            "bg-repeat-space",
            "bg-repeat-round",
            "bg-no-repeat",

            "bg-auto",
            "bg-cover",
            "bg-contain",

            "bg-",
            "bg-position-",
            "bg-size-",

            "rounded-xs",
            "rounded-sm",
            "rounded-md",
            "rounded-lg",
            "rounded-xl",
            "rounded-2xl",
            "rounded-3xl",
            "rounded-4xl",
            "rounded-none",
            "rounded-full",
            "rounded-t-xs",
            "rounded-t-sm",
            "rounded-t-md",
            "rounded-t-lg",
            "rounded-t-xl",
            "rounded-t-2xl",
            "rounded-t-3xl",
            "rounded-t-4xl",
            "rounded-t-none",
            "rounded-t-full",
            "rounded-r-xs",
            "rounded-r-sm",
            "rounded-r-md",
            "rounded-r-lg",
            "rounded-r-xl",
            "rounded-r-2xl",
            "rounded-r-3xl",
            "rounded-r-4xl",
            "rounded-r-none",
            "rounded-r-full",
            "rounded-b-xs",
            "rounded-b-sm",
            "rounded-b-md",
            "rounded-b-lg",
            "rounded-b-xl",
            "rounded-b-2xl",
            "rounded-b-3xl",
            "rounded-b-4xl",
            "rounded-b-none",
            "rounded-b-full",
            "rounded-l-xs",
            "rounded-l-sm",
            "rounded-l-md",
            "rounded-l-lg",
            "rounded-l-xl",
            "rounded-l-2xl",
            "rounded-l-3xl",
            "rounded-l-4xl",
            "rounded-l-none",
            "rounded-l-full",
            "rounded-tl-xs",
            "rounded-tl-sm",
            "rounded-tl-md",
            "rounded-tl-lg",
            "rounded-tl-xl",
            "rounded-tl-2xl",
            "rounded-tl-3xl",
            "rounded-tl-4xl",
            "rounded-tl-none",
            "rounded-tl-full",
            "rounded-tr-xs",
            "rounded-tr-sm",
            "rounded-tr-md",
            "rounded-tr-lg",
            "rounded-tr-xl",
            "rounded-tr-2xl",
            "rounded-tr-3xl",
            "rounded-tr-4xl",
            "rounded-tr-none",
            "rounded-tr-full",
            "rounded-br-xs",
            "rounded-br-sm",
            "rounded-br-md",
            "rounded-br-lg",
            "rounded-br-xl",
            "rounded-br-2xl",
            "rounded-br-3xl",
            "rounded-br-4xl",
            "rounded-br-none",
            "rounded-br-full",
            "rounded-bl-xs",
            "rounded-bl-sm",
            "rounded-bl-md",
            "rounded-bl-lg",
            "rounded-bl-xl",
            "rounded-bl-2xl",
            "rounded-bl-3xl",
            "rounded-bl-4xl",
            "rounded-bl-none",
            "rounded-bl-full",

            "border",
            "border-x",
            "border-y",
            "border-t",
            "border-r",
            "border-b",
            "border-l",

            "border-x-",
            "border-y-",
            "border-t-",
            "border-r-",
            "border-b-",
            "border-l-",
            "border-",
            "rounded-t-",
            "rounded-r-",
            "rounded-b-",
            "rounded-l-",
            "rounded-tl-",
            "rounded-tr-",
            "rounded-bl-",
            "rounded-br-",
            "rounded-",
            "text-shadow-xs",
            "text-shadow-2xs",
            "text-shadow-sm",
            "text-shadow-md",
            "text-shadow-lg",
            "text-shadow-none",
            "text-shadow-",

            "opacity-",
            "filter-none",
            "blur-xs",
            "blur-sm",
            "blur-md",
            "blur-lg",
            "blur-xl",
            "blur-2xl",
            "blur-3xl",
            "blur-none",
            "grayscale",
            "invert",
            "sepia",
            "sepia-",
            "saturate-",
            "invert-",
            "hue-rotate-",
            "-hue-rotate-",
            "grayscale-",
            "contrast-",
            "brightness-",
            "blur-",
            "filter-",
            "transition",
            "transition-all",
            "transition-colors",
            "transition-opacity",
            "transition-shadow",
            "transition-transform",
            "transition-none",
            "transition-normal",
            "transition-discrete",
            "duration-initial",
            "ease-linear",
            "ease-in",
            "ease-out",
            "ease-in-out",
            "ease-initial",
            "delay-",
            "ease-",
            "duration-",
            "transition-",
            "rotate-none",
            "scale-none",
            "origin-center",
            "origin-top",
            "origin-top-right",
            "origin-right",
            "origin-bottom-right",
            "origin-bottom",
            "origin-bottom-left",
            "origin-left",
            "origin-top-left",
            "translate-none",
            "translate-full",
            "-translate-full",
            "translate-px",
            "-translate-px",
            "origin-",
            "translate-",
            "-translate-",
            "scale-",
            "-scale-",
            "-rotate-",
            "rotate-"
        };

        public static ListView MakePropertyField(this SerializedProperty property, bool canAdd = true)
        {
            var listView = new ListView()
            {
                makeItem = MakeItem,
                unbindItem = (ve, _) =>
                {
                    var propertyField = (PropertyField)ve.ElementAt(0);
                    var btn = (Button)ve.ElementAt(1);
                    btn.clickable = null;
                    propertyField.Unbind();
                },
                showBorder = true,
                reorderable = true,
                showFoldoutHeader = true,
                reorderMode = ListViewReorderMode.Animated,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                selectionType = SelectionType.Multiple,
                showBoundCollectionSize = false,
            };
            listView.BindProperty(property);
            listView.showBorder = true;
            var action = listView.MakeHeader(property, canAdd);

            SetBind(listView, property, action);
            return listView;
        }

        public static (ListView element, Action action) MakePropertyFieldWithUpdateAction(this SerializedProperty property, bool canAdd = true, bool canRemove = true)
        {
            var listView = new ListView()
            {
                makeItem = MakeItem,
                unbindItem = (ve, _) =>
                {
                    var propertyField = (PropertyField)ve.ElementAt(0);
                    var btn = (Button)ve.ElementAt(1);
                    btn.clickable = null;
                    propertyField.Unbind();
                },
                showBorder = true,
                reorderable = true,
                showFoldoutHeader = true,
                reorderMode = ListViewReorderMode.Animated,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                selectionType = SelectionType.Multiple,
                showBoundCollectionSize = false,
            };

            listView.BindProperty(property);
            listView.showBorder = true;
            var action = listView.MakeHeader(property, canAdd);
            listView.bindItem = (ve, index) =>
            {
                var propertyField = (PropertyField)ve.ElementAt(0);
                var btn = (Button)ve.ElementAt(1);

                if (!canRemove)
                {
                    btn.style.display = DisplayStyle.None;
                    propertyField.style.height = 25;
                }
                else
                {
                    btn.clicked += () =>
                    {
                        property.serializedObject.Update();
                        property.DeleteArrayElementAtIndex(index);
                        property.serializedObject.ApplyModifiedProperties();
                        action?.Invoke();
                    };
                }

                var prop = property.GetArrayElementAtIndex(index);
                propertyField.BindProperty(prop);
                propertyField.ElementAt(0).style.flexGrow = 1;
                propertyField.schedule.Execute(() =>
                {
                    if ((index % 2) == 0)
                    {
                        propertyField.parent.parent.parent.style.backgroundColor = Color.clear;
                    }
                    else
                    {
                        ColorUtility.TryParseHtmlString("#2C2C2C", out var _col);
                        propertyField.parent.parent.parent.style.backgroundColor = _col;
                    }
                });
            };
            listView.style.maxHeight = 400;

            return (listView, action);
        }

        public static void SetBind(ListView listView, SerializedProperty property, Action action)
        {
            listView.bindItem = (ve, index) =>
            {
                var propertyField = (PropertyField)ve.ElementAt(0);
                var btn = (Button)ve.ElementAt(1);
                btn.clicked += () =>
                {
                    property.serializedObject.Update();
                    property.DeleteArrayElementAtIndex(index);
                    property.serializedObject.ApplyModifiedProperties();
                    action?.Invoke();
                };

                var prop = property.GetArrayElementAtIndex(index);
                propertyField.BindProperty(prop);
                propertyField.ElementAt(0).style.flexGrow = 1;
                propertyField.schedule.Execute(() =>
                {
                    if ((index % 2) == 0)
                    {
                        propertyField.parent.parent.parent.style.backgroundColor = Color.clear;
                    }
                    else
                    {
                        UnityEngine.ColorUtility.TryParseHtmlString("#2C2C2C", out var _col);
                        propertyField.parent.parent.parent.style.backgroundColor = _col;
                    }
                });
            };
        }

        public static VisualElement CreateStyleDrawerUI(SerializedProperty property)
        {
            var container = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    flexGrow = 1,
                    alignItems = Align.Center
                }
            };

            var _property = property.FindPropertyRelative("property");
            var value = property.FindPropertyRelative("value");

            container.Add(new Label { text = _property.stringValue, style = { paddingLeft = 0, paddingRight = 0 } });
            container.Add(new Label { text = ":", style = { unityTextAlign = TextAnchor.MiddleCenter, unityFontStyleAndWeight = FontStyle.Bold, paddingLeft = 2, paddingRight = 2 } });
            container.Add(new Label { text = value.stringValue, style = { paddingLeft = 0, paddingRight = 0, marginLeft = 2 } });
            container.Add(new Label { text = ";", style = { unityTextAlign = TextAnchor.MiddleCenter, unityFontStyleAndWeight = FontStyle.Bold, paddingLeft = 0, paddingRight = 0 } });
            return container;
        }

        private static VisualElement MakeItem()
        {
            var mainCont = new VisualElement()
            {
                style =
            {
                flexGrow = 1,
                flexDirection = FlexDirection.Row,
            }
            };
            var container = new PropertyField
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    flexGrow = 1,
                    paddingLeft = 10,
                    paddingRight = 10,
                },
            };
            mainCont.Add(container);

            var btn = new Button { text = "x" };
            btn.style.alignSelf = Align.Center;
            btn.style.backgroundColor = Color.clear;
            RemoveRadius(btn);
            RemoveBorder(btn);
            AddBorder(btn);
            mainCont.Add(btn);
            return mainCont;
        }

        private static Action MakeHeader(this ListView listView, SerializedProperty listProp, bool canAdd)
        {
            var headerContainer = listView.Q<VisualElement>(className: "unity-base-field__input");
            var toggle = listView.Q<Toggle>(className: "unity-base-field");
            var scrollView = listView.Q(className: "unity-scroll-view");

            toggle.style.backgroundColor = Color.clear;

            var container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;
            container.style.justifyContent = Justify.SpaceBetween;
            container.style.flexGrow = 1;
            container.style.alignItems = Align.Center;
            container.style.backgroundColor = Color.clear;
            container.style.minHeight = 20;

            AddBorder(headerContainer);

            var itemCount = new Label
            {
                text = $"{listProp.arraySize} items"
            };

            container.Add(new Label(listProp.displayName));
            container.Add(itemCount);
            headerContainer.Add(container);

            RemoveBorder(scrollView);


            AddBorder(scrollView);
            RemoveRadius(scrollView);
            scrollView.style.borderTopWidth = 0;

            if (canAdd)
            {
                container.style.minHeight = StyleKeyword.Null;
                var addBtn = new Button()
                {
                    text = "+",
                    style =
                    {
                        width = 24,
                        height = 24,
                        alignItems = Align.Center,
                        justifyContent = Justify.Center,
                        paddingBottom = 0,
                        paddingTop = 0,
                        paddingLeft = 0,
                        paddingRight = 0,
                        marginBottom = 0,
                        marginLeft = 0,
                        marginTop = 0,
                        marginRight = 0,
                        backgroundColor = Color.clear,
                        borderBottomLeftRadius = 0,
                        borderBottomRightRadius = 0,
                        borderTopLeftRadius = 0,
                        borderTopRightRadius = 0
                    }
                };

                RemoveBorder(addBtn);
                addBtn.style.borderLeftColor = Color.black;
                addBtn.style.borderLeftWidth = 1;
                headerContainer.Add(addBtn);

                addBtn.clicked += () =>
                {
                    int newIndex = listProp.arraySize;
                    listProp.InsertArrayElementAtIndex(newIndex);
                    listProp.serializedObject.ApplyModifiedProperties();
                    itemCount.text = $"{listProp.arraySize} items";
                    listView.Rebuild();
                };
            }

            RemovePadding(scrollView);
            return () =>
            {
                itemCount.text = $"{listProp.arraySize} items";
            };
        }

        public static void AddBorder(this VisualElement element)
        {
            element.style.borderTopWidth = 1;
            element.style.borderBottomWidth = 1;
            element.style.borderRightWidth = 1;
            element.style.borderLeftWidth = 1;

            element.style.borderBottomColor = Color.black;
            element.style.borderTopColor = Color.black;
            element.style.borderRightColor = Color.black;
            element.style.borderLeftColor = Color.black;
        }

        private static void RemoveBorder(VisualElement element)
        {
            element.style.borderTopWidth = 0;
            element.style.borderBottomWidth = 0;
            element.style.borderRightWidth = 0;
            element.style.borderLeftWidth = 0;
        }

        public static void RemovePadding(this VisualElement element)
        {
            element.style.paddingBottom = 0;
            element.style.paddingLeft = 0;
            element.style.paddingRight = 0;
            element.style.paddingTop = 0;
        }

        public static void RemoveMargin(this VisualElement element)
        {
            element.style.marginBottom = 0;
            element.style.marginLeft = 0;
            element.style.marginRight = 0;
            element.style.marginTop = 0;
        }

        public static void RemoveRadius(this VisualElement element)
        {
            element.style.borderTopLeftRadius = 0;
            element.style.borderBottomLeftRadius = 0;
            element.style.borderTopRightRadius = 0;
            element.style.borderBottomRightRadius = 0;
        }
    }

    internal class AutoComplete : TextField
    {
        VisualElement rootElement;
        ListView autoCompleteListElement;
        readonly string autoCompleteClass = "autocomplete-element";
        string previousWord;
        private int _index = 21;

        public Func<List<string>> autoCompleteValue;
        public List<string> suggestions = new List<string>();
        public AutoComplete()
        {
            autoCompleteListElement = new ListView
            {
                makeItem = () =>
                {
                    var btn = new Button();
                    btn.style.whiteSpace = WhiteSpace.Normal;
                    //btn.style.textOverflow = TextOverflow.Ellipsis;
                    Helper.RemoveMargin(btn);
                    return btn;
                },
                bindItem = Bind,
                unbindItem = (ve, index) =>
                {
                    var btn = ve as Button;
                    btn.clickable = null;
                },
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                style =
                {
                    position = Position.Absolute,
                    width = 200,
                    maxHeight = 400,
                    display = DisplayStyle.None
                }
            };
            autoCompleteListElement.focusable = false;
            autoCompleteListElement.AddToClassList(autoCompleteClass);
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            RegisterCallback<ChangeEvent<string>>(OnInput);
            RegisterCallback<FocusInEvent>(OnFocusIn);
            RegisterCallback<FocusOutEvent>(OnFocusOut);
            previousWord = string.Empty;
        }

        ~AutoComplete()
        {
            UnregisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            UnregisterCallback<ChangeEvent<string>>(OnInput);
            UnregisterCallback<FocusInEvent>(OnFocusIn);
            UnregisterCallback<FocusOutEvent>(OnFocusOut);
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            if (panel.visualTree.name.StartsWith("rootVisualContainer"))
            {
                if (rootElement != panel.visualTree)
                {
                    rootElement = panel.visualTree;
                    if (rootElement.Q<ListView>(className: autoCompleteClass) == null)
                        rootElement.Add(autoCompleteListElement);
                    else
                        autoCompleteListElement = rootElement.Q<ListView>(className: autoCompleteClass);
                }
            } else
            {
                foreach (var item in panel.visualTree.Children())
                {
                    if (item.name.StartsWith("rootVisualContainer"))
                    {
                        if (rootElement != item)
                        {
                            rootElement = item;
                            if (rootElement.Q<ListView>(className: autoCompleteClass) == null)
                                rootElement.Add(autoCompleteListElement);
                            else
                                autoCompleteListElement = rootElement.Q<ListView>(className: autoCompleteClass);
                        }
                        break;
                    }
                }
            }
        }

        private void OnInput(ChangeEvent<string> evt)
        {
            if (string.IsNullOrEmpty(evt.newValue))
            {
                autoCompleteListElement.style.display = DisplayStyle.None;
            }
            else if (autoCompleteListElement.resolvedStyle.display == DisplayStyle.None)
            {
                autoCompleteListElement.style.display = DisplayStyle.Flex;
            }


            Vector2 worldPos = this.LocalToWorld(Vector2.zero);
            Vector2 localPos = rootElement.WorldToLocal(worldPos);

            float aHeight = resolvedStyle.height;

            autoCompleteListElement.style.left = localPos.x;
            autoCompleteListElement.style.top = localPos.y + aHeight;
            autoCompleteListElement.style.width = resolvedStyle.width;
            (_index, _, _) = GetChangeSpan(previousWord, evt.newValue);
            Suggest(GetChangedWord(previousWord, evt.newValue));
            previousWord = evt.newValue;
        }

        private void OnFocusOut(FocusOutEvent evt)
        {
            schedule.Execute(() => autoCompleteListElement.style.display = DisplayStyle.None).ExecuteLater(500);
            //autoCompleteList.style.display = DisplayStyle.None;
        }

        private void OnFocusIn(FocusInEvent evt)
        {
            autoCompleteListElement.style.display = DisplayStyle.None;
            autoCompleteListElement.itemsSource = suggestions;
            autoCompleteListElement.bindItem = Bind;
            autoCompleteListElement.Rebuild();
        }

        private static string GetChangedWord(string original, string modified)
        {
            var origSpan = original.AsSpan();
            var modSpan = modified.AsSpan();

            if (origSpan.SequenceEqual(modSpan))
                return string.Empty;

            int start = 0;
            int endOriginal = origSpan.Length - 1;
            int endModified = modSpan.Length - 1;

            // Find start of difference
            while (start < origSpan.Length && start < modSpan.Length && origSpan[start] == modSpan[start])
                start++;

            // Find end of difference
            while (endOriginal >= start && endModified >= start && origSpan[endOriginal] == modSpan[endModified])
            {
                endOriginal--;
                endModified--;
            }

            // Find word boundaries in modSpan around 'start'
            int wordStart = start;
            int wordEnd = start;

            while (wordStart > 0 && modSpan[wordStart - 1] != ' ' && modSpan[wordStart - 1] != '\n')
                wordStart--;

            while (wordEnd < modSpan.Length && modSpan[wordEnd] != ' ' && modSpan[wordEnd] != '\n')
                wordEnd++;

            var changedWordSpan = modSpan.Slice(wordStart, wordEnd - wordStart);

            // Only allocate string once here
            return changedWordSpan.ToString();
        }

        private static string ReplaceWordAtIndex(string originalText, string suggestion, int endIndex)
        {
            // Safety check
            if (endIndex >= originalText.Length || endIndex < 0)
                return originalText;

            // Find the start index of the word by scanning backwards
            int startIndex = endIndex;
            while (startIndex > 0 && originalText[startIndex - 1] != '\n' && !char.IsWhiteSpace(originalText[startIndex - 1]))
                startIndex--;

            // Replace the word in-place
            string before = originalText.Substring(0, startIndex);
            string after = originalText.Substring(endIndex + 1); // +1 to skip past the current word
            return before + suggestion + after;
        }

        private static (int index, int lengthOriginal, int lengthModified) GetChangeSpan(string original, string modified)
        {
            int start = 0;
            int endOriginal = original.Length - 1;
            int endModified = modified.Length - 1;

            // Find the start of the change
            while (start <= endOriginal && start <= endModified && original[start] == modified[start])
                start++;

            // If they are completely the same
            if (start > endOriginal && start > endModified)
                return (-1, 0, 0);

            // Find the end of the change
            while (endOriginal >= start && endModified >= start && original[endOriginal] == modified[endModified])
            {
                endOriginal--;
                endModified--;
            }

            int lengthOriginal = endOriginal - start + 1;
            int lengthModified = endModified - start + 1;

            return (start, lengthOriginal, lengthModified);
        }

        private void Bind(VisualElement ve, int index)
        {
            (ve as Button).text = suggestions[index];

            (ve as Button).clicked += () =>
            {
                SetValueWithoutNotify(ReplaceWordAtIndex(previousWord, suggestions[index], _index));
                _index = -1;
                previousWord = value;
                autoCompleteListElement.style.display = DisplayStyle.None;
                //Focus();
            };
        }

        private void Suggest(string searchValue)
        {
            
            if (autoCompleteValue == null) return;

            var autoCompleteList = autoCompleteValue();
            autoCompleteListElement.Clear();
            suggestions.Clear();

            if (searchValue.Length == 0)
            {
                autoCompleteListElement.style.display = DisplayStyle.None;
                return;
            }

            var candidate = autoCompleteList;
            //var candidate = autoCompleteList.AsParallel().Where(w => w.ToLower().Contains(searchValue.ToLower())).ToList();
            //var candidate = autoCompleteList;//.AsParallel().Where(w => char.ToLower(w[0]) == char.ToLower(searchValue[0])).ToList();

            if (candidate.Count == 0)
            {
 
                autoCompleteListElement.style.display = DisplayStyle.None;
                return;
            }
            suggestions.Clear();

            suggestions.AddRange(candidate.AsParallel().Where(word => FuzzyMatch(searchValue, word)).ToList());

            if (suggestions.Count == 0 || suggestions.Count == 1 && suggestions[0] == searchValue)
            {
                autoCompleteListElement.style.display = DisplayStyle.None;
                autoCompleteListElement.Clear();
                autoCompleteListElement.Clear();
            }
            autoCompleteListElement.Rebuild();
        }

        private static bool FuzzyMatch(string input, string word)
        {
            int i = 0, j = 0;

            while (i < input.Length && j < word.Length)
            {
                if (char.ToLower(input[i]) == char.ToLower(word[j]))
                    i++;
                j++;
            }

            return i == input.Length;
        }

        public void CloseAutoComplete()
        {
            if (autoCompleteListElement == null) return;
            autoCompleteListElement.Clear();
            autoCompleteListElement.style.display = DisplayStyle.None;
        }
    }
}