using System.Collections.Generic;
using System.Linq;

namespace Kostom.Style
{
    internal class Transforms : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => new[]
        {
            SupportedValueType.Arbitrary,
            SupportedValueType.CssVariable
        };

        public override bool CanParse(string className)
        {
            return CanParseTransforms(className);
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {
            SupportedValueType? detectedType;

            if (className == "rotate-none")
            {
                return new List<(string property, UssValue value)> {
                ("rotate", new StaticValue("none"))
            };
            }
            else if (className.StartsWith("rotate-"))
            {
                string suffix = className["rotate-".Length..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("rotate", cssValue),
                };
                }
                else
                {
                    return new List<(string property, UssValue value)>
                {
                    ("rotate", new StaticValue($"{suffix}deg"))
                };
                }
            }
            else if (className.StartsWith("-rotate-"))
            {
                string suffix = className["-rotate-".Length..];

                if (!((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]"))))
                {
                    return new List<(string property, UssValue value)>
                {
                    ("rotate", new StaticValue($"-{suffix}deg"))
                };
                }
            }

            if (className == "scale-none")
            {
                return new List<(string property, UssValue value)> {
                ("scale", new StaticValue("none"))
            };
            }
            else if (className.StartsWith("scale-"))
            {
                string suffix = className["scale-".Length..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("scale", cssValue),
                };
                }
                else
                {
                    return new List<(string property, UssValue value)>
                {
                    ("scale", new StaticValue($"{suffix}%"))
                };
                }
            }
            else if (className.StartsWith("-scale-"))
            {
                string suffix = className["-scale-".Length..];

                if (!((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]"))))
                {
                    return new List<(string property, UssValue value)>
                {
                    ("scale", new StaticValue($"-{suffix}%"))
                };
                }
            }


            if (className == "origin-center")
            {
                return new List<(string property, UssValue value)> {
                ("transform-origin", new StaticValue("center"))
              };
            }
            else if (className == "origin-top")
            {
                return new List<(string property, UssValue value)> {
                ("transform-origin", new StaticValue("top"))
              };
            }
            else if (className == "origin-top-right")
            {
                return new List<(string property, UssValue value)> {
                ("transform-origin", new StaticValue("top right"))
              };
            }
            else if (className == "origin-right")
            {
                return new List<(string property, UssValue value)> {
                ("transform-origin", new StaticValue("right"))
              };
            }
            else if (className == "origin-bottom-right")
            {
                return new List<(string property, UssValue value)> {
                ("transform-origin", new StaticValue("bottom right"))
              };
            }
            else if (className == "origin-bottom")
            {
                return new List<(string property, UssValue value)> {
                ("transform-origin", new StaticValue("bottom"))
              };
            }
            else if (className == "origin-bottom-left")
            {
                return new List<(string property, UssValue value)> {
                ("transform-origin", new StaticValue("bottom left"))
              };
            }
            else if (className == "origin-left")
            {
                return new List<(string property, UssValue value)> {
                ("transform-origin", new StaticValue("left"))
              };
            }
            else if (className == "origin-top-left")
            {
                return new List<(string property, UssValue value)> {
                ("transform-origin", new StaticValue("top left"))
              };
            }
            else if (className.StartsWith("origin-"))
            {
                string suffix = className["origin-".Length..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("transform-origin", cssValue),
                };
                }
            }


            if (className == "translate-full")
            {
                return new List<(string property, UssValue value)> {
                ("translate", new StaticValue("100% 100%"))
              };
            }
            else if (className == "-translate-full")
            {
                return new List<(string property, UssValue value)> {
                ("translate", new StaticValue("-100% -100%"))
              };
            }
            else if (className == "translate-px")
            {
                return new List<(string property, UssValue value)> {
                ("translate", new StaticValue("1px 1px"))
              };
            }
            else if (className == "-translate-px")
            {
                return new List<(string property, UssValue value)> {
                ("translate", new StaticValue("-1px -1px"))
              };
            }
            else if (className.StartsWith("translate-"))
            {
                string suffix = className["translate-".Length..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("translate", cssValue),
                };
                }
                else
                {
                    return new List<(string property, UssValue value)>
                {
                    ("translate", new StaticValue($"{suffix}px"))
                };
                }
            }
            else if (className.StartsWith("-translate-"))
            {
                string suffix = className["-translate-".Length..];

                if (!((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]"))))
                {
                    return new List<(string property, UssValue value)>
                {
                    ("translate", new StaticValue($"-{suffix}px"))
                };
                }
            }
            return null;
        }

        private bool CanParseTransforms(string className)
        {
            return className == "rotate-none"
                || className == "scale-none"
                || className == "origin-center"
                || className == "origin-top"
                || className == "origin-top-right"
                || className == "origin-right"
                || className == "origin-bottom-right"
                || className == "origin-bottom"
                || className == "origin-bottom-left"
                || className == "origin-left"
                || className == "origin-top-left"
                || className == "translate-none"
                || className == "translate-full"
                || className == "-translate-full"
                || className == "translate-px"
                || className == "-translate-px"


                || className.StartsWith("origin-")
                || className.StartsWith("translate-")
                || className.StartsWith("-translate-")
                || className.StartsWith("scale-")
                || className.StartsWith("-scale-")
                || className.StartsWith("-rotate-")
                || className.StartsWith("rotate-");
        }
    }
}