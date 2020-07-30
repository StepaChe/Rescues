using UnityEngine;

namespace Rescues
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemies/EnemyData")]
    public sealed class EnemyData : ScriptableObject
    {
        public float Speed;
        public float VisionDistance = 5.0f;
        public StateEnemy StateEnemy;
    }
}