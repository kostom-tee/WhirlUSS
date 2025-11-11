using System.Collections.Generic;
using UnityEngine;

namespace Kostom.Style
{
    public enum SupportedValueType
    {
        Color,
        Image,
        Position,
        CssVariable,
        Arbitrary,
    }

    public abstract class UtilityRule : ScriptableObject
    {
        public abstract bool CanParse(string className);
        public abstract List<(string property, UssValue value)>? GetUssPropertyAndValue(string className);
        public abstract IReadOnlyList<SupportedValueType> SupportedTypes { get; }
    }
}