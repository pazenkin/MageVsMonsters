using Logic.Data.Settings;
using Zenject;

namespace Logic.Characters
{
    /// <summary>
    /// Персонаж с автовнедряемыми данными при создании экземпляра
    /// </summary>
    public class InjectableCharacter : Character
    {
        [Inject]
        private void Construct(MainCharacterData mainCharacterData)
        {
            Initialize(mainCharacterData);
        }
    }
}