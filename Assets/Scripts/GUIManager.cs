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

    Player player; // プレイヤー情報
    public Slider energyGage; // エネルギーゲージ

    public Image energyFull;
    public Image energyCaution;
    public Image energyDanger;

    [SerializeField] private RectTransform content; // 追加するコンテンツ
    [SerializeField] private RectTransform originElement; // 生成する枠
    [SerializeField] private Text originText; // 生成するテキスト
    [SerializeField] private InputField input; // 入力用テキスト

    public string readRadioName;
    ActionLog radioLog = new ActionLog(); // 参照するCSV読み込みスクリプト

    void Awake()
    {
        originElement.gameObject.SetActive(false);
        input.image.color = new Color(0, 0, 0, 0); 
    }

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<Player>();
        energyGage = GameObject.Find("EnergyGage").GetComponent<Slider>();
        energyGage.maxValue = player.maxEnergy;
        //EnergyStatusInit();
        radioLog.CsvRead(readRadioName);
        Debug.Log(radioLog.radioCsvDatas[0][1]);
        // 出力テスト用
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

    public void LogShow(int sendWidth,int sendHeight,int textwidth,int textheight) // ログ出力メソッド
    {
        input.text = radioLog.radioCsvDatas[sendWidth][sendHeight] + radioLog.radioCsvDatas[textwidth][textheight]; // 出力用テキストにメッセージを代入
        originText.text = input.text; // UI画面に出力
        input.text = string.Empty; // 出力用テキストを削除

        var element = GameObject.Instantiate<RectTransform>(originElement); // ログを生成
        element.SetParent(content, false); // コンテンツの親に
        element.SetAsFirstSibling(); // 最前面に
        element.gameObject.SetActive(true); // ログを表示
    }

    public void PlayerInfulenceLogShow(int sendWidth,int sendHeight, string playerName, int textWidth1,int textHeight1,int parameter,int textWidth2,int textHeight2)
    {
        input.text = radioLog.radioCsvDatas[sendWidth][sendHeight] + playerName + radioLog.radioCsvDatas[textWidth1][textHeight1] + parameter + radioLog.radioCsvDatas[textWidth2][textHeight2]; // 出力用テキストにメッセージを代入
        originText.text = input.text; // UI画面に出力
        input.text = string.Empty; // 出力用テキストを削除

        var element = GameObject.Instantiate<RectTransform>(originElement); // ログを生成
        element.SetParent(content, false); // コンテンツの親に
        element.SetAsFirstSibling(); // 最前面に
        element.gameObject.SetActive(true); // ログを表示
    }

    public string LogFormat(int sendWidth, int sendHeight, int width, int height)
    {
        string ret = radioLog.radioCsvDatas[sendWidth][sendHeight] + radioLog.radioCsvDatas[width][height];
        return ret;
    }

}
