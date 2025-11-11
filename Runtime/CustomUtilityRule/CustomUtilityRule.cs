using System.Collections.Generic;
using UnityEngine;

namespace Kostom.Style
{
    public abstract class CustomUtilityRule : ScriptableObject
	{
		public abstract bool CanParse(string className);
		public abstract List<StyleProperty>? GetUssPropertyAndValue(string className);
    }
}