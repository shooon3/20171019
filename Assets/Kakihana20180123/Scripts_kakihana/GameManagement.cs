using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : Photon.MonoBehaviour
{

    /*共有したいゲーム情報を管理するクラス*/
    [NonSerialized] public static GameObject[] citizenPlayerInfo = new GameObject[3]; // 市民のプレイヤー情報
    [NonSerialized] public static GameObject[] terroPlayerInfo = new GameObject[1]; // テロリストのプレイヤー情報

    [NonSerialized] public static GUIManager guimanager;

    [NonSerialized] public static int MyNumber;//プレイヤーIDのバッファ

    [NonSerialized] public static int[] playerId = new int[4]; // プレイヤーID
    [NonSerialized] public static string[] playerName = new string[4]; // プレイヤー名
    [NonSerialized] public static int[] playerHp = new int[4]; // プレイヤーHP
    [NonSerialized] public static bool[] areyouTerrorist = new bool[4]; // テロリストかどうか

    [NonSerialized] public static int citizenGroupMoney; // 市民側の所持金
    [NonSerialized] public static int terroGroupMoney; // テロリスト側の所持金

    [NonSerialized] public int count = 0; // 経過時間
    const int timeLimit = 300; // 制限時間

    [NonSerialized] public static int logCount = 0;
    [NonSerialized] public static int startCount = 0;
    int startCountLimit = 10;

    [NonSerialized] public static bool[] isStun = new bool[4]; // 気絶しているか
    [NonSerialized] public static bool[] isArrest = new bool[4]; // 確保されたか（テロリスト）
    [NonSerialized] public static bool[] isRaid = new bool[4]; // 襲撃されたか
    [NonSerialized] public static bool[] respawn = new bool[4]; // 復活待機

    [NonSerialized] public bool startFlg = false;

    private PhotonView m_photon_manege;

    private void Start()
    {
        guimanager = GetComponent<GUIManager>();
    }

    // Use this for initialization
    public void InstanceManage ()
    {
        terroPlayerInfo = GameObject.FindGameObjectsWithTag("Terrorist");
        citizenPlayerInfo = GameObject.FindGameObjectsWithTag("Citizen");
        m_photon_manege = GetComponent<PhotonView>();
        Debug.Log(MyNumber);
        playerId[MyNumber - 1] = MyNumber;
        playerName[MyNumber - 1] = "Player" + MyNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 60 == 0)
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
        Debug.Log(playerName[MyNumber - 1]);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        for (int i = 0; i < playerId.Length; i++)
        {
            if (playerId[i] == 0 || (int)stream.ReceiveNext() == 0)
                return;

            if (stream.isWriting)
            {

                if (playerId[i] == 0)
                    return;

                stream.SendNext(playerId[i]);
                stream.SendNext(playerName[i]);
                stream.SendNext(playerHp[i]);
                stream.SendNext(areyouTerrorist[i]);

            }
            else
            {
                if ((int)stream.ReceiveNext() == 0)
                    return;

                playerId[i] = (int)stream.ReceiveNext();
                playerName[i] = (string)stream.ReceiveNext();
                playerHp[i] = (int)stream.ReceiveNext();
                areyouTerrorist[i] = (bool)stream.ReceiveNext();

            }
        }
    }

    void GameFinish()
    {

    }
}
