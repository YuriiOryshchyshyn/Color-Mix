using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ButtleModel
{
    [SerializeField] List<PaintModel> _paintsModels;

    public List<PaintModel> PaintsModels => _paintsModels;
}