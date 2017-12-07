using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

    public enum SenderList // 無線送信元リスト
    {
        SYSTEM = 0, // システムメッセージ
        POLICE, // 警察本部
        COMMAND, // 通信指令部
        POLICECAR // パトカー
    }

    Player player;
    public Slider energyGage;

    public Image energyFull;
    public Image energyCaution;
    public Image energyDanger;

    public string readRadioName;
    ActionLog radioLog = new ActionLog();

	// Use this for initialization
	void Start () {
        player = GameObject.Find("PlayerController").GetComponent<Player>();
        energyGage = GameObject.Find("EnergyGage").GetComponent<Slider>();
        energyGage.maxValue = player.maxEnergy;
        //EnergyStatusInit();
        radioLog.CsvRead(readRadioName);
        Debug.Log(radioLog.radioCsvDatas[0][1]);

    }
	
	// Update is called once per frame
	void Update () {
        energyGage.value = player.energy;
		
	}

    void EnergyStatusInit()
    {
    }

    public void DownEnergy(float downValue)
    {
        energyFull.fillAmount = downValue / player.maxEnergy;
    }

    public void LogShow(int sendWidth,int sendHeight,int width,int height)
    {
        Debug.Log(radioLog.radioCsvDatas[sendWidth][sendHeight] + radioLog.radioCsvDatas[width][height]);
    }

}
