using System;
using System.Collections.Generic;
using System.Linq;

namespace Kostom.Style
{
    internal class Background : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => new[]
        {
            SupportedValueType.Color,
            SupportedValueType.Image,
            SupportedValueType.Position,
            SupportedValueType.CssVariable,
            SupportedValueType.Arbitrary
        };

        public override bool CanParse(string className)
        {
            return CanParseBG(className);
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {
            UssValue cssValue;
            SupportedValueType? detectedType;

            //attachment
            if (className == "bg-fixed")
            {
                return new List<(string property, UssValue value)> {
                ("background-attachment", new StaticValue("fixed"))
              };
            }
            else if (className == "bg-local")
            {
                return new List<(string property, UssValue value)> {
                ("background-attachment", new StaticValue("local"))
              };
            }
            else if (className == "bg-scroll")
            {
                return new List<(string property, UssValue value)> {
                ("background-attachment", new StaticValue("scroll"))
              };
            }

            //clip
            if (className == "bg-clip-border")
            {
                return new List<(string property, UssValue value)> {
                ("background-clip", new StaticValue("border-box"))
              };
            }
            else if (className == "bg-clip-padding")
            {
                return new List<(string property, UssValue value)> {
                ("background-clip", new StaticValue("padding-box"))
              };
            }
            else if (className == "bg-clip-content")
            {
                return new List<(string property, UssValue value)> {
                ("background-clip", new StaticValue("content-box"))
              };
            }
            else if (className == "bg-clip-text")
            {
                return new List<(string property, UssValue value)> {
                ("background-clip", new StaticValue("text"))
              };
            }

            //image
            if (className == "bg-none")
            {
                return new List<(string property, UssValue value)> {
                ("background-image", new StaticValue("none"))
              };
            }

            //origin
            if (className == "bg-origin-border")
            {
                return new List<(string property, UssValue value)> {
                ("background-origin", new StaticValue("border-box"))
              };
            }
            else if (className == "bg-origin-padding")
            {
                return new List<(string property, UssValue value)> {
                ("background-origin", new StaticValue("padding-box"))
              };
            }
            else if (className == "bg-origin-content")
            {
                return new List<(string property, UssValue value)> {
                ("background-origin", new StaticValue("content-box"))
              };
            }

            //position
            if (className == "bg-top-left")
            {
                return new List<(string property, UssValue value)> {
                ("background-position", new StaticValue("top left"))
              };
            }
            else if (className == "bg-top")
            {
                return new List<(string property, UssValue value)> {
                ("background-position", new StaticValue("top"))
              };
            }
            else if (className == "bg-top-right")
            {
                return new List<(string property, UssValue value)> {
                ("background-position", new StaticValue("top right"))
              };
            }
            else if (className == "bg-left")
            {
                return new List<(string property, UssValue value)> {
                ("background-position", new StaticValue("left"))
              };
            }
            else if (className == "bg-center")
            {
                return new List<(string property, UssValue value)> {
                ("background-position", new StaticValue("center"))
              };
            }
            else if (className == "bg-right")
            {
                return new List<(string property, UssValue value)> {
                ("background-position", new StaticValue("right"))
              };
            }
            else if (className == "bg-bottom-left")
            {
                return new List<(string property, UssValue value)> {
                ("background-position", new StaticValue("bottom left"))
              };
            }
            else if (className == "bg-bottom")
            {
                return new List<(string property, UssValue value)> {
                ("background-position", new StaticValue("bottom"))
              };
            }
            else if (className == "bg-bottom-right")
            {
                return new List<(string property, UssValue value)> {
                ("background-position", new StaticValue("bottom right"))
              };
            }
            else if (className.StartsWith("bg-position-"))
            {
                string suffix = className["bg-position-".Length..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("background-position", cssValue),
                };
                }
            }

            //repeat
            if (className == "bg-repeat")
            {
                return new List<(string property, UssValue value)> {
                ("background-repeat", new StaticValue("repeat"))
            };
            }
            else if (className == "bg-repeat-x")
            {
                return new List<(string property, UssValue value)> {
                ("background-repeat", new StaticValue("repeat-x"))
            };
            }
            else if (className == "bg-repeat-y")
            {
                return new List<(string property, UssValue value)> {
                ("background-repeat", new StaticValue("repeat-y"))
            };
            }
            else if (className == "bg-repeat-space")
            {
                return new List<(string property, UssValue value)> {
                ("background-repeat", new StaticValue("space"))
            };
            }
            else if (className == "bg-repeat-round")
            {
                return new List<(string property, UssValue value)> {
                ("background-repeat", new StaticValue("round"))
            };
            }
            else if (className == "bg-no-repeat")
            {
                return new List<(string property, UssValue value)> {
                ("background-repeat", new StaticValue("no-repeat"))
            };
            }

            //size
            if (className == "bg-auto")
            {
                return new List<(string property, UssValue value)> {
                ("background-size", new StaticValue("auto"))
              };
            }
            else if (className == "bg-cover")
            {
                return new List<(string property, UssValue value)> {
                ("background-size", new StaticValue("cover"))
              };
            }
            else if (className == "bg-contain")
            {
                return new List<(string property, UssValue value)> {
                ("background-size", new StaticValue("contain"))
              };
            }
            else if (className.StartsWith("bg-size-"))
            {
                string suffix = className["bg-size-".Length..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("background-size", cssValue),
                };
                }
            }

