using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kostom.Style
{
    public class MediaQuery : ResponsiveBaseElement
    {
        private readonly Dictionary<VisualElement, VisualElement> ElementAndRoot = new Dictionary<VisualElement, VisualElement>();
        private readonly Dictionary<VisualElement, Dictionary<string, List<string>>> ElementsWithBreakpointAndStyle = new Dictionary<VisualElement, Dictionary<string, List<string>>>();
        private readonly List<string> ClassesNotToRemove = new List<string>();

        public override bool CheckCompatibility(string item)
        {
            return responsiveStyleSheet?.ParsedTheme != null && !string.IsNullOrEmpty(item.Split(':', StringSplitOptions.RemoveEmptyEntries)[..^1].FirstOrDefault(x => responsiveStyleSheet!.ParsedTheme.ContainsKey("breakpoint") && responsiveStyleSheet!.ParsedTheme["breakpoint"].ContainsKey(x)));
        }

        public override void Initialize(VisualElement element)
        {
            var compClasses = GetCompatibleClasses(element);

            if (compClasses == null) return;

            if (ElementsWithBreakpointAndStyle.ContainsKey(element)) return;

            ElementsWithBreakpointAndStyle.Add(element, new Dictionary<string, List<string>>());
            var root = GetRootElement(element);

            //add elements and there extracted breakpoints and remove it from the element
            foreach (var item in compClasses)
            {
                if (responsiveStyleSheet == null || responsiveStyleSheet.ParsedTheme == null || !responsiveStyleSheet.ParsedTheme.ContainsKey("breakpoint")) continue;

                var bp = GetBreakPoint(item)!;
                if (ElementsWithBreakpointAndStyle[element].TryGetValue(bp, out var val))
                {
                    val.Add(item);
                }
                else
                {
                    ElementsWithBreakpointAndStyle[element][bp] = new List<string> { item };
                }

            }

            if (root == null) return;
            root.RegisterCallback<GeometryChangedEvent>(OnGeo);
            ElementAndRoot.TryAdd(element, root);
        }

        public override void Reset(VisualElement element)
        {
            
        }

        public override bool CanRemoveElement(VisualElement element)
        {
            return !ElementsWithBreakpointAndStyle.ContainsKey(element) || !GetAllCompatibleClasses(element).Any();
        }

        public override void OnClassAdded(VisualElement element, string @class)
        {
            if (ElementsWithBreakpointAndStyle.ContainsKey(element))
            {
                var bp = GetBreakPoint(@class);
                if (string.IsNullOrEmpty(bp) || !ElementsWithBreakpointAndStyle[element].ContainsKey(bp) || ElementsWithBreakpointAndStyle[element][bp].Contains(@class)) return;
                ElementsWithBreakpointAndStyle[element][bp].Add(@class);
                var width = element.localBound.width;

                if (!float.TryParse(responsiveStyleSheet!.ParsedTheme!["breakpoint"][bp].Render(), out var curWidth) || curWidth > width) return;
                element.AddClass(@class);
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
            if (ClassesNotToRemove.Contains(@class))
            {
                ClassesNotToRemove.RemoveAll(x => x == @class);
                return;
            }
        }

        private void OnGeo(GeometryChangedEvent evt)
        {
            var element = (VisualElement)evt.target;
            var width = element.localBound.width;

            if (responsiveStyleSheet == null || responsiveStyleSheet.ParsedTheme == null || !responsiveStyleSheet.ParsedTheme.ContainsKey("breakpoint")) return;

            //get breakpoint that's less than the current screen
            var val = responsiveStyleSheet.ParsedTheme!["breakpoint"].Where(x => {
                if (float.TryParse(x.Value.Render(), out var val))
                {
                    return val < width;
                }
                return false;
            }).ToDictionary(x => x.Key, x => float.Parse(x.Value.Render()));
            if (val.Any())
            {
                foreach (var item in ElementsWithBreakpointAndStyle)
                {
                    //get relevant classes
                    var styles = item.Value.Where(x => val.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value).Values.SelectMany(x => x).ToArray();
                    //get classes to remove from element
                    var stylesOnElementToRemove = GetCompatibleClasses(item.Key)?.Where(x =>
                    {
                        var bp = GetBreakPoint(x);
                        return string.IsNullOrEmpty(bp) || !val.ContainsKey(bp);
                    }).ToArray();

                    if (styles.Any())
                    {
                        //add supported styles
                        item.Key.AddClass(styles);
                    }
                    else
                    {
                        RemoveAllMQStyles(item.Key);
                    }

                    if (stylesOnElementToRemove != null && stylesOnElementToRemove.Any())
                    {
                        //remove unsupported styles and notify other elements
                        RemoveClass(item.Key, stylesOnElementToRemove);
                    }
                }
            }
            else //remove all breakpoints
            {
                foreach (var item in ElementsWithBreakpointAndStyle)
                {
                    RemoveAllMQStyles(item.Key);
                }
            }
        }

        private void RemoveAllMQStyles(VisualElement element)
        {
            var compClasses = GetCompatibleClasses(element);
            if (compClasses == null) return;

            RemoveClass(element, compClasses.ToArray());
        }

        private string GetBreakPoint(string @class)
        {
            if (responsiveStyleSheet == null || responsiveStyleSheet.ParsedTheme == null || !responsiveStyleSheet.ParsedTheme.ContainsKey("breakpoint")) return string.Empty;

            return @class.Split(':', StringSplitOptions.RemoveEmptyEntries)[..^1].FirstOrDefault(x => responsiveStyleSheet.ParsedTheme["breakpoint"].ContainsKey(x));
        }

        private List<string> GetAllCompatibleClasses(VisualElement element)
        {
            //comp classes on element
            var compClasses = GetCompatibleClasses(element);
            if (compClasses == null)
            {
                if (!ElementsWithBreakpointAndStyle.ContainsKey(element)) return new List<string>();
                return ElementsWithBreakpointAndStyle[element].Values.SelectMany(x => x).ToList();
            }

            //comp classes on element + saved Classes
            compClasses.AddRange(ElementsWithBreakpointAndStyle[element].Values.SelectMany(x => x).ToList().FindAll(x => !compClasses.Contains(x)));
            return compClasses;
        }

        private void RemoveClass(VisualElement element, string[] classes)
        {
            ClassesNotToRemove.AddRange(classes);
            element.RemoveClass(classes);
        }
    }
}