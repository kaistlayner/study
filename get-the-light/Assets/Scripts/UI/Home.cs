using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    [SerializeField] Slider HpBar;
    private GameObject HpBarFillArea;
    [SerializeField] Slider EnergyBar;
    private GameObject EnergyBarFillArea;
    [SerializeField] Slider ExpBar;
    private GameObject ExpBarFillArea;
    [SerializeField] Text LevelText;

    private GameObject[] Cards;
    private bool isHomeClicked = false;

    private float maximumHp = 100;
    private float currentHp = 100;

    private float maximumEnergy = 100;
    private float currentEnergy = 100;
    public float energy {
        get {return currentEnergy; }
        set {
            currentEnergy = value;
            ShowCards(false);
        }
    }
    private float energyLeakPerDeltaTime = 4;

    private float maximumExp = 100;
    private float currentExp = 0;
    private int level = 1;

    void Awake()
    {
        HpBarFillArea = GameObject.FindWithTag("HpBarFillArea");
        EnergyBarFillArea = GameObject.FindWithTag("EnergyBarFillArea");
        ExpBarFillArea = GameObject.FindWithTag("ExpBarFillArea");
        ExpBarFillArea.SetActive(false);
        Cards = GameObject.FindGameObjectsWithTag("Card");
        ShowCards(false);

        UpdateBar();
    }

    void Update()
    {
        OnEnergyLeaked();
        UpdateBar();
    }

    private void UpdateBar(){
        HpBar.value = currentHp / maximumHp;
        EnergyBar.value = currentEnergy / maximumEnergy;
        ExpBar.value = currentExp / maximumExp;
    }

    public void OnHomeAttacked(float damage){
        if(currentHp > 0) {
            currentHp -= damage;
        }
        else {
            currentHp = 0;
            RemoveHpBar();
            RemoveEnergyBar();
            ShowCards(false);
        }
    }

    public void OnEnergyLeaked(){
        if(currentEnergy > 0) {
            currentEnergy -= energyLeakPerDeltaTime * Time.deltaTime;
        }
        else {
            currentEnergy = 0;
            RemoveEnergyBar();
        }
    }
    
    private void RemoveHpBar(){
        if(HpBarFillArea.activeSelf) Debug.Log("Game Over!!");
        HpBarFillArea.SetActive(false);
    }

    private void RemoveEnergyBar(){
        EnergyBarFillArea.SetActive(false);
    }

    public void OnHomeClick(){
        ShowCards(isHomeClicked);
    }

    private void ShowCards(bool show){
        if(!HpBarFillArea.activeSelf && show){
            Debug.Log("Click reset...");
            return;
        }

        foreach(var Card in Cards){
            Card.SetActive(show);
        }

        isHomeClicked = !isHomeClicked;
    }

    public void OnClickReset(){
        currentEnergy = 100;
        currentHp = 100;
        currentExp = 0;

        HpBarFillArea.SetActive(true);
        EnergyBarFillArea.SetActive(true);
        ExpBarFillArea.SetActive(false);

        ShowCards(false);
        Debug.Log("## Reset Game ##");
    }

    public void OnGetExp(float amount){
        if(!HpBarFillArea.activeSelf) return;
        currentExp += amount;

        if(currentExp >= maximumExp){
            int levelUp = (int)System.Math.Truncate(currentExp / maximumExp);
            level += levelUp;
            currentExp -= (float)levelUp * maximumExp;
            LevelText.GetComponent<Text>().text = "Lv. " + level.ToString();
        }

        if(currentExp == 0) ExpBarFillArea.SetActive(false);
        else ExpBarFillArea.SetActive(true);
    }
}
