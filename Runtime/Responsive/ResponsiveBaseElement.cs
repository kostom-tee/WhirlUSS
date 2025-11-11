using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

namespace Kostom.Style
{
    public abstract class ResponsiveBaseElement
    {
        public ResponsiveStyleSheet? responsiveStyleSheet { get; internal set; }
        internal List<VisualElement>? Elements { get; private set; }
        public abstract bool CheckCompatibility(string @class);
        public abstract void Reset(VisualElement element);
        public abstract void Initialize(VisualElement element);
        public abstract bool CanRemoveElement(VisualElement element);
        public virtual void OnClassAdded(VisualElement element, string @class) { }
        public virtual void OnClassRemoved(VisualElement element, string @class) { }
        public List<string>? GetCompatibleClasses(VisualElement element)
        {
            if (element == null || Elements == null || !Elements.Contains(element)) return default;
            var classes = element.GetDecodedClasses();
            var compatibleClasses = classes.FindAll(CheckCompatibility);

            for (int i = 0; i < compatibleClasses.Count; i++)
            {
                compatibleClasses[i] = compatibleClasses[i].DecodeClass();
            }
            return compatibleClasses.Any() ? compatibleClasses : default;
        }
        public VisualElement? GetRootElement(VisualElement element)
        {
            if (element.panel == null || element.panel.visualTree == null)
            {
                return null;
            }

            //Checking for ui builder panel and regular panel
            return element.panel.visualTree.Q<VisualElement>(name: "canvas", className: "unity-builder-viewport__canvas") ?? element.panel.visualTree;
        }

        internal void ResetInternal()
        {
            Elements?.ForEach(Reset);
        }
        internal void ResetInternal(VisualElement element)
        {
            if (Elements != null && Elements.Contains(element))
            {
                Reset(element);
                Elements.Remove(element);
            }
        }
        internal bool CheckCompatibility(VisualElement element)
        {
            var classes = element.GetClasses();
            foreach (var item in classes)
            {
                if (CheckCompatibility(item.DecodeClass())) return true;
            }
            return false;
        }
        internal void AddElement(VisualElement element)
        {
            Elements ??= new List<VisualElement>();
            if (Elements.Contains(element)) return;
            Elements.Add(element);
            Initialize(element);
        }
        internal void TryAddResponsiveStyleSheet(ResponsiveStyleSheet responsiveStyle)
        {
            responsiveStyleSheet = responsiveStyle;
        }
        internal void RemoveElement(VisualElement element)
        {
            ResetInternal(element);
        }

        ~ResponsiveBaseElement()
        {
            ResetInternal();
        }
    }
}

