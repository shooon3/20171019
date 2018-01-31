using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour {

    /*銀行ごとのクラス*/

    PlayerController player;

    BankManager bankmanager; // 銀行管理クラス
    GUIManager guimanager;
    GameManagement gm;
    GameObject bankmanagerObj; // 銀行管理クラスのオブジェクト

    public GameObject thisBankObj; // 自分の銀行のオブジェクト

    public GameObject raidPlayerObj; // 襲撃したプレイヤーのオブジェクト

    public string thisBankId; // 自分のID
    [SerializeField] private int haveMoney; // 所持金
    public bool attacked = false; // 襲撃されたか
    public bool raid = false; // 襲撃フラグ
    public static bool raidFlg = false; // 襲撃フラグ（銀行管理クラス用
    [SerializeField]private int getMoney; // 奪われる金額

	// Use this for initialization
	void Start () {
        bankmanagerObj = GameObject.Find("Status"); // Statusの名前がついているオブジェクトを参照し取得
        bankmanager = bankmanagerObj.GetComponent<BankManager>(); // オブジェクトから銀行管理クラスコンポーネントを取得
        guimanager = bankmanagerObj.GetComponent<GUIManager>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameManagement>();
        raidPlayerObj = GameObject.FindGameObjectWithTag("Misdeed");
        haveMoney = bankmanager.PostMoney(int.Parse(thisBankId)); // 銀行管理クラスからIDと所持金を取得
        thisBankObj = this.gameObject;
        gm.SetMoney(haveMoney);
	}
	
	// Update is called once per frame
	void Update () {
        if (raidFlg)
        {
            Raid();
            raidFlg = false;
        }
        if (raid == true)
        {
            getMoney = thisBankRaid();
            haveMoney = haveMoney - getMoney;
            gm.RaidMoney(getMoney);
            guimanager.PlayerInfulenceLogShow(
                (int)GUIManager.SenderList.SYSTEM, 0,
                player.playerName,
                (int)GUIManager.SenderList.SYSTEM, 4,
                getMoney,
                (int)GUIManager.SenderList.SYSTEM, 6
                );
            raid = false;
        }
    }

    public int thisBankRaid()
    {
        int money;
        money = bankmanager.Raid(haveMoney);
        return money;
    }

    public void SetObj(GameObject obj)
    {
        raidPlayerObj = obj;
        player = raidPlayerObj.GetComponent<PlayerController>();
    }

    public void Raid()
    {
        bankmanager.RaidCheck(int.Parse(thisBankId));
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Misdeed")
        {
            SetObj(col.gameObject);
        }

    }
}
