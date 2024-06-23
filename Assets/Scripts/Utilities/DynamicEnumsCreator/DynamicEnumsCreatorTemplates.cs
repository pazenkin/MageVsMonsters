using UnityEngine;

namespace Utilities.DynamicEnumsCreator
{
    /// <summary>
    /// Шаблоны файлов для кодогенерации динамических перечислений
    /// </summary>
    // [CreateAssetMenu(fileName = "DynamicEnumsCreatorTemplates", menuName = "Settings/DynamicEnumsCreatorTemplates", order = 0)]
    public class DynamicEnumsCreatorTemplates : StaticScriptableObject<DynamicEnumsCreatorTemplates>
    {
#if UNITY_EDITOR
        public TextAsset templateFile;
        public TextAsset overriddenFile;
        public TextAsset scriptableObjectFile;
#endif
    }
}