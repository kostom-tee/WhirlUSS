using UnityEngine;

namespace Kostom.Style
{
    [CreateAssetMenu(fileName = "Utility", menuName = "Kostom/Style/Utility")]
    public sealed class StyleUtilities : ScriptableObject
    {
        public Utility[] utilities;
    }

    [System.Serializable]
    public class Utility
    {
        public string selector;
        public StyleProperty[] styleProperties;
    }

    [System.Serializable]
    public class StyleProperty
    {
        public string property, value;

        public (string property, string value) GetValues => (property, value);
    }
}