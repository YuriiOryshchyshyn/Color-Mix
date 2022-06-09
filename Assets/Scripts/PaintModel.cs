using System;
using UnityEngine;

[Serializable]
public class PaintModel
{
    [SerializeField] private Material _material;

    public Material Material => _material;
}