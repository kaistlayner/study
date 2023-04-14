using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsBuildLight { set; get; }
    private void Awake()
    {
        IsBuildLight = false;
    }
}
