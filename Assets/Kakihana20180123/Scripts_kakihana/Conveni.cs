﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveni : MonoBehaviour {

    /*銀行ごとのクラス*/

    PlayerController player;

    ConveniManager convenimanager; // 銀行管理クラス
    GUIManager guimanager;
    GameObject convenimanagerObj; // 銀行管理クラスのオブジェクト

    public GameObject thisConveniObj; // 自分の銀行のオブジェクト

    public GameObject raidPlayerObj; // 襲撃したプレイヤーのオブジェクト

    public string thisConveniId; // 自分のID
    [SerializeField] private int haveMoney; // 所持金
    public bool attacked = false; // 襲撃されたか
    public bool raid = false; // 襲撃フラグ
    public static bool raidFlg = false; // 襲撃フラグ（銀行管理クラス用
    [SerializeField] private int getMoney; // 奪われる金額

    // Use this for initialization
    void Start () {
        convenimanagerObj = GameObject.Find("Status"); // Statusの名前がついているオブジェクトを参照し取得
        convenimanager = convenimanagerObj.GetComponent<ConveniManager>(); // オブジェクトから銀行管理クラスコンポーネントを取得
        guimanager = convenimanager.GetComponent<GUIManager>();
        raidPlayerObj = GameObject.FindGameObjectWithTag("Misdeed");
        haveMoney = convenimanager.PostMoney(int.Parse(thisConveniId)); // 銀行管理クラスからIDと所持金を取得
        thisConveniObj = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (raidFlg)
        {
            Raid();
        }
        if (raid == true)
        {
            getMoney = thisBankRaid();
            haveMoney = haveMoney - getMoney;
            player.GetMoney(getMoney);
            guimanager.PlayerInfulenceLogShow(
                (int)GUIManager.SenderList.SYSTEM, 0,
                player.playerName,
                (int)GUIManager.SenderList.SYSTEM, 5,
                getMoney,
                (int)GUIManager.SenderList.SYSTEM, 6
                );
            raid = false;
        }
    }
    public int thisBankRaid()
    {
        int money;
        money = convenimanager.Raid(haveMoney);
        return money;
    }

    public void SetObj(GameObject obj)
    {
        raidPlayerObj = obj;
        player = raidPlayerObj.GetComponent<PlayerController>();
    }

    public void Raid()
    {
        convenimanager.RaidCheck(int.Parse(thisConveniId));
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Misdeed")
        {
            SetObj(col.gameObject);
        }

    }
}
