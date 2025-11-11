using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kostom.Style
{
    public class Orientation : ResponsiveBaseElement
	{
        private Dictionary<VisualElement, VisualElement> ElementAndRoot = new Dictionary<VisualElement, VisualElement>();
        private Dictionary<VisualElement, List<string>> ElementsWithStyle = new Dictionary<VisualElement, List<string>>();
        private readonly List<string> ClassesNotToRemove = new List<string>();

        public override bool CheckCompatibility(string @class)
        {
            if (!@class.Contains(':')) return false;

            return !string.IsNullOrEmpty(@class.Split(':', StringSplitOptions.RemoveEmptyEntries)[..^1].FirstOrDefault(x => x.Trim() == "pt" || x.Trim() == "ls"));
        }

        public override void Initialize(VisualElement element)
        {
            var compClasses = GetCompatibleClasses(element);

            if (compClasses == null) return;

            if (ElementsWithStyle.ContainsKey(element)) return;

            ElementsWithStyle.Add(element, new List<string>());
            var root = GetRootElement(element);

            //remove incompatible classes and add to list
            foreach (var item in compClasses)
            {
                if ((ElementRootOrientation(element) == ScreenOrientation.LandscapeLeft) && item.Contains("pt:")) {
                    element.RemoveFromClassList(item.EncodeClass());
                }
                else if((ElementRootOrientation(element) == ScreenOrientation.Portrait) && item.Contains("ls:"))
                {
                    element.RemoveFromClassList(item.EncodeClass());
                }
                if(ElementsWithStyle.ContainsKey(element) && !ElementsWithStyle[element].Contains(item))
                {
                    
                    ElementsWithStyle[element].Add(item);
                }
            }

            if (root == null) return;
            root.RegisterCallback<GeometryChangedEvent>(OnGeo);
            ElementAndRoot.TryAdd(element, root);
        }

        public override void Reset(VisualElement element)
        {
            if (!ElementsWithStyle.ContainsKey(element)) return;

            RemoveElementsWithoutRoot();

            ElementsWithStyle.Remove(element);

            if (ElementAndRoot.TryGetValue(element, out var root))
            {
                ElementAndRoot.Remove(element);
                if (ElementAndRoot.FirstOrDefault(x => x.Value == root).Key != null) return;

                root.UnregisterCallback<GeometryChangedEvent>(OnGeo);
            }
        }

        public override bool CanRemoveElement(VisualElement element)
        {
            return !ElementsWithStyle.ContainsKey(element) || !GetAllCompatibleClasses(element).Any();
        }

        public override void OnClassAdded(VisualElement element, string @class)
        {
            if (ElementsWithStyle.ContainsKey(element))
            {
                if (ElementsWithStyle[element].Contains(@class)) return;

                ElementsWithStyle[element].Add(@class);
                if (ElementOrientation(element) == ScreenOrientation.LandscapeLeft && @class.Contains("ls:") && !element.ClassListContains(@class.EncodeClass()))
                {
                    element.AddClass(@class);
                }
                else if (ElementOrientation(element) == ScreenOrientation.Portrait && @class.Contains("pt:") && !element.ClassListContains(@class.EncodeClass()))
                {
                    element.AddClass(@class);
                }
            }
            else
            {
                Initialize(element);
            }


            if (ElementAndRoot.ContainsKey(element)) return;

            var root = GetRootElement(element);
            if (root == null) return;

            ElementAndRoot.TryAdd(element, root);
        }

        public override void OnClassRemoved(VisualElement element, string @class)
        {
            if (ElementsWithStyle.ContainsKey(element))
            {
                if (ClassesNotToRemove.Contains(@class))
                {
                    ClassesNotToRemove.RemoveAll(x => x == @class);
                    return;
                }

                ElementsWithStyle[element].Remove(@class);

                element.RemoveFromClassList(@class.EncodeClass());

                if (ElementsWithStyle[element].Count == 0)
                {
                    Reset(element);
                }
            }
        }

        private void OnGeo(GeometryChangedEvent evt)
        {
            //remove null elements
            RemoveElementsWithoutRoot();

            if (ElementsWithStyle.Count == 0) return;

            VisualElement element = (VisualElement)evt.target;
            foreach (var item in ElementsWithStyle)
            {
                var root = ElementAndRoot.ContainsKey(item.Key) ? ElementAndRoot[item.Key] : null;
                if (root == null) continue;
                if (root != element) continue;

                List<string>? compClasses = GetAllCompatibleClasses(item.Key);

                if (compClasses == null || compClasses.Count == 0) continue;

                if (ElementOrientation(element) == ScreenOrientation.LandscapeLeft)
                {
                    //using this instead of AddToClassList to add utility back to compatible elements
                    item.Key.AddClass(compClasses.FindAll(x => x.Contains("ls:")).ToArray());
                    RemoveClass(item.Key, compClasses.FindAll(x => x.Contains("pt:")).ToArray());
                }
                else
                {
                    //using this instead of AddToClassList to add utility back to compatible elements
                    item.Key.AddClass(compClasses.FindAll(x => x.Contains("pt:")).ToArray());
                    RemoveClass(item.Key, compClasses.FindAll(x => x.Contains("ls:")).ToArray());
                }

                ElementsWithStyle[item.Key].AddRange(item.Key.GetDecodedClasses().FindAll(x => CheckCompatibility(x) && !ElementsWithStyle[item.Key].Contains(x)));
            }
        }

        private ScreenOrientation ElementOrientation(VisualElement element)
        {
            if (element == null)
            {
                if (Application.isEditor)
                {
                    return Screen.width > Screen.height ? ScreenOrientation.LandscapeLeft : ScreenOrientation.Portrait;
                }

                return Screen.orientation;
            }
            return element.localBound.size.x > element.localBound.size.y ? ScreenOrientation.LandscapeLeft : ScreenOrientation.Portrait;
        }

        private ScreenOrientation ElementRootOrientation(VisualElement element)
        {
            var root = GetRootElement(element);
            if (root == null)
            {
                if (Application.isEditor)
                {
                    return Screen.width > Screen.height ? ScreenOrientation.LandscapeLeft : ScreenOrientation.Portrait;
                }

                return Screen.orientation;
            }

            if (float.IsNaN(root.localBound.size.x) || float.IsNaN(root.localBound.size.y) && root.name == "PanelSettings")
            {
                return Screen.width > Screen.height ? ScreenOrientation.LandscapeLeft : ScreenOrientation.Portrait;
            }
            return root.localBound.size.x > root.localBound.size.y ? ScreenOrientation.LandscapeLeft : ScreenOrientation.Portrait;
        }

        private void RemoveElementsWithoutRoot()
        {
            ElementsWithStyle = ElementsWithStyle.Where(x => x.Key != null && x.Key.panel != null && x.Key.panel.visualTree != null).ToDictionary(x => x.Key, x => x.Value);
            ElementAndRoot = ElementAndRoot.Where(x => x.Key != null && x.Value != null).ToDictionary(x => x.Key, x => x.Value);
        }

        private List<string> GetAllCompatibleClasses(VisualElement element)
        {
            var compClasses = GetCompatibleClasses(element);
            if(compClasses == null)
            {
                if (!ElementsWithStyle.ContainsKey(element)) return new List<string>();
                return ElementsWithStyle[element];
            }
            compClasses.AddRange(ElementsWithStyle[element].FindAll(x => !compClasses.Contains(x)));
            return compClasses;
        }

        private void RemoveClass(VisualElement element, string[] classes)
        {
            ClassesNotToRemove.AddRange(classes);
            element.RemoveClass(classes);
        }
    }
}

