using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour {

    BankManager bankmanager;
    GameObject bankmanagerObj;
    Player player;
    public GameObject thisBankObj;

    public GameObject raidPlayerObj;

    public string thisBankId;
    [SerializeField] private int haveMoney;
    public bool attacked = false;
    public bool raid = false;
    public static bool raidFlg = false;
    [SerializeField]private int getMoney;

	// Use this for initialization
	void Start () {
        bankmanagerObj = GameObject.Find("Status");
        bankmanager = bankmanagerObj.GetComponent<BankManager>();
        haveMoney = bankmanager.PostMoney(int.Parse(thisBankId));
        thisBankObj = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (raid == true)
        {
            getMoney = thisBankRaid();
            haveMoney = haveMoney - getMoney;
            player.GetMoney(getMoney);
            Debug.LogFormat("【{0}】が銀行を襲撃、{1}$奪った", player.playerName, getMoney);
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
