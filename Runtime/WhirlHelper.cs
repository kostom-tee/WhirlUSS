using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kostom.Style
{
    public static class WhirlHelper
    {
        public static Action<VisualElement, string[]>? EditorAddClass, EditorRemoveClass;

        public static List<string> ParseTheme(this Theme[] theme, Dictionary<string, Dictionary<string, UssValue>> CustomTheme, bool OutputExtra = true)
        {
            List<string> extraRootVariable = new List<string>();

            //parse default
            foreach (var item in theme)
            {
                if (item.IsDefault)
                {
                    if (item.isActive && item.styleProperties != null)
                    {
                        foreach (var _theme in item.styleProperties)
                        {
                            ParseTheme(_theme);
                        }
                    }
                    break;
                }
            }

            //parse override
            foreach (var item in theme)
            {
                if (item.IsDefault) continue;
                if (item.isActive && item.styleProperties != null)
                {
                    foreach (var _theme in item.styleProperties)
                    {
                        ParseTheme(_theme);
                    }
                }
            }

            return extraRootVariable;

            void ParseTheme(StyleProperty theme)
            {
                if (string.IsNullOrEmpty(theme.property) || string.IsNullOrEmpty(theme.value)) return;

                UssValue uss = new StaticValue(theme.value);


                if (theme.property.StartsWith("--color-"))
                {
                    if (!CustomTheme.ContainsKey("color"))
                    {
                        CustomTheme.Add("color", new Dictionary<string, UssValue>());
                    }
                    var val = theme.property["--color-".Length..];
                    if (string.IsNullOrEmpty(val.Trim())) return;
                    if (!CustomTheme["color"].ContainsKey(val))
                    {
                        CustomTheme["color"].Add(val, uss);
                    }
                    else
                    {
                        CustomTheme["color"][val] = uss;
                    }
                }
                else if (theme.property.StartsWith("--font-"))
                {
                    if (!CustomTheme.ContainsKey("font"))
                    {
                        CustomTheme.Add("font", new Dictionary<string, UssValue>());
                    }

                    var val = theme.property["--font-".Length..];
                    if (string.IsNullOrEmpty(val.Trim())) return;
                    if (!CustomTheme["font"].ContainsKey(val))
                    {
                        CustomTheme["font"].Add(val, uss);
                    }
                    else
                    {
                        CustomTheme["font"][val] = uss;
                    }
                }
                else if (theme.property.StartsWith("--text-"))
                {
                    if (!CustomTheme.ContainsKey("text"))
                    {
                        CustomTheme.Add("text", new Dictionary<string, UssValue>());
                    }

                    var val = theme.property["--text-".Length..];
                    if (string.IsNullOrEmpty(val.Trim())) return;
                    if (!CustomTheme["text"].ContainsKey(val))
                    {
                        CustomTheme["text"].Add(val, uss);
                    }
                    else
                    {
                        CustomTheme["text"][val] = uss;
                    }
                }
                else if (theme.property.StartsWith("--font-weight-"))
                {
                    if (!CustomTheme.ContainsKey("font-weight"))
                    {
                        CustomTheme.Add("font-weight", new Dictionary<string, UssValue>());
                    }

                    var val = theme.property["--font-weight-".Length..];
                    if (string.IsNullOrEmpty(val.Trim())) return;
                    if (!CustomTheme["font-weight"].ContainsKey(val))
                    {
                        CustomTheme["font-weight"].Add(val, uss);
                    }
                    else
                    {
                        CustomTheme["font-weight"][val] = uss;
                    }
                }
                else if (theme.property.StartsWith("--tracking-"))
                {
                    if (!CustomTheme.ContainsKey("tracking"))
                    {
                        CustomTheme.Add("tracking", new Dictionary<string, UssValue>());
                    }

                    var val = theme.property["--tracking-".Length..];
                    if (string.IsNullOrEmpty(val.Trim())) return;
                    if (!CustomTheme["tracking"].ContainsKey(val))
                    {
                        CustomTheme["tracking"].Add(val, uss);
                    }
                    else
                    {
                        CustomTheme["tracking"][val] = uss;
                    }
                }
                else if (theme.property.StartsWith("--breakpoint-"))
                {
                    if (!CustomTheme.ContainsKey("breakpoint"))
                    {
                        CustomTheme.Add("breakpoint", new Dictionary<string, UssValue>());
                    }

                    var val = theme.property["--breakpoint-".Length..];

                    if (string.IsNullOrEmpty(val.Trim())) return;

                    var bpUSS = new StaticValue(uss.Render().Replace("px",""));
                    if (!CustomTheme["breakpoint"].ContainsKey(val))
                    {
                        CustomTheme["breakpoint"].Add(val, bpUSS);
                    }
                    else
                    {
                        CustomTheme["breakpoint"][val] = bpUSS;
                    }
                }
                else if (theme.property.StartsWith("--spacing-"))
                {
                    if (!CustomTheme.ContainsKey("spacing"))
                    {
                        CustomTheme.Add("spacing", new Dictionary<string, UssValue>());
                    }

                    var val = theme.property["--spacing-".Length..];
                    if (string.IsNullOrEmpty(val.Trim())) return;
                    if (!CustomTheme["spacing"].ContainsKey(val))
                    {

                        CustomTheme["spacing"].Add(val, uss);
                    }
                    else
                    {
                        CustomTheme["spacing"][val] = uss;
                    }
                }
                else if (theme.property.StartsWith("--spacing"))
                {
                    if (!CustomTheme.ContainsKey("spacing"))
                    {
                        CustomTheme.Add("spacing", new Dictionary<string, UssValue>());
                    }

                    if (!CustomTheme["spacing"].ContainsKey("default"))
                    {

                        CustomTheme["spacing"].Add("default", uss);
                    }
                    else
                    {
                        CustomTheme["spacing"]["default"] = uss;
                    }
                }
                else if (theme.property.StartsWith("--radius-"))
                {
                    if (!CustomTheme.ContainsKey("radius"))
                    {
                        CustomTheme.Add("radius", new Dictionary<string, UssValue>());
                    }

                    var val = theme.property["--radius-".Length..];
                    if (string.IsNullOrEmpty(val.Trim())) return;
                    if (!CustomTheme["radius"].ContainsKey(val))
                    {
                        CustomTheme["radius"].Add(val, uss);
                    }
                    else
                    {
                        CustomTheme["radius"][val] = uss;
                    }
                }
                else if (theme.property.StartsWith("--shadow-"))
                {
                    if (!CustomTheme.ContainsKey("shadow"))
                    {
                        CustomTheme.Add("shadow", new Dictionary<string, UssValue>());
                    }

                    var val = theme.property["--shadow-".Length..];
                    if (string.IsNullOrEmpty(val.Trim())) return;
                    if (!CustomTheme["shadow"].ContainsKey(val))
                    {
                        CustomTheme["shadow"].Add(val, uss);
                    }
                    else
                    {
                        CustomTheme["shadow"][val] = uss;
                    }
                }
                else if (theme.property.StartsWith("--blur-"))
                {
                    if (!CustomTheme.ContainsKey("blur"))
                    {
                        CustomTheme.Add("blur", new Dictionary<string, UssValue>());
                    }

                    var val = theme.property["--blur-".Length..];
                    if (string.IsNullOrEmpty(val.Trim())) return;
                    if (!CustomTheme["blur"].ContainsKey(val))
                    {
                        CustomTheme["blur"].Add(val, uss);
                    }
                    else
                    {
                        CustomTheme["blur"][val] = uss;
                    }
                }
                else if (theme.property.StartsWith("--aspect-"))
                {
                    if (!CustomTheme.ContainsKey("aspect"))
                    {
                        CustomTheme.Add("aspect", new Dictionary<string, UssValue>());
                    }

                    var val = theme.property["--aspect-".Length..];
                    if (string.IsNullOrEmpty(val.Trim())) return;
                    if (!CustomTheme["aspect"].ContainsKey(val))
                    {
                        CustomTheme["aspect"].Add(val, uss);
                    }
                    else
                    {
                        CustomTheme["aspect"][val] = uss;
                    }
                }
                else if (theme.property.StartsWith("--ease-"))
                {
                    if (!CustomTheme.ContainsKey("ease"))
                    {
                        CustomTheme.Add("ease", new Dictionary<string, UssValue>());
                    }

                    var val = theme.property["--ease-".Length..];
                    if (string.IsNullOrEmpty(val.Trim())) return;
                    if (!CustomTheme["ease"].ContainsKey(val))
                    {
                        CustomTheme["ease"].Add(val, uss);
                    }
                    else
                    {
                        CustomTheme["ease"][val] = uss;
                    }
                }
                else if (theme.property.StartsWith("--animate-"))
                {
                    if (!CustomTheme.ContainsKey("animate"))
                    {
                        CustomTheme.Add("animate", new Dictionary<string, UssValue>());
                    }

                    var val = theme.property["--animate-".Length..];
                    if (string.IsNullOrEmpty(val.Trim())) return;
                    if (!CustomTheme["animate"].ContainsKey(val))
                    {
                        CustomTheme["animate"].Add(val, uss);
                    }
                    else
                    {
                        CustomTheme["animate"][val] = uss;
                    }
                }
                else if (OutputExtra)
                {
                    extraRootVariable.Add($"{theme.property}: {theme.value};");
                }
            }
        }

        public static string DecodeClass(this string str) => str
            .Replace("---", ":")
            .Replace("_b_", "[")
            .Replace("_B_", "]")
            .Replace("_p_", "(")
            .Replace("_P_", ")")
            .Replace("_s_", "/")
            .Replace("_c_", ",")
            .Replace("_st_", "*");

        public static List<string> GetDecodedClasses(this VisualElement element)
        {
            var classes = element.GetClasses().ToList();
            for (int i = 0; i < classes.Count(); i++)
            {
                classes[i] = classes[i].DecodeClass();
            }
            return classes;
        }

        public static string EncodeClass(this string str) => str
            .Replace(":", "---")
            .Replace("[", "_b_")
            .Replace("]", "_B_")
            .Replace("(", "_p_")
            .Replace(")", "_P_")
            .Replace("/", "_s_")
            .Replace(",", "_c_")
            .Replace("*", "_st_");

        internal static void SetUpWhirlManager()
        {
            if(Application.isPlaying && WhirlManager.manager == null)
            {
                new GameObject("[WhirlManager]") { hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector }.AddComponent<WhirlManager>();
            }
        }

        public static T AddClass<T>(this T element, params string[] classes) where T : VisualElement
        {
            foreach (var item in classes)
            {
                if (element.GetDecodedClasses().Contains(item)) continue;
                element.AddToClassList(item.EncodeClass());
            }

            SetUpWhirlManager();

            //not added to root yet
            if (element.panel == null || element.panel.visualTree == null)
            {
                return element;
            }

            if (Application.isPlaying && WhirlManager.manager!.IsRuntimeElement(element))
            {
                WhirlManager.manager.AddClass(element, classes);
            }
            else
            {
                EditorAddClass?.Invoke(element, classes);
            }
            return element;
        }

        public static T RemoveClass<T>(this T element, params string[] classes) where T : VisualElement
        {
            foreach (var item in classes)
            {
                if (!element.GetDecodedClasses().Contains(item)) continue;
                element.RemoveFromClassList(item.EncodeClass());
            }

            SetUpWhirlManager();
            //not added to root yet
            if (element.panel == null || element.panel.visualTree == null)
            {
                return element;
            }
            if (Application.isPlaying && WhirlManager.manager!.IsRuntimeElement(element))
            {
                WhirlManager.manager.RemoveClass(element, classes);
            }
            else
            {
                EditorRemoveClass?.Invoke(element, classes);
            }
            return element;
        }
    }
}