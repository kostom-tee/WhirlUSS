using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kostom.Style
{
    internal class Typograghy : UtilityRule
    {
        public override IReadOnlyList<SupportedValueType> SupportedTypes => new[]
        {
            SupportedValueType.Arbitrary,
            SupportedValueType.CssVariable,
            SupportedValueType.Color
        };

        public override bool CanParse(string className)
        {
            if (Helper.UnityVersion >= 6000.2)
            {
                return CanParseMain(className);
            }

            return CanParseTypo(className);
        }

        public bool CanParseMain(string className)
        {
            return className == "auto-size-none"
                || className == "whitespace-pre"
                || className == "whitespace-pre-wrap"
                || className.StartsWith("auto-size-")
                || CanParseTypo(className);
        }

        private bool CanParseTypo(string className)
        {
            return className == "text-xs"
            || className == "text-sm"
            || className == "text-base"
            || className == "text-lg"
            || className == "text-xl"
            || className == "text-2xl"
            || className == "text-3xl"
            || className == "text-4xl"
            || className == "text-5xl"
            || className == "text-6xl"
            || className == "text-7xl"
            || className == "text-8xl"
            || className == "text-9xl"


            || className == "italic"
            || className == "not-italic"
            || className == "bold"
            || className == "bold-italic"

            || className == "tracking-tighter"
            || className == "tracking-tight"
            || className == "tracking-normal"
            || className == "tracking-wide"
            || className == "tracking-wider"
            || className == "tracking-widest"

            || className == "truncate"
            || className == "text-ellipsis"
            || className == "text-clip"

            || className == "text-upper-left"
            || className == "text-middle-left"
            || className == "text-lower-left"
            || className == "text-upper-center"
            || className == "text-middle-center"
            || className == "text-lower-center"
            || className == "text-upper-right"
            || className == "text-middle-right"
            || className == "text-lower-right"

            || className == "text-outline-px"

            || className == "overflow-position-start"
            || className == "overflow-position-middle"
            || className == "overflow-position-end"

            || className == "whitespace-normal"
            || className == "whitespace-nowrap"

            || className.StartsWith("word-spacing-px")
            || className.StartsWith("paragraph-spacing-px")

            || className.StartsWith("tracking-")
            || className.StartsWith("font-")
            || className.StartsWith("text-outline-")
            || className.StartsWith("word-spacing-")
            || className.StartsWith("paragraph-spacing-")
            || className.StartsWith("text-");
        }

        public override List<(string property, UssValue value)>? GetUssPropertyAndValue(string className)
        {
            SupportedValueType? detectedType;

            if (className == "auto-size-none")
            {
                return new List<(string property, UssValue value)> {
                    ("-unity-text-auto-size", new StaticValue("none")),
                };
            }
            if (className.StartsWith("auto-size-"))
            {
                string suffix = className["auto-size-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("-unity-text-auto-size", cssValue),
                    };
                }
            }

            //font-size/align/color
            if (className == "text-xs")
            {
                return new List<(string property, UssValue value)> {
                ("font-size", new UssVariableValue("--text-xs"))
              };
            }
            else if (className == "text-sm")
            {
                return new List<(string property, UssValue value)> {
                ("font-size", new UssVariableValue("--text-sm"))
              };
            }
            else if (className == "text-base")
            {
                return new List<(string property, UssValue value)> {
                ("font-size", new UssVariableValue("--text-base"))
              };
            }
            else if (className == "text-lg")
            {
                return new List<(string property, UssValue value)> {
                ("font-size", new UssVariableValue("--text-lg"))
              };
            }
            else if (className == "text-xl")
            {
                return new List<(string property, UssValue value)> {
                ("font-size", new UssVariableValue("--text-xl"))
              };
            }
            else if (className == "text-2xl")
            {
                return new List<(string property, UssValue value)> {
                ("font-size", new UssVariableValue("--text-2xl"))
              };
            }
            else if (className == "text-3xl")
            {
                return new List<(string property, UssValue value)> {
                ("font-size", new  UssVariableValue("--text-3xl"))
              };
            }
            else if (className == "text-4xl")
            {
                return new List<(string property, UssValue value)> {
                ("font-size", new  UssVariableValue("--text-4xl"))
              };
            }
            else if (className == "text-5xl")
            {
                return new List<(string property, UssValue value)> {
                ("font-size", new  UssVariableValue("--text-5xl"))
              };
            }
            else if (className == "text-6xl")
            {
                return new List<(string property, UssValue value)> {
                ("font-size", new  UssVariableValue("--text-6xl"))
              };
            }
            else if (className == "text-7xl")
            {
                return new List<(string property, UssValue value)> {
                ("font-size", new StaticValue("72px"))
              };
            }
            else if (className == "text-8xl")
            {
                return new List<(string property, UssValue value)> {
                ("font-size", new  UssVariableValue("--text-8xl"))
              };
            }
            else if (className == "text-9xl")
            {
                return new List<(string property, UssValue value)> {
                ("font-size", new UssVariableValue("--text-9xl"))
              };
            }
            else if (className == "text-upper-left")
            {
                return new List<(string property, UssValue value)> {
                ("-unity-text-align", new StaticValue(className["text-".Length..]))
              };
            }
            else if (className == "text-middle-left")
            {
                return new List<(string property, UssValue value)> {
                ("-unity-text-align", new StaticValue(className["text-".Length..]))
              };
            }
            else if (className == "text-lower-left")
            {
                return new List<(string property, UssValue value)> {
                ("-unity-text-align", new StaticValue(className["text-".Length..]))
              };
            }
            else if (className == "text-upper-center")
            {
                return new List<(string property, UssValue value)> {
                ("-unity-text-align", new StaticValue(className["text-".Length..]))
              };
            }
            else if (className == "text-middle-center")
            {
                return new List<(string property, UssValue value)> {
                ("-unity-text-align", new StaticValue(className["text-".Length..]))
              };
            }
            else if (className == "text-lower-center")
            {
                return new List<(string property, UssValue value)> {
                ("-unity-text-align", new StaticValue(className["text-".Length..]))
              };
            }
            else if (className == "text-upper-right")
            {
                return new List<(string property, UssValue value)> {
                ("-unity-text-align", new StaticValue(className["text-".Length..]))
              };
            }
            else if (className == "text-middle-right")
            {
                return new List<(string property, UssValue value)> {
                ("-unity-text-align", new StaticValue(className["text-".Length..]))
              };
            }
            else if (className == "text-lower-right")
            {
                return new List<(string property, UssValue value)> {
                ("-unity-text-align", new StaticValue(className["text-".Length..]))
              };
            }


            else if (className == "text-ellipsis")
            {
                return new List<(string property, UssValue value)> {
                ("text-overflow", new StaticValue("ellipsis"))
              };
            }
            else if (className == "text-clip")
            {
                return new List<(string property, UssValue value)> {
                ("text-overflow", new StaticValue("clip"))
              };
            }
            else if (className == "text-outline-px")
            {
                return new List<(string property, UssValue value)> {
                    ("-unity-text-outline", new StaticValue("1px"))
                  };
            }
            else if (className.StartsWith("text-outline-"))
            {
                string suffix = className["text-outline-".Length..];

                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("-unity-text-outline", cssValue),
                    };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("color") && ProcessFile.CustomTheme["color"].ContainsKey(suffix))
                {
                    detectedType = SupportedValueType.Color;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> { ("-unity-text-outline", ProcessFile.CustomTheme["color"][suffix]) };
                }
                else if (float.TryParse(suffix, out var val))
                {
                    return new List<(string property, UssValue value)> { ("-unity-text-outline", new StaticValue($"{val}px")) };
                }
            }
            else if (className.StartsWith("text-"))
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

                    if (detectedType == SupportedValueType.Arbitrary && cssValue.Render().ToLower().EndsWith("px"))
                    {
                        return new List<(string property, UssValue value)>
                        {
                            ("font-size", cssValue),
                        };
                    }
                    else if (detectedType == SupportedValueType.CssVariable)
                    {
                        var val = cssValue.Render()[4..^1];
                        if (val.Contains(':') && val[0] != ':' && val.Split(':', StringSplitOptions.RemoveEmptyEntries)[0].ToLower() == "length")
                        {
                            return new List<(string property, UssValue value)>
                            {
                                ("font-size", new UssVariableValue(val.Split(':', StringSplitOptions.RemoveEmptyEntries)[1])),
                            };
                        }
                        else if (val.Contains(':') && val[0] != ':' && val.Split(':', StringSplitOptions.RemoveEmptyEntries)[0].ToLower() == "color")
                        {
                            return new List<(string property, UssValue value)>
                            {
                                ("color", new UssVariableValue(val.Split(':', StringSplitOptions.RemoveEmptyEntries)[1])),
                            };
                        }
                    }

                    return new List<(string property, UssValue value)> {
                        ("color", cssValue),
                    };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("color") && ProcessFile.CustomTheme["color"].ContainsKey(suffix))
                {
                    detectedType = SupportedValueType.Color;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }
                    return new List<(string property, UssValue value)> { ("color", ProcessFile.CustomTheme["color"][suffix]) };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("text") && ProcessFile.CustomTheme["text"].ContainsKey(suffix))
                {
                    return new List<(string property, UssValue value)> { ("font-size", ProcessFile.CustomTheme["text"][suffix]) };
                }
            }

            //font-style
            if (className == "italic")
            {
                return new List<(string property, UssValue value)> {
                ("-unity-font-style", new StaticValue("italic"))
              };
            }
            else if (className == "not-italic")
            {
                return new List<(string property, UssValue value)> {
                ("-unity-font-style", new StaticValue("normal"))
              };
            }
            else if (className == "bold")
            {
                return new List<(string property, UssValue value)> {
                ("-unity-font-style", new StaticValue("bold"))
              };
            }
            else if (className == "bold-italic")
            {
                return new List<(string property, UssValue value)> {
                ("-unity-font-style", new StaticValue("bold-and-italic"))
              };
            }

            //font-weight
            if (className == "font-thin")
            {
                return new List<(string property, UssValue value)> {
                ("font-weight", new StaticValue("100"))
              };
            }
            else if (className == "font-extralight")
            {
                return new List<(string property, UssValue value)> {
                ("font-weight", new StaticValue("200"))
              };
            }
            else if (className == "font-light")
            {
                return new List<(string property, UssValue value)> {
                ("font-weight", new StaticValue("300"))
              };
            }
            else if (className == "font-normal")
            {
                return new List<(string property, UssValue value)> {
                ("font-weight", new StaticValue("400"))
              };
            }
            else if (className == "font-medium")
            {
                return new List<(string property, UssValue value)> {
                ("font-weight", new StaticValue("500"))
              };
            }
            else if (className == "font-semibold")
            {
                return new List<(string property, UssValue value)> {
                ("font-weight", new StaticValue("600"))
              };
            }
            else if (className == "font-bold")
            {
                return new List<(string property, UssValue value)> {
                ("font-weight", new StaticValue("700"))
              };
            }
            else if (className == "font-extrabold")
            {
                return new List<(string property, UssValue value)> {
                ("font-weight", new StaticValue("800"))
              };
            }
            else if (className == "font-black")
            {
                return new List<(string property, UssValue value)> {
                ("font-weight", new StaticValue("900"))
              };
            }
            else if (className.StartsWith("font-"))
            {
                string suffix = className["font-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("-unity-font-definition", cssValue),
                    };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("font") && ProcessFile.CustomTheme["font"].ContainsKey(suffix))
                {
                    return new List<(string property, UssValue value)> {
                        ("-unity-font-definition", ProcessFile.CustomTheme["font"][suffix])
                    };
                }

            }

            //letter spacing
            if (className == "tracking-tighter")
            {
                return new List<(string property, UssValue value)> {
                ("letter-spacing", new UssVariableValue("--tracking-tighter"))
              };
            }
            else if (className == "tracking-tight")
            {
                return new List<(string property, UssValue value)> {
                ("letter-spacing", new UssVariableValue("--tracking-tight"))
              };
            }
            else if (className == "tracking-normal")
            {
                return new List<(string property, UssValue value)> {
                ("letter-spacing", new UssVariableValue("--tracking-normal"))
              };
            }
            else if (className == "tracking-wide")
            {
                return new List<(string property, UssValue value)> {
                ("letter-spacing", new UssVariableValue("--tracking-wide"))
              };
            }
            else if (className == "tracking-wider")
            {
                return new List<(string property, UssValue value)> {
                ("letter-spacing", new UssVariableValue("--tracking-wider"))
              };
            }
            else if (className == "tracking-widest")
            {
                return new List<(string property, UssValue value)> {
                ("letter-spacing", new UssVariableValue("--tracking-widest"))
              };
            }
            else if (className.StartsWith("tracking-"))
            {
                string suffix = className["tracking-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("letter-spacing", cssValue),
                };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("tracking") && ProcessFile.CustomTheme["tracking"].ContainsKey(suffix))
                {
                    detectedType = SupportedValueType.Color;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("letter-spacing", ProcessFile.CustomTheme["tracking"][suffix])
                    };
                }
            }

            //text-dec-line
            if (className == "underline")
            {
                return new List<(string property, UssValue value)> {
                ("text-decoration-line", new StaticValue("underline"))
            };
            }
            else if (className == "overline")
            {
                return new List<(string property, UssValue value)> {
                ("text-decoration-line", new StaticValue("overline"))
            };
            }
            else if (className == "line-through")
            {
                return new List<(string property, UssValue value)> {
                ("text-decoration-line", new StaticValue("line-through"))
            };
            }
            else if (className == "no-underline")
            {
                return new List<(string property, UssValue value)> {
                ("text-decoration-line", new StaticValue("none"))
            };
            }


            if (className.StartsWith("word-spacing-px"))
            {
                return new List<(string property, UssValue value)>
                {
                    ("word-spacing", new StaticValue($"1px"))
                };
            }
            else if (className.StartsWith("word-spacing-"))
            {
                string suffix = className["word-spacing-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("word-spacing", cssValue),
                    };
                }
                else
                {
                    if (float.TryParse(suffix, out var val))
                    {
                        return new List<(string property, UssValue value)>
                        {
                            ("word-spacing", new StaticValue($"{val}px"))
                        };
                    }
                }
            }

            if (className.StartsWith("paragraph-spacing-px"))
            {
                return new List<(string property, UssValue value)>
                {
                    ("paragraph-spacing", new StaticValue($"1px"))
                };
            }
            else if (className.StartsWith("paragraph-spacing-"))
            {
                string suffix = className["paragraph-spacing-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("paragraph-spacing", cssValue),
                    };
                }
                else
                {
                    if (float.TryParse(suffix, out var val))
                    {
                        return new List<(string property, UssValue value)>
                        {
                            ("paragraph-spacing", new StaticValue($"{val}px"))
                        };
                    }
                }
            }


            //text-dec-style/text-dec-color
            if (className == "decoration-solid")
            {
                return new List<(string property, UssValue value)> {
                ("text-decoration-style", new StaticValue("solid"))
            };
            }
            else if (className == "decoration-double")
            {
                return new List<(string property, UssValue value)> {
                ("text-decoration-style", new StaticValue("double"))
            };
            }
            else if (className == "decoration-dotted")
            {
                return new List<(string property, UssValue value)> {
                ("text-decoration-style", new StaticValue("dotted"))
            };
            }
            else if (className == "decoration-dashed")
            {
                return new List<(string property, UssValue value)> {
                ("text-decoration-style", new StaticValue("dashed"))
            };
            }
            else if (className == "decoration-wavy")
            {
                return new List<(string property, UssValue value)> {
                ("text-decoration-style", new StaticValue("wavy"))
            };
            }
            else if (className == "decoration-from-font")
            {
                return new List<(string property, UssValue value)> {
                ("text-decoration-thickness", new StaticValue("from-font"))
            };
            }
            else if (className == "decoration-auto")
            {
                return new List<(string property, UssValue value)> {
                ("text-decoration-thickness", new StaticValue("auto"))
            };
            }
            else if (className.StartsWith("decoration-"))
            {
                string suffix = className["decoration-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("text-decoration-color", cssValue),
                    };
                }
                else if (ProcessFile.CustomTheme.ContainsKey("color") && ProcessFile.CustomTheme["color"].ContainsKey(suffix))
                {
                    detectedType = SupportedValueType.Color;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> { ("background-color", ProcessFile.CustomTheme["color"][suffix]) };
                }
                else
                {
                    return new List<(string property, UssValue value)>
                {
                        ("text-decoration-thickness", new StaticValue($"{suffix}px"))
                    };
                }
            }


            //text-underline-offset
            if (className == "underline-offset-auto")
            {
                return new List<(string property, UssValue value)> {
                ("text-underline-offset", new StaticValue("auto"))
            };
            }
            else if (className.StartsWith("underline-offset-"))
            {
                string suffix = className["underline-offset-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("text-underline-offset", cssValue),
                };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>underline-offset-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("text-underline-offset", new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }
            else if (className.StartsWith("-underline-offset-"))
            {
                string suffix = className["-underline-offset-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    return null;
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>underline-offset-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> {
                        ("text-underline-offset", new StaticValue($"-{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }

            //overflow position
            if (className == "overflow-position-start")
            {
                return new List<(string property, UssValue value)>
                {
                    ("-unity-text-overflow-position", new StaticValue("start"))
                };
            }
            else if (className == "overflow-position-middle")
            {
                return new List<(string property, UssValue value)>
                {
                    ("-unity-text-overflow-position", new StaticValue("middle"))
                };
            }
            else if (className == "overflow-position-end")
            {
                return new List<(string property, UssValue value)>
                {
                    ("-unity-text-overflow-position", new StaticValue("end"))
                };
            }

            //text-transform
            if (className == "uppercase")
            {
                return new List<(string property, UssValue value)> {
                ("text-transform", new StaticValue("uppercase"))
            };
            }
            else if (className == "lowercase")
            {
                return new List<(string property, UssValue value)> {
                ("text-transform", new StaticValue("lowercase"))
            };
            }
            else if (className == "capitalize")
            {
                return new List<(string property, UssValue value)> {
                ("text-transform", new StaticValue("capitalize"))
            };
            }
            else if (className == "normal-case")
            {
                return new List<(string property, UssValue value)> {
                ("text-transform", new StaticValue("none"))
            };
            }

            //text overflow
            if (className == "truncate")
            {
                return new List<(string property, UssValue value)> {
                ("overflow", new StaticValue("hidden")),
                ("text-overflow", new StaticValue("ellipsis")),
                ("white-space", new StaticValue("nowrap"))
            };
            }

            //text indent
            if (className == "indent-px")
            {
                return new List<(string property, UssValue value)> {
                ("text-indent", new StaticValue("1px"))
              };
            }
            else if (className == "-indent-px")
            {
                return new List<(string property, UssValue value)> {
                ("text-indent", new StaticValue("-1px"))
              };
            }
            else if (className.StartsWith("indent-"))
            {
                string suffix = className["indent-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("text-indent", cssValue),
                };
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>indent-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                        ("text-indent", new StaticValue($"{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }
            else if (className.StartsWith("-indent-"))
            {
                string suffix = className["-indent-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    return null;
                }
                else
                {
                    if (!float.TryParse(suffix, out var val))
                    {
                        Debug.LogWarning(($"utility error: <color=yellow><b>indent-*</b></color>", $"expected number, received {suffix}"));
                        return null;
                    }
                    return new List<(string property, UssValue value)> {
                        ("text-indent", new StaticValue($"-{val * ProcessFile.DefaultSpacing}px")),
                    };
                }
            }

            //text align
            if (className == "align-baseline")
            {
                return new List<(string property, UssValue value)> {
                ("vertical-align", new StaticValue("baseline"))
              };
            }
            else if (className == "align-top")
            {
                return new List<(string property, UssValue value)> {
                ("vertical-align", new StaticValue("top"))
              };
            }
            else if (className == "align-middle")
            {
                return new List<(string property, UssValue value)> {
                ("vertical-align", new StaticValue("middle"))
              };
            }
            else if (className == "align-bottom")
            {
                return new List<(string property, UssValue value)> {
                ("vertical-align", new StaticValue("bottom"))
              };
            }
            else if (className == "align-text-top")
            {
                return new List<(string property, UssValue value)> {
                ("vertical-align", new StaticValue("text-top"))
              };
            }
            else if (className == "align-text-bottom")
            {
                return new List<(string property, UssValue value)> {
                ("vertical-align", new StaticValue("text-bottom"))
              };
            }
            else if (className == "align-sub")
            {
                return new List<(string property, UssValue value)> {
                ("vertical-align", new StaticValue("sub"))
              };
            }
            else if (className == "align-super")
            {
                return new List<(string property, UssValue value)> {
                ("vertical-align", new StaticValue("super"))
              };
            }
            else if (className.StartsWith("align-"))
            {
                string suffix = className["align-".Length..];
                if ((suffix.StartsWith("(") && suffix.EndsWith(")")) || (suffix.StartsWith("[") && suffix.EndsWith("]")))
                {
                    UssValue cssValue = UssValueParser.Parse(suffix);
                    detectedType = suffix.StartsWith("(") ? SupportedValueType.CssVariable : SupportedValueType.Arbitrary;
                    if (!SupportedTypes.Contains(detectedType.Value))
                    {
                        return null;
                    }

                    return new List<(string property, UssValue value)> {
                    ("align-", cssValue),
                };
                }
            }


            //white space
            if (className == "whitespace-normal")
            {
                return new List<(string property, UssValue value)> {
                ("white-space", new StaticValue("normal"))
            };
            }
            else if (className == "whitespace-nowrap")
            {
                return new List<(string property, UssValue value)> {
                ("white-space", new StaticValue("nowrap"))
            };
            }
            else if (className == "whitespace-pre")
            {
                return new List<(string property, UssValue value)> {
                ("white-space", new StaticValue("pre"))
            };
            }
            else if (className == "whitespace-pre-line")
            {
                return new List<(string property, UssValue value)> {
                ("white-space", new StaticValue("pre-line"))
            };
            }
            else if (className == "whitespace-pre-wrap")
            {
                return new List<(string property, UssValue value)> {
                ("white-space", new StaticValue("pre-wrap"))
            };
            }
            else if (className == "whitespace-break-spaces")
            {
                return new List<(string property, UssValue value)> {
                ("white-space", new StaticValue("break-spaces"))
            };
            }

            //word break
            if (className == "break-normal")
            {
                return new List<(string property, UssValue value)> {
                ("word-break", new StaticValue("normal"))
            };
            }
            else if (className == "break-all")
            {
                return new List<(string property, UssValue value)> {
                ("word-break", new StaticValue("break-all"))
            };
            }
            else if (className == "break-keep")
            {
                return new List<(string property, UssValue value)> {
                ("word-break", new StaticValue("keep-all"))
            };
            }

            //overflow-wrap
            if (className == "wrap-break-word")
            {
                return new List<(string property, UssValue value)> {
                ("overflow-wrap", new StaticValue("break-word"))
            };
            }
            else if (className == "wrap-anywhere")
            {
                return new List<(string property, UssValue value)> {
                ("overflow-wrap", new StaticValue("anywhere"))
            };
            }
            else if (className == "wrap-normal")
            {
                return new List<(string property, UssValue value)> {
                ("overflow-wrap", new StaticValue("normal"))
            };
            }

            return null;
        }
    }
}