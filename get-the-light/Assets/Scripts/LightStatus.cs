using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStatus : MonoBehaviour
{
    [SerializeField]
    private float LightDamage = 1;
    public float Light_currentDamage;

    private void Awake()
    {
        Light_currentDamage = LightDamage;
    }
}