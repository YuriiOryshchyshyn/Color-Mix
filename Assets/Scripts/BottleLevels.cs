using System.Collections.Generic;
using UnityEngine;

public class BottleLevels : MonoBehaviour
{
    [SerializeField] private List<Level> levels;

    public List<Level> Levels => levels;
}
