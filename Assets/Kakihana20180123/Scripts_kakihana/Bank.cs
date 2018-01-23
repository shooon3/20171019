using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour {

    /*銀行ごとのクラス*/

    BankManager bankmanager; // 銀行管理クラス
    GUIManager guimanager;
    GameObject bankmanagerObj; // 銀行管理クラスのオブジェクト
    Player player; // プレイヤー情報
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
        haveMoney = bankmanager.PostMoney(int.Parse(thisBankId)); // 銀行管理クラスからIDと所持金を取得
        thisBankObj = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (raid == true)
        {
            getMoney = thisBankRaid();
            haveMoney = haveMoney - getMoney;
            player.GetMoney(getMoney);
            guimanager.PlayerInfulenceLogShow(
                (int)GUIManager.SenderList.SYSTEM, 0,
                player.playerName,
                (int)GUIManager.SenderList.SYSTEM, 4,
                getMoney,
                (int)GUIManager.SenderList.SYSTEM, 5
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
        player = raidPlayerObj.GetComponent<Player>();
        if (raidFlg)
        {
            Raid();
        }
    }

    public void Raid()
    {
        bankmanager.RaidCheck(int.Parse(thisBankId));
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Terrorist")
        {
            SetObj(col.gameObject);
            Debug.Log("テロリスト侵入");
        }

    }
}
