using UnityEngine;

namespace Kostom.Style
{
    [CreateAssetMenu(fileName = "Theme", menuName = "Kostom/Style/Theme")]
    public sealed class Theme : ScriptableObject
    {
        [SerializeField] bool isDefault = false;
        public bool isActive = true;
        public StyleProperty[] styleProperties;

        public bool IsDefault => isDefault;

        public void CreateDefault(StyleProperty[] styleProperties)
        {
            isDefault = true;
            this.styleProperties = styleProperties;
        }
    }
}