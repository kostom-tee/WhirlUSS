using Kostom.Style;
using System.Collections.Generic;
using UnityEngine;

namespace Kostom.Style
{
	public abstract class CustomUtility: ScriptableObject
	{
        public abstract bool CanParse(string className);
        public abstract List<StyleProperty>[]? GetUssPropertyAndValue(string className);
        public virtual string[]? AdditionalUtilityName(string className) { return null; }
    }
}

