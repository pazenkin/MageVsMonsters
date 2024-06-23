using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Дополнительные методы для изменения Transform игровых объектов
    /// </summary>
    public static class TransformExtension
    {
        public static void ResetLocal(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}