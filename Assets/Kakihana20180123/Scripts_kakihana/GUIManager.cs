using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

    /*
    UI関連表示クラス
    */

    public enum SenderList // 無線送信元リスト
    {
        SYSTEM = 0, // システムメッセージ
        POLICE, // 警察本部
        COMMAND, // 通信指令部
        POLICECAR // パトカー
    }

    Player player; // プレイヤー情報
    private PlayerController1 playerInfo1;
    private PlayerController2 playerInfo2;
    private PlayerController3 playerInfo3;
    private PlayerController4 playerInfo4;
    GameManagement gm;
    public GameObject GameMaster;
    public Slider energyGage; // エネルギーゲージ

    public Image energyFull;
    public Image energyCaution;
    public Image energyDanger;

    [SerializeField] private RectTransform content; // 追加するコンテンツ
    [SerializeField] private RectTransform originElement; // 生成する枠
    [SerializeField] private Text originText; // 生成するテキスト
    [SerializeField] private InputField input; // 入力用テキスト

    public string readRadioName; // 参照するCSVファイル名
    ActionLog radioLog = new ActionLog(); // 参照するCSV読み込みスクリプト

    void Awake()
    {
        originElement.gameObject.SetActive(false);
        input.image.color = new Color(0, 0, 0, 0); 
    }

	// Use this for initialization
	void Start () {
        gm = GameMaster.GetComponent<GameManagement>();
        player = GameObject.Find("PlayerController").GetComponent<Player>();
        playerInfo1 = gm.getPlayerInfo1(playerInfo1);
        playerInfo2 = gm.getPlayerInfo2(playerInfo2);
        playerInfo3 = gm.getPlayerInfo3(playerInfo3);
        playerInfo4 = gm.getPlayerInfo4(playerInfo4);
        energyGage = GameObject.Find("EnergyGage").GetComponent<Slider>();
        energyGage.maxValue = player.maxEnergy;
        //EnergyStatusInit();
        radioLog.CsvRead(readRadioName);
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

    public void LogShow(int sendWidth,int sendHeight,int textwidth,int textheight) // 通常ログ出力メソッド
    {
        /*
        【引数詳細】
        LogShow（送信者名横列、送信者名縦列、送信内容横列、送信内容縦列）
        */
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
        // プレイヤーの行動によりイベントが発生した際のログ出力メソッド
        /*
       【引数詳細】
        PlayerInfulenceLogShow（
        送信者名横列、送信者名縦列、
        イベントを起こしたプレイヤー名、
        送信内容横列１、送信内容縦列１、
        イベントにより変化するパラメータ名、
        送信内容横列２、送信内容縦列２
        ）
        */
        input.text = radioLog.radioCsvDatas[sendWidth][sendHeight] + playerName + radioLog.radioCsvDatas[textWidth1][textHeight1] + parameter + radioLog.radioCsvDatas[textWidth2][textHeight2]; // 出力用テキストにメッセージを代入
        originText.text = input.text; // UI画面に出力
        input.text = string.Empty; // 出力用テキストを削除

        var element = GameObject.Instantiate<RectTransform>(originElement); // ログを生成
        element.SetParent(content, false); // コンテンツの親に
        element.SetAsFirstSibling(); // 最前面に
        element.gameObject.SetActive(true); // ログを表示
    }

    public void PlayerAttackLogShow(string attackToPlayer, int textWidth1, int textHeight1, string attackedPlayer, int textWidth2, int textHeight2)
    {
        // プレイヤー同士の行動によりイベントが発生した際のログ出力メソッド
        /*
       【引数詳細】
        PlayerAttackLogShow（
        イベントを起こしたプレイヤー名、
        送信内容横列１、送信内容縦列１、
        イベントにより影響を受けるプレイヤー名、
        送信内容横列２、送信内容縦列２
        ※送信元はシステムで固定されるため送信元の引数は設定しない
        ）
        */
        input.text = radioLog.radioCsvDatas[(int)SenderList.SYSTEM][0] + attackToPlayer + radioLog.radioCsvDatas[textWidth1][textHeight1] + attackedPlayer + radioLog.radioCsvDatas[textWidth2][textHeight2]; // 出力用テキストにメッセージを代入
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
