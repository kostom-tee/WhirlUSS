using UnityEngine;

namespace Kostom.Style
{
    [CreateAssetMenu(fileName = "Component", menuName = "Kostom/Style/Component")]
    public sealed class StyleComponent : ScriptableObject
    {
        public Component[] components;
    }

    [System.Serializable]
    public class Component
    {
        public string selector;
        public string[] utilities;
    }
}