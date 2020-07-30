using UnityEngine;


[CreateAssetMenu(fileName = "CameraData", menuName = "Data/Camera/CameraData")]
public class CameraData : ScriptableObject
{
    public float CameraFreeMoveLimit;
    public int CameraDragSpeed;
    public float Distance;
}