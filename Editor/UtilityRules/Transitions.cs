using System.Collections.Generic;
using System.Linq;

namespace Kostom.Style
{
    internal class Transitions : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => new[]
        {
            SupportedValueType.Arbitrary,
            SupportedValueType.CssVariable
        };

        public override bool CanParse(string className)
        {
            return CanParseTransition(className);
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {
            SupportedValueType? detectedType;

            if (className == "transition")
            {
                return new List<(string property, UssValue value)> {
                ("transition-property", new StaticValue("color, background-color, margin, width, height, border-width, border-color, opacity, transform, translate, scale, rotate, filter, display")),
                ("transition-timing-function", new StaticValue("ease-in-out")),
                ("transition-duration", new StaticValue("150ms"))
              };
            }
            else if (className == "transition-all")
            {
                return new List<(string property, UssValue value)> {
                ("transition-property", new StaticValue("all")),
                ("transition-timing-function", new StaticValue("ease-in-out")),
                ("transition-duration", new StaticValue("150ms"))
              };
            }
            else if (className == "transition-colors")
            {
                return new List<(string property, UssValue value)> {
                ("transition-property", new StaticValue("color, background-color, border-color")),
                ("transition-timing-function", new StaticValue("ease-in-out")),
                ("transition-duration", new StaticValue("150ms"))
              };
            }
            else if (className == "transition-opacity")
            {
                return new List<(string property, UssValue value)> {
                ("transition-property", new StaticValue("opacity")),
                ("transition-timing-function", new StaticValue("ease-in-out")),
                ("transition-duration", new StaticValue("150ms"))
              };
            }
            else if (className == "transition-transform")
            {
                return new List<(string property, UssValue value)> {
                ("transition-property", new StaticValue("transform, translate, scale, rotate")),
                ("transition-timing-function", new StaticValue("ease-in-out")),
                ("transition-duration", new StaticValue("150ms"))
              };
            }
            else if (className == "transition-none")
            {
                return new List<(string property, UssValue value)> {
                ("transition-property", new StaticValue("none"))
              };
            }
            else if (className.StartsWith("transition-"))
            {
                string suffix = className["transition-".Length..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("transition-property", cssValue),
                    ("transition-timing-function", new StaticValue("ease-in-out")),
                    ("transition-duration", new StaticValue("150ms"))
                };
                }
            }


            if (className == "transition-normal")
            {
                return new List<(string property, UssValue value)> {
                ("transition-behavior", new StaticValue("normal"))
              };
            }
            else if (className == "transition-discrete")
            {
                return new List<(string property, UssValue value)> {
                ("transition-behavior", new StaticValue("allow-discrete"))
              };
            }

            if (className == "duration-initial")
            {
                return new List<(string property, UssValue value)> {
                ("transition-duration", new StaticValue("initial"))
              };
            }
            else if (className.StartsWith("duration-"))
            {
                string suffix = className["duration-".Length..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return new List<(string property, UssValue value)> { (null, null) };
                    }

                    return new List<(string property, UssValue value)> {
                    ("transition-duration", cssValue),
                };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                   ("transition-duration", new StaticValue($"{suffix}ms"))
                };
                }
            }


            if (className == "ease-linear")
            {
                return new List<(string property, UssValue value)> {
                ("transition-timing-function", new StaticValue("linear"))
            };
            }
            else if (className == "ease-in")
            {
                return new List<(string property, UssValue value)> {
                ("transition-timing-function", new StaticValue("ease-in"))
              };
            }
            else if (className == "ease-out")
            {
                return new List<(string property, UssValue value)> {
                ("transition-timing-function", new StaticValue("ease-out"))
              };
            }
            else if (className == "ease-in-out")
            {
                return new List<(string property, UssValue value)> {
                ("transition-timing-function", new StaticValue("ease-in-out"))
              };
            }
            else if (className == "ease-initial")
            {
                return new List<(string property, UssValue value)> {
                ("transition-timing-function", new StaticValue("initial"))
              };
            }
            else if (className.StartsWith("ease-"))
            {
                string suffix = className["ease-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("transition-timing-function", cssValue),
                    };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("ease") && ProcessFile.CustomTheme["ease"].ContainsKey(suffix))
                {
                    return new List<(string property, UssValue value)> {
                        ("transition-timing-function",ProcessFile.CustomTheme["ease"][suffix]),
                    };
                }
            }

            if (className.StartsWith("delay-"))
            {
                string suffix = className["delay-".Length..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("transition-delay", cssValue),
                };
                }
                else
                {
                    return new List<(string property, UssValue value)>
                {
                    ("transition-delay", new StaticValue($"{suffix}ms"))
                };
                }
            }
            return null;
        }

        private bool CanParseTransition(string className)
        {
            return className == "transition"
            || className == "transition-all"
            || className == "transition-colors"
            || className == "transition-opacity"
            || className == "transition-shadow"
            || className == "transition-transform"
            || className == "transition-none"
            || className == "transition-normal"
            || className == "transition-discrete"
            || className == "duration-initial"
            || className == "ease-linear"
            || className == "ease-in"
            || className == "ease-out"
            || className == "ease-in-out"
            || className == "ease-initial"


            || className.StartsWith("delay-")
            || className.StartsWith("ease-")
            || className.StartsWith("duration-")
            || className.StartsWith("transition-");
        }
    }
}