using UnityEngine;


namespace Rescues
{
    public abstract class BaseTrapData : ScriptableObject
    {
        public bool IsActive;
        public float CraftingTime;
        public abstract void ActivateTrap(EnemyData activatorData);
    }
}