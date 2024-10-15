using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new data", menuName = "Slice/Data", order = 51)]


public class SliceData : ScriptableObject
{
    [SerializeField] private Element[] _element;

    public Element[] Element => _element;
}

[Serializable]
public class Element
{
    [SerializeField] private Vector2[] _position;
    [SerializeField] private Vector2[] _size;
    [SerializeField] private float[] _angle;

    public Vector2[] Position => _position;
    public Vector2[] Size => _size;
    public float[] Angle => _angle;
}