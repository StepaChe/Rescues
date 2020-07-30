using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "HidingPlaceData", menuName = "Data/HidingPlaceData")]
    public sealed class HidingPlaceData: ScriptableObject
    {
        #region Fields

        public AudioClip HidingSound;
        public float AnimationDuration;

        #endregion
    }
}
