﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{

    /*共有したいゲーム情報を管理するクラス*/
    const int PLAYER_PEOPLE = 4; // プレイヤー人数

    public GameObject[] playerObj = new GameObject[PLAYER_PEOPLE]; // 参照するプレイヤー
    public GameObject[] uniqueObj = new GameObject[PLAYER_PEOPLE]; // プレイヤーを識別するための子オブジェクト
    public GameObject escapeObj;
    public Transform[] playerTrans = new Transform[PLAYER_PEOPLE]; // プレイヤーの座標
    [SerializeField]
    private PlayerController[] playerInfo = new PlayerController[PLAYER_PEOPLE];
    GUIManager guimanager;
    Raid raid;

    public int[] playerId = new int[PLAYER_PEOPLE]; // プレイヤーID
    public string[] playerName = new string[PLAYER_PEOPLE]; // プレイヤー名
    public int[] playerHp = new int[PLAYER_PEOPLE]; // プレイヤーHP
    public bool[] areyouMisdeed = new bool[PLAYER_PEOPLE]; // ミスディードかどうか

    public int guardianGroupMoney; // ガーディアン側の所持金
    public int misdeedGroupMoney; // ミスディード側の所持金

    public int count = 0; // 経過時間
    const int timeLimit = 300; // 制限時間

    public int logCount = 0; // 一度しかメッセージが流れないようにログ出力をカウント（警察サイド用）
    public int escapeLogCount = 0; // 一度しかメッセージが流れないようにログ出力をカウント（ミスディード用）
    public int startCount = 0;
    int startCountLimit = 10;

    public int raidCount = 0;

    public List<bool> isStun = new List<bool>(); // 気絶しているか
    public List<bool> isArrest = new List<bool>(); // 確保されたか（テロリスト）
    public List<bool> isRaid = new List<bool>();// 襲撃されたか
    public List<bool> rescue = new List<bool>(); // 気絶状態から回復行動をしているプレイヤーを検知 

    public bool startFlg = false;
    public int finishLogCount = 0;

	// Use this for initialization
	void Start () {
        GameObject statusObj = GameObject.Find("Status");
        guimanager = statusObj.GetComponent<GUIManager>();
        raid = statusObj.GetComponent<Raid>();
        PlayerInfoInit();
        PlayerMoneyInit();
        escapeObj.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.frameCount % 60 ==0)
        {
            startCount++;
            raidCount = raid.raidCount;
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
            if (count >= timeLimit && finishLogCount == 0) // countが300（5分）超えるとゲーム終了
            {
                // ゲーム終了メソッド
                GameOverFinish();
            }
        }
        if (raidCount >= 2 && escapeLogCount == 0)
        {
            // ここに逃走車接近のログを出す
            escapeLogCount++;
            
        }
        if (raidCount >= 4 && escapeLogCount == 1)
        {
            // ここに逃走車が警戒区域内に入るログを出す
            Debug.Log("逃走車両が侵入");
            escapeObj.SetActive(true);
            escapeLogCount++;
        }

    }

    public void PlayerInfoInit() // ユニークオブジェクトより各プレイヤーの情報を格納
    {
        int i = 0;
        for (i = 0; i < PLAYER_PEOPLE; i++)
        {
            switch (i)
            {
                case 0:
                    uniqueObj[i] = GameObject.Find("UniqueNo1");
                    break;
                case 1:
                    uniqueObj[i] = GameObject.Find("UniqueNo2");
                    break;
                case 2:
                    uniqueObj[i] = GameObject.Find("UniqueNo3");
                    break;
                case 3:
                    uniqueObj[i] = GameObject.Find("UniqueNo4");
                    break;
            }
            playerObj[i] = uniqueObj[i].transform.parent.gameObject;
            playerInfo[i] = playerObj[i].GetComponent<PlayerController>();
            playerTrans[i] = playerInfo[i].transform;
            playerId[i] = playerInfo[i].playerId;
            playerName[i] = playerInfo[i].playerName;
            playerHp[i] = playerInfo[i].charactorHp;
            if (playerInfo[i].tag == "Misdeed")
            {
                areyouMisdeed[i] = true;
                isStun[i] = false;
                isArrest[i] = false;
                isRaid[i] = false;
                isStun[i] = false;
                rescue[i] = false;
            }
            else if (playerInfo[i].tag == "Gurdian")
            {
                areyouMisdeed[i] = false;
                isStun[i] = false;
                isArrest[i] = false;
                isRaid[i] = false;
                isStun[i] = false;
                rescue[i] = false;
            }
            playerInfo[i].orderNum = i;
        }
    }

    public void PlayerMoneyInit()
    {
        int i;
        for (i = 0; i < PLAYER_PEOPLE; i++)
        {
            switch (playerInfo[i].playerType)
            {
                case 1:
                    playerInfo[i].GroupMoney = guardianGroupMoney;
                    break;
                case 2:
                    playerInfo[i].GroupMoney = misdeedGroupMoney;
                    break;
            }
        }
    }

    public void StunManager(int orderNum)
    {
        isStun[orderNum] = true;
    }

    public void SetMoney(int money)
    {
        guardianGroupMoney += money;
    }

    public void GetMoney()
    {
        int i;
        for (i = 0; i < PLAYER_PEOPLE; i++)
        {
            switch (playerInfo[i].playerType)
            {
                case 1:
                    playerInfo[i].GroupMoney = guardianGroupMoney;
                    break;
                case 2:
                    playerInfo[i].GroupMoney = misdeedGroupMoney;
                    playerInfo[i].charactorMoney = misdeedGroupMoney;
                    name = playerInfo[i].playerName;
                    break;
            }
        }
    }

    public void RaidMoney(int money)
    {
        guardianGroupMoney -= money;
        misdeedGroupMoney += money;
    }

    public void GameOverFinish() // 引き分けメソッド
    {
        guimanager.LogShow(
        (int)GUIManager.SenderList.POLICECAR, 0, (int)GUIManager.SenderList.POLICECAR, 2);
        finishLogCount++;
    }

    public void GuardianWinFinish() // ガーディアン勝利メソッド
    {
        Debug.Log("ガーディアン勝利メソッド検知");
    }

    public void MisdeedWinFinish() // ミスディード勝利メソッド
    {
     guimanager.LogShow(
     (int)GUIManager.SenderList.SYSTEM, 0, (int)GUIManager.SenderList.SYSTEM, 8);
    }

}
