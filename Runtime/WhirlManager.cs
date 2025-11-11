using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Kostom.Style
{
    public sealed class WhirlManager: MonoBehaviour
    {
        public static WhirlManager? manager;
        public Theme[] theme = new Theme[0];
        public static Dictionary<string, Dictionary<string, UssValue>>? ParsedTheme;
        public static float DefaultSpacing
        {
            get
            {
                float val = 4;
                if (ParsedTheme != null && ParsedTheme.ContainsKey("spacing") && ParsedTheme["spacing"].ContainsKey("default"))
                {
                    if (!float.TryParse(ParsedTheme["spacing"]["default"].Render().Replace("px", "").Trim(), out val))
                    {
                        val = 4;
                    }
                }
                return val;
            }
        }

        ResponsiveStyleSheet? ResponsiveStyleSheet;

        private void OnEnable()
        {
            if(manager == null)
            {
                manager = this;
                DontDestroyOnLoad(this);
            }
            ParsedTheme = new Dictionary<string, Dictionary<string, UssValue>>();
            theme.ParseTheme(ParsedTheme, false);
            ResponsiveStyleSheet ??= new ResponsiveStyleSheet();
            Initialize();

            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        private void OnDisable()
        {
            ParsedTheme?.Clear();
            ResponsiveStyleSheet?.Reset();
            SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
        }

        private void Initialize()
        {
            var rootElement = FindAnyObjectByType<UIDocument>().rootVisualElement;
            ResponsiveStyleSheet?.SetRootElement(rootElement.panel.visualTree);
            ResponsiveStyleSheet?.SetParsedTheme(ParsedTheme);
            ResponsiveStyleSheet?.Initialize();
        }

        private void SceneManager_activeSceneChanged(Scene previousScene, Scene newScene)
        {
            if (previousScene.IsValid() && newScene.IsValid())
            {
                Initialize();
            }
        }

        internal bool IsRuntimeElement(VisualElement element)
        {
            return element.panel.visualTree == ResponsiveStyleSheet?.GetRootElement;
        }

        internal void AddClass(VisualElement element, params string[] classes)
        {
            ResponsiveStyleSheet?.AddClass(element, classes);
        }

        internal void RemoveClass(VisualElement element, params string[] classes)
        {
            ResponsiveStyleSheet?.RemoveClass(element, classes);
        }
    }
}