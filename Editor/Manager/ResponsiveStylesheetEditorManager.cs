using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kostom.Style
{
	[InitializeOnLoad]
	internal static class ResponsiveStylesheetEditorManager
	{
        static double lastCheck;
        static List<EditorWindow> lastWindows = new List<EditorWindow>();
        static List<EditorWindow> newlyOpenWindow = new List<EditorWindow>();
        static List<EditorWindow> newlyClosedWindow = new List<EditorWindow>();

        static readonly Dictionary<string, ResponsiveStyleSheet> ElementsWithRSS;
        static ResponsiveStylesheetEditorManager()
        {
            ElementsWithRSS = new Dictionary<string, ResponsiveStyleSheet>();
            EditorApplication.update += CheckOpenWindows;
            WhirlHelper.EditorAddClass += EditorAddClass;
            WhirlHelper.EditorRemoveClass += EditorRemoveClass;
        }

        private static void EditorAddClass(VisualElement element, string[] classes)
        {
            foreach (var item in ElementsWithRSS)
            {
                item.Value.AddClass(element, classes);
            }
        }

        private static void EditorRemoveClass(VisualElement element, string[] classes)
        {
            foreach (var item in ElementsWithRSS)
            {
                item.Value.AddClass(element, classes);
            }
        }

        static void CheckOpenWindows()
        {
            if (EditorApplication.timeSinceStartup - lastCheck < 1.0)
                return;

            lastCheck = EditorApplication.timeSinceStartup;

            var currentWindows = Resources.FindObjectsOfTypeAll<EditorWindow>().ToList();

            newlyOpenWindow = currentWindows.Except(lastWindows).ToList();
            newlyClosedWindow = lastWindows.Except(currentWindows).ToList();

            lastWindows = currentWindows;
            ProcessWindow();
        }

        static void ProcessWindow()
        {
            if (newlyOpenWindow.Any())
            {
                foreach (EditorWindow window in newlyOpenWindow)
                {
                    if (string.IsNullOrEmpty(window.titleContent.text)) continue;
                    if (!window.rootVisualElement.name.StartsWith("rootVisualContainer")) continue;
                    string key = $"{window.titleContent.text}-{window.GetType().Name}-{window.rootVisualElement.name}";
                    if (ElementsWithRSS.ContainsKey(key)) continue;

                    ElementsWithRSS.Add(key, new ResponsiveStyleSheet());
                    ElementsWithRSS[key].SetParsedTheme(ProcessFile.CustomTheme);


                    if (window.titleContent.text == "UI Toolkit Debugger" && window.GetType().Name == "UIElementsDebugger")
                        continue;


                    if (window.titleContent.text == "UI Builder" && window.GetType().Name == "Builder")
                    {
                        window.rootVisualElement.schedule.Execute(() =>
                        {
                            VisualElement element = window.rootVisualElement.Q<VisualElement>(className: "unity-builder-viewport__document");
                            ElementsWithRSS[key].SetRootElement(element);
                            ElementsWithRSS[key].Initialize();
                        }).ExecuteLater(30);
                    }
                    else
                    {
                        ElementsWithRSS[key].SetRootElement(window.rootVisualElement);
                        window.rootVisualElement.schedule.Execute(() =>
                        {
                            ElementsWithRSS[key].Initialize();
                        }).ExecuteLater(30);
                    }
                }
                newlyOpenWindow.Clear();
            }

            if (newlyClosedWindow.Any())
            {
                foreach (var window in newlyClosedWindow)
                {
                    string key = $"{window.titleContent.text}-{window.GetType().Name}-{window.rootVisualElement.name}";
                    if (!ElementsWithRSS.ContainsKey(key)) continue;
                    ElementsWithRSS[key].Reset();
                    ElementsWithRSS.Remove(key);
                }
                newlyClosedWindow.Clear();
            }
        }
    }

    internal class TestWindow : EditorWindow
    {
        public static StyleSheet? StyleSheet
        {
            get
            {
                string[] guids = AssetDatabase.FindAssets("t:StyleSheet", new[] { "Assets" });
                StyleSheet? sheet = default;

                foreach (var guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    var asset = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);

                    if (asset.name == "ExampleUSS")
                    {
                        sheet = asset;
                        break;
                    }
                }

                return sheet;
            }
        }

        [MenuItem("Tools/Test")]
        static void CreateMenu()
        {
            var edt = GetWindow<TestWindow>();
            edt.titleContent = new GUIContent("Test Window");
            edt.minSize = new Vector2(200, 200);
        }

        private void CreateGUI()
        {
            StyleSheet? sheet = StyleSheet;
            if (sheet != null) {
                rootVisualElement.styleSheets.Add(sheet);
            }

            var container = new VisualElement
            {
                style =
                {
                    width = Length.Percent(100),
                    height = Length.Percent(100),
                }
            };
            container.AddToClassList("gap-2");
            container.Add(new VisualElement
            {
                style =
                {
                    width = Length.Percent(100),
                    height = 40,
                    backgroundColor = Color.red
                }
            });
            container.Add(new VisualElement
            {
                style =
                {
                    width = Length.Percent(100),
                    height = 40,
                    backgroundColor = Color.red
                }
            });
            container.Add(new VisualElement
            {
                style =
                {
                    width = Length.Percent(100),
                    height = 40,
                    backgroundColor = Color.red
                }
            });

            container.schedule.Execute(() =>
            {
                container.AddClass("gap-3");
            }).ExecuteLater(1000);
            rootVisualElement.Add(container);
        }
    }
}