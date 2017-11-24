using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

    Player player;
    public Slider energyGage;

    public Image energyFull;
    public Image energyCaution;
    public Image energyDanger;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("PlayerController").GetComponent<Player>();
        energyGage = GameObject.Find("EnergyGage").GetComponent<Slider>();
        energyGage.maxValue = player.maxEnergy;
        //EnergyStatusInit();
		
	}
	
	// Update is called once per frame
	void Update () {
        energyGage.value = player.energy;
        //energyFull.fillAmount = player.energy / player.maxEnergy;
		
	}

    void EnergyStatusInit()
    {
        //energyFull = GameObject.Find("EnergyFull").GetComponent<Image>();
        //energyFull.fillAmount = 1;
        //energyCaution = GameObject.Find("EnergyCaution").GetComponent<Image>();
        //energyCaution.fillAmount = 1;
        //energyDanger = GameObject.Find("EnergyDanger").GetComponent<Image>();
        //energyDanger.fillAmount = 1;
    }

    public void DownEnergy(float downValue)
    {
        energyFull.fillAmount = downValue / player.maxEnergy;
    }
}
