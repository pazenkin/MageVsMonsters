using UnityEngine;

namespace Logic.Signals
{
    /// <summary>
    /// Событие изменения позиции игрока
    /// </summary>
    public class ChangePlayerPositionEvent
    {
        public Vector3 NewPosition;
    }
}