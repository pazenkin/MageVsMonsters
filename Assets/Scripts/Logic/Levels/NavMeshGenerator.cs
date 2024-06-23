using Cysharp.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;

namespace Logic.Levels
{
    /// <summary>
    /// Генератор NavMesh для уровня
    /// </summary>
    public class NavMeshGenerator : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface _navMeshSurface;

        public async UniTask Initialize()
        {
            await _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
        }
    }
}