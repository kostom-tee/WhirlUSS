using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kostom.Style
{
    internal class Spacing : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => new[]
        {
            SupportedValueType.CssVariable,
            SupportedValueType.Arbitrary
        };

        public override bool CanParse(string className)
        {
            return CanParseSpacing(className);
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {
            var padding = "padding";
            var paddingTop = "padding-top";
            var paddingBottom = "padding-bottom";
            var paddingRight = "padding-right";
            var paddingLeft = "padding-left";


            var margin = "margin";
            var marginTop = "margin-top";
            var marginBottom = "margin-bottom";
            var marginRight = "margin-right";
            var marginLeft = "margin-left";
            SupportedValueType? detectedType;

            //margin
            if (className == "m-px")
            {
                return new List<(string property, UssValue value)> { (margin, new StaticValue("1px")) };
            }
            else if (className == "-m-px")
            {
                return new List<(string property, UssValue value)> { (margin, new StaticValue("-1px")) };
            }
            else if (className == "m-auto")
            {
                return new List<(string property, UssValue value)> { (margin, new StaticValue("auto")) };
            }
            else if (className.StartsWith("m-"))
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


                    return new List<(string property, UssValue value)> { (margin, cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>m-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> { (margin, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")) };
                }
            }
            else if (className.StartsWith("-m-"))
            {
                string suffix = className[3..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    return null;
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>m-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> { (margin, new StaticValue($"{-val * ProcessFile.DefaultSpacing}px")) };
                }
            }

            if (className == "mx-px")
            {
                return new List<(string property, UssValue value)> {
                (marginLeft, new StaticValue("1px")),
                (marginRight, new StaticValue("1px")),
            };
            }
            else if (className == "-mx-px")
            {
                return new List<(string property, UssValue value)> {
                (marginLeft, new StaticValue("-1px")),
                (marginRight, new StaticValue("-1px")),
            };
            }
            else if (className == "mx-auto")
            {
                return new List<(string property, UssValue value)> {
                (marginLeft, new StaticValue("auto")),
                (marginRight, new StaticValue("auto")),
            };
            }
            else if (className.StartsWith("mx-"))
            {
                string suffix = className[3..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        (marginLeft, cssValue),
                        (marginRight, cssValue),
                    };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>mx-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        (marginLeft, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                        (marginRight, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }
            else if (className.StartsWith("-mx-"))
            {
                string suffix = className[4..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    return null;
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>mx-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        (marginLeft, new StaticValue($"-{val * ProcessFile.DefaultSpacing}px")),
                        (marginRight, new StaticValue($"-{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }

            if (className == "my-px")
            {
                return new List<(string property, UssValue value)> {
                (marginTop, new StaticValue("1px")),
                (marginBottom, new StaticValue("1px"))
            };
            }
            else if (className == "-my-px")
            {
                return new List<(string property, UssValue value)> {
                (marginTop, new StaticValue("-1px")),
                (marginBottom, new StaticValue("-1px"))
            };
            }
            else if (className == "my-auto")
            {
                return new List<(string property, UssValue value)> {
                (marginTop, new StaticValue("auto")),
                (marginBottom, new StaticValue("auto"))
            };
            }
            else if (className.StartsWith("my-"))
            {
                string suffix = className[3..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    (marginTop, cssValue),
                    (marginBottom, cssValue)
                };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>my-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        (marginTop, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                        (marginBottom, new StaticValue($"{val * ProcessFile.DefaultSpacing}px"))
                    };
                }
            }
            else if (className.StartsWith("-my-"))
            {
                string suffix = className[4..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    return null;
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>my-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        (marginTop, new StaticValue($"-{val * ProcessFile.DefaultSpacing}px")),
                        (marginBottom, new StaticValue($"-{val * ProcessFile.DefaultSpacing}px"))
                    };
                }
            }

            if (className == "mt-px")
            {
                return new List<(string property, UssValue value)> { (marginTop, new StaticValue("1px")) };
            }
            else if (className == "-mt-px")
            {
                return new List<(string property, UssValue value)> { (marginTop, new StaticValue("-1px")) };
            }
            else if (className == "mt-auto")
            {
                return new List<(string property, UssValue value)> { (marginTop, new StaticValue("auto")) };
            }
            else if (className.StartsWith("mt-"))
            {
                string suffix = className[3..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { (marginTop, cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>mt-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> { (marginTop, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")) };
                }
            }
            else if (className.StartsWith("-mt-"))
            {
                string suffix = className[4..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    return null;
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>mt-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> { (marginTop, new StaticValue($"-{val * ProcessFile.DefaultSpacing}px")) };
                }
            }

            if (className == "mr-px")
            {
                return new List<(string property, UssValue value)> { (marginRight, new StaticValue("1px")) };
            }
            else if (className == "-mr-px")
            {
                return new List<(string property, UssValue value)> { (marginRight, new StaticValue("-1px")) };
            }
            if (className == "mr-auto")
            {
                return new List<(string property, UssValue value)> { (marginRight, new StaticValue("auto")) };
            }
            else if (className.StartsWith("mr-"))
            {
                string suffix = className[3..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { (marginRight, cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>mr-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> { (marginRight, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")) };
                }
            }
            else if (className.StartsWith("-mr-"))
            {
                string suffix = className[4..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    return null;
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>mr-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> { (marginRight, new StaticValue($"-{val * ProcessFile.DefaultSpacing}px")) };
                }
            }

            if (className == "mb-px")
            {
                return new List<(string property, UssValue value)> { (marginBottom, new StaticValue("1px")) };
            }
            else if (className == "-mb-px")
            {
                return new List<(string property, UssValue value)> { (marginBottom, new StaticValue("-1px")) };
            }
            else if (className == "mb-auto")
            {
                return new List<(string property, UssValue value)> { (marginBottom, new StaticValue("auto")) };
            }
            else if (className.StartsWith("mb-"))
            {
                string suffix = className[3..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { (marginBottom, cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>mb-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> { (marginBottom, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")) };
                }
            }
            else if (className.StartsWith("-mb-"))
            {
                string suffix = className[4..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    return null;
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>mb-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> { (marginBottom, new StaticValue($"-{val * ProcessFile.DefaultSpacing}px")) };
                }
            }

            if (className == "ml-px")
            {
                return new List<(string property, UssValue value)> { (marginLeft, new StaticValue("1px")) };
            }
            else if (className == "-ml-px")
            {
                return new List<(string property, UssValue value)> { (marginLeft, new StaticValue("-1px")) };
            }
            else if (className == "ml-auto")
            {
                return new List<(string property, UssValue value)> { (marginLeft, new StaticValue("auto")) };
            }
            else if (className.StartsWith("ml-"))
            {
                string suffix = className[3..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { (marginLeft, cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>ml-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> { (marginLeft, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")) };
                }
            }
            else if (className.StartsWith("-ml-"))
            {
                string suffix = className[4..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    return null;
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>ml-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> { (marginLeft, new StaticValue($"-{val * ProcessFile.DefaultSpacing}px")) };
                }
            }



            //padding
            if (className == "p-px")
            {
                return new List<(string property, UssValue value)> { (padding, new StaticValue("1px")) };
            }
            else if (className.StartsWith("p-"))
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


                    return new List<(string property, UssValue value)> { (padding, cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>p-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> { (padding, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")) };
                }
            }

            if (className == "px-px")
            {
                return new List<(string property, UssValue value)> {
                (paddingLeft, new StaticValue("1px")),
                (paddingRight, new StaticValue("1px")),
            };
            }
            else if (className.StartsWith("px-"))
            {
                string suffix = className[3..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    (paddingLeft, cssValue),
                    (paddingRight, cssValue),
                };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>px-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        (paddingLeft, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                        (paddingRight, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }

            if (className == "py-px")
            {
                return new List<(string property, UssValue value)> {
                (paddingTop, new StaticValue("1px")),
                (paddingBottom, new StaticValue("1px"))
            };
            }
            else if (className.StartsWith("py-"))
            {
                string suffix = className[3..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    (paddingTop, cssValue),
                    (paddingBottom, cssValue)
                };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>py-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        (paddingTop, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                        (paddingBottom, new StaticValue($"{val * ProcessFile.DefaultSpacing}px"))
                    };
                }
            }

            if (className == "pt-px")
            {
                return new List<(string property, UssValue value)> { (paddingTop, new StaticValue("1px")) };
            }
            else if (className.StartsWith("pt-"))
            {
                string suffix = className[3..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { (paddingTop, cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>pt-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> { (paddingTop, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")) };
                }
            }

            if (className == "pr-px")
            {
                return new List<(string property, UssValue value)> { (paddingRight, new StaticValue("1px")) };
            }
            else if (className.StartsWith("pr-"))
            {
                string suffix = className[3..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { (paddingRight, cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>pr-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> { (paddingRight, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")) };
                }
            }

            if (className == "pb-px")
            {
                return new List<(string property, UssValue value)> { (paddingBottom, new StaticValue("1px")) };
            }
            else if (className.StartsWith("pb-"))
            {
                string suffix = className[3..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { (paddingBottom, cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>pb-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> { (paddingBottom, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")) };
                }
            }

            if (className == "pl-px")
            {
                return new List<(string property, UssValue value)> { (paddingLeft, new StaticValue("1px")) };
            }
            else if (className.StartsWith("pl-"))
            {
                string suffix = className[3..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { (paddingLeft, cssValue) };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>pl-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> { (paddingLeft, new StaticValue($"{val * ProcessFile.DefaultSpacing}px")) };
                }
            }

            return null;
        }

        private bool CanParseSpacing(string className)
        {
            return className == "p-px"
                || className.StartsWith("p-")
                || className == "px-px"
                || className.StartsWith("px-")
                || className == "py-px"
                || className.StartsWith("py-")
                || className == "pt-px"
                || className.StartsWith("pt-")
                || className == "pr-px"
                || className.StartsWith("pr-")
                || className == "pb-px"
                || className.StartsWith("pb-")
                || className == "pl-px"
                || className.StartsWith("pl-")


                || className == "m-px"
                || className == "-m-px"
                || className.StartsWith("m-")
                || className.StartsWith("-m-")
                || className == "mx-px"
                || className == "-mx-px"
                || className.StartsWith("mx-")
                || className.StartsWith("-mx-")
                || className == "my-px"
                || className == "-my-px"
                || className.StartsWith("my-")
                || className.StartsWith("-my-")
                || className == "mt-px"
                || className == "-mt-px"
                || className.StartsWith("mt-")
                || className.StartsWith("-mt-")
                || className == "mr-px"
                || className == "-mr-px"
                || className.StartsWith("mr-")
                || className.StartsWith("-mr-")
                || className == "mb-px"
                || className == "-mb-px"
                || className.StartsWith("mb-")
                || className.StartsWith("-mb-")
                || className == "ml-px"
                || className == "-ml-px"
                || className.StartsWith("ml-")
                || className.StartsWith("-ml-");
        }
    }
}