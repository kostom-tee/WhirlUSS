using System.Collections.Generic;
using System.Linq;

namespace Kostom.Style
{
    internal class Filters : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => new[]
        {
            SupportedValueType.Arbitrary,
            SupportedValueType.CssVariable
        };

        public override bool CanParse(string className)
        {
            if (Helper.UnityVersion >= 6000.2)
            {
                return CanParseFilters(className);
            }
            return false;
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {
            SupportedValueType? detectedType;

            if (className == "filter-none")
            {
                return new List<(string property, UssValue value)> {
                ("filter", new StaticValue("none"))
            };
            }
            else if (className.StartsWith("filter-"))
            {
                string suffix = className["filter-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("filter", cssValue),
                };
                }
            }

            if (className == "blur-xs")
            {
                return new List<(string property, UssValue value)> {
                ("filter", new StaticValue("blur(var(--blur-xs))"))
              };
            }
            else if (className == "blur-sm")
            {
                return new List<(string property, UssValue value)> {
                ("filter", new StaticValue("blur(var(--blur-sm))"))
              };
            }
            else if (className == "blur-md")
            {
                return new List<(string property, UssValue value)> {
                ("filter", new StaticValue("blur(var(--blur-md))"))
              };
            }
            else if (className == "blur-lg")
            {
                return new List<(string property, UssValue value)> {
                ("filter", new StaticValue("blur(var(--blur-lg))"))
              };
            }
            else if (className == "blur-xl")
            {
                return new List<(string property, UssValue value)> {
                ("filter", new StaticValue("blur(var(--blur-xl))"))
              };
            }
            else if (className == "blur-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("filter", new StaticValue("blur(var(--blur-2xl))"))
              };
            }
            else if (className == "blur-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("filter", new StaticValue("blur(var(--blur-3xl))"))
              };
            }
            else if (className == "blur-none")
            {
                return new List<(string property, UssValue value)> {
                ("filter", new StaticValue(""))
              };
            }
            else if (className.StartsWith("blur-"))
            {
                string suffix = className["blur-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType == SupportedValueType.Arbitrary)
                    {
                        return new List<(string property, UssValue value)> {
                        ("filter", new StaticValue($"blur({cssValue.Render()})"))
                    };
                    }

                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"blur({cssValue.Render()})")),
                };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("blur") && ProcessFile.CustomTheme["blur"].ContainsKey(suffix))
                {

                    return new List<(string property, UssValue value)> {
                        ("filter",new StaticValue($"blur({ProcessFile.CustomTheme["blur"][suffix].Render()})")),
                    };
                }
                else
                {
                    if (float.TryParse(suffix.Trim(), out var val))
                    {
                        return new List<(string property, UssValue value)> {
                            ("filter", new StaticValue($"blur({val}px)")),
                        };
                    }
                }
            }

            if (className.StartsWith("brightness-"))
            {
                string suffix = className["brightness-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType == SupportedValueType.Arbitrary)
                    {
                        return new List<(string property, UssValue value)> {
                        ("filter", new StaticValue($"brightness({cssValue.Render()})"))
                    };
                    }

                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"brightness({cssValue.Render()})")),
                };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"brightness({suffix}%)")),
                };
                }
            }

            if (className.StartsWith("contrast-"))
            {
                string suffix = className["contrast-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType == SupportedValueType.Arbitrary)
                    {
                        return new List<(string property, UssValue value)> {
                        ("filter", new StaticValue($"contrast({cssValue.Render()})"))
                    };
                    }

                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"contrast({cssValue.Render()})")),
                };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"contrast({suffix}%)")),
                };
                }
            }

            if (className.StartsWith("grayscale-"))
            {
                string suffix = className["grayscale-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType == SupportedValueType.Arbitrary)
                    {
                        return new List<(string property, UssValue value)> {
                        ("filter", new StaticValue($"grayscale({cssValue.Render()})"))
                    };
                    }

                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"grayscale({cssValue.Render()})")),
                };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"grayscale({suffix}%)")),
                };
                }
            }
            else if (className == "grayscale")
            {
                return new List<(string property, UssValue value)> {
                ("filter", new StaticValue("grayscale(100%)"))
              };
            }


            if (className.StartsWith("hue-rotate-"))
            {
                string suffix = className["hue-rotate-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType == SupportedValueType.Arbitrary)
                    {
                        return new List<(string property, UssValue value)> {
                        ("filter", new StaticValue($"hue-rotate({cssValue.Render()})"))
                    };
                    }

                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"hue-rotate({cssValue.Render()})")),
                };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"hue-rotate({suffix}deg)")),
                };
                }
            }
            else if (className.StartsWith("-hue-rotate-"))
            {
                string suffix = className["-hue-rotate-".Length..];
                if (!((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]"))))
                {
                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"hue-rotate(-{suffix}deg)")),
                };
                }
            }

            if (className.StartsWith("invert-"))
            {
                string suffix = className["invert-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType == SupportedValueType.Arbitrary)
                    {
                        return new List<(string property, UssValue value)> {
                        ("filter", new StaticValue($"invert({cssValue.Render()})"))
                    };
                    }

                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"invert({cssValue.Render()})")),
                };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"invert({suffix}%)")),
                };
                }
            }
            else if (className == "invert")
            {
                return new List<(string property, UssValue value)> {
                ("filter", new StaticValue("invert(100%)"))
              };
            }


            if (className.StartsWith("saturate-"))
            {
                string suffix = className["saturate-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType == SupportedValueType.Arbitrary)
                    {
                        return new List<(string property, UssValue value)> {
                        ("filter", new StaticValue($"saturate({cssValue.Render()})"))
                    };
                    }

                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"saturate({cssValue.Render()})")),
                };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"saturate({suffix}%)")),
                };
                }
            }

            if (className.StartsWith("sepia-"))
            {
                string suffix = className["sepia-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    if (detectedType == SupportedValueType.Arbitrary)
                    {
                        return new List<(string property, UssValue value)> {
                        ("filter", new StaticValue($"sepia({cssValue.Render()})"))
                    };
                    }

                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"sepia({cssValue.Render()})")),
                };
                }
                else
                {
                    return new List<(string property, UssValue value)> {
                    ("filter", new StaticValue($"sepia({suffix}%)")),
                };
                }
            }
            else if (className == "sepia")
            {
                return new List<(string property, UssValue value)> {
                ("filter", new StaticValue("sepia(100%)"))
              };
            }

            return null;
        }

        private bool CanParseFilters(string className)
        {
            return className == "filter-none"

                || className == "blur-xs"
                || className == "blur-sm"
                || className == "blur-md"
                || className == "blur-lg"
                || className == "blur-xl"
                || className == "blur-2xl"
                || className == "blur-3xl"
                || className == "blur-none"
                || className == "grayscale"
                || className == "invert"
                || className == "sepia"


                || className.StartsWith("sepia-")
                || className.StartsWith("saturate-")
                || className.StartsWith("invert-")
                || className.StartsWith("hue-rotate-")
                || className.StartsWith("-hue-rotate-")
                || className.StartsWith("grayscale-")
                || className.StartsWith("contrast-")
                || className.StartsWith("brightness-")
                || className.StartsWith("blur-")
                || className.StartsWith("filter-");
        }
    }
}