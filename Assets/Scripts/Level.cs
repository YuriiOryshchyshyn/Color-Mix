using System;
using UnityEngine;

[Serializable]
public class Level
{
    [SerializeField] private ButtleModel[] _buttles;

    public ButtleModel[] Buttles => _buttles;
}