using UnityEngine;

[System.Serializable]
public class SpeedController
{
    public float speed = 30f;
    public float timeSpin = 3f;
    [Range(0, 50)] public float endSpeed = 5;
    public float minSpeed = 5;
}
