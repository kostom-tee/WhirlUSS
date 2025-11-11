using UnityEngine;
using UnityEngine.UIElements;

namespace Kostom.Style
{
    public class Gap : ResponsiveBaseElement
    {
        public override bool CheckCompatibility(string item)
        {
            return item.Split(':', System.StringSplitOptions.RemoveEmptyEntries)[^1].StartsWith("gap-");
        }

        public override void Initialize(VisualElement element)
        {
            element.RegisterCallback<GeometryChangedEvent>(OnGeoChanged);
            ProcessChildOnDetached(element);
        }

        public override void Reset(VisualElement element)
        {
            ResetGap(element);
            element.UnregisterCallback<GeometryChangedEvent>(OnGeoChanged);
            element.UnregisterCallback<DetachFromPanelEvent>(OnDetached);
        }

        public override void OnClassAdded(VisualElement element, string @class)
        {
            ProcessGap(element);
        }

        public override void OnClassRemoved(VisualElement element, string @class)
        {
            ResetGap(element);
            ProcessGap(element);
        }

        private void OnGeoChanged(GeometryChangedEvent evt)
        {
            VisualElement element = (VisualElement)evt.currentTarget;
            ProcessGap(element);
        }

        private void ProcessGap(VisualElement element)
        {
            var compClasses = GetCompatibleClasses(element);
            if (compClasses == null) return;

            int count = element.contentContainer.childCount - 1;
            if (count <= 0) return;
            foreach (var item in compClasses)
            {
                string classVal = item[(item.IndexOf("gap-") + "gap-".Length)..];
                string className = $"gap-{classVal}";
                VisualElement _element;
                for (int i = 0; i < count; i++)
                {
                    _element = element.contentContainer.ElementAt(i);
                    if (className.StartsWith("gap-x-") && !_element.ClassListContains($"child-gap-x-{classVal}"))
                    {
                        _element.RemoveFromClassList($"child-gap-y-{classVal}");
                        _element.AddToClassList($"child-gap-x-{classVal}");
                    }
                    else if (className.StartsWith("gap-y-") && !_element.ClassListContains($"child-gap-y-{classVal}"))
                    {
                        _element.RemoveFromClassList($"child-gap-x-{classVal}");
                        _element.AddToClassList($"child-gap-y-{classVal}");
                    }
                    else if (className.StartsWith("gap-"))
                    {
                        if (element.resolvedStyle.flexDirection == FlexDirection.Row || element.resolvedStyle.flexDirection == FlexDirection.RowReverse)
                        {
                            _element.RemoveFromClassList($"child-gap-y-{classVal}");
                            _element.AddToClassList($"child-gap-x-{classVal}");
                        }
                        else
                        {
                            _element.RemoveFromClassList($"child-gap-x-{classVal}");
                            _element.AddToClassList($"child-gap-y-{classVal}");
                        }
                    }
                }
            }
        }

        private void ResetGap(VisualElement element)
        {
            var compClasses = GetCompatibleClasses(element);
            if (compClasses == null) return;

            int count = element.contentContainer.childCount - 1;
            if (count <= 0) return;

            foreach (var item in compClasses)
            {
                string classVal = item[(item.IndexOf("gap-") + "gap-".Length)..];
                VisualElement _element;
                for (int i = 0; i < count; i++)
                {
                    _element = element.contentContainer.ElementAt(i);
                    _element.RemoveFromClassList($"child-gap-x-{classVal}");
                    _element.RemoveFromClassList($"child-gap-y-{classVal}");
                    _element.RemoveFromClassList($"child-gap-{classVal}");
                }
            }
        }

        private void ProcessChildOnDetached(VisualElement element)
        {
            for (int i = 0; i < element.childCount; i++)
            {
                element.ElementAt(i).RegisterCallback<DetachFromPanelEvent>(OnDetached);
            }
        }

        private void OnDetached(DetachFromPanelEvent evt)
        {
            VisualElement element = ((VisualElement)evt.currentTarget).parent;
            ResetGap(element);
            ProcessGap(element);
        }

        public override bool CanRemoveElement(VisualElement element)
        {
            return GetCompatibleClasses(element) == null;
        }
    }
}