            //color
            if (className.StartsWith("bg-"))
            {
                string suffix = className[3..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }
                    if (detectedType.Value == SupportedValueType.CssVariable)
                    {
                        if (cssValue.Render().Contains(":") && cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[0].Trim().Replace("var(", "") == "image")
                        {
                            string newVal = cssValue.Render().Split(':', StringSplitOptions.RemoveEmptyEntries)[1];
                            newVal = newVal.EndsWith(')') ? newVal[..^1] : newVal;
                            return new List<(string property, UssValue value)> { ("background-image", new UssVariableValue(newVal)) };
                        }
                    }

                    return new List<(string property, UssValue value)> { ("background-color", cssValue) };
                }

                if (ProcessFile.CustomTheme.ContainsKey("color") && ProcessFile.CustomTheme["color"].ContainsKey(suffix))
                {
                    detectedType = SupportedValueType.Color;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> { ("background-color", ProcessFile.CustomTheme["color"][suffix]) };
                }
            }

            //slice

            if (className.StartsWith("bg-slice-y-"))
            {
                string suffix = className[3..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("-unity-slice-top", cssValue),
                        ("-unity-slice-bottom", cssValue)
                    };
                }
                else
                {
                    if (float.TryParse(suffix, out var val))
                    {
                        return new List<(string property, UssValue value)> {
                            ("-unity-slice-top", new StaticValue(val.ToString())),
                            ("-unity-slice-bottom", new StaticValue(val.ToString()))
                        };
                    }
                }
            }
            else if (className.StartsWith("bg-slice-x-"))
            {
                string suffix = className[3..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("-unity-slice-right", cssValue),
                        ("-unity-slice-left", cssValue)
                    };
                }
                else
                {
                    if (float.TryParse(suffix, out var val))
                    {
                        return new List<(string property, UssValue value)> {
                            ("-unity-slice-right", new StaticValue(val.ToString())),
                            ("-unity-slice-left", new StaticValue(val.ToString()))
                        };
                    }
                }
            }
            else if (className.StartsWith("bg-slice-top-"))
            {
                string suffix = className[3..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("-unity-slice-top", cssValue),
                    };
                }
                else
                {
                    if (float.TryParse(suffix, out var val))
                    {
                        return new List<(string property, UssValue value)> {
                            ("-unity-slice-top", new StaticValue(val.ToString())),
                        };
                    }
                }
            }
            else if (className.StartsWith("bg-slice-bottom-"))
            {
                string suffix = className[3..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("-unity-slice-bottom", cssValue),
                    };
                }
                else
                {
                    if (float.TryParse(suffix, out var val))
                    {
                        return new List<(string property, UssValue value)> {
                            ("-unity-slice-bottom", new StaticValue(val.ToString())),
                        };
                    }
                }
            }
            else if (className.StartsWith("bg-slice-right-"))
            {
                string suffix = className[3..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("-unity-slice-right", cssValue),
                    };
                }
                else
                {
                    if (float.TryParse(suffix, out var val))
                    {
                        return new List<(string property, UssValue value)> {
                            ("-unity-slice-right", new StaticValue(val.ToString())),
                        };
                    }
                }
            }
            else if (className.StartsWith("bg-slice-left-"))
            {
                string suffix = className[3..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("-unity-slice-left", cssValue),
                    };
                }
                else
                {
                    if (float.TryParse(suffix, out var val))
                    {
                        return new List<(string property, UssValue value)> {
                            ("-unity-slice-left", new StaticValue(val.ToString())),
                        };
                    }
                }
            }
            else if (className.StartsWith("bg-slice-scale-"))
            {
                string suffix = className[3..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("-unity-slice-scale", cssValue),
                    };
                }
                else
                {
                    if (float.TryParse(suffix, out var val))
                    {
                        return new List<(string property, UssValue value)> {
                            ("-unity-slice-scale", new StaticValue(val.ToString())),
                        };
                    }
                }
            }
            else if (className.StartsWith("bg-slice-"))
            {
                string suffix = className[3..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> { ("-unity-slice", cssValue) };
                }
                else
                {
                    if (float.TryParse(suffix, out var val))
                    {
                        return new List<(string property, UssValue value)> {
                            ("-unity-slice", new StaticValue(val.ToString()))
                        };
                    }
                }
            }

            if (className.StartsWith("tint-"))
            {
                string suffix = className[3..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }


                    return new List<(string property, UssValue value)> { ("-unity-background-image-tint-color", cssValue) };
                }

                if (ProcessFile.CustomTheme.ContainsKey("color") && ProcessFile.CustomTheme["color"].ContainsKey(suffix))
                {
                    detectedType = SupportedValueType.Color;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> { ("-unity-background-image-tint-color", ProcessFile.CustomTheme["color"][suffix]) };
                }
            }

            return null;
        }

        private bool CanParseBG(string className)
        {
            return className == "bg-none"

            || className == "bg-top-left"
            || className == "bg-top"
            || className == "bg-top-right"
            || className == "bg-left"
            || className == "bg-center"
            || className == "bg-right"
            || className == "bg-bottom-left"
            || className == "bg-bottom"
            || className == "bg-bottom-right"

            || className == "bg-repeat"
            || className == "bg-repeat-x"
            || className == "bg-repeat-y"
            || className == "bg-repeat-space"
            || className == "bg-repeat-round"
            || className == "bg-no-repeat"

            || className == "bg-auto"
            || className == "bg-cover"
            || className == "bg-contain"

            || className.StartsWith("tint-")
            || className.StartsWith("bg-")
            || className.StartsWith("bg-slice-y-")
            || className.StartsWith("bg-slice-x-")
            || className.StartsWith("bg-slice-top-")
            || className.StartsWith("bg-slice-right-")
            || className.StartsWith("bg-slice-bottom-")
            || className.StartsWith("bg-slice-left-")
            || className.StartsWith("bg-slice-scale-")
            || className.StartsWith("bg-slice-")
            || className.StartsWith("bg-position-")
            || className.StartsWith("bg-size-");
        }
    }
}