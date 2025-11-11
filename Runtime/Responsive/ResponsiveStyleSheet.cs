using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kostom.Style
{
	public class ResponsiveStyleSheet
	{
        private VisualElement? rootElement;
        public List<ResponsiveBaseElement>? Elements;
        
        public Dictionary<string, Dictionary<string, UssValue>>? ParsedTheme { get; private set; }
        public float DefaultSpacing
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

        public void Initialize()
        {
            if (rootElement == null) return;
            InitElements();
            
            Elements?.ForEach(x => x.responsiveStyleSheet = this);
            var allChildren = rootElement.Query<VisualElement>().ToList();
            foreach (var element in allChildren)
            {
                Elements?.ForEach(x =>
                {
                    if (x.CheckCompatibility(element))
                    {
                        x.TryAddResponsiveStyleSheet(this);
                        x.AddElement(element);
                    }
                });
            }
        }

        public void AddClass(VisualElement element, params string[] classes)
        {
            Elements?.ForEach(x =>
            {
                if (x.Elements != null && x.Elements.Contains(element))
                {
                    foreach (var item in classes)
                    {
                        if (x.CheckCompatibility(item.DecodeClass()))
                            x.OnClassAdded(element, item.DecodeClass());
                    }

                }
                else
                {
                    if (x.CheckCompatibility(element))
                    {
                        x.AddElement(element);
                    }
                }
            });
        }

        public void RemoveClass(VisualElement element, string[] classes)
        {
            Elements?.ForEach(x =>
            {
                if (x.Elements != null && x.Elements.Contains(element))
                {
                    foreach (var item in classes)
                    {
                        if (x.CheckCompatibility(item.DecodeClass()))
                            x.OnClassRemoved(element, item.DecodeClass());
                    }

                    if (x.CanRemoveElement(element))
                    {
                        x.RemoveElement(element);
                    }
                }
            });
        }

        public void SetRootElement(VisualElement rootElement)
        {
            this.rootElement = rootElement;
        }

        public void SetParsedTheme(Dictionary<string, Dictionary<string, UssValue>>? ParsedTheme)
        {
            this.ParsedTheme = ParsedTheme;
        }

        public void Reset()
        {
            if (rootElement == null) return;
            Elements?.ForEach(x => x.ResetInternal(rootElement));
        }

        public void InitElements()
        {
            Elements = new List<ResponsiveBaseElement>
            {
                new MediaQuery(),
                new Orientation(),
                new Gap()
            };
        }

        public VisualElement? GetRootElement => rootElement;
    }
}

