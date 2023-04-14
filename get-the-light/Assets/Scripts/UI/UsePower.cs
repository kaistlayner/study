using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsePower : MonoBehaviour
{
    [SerializeField] float energyRequired;
    private Home _Home;
    
    void Awake(){
        _Home = GameObject.FindWithTag("House").GetComponent<Home>();
    }

    public void ClickCard(){
        float currentEnergy = _Home.energy;
        float energyRemain = currentEnergy - energyRequired;

        if(energyRemain >= 0){
            _Home.energy = energyRemain;
            Debug.Log(energyRequired + " power used!");
        }
        else{
            Debug.Log("not enough energy... (" + energyRequired + ")");
        }

    }
}
