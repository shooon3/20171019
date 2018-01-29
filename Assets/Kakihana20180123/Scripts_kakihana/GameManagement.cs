using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour {

    /*共有したいゲーム情報を管理するクラス*/
    const int PLAYER_PEOPLE = 4; // プレイヤー人数

    public GameObject player1, player2, player3, player4;
    public GameObject[] playerObj = new GameObject[PLAYER_PEOPLE];
    public Transform playerTrans1, playerTrans2, playerTrans3, playerTrans4;
    public GameObject Guardian1, Guardian2, Guardian3;
    public GameObject Misdeed1;
    [SerializeField]
    private PlayerController[] playerInfo = new PlayerController[PLAYER_PEOPLE];
    GUIManager guimanager;

    public int[] playerId = new int[PLAYER_PEOPLE]; // プレイヤーID
    public string[] playerName = new string[PLAYER_PEOPLE]; // プレイヤー名
    public int[] playerHp = new int[PLAYER_PEOPLE]; // プレイヤーHP
    public bool[] areyouMisdeed = new bool[PLAYER_PEOPLE]; // ミスディードかどうか

    public int citizenGroupMoney; // 市民側の所持金
    public int terroGroupMoney; // テロリスト側の所持金

    public int count = 0; // 経過時間
    const int timeLimit = 300; // 制限時間

    public int logCount = 0;
    public int startCount = 0;
    int startCountLimit = 10;

    public List<bool> isStun = new List<bool>(); // 気絶しているか
    public List<bool> isArrest = new List<bool>(); // 確保されたか（テロリスト）
    public List<bool> isRaid = new List<bool>();// 襲撃されたか
    public List<bool> rescue = new List<bool>(); // 気絶状態から回復行動をしているプレイヤーを検知 

    public bool startFlg = false;

	// Use this for initialization
	void Start () {
        GameObject statusObj = GameObject.Find("Status");
        guimanager = statusObj.GetComponent<GUIManager>();
        PlayerInfoInit();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.frameCount % 60 ==0)
        {
            startCount++;
        }
        if (startCount >= 1 && logCount == 0)
        {
            guimanager.LogShow((int)GUIManager.SenderList.POLICE, 0, (int)GUIManager.SenderList.POLICE, 1);
            logCount++;
        }
        if (startCount >= 3 && logCount == 1)
        {
            guimanager.LogShow((int)GUIManager.SenderList.POLICE, 0, (int)GUIManager.SenderList.POLICE, 2);
            logCount++;
        }
        if (startCount >= 5 && logCount == 2)
        {
            guimanager.LogShow((int)GUIManager.SenderList.COMMAND, 0, (int)GUIManager.SenderList.COMMAND, 1);
            logCount++;
        }
        if (startCount >= 7 && logCount == 3)
        {
            guimanager.LogShow((int)GUIManager.SenderList.POLICECAR, 0, (int)GUIManager.SenderList.POLICECAR, 1);
            logCount++;
        }
        if (startCount >= startCountLimit)
        {
            startFlg = true;
        }
        if (startFlg == true)
        {
            if (Time.frameCount % 60 == 0) // １秒毎にcountを足す
            {
                count++;
            }
            if (count >= timeLimit) // countが300（5分）超えるとゲーム終了
            {
                // ゲーム終了メソッド
                GameFinish();
            }
        }
		
	}

    public void PlayerInfoInit()
    {
        playerObj[0] = GameObject.Find("Player");
        playerInfo[0] = playerObj[0].GetComponent<PlayerController>();
        playerTrans1 = playerInfo[0].transform;
        playerId[0] = playerInfo[0].playerId;
        playerName[0] = playerInfo[0].playerName;
        playerHp[0] = playerInfo[0].charactorHp;

        if (playerInfo[0].tag == "Misdeed")
        {
            areyouMisdeed[0] = true;
            isStun[0] = false;
            isArrest[0] = false;
            isRaid[0] = false;
            isStun[0] = false;
            rescue[0] = false;
        }
        else if(playerInfo[0].tag == "Gurdian")
        {
            areyouMisdeed[0] = false;
            isStun[0] = false;
            isArrest[0] = false;
            isRaid[0] = false;
            isStun[0] = false;
            rescue[0] = false;
        }
        playerInfo[0].orderNum = 0;
    }

    public void StunManager(int orderNum)
    {
        isStun[orderNum] = true;
    }

    public void Rescue(Transform orderNum,float rescueTime)
    {
        if (orderNum.transform.position.magnitude <= )
        {

        }
    }

    public void GameFinish()
    {

    }

}
