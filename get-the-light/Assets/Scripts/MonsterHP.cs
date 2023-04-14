using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHP : MonoBehaviour
{

    [SerializeField]
    private float maxHP = 100;
    private float currentHP;
    GameObject Light;

    private void Start()
    {
        Light = GameObject.FindWithTag("Light");
    }

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= Light.GetComponent<LightStatus>().Light_currentDamage * Time.deltaTime * damage;
        Debug.Log("MonsterHP : " + currentHP);
    }
}
