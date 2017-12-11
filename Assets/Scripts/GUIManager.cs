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

    [SerializeField] private RectTransform content;
    [SerializeField] private RectTransform originElement;
    [SerializeField] private Text originText;
    [SerializeField] private InputField input;

    public string readRadioName;
    ActionLog radioLog = new ActionLog();

    void Awake()
    {
        originElement.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        player = GameObject.Find("PlayerController").GetComponent<Player>();
        energyGage = GameObject.Find("EnergyGage").GetComponent<Slider>();
        energyGage.maxValue = player.maxEnergy;
        //EnergyStatusInit();
        radioLog.CsvRead(readRadioName);
        Debug.Log(radioLog.radioCsvDatas[0][1]);
        LogShow((int)SenderList.POLICE, 0, (int)SenderList.POLICE, 1);
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

    public void LogShow(int sendWidth,int sendHeight,int textwidth,int textheight)
    {
        input.text = radioLog.radioCsvDatas[sendWidth][sendHeight] + radioLog.radioCsvDatas[textwidth][textheight];
        originText.text = input.text;
        input.text = string.Empty;

        var element = GameObject.Instantiate<RectTransform>(originElement);
        element.SetParent(content, false);
        element.SetAsFirstSibling();
        element.gameObject.SetActive(true);
    }

    public string LogFormat(int sendWidth, int sendHeight, int width, int height)
    {
        string ret = radioLog.radioCsvDatas[sendWidth][sendHeight] + radioLog.radioCsvDatas[width][height];
        return ret;
    }

}
