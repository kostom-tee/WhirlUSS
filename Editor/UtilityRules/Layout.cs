using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kostom.Style
{
    internal class Layout : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => new[]
        {
            SupportedValueType.CssVariable,
            SupportedValueType.Arbitrary,
        };

        public override bool CanParse(string className)
        {
            if (Helper.UnityVersion.ToString().StartsWith("2022")
                || Helper.UnityVersion.ToString().StartsWith("2023")
                || Helper.UnityVersion == 6000.0
                || Helper.UnityVersion == 6000.1
                || Helper.UnityVersion == 6000.2)
            {
                return CanParse2022(className);
            }

            return CanParseLayout(className);
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {
            var aspect = "aspect-ratio";
            var columns = "columns";
            var break_after = "break-after";
            var break_before = "break-before";
            var break_inside = "break-inside";

            SupportedValueType? detectedType;

            //aspect-ratio
            if (className == "aspect-square")
            {
                return new List<(string property, UssValue value)> { (aspect, new UssVariableValue("--aspect-square")) };
            }
            else if (className == "aspect-video")
            {
                return new List<(string property, UssValue value)> { (aspect, new UssVariableValue("--aspect-video")) };
            }
            else if (className == "aspect-auto")
            {
                return new List<(string property, UssValue value)> { (aspect, new UssVariableValue("--aspect-auto")) };
            }
            else if (className.StartsWith("aspect-"))
            {
                string suffix = className[7..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { (aspect, cssValue) };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("aspect") && ProcessFile.CustomTheme["aspect"].ContainsKey(suffix))
                {
                    return new List<(string property, UssValue value)> { ("aspect-ratio", ProcessFile.CustomTheme["aspect"][suffix]) };
                }
                else
                {
                    return new List<(string property, UssValue value)> { (aspect, new StaticValue(suffix)) };
                }
            }

            //columns
            if (className == "columns-3xs")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("256px")) };
            }
            else if (className == "columns-2xs")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("288px")) };
            }
            else if (className == "columns-xs")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("320px")) };
            }
            else if (className == "columns-sm")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("384px")) };
            }
            else if (className == "columns-md")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("448px")) };
            }
            else if (className == "columns-lg")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("512px")) };
            }
            else if (className == "columns-xl")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("576px")) };
            }
            else if (className == "columns-2xl")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("672px")) };
            }
            else if (className == "columns-3xl")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("768px")) };
            }
            else if (className == "columns-4xl")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("896px")) };
            }
            else if (className == "columns-5xl")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("1024px")) };
            }
            else if (className == "columns-6xl")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("1152px")) };
            }
            else if (className == "columns-7xl")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("1280px")) };
            }
            else if (className == "columns-auto")
            {
                return new List<(string property, UssValue value)> { (columns, new StaticValue("auto")) };
            }
            else if (className.StartsWith("columns-"))
            {
                string suffix = className[8..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { (columns, cssValue) };
                }
                else
                {
                    return new List<(string property, UssValue value)> { (columns, new StaticValue(suffix)) };
                }
            }

            //break-after
            if (className == "break-after-auto")
            {
                return new List<(string property, UssValue value)> { (break_after, new StaticValue("auto")) };
            }
            else if (className == "break-after-avoid")
            {
                return new List<(string property, UssValue value)> { (break_after, new StaticValue("avoid")) };
            }
            else if (className == "break-after-all")
            {
                return new List<(string property, UssValue value)> { (break_after, new StaticValue("all")) };
            }
            else if (className == "break-after-avoid-page")
            {
                return new List<(string property, UssValue value)> { (break_after, new StaticValue("avoid-page")) };
            }
            else if (className == "break-after-page")
            {
                return new List<(string property, UssValue value)> { (break_after, new StaticValue("page")) };
            }
            else if (className == "break-after-left")
            {
                return new List<(string property, UssValue value)> { (break_after, new StaticValue("left")) };
            }
            else if (className == "break-after-right")
            {
                return new List<(string property, UssValue value)> { (break_after, new StaticValue("right")) };
            }
            else if (className == "break-after-column")
            {
                return new List<(string property, UssValue value)> { (break_after, new StaticValue("column")) };
            }


            //break-before
            if (className == "break-before-auto")
            {
                return new List<(string property, UssValue value)> { (break_before, new StaticValue("auto")) };
            }
            else if (className == "break-before-avoid")
            {
                return new List<(string property, UssValue value)> { (break_before, new StaticValue("avoid")) };
            }
            else if (className == "break-before-all")
            {
                return new List<(string property, UssValue value)> { (break_before, new StaticValue("all")) };
            }
            else if (className == "break-before-avoid-page")
            {
                return new List<(string property, UssValue value)> { (break_before, new StaticValue("avoid-page")) };
            }
            else if (className == "break-before-page")
            {
                return new List<(string property, UssValue value)> { (break_before, new StaticValue("page")) };
            }
            else if (className == "break-before-left")
            {
                return new List<(string property, UssValue value)> { (break_before, new StaticValue("left")) };
            }
            else if (className == "break-before-right")
            {
                return new List<(string property, UssValue value)> { (break_before, new StaticValue("right")) };
            }
            else if (className == "break-before-column")
            {
                return new List<(string property, UssValue value)> { (break_before, new StaticValue("column")) };
            }


            //break-inside
            if (className == "break-inside-auto")
            {
                return new List<(string property, UssValue value)> { (break_inside, new StaticValue("auto")) };
            }
            else if (className == "break-inside-avoid")
            {
                return new List<(string property, UssValue value)> { (break_inside, new StaticValue("avoid")) };
            }
            else if (className == "break-inside-avoid-page")
            {
                return new List<(string property, UssValue value)> { (break_inside, new StaticValue("avoid-page")) };
            }
            else if (className == "break-inside-avoid-column")
            {
                return new List<(string property, UssValue value)> { (break_inside, new StaticValue("avoid-column")) };
            }


            //box-decoration-break
            if (className == "box-decoration-clone")
            {
                return new List<(string property, UssValue value)> { ("box-decoration-break", new StaticValue("clone")) };
            }
            else if (className == "box-decoration-slice")
            {
                return new List<(string property, UssValue value)> { ("box-decoration-break", new StaticValue("slice")) };
            }


            //boxer-sizing
            if (className == "box-border")
            {
                return new List<(string property, UssValue value)> { ("box-sizing", new StaticValue("border-box")) };
            }
            else if (className == "box-content")
            {
                return new List<(string property, UssValue value)> { ("box-sizing", new StaticValue("content-box")) };
            }


            //display
            if (className == "flex")
            {
                return new List<(string property, UssValue value)> { ("display", new StaticValue("flex")) };
            }
            else if (className == "hidden")
            {
                return new List<(string property, UssValue value)> { ("display", new StaticValue("none")) };
            }


            //object-fit
            if (className == "object-contain")
            {
                return new List<(string property, UssValue value)> { ("-unity-background-scale-mode", new StaticValue("scale-to-fit")) };
            }
            else if (className == "object-cover")
            {
                return new List<(string property, UssValue value)> { ("-unity-background-scale-mode", new StaticValue("scale-and-crop")) };
            }
            else if (className == "object-fill")
            {
                return new List<(string property, UssValue value)> { ("-unity-background-scale-mode", new StaticValue("stretch-to-fill")) };
            }
            else if (className == "object-none")
            {
                return new List<(string property, UssValue value)> { ("-unity-background-scale-mode", new StaticValue("none")) };
            }
            else if (className == "object-scale-down")
            {
                return new List<(string property, UssValue value)> { ("-unity-background-scale-mode", new StaticValue("scale-down")) };
            }


            //object-position
            if (className == "object-top-left")
            {
                return new List<(string property, UssValue value)> { ("object-position", new StaticValue("top left")) };
            }
            else if (className == "object-top")
            {
                return new List<(string property, UssValue value)> { ("object-position", new StaticValue("top")) };
            }
            else if (className == "object-top-right")
            {
                return new List<(string property, UssValue value)> { ("object-position", new StaticValue("top right")) };
            }
            else if (className == "object-left")
            {
                return new List<(string property, UssValue value)> { ("object-position", new StaticValue("left")) };
            }
            else if (className == "object-center")
            {
                return new List<(string property, UssValue value)> { ("object-position", new StaticValue("center")) };
            }
            else if (className == "object-right")
            {
                return new List<(string property, UssValue value)> { ("object-position", new StaticValue("right")) };
            }
            else if (className == "object-bottom-left")
            {
                return new List<(string property, UssValue value)> { ("object-position", new StaticValue("bottom left")) };
            }
            else if (className == "object-bottom")
            {
                return new List<(string property, UssValue value)> { ("object-position", new StaticValue("bottom")) };
            }
            else if (className == "object-bottom-right")
            {
                return new List<(string property, UssValue value)> { ("object-position", new StaticValue("bottom right")) };
            }
            else if (className.StartsWith("object-"))
            {
                string suffix = className[7..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { ("object-position", cssValue) };
                }
            }


            //overflow
            if (className == "overflow-auto")
            {
                return new List<(string property, UssValue value)> { ("overflow", new StaticValue("auto")) };
            }
            else if (className == "overflow-hidden")
            {
                return new List<(string property, UssValue value)> { ("overflow", new StaticValue("hidden")) };
            }
            else if (className == "overflow-scroll")
            {
                return new List<(string property, UssValue value)> { ("overflow", new StaticValue("scroll")) };
            }
            else if (className == "overflow-visible")
            {
                return new List<(string property, UssValue value)> { ("overflow", new StaticValue("visible")) };
            }


            //position
            if (className == "absolute")
            {
                return new List<(string property, UssValue value)> { ("position", new StaticValue("absolute")) };
            }
            else if (className == "relative")
            {
                return new List<(string property, UssValue value)> { ("position", new StaticValue("relative")) };
            }

            //visibility
            if (className == "visible")
            {
                return new List<(string property, UssValue value)> { ("visibility", new StaticValue("visible")) };
            }
            else if (className == "invisible")
            {
                return new List<(string property, UssValue value)> { ("visibility", new StaticValue("hidden")) };
            }
            else if (className == "collapse")
            {
                return new List<(string property, UssValue value)> { ("visibility", new StaticValue("collapse")) };
            }


            //z-index
            if (className == "z-auto")
            {
                return new List<(string property, UssValue value)> { ("z-index", new StaticValue("auto")) };
            }
            else if (className.StartsWith("z-"))
            {
                string suffix = className[2..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { ("z-index", cssValue) };
                }
                else
                {
                    return new List<(string property, UssValue value)> { ("z-index", new StaticValue(suffix)) };
                }
            }


            //top
            if (className == "top-px")
            {
                return new List<(string property, UssValue value)> { ("top", new StaticValue("1px")) };
            }
            else if (className == "-top-px")
            {
                return new List<(string property, UssValue value)> { ("top", new StaticValue("-1px")) };
            }
            else if (className == "top-full")
            {
                return new List<(string property, UssValue value)> { ("top", new StaticValue("100%")) };
            }
            else if (className == "-top-full")
            {
                return new List<(string property, UssValue value)> { ("top", new StaticValue("-100%")) };
            }
            else if (className == "-top-auto")
            {
                return new List<(string property, UssValue value)> { ("top", new StaticValue("auto")) };
            }
            else if (className.StartsWith("top-"))
            {
                string suffix = className[4..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { ("top", cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>top-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> { ("top", new StaticValue($"{val * ProcessFile.DefaultSpacing}")) };
                }
            }
            else if (className.StartsWith("-top-"))
            {
                string suffix = className[5..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { ("top", cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>-top-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> { ("top", new StaticValue($"{-val * ProcessFile.DefaultSpacing}")) };
                }
            }


            //right
            if (className == "right-px")
            {
                return new List<(string property, UssValue value)> { ("right", new StaticValue("1px")) };
            }
            else if (className == "-right-px")
            {
                return new List<(string property, UssValue value)> { ("right", new StaticValue("-1px")) };
            }
            else if (className == "right-full")
            {
                return new List<(string property, UssValue value)> { ("right", new StaticValue("100%")) };
            }
            else if (className == "-right-full")
            {
                return new List<(string property, UssValue value)> { ("right", new StaticValue("-100%")) };
            }
            else if (className == "-right-auto")
            {
                return new List<(string property, UssValue value)> { ("right", new StaticValue("auto")) };
            }
            else if (className.StartsWith("right-"))
            {
                string suffix = className[6..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { ("right", cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>right-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> { ("right", new StaticValue($"{val * ProcessFile.DefaultSpacing}")) };
                }
            }
            else if (className.StartsWith("-right-"))
            {
                string suffix = className[7..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { ("right", cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>right-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> { ("right", new StaticValue($"{-val * ProcessFile.DefaultSpacing}")) };
                }
            }


            //bottom
            if (className == "bottom-px")
            {
                return new List<(string property, UssValue value)> { ("bottom", new StaticValue("1px")) };
            }
            else if (className == "-bottom-px")
            {
                return new List<(string property, UssValue value)> { ("bottom", new StaticValue("-1px")) };
            }
            else if (className == "bottom-full")
            {
                return new List<(string property, UssValue value)> { ("bottom", new StaticValue("100%")) };
            }
            else if (className == "-bottom-full")
            {
                return new List<(string property, UssValue value)> { ("bottom", new StaticValue("-100%")) };
            }
            else if (className == "-bottom-auto")
            {
                return new List<(string property, UssValue value)> { ("bottom", new StaticValue("auto")) };
            }
            else if (className.StartsWith("bottom-"))
            {
                string suffix = className[7..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { ("bottom", cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>bottom-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> { ("bottom", new StaticValue((val * ProcessFile.DefaultSpacing).ToString())) };
                }
            }
            else if (className.StartsWith("-bottom-"))
            {
                string suffix = className[8..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { ("bottom", cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>bottom-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> { ("bottom", new StaticValue((-val * ProcessFile.DefaultSpacing).ToString())) };
                }
            }


            //left
            if (className == "left-px")
            {
                return new List<(string property, UssValue value)> { ("left", new StaticValue("1px")) };
            }
            else if (className == "-left-px")
            {
                return new List<(string property, UssValue value)> { ("left", new StaticValue("-1px")) };
            }
            else if (className == "left-full")
            {
                return new List<(string property, UssValue value)> { ("left", new StaticValue("100%")) };
            }
            else if (className == "-left-full")
            {
                return new List<(string property, UssValue value)> { ("left", new StaticValue("-100%")) };
            }
            else if (className == "-left-auto")
            {
                return new List<(string property, UssValue value)> { ("left", new StaticValue("auto")) };
            }
            else if (className.StartsWith("left-"))
            {
                string suffix = className[5..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { ("left", cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>left-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> { ("left", new StaticValue((val * ProcessFile.DefaultSpacing).ToString())) };
                }
            }
            else if (className.StartsWith("-left-"))
            {
                string suffix = className[6..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { ("left", cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>left-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> { ("left", new StaticValue((-val * ProcessFile.DefaultSpacing).ToString())) };
                }
            }

            return null;
        }

        private bool CanParseLayout(string className)
        {
            return className.StartsWith("aspect-square")
                || className.StartsWith("aspect-video")
                || className.StartsWith("aspect-auto")
                || className.StartsWith("aspect-")

                || CanParse2022(className);
        }

        private bool CanParse2022(string className)
        {
            return className.StartsWith("flex")
                || className.StartsWith("hidden")

                || className.StartsWith("overflow-hidden")
                || className.StartsWith("overflow-visible")

                || className.StartsWith("absolute")
                || className.StartsWith("relative")

                || className.StartsWith("object-contain")
                || className.StartsWith("object-cover")
                || className.StartsWith("object-fill")

                || className.StartsWith("top-px")
                || className.StartsWith("-top-px")
                || className.StartsWith("top-full")
                || className.StartsWith("-top-full")
                || className.StartsWith("top-auto")
                || className.StartsWith("-top-")
                || className.StartsWith("top-")


                || className.StartsWith("right-px")
                || className.StartsWith("-right-px")
                || className.StartsWith("right-full")
                || className.StartsWith("-right-full")
                || className.StartsWith("right-auto")
                || className.StartsWith("-right-")
                || className.StartsWith("right-")


                || className.StartsWith("bottom-px")
                || className.StartsWith("-bottom-px")
                || className.StartsWith("bottom-full")
                || className.StartsWith("-bottom-full")
                || className.StartsWith("bottom-auto")
                || className.StartsWith("-bottom-")
                || className.StartsWith("bottom-")


                || className.StartsWith("left-px")
                || className.StartsWith("-left-px")
                || className.StartsWith("left-full")
                || className.StartsWith("-left-full")
                || className.StartsWith("left-auto")
                || className.StartsWith("-left-")
                || className.StartsWith("left-")

                || className.StartsWith("visible")
                || className.StartsWith("invisible");
        }
    }
}