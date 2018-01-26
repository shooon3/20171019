﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : Photon.MonoBehaviour
{

    /*共有したいゲーム情報を管理するクラス*/

    public GameObject[] citizenPlayerInfo; // 市民のプレイヤー情報
    public GameObject[] terroPlayerInfo; // テロリストのプレイヤー情報

    public GUIManager guimanager;

    public int MyNumber;//プレイヤーIDのバッファ

    public int[] playerId; // プレイヤーID
    public string[] playerName; // プレイヤー名
    public int[] playerHp; // プレイヤーHP
    public bool[] areyouTerrorist; // テロリストかどうか

    public int citizenGroupMoney; // 市民側の所持金
    public int terroGroupMoney; // テロリスト側の所持金

    public int count = 0; // 経過時間
    const int timeLimit = 300; // 制限時間

    public int logCount = 0;
    public int startCount = 0;
    int startCountLimit = 10;

    [SerializeField] private bool[] isStun; // 気絶しているか
    [SerializeField] private bool[] isArrest; // 確保されたか（テロリスト）
    [SerializeField] private bool[] isRaid; // 襲撃されたか
    [SerializeField] private bool[] respawn; // 復活待機

    public bool startFlg = false;

    public PhotonView m_photon_manege;

	// Use this for initialization
	void Start ()
    {
        terroPlayerInfo = GameObject.FindGameObjectsWithTag("Terrorist");
        citizenPlayerInfo = GameObject.FindGameObjectsWithTag("Citizen");
        playerId = new int[terroPlayerInfo.Length + citizenPlayerInfo.Length];
        m_photon_manege = GetComponent<PhotonView>();

        playerId[MyNumber] = MyNumber;
        playerName[MyNumber] = "Player" + MyNumber;
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
    }

    void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
    {
        if(stream.isWriting)//データの送信
        {
            stream.SendNext(playerId[m_photon_manege.viewID]);
            stream.SendNext(playerName[m_photon_manege.viewID]);
            stream.SendNext(playerHp[m_photon_manege.viewID]);
            stream.SendNext(areyouTerrorist[m_photon_manege.viewID]);
        }
        else//受信
        {
            playerId = (int[])stream.ReceiveNext();
            playerName = (string[])stream.ReceiveNext();
            playerHp = (int[])stream.ReceiveNext();
            areyouTerrorist = (bool[])stream.ReceiveNext();
        }
    }

    public void GameFinish()
    {

    }
}
